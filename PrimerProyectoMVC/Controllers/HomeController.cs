using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.IO;

namespace PrimerProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();
        private AlumnoCurso alumno_curso = new AlumnoCurso();
        private Curso curso = new Curso();
        private Adjunto adjunto = new Adjunto();
        // GET: Home
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        //home/ver/?Alumno_id=1
        public PartialViewResult Cursos(int Alumno_id)
        {
            ViewBag.CursosElegidos = alumno_curso.Listar(Alumno_id);
            ViewBag.Cursos = curso.Todos(Alumno_id);
            alumno_curso.Aumno_Id = Alumno_id;
            return PartialView(alumno_curso);
        }
        //home/ver/?Alumno_id=1
        public PartialViewResult Adjuntos(int Alumno_id)
        {
            ViewBag.adjuntos = adjunto.Listar(Alumno_id);
            return PartialView();
        }

        public JsonResult GuardarCurso(AlumnoCurso model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.function = "CargarCursos();";
                }
            }
            return Json(rm);

        }

        // GuardarAdjunto
        public JsonResult GuardarAdjunto(Adjunto model, HttpPostedFileBase Archivo)
        {
            var rm = new ResponseModel();

            if (Archivo != null)
            {
                //Nombre archivo
                string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Archivo.FileName);

                //Ruta donde se guardara
                Archivo.SaveAs(Server.MapPath("~/uploads/" + archivo));

                //establecemos en nuestro modelo el nombre del archivo
                model.Archivo = archivo;

                rm = model.Guardar();
                if (rm.response)
                {
                    rm.function = "CargarAdjuntos();";
                }
            }
            rm.SetResponse(false, "Deve Cargar un Archivo");
            return Json(rm);

        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                id == 0 ? new Alumno()
                : alumno.Obtener(id));

        }

        public JsonResult Guardar(Alumno model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
                //model.Guardar();
                //return Redirect("~/home/index");
            }
            return Json(rm);
            //else
            //{
            //    return View("~/views/home/crud.cshtml", model);
            //}
        }
        public ActionResult Eliminar(int id)
        {
            alumno.Id = id;
            alumno.Eliminar();
            return Redirect("~/home/index");
        }
    }
}