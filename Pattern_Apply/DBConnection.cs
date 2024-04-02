using System;
using System.Data;
using System.Data.SqlClient;

namespace Pattern_Apply
{
    internal class DBConnection
    {
        private static DBConnection instance;
        private DatabaseString databaseString;
        private string connectionString;
        private Sql sql;
        private SqlAdapter sql_adapter;
        private DatabaseInteraction database;

        private DBConnection(DatabaseType DBType)
        {
            this.databaseString = new DatabaseString();
            this.connectionString = this.databaseString.CreateConnection(DBType);
            this.sql = new Sql(connectionString);
            this.sql_adapter = new SqlAdapter(this.sql);
            this.database = new DatabaseInteraction();
        }

        public static DBConnection GetInstance(DatabaseType DBType)
        {
            if (instance == null)
            {
                instance = new DBConnection(DBType);
            }
            return instance;
        }

        public DataTable Query(string query)
        {
            DataTable dataTable = new DataTable();
            IDbConnection connection = database.GetConnection(sql_adapter);
            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = query;

                // Execute the query and fill the DataTable with the results
                using (IDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dataTable;
        }
    }
}
