using Microsoft.Extensions.Configuration;

namespace Core.Global {
    /// <summary>
    /// Contains environment variables grabbed from IConfiguration.
    ///
    /// When adding an environment variable, be sure to add both a getter
    /// for the key and a getter for the value (grabbed from _configuration).
    /// Any environment variables prefixed with "PANDA_" will automatically
    /// be accessible from IConfiguration. Keys are static so that they can
    /// be accessed before DI is available.
    /// </summary>
    public class EnvVars {
        public static string AppNameKey => "PANDA_APPNAME";
        public string AppName => _configuration[AppNameKey];
        
        public static string ElasticsearchLogUrlKey => "PANDA_ELASTICSEARCH_LOG_URL";
        public string ElasticsearchLogUrl => _configuration[ElasticsearchLogUrlKey];
        
        // Database configuration 
        public static string PostgresqlHostKey => "PANDA_POSTGRESQL_HOST";
        public string PostgresqlHost => _configuration[PostgresqlHostKey];
        public static string PostgresqlUsernameKey => "PANDA_POSTGRESQL_USERNAME";
        public string PostgresqlUsername => _configuration[PostgresqlUsernameKey];
        public static string PostgresqlPasswordKey => "PANDA_POSTGRESQL_PASSWORD";
        public string PostgresqlPassword => _configuration[PostgresqlPasswordKey];
        public static string PostgresqlDbNameKey => "PANDA_DB_NAME";
        public string PostgresqlDbName => _configuration[PostgresqlDbNameKey];

        private IConfiguration _configuration;
        
        public EnvVars(IConfiguration configuration) {
            _configuration = configuration;
        }
    }
}