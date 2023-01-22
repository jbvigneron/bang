Fonctionnalité: Communications entre les joueurs avec SignalR

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
	Et un message "GameDeckReady" est envoyé au hub public
	Et le jeu contient 80 cartes

Scénario: Quand un joueur rejoint la partie
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Alors "Jean" reçoit un message de groupe "PlayerJoin"
	Et "Max" est prêt

Scénario: Quand la partie est prête
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors un message "AllPlayerJoined" est envoyé au hub public
	Et la partie peut commencer
	Et c'est au tour du schérif
	Et un message "ItsYourTurn" est envoyé au schérif