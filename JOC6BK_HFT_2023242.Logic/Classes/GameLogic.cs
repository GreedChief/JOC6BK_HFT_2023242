using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Models.HelpClasses;
using JOC6BK_HFT_2023242.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JOC6BK_HFT_2023242.Logic
{
    public class GameLogic : IGameLogic
    {
        IRepository<Game> repo;
        IRepository<Developer> developerRepo;
        IRepository<Player> playerRepo;

        public GameLogic(IRepository<Game> repo, IRepository<Developer> developerRepo, IRepository<Player> playerRepo)
        {
            this.repo = repo;
            this.developerRepo = developerRepo;
            this.playerRepo = playerRepo;
        }

        public void Create(Game item)
        {
            if (item.Title.Length < 3)
            {
                throw new ArgumentException("The title is too short..");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Game Read(int id)
        {
            var game = this.repo.Read(id);
            if (game == null)
            {
                throw new ArgumentException("Game not exists..");
            }
            return game;
        }

        public IQueryable<Game> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Game item)
        {
            this.repo.Update(item);
        }

        //Non Cruds 
        public IEnumerable<YearInfo> YearStatistics()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Release.Year into g
                   select new YearInfo()
                   {
                       Year = g.Key,
                       AvgRating = g.Average(t => t.Rating),
                       GameNumber = g.Count()
                   };
        }

        public IEnumerable<GameInfo> GetGamesByRelease(DateTime releaseDate)
        {
            return this.repo.ReadAll()
                .Where(game => game.Release.Date == releaseDate.Date)
                .Select(game => new GameInfo
                {
                    GameId = game.GameId,
                    Title = game.Title,
                    Release = game.Release,
                    DeveloperId = game.DeveloperId
                });
        }
        //Több táblás non-crud lekérdezések
        public IEnumerable<GameInfo> GetGamesByPlayer(int playerId)
        {
            return (from player in playerRepo.ReadAll()
                    where player.PlayerId == playerId
                    from game in player.Games
                    select new GameInfo
                    {
                        GameId = game.GameId,
                        Title = game.Title,
                        Price = game.Price,
                        Rating = game.Rating,
                        Release = game.Release,
                        DeveloperId = game.DeveloperId
                    }).Distinct();
        }

        public IEnumerable<PlayerInfo> GetPlayersByGame(int gameId)
        {
            return (from game in repo.ReadAll()
                    where game.GameId == gameId
                    from player in game.Players
                    select new PlayerInfo
                    {
                        PlayerId = player.PlayerId,
                        PlayerName = player.PlayerName
                    });
        }

        public IEnumerable<GameInfo> GetGamesByDeveloper(int developerId)
        {
            return (from developer in developerRepo.ReadAll()
                    where developer.DeveloperId == developerId
                    from game in developer.Games
                    select new GameInfo
                    {
                        GameId = game.GameId,
                        Title = game.Title,
                        Price = game.Price,
                        Rating = game.Rating,
                        Release = game.Release,
                        DeveloperId = game.DeveloperId
                    }).Distinct();
        }

        public IEnumerable<RoleInfo> GetRolesByPlayer(int playerId)
        {
            return (from player in playerRepo.ReadAll()
                    where player.PlayerId == playerId
                    from role in player.Roles
                    select new RoleInfo
                    {
                        RoleId = role.RoleId,
                        Priority = role.Priority,
                        RoleName = role.RoleName
                    }).Distinct();
        }

        public IEnumerable<RoleInfo> GetRolesByGame(int gameId)
        {
            return (from game in repo.ReadAll()
                    where game.GameId == gameId
                    from role in game.Roles
                    select new RoleInfo
                    {
                        RoleId = role.RoleId,
                        Priority = role.Priority,
                        RoleName = role.RoleName
                    }).Distinct();
        }

    }

}
