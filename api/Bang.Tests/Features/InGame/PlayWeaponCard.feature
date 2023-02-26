#language: fr-FR
Fonctionnalité: Jouer une carte de type Arme

Contexte:
	Sachant qu'une partie est créée avec ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |
	Et que tous les joueurs ont rejoint la partie
	Quand c'est au tour de "Jean", il pioche 2 cartes

Plan du Scénario: S'équiper d'une nouvelle arme
	Etant donné que "Jean" possède une carte "<weaponName>" dans son jeu
	Quand "Jean" joue une carte "<weaponName>"
	Alors "Jean" place sa carte "<weaponName>" devant lui
	Et "Jean" est armé d'une "<weaponName>" ayant une portée de <range>

Exemples:
	| weaponName | range |
	| Volcanic   | 1     |
	| Schofield  | 2     |
	| Remington  | 3     |
	| Carabine   | 4     |
	| Winchester | 5     |