using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Auth;

namespace server.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizLikeController : ControllerBase {
        private readonly QuizContext _context;
        private MyTokenHandler _tokenHandler {get;}
        public QuizLikeController(QuizContext context, MyTokenHandler tokenHandler){
            _context = context;
            _tokenHandler = tokenHandler;
        }
        [HttpPost]
        public async Task<ActionResult<FavoritePackageInfo>> PostQuizLike(FavoritePackageInfo quizFvr){
            string token = Request.Headers["Authorization"].ToString();
            string uid = _tokenHandler.ValidateToken(token);
            if(uid == null){
                return BadRequest();
            }
            quizFvr.UserID = uid;
            await _context.FavoritePackageInfos.AddRangeAsync(quizFvr);
            await _context.SaveChangesAsync();
            return quizFvr;
        }
    }
}