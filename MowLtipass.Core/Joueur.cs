using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MowLtipass.Core
{
    public class Joueur
    {
        /* Propriétés */


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
        public Stack<Carte> Etable { get; set; }

        /// <summary>
        /// Score total du joueur au cours d'une partie (init à 0). Ajoute le total nb_mouche à chaque fin de tour
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// Identifiant unique du joueur (numéro de 0 à 4)
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nom du joueur, entré au début de la partie pour un humain et choisit alétoirement dans une liste pour un joueur IA
        /// </summary>
        public virtual string Pseudo { get; set; }



        /* Méthodes */


        /// <summary>
        /// TODO piocher
        /// </summary>
        public void Piocher()
        {

        }


        /// <summary>
        /// TODO jouer carte
        /// </summary>
        /// <param name="carteJouee"></param>
        public void Jouer(Carte carteJouee)
        {

        }


        /// <summary>
        /// TODO ramasser troupeau
        /// </summary>
        public void Ramasser()
        {

        }


        /// <summary>
        /// TODO changer sens de jeu
        /// Alterne le sens de jeu de la manche (Horaire -> AntoHoraire, AntoHoraire -> Horaire)
        /// </summary>
        public void ChangerSensDeJeu()
        {

        }
    }
}
