using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Auth;

namespace server.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizRepoController : ControllerBase {
        private readonly QuizContext _context;
        private MyTokenHandler _tokenHandler {get;}
        public QuizRepoController(QuizContext context, MyTokenHandler tokenHandler){
            _context = context;
            _tokenHandler = tokenHandler;
        }
        [HttpPost]
        public async Task<ActionResult<QuizPackage>> PostQuizPackage(QuizPackage quizPackage){
            string token = Request.Headers["Authorization"].ToString();
            string uid = _tokenHandler.ValidateToken(token);
            if(uid == null){
                return BadRequest();
            }
            if(await _context.QuizPackages.FirstOrDefaultAsync(quiz => quiz.PackageName == quizPackage.PackageName) != null){
                return BadRequest();
            }
            quizPackage.UserID = uid;
            await _context.QuizPackages.AddAsync(quizPackage);
            await _context.SaveChangesAsync();
            return quizPackage;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuizPackage(int id){
            string token = Request.Headers["Authorization"].ToString();
            string uid = _tokenHandler.ValidateToken(token);
            if(uid == null){
                return BadRequest();
            }
            var quizPackage = await _context.QuizPackages.FindAsync(id);
            if(quizPackage == null || quizPackage.UserID != uid){
                return BadRequest();
            }
            _context.QuizPackages.Remove(quizPackage);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<QuizInfo>>> GetQuizInfoInPackage(int id){
            string token = Request.Headers["Authorization"].ToString();
            string uid = _tokenHandler.ValidateToken(token);
            if(uid == null){
                return BadRequest();
            }
            var quizPackage = await _context.QuizPackages.FindAsync(id);
            if(quizPackage == null || quizPackage.UserID != uid){
                return BadRequest();
            }
            quizPackage.Quizzes = await _context.QuizInfos.Where(q => q.PackageID == id).ToListAsync();
            return quizPackage.Quizzes;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<QuizPackage>> ChangeName(int id, QuizPackage quizPackage){
            string token = Request.Headers["Authorization"].ToString();
            string uid = _tokenHandler.ValidateToken(token);
            if(uid == null || uid != quizPackage.UserID){
                return BadRequest();
            }
            _context.Entry<QuizPackage>(quizPackage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return quizPackage;
        }
    }
}