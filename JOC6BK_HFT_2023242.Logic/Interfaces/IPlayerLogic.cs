using JOC6BK_HFT_2023242.Models;
using System.Collections.Generic;
using System.Linq;

namespace JOC6BK_HFT_2023242.Logic
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        void Update(Player item);
        IEnumerable<PlayerLogic.PlayerInfo> GetPlayerById(int id);
    }
}