Fonctionnalité: Initialisation du jeu

Scénario: Démarrer la partie à 4 joueurs
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a 3 autres personnes avec un autre rôle

Scénario: Démarrer la partie à 5 joueurs
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
		| Anna       |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a 4 autres personnes avec un autre rôle

Scénario: Démarrer la partie à 6 joueurs
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
		| Anna       |
		| Eric       |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a 5 autres personnes avec un autre rôle

Scénario: Démarrer la partie à 7 joueurs
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
		| Anna       |
		| Eric       |
		| Jane       |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a 6 autres personnes avec un autre rôle

Scénario: Dévoiler le schérif
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie se prépare
	Alors le schérif dévoile sa carte

Scénario: Distribution des personnages
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie se prépare
	Et "Max" pioche une carte personnage
	Alors un personnage est attribué à "Max"
	Et le nombre de vies de "Max" lui est attribué selon son personnage et son rôle
		| characterName    | lives |
		| Bart Cassidy     | 4     |
		| Black Jack       | 4     |
		| Calamity Janet   | 4     |
		| El Gringo        | 3     |
		| Jesse Jones      | 4     |
		| Jourdonnais      | 4     |
		| Kit Carlson      | 4     |
		| Lucky Duke       | 4     |
		| Paul Regret      | 3     |
		| Pedro Ramirez    | 4     |
		| Rose Doolan      | 4     |
		| Sid Ketchum      | 4     |
		| Slab le flingeur | 4     |
		| Suzy Lafayette   | 4     |
		| Sam le vautour   | 4     |
		| Willy le Kid     | 4     |
	Et l arme principale de "Max" est "Colt .45" d'une portée de 1

Scénario: Démarrage de la partie
	Sachant que les joueurs suivants veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie se prépare
	Et "Jean" pioche une carte personnage
	Et "Max" pioche une carte personnage
	Et "Emilie" pioche une carte personnage
	Et "Martin" pioche une carte personnage
	Alors c'est au shérif de commencer