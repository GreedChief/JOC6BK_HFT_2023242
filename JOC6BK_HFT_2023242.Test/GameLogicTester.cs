using System;
using System.Collections.Generic;
using System.Linq;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;
using Moq;
using NUnit.Framework;
using static JOC6BK_HFT_2023242.Logic.GameLogic;

namespace JOC6BK_HFT_2023242.Test
{

    [TestFixture]
    public class GameLogicTester
    {
        GameLogic gameLogic;
        RoleLogic roleLogic;
        Mock<IRepository<Game>> mockGameRepo;
        Mock<IRepository<Role>> mockRoleRepo;

        [SetUp]
        public void Init()
        {
            mockGameRepo = new Mock<IRepository<Game>>();
            mockGameRepo.Setup(g => g.ReadAll()).Returns(new List<Game>()
            {
                new Game("1#GameA#100#1#2008*05*02#5"),
                new Game("2#GameB#200#1#2009*05*02#6"),
                new Game("3#GameC#300#1#2009*05*02#7"),
                new Game("4#GameD#400#1#2010*05*02#8")
            }.AsQueryable());
            gameLogic = new GameLogic(mockGameRepo.Object);

            mockRoleRepo = new Mock<IRepository<Role>>();
            mockRoleRepo.Setup(g => g.ReadAll()).Returns(new List<Role>()
            {
                new Role("260#18#219#2#Support"),
                new Role("270#18#220#2#Damage Dealer"),
                new Role("280#19#230#3#Tank"),
                new Role ("290#20#240#3#Support")
            }.AsQueryable());
            roleLogic = new RoleLogic(mockRoleRepo.Object);
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
