namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("College")]
    public partial class College
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public College()
        {
            Departement = new HashSet<Departement>();
        }

        [Key]
        public int IdCollege { get; set; }

        [Required]
        [StringLength(50)]
        public string nomCo { get; set; }

        [Required]
        [StringLength(40)]
        public string AdresseSite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Departement> Departement { get; set; }
    }
}
