using CoreApi.Common;
using CoreApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;

namespace CoreApi.Controllers
{
    public class BaseController<T, Tin, Tout> : ControllerBase
    {
        public ILogger<T> _logger;
        public IServiceCollection _services;
        public WISHContext _Context;
        public BaseR<Tout> BaseR = new BaseR<Tout>();
        public IConfiguration _configuration;
        public BaseHeaderData _HeaderData;

        public virtual Tout GetMethon(string[] Parm)
        {
            BaseR.Status = "98";
            return default(Tout);
        }
        public virtual FileContentResult GetFileMethon(string[] Parm , string Parm2)
        {
            BaseR.Status = "98";
            return default;
        }

        public virtual Tout PostMethon(Tin model)
        {
            BaseR.Status = "98";
            return default(Tout);
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            return GetFirstAction(null, null);
        }

        [HttpGet("{Parm}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string Parm)
        {
            return GetFirstAction(Parm, null);
        }

        [HttpGet("{Parm}/{Parm2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string Parm, string Parm2)
        {
            return GetFirstAction(Parm, Parm2);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Tin model)
        {
            return await PostFirstAction(model);
        }

        private IActionResult GetFirstAction(string Parm, string Parm2)
        {
            if(HttpContext.Response.StatusCode==401)
                Unauthorized();

            object result=null;
            try
            {
                InJect();
                Type type = typeof(T);
                string ActionName = type.Name.Replace("Controller", "");
                _logger.LogInformation("－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－");
                _logger.LogInformation(ActionName);
                WriteLog log = HttpContext.RequestServices.GetService<WriteLog>();
                Parm = Parm.Replace("y$xz", ", ");
                Parm = Parm.Replace("y$x", ",");
                string[] ar = Parm.Split(',');
                log.WritePostRequestParm(ActionName, ar);
                using (TransactionScope ts = new TransactionScope())
                {
                    if (Parm2 == null)
                    {
                        result = GetMethon(ar);
                        BaseR.Data = (Tout)result;
                    }
                    else if (Parm2 != "")
                        result = GetFileMethon(ar, Parm2);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            if (BaseR.Status == "98")
                return NotFound();

            //log.WriteGetReponse<BaseR>(ActionName, result);
            if (Parm2 == null)
                return Ok(BaseR);
            else if(Parm2 != "")
                return (FileContentResult)result;
            else
                return BadRequest();
        }

        private async Task<IActionResult> PostFirstAction(Tin model)
        {
            if (HttpContext.Response.StatusCode == 401)
                Unauthorized();
            try
            {
                InJect();

                Type type = typeof(T);
                string ActionName = type.Name.Replace("Controller", "");
                _logger.LogInformation("－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－");
                _logger.LogInformation(ActionName);

                await Task.Run(() =>
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        Tout result = PostMethon(model);
                        BaseR.Data = result;
                        ts.Complete();
                    }
                });
                WriteLog log = HttpContext.RequestServices.GetService<WriteLog>();
                log.WritePostRequestParm(ActionName, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            if (BaseR.Status == "98")
                return NotFound();

            //log.WriteGetReponse<BaseR>(ActionName, result);
            return Ok(BaseR);
        }

        private void InJect()
        {
            BaseInJect InJect = HttpContext.RequestServices.GetService<BaseInJect>();
            _Context = InJect._Context;
            _HeaderData = HttpContext.RequestServices.GetService<BaseHeaderData>();
        }
    }
}
