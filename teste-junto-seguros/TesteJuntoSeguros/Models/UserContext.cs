using Microsoft.EntityFrameworkCore;

namespace TesteJuntoSeguros.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<UserModel> users{ get; set; } 
    }
}