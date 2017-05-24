using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult index() {
            IEnumerable<string> files = Directory.EnumerateFiles(Server.MapPath("~/App_Data/uploads"));
            return View(files);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null){
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Resultados()
        {
            
            Dictionary<string, string> resultado = new Dictionary<string, string>();

            if (Utils.Validation.ContenidoFile(HttpContext,"CONTENIDO")&&Utils.Validation.ContenidoFile(HttpContext, "REGISTRADOS")){
                resultado = Utils.Validation.Resultado(HttpContext);
            }
                        
            return View(resultado);
        }

        public ActionResult Archivo(string file)
        {
            Dictionary<int,string> Data = Utils.Validation.TextReader(HttpContext , file);            

            return View(Data);
        }
    }
}