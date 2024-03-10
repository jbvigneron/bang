using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class GameQueryHandler : IRequestHandler<GameQuery, CurrentGame>
    {
        private readonly IGameRepository gameRepository;

        public GameQueryHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public Task<CurrentGame> Handle(GameQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(
                this.gameRepository.Get(request.GameId)
            );
    }
}