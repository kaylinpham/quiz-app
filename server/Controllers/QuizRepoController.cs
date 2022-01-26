using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizRepoController {
        private readonly QuizContext _context;
        private MyTokenHandler _tokenHandler {get;}
        public QuizRepoController(QuizContext context, MyTokenHandler tokenHandler){
            _context = context;
            _tokenHandler = tokenHandler;
        }
        
    }
}