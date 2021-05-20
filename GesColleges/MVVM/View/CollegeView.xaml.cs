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
    public partial class CollegeView : UserControl
    {
        GesCollegeContext _db = new GesCollegeContext();
        public static DataGrid datagrid;
        public CollegeView()
        {
            InitializeComponent();
            load();
            LiaisonCombo();
        }

        private void LiaisonCombo()
        {
            
           
        }

        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutCollege w = new AjoutCollege();
            w.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (collegeDataGrid.SelectedItem as College).IdCollege;
                ModifCollege w = new ModifCollege(Id);
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
        private void load ()
        {
            collegeDataGrid.ItemsSource = _db.Colleges.ToList();
            datagrid = collegeDataGrid;
        }

        private void SupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                    int Id = (datagrid.SelectedItem as College).IdCollege;
                    College RmvCollege = (from c in _db.Colleges where c.IdCollege == Id select c).Single();
                    _db.Colleges.Remove(RmvCollege);
                    _db.SaveChanges();
                    datagrid.ItemsSource = _db.Colleges.ToList();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ResBtn_Click(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = _db.Colleges.ToList();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            blow.Text = search.Text;
            if (search.Text != "")
            {
                switch (CollegeComboBox.SelectedIndex)
                {
                    case 0:
                        datagrid.ItemsSource = _db.Colleges.Where(c => c.nomCo.IndexOf(search.Text) != -1).ToList();
                        break;
                    case 1:
                        datagrid.ItemsSource = (from c in _db.Colleges where c.AdresseSite.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                }
            }
            else datagrid.ItemsSource = _db.Colleges.ToList();
        }

        private void PrntBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
