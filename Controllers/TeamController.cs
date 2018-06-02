using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlayersAndTeams.Models;
using PlayersAndTeams.Factories;

namespace PlayersAndTeams.Controllers
{
    public class TeamsController : Controller
    {
        private readonly TeamFactory teamFactory;
        public TeamsController(TeamFactory teamConnect)
        {
            teamFactory = teamConnect;
        }

        [HttpGet]
        [Route("/Teams")]
        public IActionResult AllTeams()
        {
            ViewBag.Teams = teamFactory.AllTeams();
            return View("Teams");
        }

        [HttpPost]
        [Route("/AddTeam")]
        public IActionResult AddTeam(Team team)
        {
            if(ModelState.IsValid)
            {
                teamFactory.AddTeam(team);
                ViewBag.Teams = teamFactory.AllTeams();
                return View("Teams");
            }
            ViewBag.AllTeams = teamFactory.AllTeams();
            return View("Teams");
        }

        [HttpPost]
        [Route("/Team/{teamId")]
        public IActionResult TeamInfo(int teamId)
        {
            ViewBag.Team = teamFactory.FindTeamByID(teamId);
            return View("Team");
        }
    }
}