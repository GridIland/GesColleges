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

namespace GesColleges.InteractionBase.Ajout2
{
    /// <summary>
    /// Logique d'interaction pour AjoutCollege.xaml
    /// </summary>
    public partial class AjoutEtudiant2 : Window
    {
        GesCollegeContext _db;
        public AjoutEtudiant2()
        {
            _db = new GesCollegeContext();
            InitializeComponent();
            Liaison();
        }

        private void Liaison()
        {
            collegeComboBox.ItemsSource = _db.Colleges.Select(c => new { IdCollege = c.IdCollege , Nom = c.nomCo}).ToList();
            collegeComboBox.DisplayMemberPath = "Nom";
            collegeComboBox.SelectedValuePath = "IdCollege";
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool verification()
        {
            if (nomTextBox.Text == "" || prenomTextBox.Text == "" || emailTextBox.Text == "" || anneeEntreeTextBox.Text == "" || telephoneTextBox.Text == "" || collegeComboBox.Text=="")
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
                Etudiant c = new Etudiant()
                {
                    Nom = nomTextBox.Text,
                    Prenom = prenomTextBox.Text,
                    Email = emailTextBox.Text,
                    Telephone = telephoneTextBox.Text,
                    AnneeEntree = int.Parse(anneeEntreeTextBox.Text),
                    IdCollege = int.Parse(collegeComboBox.SelectedValue.ToString())
                };
                _db.Etudiants.Add(c);
                _db.SaveChanges();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource etudiantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("etudiantViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // etudiantViewSource.Source = [source de données générique]
        }

        private void ChiffreVerif(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void anneeEntreeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (anneeEntreeTextBox.Text.Length > 0)
            {
                if (int.Parse(anneeEntreeTextBox.Text.ToString()) > DateTime.Now.Year)
                {
                    MessageBox.Show("Date invalide", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
                    anneeEntreeTextBox.Text = "0";
                }
            }
        }
    }
}
