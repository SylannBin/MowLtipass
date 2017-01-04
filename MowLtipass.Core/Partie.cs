﻿using System.Collections.Generic;

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
        public int JoueurEnCours { get; set; }

        /// <summary>
        /// Indique dans quel sens le jeu se déroule, et donc quel joueur sera le suivant
        /// </summary>
        public SensDeJeu SensDeJeu { get; set; }


        /// <summary>
        /// Passe au joueur suivant
        /// AntiHoraire (0): Joueur précédent ou dernier joueur de la liste
        /// Horaire (1):     Joueur suivant ou premier joueur de la liste
        /// </summary>
        public void Suivant()
        {
            if (SensDeJeu == SensDeJeu.Horaire)
                JoueurEnCours = (JoueurEnCours == Joueurs.Count - 1)
                    ? 0
                    : JoueurEnCours + 1;
            else
                JoueurEnCours = (JoueurEnCours == 0)
                    ? Joueurs.Count - 1
                    : JoueurEnCours - 1;
        }

        /// <summary>
        /// CONSTRUCTEUR
        /// 
        /// </summary>
        public Partie()
        {
            // Propriétés par défaut
            Joueurs = new List<Joueur>();
            JoueurEnCours = 0;
            SensDeJeu = SensDeJeu.Horaire;
        }




    }
}
