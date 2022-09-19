using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;

namespace SharedUI
{
    public class RegisterChecker
    {
        
        /// <summary>
        /// 获取机器码,机器码格式为UUIDCPUIDMac
        /// </summary>
        /// <param name="retErrorInfo">如果发生异常，则由此参数返回异常信息，如果执行成功，则此参数返回空字符串</param>
        /// <returns>返回机器码</returns>
        public string GetMachineID(out string retErrorInfo)
        {
            retErrorInfo = "";
            try
            {
                //获取机器码，uuID+cpu序号+mac地址
                CHardwareInfo Tmp = new CHardwareInfo();
                string Tmpid = Tmp.GetUUID() + Tmp.GetCpuID() + Tmp.GetNetCardMacAddress();
                return Tmpid;
            }
            catch (Exception ex)
            {
                retErrorInfo = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取机器码,机器码格式为“UUIDCPUIDMac+IP”检查字符串中的加号可以分离出ip地址
        /// </summary>
        /// <param name="retErrorInfo"></param>
        /// <returns>如果发生异常，则由此参数返回异常信息，如果执行成功，则此参数返回空字符串</returns>
        public string GetMachineIDIncludeIP(out string retErrorInfo)
        {
            retErrorInfo = "";
            try
            {
                //获取机器码，uuID+cpu序号+mac地址+IP
                CHardwareInfo Tmp = new CHardwareInfo();

                string Tmpid = Tmp.GetUUID() + Tmp.GetCpuID() + Tmp.GetNetCardMacAddress();
                return Tmpid;

            }
            catch (Exception ex)
            {
                retErrorInfo = ex.Message;
                return "";
            }
        }

      
        #region 20171212 add by eddy
        /// <summary>
        /// 获取机器码,机器码格式为“UUIDCPUIDMac+IP”检查字符串中的加号可以分离出ip地址
        /// </summary>
        /// <param name="retErrorInfo"></param>
        /// <returns>如果发生异常，则由此参数返回异常信息，如果执行成功，则此参数返回空字符串</returns>
        public List<string> GetMachineIDIncludeIPs(out string retErrorInfo)
        {
            retErrorInfo = "";
            List<string> list = new List<string>();
            try
            {
                //获取机器码，uuID+cpu序号+mac地址+IP
                CHardwareInfo Tmp = new CHardwareInfo();
                string Tmpid = Tmp.GetUUID() + Tmp.GetCpuID();
                List<string> macAndIPs = Tmp.GetHardwareMacAddressAndIP();
                if (macAndIPs.Count > 0)
                {
                    foreach (string macandip in macAndIPs)
                    {
                        list.Add(Tmpid + macandip);
                    }
                }
                else
                {
                    retErrorInfo = "获取机器码信息为空";
                }
            }
            catch (Exception ex)
            {
                retErrorInfo = "获取机器码信息异常:" + ex.Message;
            }

            return list;
        }

        #endregion

        
    }
}
