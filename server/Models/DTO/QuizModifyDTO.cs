using System.Collections.Generic;
namespace server.Models.DTO {
    public class QuizModify {
        public string Title {get; set;}
        public bool isPublic {get; set;}
        public List<QuizInfo> listQuiz {get; set;}
    }
}