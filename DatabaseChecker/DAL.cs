using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseChecker
{
    class DAL
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }

        static Logger logger = new Logger();

        public DAL(string server, string database)
        {
            Server = server;
            Database = database;

            Username = ConfigurationManager.AppSettings["username"].ToString();
            Password = ConfigurationManager.AppSettings["password"].ToString();
            ConnectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        }

        public DataRowCollection GetDatabaseInfo()
        {
            DataRowCollection row = null;
            string connectionString = $"Data Source={Server};Initial Catalog={Database};Integraterd Security=True;User Id={Username};Password={Password}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    DataSet dataset = new DataSet();
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = new SqlCommand("sp_helpdb", connection);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddWithValue("@dbname", Database);
                        connection.Open();
                        adapter.Fill(dataset);

                        row = dataset.Tables[1].Rows;
                        return row;
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogType.Error, "", "GetDatabaseInfo()", ex.ToString());
                    return row;
                }
            }
        }

        public int GetNumberOfPOOutboxItemsUnsent()
        {
            int numberOfRows = -1;
            string connectionString = $"Data Source={Server};Initial Catalog={Database};Integraterd Security=True;User Id={Username};Password={Password}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    
                    using (SqlCommand command = new SqlCommand("GetUnsentPOOuboxItems", connection))
                    {
                        connection.Open();
                        numberOfRows = (int)command.ExecuteScalar();
                    }
                    return numberOfRows;
                }
                catch(Exception ex)
                {
                    logger.Log(LogType.Error, "", "GetDatabaseInfo()", ex.ToString());
                    return numberOfRows;
                }
            }
        }


    }
}
