namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Matiere")]
    public partial class Matiere
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Matiere()
        {
            Enseignant = new HashSet<Enseignant>();
            Note = new HashSet<Note>();
        }

        [Key]
        public int IdMatiere { get; set; }

        [Required]
        [StringLength(30)]
        public string libelle { get; set; }

        [ForeignKey ("Salle")]
        public int IdSalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enseignant> Enseignant { get; set; }

        public virtual Salle Salle { get; set; }

        public virtual ICollection<Note> Note { get; set; }
    }
}
