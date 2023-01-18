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
