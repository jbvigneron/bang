using Bang.Core.Admin.Commands;
using Bang.WebApi.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = Roles.IntegrationTests)]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("games")]
        public async Task<IActionResult> CreateGameAsync([FromBody] CreatePreparedGameCommand request)
        {
            var gameId = await this.mediator.Send(request);
            return this.Created($"api/games/{gameId}", null);
        }

        [HttpPost("cards/change")]
        public Task ChangeCardAsync([FromBody] ChangeCardCommand request)
        {
            return this.mediator.Send(request);
        }
    }
}
