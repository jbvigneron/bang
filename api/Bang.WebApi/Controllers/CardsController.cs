using Bang.Core.Commands;
using Bang.Core.Queries;
using Bang.Models;
using Bang.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var query = new PlayerDeckQuery(this.User);
            var cards = await mediator.Send(query);
            return cards;
        }

        [HttpPost("draw")]
        public Task DrawCardsAsync()
        {
            var command = new DrawCardsCommand(this.User);
            return mediator.Send(command);
        }

        [HttpPost("play")]
        public Task PlayCardAsync([FromBody] PlayCardRequest request)
        {
            var command = new PlayCardCommand(this.User, request.CardId, request.OpponentId);
            return mediator.Send(command);
        }
    }
}
