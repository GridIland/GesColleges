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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GesColleges.Etats;
using GesColleges.Etats.Etat_avancé;
using GesColleges.InteractionBase.Ajout2;

namespace GesColleges.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour CollegeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        GesCollegeContext _db;
        public HomeView()
        {
            InitializeComponent();
            _db = new GesCollegeContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListeDepartement t = new ListeDepartement();
            t.ShowDialog();
        }

        private void NoteMoy1_Click(object sender, RoutedEventArgs e)
        {
            SelectionMat n = new SelectionMat();
            n.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectionMat1 n = new SelectionMat1();
            n.ShowDialog();
        }

        private void ajoutMat_Click(object sender, RoutedEventArgs e)
        {
            AjoutMatiere2 n = new AjoutMatiere2();
            n.ShowDialog();
        }

        private void ajoutCol_Click(object sender, RoutedEventArgs e)
        {
            AjoutCollege2 n = new AjoutCollege2();
            n.ShowDialog();

        }

        private void AjoutDep_Click(object sender, RoutedEventArgs e)
        {
            AjoutDepartement2 n = new AjoutDepartement2();
            n.ShowDialog();
        }

        private void AjoutEtu_Click(object sender, RoutedEventArgs e)
        {
            AjoutEtudiant2 n = new AjoutEtudiant2();
            n.ShowDialog();
        }

        private void AjoutNote_Click(object sender, RoutedEventArgs e)
        {
            AjoutNote2 n = new AjoutNote2();
            n.ShowDialog();
        }

        private void AjoutSal_Click(object sender, RoutedEventArgs e)
        {
            AjoutSalle2 n = new AjoutSalle2();
            n.ShowDialog();
        }

        private void AjoutEns_Click(object sender, RoutedEventArgs e)
        {
            AjoutEnseignant2 n = new AjoutEnseignant2();
            n.ShowDialog();
        }
    }
}
