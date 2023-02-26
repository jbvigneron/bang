#language: fr-FR
Fonctionnalité: Authentification

Contexte:
	Sachant qu'une partie est créée avec ces joueurs
		| playerName | characterName  | role        |
		| Jean       | Bart Cassidy   | Schérif     |
		| Max        | Rose Doolan    | Renégat     |
		| Emilie     | Willy le Kid   | Hors-la-loi |
		| Martin     | Sam le Vautour | Hors-la-loi |

Scénario: Authentification par cookie
	Quand "Jean" rejoint la partie en demandant un cookie
	Alors "Jean" est connecté

Scénario: Authentification par header Authorization
	Quand "Jean" s'authentifie en demandant un token JWT
	Alors "Jean" est connecté