using Dapper;
using System.Collections.Generic;
using System.Data;
using Testing.Models;

namespace Testing
{
    public class GameRepository : IGameRepository
    {
        private readonly IDbConnection _conn;

        public GameRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _conn.Query<Game>("SELECT * FROM games");
        }

        public Game GetGame(int id)
        {
            return _conn.QuerySingle<Game>("SELECT * FROM games WHERE gameID = @id",
                new { id = id });
        }

        public void UpdateGame(Game game)
        {
            _conn.Execute("UPDATE games SET Name = @name, Genre = @genre WHERE gameID = @id",
             new { name = game.name, genre = game.genre, id = game.gameID });
        }
    }
}
