Fonctionnalité: Communications entre les joueurs avec SignalR

Contexte:
	Sachant que ces joueurs souhaitent faire une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand une partie est initialisée

Scénario: Quand une nouvelle partie est initialisée
	Alors un message "NewGame" est envoyé au hub public
	Et un message "DeckReady" est envoyé au hub public
	Et le jeu contient 80 cartes

Scénario: Quand un joueur rejoint la partie
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Alors "Jean" reçoit un message "PlayerJoin"
	Et "Max" est prêt

Scénario: Quand le jeu est prêt
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors un message "AllPlayerJoined" est envoyé au hub public
	Et la partie peut commencer
	Et un message "ItsYourTurn" est envoyé au hub public
	Et c'est au tour du schérif