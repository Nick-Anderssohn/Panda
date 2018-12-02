using Core.Global;

namespace Core.Db {
    public class DbHelper {
        private string _connStr;

        public string ConnStr => _connStr;

        public DbHelper(EnvVars envVars) {
            _connStr = BuildConnStr(
                envVars.PostgresqlHost,
                envVars.PostgresqlUsername,
                envVars.PostgresqlPassword,
                envVars.PostgresqlDbName
            );
        }

        private static string BuildConnStr(string host, string username, string password, string dbName) {
            return $"Host={host}; Username={username}; Password={password}; Database={dbName};";
        }
    }
}