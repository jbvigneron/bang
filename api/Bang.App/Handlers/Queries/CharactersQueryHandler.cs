using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.App.Handlers.Queries
{
    public class CharactersQueryHandler : IRequestHandler<CharactersQuery, IEnumerable<Character>>
    {
        private readonly ICharactersRepository charactersRepository;

        public CharactersQueryHandler(ICharactersRepository charactersRepository)
        {
            this.charactersRepository = charactersRepository;
        }

        public Task<IEnumerable<Character>> Handle(CharactersQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(
                this.charactersRepository.Get()
            );
    }
}