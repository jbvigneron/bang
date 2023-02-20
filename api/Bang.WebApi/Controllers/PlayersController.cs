using Bang.Core.Queries;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlayersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get player information
        /// </summary>
        [HttpGet("me")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        public Task<Player> GetMeAsync()
        {
            var playerId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var query = new PlayerQuery(playerId);
            return mediator.Send(query);
        }
    }
}
