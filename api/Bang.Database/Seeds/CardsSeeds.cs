using Bang.Models.Enums;
using Bang.Models;

namespace Bang.Database.Seeds
{
    public static class CardsSeeds
    {
        public static IEnumerable<Card> Fill() => new List<Card>()
            .Concat(Blues())
            .Concat(Browns())
            .Concat(Weapons());

        public static IEnumerable<Card> Blues() => Concat(
            Blue(CardKind.Appalossa, "Lunette", "Vous voyez les autres à une distance réduite de 1.", (int)CardValue.Ace, CardSymbol.Spade),

            Blue(CardKind.Barril, "Planque", "Piochez une carte. Si c'est un coeur = RATE !", (int)CardValue.Queen, CardSymbol.Spade),
            Blue(CardKind.Barril, "Planque", "Piochez une carte. Si c'est un coeur = RATE !", (int)CardValue.King, CardSymbol.Spade),

            Blue(CardKind.Dynamite, "Dynamite", "Piochez une carte. Si c'est un pique compris entre 2 et 9, perdez 3 points de vie, sinon passez la dynamite à votre gauche.", 2, CardSymbol.Heart),

            Blue(CardKind.Jail, "Prison", "Piochez une carte. Si c'est un coeur, défaussez la prison et jouez normalement. Sinon, défaussez la prison et passez votre tour.", 4, CardSymbol.Heart),
            Blue(CardKind.Jail, "Prison", "Piochez une carte. Si c'est un coeur, défaussez la prison et jouez normalement. Sinon, défaussez la prison et passez votre tour.", 10, CardSymbol.Spade),
            Blue(CardKind.Jail, "Prison", "Piochez une carte. Si c'est un coeur, défaussez la prison et jouez normalement. Sinon, défaussez la prison et passez votre tour.", (int)CardValue.Jack, CardSymbol.Spade),

            Blue(CardKind.Mustang, "Mustang", "Les autres vous voient à une distance augmentée de 1.", 9, CardSymbol.Heart),
            Blue(CardKind.Mustang, "Mustang", "Les autres vous voient à une distance augmentée de 1.", 8, CardSymbol.Heart)
        );

        public static IEnumerable<Card> Browns() => Concat(
            Brown(CardKind.Bang, "BANG!", string.Empty, 2, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 3, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 4, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 5, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 6, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 7, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 8, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 9, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 10, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Jack, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.King, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Queen, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Ace, CardSymbol.Diamond, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Queen, CardSymbol.Heart, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.King, CardSymbol.Heart, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Ace, CardSymbol.Heart, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Ace, CardSymbol.Spade, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 2, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 3, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 4, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 5, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 6, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 7, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 8, CardSymbol.Club, true),
            Brown(CardKind.Bang, "BANG!", string.Empty, 9, CardSymbol.Club, true),

            Brown(CardKind.Beer, "Bière", "Rend un point de vie.", 6, CardSymbol.Heart, false),
            Brown(CardKind.Beer, "Bière", "Rend un point de vie.", 7, CardSymbol.Heart, false),
            Brown(CardKind.Beer, "Bière", "Rend un point de vie.", 8, CardSymbol.Heart, false),
            Brown(CardKind.Beer, "Bière", "Rend un point de vie.", 9, CardSymbol.Heart, false),
            Brown(CardKind.Beer, "Bière", "Rend un point de vie.", 10, CardSymbol.Heart, false),
            Brown(CardKind.Beer, "Bière", "Rend un point de vie.", (int)CardValue.Jack, CardSymbol.Heart, false),

            Brown(CardKind.CatBalou, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", 9, CardSymbol.Diamond, true),
            Brown(CardKind.CatBalou, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", 10, CardSymbol.Diamond, true),
            Brown(CardKind.CatBalou, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", (int)CardValue.Jack, CardSymbol.Diamond, true),
            Brown(CardKind.CatBalou, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", (int)CardValue.King, CardSymbol.Heart, true),

            Brown(CardKind.Duel, "Duel", "Le joueur cible se défausse d'un BANG!, puis c'est à vous, etc. Le premier qui ne le fait pas perd un point de vie.", (int)CardValue.Queen, CardSymbol.Diamond, true),
            Brown(CardKind.Duel, "Duel", "Le joueur cible se défausse d'un BANG!, puis c'est à vous, etc. Le premier qui ne le fait pas perd un point de vie.", (int)CardValue.Jack, CardSymbol.Spade, true),
            Brown(CardKind.Duel, "Duel", "Le joueur cible se défausse d'un BANG!, puis c'est à vous, etc. Le premier qui ne le fait pas perd un point de vie.", 8, CardSymbol.Club, true),

            Brown(CardKind.Gatling, "Gatling", "Tout les autres joueurs subissent un BANG!", 10, CardSymbol.Heart, false),

            Brown(CardKind.GeneralStore, "Magasin", "Révélez autant de cartes qu'il y a de joueurs. Chaque joueur en prend une.", (int)CardValue.Queen, CardSymbol.Spade, false),
            Brown(CardKind.GeneralStore, "Magasin", "Révélez autant de cartes qu'il y a de joueurs. Chaque joueur en prend une.", 9, CardSymbol.Club, false),

            Brown(CardKind.Indians, "Indiens", "Tous les autres joueurs défaussent 1 BANG! ou perdent un point de vie.", (int)CardValue.King, CardSymbol.Diamond, false),
            Brown(CardKind.Indians, "Indiens", "Tous les autres joueurs défaussent 1 BANG! ou perdent un point de vie.", (int)CardValue.Ace, CardSymbol.Diamond, false),

            Brown(CardKind.Missed, "RATE!", string.Empty, 2, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 3, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 4, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 5, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 6, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 7, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 8, CardSymbol.Spade, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, 10, CardSymbol.Club, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, (int)CardValue.Ace, CardSymbol.Club, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, (int)CardValue.Jack, CardSymbol.Club, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, (int)CardValue.Queen, CardSymbol.Club, false),
            Brown(CardKind.Missed, "RATE!", string.Empty, (int)CardValue.King, CardSymbol.Club, false),

            Brown(CardKind.Panic, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", (int)CardValue.Jack, CardSymbol.Heart, true),
            Brown(CardKind.Panic, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", (int)CardValue.Queen, CardSymbol.Heart, true),
            Brown(CardKind.Panic, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", (int)CardValue.Ace, CardSymbol.Heart, true),
            Brown(CardKind.Panic, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", 8, CardSymbol.Diamond, true),

            Brown(CardKind.Saloon, "Saloon", "Tout le monde reprend un point de vie.", 5, CardSymbol.Heart, false),

            Brown(CardKind.StageCoach, "Diligence", "Piochez 3 cartes.", 3, CardSymbol.Heart, false),

            Brown(CardKind.WellsFargo, "Convoi", "Piochez 2 cartes.", 9, CardSymbol.Spade, false),
            Brown(CardKind.WellsFargo, "Convoi", "Piochez 2 cartes.", 9, CardSymbol.Spade, false)
        );

        public static IEnumerable<Card> Weapons() => Concat(
            Weapon(CardKind.Carabina, "Carabine", null, 4, (int)CardValue.Ace, CardSymbol.Club),

            Weapon(CardKind.Remington, "Remington", null, 3, (int)CardValue.King, CardSymbol.Club),

            Weapon(CardKind.Schofield, "Schofield", null, 2, (int)CardValue.King, CardSymbol.Spade),
            Weapon(CardKind.Schofield, "Schofield", null, 2, (int)CardValue.Jack, CardSymbol.Club),
            Weapon(CardKind.Schofield, "Schofield", null, 2, (int)CardValue.Queen, CardSymbol.Club),

            Weapon(CardKind.Volcanic, "Volcanic", "Vous pouvez jouer autant de BANG! que vous voulez.", 1, 10, CardSymbol.Spade),
            Weapon(CardKind.Volcanic, "Volcanic", "Vous pouvez jouer autant de BANG! que vous voulez.", 1, 10, CardSymbol.Club),

            Weapon(CardKind.Winchester, "Winchester", null, 5, 8, CardSymbol.Spade)
        );

        private static IEnumerable<T> Concat<T>(params T[] sequences) => sequences;

        private static Card Blue(CardKind kind, string name, string description, int value, CardSymbol symbol)
            => Card(CardType.Blue, kind, name, description, value, symbol, false);

        private static Card Brown(CardKind kind, string name, string description, int value, CardSymbol symbol, bool requireOpponent)
            => Card(CardType.Brown, kind, name, description, value, symbol, requireOpponent);

        private static Card Card(CardType type, CardKind kind, string name, string description, int value, CardSymbol symbol, bool requireOpponent)
            => new()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Kind = kind,
                Name = name,
                Symbol = symbol,
                Type = type,
                Value = (CardValue)value,
                RequireOpponent = requireOpponent
            };

        private static Card Weapon(CardKind kind, string name, string? description, int range, int value, CardSymbol symbol)
            => new()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Kind = kind,
                Name = name,
                Symbol = symbol,
                Type = CardType.Weapon,
                Value = (CardValue)value,
                RequireOpponent = false,
                Range = range
            };
    }
}
