Fonctionnalité: Initialisation du jeu
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](Bang.Tests/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

Scénario: Démarrer la partie à 4 joueurs
	Sachant que ces joueurs veulent lancer une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie s'initialise
	Alors il y a un shérif
	Et il y a 3 autres personnes

Scénario: Démarrer la partie à 5 joueurs
	Sachant que ces joueurs veulent lancer une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
		| Anna       |
	Quand la partie s'initialise
	Alors il y a un shérif
	Et il y a 4 autres personnes

Scénario: Démarrer la partie à 6 joueurs
	Sachant que ces joueurs veulent lancer une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
		| Anna       |
		| Eric       |
	Quand la partie s'initialise
	Alors il y a un shérif
	Et il y a 5 autres personnes

Scénario: Démarrer la partie à 7 joueurs
	Sachant que ces joueurs veulent lancer une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
		| Anna       |
		| Eric       |
		| Jane       |
	Quand la partie s'initialise
	Alors il y a un shérif
	Et il y a 6 autres personnes

Scénario: Dévoiler le schérif
	Sachant que ces joueurs veulent lancer une partie
		| playerName |
		| Jean       |
		| Max        |
		| Emilie     |
		| Martin     |
	Quand la partie s'initialise
	Alors le schérif dévoile sa carte
	Et le schérif possède une balle supplémentaire