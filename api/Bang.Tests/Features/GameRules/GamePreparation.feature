#language: fr-FR
Fonctionnalité: Préparation du jeu

Contexte:
	Sachant que ces joueurs veulent jouer
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |

Scénario: Partie à 4 joueurs
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a un renégat
	Et il y a 2 hors-la-loi

Scénario: Partie à 5 joueurs
	Sachant que ces joueurs supplémentaires veulent jouer
		| playerName |
		| Anna       |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a un renégat
	Et il y a 2 hors-la-loi
	Et il y a 1 adjoint au shérif

Scénario: Partie à 6 joueurs
	Sachant que ces joueurs supplémentaires veulent jouer
		| playerName |
		| Anna       |
		| Eric       |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a un renégat
	Et il y a 3 hors-la-loi
	Et il y a 1 adjoint au shérif

Scénario: Partie à 7 joueurs
	Sachant que ces joueurs supplémentaires veulent jouer
		| playerName |
		| Anna       |
		| Eric       |
		| Jane       |
	Quand la partie se prépare
	Alors il y a un shérif
	Et il y a un renégat
	Et il y a 3 hors-la-loi
	Et il y a 2 adjoints au shérif

Scénario: Pioche de départ
	Quand la partie se prépare
	Alors la pioche comporte 80 cartes

Scénario: Dévoiler le shérif
	Quand la partie se prépare
	Alors le shérif dévoile sa carte

Scénario: Distribution des personnages
	Quand la partie se prépare
	Et "Max" pioche une carte personnage
	Alors un personnage est attribué à "Max"
	Et le nombre de vies de "Max" lui est attribué selon son personnage et son rôle
		| characterName     | lives |
		| Bart Cassidy      | 4     |
		| Black Jack        | 4     |
		| Calamity Janet    | 4     |
		| El Gringo         | 3     |
		| Jesse Jones       | 4     |
		| Jourdonnais       | 4     |
		| Kit Carlson       | 4     |
		| Lucky Duke        | 4     |
		| Paul Regret       | 3     |
		| Pedro Ramirez     | 4     |
		| Rose Doolan       | 4     |
		| Sid Ketchum       | 4     |
		| Slab le flingueur | 4     |
		| Suzy Lafayette    | 4     |
		| Sam le Vautour    | 4     |
		| Willy le Kid      | 4     |
	Et l arme principale de "Max" est "Colt .45" d'une portée de 1

Scénario: Nombre de cartes en main
	Quand la partie se prépare
	Et "Jean" pioche une carte personnage
	Alors "Jean" possède autant de cartes qu'il a de points de vie
	Quand "Max" pioche une carte personnage
	Alors "Max" possède autant de cartes qu'il a de points de vie
	Quand "Emilie" pioche une carte personnage
	Alors "Emilie" possède autant de cartes qu'il a de points de vie
	Quand "Martin" pioche une carte personnage
	Alors "Martin" possède autant de cartes qu'il a de points de vie

Scénario: Le shérif commence
	Quand la partie se prépare
	Et "Jean" pioche une carte personnage
	Et "Max" pioche une carte personnage
	Et "Emilie" pioche une carte personnage
	Et "Martin" pioche une carte personnage
	Alors c'est au shérif de commencer