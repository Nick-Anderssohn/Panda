using System.Collections.Generic;
using System.Linq;
using Core.Db.Model;

namespace Core.Db.Dao {
    public class PandaDao {
        private DbHelper _dbHelper;

        public PandaDao(DbHelper dbHelper) {
            _dbHelper = dbHelper;
        }

        public Panda Fetch(string name) {
            using (var ctx = new PandaContext(_dbHelper)) {
                return ctx.Pandas.SingleOrDefault(p => p.Name == name);
            }
        }

        public List<Panda> FetchAll() {
            using (var ctx = new PandaContext(_dbHelper)) {
                return ctx.Pandas.ToList();
            }
        }

        public void Add(Panda panda) {
            using (var ctx = new PandaContext(_dbHelper)) {
                ctx.Pandas.Add(panda);
                ctx.SaveChanges();
            }
        }

        public void Delete(string name) {
            using (var ctx = new PandaContext(_dbHelper)) {
                var panda = ctx.Pandas.Single(p => p.Name == name);
                ctx.Remove(panda);
                ctx.SaveChanges();
            }
        }
    }
}