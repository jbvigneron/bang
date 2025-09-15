using Bang.Models;
using System;

namespace Bang.Core.Exceptions
{
    [Serializable]
    public class PlayerException : Exception
    {
        public PlayerException(string message, Player player) : base(message)
        {
            this.Player = player;
        }

        public Player Player { get; }
    }
}