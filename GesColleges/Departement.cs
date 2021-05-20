namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Departement")]
    public partial class Departement
    {
        
        public Departement()
        {
            Enseignant = new HashSet<Enseignant>();
        }

        [Key]
        public int IdDepartement { get; set; }

        [Required]
        [StringLength(40)]
        public string nomDe { get; set; }

        [ForeignKey ("College")]
        public int IdCollege { get; set; }

        public virtual College College { get; set; }

        
        public virtual ICollection<Enseignant> Enseignant { get; set; }
    }
}
