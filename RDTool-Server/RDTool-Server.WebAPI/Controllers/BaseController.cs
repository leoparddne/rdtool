using Microsoft.AspNetCore.Mvc;
using RDTool.Server.WebAPI.Filter;

namespace RDTool.Server.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/rdtool/[controller]/[action]")]
    [ApiController]
    [APIResultFilter]
    //[ServiceFilter(typeof(AuthorizeFilter))]
    public class BaseController : ControllerBase
    {

    }
}
