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
    
    public partial class Trabajador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trabajador()
        {
            this.Certificados = new HashSet<Certificados>();
            this.FichaClinica = new HashSet<FichaClinica>();
            this.ImagenesTrabajador = new HashSet<ImagenesTrabajador>();
            this.Producto = new HashSet<Producto>();
            this.Visitas = new HashSet<Visitas>();
            this.HorarioTrabajador = new HashSet<HorarioTrabajador>();
        }
    
        public int id { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public int idCargo { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public int idComuna { get; set; }
        public string clave { get; set; }
        public bool eliminado { get; set; }
        public Nullable<System.DateTime> fechaEliminado { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certificados> Certificados { get; set; }
        public virtual Comuna Comuna { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FichaClinica> FichaClinica { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImagenesTrabajador> ImagenesTrabajador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visitas> Visitas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HorarioTrabajador> HorarioTrabajador { get; set; }
    }
}
