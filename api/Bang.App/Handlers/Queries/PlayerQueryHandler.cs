using Bang.App.Queries;
using Bang.App.Repositories;
using Bang.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class PlayerQueryHandler : IRequestHandler<PlayerQuery, Player>
    {
        private readonly IPlayerRepository playerRepository;

        public PlayerQueryHandler(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public Task<Player> Handle(PlayerQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(
                this.playerRepository.Get(request.PlayerId)
            );
    }
}