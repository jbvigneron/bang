using Bang.Database.Models;

namespace Bang.Database.Seeds
{
    public static class CharactersSeeds
    {
        public static IEnumerable<Character> Data { get; } = new[]
        {
            new Character
            {
                Id = CharacterId.BartCassidy,
                Name = "Bart Cassidy",
                Description = "chaque fois qu’il perd un point de vie, il pioche immédiatement une carte",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.BlackJack,
                Name = "Black Jack",
                Description = "durant la phase 1 de son tour, il doit montrer la seconde carte qu’il a piochée. Si c’est un Cœur ou un Carreau (comme dans le cas d’un « dégaine ! »), il tire une carte supplémentaire (sans la révéler).",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.CalamityJanet,
                Name = "Calamity Janet",
                Description = "elle peut utiliser les cartes *BANG !* comme des cartes *Raté !* et vice-versa. Si elle joue un *Raté !* à la place d’un *BANG !*, elle ne peut pas jouer d’autre carte *BANG !* durant son tour (à moins d’avoir un *Volcanic* en jeu).",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.ElGringo,
                Name = "El Gringo",
                Description = "chaque fois qu’il perd un point de vie à cause d’une carte jouée par un autre joueur, il tire une carte au hasard dans la main de ce dernier (une carte par point de vie). Si ce joueur n’a plus de cartes, dommage, il ne peut pas lui en prendre ! N’oubliez pas que les points vie perdus à cause de la dynamite ne sont pas considérés comme étant causés par un joueur",
                Lives = 3
            },
            new Character
            {
                Id = CharacterId.JessJones,
                Name = "Jess Jones",
                Description = "durant la phase 1 de son tour, il peut choisir soit de piocher la première carte de la pioche, soit de prendre 1 carte au hasard dans la main d’un autre joueur. Il pioche ensuite sa deuxième carte dans la pioche.",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.Jourdonnais,
                Name = "Jourdonnais",
                Description = "on considère qu’il a une *Planque* en jeu à tout moment. Il peut « dégainer ! » quand il est la cible d’un *BANG !*, et s’il tire un cœur, le tir l’a raté. S’il a une autre vraie carte *Planque* en jeu, il peut l’utiliser également, ce qui lui donne deux chances d’annuler un *BANG !* avant d’avoir à jouer un *Raté !*",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.KitCarlson,
                Name = "Kit Carlson",
                Description = "durant la phase 1 de son tour, il regarde les trois premières cartes de la pioche, en choisit 2 qu’il garde et repose la troisième sur la pioche, face cachée.",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.LuckyDuke,
                Name = "Lucky Duke",
                Description = "chaque fois qu’il doit « dégainer », il retourne les deux premières cartes de la pioche et choisit le résultat qu’il préfère. Il défausse ensuite les deux cartes",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.PaulRegret,
                Name = "Paul Regret",
                Description = "on considère qu’il a un *Mustang* en jeu à tout moment : tous les autres joueurs doivent ajouter 1 à la distance qui les sépare de lui. S’il a un autre Mustang réel en jeu, il peut utiliser les deux, ce qui augmente la distance de 2 en tout. ",
                Lives = 3
            },
            new Character
            {
                Id = CharacterId.PedroRamirez,
                Name = "Pedro Ramirez",
                Description = "durant la phase 1 de son tour, il peut choisir de piocher la première carte de la défausse au lieu de la prendre dans la pioche. Il pioche sa seconde carte normalement, dans la pioche.",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.RoseDoolan,
                Name = "Rose Doolan",
                Description = "on considère qu’elle a une *Lunette* en jeu à tout moment : la distance de tous les autres joueurs est réduite de 1 pour elle. Si elle a une autre *Lunette* réelle en jeu, elle peut utiliser les deux, ce qui réduit la distance de tous les autres joueurs de 2 en tout.",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.SidKetchum,
                Name = "Sid Ketchum",
                Description = "à tout moment, il peut défausser 2 cartes de sa main pour regagner 1 point de vie. S’il le désire et si c’est possible, il peut utiliser cette caractéristique plusieurs fois d’affilée. Mais souvenez-vous : il ne peut à aucun moment avoir plus de 4 points de vie",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.SlabLeFlingueur,
                Name = "Slab le flingueur",
                Description = "quand il joue une carte *BANG !* contre un joueur, celui-ci doit dépenser 2 cartes *Raté !* au lieu d’une pour l’annuler. L’effet de la Planque ne compte que pour une carte *Raté !*",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.SuzyLafayette,
                Name = "Suzy Lafayette",
                Description = "dès qu’elle n’a plus aucune carte en main, elle prend une carte dans la pioche",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.SamLeVautour,
                Name = "Sam le Vautour",
                Description = "dès qu’un personnage est éliminé de la partie, Sam prend toutes les cartes que ce joueur avait en main et en jeu, et il les ajoute à sa propre main.",
                Lives = 4
            },
            new Character
            {
                Id = CharacterId.WillyLeKid,
                Name = "Willy le Kid",
                Description = "il peut jouer autant de cartes *BANG !* qu’il le désire pendant son tour.",
                Lives = 4
            }
        };
    }
}
