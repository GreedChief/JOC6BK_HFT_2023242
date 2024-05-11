using JOC6BK_HFT_2023242.Models;
using System.Collections.Generic;
using System.Linq;

namespace JOC6BK_HFT_2023242.Logic
{
    public interface IRoleLogic
    {
        void Create(Role item);
        void Delete(int id);
        Role Read(int id);
        IQueryable<Role> ReadAll();
        void Update(Role item);
        IEnumerable<RoleLogic.MostPlayedRoleInfo> GetMostPlayedRole();

    }
}