using System;
using System.Linq;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;

namespace JOC6BK_HFT_2023242.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ctx = new GameDbContext();
            var repo = new GameRepository(ctx);
            var logic = new GameLogic(repo);
            Game g = new Game()
            {
                DeveloperId = 1,
                Title = "G1",

            };
            //logic.Create(g);
            var nc = logic.YearStatistics().ToArray();
            var items = logic.ReadAll();
            var asd = logic.GetGamesByDeveloper(1);
            var asd2 = logic.GetGamesByRelease(new DateTime(2018, 10, 26));
            var asd3 = logic.GetHighestRating();
        }
    }
}
