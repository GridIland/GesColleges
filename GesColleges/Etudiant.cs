namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Etudiant")]
    public partial class Etudiant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etudiant()
        {
            Note = new HashSet<Note>();
        }

        [Key]
        public int IdEtudiant { get; set; }

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

        public int AnneeEntree { get; set; }

        [ForeignKey("College")]
        public int IdCollege { get; set; }

        public virtual College College { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Note { get; set; }
    }
}
