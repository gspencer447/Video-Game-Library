using System.Collections;
using System.Collections.Generic;
using Testing.Models;

namespace Testing
{
    public interface IGameRepository
    {
        public IEnumerable<Game> GetAllGames();
        public Game GetGame(int id);
        public void UpdateGame(Game game);
        public void InsertGame(Game gameToInsert);
    }
}
