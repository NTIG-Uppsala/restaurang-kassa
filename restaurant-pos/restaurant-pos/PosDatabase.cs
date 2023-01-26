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


            // Will be used if the database initialy doesn't exist
            string[] tableQueries = new string[]
            {
                "CREATE TABLE \"Allergies\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"allergy\"\tTEXT NOT NULL,\r\n\tPRIMARY KEY(\"id\")\r\n)",
                "CREATE TABLE \"Booker\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"firstName\"\tTEXT NOT NULL,\r\n\t\"lastName\"\tTEXT NOT NULL,\r\n\t\"email\"\tTEXT,\r\n\t\"phone\"\tTEXT NOT NULL,\r\n\tPRIMARY KEY(\"id\" AUTOINCREMENT)\r\n)",
                "CREATE TABLE \"Bookings\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"bookerID\"\tINTEGER NOT NULL,\r\n\t\"tableNumberID\"\tINTEGER NOT NULL,\r\n\t\"bookingDate\"\tTEXT NOT NULL,\r\n\t\"amountOfPeople\"\tINTEGER NOT NULL,\r\n\t\"paymentID\"\tINTEGER NOT NULL,\r\n\tFOREIGN KEY(\"bookerID\") REFERENCES \"Booker\"(\"id\"),\r\n\tFOREIGN KEY(\"tableNumberID\") REFERENCES \"TableNumber\"(\"tableNumberID\"),\r\n\tPRIMARY KEY(\"id\" AUTOINCREMENT)\r\n)",
                "CREATE TABLE \"Payment\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"paymentTypeID\"\tINTEGER NOT NULL,\r\n\t\"datePaid\"\tTEXT,\r\n\t\"amount\"\tNUMERIC NOT NULL,\r\n\t\"isPaid\"\tNUMERIC NOT NULL,\r\n\t\"bookingID\"\tINTEGER NOT NULL,\r\n\tFOREIGN KEY(\"paymentTypeID\") REFERENCES \"PaymentTypes\"(\"id\"),\r\n\tFOREIGN KEY(\"bookingID\") REFERENCES \"Bookings\"(\"id\"),\r\n\tPRIMARY KEY(\"id\")\r\n)",
                "CREATE TABLE \"PaymentTypes\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"type\"\tTEXT NOT NULL,\r\n\tPRIMARY KEY(\"id\" AUTOINCREMENT)\r\n)",
                "CREATE TABLE \"ProductReceipt\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"receiptID\"\tINTEGER NOT NULL,\r\n\t\"productID\"\tINTEGER NOT NULL,\r\n\tFOREIGN KEY(\"receiptID\") REFERENCES \"Receipts\"(\"id\"),\r\n\tFOREIGN KEY(\"productID\") REFERENCES \"Products\"(\"id\"),\r\n\tPRIMARY KEY(\"id\")\r\n)",
                "CREATE TABLE \"Products\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"price\"\tINTEGER NOT NULL,\r\n\t\"name\"\tTEXT NOT NULL,\r\n\t\"description\"\tTEXT,\r\n\t\"taxID\"\tINTEGER NOT NULL,\r\n\tFOREIGN KEY(\"taxID\") REFERENCES \"Tax\"(\"id\"),\r\n\tPRIMARY KEY(\"id\")\r\n)",
                "CREATE TABLE \"Receipts\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"paymentID\"\tINTEGER NOT NULL,\r\n\t\"content\"\tTEXT,\r\n\tFOREIGN KEY(\"paymentID\") REFERENCES \"Payment\"(\"id\"),\r\n\tPRIMARY KEY(\"id\")\r\n)",
                "CREATE TABLE \"TableNumber\" (\r\n\t\"tableNumberID\"\tINTEGER NOT NULL UNIQUE,\r\n\tPRIMARY KEY(\"tableNumberID\" AUTOINCREMENT)\r\n)",
                "CREATE TABLE \"Tax\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"value\"\tNUMERIC NOT NULL,\r\n\tPRIMARY KEY(\"id\" AUTOINCREMENT)\r\n)",
                "CREATE TABLE \"productAllergies\" (\r\n\t\"id\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"productID\"\tINTEGER NOT NULL,\r\n\t\"allergyID\"\tINTEGER NOT NULL,\r\n\tFOREIGN KEY(\"allergyID\") REFERENCES \"Allergies\"(\"id\"),\r\n\tFOREIGN KEY(\"productID\") REFERENCES \"Products\"(\"id\"),\r\n\tPRIMARY KEY(\"id\")\r\n)"
            };

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




        /*
            // Method to be used to get values from table
            query: SELECT * FROM Products WHERE id = @product_id AND price > @product_max_price
            queryParameters: Dictionary<string, object> = {
                "@product_id": "value",
                "@product_max_price": "10",
            }
            
            
         
         */

        public DataTable QueryDataGetter(string query, Dictionary<string, object> queryParameters = null) 
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
                        foreach (KeyValuePair<string, object> param in queryParameters)
                        {
                            var parameter = new SqliteParameter(param.Key,  param.Value);
                            command.Parameters.Add(parameter);
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

        public int QueryDataSetter(string query, Dictionary<string, object> ?queryParameters = null)
        {
            int result = 0;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = query;

                try
                {
                    if (queryParameters != null)
                    {
                        foreach (KeyValuePair<string, object> param in queryParameters)
                        {
                            var parameter = new SqliteParameter(param.Key, param.Value);
                            command.Parameters.Add(parameter);
                        }
                    }
                }
                catch (Exception exc)
                {
                    System.Diagnostics.Debug.WriteLine(exc.Message);
                    throw new Exception(exc.Message);
                }

                try
                {
                    result = command.ExecuteNonQuery();

                }
                catch(Exception exc)
                {
                    System.Diagnostics.Debug.WriteLine(exc.Message);
                    result = -1;
                }

                connection.Close();
            }
            return result;
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
