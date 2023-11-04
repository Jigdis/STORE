using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STORE.Entities.Models
{
    [Table("ClienteArticulo")]
    public class ClienteArticulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteArticuloID { get; set; }
        public int ClienteID { get; set; }
        public int ArticuloID { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }

        [ForeignKey("ArticuloID")]
        public Articulo Articulo { get; set; }
    }
}
