using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STORE.Entities.Models
{
    [Table("Tienda")]
    public class Tienda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TiendaID { get; set; }

        [StringLength(250)]
        public string Sucursal { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }
        public List<ArticuloTienda> ArticulosTiendas { get; set; }
    }

}
