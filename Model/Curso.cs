namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Curso> Todos(int Alumno_id = 0)
        {
            var cursos = new List<Curso>();
            try
            {
                using (var ctx = new TextContext())
                {
                    cursos = ctx.Curso.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return cursos;
        }
    }
}
