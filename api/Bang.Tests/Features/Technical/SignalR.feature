Fonctionnalité: SignalR

Contexte:
	Sachant que ces joueurs souhaitent faire une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand une partie est initialisée

Scénario: Quand une nouvelle partie est créée
	Alors un message "NewGame" est envoyé au hub public
	Et le jeu contient 80 cartes

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
	Et un message "YourTurn" est envoyé au schérif
	Et c'est au tour du schérif

Scénario: Quand les joueurs piochent leurs cartes
	Quand "Jean" rejoint la partie
	Alors un message "DeckReady" est envoyé à "Jean"

Scénario: Quand c'est le tour du premier joueur
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors le schérif pioche de nouvelles cartes
	Et un message "CardsDrawn" est envoyé au hub du jeu
	Et un message "NewCards" est envoyé au schérif