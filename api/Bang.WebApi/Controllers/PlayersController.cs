using Bang.Domain.Entities;
using Bang.Domain.Extensions;
using Bang.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

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
            var playerId = this.User.GetId();

            var query = new PlayerQuery(playerId);
            return this.mediator.Send(query);
        }
    }
}