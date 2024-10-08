﻿using JOC6BK_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Repository
{
    public class RoleRepository : Repository<Role>, IRepository<Role>
    {
        public RoleRepository(GameDbContext ctx) : base(ctx)
        {
        }

        public override Role Read(int id)
        {
            return ctx.Roles.FirstOrDefault(t => t.RoleId == id);
        }

        public override void Update(Role item)
        {
            var old = Read(item.RoleId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
