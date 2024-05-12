using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JOC6BK_HFT_2023242.Logic.GameLogic;

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
            var existingRole = repo.ReadAll().FirstOrDefault(r => r.RoleName == item.RoleName);
            if (existingRole != null)
            {
                throw new ArgumentException("The name of the role is already in the database");
            }
            repo.Create(item);
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
        public IEnumerable<RoleInfo> GetMostPlayedRole()
        {
            var roleCounts = repo.ReadAll()
                                .GroupBy(r => r.RoleName)
                                .Select(group => new
                                {
                                    RoleName = group.Key,
                                    RoleCount = group.Count()
                                })
                                .OrderByDescending(x => x.RoleCount)
                                .Take(5)
                                .ToList();

            var mostMostPlayedRole = roleCounts.Select(roleCount =>
                new RoleInfo
                {
                    RoleName = roleCount.RoleName.ToString(),
                    RoleCount = roleCount.RoleCount
                });

            return mostMostPlayedRole.Take(1);
        }
        public class RoleInfo
        {
            public string RoleName { get; set; }
            public int RoleCount { get; set; }
            public override bool Equals(object obj)
            {
                RoleInfo b = obj as RoleInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.RoleName == b.RoleName
                        && this.RoleCount == b.RoleCount;
                }
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(this.RoleName, this.RoleCount);
            }
        }
    }
}

