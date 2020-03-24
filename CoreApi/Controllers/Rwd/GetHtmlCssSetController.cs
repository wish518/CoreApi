using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApi.Controllers.Rwd
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetHtmlCssSetController : BaseController<GetHtmlCssSetController, BaseData<HtmlCssSet>, GetRwdSet>
    {
        public GetHtmlCssSetController(ILogger<GetHtmlCssSetController> logger)
        {
            _logger = logger;
        }

        public override GetRwdSet PostMethon(BaseData<HtmlCssSet> Parm)
        {
            GetRwdSet result = new GetRwdSet();
            var filter = _Context.HtmlCssSetDetail.AsQueryable().Where(o => (o.Uid == Parm.Uid || o.Uid == "System")
                                                                    && o.IsSet == "Y" && o.PageCode == Parm.Data.PageCode
                                                                    && (o.Width >= Parm.Data.Width && o.Height >= Parm.Data.Height))
                                                          .OrderBy(o=>o.Width).ThenBy(o=>o.Height).ToList();
            filter = filter.Where(o => o.Width == filter[0].Width && o.Height == filter[0].Height).ToList();

            var filter2 = _Context.HtmlCssSet.Where(o=>o.PageCode == Parm.Data.PageCode && o.Width == filter[0].Width && o.Height == filter[0].Height).ToList();
            var filter3 = _Context.HtmlSet.Where(o => o.PageCode == Parm.Data.PageCode).ToList();
            result.HtmlCssSetDetailData = filter;
            result.HtmlCssSetData = filter2;
            result.HtmlSetData = filter3;
            base.BaseR.IS_Error = "N";
            return result;
        }

    }
}