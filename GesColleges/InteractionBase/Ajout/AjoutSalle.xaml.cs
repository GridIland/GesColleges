using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GesColleges.MVVM.View;

namespace GesColleges.InteractionBase.Ajout
{
    /// <summary>
    /// Logique d'interaction pour AjoutCollege.xaml
    /// </summary>
    public partial class AjoutSalle : Window
    {
        GesCollegeContext db = new GesCollegeContext();
        UserControl userSalle;
        public AjoutSalle(UserControl user)
        {
            userSalle = user;
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool verification()
        {
            if (nomTextBox.Text == "" || capaciteTextBox.Text == "")
            {
                return false;
            }
            else
            { return true; }
        }

       

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (verification())
            {
                Salle c = new Salle()
                {
                    Nom = nomTextBox.Text,
                    Capacite = int.Parse(capaciteTextBox.Text)
                };
                db.Salles.Add(c);
                db.SaveChanges();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                (userSalle as SalleView).Actualiser();
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource salleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("salleViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // salleViewSource.Source = [source de données générique]
        }

        private void capaciteTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
