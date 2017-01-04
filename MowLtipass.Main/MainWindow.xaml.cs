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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO.Packaging;
using MowLtipass.Core;
using System.Security.Cryptography;

namespace MowGame.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // La partie commence
        Partie partie = new Partie();

        // La manche commence
        Manche manche = new Manche();

        // Carte carteJoueur
        Carte carteJoueur = new Carte();

        public bool evenement = false;// Booléen qui va instencier si on peut cliqué sur les carte ou non

        public MainWindow()
        {
            InitializeComponent();
            int taille;
            int CardHeight = 110;
            int CardWidth = 60;

            bool result;
            // Exécute les tests de placement de la carte: place la carte si c'est possible
            manche.PlacerCarte(carteJoueur, out result);
            
            // si le placement de la carte échoue
            if (!result)
            {
                // j'envoie le message pour indiquer que la carte n'est pas jouable

            }
            


            // Test du binding sur la fenêtre principale (inutile, juste pour tester)
            this.DataContext = this;

            // Déclaration path Cartes spéciales
            // var srcBackGr = new Uri(@"/Images/GameBG.jpg", UriKind.Relative);

            var srcSensDuJeu = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "SensDuJeu.jpg"));
            var srcRetardataire = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "retardataire.jpg"));
            var srcSerreFile0 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "serreFile0.jpg"));
            var srcSerreFile16 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "serreFile16.jpg"));
            var srcAcrobate7 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "acrobate7.jpg"));
            var srcAcrobate9 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "acrobate9.jpg"));


            // Initialisation
            //Game.Background = new BitmapImage(srcBackGr);

            Card0.Fill = new ImageBrush(new BitmapImage(srcSerreFile0));
            Card1.Fill = new ImageBrush(new BitmapImage(srcAcrobate7));
            Card2.Fill = new ImageBrush(new BitmapImage(srcAcrobate9));
            Card3.Fill = new ImageBrush(new BitmapImage(srcSerreFile16));

            SensDuJeu.Fill = new ImageBrush(new BitmapImage(srcSensDuJeu));


            // Définition des images de la main
            var uriSource1 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "standard4_1.jpg"));
            var uriSource2 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "acrobate9.jpg"));
            var uriSource3 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "default.jpg"));
            var uriSource4 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "default.jpg"));
            var uriSource5 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "default.jpg"));
            var uriSource6 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "up.jpg"));

            JoueurCourantCarte1.Fill = new ImageBrush(new BitmapImage(uriSource1));
            JoueurCourantCarte2.Fill = new ImageBrush(new BitmapImage(uriSource2));
            JoueurCourantCarte3.Fill = new ImageBrush(new BitmapImage(uriSource3));
            JoueurCourantCarte4.Fill = new ImageBrush(new BitmapImage(uriSource4));
            JoueurCourantCarte5.Fill = new ImageBrush(new BitmapImage(uriSource5));
            // Origine Nathan
        }






        //test random, property from carte //
        private void image_click_up(object sender, RoutedEventArgs e)
        {
            // Vide la pioche, la remplie de nouveau avec la liste des cartes du jeu, et mélange les cartes
            manche.MelangerPioche();

            
            var uriSource6 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "up.jpg"));
            // Origine Nathan
            imagerefresh.Source = new BitmapImage(uriSource6);
            // Alternative Bouton refresh
            //imagerefresh.Fill = new ImageBrush( new BitmapImage(uriSource6) );
        }

        private void image_click_down(object sender, RoutedEventArgs e)
        {
            var uriSource6 = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "refresh.jpg"));
            // Origine Nathan
            imagerefresh.Source = new BitmapImage(uriSource6);
            // Alternative Bouton refresh
            //imagerefresh.Fill = new ImageBrush( new BitmapImage(uriSource6) );
        }

        private void image_enter(object sender, MouseEventArgs e)
        {
            Image img = ((Image)sender);
            img.Height = 40;
            img.Width = 40;
        }

        private void image_leave(object sender, MouseEventArgs e)
        {
            Image img = ((Image)sender);
            img.Height = 33;
            img.Width = 33;
        }

        private void BtnRamasserClick(object sender, RoutedEventArgs e) // Rammase le troupeau et le remet à 0
        {



            // Supprimer le contenu du troupeau et ajouter à l'étable du joueur qui vient de ramasser
            // Doit faire appel à 2 méthodes:
            // se référer aux classes (et voir le diagramme UML)
            // exemple: imaginer que cette ligne a été écrite à sa place (création de la partie)
            Humain romain = new Humain();
            // j'appelle la méthode ramasser
            romain.Ramasser(manche);

            // Problème: cette fonction doit utiliser le bon joueur

            MessageBox.Show("Vous avez rammassé le troupeau !");
        }

        public void BtnJouerCarteClick(object sender, RoutedEventArgs e) // Rends les cartes cliquable et prévient l'utilisateur  
        {
            evenement = true;
            MessageBox.Show("Vous pouvez jouer une carte !");

        }
        /*Image monImage = (Image)sender;
        string chemin = (string)monImage.DataContext;
        MessageBox.Show(chemin);
        */


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
