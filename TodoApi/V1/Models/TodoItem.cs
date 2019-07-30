namespace TodoApi.V1.Models
{
    /// <summary>
    /// Todo Itemモデル
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Todo ID
        /// </summary>
        /// <value>TodoのID</value>
        public long Id { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 完了済みかどうか
        /// true;完了済み
        /// false:未完了
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
