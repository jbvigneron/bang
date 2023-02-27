using Bang.Models.Enums;

namespace Bang.Core.Admin.Models
{
    public class PlayersInfos
    {
        public PlayersInfos()
        {
            this.Name = string.Empty;
        }

        public string Name { get; set; }
        public CharacterKind CharacterId { get; set; }
        public RoleKind RoleId { get; set; }
    }
}
