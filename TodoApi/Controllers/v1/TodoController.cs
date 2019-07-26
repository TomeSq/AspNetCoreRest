using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers.v1
{
    /// <summary>
    /// Todoモデルにアクセスするためのコントローラー
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if(_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Todo一覧を取得する
        /// </summary>
        /// <returns>Todo一覧を返す</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// 指定されたidのTodoを取得する
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>todo</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if(todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        /// <summary>
        /// todoを更新する
        /// </summary>
        /// <param name="item">todo item</param>
        /// <returns>更新結果</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        /// <summary>
        /// 指定されたidのtodoを更新する
        /// </summary>
        /// <param name="id">todo id</param>
        /// <param name="item">更新内容</param>
        /// <returns>削除結果</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// 指定されたidのtodoを削除する
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>削除結果</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
