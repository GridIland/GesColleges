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
    public partial class MissMat : Window
    {
        GesCollegeContext db;
        int Id;
        string Nom, Prenom;
        public MissMat(int id, string nom , string prenom)
        {
            Nom = nom;
            Prenom = prenom;
            Id = id;
            InitializeComponent();
            db = new GesCollegeContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var col = db.Etudiants.Join(db.Colleges,
                                        Etudiant => Etudiant.IdCollege,
                                        College => College.IdCollege,
                                        (Etudiant, College) => new
                                        {
                                            IdCollege = Etudiant.IdCollege,
                                            IdEtudiant = Etudiant.IdEtudiant,
                                            Nom = Etudiant.Nom,
                                            Prenom = Etudiant.Prenom,
                                            nomCo = College.nomCo,
                                            AdresseSite = College.AdresseSite
                                        }).Where(c => c.IdEtudiant == Id).Single();
            var idmat = db.Notes.Join(db.Matieres,
                                        Notes => Notes.IdMatiere,
                                        Matiere => Matiere.IdMatiere,
                                        (Note, Matiere) => new
                                        {
                                            IdEtudiant = Note.IdEtudiant,
                                            IdMatiere = Matiere.IdMatiere
                                        }).Where(c => c.IdEtudiant == Id).ToList();
            List<object> verif = new List<object>();
            foreach (int id in idmat.Select(c => c.IdMatiere).ToList())
            {
                verif.Add(db.Matieres.Select(Matiere => new {
                    IdMatiere = Matiere.IdMatiere,
                    Nom = col.Nom,
                    Prenom = col.Prenom,
                    nomCo = col.nomCo,
                    AdresseSite = col.AdresseSite,
                    libelle = Matiere.libelle,

                }).Where(c => c.IdMatiere != id).Single());
            }
            var data = db.Matieres.Select(Matiere => new {
                        IdMatiere = Matiere.IdMatiere,
                        Nom = col.Nom,
                        Prenom = col.Prenom,
                        nomCo = col.nomCo,
                        AdresseSite = col.AdresseSite,
                        libelle = Matiere.libelle,

            }).Where(c => c.IdMatiere != Id).ToList();
            MatiereDisp a = new MatiereDisp();
            a.SetDataSource(verif);
            Crystalbul.ViewerCore.ReportSource = a;
        }
    }
}
