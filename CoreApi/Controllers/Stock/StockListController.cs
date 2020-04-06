using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApi.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockListController : BaseController<StockListController, DropDownData, List<DropDownData>>
    {
        public StockListController(ILogger<StockListController> logger)
        {
            _logger = logger;
        }
        public override List<DropDownData> PostMethon(DropDownData Parm)
        {
            List<DropDownData> result = new List<DropDownData>();
            using (_conn)
            {
                string strSql = @" SELECT CategoryCode Value,CategoryName Text
                                     FROM SotckCategory
                                    ORDER BY CategoryCode ";
                var resultData = _conn.Query<DropDownData>(strSql);
                result = resultData.ToList();
            }
            base.BaseR.IS_Error = "N";
            return result;
        }
    }
}