using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Models.HelpClasses
{
    public class RoleInfo
    {
        public int RoleId { get; set; }
        public int Priority { get; set; }
        public string RoleName { get; set; }

        public override bool Equals(object obj)
        {
            RoleInfo b = obj as RoleInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.RoleId == b.RoleId
                    && this.Priority == b.Priority
                    && this.RoleName == b.RoleName;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.RoleId, this.Priority, this.RoleName);
        }
    }
}
