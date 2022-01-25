using System;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace server.Models {
    [Table("User")]
    public class User {
        [Key]
        public string UID {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
    }
}