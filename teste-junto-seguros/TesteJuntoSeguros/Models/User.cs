using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteJuntoSeguros.Models
{
    [Table("User")]    
    public class UserModel
    {
        public long id {get; set;}
        public string email {get; set;}
        public string name {get; set;}
        public string  username {get; set;}
        public string password {get; set;}
        public string tokenChangePassword {get; set;}
        public string phone {get; set;}
        public string city {get; set;}
    }
}

