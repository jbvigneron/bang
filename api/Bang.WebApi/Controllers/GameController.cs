using MediatR;
using Microsoft.AspNetCore.Mvc;
using Bang.Core.Commands;
using Bang.Database.Models;

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

        [HttpPost("create")]
        public async Task<Game> CreateAsync([FromBody] IEnumerable<string> playerNames)
        {
            var request = new CreateGameCommand(playerNames);
            var game = await mediator.Send(request);
            game.Players.ForEach(p => p.Role = null);

            return game;
        }

        [HttpPost("join/{gameId}")]
        public async Task<Player> JoinAsync([FromRoute] Guid gameId, [FromBody] string playerName)
        {
            var request = new JoinGameCommand(gameId, playerName);
            var player = await mediator.Send(request);
            return player;
        }
    }
}
