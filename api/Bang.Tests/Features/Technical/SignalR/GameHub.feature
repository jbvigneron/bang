#language: fr-FR
Fonctionnalité: SignalR - Hub du jeu

Contexte:
	Sachant qu'une partie est créée avec ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |
	Quand le hub du jeu est connecté

Scénario: Quand un joueur rejoint la partie
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Alors un message "PlayerJoined" est envoyé au hub du jeu
	Et "Max" est prêt

Scénario: Quand la partie est prête
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors un message "AllPlayerJoined" est envoyé au hub du jeu
	Et la partie peut commencer

Scénario: Quand c'est au tour du premier joueur
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors la partie peut commencer
	Et un message "PlayerTurn" est envoyé au hub du jeu
	Et c'est au tour de "Jean"