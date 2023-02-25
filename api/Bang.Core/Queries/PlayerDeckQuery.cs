using Bang.Core.Extensions;
using Bang.Models;
using MediatR;
using System.Security.Claims;

namespace Bang.Core.Queries
{
    public class PlayerDeckQuery : IRequest<IList<Card>>
    {
        public PlayerDeckQuery(ClaimsPrincipal user)
        {
            this.PlayerId = user.GetId();
        }

        public Guid PlayerId { get; }
    }
}
