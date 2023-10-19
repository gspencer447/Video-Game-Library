using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Testing.Models;

namespace Testing.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository repo;

        public GameController(IGameRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string sort)
        {
            IEnumerable<Game> games;

            switch (sort)
            {
                case "beaten":
                    games = repo.GetAllGames().Where(game => game.status == "Beaten");
                    break;
                case "played":
                    games = repo.GetAllGames().Where(game => game.status == "Played Not Beaten");
                    break;
                case "to play":
                    games = repo.GetAllGames().Where(game => game.status == "To Play");
                    break;
                default:
                    games = repo.GetAllGames();
                    break;
            }
            return View(games);
        }

        public IActionResult ViewGame(int id)
        {
            var game = repo.GetGame(id);

            return View(game);
        }

        public IActionResult UpdateGame(int id)
        {
            Game game = repo.GetGame(id);
            if (game == null)
            {
                return View("GameNotFound");
            }
            return View(game);
        }

        public IActionResult UpdateGameToDatabase(Game game)
        {
            if (string.IsNullOrWhiteSpace(game.status))
            {
                game.status = "Not Played";
            }

            repo.UpdateGame(game);
            return RedirectToAction("ViewGame", new { id = game.gameID });
        }

        public IActionResult InsertGame()
        {
            var game = new Game();
            return View(game);
        }

        public IActionResult InsertGameToDatabase(Game gameToInsert)
        {
            if (string.IsNullOrWhiteSpace(gameToInsert.status))
            {
                gameToInsert.status = "Not Played";
            }

            repo.InsertGame(gameToInsert);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteGame(Game game) 
        {
            repo.DeleteGame(game);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Game/UpdateGameStatus/{gameID}")]
        public IActionResult UpdateGameStatus(int gameID, [FromBody] string status)
        {
            repo.UpdateGameStatus(gameID, status);

            return Ok(new { message = "Game status updated successfully" });
        }

    }
}
