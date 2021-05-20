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
    public partial class EtudiantView : UserControl
    {
        GesCollegeContext _db = new GesCollegeContext();
        public static DataGrid datagrid;
        public EtudiantView()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            etudiantDataGrid.ItemsSource = _db.Etudiants.ToList();
            datagrid = etudiantDataGrid;
        }

        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutEtudiant a = new AjoutEtudiant(this);
            a.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (etudiantDataGrid.SelectedItem != null)
            {
                int Id = (etudiantDataGrid.SelectedItem as Etudiant).IdEtudiant;
                ModifEtudiant a = new ModifEtudiant(Id);
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

        private void ResBtn_Click(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = _db.Etudiants.ToList();
        }
        public void Actualiser()
        {
            datagrid.ItemsSource = _db.Etudiants.ToList();
        }
        private void SupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                    int Id = (datagrid.SelectedItem as Etudiant).IdEtudiant;
                    Etudiant RmvEtudiant = (from c in _db.Etudiants where c.IdEtudiant == Id select c).Single();
                    _db.Etudiants.Remove(RmvEtudiant);
                    _db.SaveChanges();
                    datagrid.ItemsSource = _db.Etudiants.ToList();
                
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
                switch (etudiantComboBox.Text)
                {
                    case "Nom":
                        datagrid.ItemsSource = (from c in _db.Etudiants where c.Nom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Prénom":
                        datagrid.ItemsSource = (from c in _db.Etudiants where c.Prenom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Téléphone":
                        datagrid.ItemsSource = (from c in _db.Etudiants where c.Telephone.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Email":
                        datagrid.ItemsSource = (from c in _db.Etudiants where c.Email.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Collège":
                        datagrid.ItemsSource = (from c in _db.Etudiants where c.College.nomCo.IndexOf(search.Text) != -1 select c).ToList();
                        break;

                }
            }
            else datagrid.ItemsSource = _db.Etudiants.ToList();
        }

        private void PrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (etudiantDataGrid.SelectedItem as Etudiant).IdEtudiant;
                FicheEtudiant t = new FicheEtudiant(Id);
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void PrtBtn2_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (etudiantDataGrid.SelectedItem as Etudiant).IdEtudiant;
                Bulletin t = new Bulletin(Id);
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrtBtn3_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Id = (etudiantDataGrid.SelectedItem as Etudiant).IdEtudiant;
                string nom = (etudiantDataGrid.SelectedItem as Etudiant).Nom;
                string prenom = (etudiantDataGrid.SelectedItem as Etudiant).Prenom;
                MissMat t = new MissMat(Id,nom,prenom);
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Aucune Sélection faite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
