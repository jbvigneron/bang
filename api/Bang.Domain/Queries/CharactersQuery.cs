using Bang.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Bang.Domain.Queries
{
    public class CharactersQuery : IRequest<IEnumerable<Character>>
    {
    }
}