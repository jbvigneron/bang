using Bang.Core.Commands;
using Bang.Core.Queries;
using Bang.Models;
using Bang.WebApi.Models;
using Bang.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CardsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("mine")]
        public async Task<IList<Card>> GetCardsAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var query = new PlayerDeckQuery(playerId);
            var cards = await mediator.Send(query);
            return cards;
        }

        [HttpPost("draw")]
        public Task DrawCardsAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var command = new DrawCardsCommand(playerId);
            return mediator.Send(command);
        }

        [HttpPost("play")]
        public Task PlayCardAsync([FromBody] PlayCardRequest request)
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var command = new PlayCardCommand(playerId, request.Card, request.OpponentId);
            return mediator.Send(command);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("switch")]
        public Task SwitchCard([FromBody] SwitchCardRequest request)
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var command = new SwitchCardCommand(playerId, request.OldCard, request.NewCardName);
            return mediator.Send(command);
        }
    }
}
