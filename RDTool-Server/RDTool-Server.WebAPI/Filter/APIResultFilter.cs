using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RDTool.Server.WebAPI.Dto;
using RDTool.Server.WebAPI.Model;
using System;

namespace RDTool.Server.WebAPI.Filter
{
    public class APIResultFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 执行方法体之后
        /// 返回结果为JsonResult的请求进行Result包装
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //过滤exception
            if (context.Exception != null)
            {
                return;
            }

            // 统一返回结构
            if (context.Result != null)
            {
                Type resultType = typeof(APIResponseModel<>);

                switch (context.Result)
                {
                    //TODO
                    //case APIResponseModel<>:
                    //    return;

                    case OkResult:
                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "" });
                        break;

                    case ObjectResult obj:
                        var result = context.Result as ObjectResult;

                        //忽略原先旧接口返回类型,后续可移除
                        if (result.Value is not null)
                        {
                            var returnType = result.Value.GetType();
                            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == resultType)
                            {
                                return;
                            }
                        }

                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "", Data = result.Value });
                        break;
                    case JsonResult jsonResult:
                        //忽略原先旧接口返回类型,后续可移除
                        if (jsonResult.Value is not null)
                        {
                            var returnType = jsonResult.Value.GetType();
                            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == resultType)
                            {
                                return;
                            }
                        }

                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "", Data = jsonResult.Value });
                        break;

                    case EmptyResult empry:
                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "" });
                        break;

                    //case ContentResult c:
                    //    Type contentType = typeof(APIResponseModel<>);
                    //    //忽略原先旧接口返回类型,后续可移除
                    //    if (c.Content.GetType().GetGenericTypeDefinition() == contentType)
                    //    {
                    //        return;

                    //    }
                    //    context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.SUCCESS.GetHashCode(), Message = "", Data = c.Content });
                    //    break;

                    default:
                        var defaultObj = context.Result as ObjectResult;

                        Type defaultType = typeof(APIResponseModel<>);
                        //忽略原先旧接口返回类型,后续可移除
                        if (defaultObj.Value is not null)
                        {
                            var defaultReturnType = defaultObj.Value.GetType();
                            if (defaultReturnType.IsGenericType && defaultReturnType.GetGenericTypeDefinition() == defaultType)
                            {
                                return;
                            }
                        }

                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "", Data = defaultObj.Value });
                        break;
                }
            }


            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 执行方法体之前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}