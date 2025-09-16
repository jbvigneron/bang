#language: fr-FR
Fonctionnalité: SignalR - Hub du joueur

Contexte:
	Sachant qu'une partie est créée avec ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie

Scénario: Quand c'est au tour du premier joueur
	Quand le hub de "Jean" est connecté
	Et c'est au tour de "Jean", il pioche 2 cartes
	Alors un message "YourHand" est envoyé à "Jean"