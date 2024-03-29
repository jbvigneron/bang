﻿using Bang.Core.Queries;
using Bang.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
        public Task<Character[]> Get()
        {
            var query = new CharactersQuery();
            return this.mediator.Send(query);
        }
    }
}
