using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Testing.Models;


namespace Testing.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IGameRepository _repo;
        public LibraryController(IGameRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            // Fetch games with modified statuses
            var libraryGames = FetchLibraryGames();

            return View(libraryGames);
        }

        public IActionResult GamesBeaten()
        {
            var beatenGames = FetchLibraryGames().Where(game => game.status == "Beaten");
            return View("Index", beatenGames);
        }

        public IActionResult GamesPlayedButNotBeaten()
        {
            var playedNotBeatenGames = FetchLibraryGames().Where(game => game.status == "Played Not Beaten");
            return View("Index", playedNotBeatenGames);
        }

        public IActionResult GamesToPlay()
        {
            var toPlayGames = FetchLibraryGames().Where(game => game.status == "To Play");
            return View("Index", toPlayGames);
        }
        private IEnumerable<Game> FetchLibraryGames()
        {
            return _repo.GetAllGames().Where(game =>
                game.status == "Beaten" || game.status == "Played Not Beaten" || game.status == "To Play");
        }

    }
}
