using System;
using System.Data;
using System.Data.SqlClient;

namespace Pattern_Apply
{

    public interface IDatabase
    {
        IDbConnection Connection();
    }

    public class Sql
    {
        private string connectionString;

        public Sql(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            try
            {
                Console.WriteLine(connectionString);

                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error establishing SQL connection: {ex.Message}");
                throw; // Re-throw the exception to propagate it
            }
        }
    }

    public class SqlAdapter : IDatabase
    {
        private Sql sql;

        public SqlAdapter(Sql sql)
        {
            this.sql = sql;
        }

        public IDbConnection Connection()
        {
            return sql.GetConnection();
        }
    }

    internal class DatabaseInteraction
    {
       

        public IDbConnection GetConnection(IDatabase database)
        {
            try
            {
                return database.Connection();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
                throw; // Re-throw the exception to propagate it
            }
        }
    }
}


