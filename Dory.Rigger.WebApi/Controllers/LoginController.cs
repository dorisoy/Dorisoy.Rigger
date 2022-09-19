using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dory.Rigger.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("CreateToken")]
        public AppSrvResult<Response<string>> CreateToken()
        {
            // 1. 定义需要使用到的Claims
            var claims = new[]
            {
        new Claim("Id", "9527"),
        new Claim("Name", "Admin")
    };

            // 2. 从 appsettings.json 中读取SecretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 从 appsettings.json 中读取Expires
            var expires = Convert.ToDouble(_configuration["JWT:Expires"]);

            // 6. 根据以上，生成token
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],     //Issuer
                _configuration["JWT:Audience"],   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddDays(expires),   //expires
                signingCredentials               //Credentials
            );

            // 7. 将token变为string
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var res = new Response<string>();
            res.Result = jwtToken;
            return res;
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var token = CreateToken();
            return Ok(token);
        }

        [HttpGet]
        [Route("Test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("ggg");
        }
    }
}
