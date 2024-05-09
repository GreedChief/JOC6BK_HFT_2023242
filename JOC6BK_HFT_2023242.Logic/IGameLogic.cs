using JOC6BK_HFT_2023242.Models;
using System.Collections.Generic;
using System.Linq;

namespace JOC6BK_HFT_2023242.Logic
{
    public interface IGameLogic
    {
        void Create(Game item);
        void Delete(int id);
        double? GetAverageRatePerMonth(int year);
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game item);
        IEnumerable<GameLogic.YearInfo> YearStatistics();
    }
}