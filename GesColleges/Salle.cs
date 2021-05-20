namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salle")]
    public partial class Salle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salle()
        {
            Matiere = new HashSet<Matiere>();
        }

        [Key]
        public int IdSalle { get; set; }

        [Required]
        [StringLength(20)]
        public string Nom { get; set; }

        public int Capacite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matiere> Matiere { get; set; }
    }
}
