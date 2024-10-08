﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Xml;
using ConsoleTools;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Models.HelpClasses;

namespace JOC6BK_HFT_2023242.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player Name: ");
                string name = Console.ReadLine();
                rest.Post(new Player() { PlayerName = name }, "player");
            }

            if (entity == "Role")
            {
                Console.Write("Enter Role Name: ");
                string roleName = Console.ReadLine();
                rest.Post(new Role() { RoleName = roleName }, "role");
            }

            if (entity == "Developer")
            {
                Console.Write("Enter Developer Name: ");
                string developerName = Console.ReadLine();
                rest.Post(new Developer() { DeveloperName = developerName }, "developer");
            }

            if (entity == "Game")
            {
                Console.Write("Enter Game's Name: ");
                string gameName = Console.ReadLine();
                rest.Post(new Game() { Title = gameName }, "game");
            }
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                List<Player> players = rest.Get<Player>("player");
                foreach (var item in players)
                {
                    Console.WriteLine(item.PlayerId + ": " + item.PlayerName);
                }
            }

            if (entity == "Role")
            {
                List<Role> roles = rest.Get<Role>("role");
                foreach (var item in roles)
                {
                    Console.WriteLine(item.RoleId + ": " + item.RoleName + " Priority: " + item.Priority + " GameId: " + item.GameId + " PlayerId: " + item.PlayerId);
                }
            }

            if (entity == "Developer")
            {
                List<Developer> developers = rest.Get<Developer>("developer");
                foreach (var item in developers)
                {
                    Console.WriteLine(item.DeveloperId + ": " + item.DeveloperName);
                }
            }

            if (entity == "Game")
            {
                List<Game> games = rest.Get<Game>("game");
                foreach (var item in games)
                {
                    Console.WriteLine(item.GameId + ": " + item.Title + " Release Date:" + item.Release.ToShortDateString() + " DeveloperId: " + item.DeveloperId);
                }
            }


            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Player one = rest.Get<Player>(id, "player");
                Console.Write($"New player's name [old: {one.PlayerName}]: ");
                string playerName = Console.ReadLine();
                one.PlayerName = playerName;
                rest.Put(one, "player");
            }

            if (entity == "Role")
            {
                Console.Write("Enter Role's id to update: ");
                int roleId = int.Parse(Console.ReadLine());
                Role two = rest.Get<Role>(roleId, "role");
                Console.Write($"New role's name [old: {two.RoleName}]: ");
                string roleName = Console.ReadLine();
                two.RoleName = roleName;
                rest.Put(two, "role");
            }

            if (entity == "Developer")
            {
                Console.Write("Enter Developer's id to update: ");
                int devId = int.Parse(Console.ReadLine());
                Developer three = rest.Get<Developer>(devId, "developer");
                Console.Write($"New developer's name [old: {three.DeveloperName}]: ");
                string developerName = Console.ReadLine();
                three.DeveloperName = developerName;
                rest.Put(three, "developer");
            }

            if (entity == "Game")
            {
                Console.Write("Enter Game's id to update: ");
                int gameId = int.Parse(Console.ReadLine());
                Game four = rest.Get<Game>(gameId, "game");
                Console.Write($"New game's name [old: {four.Title}]: ");
                string gameName = Console.ReadLine();
                four.Title = gameName;
                rest.Put(four, "game");
            }

        }
        static void Delete(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to delete: ");
                int id1 = int.Parse(Console.ReadLine());
                rest.Delete(id1, "player");
            }

            if (entity == "Role")
            {
                Console.Write("Enter Role's id to delete: ");
                int id2 = int.Parse(Console.ReadLine());
                rest.Delete(id2, "role");
            }

            if (entity == "Developer")
            {
                Console.Write("Enter Developer's id to delete: ");
                int id3 = int.Parse(Console.ReadLine());
                rest.Delete(id3, "developer");
            }

            if (entity == "Game")
            {
                Console.Write("Enter Game's id to delete: ");
                int id4 = int.Parse(Console.ReadLine());
                rest.Delete(id4, "game");
            }
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:28357/", "game");


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

            var noncrudsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("GetGamesByDeveloper", () => GetGamesByDeveloper("GetGamesByDeveloper"))
                .Add("GetPlayersByGame", () => GetPlayersByGame("GetPlayersByGame"))
                .Add("GetRolesByGame", () => GetRolesByGame("GetRolesByGame"))
                .Add("GetGamesByPlayer", () => GetGamesByPlayer("GetGamesByPlayer"))
                .Add("GetRolesByPlayer", () => GetRolesByPlayer("GetRolesByPlayer"))
                .Add("YearStatistics", () => YearStatistics("YearStatistics"))
                .Add("GetGamesByRelease", () => GetGamesByRelease("GetGamesByRelease"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Games", () => gameSubMenu.Show())
                .Add("Players", () => playerSubMenu.Show())
                .Add("Roles", () => roleSubMenu.Show())
                .Add("Developers", () => developerSubMenu.Show())
                .Add("NonCruds", () => noncrudsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        //tobb tablas noncrud
        private static void GetRolesByPlayer(string endpoint)
        {
            Console.Write("Enter the searched player's ID: ");
            int playerId = int.Parse(Console.ReadLine());
            var result = rest.Get<RoleInfo>($"Stat/{endpoint}/{playerId}");

            foreach (var item in result)
            {
                Console.WriteLine($"RoleId: {item.RoleId} | RoleName: {item.RoleName}");
            }
            Console.ReadLine();
        }

        private static void GetGamesByPlayer(string endpoint)
        {
            Console.Write("Enter the searched player's ID: ");
            int playerId = int.Parse(Console.ReadLine());
            var result = rest.Get<GameInfo>($"Stat/{endpoint}/{playerId}");

            foreach (var item in result)
            {
                Console.WriteLine($"GameId: {item.GameId} |  Title: {item.Title} | Release: {item.Release} | DeveloperId: {item.DeveloperId}");
            }
            Console.ReadLine();
        }

        private static void GetRolesByGame(string endpoint)
        {
            Console.Write("Enter the searched Game's ID: ");
            int gameId = int.Parse(Console.ReadLine());
            var result = rest.Get<RoleInfo>($"Stat/{endpoint}/{gameId}");

            foreach (var item in result)
            {
                Console.WriteLine($"RoleId: {item.RoleId} | RoleName: {item.RoleName}");
            }
            Console.ReadLine();
        }

        private static void GetPlayersByGame(string endpoint)
        {
            Console.Write("Enter the searched Game's ID: ");
            int gameId = int.Parse(Console.ReadLine());
            var result = rest.Get<PlayerInfo>($"Stat/{endpoint}/{gameId}");

            foreach (var item in result)
            {
                Console.WriteLine($"PlayerId: {item.PlayerId} | PlayerName: {item.PlayerName}");
            }
            Console.ReadLine();
        }

        
        private static void GetGamesByDeveloper(string endpoint)
        {
            Console.Write("Enter the searched developer's ID: ");
            int developerId = int.Parse(Console.ReadLine());
            var result = rest.Get<GameInfo>($"Stat/{endpoint}/{developerId}");

            foreach (var item in result)
            {
                Console.WriteLine($"GameId: {item.GameId} |  Title: {item.Title} | Release: {item.Release} | DeveloperId: {item.DeveloperId}");
            }
            Console.ReadLine();
        }
        //noncrud
        private static void GetGamesByRelease(string endpoint) 
        {
            Console.Write("Enter the release date(yyyy.MM.dd): ");
            DateTime release = DateTime.Parse(Console.ReadLine());
            var result = rest.Get<GameInfo>($"Stat/{endpoint}/{release}");
            foreach (var item in result)
            {
                Console.WriteLine($"GameId: {item.GameId} |  Title: {item.Title} | Release: {item.Release} | DeveloperId: {item.DeveloperId}");
            }        
            Console.ReadLine();
        }

        private static void YearStatistics(string endpoint)
        {
            var result = rest.Get<YearInfo>($"Stat/{endpoint}");

            foreach (var item in result)
            {
                Console.WriteLine($"Year: {item.Year} | AvgRating: {Math.Round((decimal)item.AvgRating, 2)} | GameNumber: {item.GameNumber}");
            }
            Console.ReadLine();
        }
    }
}
