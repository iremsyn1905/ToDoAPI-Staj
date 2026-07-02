using Microsoft.AspNetCore.Mvc;
using ToDoAPI.DTOs;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static List<GameItem> _gamelist = new List<GameItem>
        {
            new GameItem { Id = 1, Name = "GTA V", Genre = "Aksiyon/Açık Dünya", Rating = 9.5, IsInstalled = true },
            new GameItem { Id = 2, Name = "Cyberpunk 2077", Genre = "Suç,Aksiyon", Rating = 8.8, IsInstalled = false }
        };
        [HttpGet]
      
        public IActionResult GetAll()
        {
            
            return Ok(_gamelist);
        }
        [HttpPost]
        /// <summary>
        /// Sistemde yeni bir oyun oluşturur.
        /// </summary>
        /// <remarks>
        /// Bu metodu tetiklemek için sağ üstteki kilit butonundan (Bearer Token) giriş yapılmış olması gerekir.
        /// </remarks>
       
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Create([FromBody] CreateGameDto newGameDto)
        
      
        {
            var game = new GameItem
            {
                Id = _gamelist.Count > 0 ? _gamelist.Max(g => g.Id) + 1 : 1,
                Name = newGameDto.Name,
                Genre = newGameDto.Genre,
                Rating = newGameDto.Rating,
                IsInstalled = newGameDto.IsInstalled
            };
            _gamelist.Add(game);
            return CreatedAtAction(nameof(GetAll), new { id = game.Id }, game);
        }
        [HttpPut]
        public IActionResult Update(int id, [FromBody] CreateGameDto updatedGameDto)
        {
            var game = _gamelist.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound("Güncellemek istenen oyun bulunamadı!");
            }
            game.Name = updatedGameDto.Name;
            game.Genre = updatedGameDto.Genre;
            game.Rating = updatedGameDto.Rating;
            game.IsInstalled = updatedGameDto.IsInstalled;
            return NoContent();

        }
        [HttpDelete("{id}")]
         public IActionResult Delete (int id)
        {
            var game = _gamelist.FirstOrDefault(g => g.Id == id);
            if(game == null)
            {
                return NotFound("Silinmek istenen oyun bulunamadı!");
            }
            _gamelist.Remove(game);
            return NoContent();
        }
   

    
    }
       
         
}

    

