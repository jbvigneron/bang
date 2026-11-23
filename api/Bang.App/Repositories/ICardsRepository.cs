using Bang.Domain.Entities;
using System;

namespace Bang.App.Repositories
{
    public interface ICardsRepository
    {
        Card Get(Guid cardId);
    }
}