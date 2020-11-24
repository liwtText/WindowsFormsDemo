using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unit;
using UnitInter;

namespace InterfaceServer
{
    public abstract class IWebService
    {
        /// <summary>
        /// 字段映射
        /// </summary>
        protected TransProp transProp;
        /// <summary>
        /// 包
        /// </summary>
        protected IPackage package;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="transProp"></param>
        /// <param name="package"></param>
        public IWebService(TransProp transProp, IPackage package)
        {
            this.transProp = transProp;
            this.package = package;
        }
        /// <summary>
        /// 获取服务路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetUrl()
        {
            return transProp.url;
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual string ProcessPost(string request)
        {
            string retStr=string.Empty;
            if (package == null)
            {
                //解包
                //string deData = package.DePackage(request, transProp);
                //处理
                string doData=DoWork(request);
                //打包
                retStr = package.GetPackage(doData, transProp);
            }
            else
            {
                retStr=DoWork(request);
            }
            return retStr;
        }
        /// <summary>
        /// 业务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract string DoWork(string request);
    }
}
