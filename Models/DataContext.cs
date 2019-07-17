using GoodConvo.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodConvo.Models
{
    public class JournalContext : DbContext
    {
        public JournalContext(DbContextOptions<JournalContext> options)
            : base(options)
        { }

        public DbSet<UserData> UserData { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Coach> Coaches { get; set; }
    }

}
