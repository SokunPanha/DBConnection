using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Pattern_Apply
{
    public enum DatabaseType
    {
        SQL,
        MongoDB,
        // Add other database types as needed
    }
    public interface IDatabaseFactory
    {
        string CreateConnectionString();
    }

    public class SqlDatabase: IDatabaseFactory
    {
        private SqlSettings sqlSettings;
        public SqlDatabase(SqlSettings sqlSettings) { 
            this.sqlSettings = sqlSettings;
        }
        public string CreateConnectionString()
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
    public class SqlDatabaseFactory
    {
        private string DatabaseType;
        public SqlDatabaseFactory(string databaseType)
        {
            DatabaseType = databaseType;
        }

        public string GetDatabaseObject()
        {
            string json = File.ReadAllText("E:\\Pattern_Apply\\Pattern_Apply\\config.json");
            DatabaseSettings databaseSettings = JsonConvert.DeserializeObject<DatabaseSettings>(json)!;
          
            switch (DatabaseType.ToLower())
            {
                case "sql":

                     SqlDatabase connectionString = new SqlDatabase(databaseSettings.SQL!);
                    return connectionString.CreateConnectionString();
                case "moongo" :
                    throw new NotImplementedException("MongoDB is not implemented yet");
                // Add more cases for other database types as needed
                default:
                    throw new ArgumentException("Unsupported database type");
            }
        }

      

        private string CreateMongoDbConnectionString(MongoSettings mongoSettings)
        {
            // Logic to create MongoDB connection string
            // ...

            return "MongoDBConnectionString";  // Placeholder, replace with actual logic
        }
    }

    public class DatabaseSettings
    {
        public DatabaseType DatabaseType { get; set; }
        public SqlSettings? SQL { get; set; }
        public MongoSettings? MongoDB { get; set; }
        // Add properties for other database types if needed
    }

    public class SqlSettings
    {
        public string? Server { get; set; }
        public string? Database { get; set; }
        public bool IntegratedSecurity { get; set; }
    }

    public class MongoSettings
    {
        // Add properties specific to MongoDB
    }


    internal class DatabaseHelper
    {

    }
}
