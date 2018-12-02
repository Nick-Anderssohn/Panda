using Core.Global;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Core.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AppNameController {
        private EnvVars _envVars;
        
        // EnvVars is setup for DI. See Startup#ConfigureServices
        public AppNameController(EnvVars envVars) {
            _envVars = envVars;
        }

        [HttpGet]
        public ActionResult<string> Get() {
            Log.Information("App name requested.");
            return _envVars.AppName;
        }
    }
}