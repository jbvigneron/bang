#language: fr-FR
Fonctionnalité: SignalR - Hub du joueur

Contexte:
	Sachant que ces joueurs souhaitent faire une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie est initialisée

Scénario: Quand c'est le tour du premier joueur
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Et le hub du jeu du shérif est connecté
	Alors c'est au tour du shérif
	Et le shérif pioche de nouvelles cartes
	Et un message "CardsInHand" est envoyé au shérif

Scénario: Quand les joueurs piochent leurs cartes
	Quand "Jean" rejoint la partie
	Et le hub de "Jean" est connecté
	Alors un message "CardsInHand" est envoyé à "Jean"