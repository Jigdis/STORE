using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STORE.Entities.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }

        [StringLength(150)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Apellidos { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }
        public List<ClienteArticulo> ClientesArticulos { get; set; }
    }
}
