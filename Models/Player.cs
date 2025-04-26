using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examenparcialjj.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Range(14, 45)] //hubo jugadores de 45 años
        public int Edad { get; set; }

        [Required]
        public string Posicion { get; set; }

        
        [Display(Name = "Equipo Actual")]
        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
