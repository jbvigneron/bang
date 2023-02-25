#language: fr-FR
Fonctionnalité: Tour d'un joueur

Scénario: Jouer une carte
	Sachant qu'une partie est lancée avec ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |
	Quand c'est au tour de "Jean", il pioche 2 cartes
	Alors "Jean" possède 7 cartes en main
	Quand "Jean" joue une carte
	Alors "Jean" possède 6 cartes en main