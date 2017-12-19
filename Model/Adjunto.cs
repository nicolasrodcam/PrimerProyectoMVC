namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adjunto")]
    public partial class Adjunto
    {
        public int Id { get; set; }

        public int? Aumno_Id { get; set; }

        [StringLength(100)]
        public string Archivo { get; set; }

        public virtual Alumno Alumno { get; set; }
    }
}
