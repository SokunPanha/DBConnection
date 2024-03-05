using System;
using System.Data;

namespace Pattern_Apply
{
    internal class DBConnection
    {

        // Create an instance of SqlDatabaseFactory
        private readonly SqlDatabaseFactory sqlfactory;
        // Use the IDatabaseFactory to create a connection string
        private string connectionString;

        private Sql sql;
        private SqlAdapter sql_adapter;
        private DatabaseInteraction database;
        private static DBConnection? instance;

        private DBConnection(string DBType)
        {
            this.sqlfactory = new SqlDatabaseFactory(DBType!);
            this.connectionString = this.sqlfactory.GetDatabaseObject();
            this.sql = new Sql(connectionString);
            this.sql_adapter = new SqlAdapter(this.sql);
            this.database = new DatabaseInteraction();
        }

        public static DBConnection GetInstance(string DBType)
        {
           
                if (instance == null)
                {
                    instance = new DBConnection(DBType);
                }
                return instance;
            
        }

        public IDbConnection GetConnection()
        {
            return database.GetConnection(this.sql_adapter);
        }
    }
}
