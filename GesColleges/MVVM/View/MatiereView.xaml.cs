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
using GesColleges.InteractionBase.Ajout;
using GesColleges.InteractionBase.Modifier;


namespace GesColleges.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour CollegeView.xaml
    /// </summary>
    public partial class MatiereView : UserControl
    {
        GesCollegeContext _db = new GesCollegeContext();
        public static DataGrid datagrid;
        public MatiereView()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            matiereDataGrid.ItemsSource = _db.Matieres.ToList();
            datagrid = matiereDataGrid;
        }

        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutMatiere a = new AjoutMatiere(this);
            a.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (datagrid.SelectedItem as Salle).IdSalle;
                ModifMatiere a = new ModifMatiere(Id);
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
            datagrid.ItemsSource = _db.Matieres.ToList();
        }

        private void SupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                    int Id = (datagrid.SelectedItem as Matiere).IdMatiere;
                    Matiere RmvMtr = (from c in _db.Matieres where c.IdMatiere == Id select c).Single();
                    _db.Matieres.Remove(RmvMtr);
                    _db.SaveChanges();
                    datagrid.ItemsSource = _db.Matieres.ToList();
                
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       
        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            blow.Text = search.Text;
            if (search.Text != "")
            {
                switch (matiereComboBox.Text)
                {
                    case "Libellé":
                        datagrid.ItemsSource = (from c in _db.Matieres where c.libelle.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Salle":
                        datagrid.ItemsSource = (from c in _db.Matieres where c.Salle.Nom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                }
            }
            else datagrid.ItemsSource = _db.Matieres.ToList();
        }

        private void PrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (matiereDataGrid.SelectedItem as Matiere).IdMatiere;
                ListeEnseignant t = new ListeEnseignant(Id);
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
