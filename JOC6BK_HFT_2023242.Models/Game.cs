using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [StringLength(240)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public double Price { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        public int DeveloperId { get; set; }

        public virtual Developer Developer { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


        public Game()
        {

        }

        public Game(string line)
        {
            string[] split = line.Split('#');
            GameId = int.Parse(split[0]);
            Title = split[1];
            Price = double.Parse(split[2]);
            DeveloperId = int.Parse(split[3]);
            Release = DateTime.Parse(split[4].Replace('*', '.'));
            Rating = double.Parse(split[5]);
        }

    }

}
