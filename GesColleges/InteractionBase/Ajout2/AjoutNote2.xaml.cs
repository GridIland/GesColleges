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

namespace GesColleges.InteractionBase.Ajout2
{
    /// <summary>
    /// Logique d'interaction pour AjoutCollege.xaml
    /// </summary>
    public partial class AjoutNote2 : Window
    {
        GesCollegeContext _db = new GesCollegeContext();
        public AjoutNote2()
        {
            InitializeComponent();
            etudiantComboBox.IsEnabled = false;
            enseignantComboBox.IsEnabled = false;
            Liaison();
        }

        private void Liaison()
        {
            // Combobox Etudiant liaison
            etudiantComboBox.ItemsSource = _db.Etudiants.Select(c => new { IdEtudiant = c.IdEtudiant, nomPrenom = c.Nom + " " + c.Prenom}).ToList();
            etudiantComboBox.DisplayMemberPath = "nomPrenom";
            etudiantComboBox.SelectedValuePath = "IdEtudiant";

            // Combobox Matières liaison
            matiereComboBox.ItemsSource = _db.Matieres.Select(c => new { IdMatiere = c.IdMatiere, libelle = c.libelle }).ToList();
            matiereComboBox.DisplayMemberPath = "libelle";
            matiereComboBox.SelectedValuePath = "IdMatiere";

            // ComboBox Enseignant liaison
            enseignantComboBox.ItemsSource = _db.Enseignants.Select(c => new { IdEnseignant = c.IdEnseignant, nomPrenom = c.Nom + " " + c.Prenom }).ToList();
            enseignantComboBox.DisplayMemberPath = "nomPrenom";
            enseignantComboBox.SelectedValuePath = "IdEnseignant";
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource noteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("noteViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // noteViewSource.Source = [source de données générique]
        }

        private bool verification()
        {
            if (noteControleTextBox.Text == "" || etudiantComboBox.Text == "" || matiereComboBox.Text == "")
            {
                return false;
            }
            else
            return true;
        }



        private void RegistrerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (verification())
            {
                Note c = new Note()
                {
                    IdEtudiant = int.Parse(etudiantComboBox.SelectedValue.ToString()),
                    IdMatiere = int.Parse(matiereComboBox.SelectedValue.ToString()),
                    IdEnseignant = int.Parse(enseignantComboBox.SelectedValue.ToString()),
                    NoteControle = double.Parse(noteControleTextBox.Text)
                };
                _db.Notes.Add(c);
                _db.SaveChanges();
                MessageBox.Show("Enregistrement Réussi", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez completer tous les champs", "Erreur de Remplissage", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void matiereComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (matiereComboBox.SelectedItem!=null)
            {
                etudiantComboBox.IsEnabled = true;
            }
        }

        private void etudiantComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (etudiantComboBox.SelectedItem!=null)
            {
                int idEtu = int.Parse(etudiantComboBox.SelectedValue.ToString());
                int idCol = (from c in _db.Etudiants where c.IdEtudiant == idEtu select c).Single().IdCollege;
                int idMat = int.Parse(matiereComboBox.SelectedValue.ToString());
                enseignantComboBox.ItemsSource = _db.Enseignants
                    .Join(_db.Departements , 
                            Enseignant => Enseignant.IdDepartement,
                            Departement => Departement.IdDepartement,
                            (Enseignant,Departement) => 
                            new { 
                                IdEnseignant = Enseignant.IdEnseignant,
                                nomPrenom = Enseignant.Nom + " " + Enseignant.Prenom,
                                IdMatiere = Enseignant.IdMatiere,
                                IdCollege = Departement.IdCollege
                            }).Where(c=> c.IdCollege == idCol && c.IdMatiere ==idMat).ToList();
                enseignantComboBox.DisplayMemberPath = "nomPrenom";
                enseignantComboBox.SelectedValuePath = "IdEnseignant";
                enseignantComboBox.IsEnabled = true;
            }
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
