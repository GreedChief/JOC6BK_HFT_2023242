﻿using JOC6BK_HFT_2023242.Logic;
using JOC6BK_HFT_2023242.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace JOC6BK_HFT_2023242.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleLogic logic;

        public RoleController(IRoleLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Role> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Role Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Role value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Role value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
