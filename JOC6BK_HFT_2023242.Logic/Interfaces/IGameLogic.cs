using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Models.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JOC6BK_HFT_2023242.Logic
{
    public interface IGameLogic
    {
        void Create(Game item);
        void Delete(int id);
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game item);
        IEnumerable<YearInfo> YearStatistics();
        IEnumerable<GameInfo> GetGamesByRelease(DateTime releaseDate);
        IEnumerable<GameInfo> GetGamesByPlayer(int playerId);
        IEnumerable<PlayerInfo> GetPlayersByGame(int gameId);
        IEnumerable<GameInfo> GetGamesByDeveloper(int developerId);
        IEnumerable<RoleInfo> GetRolesByPlayer(int playerId);
        IEnumerable<RoleInfo> GetRolesByGame(int gameId);
    }
}