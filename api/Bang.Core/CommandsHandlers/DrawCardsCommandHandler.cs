using Bang.Core.Commands;
using Bang.Core.Events;
using Bang.Core.Exceptions;
using Bang.Core.Queries;
using Bang.Database.Enums;
using MediatR;

namespace Bang.Core.CommandsHandlers
{
    public class DrawCardsCommandHandler : AsyncRequestHandler<DrawCardsCommand>
    {
        private readonly IMediator mediator;

        public DrawCardsCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override async Task Handle(DrawCardsCommand request, CancellationToken cancellationToken)
        {
            var player = await this.mediator.Send(new PlayerQuery(request.PlayerId), cancellationToken);

            if (player.HasDrawnCards)
            {
                throw new PlayerException("Vous avez déjà pioché vos cartes.", player);
            }

            var game = await this.mediator.Send(new GameQuery(player.GameId), cancellationToken);

            if (game.GameStatus == GameStatus.WaitingForPlayers)
            {
                throw new GameException("La partie n'a pas encore démarré.", game);
            }

            if (game.GameStatus == GameStatus.Finished)
            {
                throw new GameException("La partie est terminée.", game);
            }

            if (game.CurrentPlayerName != player.Name)
            {
                throw new PlayerException("Ce n'est pas à vous de jouer.", player);
            }

            await this.mediator.Publish(new CardsDraw(request.PlayerId), cancellationToken);
        }
    }
}
