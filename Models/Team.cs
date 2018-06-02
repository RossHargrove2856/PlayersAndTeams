using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace PlayersAndTeams.Models
{
    public class Team : BaseEntity
    {
        public Team() {
            players = new List<Player>();
        }
        [Key]
        public int id { get; set; }
        [Required]
        [MinLength(2)]
        public string name { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        [MinLength(10)]
        public string information { get; set; }
        public ICollection<Player> players { get; set; }
    }
}