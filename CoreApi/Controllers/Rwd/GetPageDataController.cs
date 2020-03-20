using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApi.Controllers.Rwd
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPageDataController : BaseController<GetPageDataController, PageData, GetPageData>
    {
        // GET: /<controller>/
        public GetPageDataController(ILogger<GetPageDataController> logger)
        {
            _logger = logger;
        }

        public override GetPageData PostMethon(PageData Parm)
        {
            var filter = _Context.PageData.AsQueryable().Where(o => o.PageArchitecture == ((PageData)Parm).PageArchitecture).ToList();
            var filter2 = _Context.DropDownData.AsQueryable().Where(o => o.SourceCode == "Resolution").OrderBy(o=>o.Value).ToList();
            GetPageData result = new GetPageData();
            BaseR.IS_Error = "N";
            BaseR.Status = "00";
            result.PageDataList = filter;
            result.DropDownData = filter2;
            return result;
        }
    }
}
