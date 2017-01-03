using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;

namespace MowLtipass.Core
{
    public class Manche
    {
        /* Propriétés */

        public SensDeJeu sens { get; set; } = SensDeJeu.Horaire;   

        /// <summary>
        /// Liste des cartes existantes dans le jeu
        /// </summary>
        private List<Carte> listeDesCartes;

        /// <summary>
        /// Liste des numéros qui sont déjà dans la pioche
        /// </summary>
        private List<int> numeroUtilises;

        /// <summary>
        /// Mélangé au début de chaque manche.
        /// Lorsque la pioche est vide, le premier joueur qui ramasse le troupeau met fin à la manche.
        /// </summary>
        public Stack<Carte> Pioche { get; set; }

        /// <summary>
        /// Zone de jeu principale, contient au max une carte de chaque Numero
        /// Les Retardataires peuvent être placées entre 2 numéros déjà présents
        /// Exemple entre 8 et 10 déjà présents dans le troupeau
        /// Les Acrobates 7 et 9 peuvent remplacer les cartes Standard 7 et 9 respectivement
        /// </summary>
        public ObservableCollection<Carte> Troupeau { get; set; }


        /* Méthodes */


        /// <summary>
        /// Ajoute les 48 cartes à la liste des cartes du jeu 'listeDesCartes'
        /// - 15 cartes vaches (numérotées de 1 à 15), avec 0 mouche
        /// - 13 cartes vaches (numérotées de 2 à 14), avec 1 mouche
        /// - 11 cartes vaches (numérotées de 3 à 13), avec 2 mouches
        /// - 3 cartes vaches (numérotées 7, 8, 9), avec 3 mouches
        /// - 2 serre-files
        /// - 2 vaches acrobates
        /// - 2 vaches retardataires
        /// </summary>
        public void listerLesCartes()
        {
            listeDesCartes = new List<Carte>();

            for (int i = 1; i < 16; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 0,
                    TypeDeCarte = TypesDeCarte.standard,
                    CheminImage = "standard_" + i.ToString() + "_0.jpeg"
                });
            }

            for (int i = 2; i < 15; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 1,
                    TypeDeCarte = TypesDeCarte.standard,
                    CheminImage = "standard_" + i.ToString() + "_1.jpeg"
                });
            }

            for (int i = 3; i < 14; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 2,
                    TypeDeCarte = TypesDeCarte.standard,
                    CheminImage = "standard_" + i.ToString() + "_2.jpeg"
                });
            }

            for (int i = 7; i < 10; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 3,
                    TypeDeCarte = TypesDeCarte.standard,
                    CheminImage = "standard_" + i.ToString() + "_3.jpeg"
                });
            }

            listeDesCartes.Add(new Carte() {
                Numero = 0,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.serreFile,
                CheminImage = "serreFile_0_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = 16,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.serreFile,
                CheminImage = "serreFile_16_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = 7,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.acrobate,
                CheminImage = "acrobate_7_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = 9,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.acrobate,
                CheminImage = "acrobate_9_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = -1,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.retardataire,
                CheminImage = "retardataire.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = -1,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.retardataire,
                CheminImage = "retardaataire.jpeg"
            });
        }

        #region Melange des cartes
        /// <summary>
        /// Renvoie un nombre aléatoire compris entre min inclu et max exclu
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>int</returns>
        public RNGCryptoServiceProvider _RNG = new RNGCryptoServiceProvider();
        public int GetRnd(int min, int max)
        {
            byte[] rndBytes = new byte[4];
            _RNG.GetBytes(rndBytes);
            int rand = BitConverter.ToInt32(rndBytes, 0);
            const Decimal OldRange = (Decimal)int.MaxValue - (Decimal)int.MinValue;
            Decimal NewRange = max - min;
            Decimal NewValue = ((Decimal)rand - (Decimal)int.MinValue) / OldRange * NewRange + (Decimal)min;
            return (int)NewValue;
        }

        /// <summary>
        /// Ajoute de manière aléatoire les 48 cartes du jeu dans la pioche
        /// </summary>
        /// <typeparam name="T">Objet de type "Vache" avec trois propriétés ( Valeur - nb_mouches - Categorie )</typeparam>
        private void Shuffle<T>(IList<T> listeDesCartes)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = listeDesCartes.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];

                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));

                int k = (box[0] % n);
                n--;

                T tmpValue = listeDesCartes[k];
                listeDesCartes[k] = listeDesCartes[n];
                listeDesCartes[n] = tmpValue;
            }
        }

        /// <summary>
        /// Ajoute de manière aléatoire les 48 cartes du jeu dans la pioche
        /// </summary>
        public void MelangerPioche()
        {
            // Générateur de nombre aléatoire
            Random randomGenerator = new Random();

            // revide la pioche au cas où
            Pioche.Clear();

            // Jusqu'à ce que la pioche soit complète
            for (int i=0; i<48; i++)
            {
                // Génère un nombre aléatoire entre 0 et 48 qui ne soit pas déjà utilisé
                int randomNumber = -1;
                while (numeroUtilises.Contains(randomNumber))
                {
                    randomNumber = randomGenerator.Next(48);
                }

                // Ajoute à la pioche, la carte située à l'emplacement indiqué par le nombre aléatoire.
                Pioche.Push(listeDesCartes.ElementAt(randomNumber));

                // Ajoute l'emplacement à la liste des numéros utilisés
                numeroUtilises.Add(randomNumber);
            }   
        }
        #endregion

        /// <summary>
        /// Renvoie vrai si toutes les places du troupeau sont occupées (de 0 à 16).
        /// </summary>
        public bool TroupeauComplet()
        {
            return Troupeau.Count == 19; //  1 2 3 <> 5 6 77  99 10 11 12 13 14 15 16
        }

        /// <summary>
        /// Renvoie vrai, si la carte peut être placée dans le troupeau, faux sinon.
        /// Elimine les cas "vrai", puis renvoie faux par défaut
        /// </summary>
        /// <param name="carteJoueur">La carte que l'on essaie de jouer</param>
        /// <returns>bool</returns>
        public bool TestJouabilite(Carte carteJoueur)
        {
            // Ex Troupeau:      5 <> 7 <> 9 10 12 13
            // Parcourt chaque carte placée dans le troupeau
            foreach (Carte cartePlacee in Troupeau)
            {
                // Si la carte est acrobate et son numéro est égal à 7 OU à 9
                if (carteJoueur.TypeDeCarte == TypesDeCarte.acrobate && carteJoueur.Numero == cartePlacee.Numero)
                {
                    return true;
                }
                
                // Si la carte est retardataire, elle doit s'inserer entre 2 cartes (où l'intervalle vaut 1)
                if (carteJoueur.TypeDeCarte == TypesDeCarte.retardataire &&
                    Troupeau.ElementAt(Troupeau.IndexOf(cartePlacee) + 1).Numero == cartePlacee.Numero + 2)
                {
                    return true;
                }
            }

            // La carte standard que l'on essaie de jouer est bien plus grande que la plus grande du troupeau
            //                                                  OU plus petite que la plus petite du troupeau
            if (carteJoueur.Numero < Troupeau.First().Numero || carteJoueur.Numero > Troupeau.Last().Numero)
            {
                return true;
            }

            // Si aucune condition ne permet de valider la carte, on  ne peut pas la jouer
            return false;
        }


        /// <summary>
        /// TODO
        /// </summary>
        public void placerCarte()
        {
            // retardataire : récup
        }




        /// <summary>
        /// Retourne Vrai si la pioche ne contient aucune carte, faux dans le cas contraire
        /// </summary>
        /// <returns>bool</returns>
        public bool PiocheVide()
        {
            return Pioche.Count == 0;
        }

        /// <summary>
        /// CONSTRUCTEUR
        /// Initialise le Troupeau, la pioche, le sens de jeu
        /// </summary>
        public Manche()
        {
            SensDeJeu SensDeJeu = SensDeJeu.Horaire;
            Troupeau = new ObservableCollection<Carte>();
            Pioche = new Stack<Carte>();
            listerLesCartes();
            MelangerPioche();
        }
    }
}
