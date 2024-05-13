using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models.HelpClasses
{
    public class YearInfo
    {
        public int Year { get; set; }
        public double? AvgRating { get; set; }
        public int GameNumber { get; set; }

        public override bool Equals(object obj)
        {
            YearInfo b = obj as YearInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Year == b.Year
                    && this.AvgRating == b.AvgRating
                    && this.GameNumber == b.GameNumber;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Year, this.AvgRating, this.GameNumber);
        }
    }
}
