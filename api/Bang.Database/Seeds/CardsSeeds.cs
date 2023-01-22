using Bang.Database.Enums;
using Bang.Database.Models;

namespace Bang.Database.Seeds
{
    public static class CardsSeeds
    {
        public static IEnumerable<Card> Fill() => new List<Card>()
            .Concat(Blues())
            .Concat(Browns())
            .Concat(Weapons());

        public static IEnumerable<Card> Blues() => Concat(
            Blue(CardKind.Dynamite, "Dynamite", "Piochez une carte. Si c'est un pique compris entre 2 et 9, perdez 3 points de vie, sinon passez la dynamite à votre gauche.", 2, CardSymbol.Coeur),

            Blue(CardKind.Lunette, "Lunette", "Vous voyez les autres à une distance réduite de 1.", (int)CardValue.Ace, CardSymbol.Pique),

            Blue(CardKind.Mustang, "Mustang", "Les autres vous voient à une distance augmentée de 1.", 9, CardSymbol.Coeur),
            Blue(CardKind.Mustang, "Mustang", "Les autres vous voient à une distance augmentée de 1.", 8, CardSymbol.Coeur),

            Blue(CardKind.Planque, "Planque", "Piochez une carte. Si c'est un coeur = RATE !", (int)CardValue.Queen, CardSymbol.Pique),
            Blue(CardKind.Planque, "Planque", "Piochez une carte. Si c'est un coeur = RATE !", (int)CardValue.King, CardSymbol.Pique),

            Blue(CardKind.Prison, "Prison", "Piochez une carte. Si c'est un coeur, défaussez la prison et jouez normalement. Sinon, défaussez la prison et passez votre tour.", 4, CardSymbol.Coeur),
            Blue(CardKind.Prison, "Prison", "Piochez une carte. Si c'est un coeur, défaussez la prison et jouez normalement. Sinon, défaussez la prison et passez votre tour.", 10, CardSymbol.Pique),
            Blue(CardKind.Prison, "Prison", "Piochez une carte. Si c'est un coeur, défaussez la prison et jouez normalement. Sinon, défaussez la prison et passez votre tour.", (int)CardValue.Jack, CardSymbol.Pique)
        );

        public static IEnumerable<Card> Browns() => Concat(
            Brown(CardKind.Bang, "BANG!", string.Empty, 2, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 3, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 4, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 5, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 6, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 7, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 8, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 9, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, 10, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Jack, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.King, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Queen, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Ace, CardSymbol.Carreau),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Queen, CardSymbol.Coeur),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.King, CardSymbol.Coeur),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Ace, CardSymbol.Coeur),
            Brown(CardKind.Bang, "BANG!", string.Empty, (int)CardValue.Ace, CardSymbol.Pique),
            Brown(CardKind.Bang, "BANG!", string.Empty, 2, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 3, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 4, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 5, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 6, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 7, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 8, CardSymbol.Trefle),
            Brown(CardKind.Bang, "BANG!", string.Empty, 9, CardSymbol.Trefle),

            Brown(CardKind.Biere, "Bière", "Rend un point de vie.", 6, CardSymbol.Coeur),
            Brown(CardKind.Biere, "Bière", "Rend un point de vie.", 7, CardSymbol.Coeur),
            Brown(CardKind.Biere, "Bière", "Rend un point de vie.", 8, CardSymbol.Coeur),
            Brown(CardKind.Biere, "Bière", "Rend un point de vie.", 9, CardSymbol.Coeur),
            Brown(CardKind.Biere, "Bière", "Rend un point de vie.", 10, CardSymbol.Coeur),
            Brown(CardKind.Biere, "Bière", "Rend un point de vie.", (int)CardValue.Jack, CardSymbol.Coeur),

            Brown(CardKind.Braquage, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", (int)CardValue.Jack, CardSymbol.Coeur),
            Brown(CardKind.Braquage, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", (int)CardValue.Queen, CardSymbol.Coeur),
            Brown(CardKind.Braquage, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", (int)CardValue.Ace, CardSymbol.Coeur),
            Brown(CardKind.Braquage, "Braquage", "Piochez une carte à votre voisin de gauche ou de droite.", 8, CardSymbol.Carreau),

            Brown(CardKind.Convoi, "Convoi", "Piochez 2 cartes.", 9, CardSymbol.Pique),
            Brown(CardKind.Convoi, "Convoi", "Piochez 2 cartes.", 9, CardSymbol.Pique),

            Brown(CardKind.CoupDeFoudre, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", 9, CardSymbol.Carreau),
            Brown(CardKind.CoupDeFoudre, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", 10, CardSymbol.Carreau),
            Brown(CardKind.CoupDeFoudre, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", (int)CardValue.Jack, CardSymbol.Carreau),
            Brown(CardKind.CoupDeFoudre, "Coup de Foudre", "Défaussez une carte à n'importe quel joueur.", (int)CardValue.King, CardSymbol.Coeur),

            Brown(CardKind.Diligence, "Diligence", "Piochez 3 cartes.", 3, CardSymbol.Coeur),

            Brown(CardKind.Duel, "Duel", "Le joueur cible se défausse d'un BANG!, puis c'est à vous, etc. Le premier qui ne le fait pas perd un point de vie.", (int)CardValue.Queen, CardSymbol.Carreau),
            Brown(CardKind.Duel, "Duel", "Le joueur cible se défausse d'un BANG!, puis c'est à vous, etc. Le premier qui ne le fait pas perd un point de vie.", (int)CardValue.Jack, CardSymbol.Pique),
            Brown(CardKind.Duel, "Duel", "Le joueur cible se défausse d'un BANG!, puis c'est à vous, etc. Le premier qui ne le fait pas perd un point de vie.", 8, CardSymbol.Trefle),

            Brown(CardKind.Gatling, "Gatling", "Tout les autres joueurs subissent un BANG!", 10, CardSymbol.Coeur),

            Brown(CardKind.Indiens, "Indiens", "Tous les autres joueurs défaussent 1 BANG! ou perdent un point de vie.", (int)CardValue.King, CardSymbol.Carreau),
            Brown(CardKind.Indiens, "Indiens", "Tous les autres joueurs défaussent 1 BANG! ou perdent un point de vie.", (int)CardValue.Ace, CardSymbol.Carreau),

            Brown(CardKind.Magasin, "Magasin", "Révélez autant de cartes qu'il y a de joueurs. Chaque joueur en prend une.", (int)CardValue.Queen, CardSymbol.Pique),
            Brown(CardKind.Magasin, "Magasin", "Révélez autant de cartes qu'il y a de joueurs. Chaque joueur en prend une.", 9, CardSymbol.Trefle),

            Brown(CardKind.Rate, "RATE!", string.Empty, 2, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 3, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 4, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 5, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 6, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 7, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 8, CardSymbol.Pique),
            Brown(CardKind.Rate, "RATE!", string.Empty, 10, CardSymbol.Trefle),
            Brown(CardKind.Rate, "RATE!", string.Empty, (int)CardValue.Ace, CardSymbol.Trefle),
            Brown(CardKind.Rate, "RATE!", string.Empty, (int)CardValue.Jack, CardSymbol.Trefle),
            Brown(CardKind.Rate, "RATE!", string.Empty, (int)CardValue.Queen, CardSymbol.Trefle),
            Brown(CardKind.Rate, "RATE!", string.Empty, (int)CardValue.King, CardSymbol.Trefle),

            Brown(CardKind.Saloon, "Saloon", "Tout le monde reprend un point de vie.", 5, CardSymbol.Coeur)
        );

        public static IEnumerable<Card> Weapons() => Concat(
            Weapon(CardKind.Carabine, "Carabine", null, 4, (int)CardValue.Ace, CardSymbol.Trefle),

            Weapon(CardKind.Remington, "Remington", null, 3, (int)CardValue.King, CardSymbol.Trefle),

            Weapon(CardKind.Schofield, "Schofield", null, 2, (int)CardValue.King, CardSymbol.Pique),
            Weapon(CardKind.Schofield, "Schofield", null, 2, (int)CardValue.Jack, CardSymbol.Trefle),
            Weapon(CardKind.Schofield, "Schofield", null, 2, (int)CardValue.Queen, CardSymbol.Trefle),

            Weapon(CardKind.Volcanic, "Volcanic", "Vous pouvez jouer autant de BANG! que vous voulez.", 1, 10, CardSymbol.Pique),
            Weapon(CardKind.Volcanic, "Volcanic", "Vous pouvez jouer autant de BANG! que vous voulez.", 1, 10, CardSymbol.Trefle),

            Weapon(CardKind.Winchester, "Winchester", null, 5, 8, CardSymbol.Pique)
        );

        private static IEnumerable<T> Concat<T>(params T[] sequences) => sequences;

        private static Card Blue(CardKind kind, string name, string description, int value, CardSymbol symbol)
            => Card(CardType.Blue, kind, name, description, value, symbol);

        private static Card Brown(CardKind kind, string name, string description, int value, CardSymbol symbol)
            => Card(CardType.Brown, kind, name, description, value, symbol);

        private static Card Card(CardType type, CardKind kind, string name, string description, int value, CardSymbol symbol)
            => new()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Kind = kind,
                Name = name,
                Symbol = symbol,
                Type = type,
                Value = (CardValue)value,
            };

        private static Card Weapon(CardKind kind, string name, string? description, int range, int value, CardSymbol symbol)
            => new()
            {
                Id = Guid.NewGuid(),
                Kind = kind,
                Description = description,
                Name = name,
                Symbol = symbol,
                Range = range,
                Type = CardType.Weapon,
                Value = (CardValue)value,
            };
    }
}
