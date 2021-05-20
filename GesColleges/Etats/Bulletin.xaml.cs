using CrystalDecisions.CrystalReports.Engine;
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

namespace GesColleges.Etats
{
    /// <summary>
    /// Logique d'interaction pour FicheEtudiant.xaml
    /// </summary>
    public partial class Bulletin : Window
    {
        GesCollegeContext db;
        int Id;
        public Bulletin(int id)
        {
            Id = id;
            InitializeComponent();
            db = new GesCollegeContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = db.Notes
                        .Join(
                            db.Etudiants,
                            Note => Note.IdEtudiant,
                            Etudiant => Etudiant.IdEtudiant,
                            (Note,Etudiant) => new
                            {
                                IdEtudiant = Etudiant.IdEtudiant,
                                IdCollege = Etudiant.IdCollege,
                                Nom = Etudiant.Nom,
                                Prenom = Etudiant.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle
                            }
                            ).Join(
                            db.Matieres,
                            Note => Note.IdMatiere,
                            Matiere => Matiere.IdMatiere,
                            (Note , Matiere) => new
                            {
                                IdEtudiant = Note.IdEtudiant,
                                IdCollege = Note.IdCollege,
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
                                Nom = Note.Nom,
                                Prenom = Note.Prenom,
                                IdMatiere = Note.IdMatiere,
                                NoteControle = Note.NoteControle,
                                libelle = Note.libelle,
                                nomCo = College.nomCo,
                                AdresseSite = College.AdresseSite
                            }
                            ).Where(c => c.IdEtudiant == Id).ToList();
            BullNote a = new BullNote();
            a.SetDataSource(data);
            Crystalbul.ViewerCore.ReportSource = a;
        }
    }
    public enum Mention
    {
        NonAdmis = 1 ,
        Admis = 2
    }
}
