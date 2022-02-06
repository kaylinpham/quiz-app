using System.Collections.Generic;
namespace server.Models.DTO {
    public class QuizPackageDTO {
        public string Title {get; set;}
        public bool isPublic {get; set;}
        public List<QuizInfoDTO> listQuiz {get; set;}
    }
}