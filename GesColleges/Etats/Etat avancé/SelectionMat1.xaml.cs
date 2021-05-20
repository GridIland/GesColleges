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

namespace GesColleges.Etats.Etat_avancé
{
    /// <summary>
    /// Logique d'interaction pour SelectionMat.xaml
    /// </summary>
    public partial class SelectionMat1 : Window
    {
        GesCollegeContext db;
        public SelectionMat1()
        {
            db = new GesCollegeContext();
            InitializeComponent();
            Liaison();
        }

        private void Liaison()
        {
            matiereComboBox.ItemsSource = db.Matieres.Select(c => new { IdMatiere = c.IdMatiere, libelle = c.libelle }).ToList();
            matiereComboBox.DisplayMemberPath = "libelle";
            matiereComboBox.SelectedValuePath = "IdMatiere";
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            int IdMat = int.Parse(matiereComboBox.SelectedValue.ToString());
            SelectionClg1 c = new SelectionClg1(IdMat);
            c.ShowDialog();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource matiereViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("matiereViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // matiereViewSource.Source = [source de données générique]
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
