using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace server.Models {
    [Table("FavoritePackageInfo")]
    public class FavoritePackageInfo {
        [Key]
        public int InfoID {get; set;}
        [ForeignKey("User")]
        public int UserID {get; set;}
        [ForeignKey("QuizPackage")]
        public int PackageID {get; set;}
    }
}