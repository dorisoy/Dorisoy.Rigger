using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI
{
    public class CHardwareInfo
    {
        /// <summary>
        /// 取机器名 
        /// </summary>
        /// <returns></returns>
        public string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        /// <summary>
        /// 取CPU编号 
        /// </summary>
        /// <returns></returns>
        public String GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 读取Mac地址
        /// </summary>
        /// <returns></returns>
        public string GetNetCardMacAddress()
        {
            ManagementClass mc;
            ManagementObjectCollection moc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            moc = mc.GetInstances();
            string str = "";
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                    str = mo["MacAddress"].ToString();

            }
            return str;
        }
        /// <summary>
        /// 读取C盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetDiskVolumeSerialNumber()
        {
            ManagementObject disk;
            disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }


        ///<summary>
        ///   获取硬盘ID
        ///</summary>
        ///<returns> string </returns>
        public string GetHDid()
        {
            string HDid = " ";
            using (ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive"))
            {
                ManagementObjectCollection moc1 = cimobject1.GetInstances();
                foreach (ManagementObject mo in moc1)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                    mo.Dispose();
                }
            }
            return HDid.ToString();
        }

        public String GetDiskSerialNumber()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query = new SelectQuery("Win32_DiskDrive", "", new string[] { "PNPDeviceID", "Signature" });
            ManagementObjectCollection myCollection = mos.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject moo = em.Current;
            string id = moo.Properties["signature"].Value.ToString().Trim();
            return id;
        }

        /// <summary>
        /// 获取？？
        /// </summary>
        /// <returns></returns>
        public string GetUUID()
        {
            ManagementClass clsMgtClass = new ManagementClass("Win32_ComputerSystemProduct");
            ManagementObjectCollection colMgtObjCol = clsMgtClass.GetInstances();
            foreach (ManagementObject objMgtObj in colMgtObjCol)
            {
                // MessageBox.Show(objMgtObj.Properties["IdentifyingNumber"].Value.ToString());  //主板序列号
                return objMgtObj.Properties["UUID"].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取？？
        /// </summary>
        /// <returns></returns>
        public string GetIdentifyingNumber()
        {
            ManagementClass clsMgtClass = new ManagementClass("Win32_ComputerSystemProduct");
            ManagementObjectCollection colMgtObjCol = clsMgtClass.GetInstances();
            foreach (ManagementObject objMgtObj in colMgtObjCol)
            {
                // MessageBox.Show(objMgtObj.Properties["IdentifyingNumber"].Value.ToString());  //主板序列号
                return objMgtObj.Properties["IdentifyingNumber"].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取物理网卡的Mac和对应的IP
        /// </summary>
        /// <returns></returns>
        public List<string> GetHardwareMacAddressAndIP()
        {
            List<string> list = new List<string>();

            System.Net.NetworkInformation.NetworkInterface[] fNetworkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (System.Net.NetworkInformation.NetworkInterface adapter in fNetworkInterfaces)
            {
                string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                if (rk != null)
                {
                    // 区分 PnpInstanceID  
                    // 如果前面有 PCI 就是本机的真实网卡 
                    string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    if (fPnpInstanceID.Length > 3 && fPnpInstanceID.Substring(0, 3) == "PCI")
                    {
                        string macaddress = MACFormat(adapter.GetPhysicalAddress(), ":");
                        string ipaddress = "";
                        System.Net.NetworkInformation.IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();
                        System.Net.NetworkInformation.UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;
                        foreach (System.Net.NetworkInformation.UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                        {
                            if (UnicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                ipaddress = UnicastIPAddressInformation.Address.ToString();

                                if (IPFilter(ipaddress))
                                {
                                    list.Add(macaddress + "+" + ipaddress); //Ip 地址
                                }
                            }
                        }
                    }
                }
            }


            #region 备注：网卡区分
            //string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
            //int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
            //if (fPnpInstanceID.Length > 3 &&
            //    fPnpInstanceID.Substring(0, 3) == "PCI")
            //    fCardType = "物理网卡";
            //else if (fMediaSubType == 1)
            //    fCardType = "虚拟网卡";
            //else if (fMediaSubType == 2)
            //    fCardType = "无线网卡";
            #endregion

            return list;
        }

        /// <summary>
        /// MAC地址格式为为16进制
        /// </summary>
        /// <param name="address"></param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        private string MACFormat(System.Net.NetworkInformation.PhysicalAddress pa, string split)
        {
            // 格式化显示MAC地址              
            byte[] bytes = pa.GetAddressBytes();//返回当前实例的地址
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));//以十六进制格式化
                if (i != bytes.Length - 1)
                {
                    sb.Append(split);
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// IP地址有效性验证
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private bool IPFilter(string ip)
        {
            if (ip == "0.0.0.0")
            {
                return false;
            }
            else if (ip == "255.255.255.255")
            {
                return false;
            }
            else if (ip.IndexOf("169.254.") == 0)
            {//169.254.X.X是保留地址。如果你的IP地址是自动获取IP地址，而你在网络上又没有找到可用的DHCP服务器
                return false;
            }
            else
                return true;
        }
    }
}
