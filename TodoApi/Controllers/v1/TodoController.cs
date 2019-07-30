using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers.V1
{
    /// <summary>
    /// Todoモデルにアクセスするためのコントローラー
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;

            if (this._context.TodoItems.Count() == 0)
            {
                this._context.TodoItems.Add(new TodoItem { Name = "Item1" });
                this._context.SaveChanges();
            }
        }

        /// <summary>
        /// Todo一覧を取得する
        /// </summary>
        /// <returns>Todo一覧を返す</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await this._context.TodoItems.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 指定されたidのTodoを取得する
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>todo</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await this._context.TodoItems.FindAsync(id).ConfigureAwait(false);

            if (todoItem == null)
            {
                return this.NotFound();
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
            if (item == null)
            {
                return this.BadRequest();
            }

            this._context.TodoItems.Add(item);
            await this._context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction(nameof(this.GetTodoItem), new { id = item.Id }, item);
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
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (id != item.Id)
            {
                return this.BadRequest();
            }

            this._context.Entry(item).State = EntityState.Modified;
            await this._context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        /// <summary>
        /// 指定されたidのtodoを削除する
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>削除結果</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await this._context.TodoItems.FindAsync(id).ConfigureAwait(false);

            if (todoItem == null)
            {
                return this.NotFound();
            }

            this._context.TodoItems.Remove(todoItem);
            await this._context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }
    }
}
