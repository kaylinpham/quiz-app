using System.Collections.Generic;
namespace server.Models.DTO {
    public class QuizPublicDTO {
        public string Title { get; set; }
        public List<QuizInfo> ListQuiz { get; set; }
    }
}