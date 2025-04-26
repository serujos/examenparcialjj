using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace examenparcialjj.Models
{
    public class Player
    {
        public int Id { get; set; } //llave primaria

        [Required]
        public string Nombre { get; set; }

        [Range(14, 45)] //hubo jugadores hasta de 45 años
        public int Edad { get; set; }

        [Required]
        public string Posicion { get; set; }

        public List<Assignment> Assignments { get; set; }
    }
}
