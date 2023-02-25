namespace Bang.Core.Events
{
    public class WeaponCardPlay : CardPlay
    {
        public WeaponCardPlay(Guid gameId, Guid playerId, Guid cardId)
            : base(gameId, playerId, cardId) { }
    }
}
