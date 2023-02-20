using Bang.Models.Enums;

namespace Bang.WebApi.Requests
{
    public class CreateGameRequest
    {
        public CreateGameRequest()
        {
        }

        public CreateGameRequest(IEnumerable<string> playerNames)
        {
            this.PlayerNames = playerNames;
        }

        public CreateGameRequest(IEnumerable<PlayersInfos> players)
        {
            this.Players = players;
        }

        public IEnumerable<string>? PlayerNames { get; set; }
        public IEnumerable<PlayersInfos>? Players { get; set; }

        public class PlayersInfos
        {
            public string Name { get; set; }
            public CharacterKind CharacterId { get; set; }
            public RoleKind RoleId { get; set; }
        }
    }
}
