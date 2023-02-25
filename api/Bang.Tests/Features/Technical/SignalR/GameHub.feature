#language: fr-FR
Fonctionnalité: SignalR - Hub du jeu

Contexte:
	Sachant que ces joueurs souhaitent faire une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie est initialisée
	Quand le hub du jeu est connecté

Scénario: Quand un joueur rejoint la partie
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Alors un message "PlayerJoin" est envoyé au hub du jeu
	Et "Max" est prêt

Scénario: Quand la partie est prête
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors un message "AllPlayerJoined" est envoyé au hub du jeu
	Et la partie peut commencer
	Et un message "PlayerTurn" est envoyé au hub du jeu
	Et c'est au tour du shérif

Scénario: Quand c'est le tour du premier joueur
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors le shérif pioche de nouvelles cartes
	Et un message "CardsDrawn" est envoyé au hub du jeu