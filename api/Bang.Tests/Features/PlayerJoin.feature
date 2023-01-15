Fonctionnalité: Un joueur rejoint une partie

Contexte:
	Sachant que ces joueurs veulent lancer une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie s'initialise

Scénario: Rejoindre une partie
	Quand "Max" veut rejoindre la partie
	Alors un personnage lui est attribué
	Et son nombre de vies lui est attribué selon son personnage
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
	Et le joueur est armé avec "Colt .45" d'une portée de 1