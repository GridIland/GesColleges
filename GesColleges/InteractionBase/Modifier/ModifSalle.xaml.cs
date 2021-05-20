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
    public partial class ModifSalle : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        int Id;
        public ModifSalle(int ID)
        {
            InitializeComponent();
            Id = ID;
            Remplissage();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Remplissage()
        {
            Salle RmpSle = (from c in _db.Salles where c.IdSalle == Id select c).Single();
            nomTextBox.Text = RmpSle.Nom;
            capaciteTextBox.Text = RmpSle.Capacite.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource salleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("salleViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // salleViewSource.Source = [source de données générique]
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Salle updtSalle = (from c in _db.Salles where c.IdSalle == Id select c).Single();
                updtSalle.Nom = nomTextBox.Text;
                updtSalle.Capacite = int.Parse(capaciteTextBox.Text);
                _db.SaveChanges();
                SalleView.datagrid.ItemsSource = _db.Salles.ToList();
                this.Close();
            }
            
        }

        private void capaciteTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
