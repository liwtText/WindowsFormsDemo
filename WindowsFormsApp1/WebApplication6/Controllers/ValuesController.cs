using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication6.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
        /// <summary>
        /// 得到所有数据
        /// </summary>
        /// <returns>返回数据</returns>
        [HttpGet]
        public string GetAllChargingData()
        {
            return "ChargingData";
        }

        /// <summary>
        /// 得到当前Id的所有数据
        /// </summary>
        /// <param name="id">参数Id</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public string GetAllChargingData(string id)
        {
            return "ChargingData" + id;
        }

        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="oData">对象</param>
        /// <returns>提交是否成功</returns>
        [HttpPost]
        public bool Post(TB_CHARGING oData)
        {
            return true;
        }

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="oData">对象</param>
        /// <returns>提交是否成功</returns>
        [HttpPut]
        public bool Put(TB_CHARGING oData)
        {
            return true;
        }

        /// <summary>
        /// delete操作
        /// </summary>
        /// <param name="id">对象id</param>
        /// <returns>操作是否成功</returns>
        [HttpDelete]
        public bool Delete(string id)
        {
            return true;
        }
    }

    /// <summary>
    /// 充电对象实体
    /// </summary>
    public class TB_CHARGING
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 充电设备名称
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 充电设备描述
        /// </summary>
        public string DES { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATETIME { get; set; }
    }
}
