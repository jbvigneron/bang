using Bang.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class CharactersQuery : IRequest<IEnumerable<Character>>
    {
    }
}
