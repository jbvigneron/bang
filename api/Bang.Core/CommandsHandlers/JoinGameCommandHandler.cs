using MediatR;
using Microsoft.EntityFrameworkCore;
using Bang.Core.Commands;
using Bang.Database;
using Bang.Database.Models;
using Bang.Database.Seeds;
using System.Numerics;

namespace Bang.Core.CommandsHandlers
{
    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Player>
    {
        private readonly BangDbContext context;
        private readonly List<PlayerRole> roles;

        public JoinGameCommandHandler(BangDbContext context)
        {
            this.context = context;
        }

        public async Task<Player> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var game = await this.context.Games.Include(g => g.Players).FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

            if (game == null || game.GameStatus != GameStatus.WaitingForPlayers)
            {
                throw new ArgumentException("L'identifiant de la partie est incorrect", nameof(request.GameId));
            }

            var player = game.Players.FirstOrDefault(p => p.Name == request.PlayerName);

            if (player is null)
            {
                throw new ArgumentException("Joueur introuvable", nameof(request.PlayerName));
            }

            player.Character = await this.GetRandomCharacterAsync(cancellationToken);
            player.Lives += player.Character.Lives;
            player.Weapon = await this.GetColt45Async(cancellationToken);

            await this.context.SaveChangesAsync();
            return player;
        }

        private Task<Character> GetRandomCharacterAsync(CancellationToken cancellationToken)
        {
            return this.context.Characters.OrderBy(c => Guid.NewGuid()).FirstAsync(cancellationToken);
        }

        private Task<Weapon> GetColt45Async(CancellationToken cancellationToken)
        {
            return this.context.Weapons.SingleAsync(w => w.Id == WeaponId.Colt45, cancellationToken);
        }
    }
}
