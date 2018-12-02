using System.Collections.Generic;
using System.Linq;
using Core.Db.Dao;
using Core.Db.Model;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PandaController : ControllerBase {
        private PandaDao _pandaDao;
        public PandaController(PandaDao pandaDao) {
            _pandaDao = pandaDao;
            CreateExamplePandaIfNotExist();
        }
        // GET api/panda
        [HttpGet]
        public ActionResult<List<Panda>> Get() {
            return _pandaDao.FetchAll();
        }

        // GET api/panda/{name}
        [HttpGet("{name}")]
        public ActionResult<Panda> Get(string name) {
            return _pandaDao.Fetch(name);
        }

        // POST api/panda
        [HttpPut]
        public void Put([FromBody] Panda panda) {
            _pandaDao.Add(panda);
        }

        // DELETE api/panda/{name}
        [HttpDelete("{name}")]
        public void Delete(string name) {
            _pandaDao.Delete(name);
        }

        private void CreateExamplePandaIfNotExist() {
            string examplePandaName = "Example Panda";
            Panda potentialPanda = _pandaDao.Fetch(examplePandaName);
            
            if (potentialPanda != null) {
                return;
            }
            
            _pandaDao.Add(new Panda {
                Name = examplePandaName,
                Awards = new List<string>(new []{"Coolest Panda Ever", "Plumpest Panda in Western Washington"})
            });
        }
    }
}