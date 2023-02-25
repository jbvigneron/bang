namespace Bang.Core.Events
{
    public class BlueCardPlay : CardPlay
    {
        public BlueCardPlay(Guid gameId, Guid playerId, Guid cardId)
            : base(gameId, playerId, cardId) { }
    }
}
