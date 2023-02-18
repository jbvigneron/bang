using Bang.Core.Commands;
using Bang.Core.Queries;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator mediator;

        public GameController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> PostAsync([FromBody] IEnumerable<string> playerNames)
        {
            var request = new CreateGameCommand(playerNames);
            var gameId = await mediator.Send(request);
            return gameId;
        }

        [HttpGet("{gameId:guid}")]
        public async Task<Game> GetAsync([FromRoute] Guid gameId)
        {
            var request = new GameQuery(gameId);
            var game = await mediator.Send(request);
            return game;
        }

        [HttpPost("{gameId:guid}")]
        public async Task JoinAsync([FromRoute] Guid gameId, [FromBody] string playerName)
        {
            var request = new JoinGameCommand(gameId, playerName);
            var playerId = await mediator.Send(request);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, playerId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
