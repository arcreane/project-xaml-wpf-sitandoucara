# C# - Quiz Application

Une application de quiz développée en C# utilisant .NET MAUI, permettant de répondre à des questions de différents types (choix multiples, vrai ou faux).

## Fonctionnalités

- **Choisir une catégorie** : L'utilisateur peut choisir une catégorie de quiz parmi Mathématique, Histoire, Géographie, et Biologie.
- **Répondre aux questions** : L'utilisateur répond à une série de questions à choix multiples.
- **Vérification des réponses** : Affichage immédiat des réponses correctes ou incorrectes.
- **Récapitulatif** : Affichage d'un récapitulatif des réponses après chaque série de questions.
- **Navigation** : Navigation entre différentes étapes de quiz.

## Étapes de l'exercice

1. **Front-end** : Interface utilisateur développée avec .NET MAUI.
2. **Base de données** : Connexion à une base de données MySQL pour récupérer les questions.
3. **Questions multiples** : Implémentation de questions à choix multiples.
4. **Questions vrai ou faux** : Implémentation de questions vrai ou faux.
5. **Récapitulatif** : Affichage des scores et des réponses correctes/incorrectes.

## Structure du Projet

- **Models**
  - `Question.cs` : Classe abstraite représentant une question.
  - `MultipleChoiceQuestion.cs` : Classe dérivée pour les questions à choix multiples.
  - `TrueFalseQuestion.cs` : Classe dérivée pour les questions vrai ou faux.
  - `AnswerResult.cs` : Classe pour stocker les résultats des réponses.
  - `DatabaseConfig.cs` : Classe de configuration pour la connexion à la base de données.
- **Views**
  - `MainPage.xaml` : Page principale pour choisir la catégorie.
  - `QuizPage.xaml` : Page pour répondre aux questions à choix multiples.
  - `TrueFalsePage.xaml` : Page pour répondre aux questions vrai ou faux.
  - `RecapPage.xaml` : Page pour afficher le récapitulatif des réponses.

## Installation

Pour démarrer avec ce projet, suivez ces étapes :

1. Clonez le repository :

2. Configurez la connexion à la base de données dans `DatabaseConfig.cs` :

   ```csharp
   // Exemple de configuration
   private const string host = "127.0.0.1";
   private const string database = "QuizDatabase";
   private const string user = "root";
   private const string password = "root";
   private const string port = "8889";
   ```

3. Assurez-vous d'avoir MySQL installé et configurez votre base de données avec les scripts SQL fournis.

4. Ouvrez le projet dans Visual Studio.

5. Restaurez les packages NuGet :

   ```bash
   dotnet restore
   ```

6. Déployez l'application sur un simulateur ou un appareil réel.

## Utilisation

1. Choisissez une catégorie de quiz sur la page principale.
2. Répondez aux questions affichées.
3. Consultez le récapitulatif de vos réponses après chaque série de questions.
4. Passez à l'étape suivante du quiz (si implémentée).
