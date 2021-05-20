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
    public partial class SalleView : UserControl
    {
        GesCollegeContext _db = new GesCollegeContext();
        public static DataGrid datagrid;
        public SalleView()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            salleDataGrid.ItemsSource = _db.Salles.ToList();
            datagrid = salleDataGrid;
        }

        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutSalle a = new AjoutSalle(this);
            a.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (salleDataGrid.SelectedItem as Salle).IdSalle;
                ModifSalle a = new ModifSalle(Id);
                a.ShowDialog();
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
        public void Actualiser()
        {
            datagrid.ItemsSource = _db.Salles.ToList();
        }

        private void SupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                    int Id = (datagrid.SelectedItem as Salle).IdSalle;
                    Salle RmvSle = (from c in _db.Salles where c.IdSalle == Id select c).Single();
                    _db.Salles.Remove(RmvSle);
                    _db.SaveChanges();
                    datagrid.ItemsSource = _db.Salles.ToList();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ResBtn_Click(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = _db.Salles.ToList();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            blow.Text = search.Text;
            if (search.Text != "")
            {
                switch (salleComboBox.Text)
                {
                    case "Nom":
                        datagrid.ItemsSource = (from c in _db.Salles where c.Nom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Capacité":
                        datagrid.ItemsSource = (from c in _db.Salles where c.Capacite.ToString().IndexOf(search.Text) != -1 select c).ToList();
                        break;
                }
            }
            else datagrid.ItemsSource = _db.Salles.ToList();
        }
    }
}
