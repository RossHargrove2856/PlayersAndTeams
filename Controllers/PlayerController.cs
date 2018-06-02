using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlayersAndTeams.Factories;
using PlayersAndTeams.Models;

namespace PlayersAndTeams.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayerFactory playerFactory;
        public PlayersController(PlayerFactory playerConnect)
        {
            playerFactory = playerConnect;
        }

        [HttpGet]
        [Route("/Players")]
        public IActionResult AllPlayers()
        {
            ViewBag.Players = playerFactory.AllPlayers();
            return View("Players");
        }

        [HttpPost]
        [Route("/AddPlayer")]
        public IActionResult AddPlayer(Player player)
        {
            if(ModelState.IsValid)
            {
                playerFactory.AddPlayer(player);
                ViewBag.Players = playerFactory.AllPlayers();
                return View("Players");
            }
            ViewBag.Players = playerFactory.AllPlayers();
            return View("Players");
        }

        [HttpPost]
        [Route("/Player/{playerId}")]
        public IActionResult PlayerInfo(int playerId)
        {
            ViewBag.Player = playerFactory.FindPlayerByID(playerId);
            return View("");
        }
    }
}
