using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STORE.Entities.Models
{
    [Table("ArticuloTienda")]
    public class ArticuloTienda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticuloTiendaID { get; set; }
        public int ArticuloID { get; set; }
        public int TiendaID { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("ArticuloID")]
        public Articulo Articulo { get; set; }

        [ForeignKey("TiendaID")]
        public Tienda Tienda { get; set; }
    }
}
