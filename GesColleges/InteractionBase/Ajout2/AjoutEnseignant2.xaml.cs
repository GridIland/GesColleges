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
    public partial class AjoutEnseignant2 : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        public AjoutEnseignant2()
        {
            InitializeComponent();
            Liaison();
        }

        private void Liaison()
        {
            // Combobox Departements liaison
            departementComboBox.ItemsSource = _db.Departements.Select(c => new { IdDepartement = c.IdDepartement,Departement =c.nomDe+" du "+c.College.nomCo }).ToList();
            departementComboBox.DisplayMemberPath = "Departement";
            departementComboBox.SelectedValuePath = "IdDepartement";

            // Combobox Matières liaison
            matiereComboBox.ItemsSource = _db.Matieres.Select(c => new { IdMatiere = c.IdMatiere, libelle = c.libelle }).ToList();
            matiereComboBox.DisplayMemberPath = "libelle";
            matiereComboBox.SelectedValuePath = "IdMatiere";
        }
        public bool verifChef()
        {
            int IdDep = int.Parse(departementComboBox.SelectedValue.ToString());
            if ((from c in _db.Enseignants where c.IdDepartement == IdDep && c.ChefDepartement == true select c).SingleOrDefault() == null)
            {
                return true;
            }
            else return false;
        }
        private bool verification()
        {
            if (nomTextBox.Text == "" || prenomTextBox.Text == "" || emailTextBox.Text == "" || telephoneTextBox.Text == "" || departementComboBox.Text == "" || matiereComboBox.Text == "" || datePriseFonctionDatePicker.SelectedDate == null || datePriseFonctionDatePicker.SelectedDate.Value > DateTime.Now )
            {
                return false;
            }
            else
            { return true; }
        }
        private bool checkreturn()
        {
            if (chefDepartementCheckBox.IsChecked == false)
                return false;
            else return true;
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource enseignantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("enseignantViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // enseignantViewSource.Source = [source de données générique]
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (chefDepartementCheckBox.IsChecked == true)
            {
                if (verification()&&verifChef())
                {

                    Enseignant c = new Enseignant()
                    {
                        Nom = nomTextBox.Text,
                        Prenom = prenomTextBox.Text,
                        Email = emailTextBox.Text,
                        Telephone = telephoneTextBox.Text,
                        DatePriseFonction = datePriseFonctionDatePicker.SelectedDate.Value,
                        IdDepartement = int.Parse(departementComboBox.SelectedValue.ToString()),
                        IdMatiere = int.Parse(matiereComboBox.SelectedValue.ToString()),
                        ChefDepartement = checkreturn()

                    };
                    _db.Enseignants.Add(c);
                    _db.SaveChanges();
                    MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Veuillez completer tous les champs ou il existe déjà un chef", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (verification())
                {

                    Enseignant c = new Enseignant()
                    {
                        Nom = nomTextBox.Text,
                        Prenom = prenomTextBox.Text,
                        Email = emailTextBox.Text,
                        Telephone = telephoneTextBox.Text,
                        DatePriseFonction = datePriseFonctionDatePicker.SelectedDate.Value,
                        IdDepartement = int.Parse(departementComboBox.SelectedValue.ToString()),
                        IdMatiere = int.Parse(matiereComboBox.SelectedValue.ToString()),
                        ChefDepartement = checkreturn()

                    };
                    _db.Enseignants.Add(c);
                    _db.SaveChanges();
                    MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void telephoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
