using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedUI.Models;

namespace Dory.Rigger.WebApi.Controllers
{
    [ApiController]
    [Route("machine")]
    public class MachineController : Controller
    {
        public MachineController()
        {

        }

        [HttpPost]
        [Route("GetSN")]
        [Authorize]
        public AppSrvResult<Response<string>> GetSN(RegisteInfo registeInfo)
        {
            RSACryption TmpProcessor = new RSACryption();
            var (xmlKeys, xmlPublicKey) = TmpProcessor.RSAKey();
            //对注册信息进行摘要生成
            string TmpMcode = registeInfo.Email + registeInfo.RegName + registeInfo.MachineCode;//机器码

            string TmpSN = "";
            TmpSN= TmpProcessor.RSAEncrypt1(xmlPublicKey, TmpMcode);
            var xxx = JySN(xmlKeys, TmpSN);
            var res = new Response<string>();
            res.Message = "成功";
            res.Result = TmpSN;
            return res;
        }

        [HttpPost]
        [Route("JySN")]
    /*    [Authorize]*/
        public AppSrvResult<Response<string>> JySN(string xmlKeys,string TmpSN)
        {
            RSACryption TmpProcessor = new RSACryption();
            TmpSN = TmpProcessor.RSADecrypt1(xmlKeys, TmpSN);
            var res = new Response<string>();
            res.Message = "成功";
            res.Result = TmpSN;
            return res;
        }
    }
}
