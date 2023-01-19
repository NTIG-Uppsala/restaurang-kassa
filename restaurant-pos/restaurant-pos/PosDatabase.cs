using Microsoft.Data.Sqlite;
using System.Data;

namespace Restaurant_pos_program
{
    public class Database
    {
        private string username { get; set; }
        private string path { get; set; }
        private string filename { get; set; }
        private string fullpath { get; set; }
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
        // INSERT INTO Receipts VALUES (?, ?, ?, ?,), {"value 1", "value 2", "value3"...}

        // Method to be used to get values from table
        private DataTable QueryDataGetter(string query, string[] queryParameters = null) 
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
                        foreach (string param in queryParameters)
                        {
                            var p = new SqliteParameter();
                            p.Value = param;

                            command.Parameters.Add(p);
                        }
                    }
                }
                catch(Exception ex) 
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw new Exception(ex.Message);
                    return new DataTable();
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
            // SELECT * FROM Products;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT p.id, p.price, p.name, p.description, t.value
                    FROM Products p 
	                    INNER JOIN Tax t ON ( t.id = p.taxID  )  
                ";

                using (var reader = command.ExecuteReader())
                {
                    using (DataTable datatable = new())
                    {
                        datatable.Load(reader);
                        foreach (DataRow row in datatable.Rows)
                        {
                            Int64 id = (Int64)row["id"];
                            decimal price = Convert.ToDecimal(row["price"]);
                            string name = (string)row["name"];
                            string description = (string)row["description"];
                            decimal tax = Convert.ToDecimal(row["value"]);

                            Product product = new Product(id, name, description, price, tax);

                            OutputList.Add(product);

                        }
                    }
                }
                connection.Close();
            }
            return OutputList;
        }

    }


}
