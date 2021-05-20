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

namespace GesColleges.Etats.Etat_avancé
{
    /// <summary>
    /// Logique d'interaction pour NotesMoyennesColg.xaml
    /// </summary>
    public partial class NotesMoyennesDep : Window
    {
        GesCollegeContext db;
        int IdMat, IdDep;
        public NotesMoyennesDep(int idMat , int idDep)
        {
            IdMat = idMat;
            IdDep = idDep;
            db = new GesCollegeContext();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = db.Notes
                        .Join(
                            db.Etudiants,
                            Note => Note.IdEtudiant,
                            Etudiant => Etudiant.IdEtudiant,
                            (Note, Etudiant) => new
                            {
                                IdEtudiant = Etudiant.IdEtudiant,
                                IdCollege = Etudiant.IdCollege,
                                Nom = Etudiant.Nom,
                                Prenom = Etudiant.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle,
                                IdEnseignant = Note.IdEnseignant
                            }
                            ).Join(
                            db.Matieres,
                            Note => Note.IdMatiere,
                            Matiere => Matiere.IdMatiere,
                            (Note, Matiere) => new
                            {
                                IdEtudiant = Note.IdEtudiant,
                                IdCollege = Note.IdCollege,
                                IdEnseignant = Note.IdEnseignant,
                                Nom = Note.Nom,
                                Prenom = Note.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle,
                                libelle = Matiere.libelle
                            }
                            ).Join(
                            db.Colleges,
                            Note => Note.IdCollege,
                            College => College.IdCollege,
                            (Note, College) => new
                            {
                                IdEtudiant = Note.IdEtudiant,
                                IdCollege = Note.IdCollege,
                                IdEnseignant = Note.IdEnseignant,
                                Nom = Note.Nom,
                                Prenom = Note.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle,
                                libelle = Note.libelle,
                                nomCo = College.nomCo,
                                AdresseSite = College.AdresseSite
                            }
                            ).Join(
                            db.Enseignants,
                            Note => Note.IdEnseignant,
                            Enseignant => Enseignant.IdEnseignant,
                            (Note, Enseignant) => new
                            {
                                IdEtudiant = Note.IdEtudiant,
                                IdCollege = Note.IdCollege,
                                IdEnseignant = Note.IdEnseignant,
                                Nom = Note.Nom,
                                Prenom = Note.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle,
                                libelle = Note.libelle,
                                nomCo = Note.nomCo,
                                AdresseSite = Note.AdresseSite,
                                IdDepartement = Enseignant.IdDepartement
                            }
                            ).Join(
                            db.Departements,
                            Note => Note.IdDepartement,
                            Departement => Departement.IdDepartement,
                            (Note, Departement) => new
                            {
                                IdEtudiant = Note.IdEtudiant,
                                IdCollege = Note.IdCollege,
                                IdEnseignant = Note.IdEnseignant,
                                Nom = Note.Nom,
                                Prenom = Note.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle,
                                libelle = Note.libelle,
                                nomCo = Note.nomCo,
                                AdresseSite = Note.AdresseSite,
                                IdDepartement = Note.IdDepartement,
                                nomDe = Departement.nomDe
                            }
                            ).Where(c => c.IdMatiere == IdMat && c.IdDepartement == IdDep).ToList();
            NoteMoy2 a = new NoteMoy2();
            a.SetDataSource(data);
            CrystalEnseignant.ViewerCore.ReportSource = a;
        }
    }
}
