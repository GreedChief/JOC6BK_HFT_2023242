using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models.HelpClasses
{
    public class PlayerInfo
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public override bool Equals(object obj)
        {
            PlayerInfo b = obj as PlayerInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.PlayerId == b.PlayerId
                    && this.PlayerName == b.PlayerName;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.PlayerId, this.PlayerName);
        }
    }
}
