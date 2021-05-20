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
    public partial class FicheEtudiant : Window
    {
        GesCollegeContext db;
        int Id;
        public FicheEtudiant(int id)
        {
            InitializeComponent();
            Id = id;
            db = new GesCollegeContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = db.Etudiants
                        .Join(
                         db.Colleges,
                            College => College.IdCollege,
                            Etudiant => Etudiant.IdCollege,
                            (Etudiant, College) => new
                            {
                                IdEtudiant = Etudiant.IdEtudiant,
                                Nom = Etudiant.Nom,
                                Prenom = Etudiant.Prenom,
                                Telephone = Etudiant.Telephone,
                                Email = Etudiant.Email,
                                AnneeEntree = Etudiant.AnneeEntree,
                                nomCo = College.nomCo,
                                AdresseSite = College.AdresseSite
                            }
                            ).Where(c => c.IdEtudiant == Id).ToList();
            ReportDocument report = new ReportDocument();
            FSEtudiant d = new FSEtudiant();
            d.SetDataSource(data);
            CrystalEtudiant.ViewerCore.ReportSource = d;
        }
    }
}
