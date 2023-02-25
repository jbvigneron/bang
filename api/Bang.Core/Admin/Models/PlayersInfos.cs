using Bang.Models.Enums;

namespace Bang.Core.Admin.Models
{
    public class PlayersInfos
    {
        public string Name { get; set; }
        public CharacterKind CharacterId { get; set; }
        public RoleKind RoleId { get; set; }
    }
}
