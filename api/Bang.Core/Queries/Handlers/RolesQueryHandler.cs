using Bang.Database;
using Bang.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.Queries.Handlers
{
    public class RolesQueryHandler : IRequestHandler<RolesQuery, Role[]>
    {
        private readonly BangDbContext dbContext;

        public RolesQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Role[]> Handle(RolesQuery request, CancellationToken cancellationToken)
        {
            return this.dbContext.Roles.ToArrayAsync(cancellationToken);
        }
    }
}
