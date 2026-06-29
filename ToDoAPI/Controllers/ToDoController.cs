using Microsoft.AspNetCore.Mvc;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        // Şimdilik veritabanı yerine hafızada statik bir liste tutuyoruz
        private static List<ToDoItem> _todoList = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Title = "Stajın 1. gününü tamamla", IsCompleted = true },
            new ToDoItem { Id = 2, Title = "Yazdığın kodu GitHub'a gönder", IsCompleted = false }
        };

        // GET: api/todo (Tüm listeyi getirir)
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_todoList);
        }

        // POST: api/todo (Yeni görev ekler)
        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem item)
        {
            item.Id = _todoList.Count > 0 ? _todoList.Max(t => t.Id) + 1 : 1;
            _todoList.Add(item);
            return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
        }

        // PUT: api/todo/{id} (Görevi günceller/tamamlandı yapar)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ToDoItem updatedItem)
        {
            var todo = _todoList.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound("Görev bulunamadı!");

            todo.Title = updatedItem.Title;
            todo.IsCompleted = updatedItem.IsCompleted;

            return Ok(todo);
        }

        // DELETE: api/todo/{id} (Görevi siler)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _todoList.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound("Görev zaten yok!");

            _todoList.Remove(todo);
            return Ok("Görev başarıyla silindi.");
        }
    }
}