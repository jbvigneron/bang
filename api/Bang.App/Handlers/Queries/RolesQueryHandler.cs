using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class RolesQueryHandler : IRequestHandler<RolesQuery, IEnumerable<Role>>
    {
        private readonly IRoleRepository roleRepository;

        public RolesQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public Task<IEnumerable<Role>> Handle(RolesQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(
                this.roleRepository.Get()
            );
    }
}