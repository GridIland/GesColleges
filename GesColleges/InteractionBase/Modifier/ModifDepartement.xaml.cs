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
    public partial class ModifDepartement : Window
    {
        GesCollegeContext _db;
        int Id;
        public ModifDepartement(int id)
        {
            Id = id;
            _db = new GesCollegeContext();
            InitializeComponent();
            Liaison();
            Remplissage();

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource departementViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("departementViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // departementViewSource.Source = [source de données générique]
        }
        private void Remplissage()
        {
            Departement RmpDprt = (from c in _db.Departements where c.IdDepartement == Id select c).Single();
            nomTextBox.Text = RmpDprt.nomDe;
            collegeComboBox.SelectedValue = RmpDprt.IdCollege;

        }
        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes) 
            {
                Departement UpdtDeprtmnt = (from c in _db.Departements where c.IdDepartement == Id select c).Single();
                UpdtDeprtmnt.nomDe = nomTextBox.Text;
                UpdtDeprtmnt.IdCollege = int.Parse(collegeComboBox.SelectedValue.ToString());
                _db.SaveChanges();
                DepartementView.datagrid.ItemsSource = _db.Departements.ToList();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();   
            }
        }
    }
}
