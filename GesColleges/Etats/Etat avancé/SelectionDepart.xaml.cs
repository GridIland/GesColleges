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
    /// Logique d'interaction pour SelectionClg.xaml
    /// </summary>
    public partial class SelectionDepart : Window
    {
        int IdCol;
        int IdMat;
        GesCollegeContext db;
        public SelectionDepart(int idCol, int idMat)
        {
            IdMat = idMat;
            IdCol = idCol;
            db = new GesCollegeContext();
            InitializeComponent();
            Liaison();
        }
        private void Liaison()
        {
            departementComboBox.ItemsSource = db.Departements.Select(c => new 
            { IdDepartement = c.IdDepartement, nomDe = c.nomDe , IdCollege = c.IdCollege}).Where(c => c.IdCollege == IdCol).ToList();
            departementComboBox.DisplayMemberPath = "nomDe";
            departementComboBox.SelectedValuePath = "IdDepartement";
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            int IdDep = int.Parse(departementComboBox.SelectedValue.ToString());
            NotesMoyennesDep n = new NotesMoyennesDep(IdMat,IdDep);
            n.ShowDialog();
            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource departementViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("departementViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // departementViewSource.Source = [source de données générique]
        }
    }
}
