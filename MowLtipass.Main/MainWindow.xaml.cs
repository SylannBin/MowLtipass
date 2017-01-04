/**
 * 
 *  MowLtipass, a game to enjoy ! One day in the future, probably, maybe.
 *  Copyright (C) <2017>  <Author: Romain Vincent et Nathan Descombe>
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using MowLtipass.Core;

namespace MowGame.Main
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Chemin vers le dossier contenant les images
        public string DossierImages = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        
        // Indique si l'on peut cliquer sur les cartes ou non
        public bool evenement = false;

        Partie partie = new Partie();
        Manche manche = new Manche();
        Carte carteJoueur = new Carte();
        

        Joueur J1 = new Joueur(id: 0, pseudo: "Romain", race: "Humain");
        Joueur J2 = new Joueur(id: 1, pseudo: "Nathan", race: "Humain");
        Joueur J3 = new Joueur(id: 2, pseudo: "R209", race: "Robot");


        public MainWindow()
        {
            InitializeComponent();

            #region NATHAN Déroulement du jeu
            /**
             * Ajoute ici le déroulement du jeu,
             * jusqu'à la balise #endregion
             * 
             * - fin de partie, de manche
             * - début de partie, de manche -> variables à instancier, constructeurs à appeler
             * - ...
             */
             // TODO : Thread this shit
            // La partie continue tant que Continue return true.
            while (partie.Continue())
            {
                manche.MelangerPioche();
                foreach (Joueur joueur in partie.Joueurs)
                {
                    for (int i = 0; i < partie.Joueurs.Count(); i++)
                    {
                        joueur.MainDuJoueur.Add(manche.Pioche.Pop());
                    }
                }

                
                // La manche continue tant que la piche ET le troupeau ne sont pas vide.
                while(manche.Pioche.Count != 0 && manche.Troupeau.Count != 0)
                {
                    // le joueur fait son tour
                    partie.JoueurSuivant();
                }
            }


             

            // Exemple ci-dessous (vu ensemble hier)
            //

            bool cartePlacee;
            // Exécute les tests de placement de la carte: place la carte si c'est possible
            manche.PlacerCarte(carteJoueur, out cartePlacee);
            
            // si le placement de la carte échoue
            if (!cartePlacee)
            {
                // j'envoie le message pour indiquer que la carte n'est pas jouable

            }


            #endregion

            #region Chargement des images (Exemple)

            // Déclaration fichier source des Cartes spéciales
            // var srcBackGr = new Uri(@"/Images/GameBG.jpg", UriKind.Relative);
            var srcSensDuJeu = new Uri(Path.Combine(DossierImages, "SensDuJeu.jpg"));
            var srcRetardataire = new Uri(Path.Combine(DossierImages, "retardataire.jpg"));
            var srcSerreFile0 = new Uri(Path.Combine(DossierImages, "serreFile0.jpg"));
            var srcSerreFile16 = new Uri(Path.Combine(DossierImages, "serreFile16.jpg"));
            var srcAcrobate7 = new Uri(Path.Combine(DossierImages, "acrobate7.jpg"));
            var srcAcrobate9 = new Uri(Path.Combine(DossierImages, "acrobate9.jpg"));

            // Déclaration fichier source des Cartes présentes dans la main
            var uriSource1 = new Uri(Path.Combine(DossierImages, "standard4_1.jpg"));
            var uriSource2 = new Uri(Path.Combine(DossierImages, "acrobate9.jpg"));
            var uriSource3 = new Uri(Path.Combine(DossierImages, "default.jpg"));
            var uriSource4 = new Uri(Path.Combine(DossierImages, "default.jpg"));
            var uriSource5 = new Uri(Path.Combine(DossierImages, "default.jpg"));
            var uriSource6 = new Uri(Path.Combine(DossierImages, "up.jpg"));

            // Initialisation
            //Game.Background = new BitmapImage(srcBackGr);

            // Instanciation des images spéciales
            Card0.Fill = new ImageBrush(new BitmapImage(srcSerreFile0));
            Card1.Fill = new ImageBrush(new BitmapImage(srcAcrobate7));
            Card2.Fill = new ImageBrush(new BitmapImage(srcAcrobate9));
            Card3.Fill = new ImageBrush(new BitmapImage(srcSerreFile16));
            SensDuJeu.Fill = new ImageBrush(new BitmapImage(srcSensDuJeu));

            // Instanciation des images
            JoueurCourantCarte1.Fill = new ImageBrush(new BitmapImage(uriSource1));
            JoueurCourantCarte2.Fill = new ImageBrush(new BitmapImage(uriSource2));
            JoueurCourantCarte3.Fill = new ImageBrush(new BitmapImage(uriSource3));
            JoueurCourantCarte4.Fill = new ImageBrush(new BitmapImage(uriSource4));
            JoueurCourantCarte5.Fill = new ImageBrush(new BitmapImage(uriSource5));

            #endregion


        }


        // Exemple datacontext conservé pour comparaison
        /*Image monImage = (Image)sender;
        string chemin = (string)monImage.DataContext;
        MessageBox.Show(chemin);
        */



        /// <summary>
        /// test random, property from carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_click_up(object sender, RoutedEventArgs e)
        {
            // Vide la pioche, la remplie de nouveau avec la liste des cartes du jeu, et mélange les cartes
            manche.MelangerPioche();

            
            var uriSource6 = new Uri(Path.Combine(DossierImages, "up.jpg"));
            // Origine Nathan
            imagerefresh.Source = new BitmapImage(uriSource6);
            // Alternative Bouton refresh
            //imagerefresh.Fill = new ImageBrush( new BitmapImage(uriSource6) );
        }

        /// <summary>
        /// Origine Nathan
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_click_down(object sender, RoutedEventArgs e)
        {
            var uriSource6 = new Uri(Path.Combine(DossierImages, "refresh.jpg"));
            imagerefresh.Source = new BitmapImage(uriSource6);
            // imagerefresh.Fill = new ImageBrush( new BitmapImage(uriSource6) ); // alternative ci-dessus
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_enter(object sender, MouseEventArgs e)
        {
            Image img = ((Image)sender);
            img.Height = 40;
            img.Width = 40;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_leave(object sender, MouseEventArgs e)
        {
            Image img = ((Image)sender);
            img.Height = 33;
            img.Width = 33;
        }

        /// <summary>
        /// Le joueur qui déclanche la méthode rammase le troupeau:
        /// - ajoute les cartes à son étable, et recalcule son score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRamasserClick(object sender, RoutedEventArgs e)
        {
            // exemple:
            partie.JoueurEnCours.Ramasser(manche.Troupeau, manche);
            MessageBox.Show(partie.JoueurEnCours.Pseudo + " a rammassé le troupeau !");
        }

        public void BtnJouerCarteClick(object sender, RoutedEventArgs e) // Rends les cartes cliquable et prévient l'utilisateur  
        {
            // Il faut choisir une carte (ici, je met en dur, mais il faut la récupérer avec le clic souris)
            Carte carte = new Carte();

            // 
            partie.JoueurEnCours.Jouer(manche, carte);
            MessageBox.Show(partie.JoueurEnCours.Pseudo + " a joué la carte " + carte);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnPoserCarte(object sender, MouseButtonEventArgs e) // Place la carte lorsqu'elle est cliqué
        {
            if (evenement)
            {
                if (Card3.Fill == null)
                {
                    var uriSource = new Uri(@"C:\Users\Admin\Desktop\EPSI\C#\mow\Vaches\Vache_9.png");
                    Card3.Fill = new ImageBrush(new BitmapImage(uriSource));
                    Card3.DataContext = @"C:\Users\Admin\Desktop\EPSI\C#\mow\Vaches\Vache_9.png";
                }
                else
                {
                    var uriSource2 = new Uri(@"C:\Users\Admin\Desktop\EPSI\C#\mow\Vaches\Vache_4.png");
                    Card1.Fill = new ImageBrush(new BitmapImage(uriSource2));
                    Card1.DataContext = @"C:\Users\Admin\Desktop\EPSI\C#\mow\Vaches\Vache_9.png";
                }
                evenement = false;
            }
        }


        private void BtnPiocherCarteClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Je Pioche");
        }

        private void Troupeau_DragEnter(object sender, DragEventArgs e)
        {
            // TODO: Ajouter un message dans l'historique dès que l'on commence à dragndrop
            //this.HistoriquePartie
        }

        // Lorsque le curseur entre dans la zone stackpanel, ça taille vertical augmente à 120
        private void hover(object sender, MouseEventArgs e)
        {
            int taille = 120;
            panel.Height = taille;

        }

        // Lorsque le curseur sort de la zone stackpanel, ça taille vertical revient à 25
        private void finhover(object sender, MouseEventArgs e)
        {
            int taille = 25;
            panel.Height = taille;
        }

    } // Fin class MainWindow : Window
} // Fin namespace mowProject
