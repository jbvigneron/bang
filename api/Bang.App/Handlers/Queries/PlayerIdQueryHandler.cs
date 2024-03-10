using Bang.App.Repositories;
using Bang.Domain.Queries;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class PlayerIdQueryHandler : IRequestHandler<PlayerIdQuery, Guid>
    {
        private readonly IGameRepository gameRepository;

        public PlayerIdQueryHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public Task<Guid> Handle(PlayerIdQuery request, CancellationToken cancellationToken)
        {
            var game = this.gameRepository.Get(request.GameId);
            var player = game.Players.Single(p => p.Name == request.PlayerName);

            return Task.FromResult(player.Id);
        }
    }
}