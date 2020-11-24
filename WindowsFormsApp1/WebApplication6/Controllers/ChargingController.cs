using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication6.Controllers
{
    //[EnableCors(origins: "http://localhost:8082/", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class ChargingController : ApiController
    {
        /// <summary>
        /// 得到所有数据
        /// </summary>
        /// <returns>返回数据</returns>
        [HttpGet]
        public string GetAllChargingData()
        {
            return "Success";
        }
    }
}