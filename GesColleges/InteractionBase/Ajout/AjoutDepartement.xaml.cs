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
    public partial class AjoutDepartement : Window
    {
        GesCollegeContext _db;
        UserControl UserDepartement;
        public AjoutDepartement(UserControl user)
        {
            UserDepartement = user;
            _db = new GesCollegeContext();
            InitializeComponent();
            Liaison();
        }

        private void Liaison()
        {
            collegeComboBox.ItemsSource = _db.Colleges.Select(c => new { IdCollege = c.IdCollege, nom = c.nomCo }).ToList();
            collegeComboBox.DisplayMemberPath = "nom";
            collegeComboBox.SelectedValuePath = "IdCollege";
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool verification()
        {
            if (nomTextBox.Text == "" || collegeComboBox.SelectedItem.ToString() == "")
            {
                return false;
            }
            else
            { return true; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource departementViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("departementViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // departementViewSource.Source = [source de données générique]
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (verification())
            {
                Departement d = new Departement()
                {
                    nomDe = nomTextBox.Text,
                    IdCollege = int.Parse(collegeComboBox.SelectedValue.ToString())
                };
                _db.Departements.Add(d);
                _db.SaveChanges();
                (UserDepartement as DepartementView).Actualiser();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
