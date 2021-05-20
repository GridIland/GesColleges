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
    public partial class ListeEnseignant : Window
    {
        GesCollegeContext db;
        int Id;
        public ListeEnseignant(int id)
        {
            Id = id;
            InitializeComponent();
            db = new GesCollegeContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = db.Enseignants
                        .Join(
                            db.Matieres,
                            Matiere => Matiere.IdMatiere,
                            Enseignant => Enseignant.IdMatiere,
                            (Enseignant, Matiere) => new
                            {
                                IdEnseignant = Enseignant.IdEnseignant,
                                IdMatiere = Enseignant.IdMatiere,
                                Nom = Enseignant.Nom,
                                Prenom = Enseignant.Prenom,
                                Telephone = Enseignant.Telephone,
                                Email = Enseignant.Email,
                                DatePriseFonction = Enseignant.DatePriseFonction,
                                libelle = Matiere.libelle
                            }
                            ).Where(c => c.IdMatiere == Id).ToList();
            ListeEns a = new ListeEns();
            a.SetDataSource(data);
            CrystalListEns.ViewerCore.ReportSource = a;
        }
    }
}
