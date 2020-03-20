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
    public class GetHtmlCssSetController : BaseController<GetHtmlCssSetController, HtmlCssSet, HtmlCssSet>
    {
        public GetHtmlCssSetController(ILogger<GetHtmlCssSetController> logger)
        {
            _logger = logger;
        }
        /*public override HtmlCssSet PostMethon(HtmlCssSet Parm)
        {
            var filter = _Context.HtmlCssSet.AsQueryable().Where(o => o.Uid == Uid && o.PageCode == PageCode).OrderBy(o => o.CssTag).ToList();
            var filte2 = _Context.HtmlCssSetDetail.AsQueryable().Where(o => o.Uid == Uid && o.PageCode == PageCode).OrderBy(o => o.CssTag).ToList();
            return GetHtmlCss(Parm.Uid, Parm.PageCode);
        }*/

    }
}