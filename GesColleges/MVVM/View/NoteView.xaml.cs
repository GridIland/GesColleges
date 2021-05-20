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
    public partial class NoteView : UserControl
    {
        GesCollegeContext _db = new GesCollegeContext();
        public static DataGrid datagrid;
        public NoteView()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            noteDataGrid.ItemsSource = _db.Notes.ToList();
            datagrid = noteDataGrid;
        }
        public void Actualiser()
        {
            datagrid.ItemsSource = _db.Notes.ToList();
        }
        private void AjtBtn_Click(object sender, RoutedEventArgs e)
        {
            AjoutNote a = new AjoutNote(this);
            a.ShowDialog();
        }

        private void MdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                int Idmat = (noteDataGrid.SelectedItem as Note).IdMatiere;
                int Idetu = (noteDataGrid.SelectedItem as Note).IdEtudiant;
                int Idens = (noteDataGrid.SelectedItem as Note).IdEnseignant;
                ModifNote a = new ModifNote(Idmat, Idetu , Idens);
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
                    int Idmat = (datagrid.SelectedItem as Note).IdMatiere;
                    int Idetu = (datagrid.SelectedItem as Note).IdEtudiant;
                    int IdEns = (datagrid.SelectedItem as Note).IdEnseignant;
                    Note UpdtNt = (from c in _db.Notes where c.IdEtudiant == Idetu && c.IdMatiere == Idmat && c.IdEnseignant == IdEns select c).Single();
                    _db.Notes.Remove(UpdtNt);
                    _db.SaveChanges();
                    datagrid.ItemsSource = _db.Notes.ToList();
                
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
                switch (noteComboBox.Text)
                {
                    case "Nom élève":
                        datagrid.ItemsSource = (from c in _db.Notes where c.Etudiant.Nom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Prénom élève":
                        datagrid.ItemsSource = (from c in _db.Notes where c.Etudiant.Prenom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Nom enseignant":
                        datagrid.ItemsSource = (from c in _db.Notes where c.Enseignant.Nom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Prénom enseignant":
                        datagrid.ItemsSource = (from c in _db.Notes where c.Enseignant.Prenom.IndexOf(search.Text) != -1 select c).ToList();
                        break;
                    case "Note":
                        datagrid.ItemsSource = (from c in _db.Notes where c.NoteControle.ToString().IndexOf(search.Text) != -1 select c).ToList();
                        break;

                }
            }
            else datagrid.ItemsSource = _db.Notes.ToList();
        }
    }
}
