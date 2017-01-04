using System.Collections.Generic;
using System.Linq;

namespace MowLtipass.Core
{
    public class Partie
    {
        /// <summary>
        /// Liste des joueurs inscrits au jeu
        /// Vide par défaut
        /// </summary>
        public List<Joueur> Joueurs { get; set; }

        /// <summary>
        /// Joueur qui est en train de joueur
        /// </summary>
        public Joueur JoueurEnCours { get; set; }

        /// <summary>
        /// Indique dans quel sens le jeu se déroule, et donc quel joueur sera le suivant
        /// </summary>
        public SensDeJeu SensDeJeu { get; set; }

        /// <summary>
        /// Ajoute un joueur à la liste des joueurs de la partie s'il reste de la place
        /// </summary>
        public void inscrireJoueur(Joueur joueur)
        {
            if (Joueurs.Count < 5)
                Joueurs.Add(joueur);
        }


        /// <summary>
        /// Attribue à l'objet Joueur en cours, le joueur suivant selon le sens de jeu
        /// et le joueur actuel
        /// </summary>
        public void JoueurSuivant()
        {
            // Position du joueur dans la liste
            int index = Joueurs.IndexOf(JoueurEnCours);

            // Horaire (1): Joueur suivant ou premier joueur de la liste
            if (SensDeJeu == SensDeJeu.Horaire)
                // Si Le joueur courant est le dernier de la liste, on revient à 0, sinon suivant
                JoueurEnCours = (index == Joueurs.Count - 1)
                    ? Joueurs.First()
                    : Joueurs.ElementAt(index + 1);
            // AntiHoraire (0): Joueur précédent ou dernier joueur de la liste
            else
                // Si Le joueur courant est le premier de la liste, on va à la fin de la liste, sinon précédent
                JoueurEnCours = (index == 0)
                    ? Joueurs.Last()
                    : Joueurs.ElementAt(index - 1);
        }


        /// <summary>
        /// Renvoie vrai si au moins un joueur a dépassé un score de 100 points
        /// Faux si aucun joueur n'a atteint ce score
        /// </summary>
        /// <returns></returns>
        public bool Continue()
        {
            // Si au moins 1 joueur a plus de 100 points
            foreach(Joueur joueur in Joueurs)
                if (joueur.Score >= 100)
                    return true;
            // Sinon ...
            return false;
        }

        /// <summary>
        /// CONSTRUCTEUR
        /// 
        /// </summary>
        public Partie()
        {
            // Propriétés par défaut
            Joueurs = new List<Joueur>();
            SensDeJeu = SensDeJeu.Horaire;
        }




    }
}
