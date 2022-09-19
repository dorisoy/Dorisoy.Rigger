using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dory.Rigger.WebApi
{
    public class RSACryption
    {
        #region RSA 加密解密

        #region RSA 的密钥产生

        /// <summary>
        /// 创建密钥文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key"></param>
        public void CreateKeyXML(string path, string privatekey)
        {
            try
            {

                FileStream keyxml = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(keyxml);
                sw.WriteLine(privatekey);
                sw.Close();
                keyxml.Close();

                //BinarySerialize<string> serialize = new BinarySerialize<string>();
                //serialize.Serialize(privatekey, path);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// RSA产生密钥
        /// </summary>
        /// <param name="xmlKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public (string, string) RSAKey()
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                string xmlKeys = rsa.ToXmlString(true);
                string xmlPublicKey = rsa.ToXmlString(false);
                return (xmlKeys, xmlPublicKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RSAKey(string PrivateKeyPath, string PublicKeyPath)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                string xmlPrivateKeys = rsa.ToXmlString(true);
                this.CreateKeyXML(PrivateKeyPath, xmlPrivateKeys);
                string xmlPublicKey = rsa.ToXmlString(false);
                this.CreateKeyXML(PublicKeyPath, xmlPublicKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取公钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadPublicKey(string path)
        {
            try
            {

                StreamReader reader = new StreamReader(path);
                string publickey = reader.ReadToEnd();
                reader.Close();
                return publickey;

                //BinarySerialize<string> serialize = new BinarySerialize<string>();
                //string publickey = serialize.DeSerialize(path);
                //return publickey;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 读取私钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadPrivateKey(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                string privatekey = reader.ReadToEnd();
                reader.Close();
                return privatekey;

                //BinarySerialize<string> serialize = new BinarySerialize<string>();
                //string privitekey = serialize.DeSerialize(path);
                //return privitekey;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 初始化注册表，程序运行时调用，在调用之前更新公钥xml
        /// </summary>
        /// <param name="path">公钥路径</param>
        public void InitialReg(string path)
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");
            Random ra = new Random();
            string publickey = this.ReadPublicKey(path);
            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE/JX/Register").ValueCount <= 0)
            {
                this.WriteReg("RegisterRandom", ra.Next(1, 100000).ToString());
                this.WriteReg("RegisterPublicKey", publickey);
            }
            else
            {
                this.WriteReg("RegisterPublicKey", publickey);
            }
        }

        /// <summary>
        /// 读注册表中指定键的值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>返回键值</returns>
        private string ReadReg(string key)
        {
            string temp = "";
            try
            {
                RegistryKey myKey = Registry.LocalMachine;
                RegistryKey subKey = myKey.OpenSubKey(@"SOFTWARE/JX/Register");
                temp = subKey.GetValue(key).ToString();
                subKey.Close();
                myKey.Close();
                return temp;
            }
            catch (Exception)
            {
                throw;//可能没有此注册项;
            }
        }

        /// <summary>
        /// 创建注册表中指定的键和值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        private void WriteReg(string key, string value)
        {
            try
            {
                RegistryKey rootKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");
                rootKey.SetValue(key, value);
                rootKey.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }



        #endregion

        #region RSA加密函数
        //############################################################################## 
        //RSA 方式加密 
        //KEY必须是XML的形式,返回的是字符串 
        //该加密方式有长度限制的！
        //############################################################################## 

        /// <summary>
        /// RSA的加密函数
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns></returns>
        public string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA的加密函数 
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="EncryptString">待加密的字节数组</param>
        /// <returns></returns>
        public string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {
            try
            {
                byte[] CypherTextBArray;
                string Result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                CypherTextBArray = rsa.Encrypt(EncryptString, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA的解密函数
        /// <summary>
        /// RSA的解密函数
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns></returns>
        public string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA的解密函数 
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="DecryptString">待解密的字节数组</param>
        /// <returns></returns>
        public string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
               RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                DypherTextBArray = rsa.Decrypt(DecryptString, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region RSA数字签名

        #region 获取Hash描述表
        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="strSource">待签名的字符串</param>
        /// <param name="HashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(string strSource, ref byte[] HashData)
        {
            try
            {
                byte[] Buffer;
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                Buffer = Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="strSource">待签名的字符串</param>
        /// <param name="strHashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(string strSource, ref string strHashData)
        {
            try
            {
                //从字符串中取得Hash描述 
                byte[] Buffer;
                byte[] HashData;
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Buffer = Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="objFile">待签名的文件</param>
        /// <param name="HashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            try
            {
                //从文件中取得Hash描述 
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="objFile">待签名的文件</param>
        /// <param name="strHashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            try
            {
                //从文件中取得Hash描述 
                byte[] HashData;
                HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA签名
        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="HashbyteSignature">待签名Hash描述</param>
        /// <param name="EncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="HashbyteSignature">待签名Hash描述</param>
        /// <param name="m_strEncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] EncryptedSignatureData;
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="strHashbyteSignature">待签名Hash描述</param>
        /// <param name="EncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;

                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="strHashbyteSignature">待签名Hash描述</param>
        /// <param name="strEncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                byte[] EncryptedSignatureData;
                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA 签名验证
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="HashbyteDeformatter">Hash描述</param>
        /// <param name="DeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="strHashbyteDeformatter">Hash描述</param>
        /// <param name="DeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="HashbyteDeformatter">Hash描述</param>
        /// <param name="strDeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="strHashbyteDeformatter">Hash描述</param>
        /// <param name="strDeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                byte[] HashbyteDeformatter;
                //摘要进行base64算法
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                //UserDetailContent进行base64加密
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public  string RSAEncrypt1(string publickey, string content)
        {
            /*publickey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
*/            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public  string RSADecrypt1(string privatekey, string content)
        {
            //privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

    }
}
