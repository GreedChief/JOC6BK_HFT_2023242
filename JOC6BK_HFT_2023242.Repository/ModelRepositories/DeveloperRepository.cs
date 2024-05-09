using JOC6BK_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOC6BK_HFT_2023242.Repository
{
    public class DeveloperRepository : Repository<Developer>, IRepository<Developer>
    {
        public DeveloperRepository(GameDbContext ctx) : base(ctx)
        {
        }

        public override Developer Read(int id)
        {
            return ctx.Developers.FirstOrDefault(t => t.DeveloperId == id);
        }

        public override void Update(Developer item)
        {
            var old = Read(item.DeveloperId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
