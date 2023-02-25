using Bang.Core.Commands;
using Bang.Core.Queries;
using Bang.Models;
using Bang.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator mediator;

        public GamesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        /// <param name="request">
        ///     2 availables modes :
        ///     - If "playerNames" property is set, characters and roles are distributed randomly
        ///     - If "players" property is set, you must define names, characters and roles for all players 
        /// </param>
        /// <remarks>
        /// 
        /// 2 availables modes :
        ///     - If "playerNames" property is set, characters and roles are distributed randomly
        ///     - If "players" property is set, you must define names, characters and roles for all players 
        /// 
        /// Example with playerNames property:
        /// 
        ///     POST /game
        ///     {
        ///         "playerNames": ["Jean", "Emilie", "Max", "Martin"]
        ///     }
        ///     
        /// Example with players property:
        /// 
        ///     POST /game
        ///     {
        ///         "players": [{
        ///             "name": "Jean",
        ///             "characterId": 3
        ///             "roleId": 0
        ///         },
        ///         {
        ///             "name": "Emilie",
        ///             "characterId": 6
        ///             "roleId": 1
        ///         },
        ///         {
        ///             "name": "Max",
        ///             "characterId": 9
        ///             "roleId": 2
        ///         },
        ///         {
        ///             "name": "Martin",
        ///             "characterId": 5
        ///             "roleId": 2
        ///         }]
        ///     }
        ///     
        /// To get availables values for characterId and roleId properties, call these resources:
        /// - GET api/roles
        /// - GET api/characters
        /// </remarks>
        /// <returns>The created game identifier</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGameAsync([FromBody] CreateGameRequest request)
        {
            if (request.PlayerNames == null && request.Players == null)
            {
                return BadRequest("Please set playerNames or players property");
            }

            IRequest<Guid>? command = null;

            if (request.PlayerNames != null)
            {
                command = new CreateGameCommand(request.PlayerNames);
            }
            else if (request.Players != null)
            {
                command = new CreatePreparedGameCommand(
                    request.Players.Select(p => (p.Name, p.CharacterId, p.RoleId))
                );
            }

            var gameId = await mediator.Send(command!);
            return this.Created($"{this.Request.Path}/{gameId}", null);
        }

        /// <summary>
        /// Get a game
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>The associated game</returns>
        [HttpGet("{gameId:guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        public async Task<Game> GetAsync([FromRoute] Guid gameId)
        {
            var query = new GameQuery(gameId);
            var game = await mediator.Send(query);
            return game;
        }

        /// <summary>
        /// Join a game
        /// </summary>
        /// <param name="gameId">Game id to join</param>
        /// <param name="playerName">Player name requesting to join</param>
        [HttpPost("{gameId:guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task JoinAsync([FromRoute] Guid gameId, [FromBody] string playerName)
        {
            var command = new JoinGameCommand(gameId, playerName);
            var playerId = await mediator.Send(command);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, playerName),
                new Claim(JwtRegisteredClaimNames.NameId, playerId.ToString()),
                new Claim("gameId", gameId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: this.configuration["Jwt:Issuer"],
                audience: this.configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.WriteToken(token);

            var cookiesOptions = new CookieOptions {
                HttpOnly = true,
                Secure = !environment.IsDevelopment(),
                IsEssential = true
            };

            this.HttpContext.Response.Cookies.Append("auth", jwt, cookiesOptions);
        }
    }
}
