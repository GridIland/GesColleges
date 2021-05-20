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
    public partial class AjoutCollege : Window
    {
        GesCollegeContext db = new GesCollegeContext();
        public AjoutCollege()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool verification()
        {
            if(nomTextBox.Text=="" || adresseSiteTextBox.Text=="")
            {
                return false;
            }
            else
            { return true; }
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(verification())
            {
                College c = new College()
                {
                    nomCo = nomTextBox.Text,
                    AdresseSite = adresseSiteTextBox.Text
                };
                db.Colleges.Add(c);
                db.SaveChanges();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                CollegeView.datagrid.ItemsSource = db.Colleges.ToList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource collegeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("collegeViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // collegeViewSource.Source = [source de données générique]
        }
    }
}
