using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApi.Controllers.Rwd
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetHtmlCssController : BaseController<GetHtmlCssController, HtmlCssSet, string>
    {
        // GET: /<controller>/
        public GetHtmlCssController(ILogger<GetHtmlCssController> logger)
        {
            _logger = logger;
        }

        public override FileContentResult GetFileMethon(string[] Parm, string Parm2)
        {
            string css = GetHtmlCss(Parm[1], Parm[0], Convert.ToInt32(Parm2.Substring(3)), Convert.ToInt32(Parm2.Substring(2, 3)));
            byte[] array = Encoding.ASCII.GetBytes(css);
            return File(array, "text/css"); ;

        }

        private string GetHtmlCss(string Uid, string PageCode, int Width, int Height)
        {
            /*var filter = _Context.HtmlCssSetDetail.AsQueryable()
                        .Join(_Context.HtmlCssSet, o => new { o.PageCode, o.CssTag }, p => new { p.PageCode, p.CssTag },
                              (o, p) => new { o, p.Srno, p.Width, p.Height })
                        .Where(o => (o.o.Uid == Uid || o.o.Uid == "") && o.o.PageCode == PageCode && o.Width >= Width && o.Height >= Height)
                        .OrderByDescending(o => o.o.Uid).ThenBy(o => o.Srno)/*.ThenByDescending(o => new {o.Width,o.Height, o.o.CssTag }).ToList();*/

            var filter = from o in _Context.HtmlCssSetDetail.AsQueryable()
                         join p in _Context.HtmlCssSet
                           on new { o.PageCode, o.CssTag, o.Width, o.Height } equals new { p.PageCode, p.CssTag, p.Width, p.Height }
                         where (o.Uid == Uid || o.Uid == "System") && o.IsSet=="N" &&  o.PageCode == PageCode && o.Width >= Width && o.Height >= Height
                       orderby o.Uid descending, p.Srno, o.Width descending, o.Height descending, o.CssTag
                         select new { o, p.Srno, p.Width, p.Height };

            string result = "", o_CssTag = "", CssTag = "", o_Rwd = "";
            foreach (var row in filter)
            {
                row.o.CssTag += " {OovalueoO}";

                if (row.o.CssTag != o_CssTag || o_Rwd != (row.Width.ToString() + row.Height.ToString()))
                {
                    if (o_Rwd == (row.Width.ToString() + row.Height.ToString()))
                    {
                        CssTag = CssTag.Replace("OovalueoO} }","} "+ row.o.CssTag + " }");
                    }
                    else
                    {
                        result += CssTag.Replace("OovalueoO", "");

                        if (row.Width < 999 || row.Height < 999)
                        {
                            CssTag = "@media screen ";
                            if (row.Width < 999)
                                CssTag += string.Format("and (max-width: {0}px) ", row.Width);
                            if (row.Height < 999)
                                CssTag += string.Format("and (max-height: {0}px) ", row.Height);

                            CssTag += "{ " + row.o.CssTag + " }";
                        }
                        else
                        {
                            CssTag = row.o.CssTag;
                        }
                    }
                    o_Rwd = row.Width.ToString() + row.Height.ToString();
                    o_CssTag = row.o.CssTag;
                    result += " \r\n";
                }

                CssTag = CssTag.Replace("OovalueoO", row.o.Css + " OovalueoO");
            }
            result += CssTag.Replace("OovalueoO", "");
            BaseR.IS_Error = "N";
            BaseR.Status = "00";

            return result;
        }
    }
}