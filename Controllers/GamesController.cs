using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class GamesController : ControllerBase
        {

            [HttpGet("getUserGameList/id/{id}")]
            public List<Game> Get(int id)
            {
                Game game = new Game();
                return game.Read(id);
            }

            [HttpGet("getUsersWishList/id/{id}")]
            public List<Game> GetWishList(int id)
            {
                Game game = new Game();
                return game.ReadWishList(id);
            }

            // GET api/<GamesController>/5
            [HttpGet("searchByPrice")]
            public List<Game> GetByPrice(double price, int id)
            {
                 Game game = new Game();
                 return game.GetGameByPrice(price,id);
            }

            [HttpGet("searchByRankScore/scoreRank/{scoreRank}/id/{id}")]
            public List<Game> GetByRankScore(int scoreRank, int id)
            {
                 Game game = new Game();
                 return game.GetGamesByRankScore(scoreRank, id);
            }

            // POST api/<GamesController>
            [HttpPost("addToFavorites/id/{id}/appID/{appID}")]
            public int Post(int id, int appID)
            {
                 Game game = new Game();
                 return game.InsertToFavorite(id,appID);
            }

            [HttpDelete("DeleteByID/id/{id}/appID/{appID}")]
            public int DeleteByID(int id, int appID)
            {
                 Game game = new Game();
                 return game.DeleteFromGamesList(id, appID);
            }
        }
}
    
