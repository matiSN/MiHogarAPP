//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConexionMiHogarAPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Postulaciones
    {
        public int id { get; set; }
        public int idTutor { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fecha { get; set; }
        public string correo { get; set; }
    
        public virtual Tutor Tutor { get; set; }
    }
}
