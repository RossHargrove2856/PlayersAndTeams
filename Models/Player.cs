using System.ComponentModel.DataAnnotations;

namespace PlayersAndTeams.Models
{
    public class Player : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MinLength(2)]
        public string name { get; set; }
        [Required]
        public string level { get; set; }
        [Required]
        [MinLength(10)]
        public string description { get; set; }
        public Team team { get; set; }
    }
}