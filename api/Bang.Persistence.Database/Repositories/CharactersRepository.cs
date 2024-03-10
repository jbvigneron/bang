using Bang.App.Repositories;
using Bang.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly BangDbContext context;

        public CharactersRepository(BangDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Character> Get() =>
            this.context.Characters;

        public Character GetRandom() =>
            this.context.Characters.OrderBy(c => Guid.NewGuid()).First();
    }
}