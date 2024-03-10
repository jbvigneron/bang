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
    public class CharactersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CharactersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get all available characters
        /// </summary>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Character[]), StatusCodes.Status200OK)]
        public Task<IEnumerable<Character>> Get()
        {
            var query = new CharactersQuery();
            return this.mediator.Send(query);
        }
    }
}