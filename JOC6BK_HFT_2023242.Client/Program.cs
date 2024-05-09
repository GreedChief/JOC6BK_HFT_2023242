using System;
using System.Linq;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;

namespace JOC6BK_HFT_2023242.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IRepository<Player> repo = new PlayerRepository(new GameDbContext());
            Player a = new Player() { PlayerName = "Bela" };
            repo.Create(a);
            var another = repo.Read(1);
            another.PlayerName = "Sanyi";
            repo.Update(another);

            var items = repo.ReadAll().ToArray();
        }
    }
}
