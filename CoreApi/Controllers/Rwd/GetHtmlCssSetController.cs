using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            /*var filter = _Context.HtmlCssSetDetail.AsQueryable().Where(o => (o.Uid == Parm.Uid || o.Uid == "System")
                                                                    && o.IsSet == "Y" && o.PageCode == Parm.Data.PageCode
                                                                    && (o.Width >= Parm.Data.Width && o.Height >= Parm.Data.Height))
                                                          .OrderBy(o=>o.Width).ThenBy(o=>o.Height).ThenBy(o => o.CssTag).ThenBy(o => o.Css).ToList();
            filter = filter.Where(o => o.Width == filter[0].Width && o.Height == filter[0].Height).OrderBy(o => o.CssTag).ToList();*/
            //var filter2 = _Context.HtmlCssSet.Where(o=>o.PageCode == Parm.Data.PageCode && o.Width == filter[0].Width && o.Height == filter[0].Height).ToList();

            using (_conn)
            {
                string strSql = @" SELECT CssTag,Css,MIN(Width) Width,MIN(Height) Height
                                     INTO #CssWH
                                     FROM HtmlCssSetDetail
                                    WHERE PageCode=@PageCode
                                      AND (Uid = @Uid OR Uid = 'System')
                                      AND Width >= @Width AND Height >= @Height
                                    GROUP BY CssTag,Css

                                  SELECT HtmlCssSet.*
                                    FROM HtmlCssSet 
                                    JOIN (SELECT CssTag,MIN(Width) Width,MIN(Height) Height
                                            FROM #CssWH GROUP BY CssTag) A
                                      ON HtmlCssSet.PageCode=@PageCode
                                     AND A.CssTag = HtmlCssSet.CssTag AND A.Height = HtmlCssSet.Height
                                     AND A.Width = HtmlCssSet.Width AND A.Height = HtmlCssSet.Height
                                   ORDER BY A.CssTag
                                  
                                  SELECT HtmlCssSetDetail.*
                                    FROM HtmlCssSetDetail
                                    JOIN #CssWH A
                                      ON HtmlCssSetDetail.PageCode=@PageCode
                                     AND (HtmlCssSetDetail.Uid = @Uid OR HtmlCssSetDetail.Uid = 'System')
                                     AND A.CssTag = HtmlCssSetDetail.CssTag  AND A.Css = HtmlCssSetDetail.Css
                                     AND A.Width = HtmlCssSetDetail.Width  AND A.Height = HtmlCssSetDetail.Height
                                    
                                   ORDER BY A.CssTag,A.Css

                                  SELECT * FROM HtmlSet WHERE PageCode=@PageCode ORDER BY IIF(ID='',GearingID,ID)";
                var parm = new { PageCode = Parm.Data.PageCode, Uid= Parm.Uid, Width= Parm.Data.Width, Height = Parm.Data.Height };
                var resultData = _conn.QueryMultiple(strSql, parm);
                result.HtmlCssSetData = resultData.Read<HtmlCssSet>().ToList();
                result.HtmlCssSetDetailData = resultData.Read<HtmlCssSetDetail>().ToList();
                result.HtmlSetData = resultData.Read<HtmlSet>().ToList();
                /*strSql = "SELECT * FROM HtmlSet WHERE PageCode=@PageCode ORDER BY IIF(ID='',GearingID,ID)";
                result.HtmlSetData = _conn.Query<HtmlSet>(strSql,new { PageCode= Parm.Data.PageCode }).ToList();*/
            }
            base.BaseR.IS_Error = "N";
            return result;
        }
    }
}