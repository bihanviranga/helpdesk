using HelpDesk.Model;
using HelpDesk.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    public class UserController : Controller
    {

        [HttpPost]
        [Route("[controller]/Register")]
        public  JsonResult Register([FromBody]RegistrationModel model)
        {
            
            var user = new TktUser
            {
                UserName = model.UserName,
                CompanyId = model.CompanyId,
                UserType = model.UserType,
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                UserImage = model.UserImage,
                UserRole = model.UserRole

            };

            // Create Token
                if ( model.TokenAvailable == null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // user store code shoud be here

                    Expires = DateTime.UtcNow.AddDays(1),   
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Json(token);
            }
            else if ( model.TokenAvailable != null)
            {
                return Json("User Registration successful");
            }

            return Json("Your Email has Alrady Exist");
        }

        [HttpPost]
        [Route("[controller]/Login")]
        public JsonResult Login([FromBody]LoginModel model)
        {
            //user existing chechk code here ( not dev yet )


            if (1==1) // demo condition ** will change
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // save to token in DB

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Json(token);
            }
            return Json("faild To logIn");
        }

        
    }
}
