using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models
{
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeveloperId { get; set; }

        [Required]
        [StringLength(240)]
        public string DeveloperName { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Developer()
        {
            Games = new HashSet<Game>();
        }

        public Developer(string line)
        {
            string[] split = line.Split('#');
            DeveloperId = int.Parse(split[0]);
            DeveloperName = split[1];
            Games = new HashSet<Game>();
        }
    }

}
