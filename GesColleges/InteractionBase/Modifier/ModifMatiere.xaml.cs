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
    public partial class ModifMatiere : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        int Id;
        public ModifMatiere(int id)
        {
            InitializeComponent();
            Id = id;
            Remplissage();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Remplissage()
        {
            Matiere RmpMtr = (from c in _db.Matieres where c.IdMatiere == Id select c).Single();
            libelleTextBox.Text = RmpMtr.libelle;
            salleComboBox.SelectedValue = RmpMtr.IdSalle;
            
        }
        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Matiere UpdtMt = (from c in _db.Matieres where c.IdMatiere == Id select c).Single();
                UpdtMt.libelle = libelleTextBox.Text;
                UpdtMt.IdSalle = int.Parse(salleComboBox.SelectedValue.ToString());
                _db.SaveChanges();
                MatiereView.datagrid.ItemsSource = _db.Matieres.ToList();
                this.Close();
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
