namespace RDTool.Server.WebAPI.Dto
{
    public class APIResponseModel<T>
    {
        //
        // 摘要:
        //     响应码 0--成功 1--失败 401--未授权
        public int Code { get; set; }

        //
        // 摘要:
        //     返回信息
        public string Message { get; set; }

        //
        // 摘要:
        //     数据
        public T Data { get; set; }
    }
}
