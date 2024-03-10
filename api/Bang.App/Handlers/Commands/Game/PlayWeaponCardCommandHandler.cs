using Bang.App.Repositories;
using Bang.Domain.Commands.Game;
using Bang.Domain.Events;
using Bang.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Commands.Game
{
    public class PlayWeaponCardCommandHandler : IRequestHandler<PlayWeaponCardCommand>
    {
        private readonly ILogger<PlayWeaponCardCommand> logger;
        private readonly IMediator mediator;

        private readonly IPlayerRepository playerRepository;

        public PlayWeaponCardCommandHandler(
            ILogger<PlayWeaponCardCommand> logger,
            IMediator mediator,
            IPlayerRepository playerRepository)
        {
            this.logger = logger;
            this.mediator = mediator;

            this.playerRepository = playerRepository;
        }

        public async Task Handle(PlayWeaponCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.User.GetId();
            var gameId = request.User.GetGameId();
            var card = request.Card;

            var playerHand = this.playerRepository.GetHand(playerId);
            var newWeapon = this.playerRepository.PlayWeaponCard(playerHand, card);

            this.logger.LogInformation("Player {PlayerName} uses weapon {WeaponName}", playerHand.Player.Name, card.Name);

            await this.mediator.Publish(
                new WeaponChanged(gameId, playerId, newWeapon)
            );
        }
    }
}