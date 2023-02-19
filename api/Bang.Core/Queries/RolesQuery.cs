using Bang.Models;
using MediatR;

namespace Bang.Core.Queries
{
    public class RolesQuery : IRequest<IEnumerable<Role>>
    {
    }
}
