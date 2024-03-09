using System;
using System.Data;
using System.Data.SqlClient;

namespace Pattern_Apply
{
    internal class DBConnection
    {

        // Create an instance of SqlDatabaseFactory
        private readonly SqlDatabaseStrategy sqlDatabase;
        private string connectionString;

        private Sql sql;
        private SqlAdapter sql_adapter;

        private DatabaseInteraction database;
        private static DBConnection? instance;

        private DBConnection(string DBType)
        {
            this.sqlDatabase = new SqlDatabaseStrategy();
            this.connectionString = this.sqlDatabase.CreateConnectionString();
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

        public SqlConnection GetConnection()
        {
            return (SqlConnection)database.GetConnection(this.sql_adapter);
        }
    }
}
