namespace Bang.Core.Events
{
    public class BrownCardPlay : CardPlay
    {
        public BrownCardPlay(Guid gameId, Guid playerId, Guid cardId, Guid? opponentId)
            : base(gameId, playerId, cardId)
        {
            this.OpponentId = opponentId;
        }

        public Guid? OpponentId { get; }
    }
}
