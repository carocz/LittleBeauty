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
    
    public partial class Empleados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleados()
        {
            this.Apartamentos = new HashSet<Apartamentos>();
        }
    
        public int id_empleado { get; set; }
        public Nullable<int> id_pais { get; set; }
        public Nullable<int> id_entidad { get; set; }
        public Nullable<int> id_municipio { get; set; }
        public string nombre_empleado { get; set; }
        public string direccion { get; set; }
        public string apellido_empleado { get; set; }
        public int telefono { get; set; }
        public string rol { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apartamentos> Apartamentos { get; set; }
        public virtual Entidades Entidades { get; set; }
        public virtual Municipios Municipios { get; set; }
        public virtual Paises Paises { get; set; }
    }
}
