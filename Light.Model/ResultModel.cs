namespace Light.Model
{
    /// <summary>
    /// 返回结果实体
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
