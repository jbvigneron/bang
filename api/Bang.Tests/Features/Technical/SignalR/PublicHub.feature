#language: fr-FR
Fonctionnalité: SignalR - Hub public

Contexte:
	Sachant que ces joueurs souhaitent faire une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand le hub public est connecté
	Et la partie est initialisée

Scénario: Quand une nouvelle partie est créée
	Alors un message "GameCreated" est envoyé au hub public