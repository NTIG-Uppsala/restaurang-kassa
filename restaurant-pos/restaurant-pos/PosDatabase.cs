using Microsoft.Data.Sqlite;
using System.Data;
using System.Diagnostics;

namespace Restaurant_pos_program
{
    public class Database
    {
        public string username { get; set; }
        public string path { get; set; }
        public string filename { get; set; }
        public string fullpath { get; set; }
        public string connectionString { get; set; }
        public Database(string filename = "database.db")
        {
            this.username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[1];
            this.path = string.Format(@"C:\Users\{0}\Documents\restaurant-database", username);
            this.filename = filename;
            this.fullpath = path + @"\" + this.filename;

            connectionString = new SqliteConnectionStringBuilder()
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                DataSource = fullpath
            }.ToString();

            // Create directory if it doesn't exist when creating new object instance
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        // SELECT * FROM Products WHERE id = ? AND price > ? , {"1", "123123"}

        // SELECT * FROM Products WHERE id = @product_id AND price > @product_max_price , {"1", "123123"}

        // Method to be used to get values from table
        private DataTable QueryDataGetter(string query, List<string[]> queryParameters = null) 
        { 
            DataTable datatable = new DataTable();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = query;

                try
                {
                    if (queryParameters != null)
                    {
                        foreach (string[] param in queryParameters)
                        {
                            string paramName = param[0];
                            string paramValue = param[1];

                            var p = new SqliteParameter(paramName,  paramValue);
                            command.Parameters.Add(p);
                        }
                    }
                }
                catch(Exception ex) 
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw new Exception(ex.Message);
                }

                // Execute Query and load into datatable
                using (var reader = command.ExecuteReader())
                {
                    datatable.Load(reader);
                }

                connection.Close();
            }
            return datatable; 
        }

        public List<Product> GetProducts()
        {

            List<Product> OutputList = new();
            
            string Query =
            @"
                SELECT p.id, p.price, p.name, p.description, t.value
                FROM Products p 
	                INNER JOIN Tax t ON ( t.id = p.taxID )
            ";
            
            DataTable databaseResult = QueryDataGetter(Query);
                
            foreach (DataRow row in databaseResult.Rows)
            {
                Int64 id = (Int64)row["id"];
                decimal price = Convert.ToDecimal(row["price"]);
                string name = (string)row["name"];
                string description = (string)row["description"];
                decimal tax = Convert.ToDecimal(row["value"]);

                Product product = new Product(id, name, description, price, tax);

                OutputList.Add(product);

            }
                    
            return OutputList;
        }
    }


}
