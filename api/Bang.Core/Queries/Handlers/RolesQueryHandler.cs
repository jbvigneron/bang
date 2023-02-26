using Bang.Database;
using Bang.Models;
using MediatR;

namespace Bang.Core.Queries.Handlers
{
    public class RolesQueryHandler : RequestHandler<RolesQuery, IEnumerable<Role>>
    {
        private readonly BangDbContext dbContext;

        public RolesQueryHandler(BangDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override IEnumerable<Role> Handle(RolesQuery request)
        {
            return this.dbContext.Roles;
        }
    }
}
