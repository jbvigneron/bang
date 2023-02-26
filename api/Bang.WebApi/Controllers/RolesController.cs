using Bang.Core.Queries;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator mediator;

        public RolesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get all game roles
        /// </summary>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Role>), StatusCodes.Status200OK)]
        public Task<IEnumerable<Role>> Get()
        {
            var request = new RolesQuery();
            return this.mediator.Send(request);
        }
    }
}
