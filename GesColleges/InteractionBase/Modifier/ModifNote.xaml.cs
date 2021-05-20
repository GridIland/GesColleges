using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ModifNote : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        int IdMat;
        int IdEtu;
        int IdEns;
        public ModifNote(int id1 , int id2, int id3)
        {
            InitializeComponent();
            Liaison();
            IdMat = id1;
            IdEtu = id2;
            IdEns = id3;
            Remplissage();
        }

        private void Liaison()
        {
            // Combobox Etudiant liaison
            etudiantComboBox.ItemsSource = _db.Etudiants.Select(c => new { IdEtudiant = c.IdEtudiant, nomPrenom = c.Nom + " " + c.Prenom }).ToList();
            etudiantComboBox.DisplayMemberPath = "nomPrenom";
            etudiantComboBox.SelectedValuePath = "IdEtudiant";

            // Combobox Matières liaison
            matiereComboBox.ItemsSource = _db.Matieres.Select(c => new { IdMatiere = c.IdMatiere, libelle = c.libelle }).ToList();
            matiereComboBox.DisplayMemberPath = "libelle";
            matiereComboBox.SelectedValuePath = "IdMatiere";

            // ComboBox Enseignant liaison
            enseignantComboBox.ItemsSource = _db.Enseignants.Select(c=> new { IdEnseignant = c.IdEnseignant, nomPrenom = c.Nom + " " + c.Prenom}).ToList();
            enseignantComboBox.DisplayMemberPath = "nomPrenom";
            enseignantComboBox.SelectedValuePath = "IdEnseignant";
        }
        private void Remplissage()
        {
            Note RmpNt = (from c in _db.Notes where c.IdEtudiant == IdEtu && c.IdMatiere == IdMat select c).Single();
            matiereComboBox.SelectedValue = RmpNt.IdMatiere;
            etudiantComboBox.SelectedValue = RmpNt.IdEtudiant;
            enseignantComboBox.SelectedValue = RmpNt.IdEnseignant;
            noteControleTextBox.Text = RmpNt.NoteControle.ToString();
            
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous valider la modification ?", "Modification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Note UpdtNt = (from c in _db.Notes where c.IdEtudiant == IdEtu && c.IdMatiere == IdMat select c).Single();
                UpdtNt.IdEtudiant = int.Parse(etudiantComboBox.SelectedValue.ToString());
                UpdtNt.IdMatiere = int.Parse(matiereComboBox.SelectedValue.ToString());
                UpdtNt.NoteControle = double.Parse(noteControleTextBox.Text);
                UpdtNt.IdEnseignant = int.Parse(enseignantComboBox.SelectedValue.ToString());
                _db.SaveChanges();
                NoteView.datagrid.ItemsSource = _db.Notes.ToList();
                this.Close();
            }
            
        }

        private void Window_Loaded()
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource noteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("noteViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // noteViewSource.Source = [source de données générique]
        }

        private void noteControleTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void noteControleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (noteControleTextBox.Text.Length > 1)
            {
                if (double.Parse(noteControleTextBox.Text) > 20)
                {
                    MessageBox.Show("La note ne doit pas dépasser 20", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
                    noteControleTextBox.Text = "0";
                }
            }
        }
    }
}
