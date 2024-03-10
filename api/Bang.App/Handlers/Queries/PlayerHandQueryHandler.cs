using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class PlayerHandQueryHandler : IRequestHandler<PlayerHandQuery, ICollection<Card>>
    {
        private readonly IPlayerRepository playerRepository;

        public PlayerHandQueryHandler(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public Task<ICollection<Card>> Handle(PlayerHandQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(
                this.playerRepository.GetHand(request.PlayerId).Cards
            );
    }
}