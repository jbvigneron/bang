using Bang.Domain.Entities;
using Bang.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

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
        [ProducesResponseType(typeof(Role[]), StatusCodes.Status200OK)]
        public Task<IEnumerable<Role>> Get()
        {
            var request = new RolesQuery();
            return this.mediator.Send(request);
        }
    }
}