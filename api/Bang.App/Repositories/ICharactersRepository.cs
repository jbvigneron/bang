using Bang.Domain.Entities;
using System.Collections.Generic;

namespace Bang.App.Repositories
{
    public interface ICharactersRepository
    {
        IEnumerable<Character> Get();

        Character GetRandom();
    }
}