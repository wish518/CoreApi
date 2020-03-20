using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Common;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace CoreApi.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResendMailController : BaseController<ResendMailController, UserData, string>
    {
        public ResendMailController(ILogger<ResendMailController> logger)
        {
            _logger = logger;
        }

        public override string PostMethon(UserData Parm)
        {
            var filter = _Context.UserData.AsQueryable().Where(o => o.Uid == ((UserData)Parm).Uid &&
                                                                o.VaildId != "Y");
            UserData result = new UserData();
            if (filter != null && filter.Count() == 1)
            {
                string VaildID = Guid.NewGuid().ToString();
                string Html = "http://114.32.54.227/DTW_Business/RegisteredVaild/" + VaildID;
                Email EMail = new Email();
                EMail.UseGmail(Parm.Email, "東方神秘世界驗證信(重新寄發)", "點選驗證網址： " + Html + " ，以便開通服務");


                //更新最新嘗試登入時間
                result = filter.First();
                result.Uid = Parm.Uid;
                result.Email = Parm.Email;
                result.VaildId = VaildID;
                _Context.UserData.Attach(result);
                _Context.Entry(result).State = EntityState.Modified;
                _Context.SaveChanges();

                BaseR.MSG = "已寄發信箱驗證信<br />到" + Parm.Email +"信箱<br />請至信箱點擊驗證網址 開通服務";
                BaseR.UID = result.Uid;
                BaseR.IS_Error = "N";
                BaseR.Status = "00";
                return BaseR.MSG;
            }
            else
            {
                BaseR.MSG = "寄發驗證信發生錯誤，請再重新填寫驗證信箱";
                BaseR.IS_Error = "Y";
                BaseR.Status = "100";
            }
            return BaseR.MSG;
        }

    }
}