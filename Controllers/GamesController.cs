using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class GamesController : ControllerBase
        {
        // GET: api/<GamesController>
        //[HttpGet]
        //public IEnumerable<Game> Get()
        //{
        //    Game game = new Game();
        //    return game.Read();
        //}
        [HttpGet("getUserGameList/id/{id}")]
        public List<Game> Get(int id)
        {
            Game game = new Game();
            return game.Read(id);
        }





        // GET api/<GamesController>/5
        [HttpGet("searchByPrice")]
            public IEnumerable<Game> GetByPrice(double price)
            {
                Game game = new Game();
                return game.GetGameByPrice(price);
            }

            [HttpGet("searchByRankScore/scoreRank/{scoreRank}")]
            public IEnumerable<Game> GetByRankScore(int scoreRank)
            {
                Game game = new Game();
                return game.GetGamesByRankScore(scoreRank);

            }

            // POST api/<GamesController>
            [HttpPost("addToFavorites/id/{id}/appID/{appID}")]
            public int Post(int id, int appID)
            {
                Game game = new Game();
                return game.InsertToFavorite(id,appID);
            }

            // PUT api/<GamesController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            
            }


            [HttpDelete("DeleteByID")]
            public IActionResult DeleteByID(int id)
            {
                try
                {
                    Game gameToRemove = Game.gamesList.FirstOrDefault(g => g.AppID == id);
                    if (gameToRemove == null)
                    {
                        return NotFound(new { message = $"Game with ID {id} not found." });
                    }

                    Game.gamesList.Remove(gameToRemove);
                    return Ok(new { message = $"Game with ID {id} was successfully deleted." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
                }
            }

            //[HttpPost("InitPostForAllGames")]
            //public bool Post([FromBody] List<Game> games)
            //{
            //    return Game.InsertAllGamesOnce(games);
            //}
        }
        }
    
