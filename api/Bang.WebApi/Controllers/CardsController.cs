using Bang.Domain.Commands.Game;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using Bang.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            var query = new PlayerHandQuery(this.User);
            var cards = await this.mediator.Send(query);
            return cards;
        }

        [HttpPost("draw")]
        public Task DrawCardsAsync()
        {
            var command = new DrawCardsCommand(this.User);
            return this.mediator.Send(command);
        }

        [HttpPost("play")]
        public Task PlayCardAsync([FromBody] PlayCardRequest request)
        {
            var command = new PlayCardCommand(this.User, request.CardId, request.OpponentId);
            return this.mediator.Send(command);
        }
    }
}