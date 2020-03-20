using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApi.Models;
using Jose;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;

namespace CoreApi.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChkLoginController : BaseController<ChkLoginController, UserData, UserData>
    {
        public ChkLoginController(ILogger<ChkLoginController> logger,IConfiguration configuration)
        {
            _configuration = configuration;
            base._logger = logger;
        }

        public override UserData PostMethon(UserData Parm)
        {
            var filter = _Context.UserData.AsQueryable().Where(o => o.Id == ((UserData)Parm).Id && 
                                                                    o.Password == ((UserData)Parm).Password);
            UserData result = new UserData();
            if (filter != null && filter.Count()==1)
            {
                //更新最新嘗試登入時間
                result = filter.First();
                result.LateLoginTime = DateTime.Now;
                _Context.UserData.Attach(result);
                _Context.Entry(result).Property(p => p.LateLoginTime).IsModified = true;
                _Context.SaveChanges();

                result.Password = "";
                result.Email = result.Email.Replace(".", "!!!");
                if (result.VaildId != "Y")
                {
                    BaseR.MSG = "此帳號尚未通過信箱驗證<br />請至註冊信箱開通驗證信<br />";
                    BaseR.IS_Error = "Y";
                    BaseR.Status = "01";
                    return result;
                }
                _HeaderData.id = result.Id;
                _HeaderData.Name = result.Name;
                _HeaderData.Sex = result.Sex;
                _HeaderData.Uid = result.Uid;
                string JwtKey = base._configuration.GetValue<string>("JwtKey");
                BaseR.Token = Jose.JWT.Encode(_HeaderData, Encoding.UTF8.GetBytes(JwtKey), JwsAlgorithm.HS256);
                BaseR.MSG = "歡迎回來 " + result.Name;
                BaseR.UID = result.Uid;
                BaseR.IS_Error = "N";
                BaseR.Status = "00";
                
            }
            else
            {
                BaseR.MSG = "使用者帳密錯誤";
                BaseR.IS_Error = "Y";
                BaseR.Status = "100";
            }
            return result;
        }
    }
}