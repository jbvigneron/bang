using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BangDbContext context;

        public RoleRepository(BangDbContext context)
        {
            this.context = context;
        }

        public Role Get(RoleKind id) =>
            this.context.Roles.Single(r => r.Id == id);

        public IEnumerable<Role> Get() =>
            this.context.Roles;
    }
}