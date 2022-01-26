using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace server.Models {
    [Table("QuizPackage")]
    public class QuizPackage {
        [Required]
        public int PackageID {set; get;}
        public string PackageName {set; get;}
        [ForeignKey("User")]
        public string UserID {set; get;}
        public bool isFavorite {get; set;}

    }
}