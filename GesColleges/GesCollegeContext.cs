using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GesColleges
{
    public class GesCollegeContext : DbContext
    {
        public GesCollegeContext()
            : base("name=GesCollegeContext")
        {
        }

        public virtual DbSet<College> Colleges { get; set; }
        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<Enseignant> Enseignants { get; set; }
        public virtual DbSet<Etudiant> Etudiants { get; set; }
        public virtual DbSet<Matiere> Matieres { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Salle> Salles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Departement>()
                .Property(e => e.nomDe)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Enseignant>()
                .Property(e => e.Nom)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Enseignant>()
                .Property(e => e.Prenom)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Enseignant>()
                .Property(e => e.Telephone)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Etudiant>()
                .Property(e => e.Nom)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Etudiant>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Etudiant>()
                .Property(e => e.Telephone)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Matiere>()
                .Property(e => e.libelle)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Salle>()
                .Property(e => e.Nom)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Note>().HasKey(e => new { e.IdEtudiant, e.IdMatiere,e.IdEnseignant});
        }
    }
}
