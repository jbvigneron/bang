using Bang.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Bang.App.Queries
{
    public class CharactersQuery : IRequest<IEnumerable<Character>>
    {
    }
}