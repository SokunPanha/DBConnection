using System;
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Pattern_Apply
{
    public interface IDatabaseStrategy
    {
        string Execute(DatabaseSettings databaseSettings);
    }

    public class DatabaseString
    {
        private IDatabaseStrategy databaseStrategy;

        public DatabaseString()
        {
            this.databaseStrategy = null!;
        }

        public string CreateConnection(DatabaseType databaseType)
        {
            DatabaseSettings databaseSettings = ReadSettingsFromJson();
            SetDatabaseStrategy(databaseType);

            return databaseStrategy.Execute(databaseSettings);
        }

        private void SetDatabaseStrategy(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.SQL:
                    databaseStrategy = new SqlDatabaseStrategy();
                    break;
                case DatabaseType.MySQL:
                    databaseStrategy = new MySqlDatabaseStrategy();
                    break;
                default:
                    throw new NotSupportedException("Unsupported database type");
            }
        }

        private DatabaseSettings ReadSettingsFromJson()
        {
            string jsonFilePath = "E:\\01 OOAD Assignment\\Pattern_Apply\\Pattern_Apply\\config.json";
            string json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<DatabaseSettings>(json)!;
        }
    }

    public class SqlDatabaseStrategy : IDatabaseStrategy
    {
        public string Execute(DatabaseSettings databaseSettings)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = databaseSettings.SQL!.Server,
                InitialCatalog = databaseSettings.SQL.Database,
                IntegratedSecurity = databaseSettings.SQL.IntegratedSecurity
            };

            return builder.ToString();
        }
    }

    public class MySqlDatabaseStrategy : IDatabaseStrategy
    {
        public string Execute(DatabaseSettings databaseSettings)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = databaseSettings.MySQL!.Server,
                Database = databaseSettings.MySQL.Database,
                IntegratedSecurity = databaseSettings.MySQL.IntegratedSecurity
            };

            return builder.ToString();
        }
    }

    public class DatabaseSettings
    {
        public DatabaseType DatabaseType { get; set; }
        public SqlSettings? SQL { get; set; }
        public MySqlSettings? MySQL { get; set; }
    }

    public enum DatabaseType
    {
        SQL,
        MySQL
    }

    public class SqlSettings
    {
        public string? Server { get; set; }
        public string? Database { get; set; }
        public bool IntegratedSecurity { get; set; }
    }

    public class MySqlSettings
    {
        public string? Server { get; set; }
        public string? Database { get; set; }
        public bool IntegratedSecurity { get; set; }
    }

   
}
