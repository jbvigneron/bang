using Bang.Domain.Entities;
using MediatR;
using System;

namespace Bang.Domain.Events
{
    public sealed class WeaponChanged : INotification
    {
        public WeaponChanged(Guid gameId, Guid playerId, Weapon weapon)
        {
            this.GameId = gameId;
            this.PlayerId = playerId;
            this.Weapon = weapon;
        }

        public Guid GameId { get; }
        public Guid PlayerId { get; }
        public Weapon Weapon { get; }
    }
}