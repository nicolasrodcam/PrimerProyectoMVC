namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Alumno")]
    public partial class Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumno()
        {
            Adjunto = new HashSet<Adjunto>();
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        public int? sexo { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? Fecha_Nacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjunto> Adjunto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Alumno> Listar()
        {
            var alumnos = new List<Alumno>();
            try
            {
                using (var ctx = new TextContext())
                {
                    alumnos = ctx.Alumno.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return alumnos;
        }
        public Alumno Obtener(int id)
        {
            var alumnos = new Alumno();
            try
            {
                using (var ctx = new TextContext())
                {
                    alumnos = ctx.Alumno.Include("AlumnoCurso")
                        .Include("AlumnoCurso.Curso")
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return alumnos;
        }

        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new TextContext())
                {
                    if (this.Id > 0)
                    {
                        ctx.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {

                        ctx.Entry(this).State = EntityState.Added;
                    }
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
        public void Eliminar()
        {
            try
            {
                using (var ctx = new TextContext())
                {
                    ctx.Entry(this).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}