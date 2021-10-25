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
    
    public partial class Producto
    {
        public int id { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public int idPaciente { get; set; }
        public int idTipoProducto { get; set; }
        public int cantidad { get; set; }
        public int idTrabajador { get; set; }
        public System.DateTime fechaIngreso { get; set; }
        public int idTutor { get; set; }
    
        public virtual Pacientes Pacientes { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
        public virtual Trabajador Trabajador { get; set; }
        public virtual Tutor Tutor { get; set; }
    }
}