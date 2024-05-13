using System;
using System.Collections.Generic;
using System.Linq;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Models.HelpClasses;
using JOC6BK_HFT_2023242.Repository;
using Moq;
using NUnit.Framework;

namespace JOC6BK_HFT_2023242.Test
{

    [TestFixture]
    public class GameLogicTester
    {
        GameLogic gameLogic;
        RoleLogic roleLogic;
        PlayerLogic playerLogic;
        DeveloperLogic developerLogic;
        Mock<IRepository<Game>> mockGameRepo;
        Mock<IRepository<Role>> mockRoleRepo;
        Mock<IRepository<Player>> mockPlayerRepo;
        Mock<IRepository<Developer>> mockDeveloperRepo;

        [SetUp]
        public void Init()
        {
            mockGameRepo = new Mock<IRepository<Game>>();
            mockGameRepo.Setup(g => g.ReadAll()).Returns(new List<Game>()
            {
                 new Game {GameId = 1,Title="GameA", Price=200, DeveloperId=1, Release= new DateTime(2008, 5, 2), Rating=5 },
                 new Game {GameId = 2,Title="GameB", Price=300, DeveloperId=1, Release= new DateTime(2009, 5, 2), Rating=6 },
                 new Game {GameId = 3,Title="GameC", Price=400, DeveloperId=2, Release= new DateTime(2009, 5, 2), Rating=7 },
                 new Game {GameId = 4,Title="GameD", Price=500, DeveloperId=3, Release= new DateTime(2010, 5, 2), Rating=8 },
            }.AsQueryable());
            gameLogic = new GameLogic(mockGameRepo.Object, mockDeveloperRepo.Object, mockPlayerRepo.Object);

            mockRoleRepo = new Mock<IRepository<Role>>();
            mockRoleRepo.Setup(g => g.ReadAll()).Returns(new List<Role>()
            {
                new Role { RoleId = 1, Priority=5, RoleName= "Support", GameId= 1, PlayerId=1 },
                new Role { RoleId = 2, Priority=6, RoleName= "Damage Dealer", GameId= 2, PlayerId=2 },
                new Role { RoleId = 3, Priority=6, RoleName= "Tank", GameId= 3, PlayerId=3 },
                new Role { RoleId = 4, Priority=3, RoleName= "Support", GameId= 4, PlayerId=4 },
            }.AsQueryable());
            roleLogic = new RoleLogic(mockRoleRepo.Object);

            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockPlayerRepo.Setup(g => g.ReadAll()).Returns(new List<Player>()
            {
                 new Player {PlayerId = 1, PlayerName= "PlayerA" },
                 new Player {PlayerId = 2, PlayerName= "PlayerB"  },
                 new Player {PlayerId = 3, PlayerName= "PlayerC"  },
                 new Player {PlayerId = 4, PlayerName= "PlayerD"  },
            }.AsQueryable());
            playerLogic = new PlayerLogic(mockPlayerRepo.Object);

            mockDeveloperRepo = new Mock<IRepository<Developer>>();
            mockDeveloperRepo.Setup(g => g.ReadAll()).Returns(new List<Developer>()
            {
                 new Developer {DeveloperId = 1, DeveloperName= "DeveloperA" },
                 new Developer {DeveloperId = 2, DeveloperName= "DeveloperB"  },
                 new Developer {DeveloperId = 3, DeveloperName= "DeveloperC"  },
                 new Developer {DeveloperId = 4, DeveloperName= "DeveloperD"  },
            }.AsQueryable());
            developerLogic = new DeveloperLogic(mockDeveloperRepo.Object);
        }

        //NonCrudTests

        [Test]
        public void YearStatisticsTest()
        {
            var actual = gameLogic.YearStatistics().ToList();
            var expected = new List<YearInfo>()
            {
                new YearInfo()
                {
                    Year = 2008,
                    AvgRating = 5,
                    GameNumber = 1
                },
                new YearInfo()
                {
                    Year = 2009,
                    AvgRating = 6.5,
                    GameNumber= 2
                },
                new YearInfo()
                {
                    Year = 2010,
                    AvgRating= 8,
                    GameNumber= 1
                }
            };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetGamesByReleaseTest()
        {
            // Arrange
            var releaseDate = new DateTime(2009, 5, 2);
            var expectedGames = new List<GameInfo>
            {
                new GameInfo() 
                { 
                    GameId = 2, 
                    Title = "GameB",
                    Release= new DateTime(2009, 5, 2),
                    DeveloperId = 1
                },
                new GameInfo() 
                { 
                    GameId = 3, 
                    Title = "GameC",
                    Release= new DateTime(2009, 5, 2),
                    DeveloperId = 2 
                }
            };

            // Act
            var actualGames = gameLogic.GetGamesByRelease(releaseDate).ToList();
            // Assert
            Assert.AreEqual(expectedGames, actualGames);
        }

      


        //CreateTests
        [Test]
        public void CreateGameTestWithCorrectTitle()
        {
            var game = new Game() { Title = "ABCD" };
            //ACT
            gameLogic.Create(game);
            //ASSERT
            mockGameRepo.Verify(r => r.Create(game), Times.Once);
        }

        [Test]
        public void CreateGameTestWithInCorrectTitle()
        {
            var game = new Game() { Title = "24" };
            try
            {

                //ACT
                gameLogic.Create(game);
            }
            catch
            {

            }

            //ASSERT
            mockGameRepo.Verify(r => r.Create(game), Times.Never);
        }

        [Test]
        public void TestCreateNewRole()
        {
            var newRole = new Role { RoleName = "Rouge" };
            // Act
            roleLogic.Create(newRole);

            // Assert
            mockRoleRepo.Verify(r => r.Create(newRole), Times.Once);
        }

        [Test]
        public void TestCreateExistingRole()
        {
            var role = new Role() { RoleName = "Support" };
            try
            {

                //ACT
                roleLogic.Create(role);
            }
            catch
            {

            }

            //ASSERT
            mockRoleRepo.Verify(r => r.Create(role), Times.Never);
        }
    }
}
