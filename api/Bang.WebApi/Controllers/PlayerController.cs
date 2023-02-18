using Bang.Core.Commands;
using Bang.Core.Queries;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlayerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("me")]
        public async Task<Player> GetMeAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var request = new PlayerQuery(playerId);
            var player = await mediator.Send(request);
            return player;
        }

        [HttpGet("cards")]
        public async Task<IList<Card>> GetCardsAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var request = new PlayerDeckQuery(playerId);
            var cards = await mediator.Send(request);
            return cards;
        }

        [HttpPost("cards/draw")]
        public Task DrawCardsAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var request = new DrawCardsCommand(playerId);
            return mediator.Send(request);
        }
    }
}
