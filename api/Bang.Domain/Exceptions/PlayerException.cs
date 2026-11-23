using Bang.Domain.Entities;
using System;

namespace Bang.Domain.Exceptions
{
    public class PlayerException : Exception
    {
        public PlayerException(string message, Player player) : base(message)
        {
            this.Player = player;
        }

        public Player Player { get; }
    }
}