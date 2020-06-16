using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class UserController : Controller
    {
        IUserRepository _User;

        public UserController(IUserRepository User)
        {
            this._User = User;
        }

        [HttpPost]
        [Route("[controller]/Register")]
        public JsonResult Register([FromBody] RegistrationModel model)
        {

            var user = new UserModel
            {
                UserName = model.UserName,
                CompanyId = model.CompanyId,
                UserType = model.UserType,
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                UserImage = model.UserImage,
                UserRole = model.UserRole,
                PasswordHash = model.Password
            };

            //var Result = _User.Add(user);

            // Create Token
            if (model.TokenAvailable == null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                            new Claim(ClaimTypes.Name, user.CompanyId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Json(token);
            }
            else if (model.TokenAvailable != null)
            {
                return Json("User Registration successful");
            }

            return Json("Your Email has Alrady Exist");
        }

        [HttpPost]
        [Route("[controller]/Login")]
        public JsonResult Login([FromBody] LoginModel model) //not working yet
        {
            //user existing chechk code here ( not dev yet )


            if (1 == 1) // demo condition ** will change
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
