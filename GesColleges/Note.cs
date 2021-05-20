namespace GesColleges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Note")]
    public partial class Note
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEtudiant { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMatiere { get; set; }

        [ForeignKey("Enseignant")]
        public int IdEnseignant { get; set; }

        public double NoteControle { get; set; }

        public virtual Enseignant Enseignant { get; set; }
        
        public virtual Etudiant Etudiant { get; set; }

        public virtual Matiere Matiere { get; set; }
    }
}
