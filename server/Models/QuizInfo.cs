using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace server.Models {
    [Table("QuizInfo")]
    public class QuizInfo {
        [ForeignKey("User")]
        public string UserID {get; set;}
        [Key]
        public int QuizID {get; set;}
        public string Question {get; set;}
        public string Answer {get; set;}
        [ForeignKey("QuizPackage")]
        public int PackageID {get; set;}
        [JsonIgnore]
        public QuizPackage QuizPackage {get; set;}
    }
}