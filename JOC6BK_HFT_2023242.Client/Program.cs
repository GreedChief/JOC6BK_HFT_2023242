using System;
using System.Linq;
using ConsoleTools;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;

namespace JOC6BK_HFT_2023242.Client
{
    internal class Program
    {
        static PlayerLogic playerLogic;
        static RoleLogic roleLogic;
        static DeveloperLogic developerLogic;
        static GameLogic gameLogic;

        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                var items = playerLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.PlayerId + "\t" + item.PlayerName);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            var ctx = new GameDbContext();

            var gameRepo = new GameRepository(ctx);
            var roleRepo = new RoleRepository(ctx);
            var playerRepo = new PlayerRepository(ctx);
            var developerRepo = new DeveloperRepository(ctx);

            gameLogic = new GameLogic(gameRepo);
            roleLogic = new RoleLogic(roleRepo);
            playerLogic = new PlayerLogic(playerRepo);
            developerLogic = new DeveloperLogic(developerRepo);
            
            
            var playerSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Player"))
               .Add("Create", () => Create("Player"))
               .Add("Delete", () => Delete("Player"))
               .Add("Update", () => Update("Player"))
               .Add("Exit", ConsoleMenu.Close);

            var roleSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Role"))
                .Add("Create", () => Create("Role"))
                .Add("Delete", () => Delete("Role"))
                .Add("Update", () => Update("Role"))
                .Add("Exit", ConsoleMenu.Close);

            var developerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Developer"))
                .Add("Create", () => Create("Developer"))
                .Add("Delete", () => Delete("Developer"))
                .Add("Update", () => Update("Developer"))
                .Add("Exit", ConsoleMenu.Close);

            var gameSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Game"))
                .Add("Create", () => Create("Game"))
                .Add("Delete", () => Delete("Game"))
                .Add("Update", () => Update("Game"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Games", () => gameSubMenu.Show())
                .Add("Players", () => playerSubMenu.Show())
                .Add("Roles", () => roleSubMenu.Show())
                .Add("Developers", () => developerSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
