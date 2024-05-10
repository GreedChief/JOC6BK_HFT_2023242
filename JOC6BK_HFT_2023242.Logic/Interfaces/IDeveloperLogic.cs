using JOC6BK_HFT_2023242.Models;
using System.Linq;

namespace JOC6BK_HFT_2023242.Logic
{
    public interface IDeveloperLogic
    {
        void Create(Developer item);
        void Delete(int id);
        Developer Read(int id);
        IQueryable<Developer> ReadAll();
        void Update(Developer item);
    }
}