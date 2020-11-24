using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitInter
{
    public abstract class IPackage
    {
        public string key { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        public IPackage(string key)
        {
            this.key = key;
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="transProp"></param>
        /// <returns></returns>
        public abstract string GetPackage(string data, TransProp transProp);
        /// <summary>
        /// 解包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="transProp"></param>
        /// <returns></returns>
        public abstract string DePackage(string data, TransProp transProp);

        /// <summary>
        /// 获取Json字符串
        /// </summary>
        /// <param name="isClientSend">打包方向：作为客户端发送：true，作为接收端接收：false</param>
        /// <param name="inStr"></param>
        /// <param name="transProp"></param>
        /// <returns></returns>
        protected string GetJsonStr(bool isClientSend, string inStr, TransProp transProp)
        {
            string retStr = string.Empty;

            JObject jObject = JObject.Parse(inStr);

            if (transProp != null && jObject != null)
            {
                List<DataConfig> lstDataConfig = isClientSend ? transProp.lstDataConfigSend : transProp.lstDataConfigReceive;
                if (transProp.IsTrans == "true" && lstDataConfig != null)
                {
                    #region 转换

                    JObject jObjectSend = new JObject();

                    foreach (var item in lstDataConfig)
                    {
                        string beforeName = isClientSend ? item.OldName : item.NewName;
                        string afterName = isClientSend ? item.NewName : item.OldName;

                        JToken value = jObject[beforeName] != null ? jObject[beforeName] : null;

                        if (value == null && !string.IsNullOrEmpty(item.DefaultValue))
                        {
                            value = JToken.FromObject(item.DefaultValue);
                        }

                        if (value != null)
                        {
                            try
                            {
                                switch (item.DataType)
                                {
                                    case "string":
                                        if (!string.IsNullOrEmpty(item.DataMode))
                                        {
                                            JObject tempDataMode = JObject.Parse(item.DataMode);
                                            jObjectSend.Add(afterName, tempDataMode[value.Value<string>()]);
                                        }
                                        else
                                        {
                                            jObjectSend.Add(afterName, value.Value<string>());
                                        }
                                        break;
                                    case "DateTime":
                                        jObjectSend.Add(afterName, Convert.ToDateTime(value.Value<string>()).ToString(item.DataMode));
                                        break;
                                    case "ImagePath":
                                        if (!string.IsNullOrEmpty(value.Value<string>()))
                                            jObjectSend.Add(afterName, item.DataMode.TrimEnd('/') + value.Value<string>());
                                        else
                                            jObjectSend.Add(afterName, "");
                                        break;
                                    case "double":
                                        double numdouble = Convert.ToDouble(value.Value<string>());
                                        double transRatedouble = !string.IsNullOrEmpty(item.DataMode) ? Convert.ToDouble(item.DataMode) : 1;
                                        numdouble *= transRatedouble;
                                        jObjectSend.Add(afterName, numdouble);
                                        break;
                                    case "int":
                                        double numint = Convert.ToDouble(value.Value<string>());
                                        double transRateint = !string.IsNullOrEmpty(item.DataMode) ? Convert.ToDouble(item.DataMode) : 1;
                                        numint *= transRateint;
                                        jObjectSend.Add(afterName, Convert.ToInt32(numint));
                                        break;
                                    case "Array":
                                    case "Object":
                                        jObjectSend.Add(afterName, value);
                                        break;

                                }
                            }
                            catch (Exception ex)
                            {
                                //LogFileCode.WriteErrorLogMessage("解析" + beforeName + "到" + afterName + "异常" + ex.ToString());
                            }
                        }
                        else
                        {
                            //LogFileCode.WriteErrorLogMessage("解析" + beforeName + "到" + afterName + "失败，" + beforeName + "不存在");
                        }
                    }

                    retStr = Newtonsoft.Json.JsonConvert.SerializeObject(jObjectSend);

                    #endregion
                }
                else
                {
                    retStr = inStr;
                }
            }
            else
            {
                retStr = inStr;
            }

            return retStr;
        }
    }
}
