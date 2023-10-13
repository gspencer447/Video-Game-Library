using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            var games = repo.GetAllGames();
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
            repo.InsertGame(gameToInsert);
            return RedirectToAction("Index");
        }
    }
}
