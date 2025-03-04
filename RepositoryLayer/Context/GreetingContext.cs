using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class GreetingContext : DbContext
    {
        public GreetingContext(DbContextOptions<GreetingContext> options) : base(options) { }
        public DbSet<Entity.GreetingEntity> Greetings { get; set; }
    }
}
