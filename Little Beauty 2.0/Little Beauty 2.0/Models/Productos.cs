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
    
    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            this.Compra_producto = new HashSet<Compra_producto>();
            this.Orden_producto = new HashSet<Orden_producto>();
        }
    
        public int id_producto { get; set; }
        public int id_proveedor { get; set; }
        public string nombre_producto { get; set; }
        public int id_apartamento { get; set; }
        public int id_categoria { get; set; }
        public int id_sub_categoria { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string descripcion { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }
        public Nullable<System.DateTime> ult_actualizacion { get; set; }
        public string imagen { get; set; }
        public int stock { get; set; }
    
        public virtual Apartamentos Apartamentos { get; set; }
        public virtual Categorias Categorias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra_producto> Compra_producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden_producto> Orden_producto { get; set; }
        public virtual Proveedores Proveedores { get; set; }
        public virtual Sub_categorias Sub_categorias { get; set; }
    }
}
