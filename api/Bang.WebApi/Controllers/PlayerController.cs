using Bang.Core.Queries;
using Bang.Database.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlayerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<Player> GetMeAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var request = new PlayerQuery(playerId);
            var player = await mediator.Send(request);
            return player;
        }
    }
}
