using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;

namespace JOC6BK_HFT_2023242.Logic
{
    public class GameLogic 
    {
        IRepository<Game> repo;

        public GameLogic(IRepository<Game> repo)
        {
            this.repo = repo;
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
            if (game == null) {
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

        //Non Cruds (min5db)
        public double? GetAverageRatePerMonth(int year) {
            return this.repo.ReadAll().Where(t => t.Release.Year == year)
                .Average(t => t.Rating);
        }
        public IEnumerable<YearInfo> YearStatistics()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Release.Year into g
                   select new YearInfo()
                   {
                       Year = g.Key,
                       AvgRating = g.Average(t => t.Rating),
                       GameNumber = g.Count(),
                       RoleNumber = g.Sum(t => t.Roles.Count)
                   };
        }
        public class YearInfo 
        { 
            public int Year { get; set; }   
            public double? AvgRating { get; set;}
            public int GameNumber { get; set; }
            public int RoleNumber { get; set; }
        }
    }
    
}
