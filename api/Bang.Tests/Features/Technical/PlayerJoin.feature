Fonctionnalité: Un joueur rejoint une partie

Contexte:
	Sachant qu'une partie est initialisée avec ces joueurs
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |

Scénario: Avertir qu'un autre joueur vient de rejoindre la partie
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Alors "Jean" est averti que "Max" est présent

Scénario: Le jeu est prêt
	Quand "Jean" rejoint la partie
	Et "Max" rejoint la partie
	Et "Emilie" rejoint la partie
	Et "Martin" rejoint la partie
	Alors la partie peut commencer