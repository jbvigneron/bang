using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pang.Core.Commands;
using Pang.Database.Models;

namespace Pang.WebApi.Controllers
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
            return game;
        }
    }
}
