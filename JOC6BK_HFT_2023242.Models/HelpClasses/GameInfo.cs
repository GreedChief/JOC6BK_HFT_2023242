using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models.HelpClasses
{
    public class GameInfo
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public DateTime Release { get; set; }
        public int DeveloperId { get; set; }

        public override bool Equals(object obj)
        {
            GameInfo b = obj as GameInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.GameId == b.GameId
                    && this.Title == b.Title
                    && this.Price == b.Price
                    && this.Rating == b.Rating
                    && this.Release == b.Release
                    && this.DeveloperId == b.DeveloperId;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GameId, this.Title, this.Price, this.Rating, this.Release, this.DeveloperId);
        }
    }
}
