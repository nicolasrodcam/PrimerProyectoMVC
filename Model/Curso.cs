namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
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
                    if (Alumno_id > 0)
                    {
                        var cursos_tomados = ctx.AlumnoCurso.Where(x => x.Aumno_Id == Alumno_id)
                            .Select(x => x.Curso_Id)
                            .ToList();

                        cursos = ctx.Curso.Where(x => !cursos_tomados.Contains(x.Id)).ToList();

                    }
                    else
                    {
                        cursos = ctx.Curso.ToList();
                    }

                    //forma Mas Sencilla
                    //ctx.Database.SqlQuery<Curso>("SELECT c.* FROM Curso c WHERE Id MoT IN(SELECT Curso_Id FROM AlumnoCurso ac WHERE ac.Curso_Id = c.Id AND ac.Alumno_Id = @Alumno_Id ", new SqlParameter("Alumno_Id", Alumno_id)).ToList();
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
