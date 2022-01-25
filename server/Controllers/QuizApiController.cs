using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizApiController : ControllerBase
    {
        private readonly QuizContext _context;
        private MyTokenHandler _tokenHandler {get;}
        public QuizApiController(QuizContext context, MyTokenHandler tknHandler)
        {
            _context = context;
            _tokenHandler = tknHandler;
        }

        // GET: api/QuizApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizInfo>>> GetQuizInfos()
        {
            string token;
            token = Request.Headers["Authorization"].ToString();

            string getUID = _tokenHandler.ValidateToken(token);
            if(getUID == null){
                return NotFound();
            } else {
                if(await ValidateUser(Request)){
                    return await _context.QuizInfos.Where(quizinfo => quizinfo.UserID == getUID).ToListAsync();
                } else {
                    return BadRequest();
                }
            }
        }
        // public ActionResult<string> GetQuiz(){
        //     string token;
        //     token = Request.Headers["Authorization"].ToString();

        //     string getUID = _tokenHandler.ValidateToken(token);
        //     return getUID;
        // }

        // GET: api/TodoItems/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<QuizInfo>> GetQuizInfo(int id)
        // {
        //     var quizInfo = await _context.QuizInfos.FindAsync(id);

        //     if (quizInfo == null)
        //     {
        //         return NotFound();
        //     }

        //     return quizInfo;
        // }

        //PUT: api/QuizApi/PutQuizInfo/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizInfo(int id, QuizInfo quizInfo)
        {
            if (id != quizInfo.QuizID)
            {
                return BadRequest();
            }

            _context.Entry(quizInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuizApi/PostQuizInfo
        [HttpPost]
        public async Task<ActionResult<QuizInfo>> PostQuizInfo(QuizInfo quizInfo)
        {
            //find if this is valid customer
            string uid = getUID(Request);

            QuizInfo addNew = new QuizInfo(){UserID = uid, Question = quizInfo.Question, Answer = quizInfo.Answer};
            _context.QuizInfos.Add(addNew);
            await _context.SaveChangesAsync();
            return addNew;
        }

        // DELETE: api/QuizApi/DeleteQuizInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizInfo(int id)
        {
            string uid = getUID(Request);

            var quizInfo = await _context.QuizInfos.FindAsync(id);
            if (quizInfo == null)
            {
                return NotFound();
            }
            if (quizInfo.UserID != uid){
                return BadRequest();
            }

            _context.QuizInfos.Remove(quizInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizInfoExists(int id)
        {
            return _context.QuizInfos.Any(e => e.QuizID == id);
        }
        private string getUID(HttpRequest request){
            string token;
            token = Request.Headers["Authorization"].ToString();
            string getUserID = _tokenHandler.ValidateToken(token);
            return getUserID;
        }
        private async Task<List<User>> getListUser(){
            return await _context.Users.ToListAsync();
        }
        private async Task<bool> ValidateUser(HttpRequest request){
            string uid = getUID(request);
            if(uid == null) return false;
            List<User> userList = await getListUser();
            var isInList = userList.Find(user => user.UID == uid);
            if(isInList == null) return false;
            return true;
        }
    }
}
