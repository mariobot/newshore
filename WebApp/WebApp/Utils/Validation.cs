using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Utils
{
    public class Validation
    {
        public static string value;

        public static bool ContenidoFile(HttpContextBase _HttpContext, string filename)
        {
            var relativePath = "~/App_Data/uploads/" + filename + ".txt";
            var absolutePath = _HttpContext.Server.MapPath(relativePath);
            if (System.IO.File.Exists(absolutePath)){
                return true;
            }else{
                return false;
            }   
        }

        public static Dictionary<int,string> TextReader(HttpContextBase _HttpContext, string filename)
        {
            var relativePath = "~/App_Data/uploads/" + filename + ".txt";
            var absolutePath = _HttpContext.Server.MapPath(relativePath);
            var count = 0;
            Dictionary<int, string> _Datos = new Dictionary<int,string>();            

            try{
                using (StreamReader sr = new StreamReader(absolutePath)){
                    while (sr.Peek() >= 0){
                        _Datos.Add(count,sr.ReadLine());
                        count += 1;
                    }
                }
            }
            catch (Exception e){
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return _Datos;
        }

        public static Dictionary<string, string> Resultado(HttpContextBase _HttpContext)
        {
            Dictionary<string, string> Resultados = new Dictionary<string, string>();
            Dictionary<int, string> _registrados = Utils.Validation.TextReader(_HttpContext, "REGISTRADOS");
            Dictionary<int, string> contenido;            

            foreach (var registro in _registrados)
            {
                contenido = Utils.Validation.TextReader(_HttpContext,"CONTENIDO");

                foreach (char c in registro.Value)
                {
                    contenido = RevisarContenido(contenido, c);
                }

                if (value == registro.Value)
                {
                   Resultados.Add(registro.Value,"Si existe");
                }
                else
                {
                   Resultados.Add(registro.Value, "No existe");
                }
                value = "";
            }

            return Resultados;
        }

        public static Dictionary<int,string> RevisarContenido(Dictionary<int, string> contenido, char c)
        {
            foreach (var cont in contenido)
            {
                if (cont.Value.ToUpper() == c.ToString().ToUpper())
                {
                    contenido.Remove(cont.Key);
                    value += cont.Value.ToUpper();
                    return contenido;
                }
            }

            return contenido;            
        }
    }
}