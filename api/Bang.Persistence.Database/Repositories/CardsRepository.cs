using Bang.App.Repositories;
using Bang.Domain.Entities;
using System;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class CardsRepository : ICardsRepository
    {
        private readonly BangDbContext context;

        public CardsRepository(BangDbContext context)
        {
            this.context = context;
        }

        public Card Get(Guid id) =>

            this.context.Cards.Single(c => c.Id == id);
    }
}