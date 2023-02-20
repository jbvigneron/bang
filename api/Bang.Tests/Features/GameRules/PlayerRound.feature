#language: fr-FR
Fonctionnalité: Tour d'un joueur

Contexte:
	Sachant qu'une partie est initiée par ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |
	Et que les joueurs rejoignent la partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand c'est au tour de "Jean"

Scénario: Piocher 2 cartes
	Alors "Jean" possède 7 cartes en main

Plan du Scénario: Jouer une carte de type Arme
	Etant donné que "Jean" possède une carte "<weaponName>" dans son jeu
	Quand "Jean" joue une carte "<weaponName>"
	Alors "Jean" place sa carte "<weaponName>" devant lui
	Et "Jean" est armé d'une "<weaponName>" ayant une portée de <range>
	Et "Jean" possède 6 cartes en main

Exemples:
	| weaponName | range |
	| Volcanic   | 1     |
	| Schofield  | 2     |
	| Remington  | 3     |
	| Carabine   | 4     |
	| Winchester | 5     |