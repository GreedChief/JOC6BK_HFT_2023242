using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Logic
{
    public class RoleLogic : IRoleLogic
    {
        IRepository<Role> repo;

        public RoleLogic(IRepository<Role> repo)
        {
            this.repo = repo;
        }

        public void Create(Role item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Role Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Role> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Role item)
        {
            this.repo.Update(item);
        }
        public IEnumerable<MostPlayedRoleInfo> GetMostPlayedRole()
        {
            var roleCounts = repo.ReadAll()
                                .GroupBy(r => r.RoleId)
                                .Select(group => new
                                {
                                    RoleId = group.Key,
                                    RoleCount = group.Count()
                                })
                                .OrderByDescending(x => x.RoleCount)
                                .Take(5)
                                .ToList();

            var mostMostPlayedRole = roleCounts.Select(roleCount =>
                new MostPlayedRoleInfo
                {
                    RoleId = roleCount.RoleId.ToString(),
                    RoleCount = roleCount.RoleCount
                });

            return mostMostPlayedRole.Take(1);
        }
        public class MostPlayedRoleInfo
        {
            public string RoleId { get; set; }
            public int RoleCount { get; set; }
        }
    }
}

