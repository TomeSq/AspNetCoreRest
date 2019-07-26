namespace TodoApi.Models
{
    /// <summary>
    /// Todo Itemモデル
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// todo名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// true;完了済み
        /// false:未完了
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
