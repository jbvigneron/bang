using Bang.App.Commands.Game;
using Bang.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurnsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TurnsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("end")]
        public Task EndTurnAsync([FromBody] EndTurnRequest request)
        {
            var command = new EndTurnCommand(this.User, request.CardIds);
            return this.mediator.Send(command);
        }
    }
}
