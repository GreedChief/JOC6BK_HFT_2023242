using JOC6BK_HFT_2023242.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace JOC6BK_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic logic;
        IPlayerLogic playerLogic;
        IRoleLogic roleLogic;

        public StatController(IGameLogic logic, IPlayerLogic playerLogic, IRoleLogic roleLogic)
        {
            this.logic = logic;
            this.playerLogic = playerLogic;
            this.roleLogic = roleLogic;
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

        [HttpGet]
        IEnumerable<RoleLogic.MostPlayedRoleInfo> GetMostPlayedRole() 
        { 
            return this.roleLogic.GetMostPlayedRole();
        }

    }
}
