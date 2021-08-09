﻿using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {

        private readonly ILeaderBoardDAO leaderBoardDAO;
        private int tempEventId;

        public LeaderBoardController(ILeaderBoardDAO _leaderBoardDAO)
        {
            leaderBoardDAO = _leaderBoardDAO;
        }
    
        [HttpGet("{eventId}")]
        public ActionResult<List<EventLeaderBoard>> GetLeader(int eventId)
        {
            tempEventId = eventId;

            List<EventLeaderBoard> events = new List<EventLeaderBoard>();
            try
            {
                events = leaderBoardDAO.GetLeader(eventId);
                return Ok(events);
            }
            catch (SqlException ex)
            {
                return NotFound("Unable to communicate with database. Reporting this error: " + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<EventLeaderBoard>> LeaderComparison(int eventId)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);

            List<EventLeaderBoard> events = new List<EventLeaderBoard>();
            try
            {
                events = leaderBoardDAO.LeaderComparison(eventId, userId);
                return Ok(events);
            }
            catch (SqlException ex)
            {
                return NotFound("Unable to communicate with database. Reporting this error: " + ex.Message);
            }
        }


    }
}

