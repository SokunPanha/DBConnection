using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pattern_Apply
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }

    public interface ICrud<T> where T : class
    {
        void create(T item);
        void update(int Id);
    }

    public class ProductStrategy : ICrud<Product>
    {
        private DBConnection DBinstance;
        private SqlConnection con;
        public ProductStrategy()
        {
            this.DBinstance = DBConnection.GetInstance("sql");
            this.con = DBinstance.GetConnection();
        }
        public void create(Product product)
        {

            try
            {
                this.con.Open();
                string insertQuery = "INSERT INTO tblBook(bookCode, bookTitle, bookAuthor) VALUES (@code, @Name, @Age)";

                // Create a new SqlCommand with the query and connection
                using (SqlCommand cmd = new SqlCommand(insertQuery, this.con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@code", product.Id);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Age", product.AuthorName);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data inserted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No rows affected. Insertion failed.");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection after use
                con.Close();

            }

        }
    
        public void update(int Id)
        {
            Console.WriteLine($"Product ID: {Id}");
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CustomerStrategy : ICrud<Customer>
    {
        public void create(Customer customer)
        {
            Console.WriteLine("Customer Name: " + customer.Name);
        }

        public void update(int Id)
        {
            Console.WriteLine($"Customer ID: {Id}");
        }
    }

    public class CrudService<T> where T : class
    {


        private ICrud<T> crud;

        public CrudService(ICrud<T> crud)
        {
            this.crud = crud;
        }

        public void Create(T item)
        {
            this.crud.create(item);
        }

        // Other methods can be added here if needed
    }

}
