using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace server.Models {
    [Table("QuizPackage")]
    public class QuizPackage {
        [Key]
        public int PackageID {set; get;}
        public string PackageName {set; get;}
        [ForeignKey("User")]
        public string UserID {set; get;}
        public bool isPublic {get; set;}
        [JsonIgnore]
        public List<QuizInfo> Quizzes {get; set;}

    }
}