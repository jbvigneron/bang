#language: fr-FR
Fonctionnalité: Tour d'un joueur

Contexte:
	Sachant qu'une partie est créée avec ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |
	Et que tous les joueurs ont rejoint la partie
	Quand c'est au tour de "Jean", il pioche 2 cartes
	Alors "Jean" possède 7 cartes en main

Scénario: Jouer une carte bleue
	Quand "Jean" joue une carte bleue
	Alors "Jean" possède 6 cartes en main