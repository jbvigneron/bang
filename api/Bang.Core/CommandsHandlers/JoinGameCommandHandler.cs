using Bang.Core.Commands;
using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Core.Extensions;
using Bang.Core.Notifications;
using Bang.Database;
using Bang.Database.Enums;
using Bang.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bang.Core.CommandsHandlers
{
    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Player>
    {
        private readonly BangDbContext context;
        private readonly IMediator mediator;

        public JoinGameCommandHandler(BangDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Player> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var game = await this.context.Games
                .Include(g => g.Players)
                .FirstAsync(g => g.Id == request.GameId, cancellationToken);

            if (game.GameStatus != GameStatus.WaitingForPlayers)
            {
                throw new GameException("L'identifiant de la partie est incorrect");
            }

            var player = game.Players.First(p => p.Name == request.PlayerName);
            player.Character = await this.GetRandomCharacterAsync(cancellationToken);
            player.Lives = GetLives(player.Character, player.IsScheriff);
            player.Weapon = await this.GetColt45Async(cancellationToken);
            player.Status = PlayerStatus.Alive;

            if (game.Players.All(p => p.Status == PlayerStatus.Alive))
            {
                game.GameStatus = GameStatus.InProgress;
                game.CurrentPlayerName = game.GetScheriff().Name;
            }

            await this.context.SaveChangesAsync(cancellationToken);
            await this.SendEventsAsync(game, player, cancellationToken);

            return player;
        }

        private Task<Weapon> GetColt45Async(CancellationToken cancellationToken) =>
            this.context.Weapons.SingleAsync(w => w.Id == WeaponKind.Colt45, cancellationToken);

        private Task<Character> GetRandomCharacterAsync(CancellationToken cancellationToken) =>
            this.context.Characters.OrderBy(c => Guid.NewGuid()).FirstAsync(cancellationToken);

        private static int GetLives(Character character, bool isScheriff)
        {
            var lives = character.Lives;
            lives += isScheriff ? 1 : 0;
            return lives;
        }

        private async Task SendEventsAsync(Game game, Player player, CancellationToken cancellationToken)
        {
            await this.mediator.Publish(new PlayerReadyEvent(game, player), cancellationToken);

            if (game.GameStatus == GameStatus.InProgress)
            {
                await this.mediator.Publish(new GameReadyEvent(game), cancellationToken);
            }
        }
    }
}
