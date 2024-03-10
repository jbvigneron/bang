using Bang.App.Extensions;
using Bang.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Bang.App.Queries
{
    public class PlayerHandQuery : IRequest<ICollection<Card>>
    {
        public PlayerHandQuery(ClaimsPrincipal user)
        {
            this.PlayerId = user.GetId();
        }

        public Guid PlayerId { get; }
    }
}