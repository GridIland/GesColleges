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
using System.Windows.Shapes;
using GesColleges.MVVM.View;

namespace GesColleges.InteractionBase.Ajout
{
    /// <summary>
    /// Logique d'interaction pour AjoutCollege.xaml
    /// </summary>
    public partial class AjoutMatiere : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        UserControl UserMatiere;
        public AjoutMatiere(UserControl user)
        {
            UserMatiere = user;
            InitializeComponent();
            Liaison();
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Liaison()
        {
            salleComboBox.ItemsSource = _db.Salles.Select(c => new { IdSalle = c.IdSalle, nom = c.Nom}).ToList();
            salleComboBox.DisplayMemberPath = "nom";
            salleComboBox.SelectedValuePath = "IdSalle";
        }

        private bool verification()
        {
            if (libelleTextBox.Text == "" || salleComboBox.Text == "")
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
                Matiere d = new Matiere()
                {
                    libelle = libelleTextBox.Text,
                    IdSalle = int.Parse(salleComboBox.SelectedValue.ToString())
                };
                _db.Matieres.Add(d);
                _db.SaveChanges();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                (UserMatiere as MatiereView).Actualiser();
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource matiereViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("matiereViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // matiereViewSource.Source = [source de données générique]
        }
    }
}
