using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserApi.UserApiModels;

namespace UserApi.UserApIDbAccess
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
