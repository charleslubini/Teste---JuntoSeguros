 
 namespace TesteJuntoSeguros.Models
{
    public class ChangePasswordModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
        public string tokenChangePassword { get; set; }
    }
}