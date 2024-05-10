using JOC6BK_HFT_2023242.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JOC6BK_HFT_2023242.Logic
{
    public interface IGameLogic
    {
        void Create(Game item);
        void Delete(int id);
        double? GetAverageRatePerYear(int year);
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game item);
        IEnumerable<GameLogic.YearInfo> YearStatistics();
        IEnumerable<GameLogic.GameDetail> GetGamesByDeveloper(int developerId);
        IEnumerable<GameLogic.GameDetail> GetGamesByRelease(DateTime releaseDate);
        double? GetHighestRating();
    }
}