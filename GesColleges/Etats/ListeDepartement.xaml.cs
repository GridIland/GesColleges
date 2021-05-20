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
    public partial class ListeDepartement : Window
    {
        GesCollegeContext db;
        public ListeDepartement()
        {
            InitializeComponent();
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
                                IdDepartement = Enseignant.IdDepartement,
                                IdCollege = Departement.IdCollege,
                                Nom = Enseignant.Nom,
                                Prenom = Enseignant.Prenom,
                                nomDe = Departement.nomDe,
                                ChefDepartement = Enseignant.ChefDepartement
                            }
                            ).Join(
                            db.Colleges,
                            College => College.IdCollege,
                            Enseignant => Enseignant.IdCollege,
                            (Enseignant, College) => new
                            {
                                IdEnseignant = Enseignant.IdEnseignant,
                                IdCollege = Enseignant.IdCollege,
                                IdDepartement = Enseignant.IdDepartement,
                                Nom = Enseignant.Nom,
                                Prenom = Enseignant.Prenom,
                                nomDe = Enseignant.nomDe,
                                nomCo = College.nomCo,
                                AdresseSite = College.AdresseSite,
                                ChefDepartement = Enseignant.ChefDepartement
                            }
                            ).Where(c => c.ChefDepartement == true).ToList();
            ListeDepart a = new ListeDepart();
            a.SetDataSource(data);
            CrystalEnseignant.ViewerCore.ReportSource = a;
        }
    }
}
