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
	Etant donné que "Jean" possède une carte "Lunette" dans son jeu
	Quand "Jean" joue une carte bleue
	Alors "Jean" possède 6 cartes en main

Scénario: Jouer une carte marron
	Etant donné que "Jean" possède une carte "Bière" dans son jeu
	Quand "Jean" joue une carte "Bière"
	Alors "Jean" possède 6 cartes en main

Scénario: Terminer son tour passe la main au joueur suivant
	Quand "Jean" termine son tour en défaussant 2 cartes
	Alors c'est au tour de "Max"

Scénario: Terminer son tour sans défausser le nombre requis est refusé
	Quand "Jean" tente de terminer son tour en défaussant 0 carte
	Alors la fin de tour est refusée

Scénario: Le tour fait un tour complet et revient au premier joueur
	Quand "Jean" termine son tour en défaussant 2 cartes
	Et c'est au tour de "Max", il pioche 2 cartes
	Et "Max" termine son tour en défaussant 2 cartes
	Et c'est au tour de "Emilie", il pioche 2 cartes
	Et "Emilie" termine son tour en défaussant 2 cartes
	Et c'est au tour de "Martin", il pioche 2 cartes
	Et "Martin" termine son tour en défaussant 2 cartes
	Alors c'est au tour de "Jean"