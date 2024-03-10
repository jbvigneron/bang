using Bang.Domain.Commands.Game;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using Bang.WebApi.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public GamesController(IMediator mediator, IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.environment = environment;
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        /// <param name="playerNames">
        /// An array with player names.
        /// The array must contain between 4 and 7 names
        /// </param>
        /// <returns>The created game identifier</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGameAsync([FromBody] IEnumerable<string> playerNames)
        {
            var command = new CreateGameCommand(playerNames);
            var gameId = await this.mediator.Send(command);
            return this.Created($"api/games/{gameId}", null);
        }

        /// <summary>
        /// Get a game
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>The associated game</returns>
        [HttpGet("{gameId:guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CurrentGame), StatusCodes.Status200OK)]
        public async Task<CurrentGame> GetAsync([FromRoute] Guid gameId)
        {
            var query = new GameQuery(gameId);
            var game = await this.mediator.Send(query);
            return game;
        }

        /// <summary>
        /// Join a game
        /// </summary>
        /// <param name="gameId">Game id to join</param>
        /// <param name="playerName">Player name requesting to join</param>
        /// <param name="authMode">Authentication mode (1 = Cookie, 2 = JWT). Default is cookie</param>
        [HttpPost("{gameId:guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinAsync([FromRoute] Guid gameId, [FromBody] string playerName, [FromQuery] AuthMode? authMode = AuthMode.Cookie)
        {
            var command = new JoinGameCommand(gameId, playerName);
            var playerId = await this.mediator.Send(command);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, playerName),
                new(ClaimTypes.NameIdentifier, playerId.ToString()),
                new(Domain.Constants.JwtConstants.GameId, gameId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: this.configuration["Jwt:Issuer"],
                audience: this.configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.WriteToken(token);

            if (authMode == AuthMode.Jwt)
            {
                return this.Ok(jwt);
            }

            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = !this.environment.IsDevelopment(),
                IsEssential = true
            };

            this.HttpContext.Response.Cookies.Append("auth", jwt, cookiesOptions);
            return this.Ok();
        }
    }
}