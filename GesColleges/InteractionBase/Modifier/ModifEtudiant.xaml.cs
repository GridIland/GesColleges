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

namespace GesColleges.InteractionBase.Modifier
{
    /// <summary>
    /// Logique d'interaction pour AjoutCollege.xaml
    /// </summary>
    public partial class ModifEtudiant : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        int ID;
        public ModifEtudiant(int id)
        {
            InitializeComponent();
            ID = id;
            Liaison();
            Remplissage();
        }
        private void Liaison()
        {
            collegeComboBox.ItemsSource = _db.Colleges.Select(c => new { IdCollege = c.IdCollege, Nom = c.nomCo }).ToList();
            collegeComboBox.DisplayMemberPath = "Nom";
            collegeComboBox.SelectedValuePath = "IdCollege";
        }
        private void Remplissage()
        {
            Etudiant RmpEtdt = (from c in _db.Etudiants where c.IdEtudiant == ID select c).Single();
            nomTextBox.Text = RmpEtdt.Nom;
            prenomTextBox.Text = RmpEtdt.Prenom;
            emailTextBox.Text = RmpEtdt.Email;
            telephoneTextBox.Text = RmpEtdt.Telephone;
            anneeEntreeTextBox.Text = RmpEtdt.AnneeEntree.ToString();
            collegeComboBox.SelectedValue = RmpEtdt.IdCollege;
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource etudiantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("etudiantViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // etudiantViewSource.Source = [source de données générique]
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Etudiant UpdtEdt = (from c in _db.Etudiants where c.IdEtudiant == ID select c).Single();
                UpdtEdt.Nom = nomTextBox.Text;
                UpdtEdt.Prenom = prenomTextBox.Text;
                UpdtEdt.Email = emailTextBox.Text;
                UpdtEdt.Telephone = telephoneTextBox.Text;
                UpdtEdt.AnneeEntree = int.Parse(anneeEntreeTextBox.Text);
                UpdtEdt.IdCollege = int.Parse(collegeComboBox.SelectedValue.ToString());
                _db.SaveChanges();
                EtudiantView.datagrid.ItemsSource = _db.Etudiants.ToList();
                this.Close();
            }
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

        private void Chiffreverif(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
