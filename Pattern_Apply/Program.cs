using System.Data;
using System.Data.SqlClient;

namespace Pattern_Apply
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create instances of Product and Customer
            Product newProduct = new Product { Id = 10, Name = "New Laptop", AuthorName = "Jack" };
            Customer newCustomer = new Customer { Id = 1, Name = "John Doe" };

            // Create instances of ProductStrategy and CustomerStrategy
            ProductStrategy productStrategy = new ProductStrategy();
            CustomerStrategy customerStrategy = new CustomerStrategy();

            // Create CrudService instances for Product and Customer
            CrudService<Product> productCrudService = new CrudService<Product>(productStrategy);
            CrudService<Customer> customerCrudService = new CrudService<Customer>(customerStrategy);

            // Use CrudService to perform CRUD operations
            productCrudService.Create(newProduct);
            customerCrudService.Create(newCustomer);

            /*DBConnection DBinstance = DBConnection.GetInstance("sql");
            IDbConnection con = DBinstance.GetConnection();
            try
            {
                // Open the connection
                con.Open();
                Console.WriteLine($"Connection State: {con.State}");

                // Example query
                string query = "SELECT * FROM tblBook";  // Replace YourTableName with the actual table name

                // Create a command
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;

                    // Execute the query
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Process the results
                            Console.WriteLine(reader.GetString(2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Close the connection
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

*/
        }

    
    }
}
