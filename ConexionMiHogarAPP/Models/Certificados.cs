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
    
    public partial class Certificados
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idTrabajador { get; set; }
        public string archivo { get; set; }
    
        public virtual Trabajador Trabajador { get; set; }
    }
}