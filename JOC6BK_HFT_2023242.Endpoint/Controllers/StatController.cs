using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using JOC6BK_HFT_2023242.Models.HelpClasses;
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

        public StatController(IGameLogic logic)
        {
            this.logic = logic;
        }
        //non cruds
        [HttpGet]
        public IEnumerable<YearInfo> YearStatistics(int year)
        {
            return this.logic.YearStatistics();
        }

        [HttpGet("{releaseDate}")]
        public IEnumerable<GameInfo> GetGamesByRelease(DateTime releaseDate)
        {
            return this.logic.GetGamesByRelease(releaseDate);
        }

        //több táblásak
        [HttpGet("{playerId}")]
        public IEnumerable<GameInfo> GetGamesByPlayer(int playerId)
        { 
            return this.logic.GetGamesByPlayer(playerId);
        }

        [HttpGet("{gameId}")]
        public IEnumerable<PlayerInfo> GetPlayersByGame(int gameId)
        { 
            return this.logic.GetPlayersByGame(gameId);
        }

        [HttpGet("{developerId}")]
        public IEnumerable<GameInfo> GetGamesByDeveloper(int developerId)
        { 
            return this.logic.GetGamesByDeveloper(developerId);
        }

        [HttpGet("{playerId}")]
        public IEnumerable<RoleInfo> GetRolesByPlayer(int playerId) 
        {
            return this.logic.GetRolesByPlayer(playerId);
        }

        [HttpGet("{gameId}")]
        public IEnumerable<RoleInfo> GetRolesByGame(int gameId) 
        {
            return this.logic.GetRolesByGame(gameId);
        }
    }
}
