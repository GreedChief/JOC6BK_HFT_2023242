using System;
using System.Collections.Generic;
using System.Linq;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;
using Moq;
using NUnit.Framework;
using static JOC6BK_HFT_2023242.Logic.GameLogic;
using static JOC6BK_HFT_2023242.Logic.PlayerLogic;
using static JOC6BK_HFT_2023242.Logic.RoleLogic;

namespace JOC6BK_HFT_2023242.Test
{

    [TestFixture]
    public class GameLogicTester
    {
        GameLogic gameLogic;
        RoleLogic roleLogic;
        PlayerLogic playerLogic;
        Mock<IRepository<Game>> mockGameRepo;
        Mock<IRepository<Role>> mockRoleRepo;
        Mock<IRepository<Player>> mockPlayerRepo;

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
            gameLogic = new GameLogic(mockGameRepo.Object);

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
        }

        //NonCrudTests
        [Test]
        public void AvgRatePerYearTest()
        {
            double? avg = gameLogic.GetAverageRatePerYear(2009);
            Assert.That(avg, Is.EqualTo(6.5));
        }

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
        public void GetGamesByDeveloperTest()
        {
            int testDeveloperId = 1;
            var actualGames = gameLogic.GetGamesByDeveloper(testDeveloperId).ToList();
            var expectedGames = new List<GameDetail>()
            {
                new GameDetail() 
                { 
                  GameId = 1,
                  Title = "GameA",
                  Release = new DateTime(2008, 5, 2),
                  DeveloperId = 1
                },
                new GameDetail() 
                {
                    GameId = 2,
                    Title="GameB", 
                    Release= new DateTime(2009, 5, 2),
                    DeveloperId = 1
                }
            };

            Assert.AreEqual(expectedGames, actualGames);

        }

        [Test]
        public void GetGamesByReleaseTest()
        {
            // Arrange
            var releaseDate = new DateTime(2009, 5, 2);
            var expectedGames = new List<GameDetail>
            {
                new GameDetail() 
                { 
                    GameId = 2, 
                    Title = "GameB",
                    Release= new DateTime(2009, 5, 2),
                    DeveloperId = 1
                },
                new GameDetail() 
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

        [Test]
        public void GetPlayerByIdTest()
        {
            int testPlayerId = 1;
            var actualPlayer = playerLogic.GetPlayerById(testPlayerId).ToList();
            var expectedPlayer = new List<PlayerInfo>()
            {
                new PlayerInfo()
                {
                    PlayerId = 1,
                    Name = "PlayerA"                    
                },
            };

            Assert.AreEqual(expectedPlayer, actualPlayer);
        }

        [Test]
        public void GetMostPlayedRoleTest()
        {

            var expectedRole = new List<MostPlayedRoleInfo>
            {
                new MostPlayedRoleInfo()
                {
                    RoleName = "Support",
                    RoleCount = 2
                }
            };

            // Act
            var actualRole = roleLogic.GetMostPlayedRole().ToList();

            // Assert
            Assert.AreEqual(expectedRole, actualRole);

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
