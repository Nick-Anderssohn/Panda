using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Core.Db.Model {
    public class PandaContext : DbContext {
        public DbSet<Panda> Pandas { get; set; }

        private DbHelper _dbHelper;

        public PandaContext(DbHelper dbHelper) {
            _dbHelper = dbHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(_dbHelper.ConnStr);
        }
    }

    public class Panda {
        [Key]
        public string Name { get; set; }
        public List<string> Awards { get; set; }
    }
}