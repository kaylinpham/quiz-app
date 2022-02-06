using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Auth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QuizContext _context;
        private MyTokenHandler tokenHandler {get;}
        private MyPasswordEncryptor passwordEncryptor {get;}

        public UserController(QuizContext context, MyTokenHandler tknHandler, MyPasswordEncryptor myPasswordEncryptor)
        {
            _context = context;
            tokenHandler = tknHandler;
            passwordEncryptor = myPasswordEncryptor;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Register(User user)
        {
            //validate that this user does not existed!
            string name = user.UserName;
            string password = user.Password;
            var isExist = _context.Users.Where(userMem => userMem.UserName == name).FirstOrDefault();
            if(isExist != null){
                    var log = new {Message = $"The username {user.UserName} existed."};
                    var mess = JsonConvert.SerializeObject(log);
                    return new ObjectResult(mess){StatusCode = StatusCodes.Status400BadRequest};
            }
            try {
                Guid gen = Guid.NewGuid();
                string uid = Convert.ToBase64String(gen.ToByteArray());
                password = passwordEncryptor.EncryptPassword(password);
                var newAdded = new User(){UID = uid, UserName = name, Password = password};
                await _context.AddRangeAsync(newAdded);
                await _context.SaveChangesAsync();

                //Generate Token to User
                string token = tokenHandler.GenerateToken(newAdded);
                return token;
            } catch (Exception ex){
                return "There was an error" + ex.ToString();
            } 
        }
        [HttpPost]
        public ActionResult<string> Login(User user)
        {
            string name = user.UserName;
            string password = user.Password;
            var isExist = (User)_context.Users.Where(userMem => userMem.UserName == name).FirstOrDefault();
            if(isExist == null){
                return BadRequest();
            }
            string validPassword = passwordEncryptor.DecryptPassword(isExist.Password);
            if(password != validPassword){
                return Unauthorized();
            }
            string token = tokenHandler.GenerateToken(isExist);
            return token;
        }
    }
}
