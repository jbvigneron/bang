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

            if (game.GameStatus != GameStatusEnum.WaitingForPlayers)
            {
                throw new GameException("L'identifiant de la partie est incorrect");
            }

            var player = game.Players.First(p => p.Name == request.PlayerName);
            player.Character = await this.GetRandomCharacterAsync(cancellationToken);
            player.Lives = GetLives(player);
            player.Weapon = await this.GetColt45Async(cancellationToken);
            player.Status = PlayerStatusEnum.Alive;

            if (game.Players.All(p => p.Status == PlayerStatusEnum.Alive))
            {
                game.GameStatus = GameStatusEnum.InProgress;
                game.CurrentPlayerName = game.GetScheriff().Name;
            }

            await this.context.SaveChangesAsync(cancellationToken);
            await this.SendEventsAsync(game, player, cancellationToken);

            return player;
        }

        private Task<Character> GetRandomCharacterAsync(CancellationToken cancellationToken)
        {
            return this.context.Characters.OrderBy(c => Guid.NewGuid()).FirstAsync(cancellationToken);
        }

        private static int GetLives(Player player)
        {
            var lives = player.Character.Lives;
            lives += player.IsScheriff ? 1 : 0;
            return lives;
        }

        private Task<Weapon> GetColt45Async(CancellationToken cancellationToken)
        {
            return this.context.Weapons.SingleAsync(w => w.Id == WeaponEnum.Colt45, cancellationToken);
        }

        private async Task SendEventsAsync(Game game, Player player, CancellationToken cancellationToken)
        {
            await this.mediator.Publish(new PlayerReadyEvent(game, player), cancellationToken);

            if (game.GameStatus == GameStatusEnum.InProgress)
            {
                await this.mediator.Publish(new GameReadyEvent(game), cancellationToken);
            }
        }
    }
}
