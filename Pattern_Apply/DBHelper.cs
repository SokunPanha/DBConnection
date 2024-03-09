using System;
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Pattern_Apply
{
    public abstract class  IDatabaseStrategy
    {
      protected DatabaseSettings databaseSetting;

      protected IDatabaseStrategy() { 
        this.databaseSetting = JsonToObjectConvert();
        }
      protected DatabaseSettings JsonToObjectConvert()
        {
            string json = File.ReadAllText("E:\\01 OOAD Assignment\\Pattern_Apply\\Pattern_Apply\\config.json");
            DatabaseSettings databaseSetting = JsonConvert.DeserializeObject<DatabaseSettings>(json)!;
            return databaseSetting;

        }
        public  abstract string CreateConnectionString();
    }

    public class SqlDatabaseStrategy : IDatabaseStrategy
    {
        private SqlSettings sqlSettings;

        public SqlDatabaseStrategy()
        {
            this.sqlSettings = databaseSetting.SQL!;
        }

        public override string CreateConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = sqlSettings.Server,
                InitialCatalog = sqlSettings.Database,
                IntegratedSecurity = sqlSettings.IntegratedSecurity
            };

            return builder.ToString();
        }
    }

    public class MySqlDatabaseStrategy : IDatabaseStrategy
    {
        private MySqlSettings mySqlSettings;

        public MySqlDatabaseStrategy(MySqlSettings mySqlSettings)
        {
            this.mySqlSettings = mySqlSettings;
        }

        public override string CreateConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = mySqlSettings.Server,
                Database = mySqlSettings.Database,
                IntegratedSecurity = mySqlSettings.IntegratedSecurity
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
        MySQL,  // Add MySQL database type
        // Remove MongoDB
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




