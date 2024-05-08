using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        [StringLength(240)]
        public string PlayerName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public Player()
        {

        }

        public Player(string line)
        {
            string[] split = line.Split('#');
            PlayerId = int.Parse(split[0]);
            PlayerName = split[1];
        }
    }

}
