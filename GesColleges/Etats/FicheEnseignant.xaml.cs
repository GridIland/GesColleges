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
    public partial class FicheEnseignant : Window
    {
        GesCollegeContext db;
        int Id;
        public FicheEnseignant(int id)
        {
            InitializeComponent();
            Id = id;
            db = new GesCollegeContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = db.Enseignants
                        .Join(
                         db.Departements,
                            Departement => Departement.IdDepartement,
                            Enseignant => Enseignant.IdDepartement,
                            (Enseignant, Departement) => new
                            {
                                IdEnseignant = Enseignant.IdEnseignant,
                                IdMatiere = Enseignant.IdMatiere,
                                IdCollege = Departement.IdCollege,
                                Nom = Enseignant.Nom,
                                Prenom = Enseignant.Prenom,
                                Telephone = Enseignant.Telephone,
                                Email = Enseignant.Email,
                                DatePriseFonction = Enseignant.DatePriseFonction,
                                nomDe = Departement.nomDe,
                            }
                            ).Join(
                            db.Matieres,
                            Matiere => Matiere.IdMatiere,
                            Enseignant => Enseignant.IdMatiere,
                            (Enseignant, Matiere) => new
                            {
                                IdEnseignant = Enseignant.IdEnseignant,
                                IdMatiere = Enseignant.IdMatiere,
                                IdCollege = Enseignant.IdCollege,
                                Nom = Enseignant.Nom,
                                Prenom = Enseignant.Prenom,
                                Telephone = Enseignant.Telephone,
                                Email = Enseignant.Email,
                                DatePriseFonction = Enseignant.DatePriseFonction,
                                nomDe = Enseignant.nomDe,
                                libelle = Matiere.libelle
                            }
                            ).Join(
                            db.Colleges,
                            College => College.IdCollege,
                            Enseignant => Enseignant.IdCollege,
                            (Enseignant, College) => new
                            {
                                IdEnseignant = Enseignant.IdEnseignant,
                                IdMatiere = Enseignant.IdMatiere,
                                Nom = Enseignant.Nom,
                                Prenom = Enseignant.Prenom,
                                Telephone = Enseignant.Telephone,
                                Email = Enseignant.Email,
                                DatePriseFonction = Enseignant.DatePriseFonction,
                                nomDe = Enseignant.nomDe,
                                libelle = Enseignant.libelle,
                                nomCo = College.nomCo,
                                AdresseSite = College.AdresseSite
                            }
                            ).Where(c => c.IdEnseignant == Id).ToList();
            FSEnseignant a = new FSEnseignant();
            a.SetDataSource(data);
            CrystalEnseignant.ViewerCore.ReportSource = a;
        }
    }
}
