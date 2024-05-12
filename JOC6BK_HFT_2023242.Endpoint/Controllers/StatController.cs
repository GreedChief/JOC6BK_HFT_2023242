using JOC6BK_HFT_2023242.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static JOC6BK_HFT_2023242.Logic.GameLogic;

namespace JOC6BK_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic logic;
        IPlayerLogic playerLogic;

        public StatController(IGameLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{year}")]
        public double? AverageRatePerYear(int year)
        {
            return this.logic.GetAverageRatePerYear(year);
        }

        [HttpGet]
        public IEnumerable<GameLogic.YearInfo> YearStatistics(int year)
        {
            return this.logic.YearStatistics();
        }

        [HttpGet("{developerId}")]
        public IEnumerable<GameLogic.GameDetail> GetGamesByDeveloper(int developerId) 
        { 
            return this.logic.GetGamesByDeveloper(developerId);
        }

        [HttpGet("{releaseDate}")]
        public IEnumerable<GameLogic.GameDetail> GetGamesByRelease(DateTime releaseDate) 
        { 
            return this.logic.GetGamesByRelease(releaseDate);
        }

        [HttpGet("{id}")]
        public IEnumerable<PlayerLogic.PlayerInfo> GetPlayerById(int id) 
        { 
            return this.playerLogic.GetPlayerById(id);
        }
    }
}
