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
    public partial class EnseignantView : UserControl
    {
        GesCollegeContext _db = new GesCollegeContext();
        public static DataGrid datagrid;
        public EnseignantView()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            enseignantDataGrid.ItemsSource = _db.Enseignants.ToList();
            datagrid = enseignantDataGrid;
        }

        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutEnseignant a = new AjoutEnseignant(this);
            a.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (enseignantDataGrid.SelectedItem != null)
            {
                int Id = (enseignantDataGrid.SelectedItem as Enseignant).IdEnseignant;
                ModifEnseignant a = new ModifEnseignant(Id);
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

        private void SupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                
                 int Id = (datagrid.SelectedItem as Enseignant).IdEnseignant;
                 Enseignant RmvEnsgnt = (from c in _db.Enseignants where c.IdEnseignant == Id select c).Single();
                 _db.Enseignants.Remove(RmvEnsgnt);
                 _db.SaveChanges();
                 datagrid.ItemsSource = _db.Enseignants.ToList();
                
                
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public void Actualiser()
        {
            datagrid.ItemsSource = _db.Enseignants.ToList();
        }
        private void ResBtn_Click(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = _db.Enseignants.ToList();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            blow.Text = search.Text;
            if (search.Text!= "")
            {
                switch (enseignantComboBox.Text)
                {
                    case "Nom":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.Nom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Prénom":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.Prenom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Téléphone":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.Telephone.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Email":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.Email.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Début":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.DatePriseFonction.ToString().IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Matière":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.Matiere.libelle.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Département":
                        datagrid.ItemsSource = (from c in _db.Enseignants where c.Departement.nomDe.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                }
            }
            else datagrid.ItemsSource = _db.Enseignants.ToList();
        }

        private void PrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (enseignantDataGrid.SelectedItem as Enseignant).IdEnseignant;
                FicheEnseignant t = new FicheEnseignant(Id);
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
