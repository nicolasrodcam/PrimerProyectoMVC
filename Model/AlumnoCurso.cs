namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;

    [Table("AlumnoCurso")]
    public partial class AlumnoCurso
    {
        public int Id { get; set; }

        public int? Aumno_Id { get; set; }

        public int? Curso_Id { get; set; }

        [Required(ErrorMessage ="Debe Ingresar una nota para el Alumno")]
        [Range(40,100)]
        public int? Nota { get; set; }

        public virtual Alumno Alumno { get; set; }

        public virtual Curso Curso { get; set; }

        public List<AlumnoCurso> Listar(int Aumno_Id)
        {
            var alumnocursos = new List<AlumnoCurso>();
            try
            {
                using (var ctx = new TextContext())
                {
                    alumnocursos = ctx.AlumnoCurso
                        .Include("Curso")
                        .Where(x => x.Aumno_Id == Aumno_Id)
                        .ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return alumnocursos;
        }

        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new TextContext())
                {
                    ctx.Entry(this).State = EntityState.Added;
                    rm.SetResponse(true);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
    }
}
