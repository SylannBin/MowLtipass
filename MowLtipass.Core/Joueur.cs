using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MowLtipass.Core
{
    public class Joueur
    {
        /* Propriétés */

        // Liste des noms des joueurs IA
        public string[] botNames = { "Jeremy", "Sarah", "Jack", "George", "Marie", "Hasan", "Sung Li", "Nikolaï", "R209",
            "EvilSylann", "Evildji_AC", "WhiteValsov", "EvilDrNate" };

        /// <summary>
        /// Main du joueur. Contient jusqu'à 5 cartes (Vache)
        /// Doit être vidée puis ajoutée à l'étable à chaque fin de tour
        /// </summary>
        public ObservableCollection<Carte> MainDuJoueur { get; set; }

        /// <summary>
        /// Etable du joueur. Contient les cartes ramassées au cours d'un tour.
        /// Doit être vidée à chaque fin de tour après avoir :
        /// 1 - ajouté les cartes de la main (MainDuJoueur.Retirer -> Etable.Ajouter) (pour chaque carte de MainDuJoueur)
        /// 2 - compté le nombre de mouche (Etable.CompterMouches)
        /// </summary>
        public List<Carte> Etable { get; set; }

        /// <summary>
        /// Score total du joueur au cours d'une partie (init à 0). Ajoute le total nb_mouche à chaque fin de tour
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Identifiant unique du joueur (numéro de 0 à 4)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom du joueur, entré au début de la partie pour un humain et choisit alétoirement dans une liste pour un joueur IA
        /// </summary>
        public string Pseudo { get; set; }


        /// <summary>
        /// Type de joueur
        /// </summary>
        public string Race { get; set; }

        /* Méthodes */


        /// <summary>
        /// Prendre la dernière carte de la pioche (il faut que la carte soit enlevée de la stack)
        /// Ajoute cette carte à la main du joueur
        /// </summary>
        public void Piocher(Manche manche)
        {
            if (MainDuJoueur.Count != 0)
            {
                MainDuJoueur.Add(manche.Pioche.Pop());
            }
        }


        /// <summary>
        /// Enlève la carte en paramêtre de la main du joueur, et la place dans le troupeau.
        /// SSI c'est possible (vérifier que l'emplacement est libre)
        /// </summary>
        /// <param name="carteJouee"></param>
        public void Jouer(Manche manche, Carte carteJouee)
        {
            if (manche.EstJouable(carteJouee))
            {
                manche.Troupeau.Add(carteJouee);
                MainDuJoueur.Remove(carteJouee);
                Piocher(manche);
            }
        }


        /// <summary>
        /// Enlève les cartes présentes dans l'ensemble générique de cartes (troupeau ou MainDuJoueur),
        /// les ajoute à l'étable du joueur.
        /// Exécute MAJScore() (compte les mouches et ajoute le résultat au score)
        /// </summary>
        public void Ramasser<T>(T cartes, Manche manche) where T : IList<Carte>
        {
            if(cartes.Count != 0)
            {
                // Compte le score
                MAJScore(cartes);
                // Ajoute les cartes à l'étable
                Etable.AddRange(cartes);
                // Vide l'ensemble de carte (manche.Troupeau ou MainDuJoueur)
                cartes.Clear();
            }
        }


        /// <summary>
        /// Ajoute les mouches d'un ensemble de carte au score du joueur
        /// </summary>
        private void MAJScore<T>(T cartes) where T : IList<Carte>
        {
            foreach (Carte carte in cartes)
            {
                Score += carte.NbMouche;
            }
        }


        /// <summary>
        /// Alterne le sens de jeu de la manche (Horaire -> AntoHoraire, AntoHoraire -> Horaire)
        /// </summary>
        public void ChangerSensDeJeu(Manche manche)
        {
            manche.sens = (manche.sens == SensDeJeu.Horaire) ? SensDeJeu.AntiHoraire : SensDeJeu.Horaire;
        }


        public Joueur(int id, string pseudo, string race)
        {
            this.Id = id;
            this.Pseudo = pseudo;
            this.Race = race;
            this.Score = 0;
            /*
            // Générateur de nombre aléatoire
            Random random = new Random();
        
            // Selon la race, génère un nom ou
            if (Race == "Humain")
            {
                Pseudo = botNames[random.Next(botNames.Length)];
            }
            else
            {
                Pseudo = nom;
            }
            */
        }        
    
    }
}
