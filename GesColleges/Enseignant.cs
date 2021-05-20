namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enseignant")]
    public partial class Enseignant
    {
        public Enseignant()
        {
            Notes = new HashSet<Note>();
        }

        [Key]
        public int IdEnseignant { get; set; }

        [Required]
        [StringLength(20)]
        public string Nom { get; set; }

        [Required]
        [StringLength(30)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Required]
        public string Email { get; set; }

        public bool ChefDepartement { get; set; }

        public DateTime DatePriseFonction { get; set; }

        [ForeignKey ("Departement")]
        public int IdDepartement { get; set; }

        [ForeignKey("Matiere")]
        public int IdMatiere { get; set; }

        public virtual Departement Departement { get; set; }

        public virtual Matiere Matiere { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
