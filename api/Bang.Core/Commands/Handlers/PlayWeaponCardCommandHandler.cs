using Bang.Core.Events;
using Bang.Core.Extensions;
using Bang.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bang.Core.Commands.Handlers
{
    public class PlayWeaponCardCommandHandler : IRequestHandler<PlayWeaponCardCommand>
    {
        private readonly BangDbContext dbContext;
        private readonly IMediator mediator;
        private readonly ILogger<PlayWeaponCardCommand> logger;

        public PlayWeaponCardCommandHandler(BangDbContext dbContext, IMediator mediator, ILogger<PlayWeaponCardCommand> logger)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(PlayWeaponCardCommand request, CancellationToken cancellationToken)
        {
            var playerId = request.User.GetId();
            var gameId = request.User.GetGameId();
            var card = request.Card;

            var hand = this.dbContext.PlayersHands
                .Include(d => d.Cards)
                .Include(d => d.Player!)
                    .ThenInclude(p => p.CardsInGame)
                .Single(p => p.PlayerId == playerId);

            hand.Cards!.Remove(card);
            hand.Player!.CardsInGame!.Add(card);

            var weapon = this.dbContext.Weapons.Single(w => w.Id.ToString() == card.Kind.ToString());
            hand.Player.Weapon = weapon;

            this.dbContext.SaveChanges();

            this.logger.LogInformation("{@Player} replaces weapon with {@Weapon}", hand.Player, weapon);
            await this.mediator.Publish(new WeaponChanged(gameId, playerId, weapon));
        }
    }
}
