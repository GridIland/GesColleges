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
    public partial class SelectionClg : Window
    {
        int Idmat;
        GesCollegeContext db;
        public SelectionClg(int idMat)
        {
            Idmat = idMat;
            db = new GesCollegeContext();
            InitializeComponent();
            Liaison();
        }
        private void Liaison()
        {
            collegeComboBox.ItemsSource = db.Colleges.Select(c => new { IdCollege = c.IdCollege, nomCo = c.nomCo }).ToList();
            collegeComboBox.DisplayMemberPath = "nomCo";
            collegeComboBox.SelectedValuePath = "IdCollege";
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            int IdCol = int.Parse(collegeComboBox.SelectedValue.ToString());
            NotesMoyennesColg n = new NotesMoyennesColg(Idmat,IdCol);
            n.ShowDialog();
            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
