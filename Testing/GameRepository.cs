using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public void InsertGame(Game gameToInsert)
        {
            _conn.Execute("INSERT INTO games (name, genre, release_date, status, gameID) VALUES (@name, @genre, @release_date, @status, @gameID);",
                new { name = gameToInsert.name, genre = gameToInsert.genre, release_date = gameToInsert.release_date, status = gameToInsert.status, gameID = gameToInsert.gameID,  });
        }

        public void UpdateGame(Game game)
        {
            _conn.Execute("UPDATE games SET Name = @name, Genre = @genre, Release_Date = @release_date, Status = @status WHERE gameID = @id",
             new { name = game.name, genre = game.genre, release_date = game.release_date, status = game.status, id = game.gameID  });
        }

        public void DeleteGame(Game game) 
        {
            _conn.Execute("DELETE FROM games WHERE gameID = @id;",
                new { id = game.gameID });
        }

        public void UpdateGameStatus(int gameId, string status)
        {
            var query = "UPDATE games SET status = @status WHERE gameID = @gameID";
            _conn.Execute(query, new { status = status, gameID = gameId });
        }
    }
}
