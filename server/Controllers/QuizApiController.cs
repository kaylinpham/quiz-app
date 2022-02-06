using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Models.DTO;
using server.Auth;
using Newtonsoft.Json;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizInfo>>> GetQuizInfos()
        {
            string token;
            token = Request.Headers["Authorization"].ToString();
            string getUID = _tokenHandler.ValidateToken(token);
            if(getUID == null){
                    return SendMessage("User Not Found", StatusCodes.Status400BadRequest);
            } else {
                if(await ValidateUser(Request)){
                    return await _context.QuizInfos.Where(quizinfo => quizinfo.UserID == getUID).ToListAsync();
                } else {
                    return SendMessage("User Not Permitted", StatusCodes.Status401Unauthorized);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizInfo>> GetQuizInfo(int id)
        {
            var quizInfo = await _context.QuizInfos.FindAsync(id);
            if(quizInfo.UserID != getUID(Request)){
                return SendMessage("User not found", StatusCodes.Status400BadRequest);
            }
            if (quizInfo == null)
            {
               return SendMessage("Information not found", StatusCodes.Status404NotFound);
            }

            return quizInfo;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizInfo(int id, QuizInfo quizInfo)
        {
            if (id != quizInfo.QuizID || quizInfo.UserID != getUID(Request))
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
                    return SendMessage("Information removed.", StatusCodes.Status404NotFound);
                }
                else
                {
                    throw;
                }
            }

            return SendMessage("Put quiz success", StatusCodes.Status200OK);
        }
        [HttpGet]
        public async Task<ActionResult<List<QuizPublicDTO>>> GetPublicQuiz(){
            List<QuizPublicDTO> resList = new List<QuizPublicDTO>();
            var listRepo = await _context.QuizPackages.Where(quiz => quiz.isPublic == true).ToListAsync();
            foreach(var repo in listRepo){
                var listQuiz = await _context.QuizInfos.Where(quiz => quiz.PackageID == repo.PackageID).ToListAsync();
                resList.Add(new QuizPublicDTO(){Title = repo.PackageName, ListQuiz = listQuiz});
            }
            return resList;
        }


        [HttpPost]
        public async Task<ActionResult<List<QuizInfo>>> PostListQuizInfo(QuizPackageDTO quizDTO){
            string uid = getUID(Request);
            if(uid == null){
                return SendMessage("User not found", StatusCodes.Status400BadRequest);
            } else {
                string title = quizDTO.Title;
                List<QuizInfoDTO> listQuiz = quizDTO.listQuiz;
                bool isPublic = quizDTO.isPublic;
                var isExist = _context.QuizPackages.FirstOrDefault(quiz => quiz.PackageName == title);
                if(isExist != null){
                    return SendMessage("The package existed", StatusCodes.Status400BadRequest);
                }
                QuizPackage packageName = new QuizPackage(){PackageName = title, UserID = uid, isPublic = isPublic};
                await _context.QuizPackages.AddAsync(packageName);
                List<QuizInfo> res = new List<QuizInfo>();
                foreach(var quiz in listQuiz){
                    var quizInfo = new QuizInfo(){Question = quiz.Question,
                                                                    Answer = quiz.Answer,
                                                                    PackageID = packageName.PackageID,
                                                                    UserID = uid,
                                                                    QuizPackage = packageName};
                    res.Add(quizInfo);
                    await _context.QuizInfos.AddAsync(quizInfo);
                }
                await _context.SaveChangesAsync();
                return res;
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutListQuiz(QuizModify quizDTO){
            string uid = getUID(Request);
            if(uid == null){
                return SendMessage("User not found", StatusCodes.Status400BadRequest);
            } else {
                string title = quizDTO.Title;
                List<QuizInfo> listQuiz = quizDTO.listQuiz;
                bool isPublic = quizDTO.isPublic;
                var isExist = _context.QuizPackages.FirstOrDefault(quiz => quiz.PackageName == title);
                if(isExist == null){
                    return SendMessage("The package is not existed", StatusCodes.Status400BadRequest);
                }
                foreach(var quiz in listQuiz){
                    if (quiz.UserID != uid)
                    {
                        var log = new {Message = "User invalid to modify information", Quiz = quiz};
                        var mess = JsonConvert.SerializeObject(log);
                        
                        return new ObjectResult(mess){StatusCode = StatusCodes.Status401Unauthorized};
                    }

                    _context.Entry(quiz).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QuizInfoExists(quiz.QuizID))
                        {
                            var log = new {Message = "Information removed", Quiz = quiz};
                            var mess = JsonConvert.SerializeObject(log);
                            return new ObjectResult(mess){StatusCode = StatusCodes.Status409Conflict};

                        }
                        else
                        {
                            throw;
                        }
                    }
                    
                }
                return new ObjectResult(quizDTO){StatusCode = StatusCodes.Status200OK};
                    
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizInfo(int id)
        {

            if(await ValidateUser(Request)){
                var quizInfo = await _context.QuizInfos.FindAsync(id);
                if (quizInfo == null)
                {
                    return NotFound();
                }
                if(quizInfo.UserID == getUID(Request)){
                    _context.QuizInfos.Remove(quizInfo);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            return BadRequest();
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
        private ObjectResult SendMessage(string mess, int statusCode){
            var log = new {Message = mess};
            var send = JsonConvert.SerializeObject(log);
            return new ObjectResult(send){StatusCode = statusCode};
        }
    }
}
