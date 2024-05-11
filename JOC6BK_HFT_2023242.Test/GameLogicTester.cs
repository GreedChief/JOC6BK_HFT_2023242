using System;
using System.Collections.Generic;
using System.Linq;
using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Repository;
using NUnit.Framework;
using static JOC6BK_HFT_2023242.Logic.GameLogic;

namespace JOC6BK_HFT_2023242.Test
{
    public class FakeGameRepository : IRepository<Game>
    {
        public void Create(Game item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Game Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Game> ReadAll()
        {
            return new List<Game>()
            {
                new Game("1#GameA#100#1#2008*05*02#5"),
                new Game("2#GameB#200#1#2009*05*02#6"),
                new Game("3#GameC#300#1#2009*05*02#7"),
                new Game("4#GameD#400#1#2010*05*02#8"),
            }.AsQueryable();
        }

        public void Update(Game item)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class GameLogicTester
    {
        GameLogic logic;
        
        [SetUp]
        public void Init()
        {
            logic = new GameLogic(new FakeGameRepository());
        }

        [Test]
        public void AvgRatePerYearTest() 
        {
            double? avg = logic.GetAverageRatePerYear(2009);
            Assert.That(avg, Is.EqualTo(6.5));
        }

        [Test]
        public void YearStatisticsTest() 
        { 
            var actual = logic.YearStatistics().ToList();
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
    }
}
