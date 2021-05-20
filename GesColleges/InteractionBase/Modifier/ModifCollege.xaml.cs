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
    public partial class ModifCollege : Window
    {
        GesCollegeContext _db ;
        int Id;
        public ModifCollege(int IdCollege)
        {
            _db = new GesCollegeContext();
            InitializeComponent();
            Id = IdCollege;
            Remplissage();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Remplissage()
        {
            College RmpClg = (from c in _db.Colleges where c.IdCollege == Id select c).Single();
            nomTextBox.Text = RmpClg.nomCo;
            adresseSiteTextBox.Text = RmpClg.AdresseSite;

        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { 
                College UpdtCollege = (from c in _db.Colleges where c.IdCollege == Id select c).Single();
                UpdtCollege.nomCo = nomTextBox.Text;
                UpdtCollege.AdresseSite = adresseSiteTextBox.Text;
                _db.SaveChanges();
                CollegeView.datagrid.ItemsSource = _db.Colleges.ToList();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
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
