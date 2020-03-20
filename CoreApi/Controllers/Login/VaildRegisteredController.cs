using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CoreApi.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaildRegisteredController :BaseController<VaildRegisteredController, UserData, UserData>
    {

        public VaildRegisteredController(ILogger<VaildRegisteredController> logger)
        {
            _logger = logger;
        }
        public override UserData GetMethon(string[] Parm)
        {
            return  _Context.UserData.AsQueryable().Where(o => o.Uid == Parm[0].ToString()).First();
        }


    }
}