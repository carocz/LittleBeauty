//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Little_Beauty_2._0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Apartamentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartamentos()
        {
            this.Categorias = new HashSet<Categorias>();
            this.Productos = new HashSet<Productos>();
        }
    
        public int id_apartamento { get; set; }
        public string nombre_apartamento { get; set; }
        public int id_empleado_encargado { get; set; }
        public string descripcion { get; set; }
    
        public virtual Empleados Empleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categorias> Categorias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Productos> Productos { get; set; }
    }
}
