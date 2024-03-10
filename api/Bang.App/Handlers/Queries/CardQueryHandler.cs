using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class CardQueryHandler : IRequestHandler<CardQuery, Card>
    {
        private readonly ICardsRepository cardsRepository;

        public CardQueryHandler(ICardsRepository cardsRepository)
        {
            this.cardsRepository = cardsRepository;
        }

        public Task<Card> Handle(CardQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(
                this.cardsRepository.Get(request.CardId)
            );
    }
}