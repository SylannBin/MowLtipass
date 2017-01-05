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

        public SensDeJeu Sens { get; set; }

        public bool PeutChangerSens { get; set; }

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
        public List<Carte> Pioche { get; set; }

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
                    NomImage = "standard_" + i.ToString() + "_0.jpeg"
                });
            }

            for (int i = 2; i < 15; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 1,
                    TypeDeCarte = TypesDeCarte.standard,
                    NomImage = "standard_" + i.ToString() + "_1.jpeg"
                });
            }

            for (int i = 3; i < 14; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 2,
                    TypeDeCarte = TypesDeCarte.standard,
                    NomImage = "standard_" + i.ToString() + "_2.jpeg"
                });
            }

            for (int i = 7; i < 10; i++)
            {
                listeDesCartes.Add(new Carte()
                {
                    Numero = i,
                    NbMouche = 3,
                    TypeDeCarte = TypesDeCarte.standard,
                    NomImage = "standard_" + i.ToString() + "_3.jpeg"
                });
            }

            listeDesCartes.Add(new Carte() {
                Numero = 0,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.serreFile,
                NomImage = "serreFile_0_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = 16,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.serreFile,
                NomImage = "serreFile_16_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = 7,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.acrobate,
                NomImage = "acrobate_7_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = 9,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.acrobate,
                NomImage = "acrobate_9_5.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = -1,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.retardataire,
                NomImage = "retardataire.jpeg"
            });

            listeDesCartes.Add(new Carte() {
                Numero = -1,
                NbMouche = 5,
                TypeDeCarte = TypesDeCarte.retardataire,
                NomImage = "retardaataire.jpeg"
            });
        }

        #region Melange des cartes

        /// <summary>
        /// Ajoute de manière aléatoire les 48 cartes du jeu dans la pioche
        /// </summary>
        /// <typeparam name="T">Objet de type "Vache" avec trois propriétés ( Valeur - nb_mouches - Categorie )</typeparam>
        private void Shuffle<T>(IList<T> liste)
        {
            // Générateur de vrais nombres aléatoires
            RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

            // Nombre d'élément dans la liste
            int n = liste.Count;

            // pour chaque élément de la liste
            while (n > 1)
            {
                // Conteneur de valeurs aléatoires
                byte[] mixer = new byte[1];
                
                int coef;
                bool tropGrand;

                // Génère une place aléatoire pour l'élément en cours
                do
                {
                    Random.GetBytes(mixer);
                    coef = n * (Byte.MaxValue / n); // pas trop compris
                    tropGrand = mixer[0] >= coef;
                }
                while (tropGrand);


                int k = (mixer[0] % n);
                // décrémente n
                n--;

                // Inverse les éléments en k et n dans la liste
                T tmpValue = liste[k];
                liste[k] = liste[n];
                liste[n] = tmpValue;
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
            Shuffle(Pioche);

            // Jusqu'à ce que la pioche soit complète
            //for (int i=0; i<48; i++)
            //{
            //    // Génère un nombre aléatoire entre 0 et 48 qui ne soit pas déjà utilisé
            //    int randomNumber = -1;
            //    while (numeroUtilises.Contains(randomNumber))
            //    {
            //        randomNumber = randomGenerator.Next(48);
            //    }

            //    // Ajoute à la pioche, la carte située à l'emplacement indiqué par le nombre aléatoire.
            //    Pioche.Push(listeDesCartes.ElementAt(randomNumber));

            //    // Ajoute l'emplacement à la liste des numéros utilisés
            //    numeroUtilises.Add(randomNumber);
            //}   
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
        public bool EstJouable(Carte carteJoueur)
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
        /// Teste si la carte est jouable
        /// Si oui, joue la carte (la met à l'emplacement correct) et renvoie true
        /// Sinon, renvoie false
        /// </summary>
        /// <return>bool</return>
        /// TODO : A relire juste pour voir si c'est cohérent niveau algo
        /// WARNING : Ne refais pas tout si ça marche, Romain.
        public void PlacerCarte(Carte carteJoueur, out bool result)
        {
            if (EstJouable(carteJoueur))
            {
                // retardataire : récupérer l'emplacement (le premier vide entre 2 cartes (intervalle 1)
                if (carteJoueur.TypeDeCarte == TypesDeCarte.retardataire)
                {
                    int index = Troupeau.IndexOf(Troupeau.Where(cartePlacee =>
                         Troupeau.ElementAt(Troupeau.IndexOf(cartePlacee) + 1).Numero == cartePlacee.Numero + 2)
                         .First()) + 1;
                    carteJoueur.Numero = Troupeau.ElementAt(index - 1).Numero + 1;
                    Troupeau.Insert(index, carteJoueur);
                }

                // acrobate : emplacement (carte placée de même numéro)
                else if (carteJoueur.TypeDeCarte == TypesDeCarte.acrobate)
                {
                    int index = Troupeau.IndexOf(Troupeau.Where(cartePlacee => 
                    (cartePlacee.Numero == carteJoueur.Numero))
                    .First()) + 1;
                    Troupeau.Insert(index, carteJoueur);
                }

                // le reste : si plus petite que First => first - 1
                //            si plus grande que Last => Last + 1
                else if (carteJoueur.Numero < Troupeau.First().Numero)
                {
                    Troupeau.Insert(0, carteJoueur);
                }
                else if (carteJoueur.Numero > Troupeau.Last().Numero)
                {
                    Troupeau.Add(carteJoueur);
                }

                result = true;
            }
            // si ce n'est pas jouable 
            else
            {
                result = false;
            }


            // Si l'on a joué une carte avec 5 mouches
            if (carteJoueur.TypeDeCarte != TypesDeCarte.standard)
            {
                PeutChangerSens = true;
            }
            // Sinon ...
            PeutChangerSens = false;
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
            Troupeau = new ObservableCollection<Carte>();
            Pioche = new List<Carte>();
            listerLesCartes();
            MelangerPioche();
        }
    }
}
