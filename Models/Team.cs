using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace examenparcialjj.Models
{
    public class Team
    {
        public int Id { get; set; } 

        [Required]
        public string Nombre { get; set; }

        public List<Assignment> Assignments { get; set; }
    }
}
