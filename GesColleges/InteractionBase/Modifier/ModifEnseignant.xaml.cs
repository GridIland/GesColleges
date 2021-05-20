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

namespace GesColleges.InteractionBase.Modifier
{
    /// <summary>
    /// Logique d'interaction pour AjoutCollege.xaml
    /// </summary>
    public partial class ModifEnseignant : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        int Id;
        public ModifEnseignant(int id)
        {
            Id = id;
            InitializeComponent();
            Liaison();
            Remplissage();
        }
        private void Remplissage()
        {
            Enseignant RmpEnsgnt = (from c in _db.Enseignants where c.IdEnseignant == Id select c).Single();
            nomTextBox.Text = RmpEnsgnt.Nom;
            prenomTextBox.Text = RmpEnsgnt.Prenom;
            emailTextBox.Text = RmpEnsgnt.Email;
            telephoneTextBox.Text = RmpEnsgnt.Telephone;
            datePriseFonctionDatePicker.SelectedDate = RmpEnsgnt.DatePriseFonction;
            matiereComboBox.SelectedValue = RmpEnsgnt.IdMatiere;
            departementComboBox.SelectedValue = RmpEnsgnt.IdDepartement;
            chefDepartementCheckBox.IsChecked = RmpEnsgnt.ChefDepartement;
        }
        private void Liaison()
        {
            // Combobox Departements liaison
            departementComboBox.ItemsSource = _db.Departements.Select(c => new { IdDepartement = c.IdDepartement, Departement = c.nomDe + " du " + c.College.nomCo }).ToList();
            departementComboBox.DisplayMemberPath = "Departement";
            departementComboBox.SelectedValuePath = "IdDepartement";

            // Combobox Matières liaison
            matiereComboBox.ItemsSource = _db.Matieres.Select(c => new { IdMatiere = c.IdMatiere, libelle = c.libelle }).ToList();
            matiereComboBox.DisplayMemberPath = "libelle";
            matiereComboBox.SelectedValuePath = "IdMatiere";
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public bool verifChef()
        {
            int IdDep = int.Parse(departementComboBox.SelectedValue.ToString());
            if ((from c in _db.Enseignants where c.IdDepartement == IdDep  && c.ChefDepartement==true select c).SingleOrDefault()==null)
            {
                return true;
            }
            return false;
        }
        private bool verification()
        {
            if (nomTextBox.Text == "" || prenomTextBox.Text == "" || emailTextBox.Text == "" || telephoneTextBox.Text == "" || departementComboBox.Text == "" || matiereComboBox.Text == "" || datePriseFonctionDatePicker.SelectedDate.Value > DateTime.Now)
            {
                return false;
            }
            else
            { return true; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource enseignantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("enseignantViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // enseignantViewSource.Source = [source de données générique]
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(chefDepartementCheckBox.IsChecked == true)
            {
                if (verification() && verifChef())
                {
                    if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Enseignant UpdtEnsgnt = (from c in _db.Enseignants where c.IdEnseignant == Id select c).Single();
                        UpdtEnsgnt.Nom = nomTextBox.Text;
                        UpdtEnsgnt.Prenom = prenomTextBox.Text;
                        UpdtEnsgnt.Email = emailTextBox.Text;
                        UpdtEnsgnt.Telephone = telephoneTextBox.Text;
                        UpdtEnsgnt.IdDepartement = int.Parse(departementComboBox.SelectedValue.ToString());
                        UpdtEnsgnt.IdMatiere = int.Parse(matiereComboBox.SelectedValue.ToString());
                        UpdtEnsgnt.ChefDepartement = (chefDepartementCheckBox.IsChecked == false) ? false : true;
                        UpdtEnsgnt.DatePriseFonction = datePriseFonctionDatePicker.SelectedDate.Value;
                        _db.SaveChanges();
                        EnseignantView.datagrid.ItemsSource = _db.Enseignants.ToList();
                        MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("il y a un champ vide ou il existe déjà un chef", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (verification())
                {

                    if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Enseignant UpdtEnsgnt = (from c in _db.Enseignants where c.IdEnseignant == Id select c).Single();
                        UpdtEnsgnt.Nom = nomTextBox.Text;
                        UpdtEnsgnt.Prenom = prenomTextBox.Text;
                        UpdtEnsgnt.Email = emailTextBox.Text;
                        UpdtEnsgnt.Telephone = telephoneTextBox.Text;
                        UpdtEnsgnt.IdDepartement = int.Parse(departementComboBox.SelectedValue.ToString());
                        UpdtEnsgnt.IdMatiere = int.Parse(matiereComboBox.SelectedValue.ToString());
                        UpdtEnsgnt.ChefDepartement = (chefDepartementCheckBox.IsChecked == false) ? false : true;
                        UpdtEnsgnt.DatePriseFonction = datePriseFonctionDatePicker.SelectedDate.Value;
                        _db.SaveChanges();
                        EnseignantView.datagrid.ItemsSource = _db.Enseignants.ToList();
                        MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("il y a un champ vide", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
