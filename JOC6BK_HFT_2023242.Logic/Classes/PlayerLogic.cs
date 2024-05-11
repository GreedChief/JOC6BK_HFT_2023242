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
    public class PlayerLogic : IPlayerLogic
    {
        IRepository<Player> repo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }

        public void Create(Player item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Player Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Player> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Player item)
        {
            this.repo.Update(item);
        }
        public IEnumerable<PlayerInfo> GetPlayerById(int id)
        {
            return repo.ReadAll()
                .Where(game => game.PlayerId == id)
                .Select(game => new PlayerInfo
                {
                    PlayerId = game.PlayerId,
                    Name = game.PlayerName,
                });
        }
        public class PlayerInfo 
        { 
            public int PlayerId { get; set; }
            public string Name { get; set; }
        }

    }
}
