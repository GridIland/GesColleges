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
using GesColleges.InteractionBase.Ajout;
using GesColleges.InteractionBase.Modifier;

namespace GesColleges.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour CollegeView.xaml
    /// </summary>
    public partial class DepartementView : UserControl
    {
        GesCollegeContext _db;
        public static DataGrid datagrid;
        public DepartementView()
        {
            _db = new GesCollegeContext();
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            departementDataGrid.ItemsSource = _db.Departements.ToList();
            datagrid = departementDataGrid;
        }

        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutDepartement a = new AjoutDepartement(this);
            a.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if(departementDataGrid.SelectedItem != null)
            {
                int Id = (departementDataGrid.SelectedItem as Departement).IdDepartement;
                ModifDepartement w = new ModifDepartement(Id);
                w.ShowDialog();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Ne chargez pas vos données au moment du design.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Chargez vos données ici et assignez le résultat à CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void SupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (departementDataGrid.SelectedItem != null)
            {
                    int Id = (departementDataGrid.SelectedItem as Departement).IdDepartement;
                    Departement RmvDeprtmt = (from c in _db.Departements where c.IdDepartement == Id select c).Single();
                    _db.Departements.Remove(RmvDeprtmt);
                    _db.SaveChanges();
                    datagrid.ItemsSource = _db.Departements.ToList();
                
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void Actualiser()
        {
            datagrid.ItemsSource = _db.Departements.ToList();
        }
        private void ResBtn_Click(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = _db.Departements.ToList();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            blow.Text = search.Text;
            if (search.Text != "")
            {
                switch (departementComboBox.Text)
                {
                    case "Nom du département":
                        datagrid.ItemsSource = (from c in _db.Departements where c.nomDe.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Nom du collège":
                        datagrid.ItemsSource = (from c in _db.Departements where c.College.nomCo.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                }
            }
            else datagrid.ItemsSource = _db.Departements.ToList();
        }
    }
}
