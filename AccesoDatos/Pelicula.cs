using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Pelicula
    {
        //Campos parseados con SQL2Object
        public string PeliculaID { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public int Anio { get; set; }        
    }   
}
