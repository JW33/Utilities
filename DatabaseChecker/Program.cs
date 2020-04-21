using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseChecker
{
    class Program
    {
        static Logger logger = new Logger();

        static void Main(string[] args)
        {
            List<string> serverAndDatabaseList = Utility.ReadInNamesFile("names.txt");

            if(serverAndDatabaseList == null || serverAndDatabaseList.Count == 0)
            {
                logger.Log(LogType.Warning, "Server and database names list was empty", "Main()", "");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                Email email = new Email();

                foreach(string line in serverAndDatabaseList)
                {
                    string server = "";
                    string database = "";

                    try
                    {
                        string[] tempArray = line.Split(',');
                        server = tempArray[0];
                        database = tempArray[1];

                        DAL dal = new DAL(server, database);
                        DataRowCollection row = dal.GetDatabaseInfo();
                        int numberOfEmailsNotSent = dal.GetNumberOfPOOutboxItemsUnsent();

                        sb.AppendLine("---------");
                        sb.Append($"{server}");
                        sb.Append(numberOfEmailsNotSent.ToString());
                        sb.AppendLine("---------");

                        Console.Write($"{server}");
                        Console.Write(numberOfEmailsNotSent.ToString());
                        Console.WriteLine();

                        logger.Log(LogType.Info, sb.ToString(), "", "");
                        sb.Clear();
                    }
                    catch (Exception ex)
                    {
                        logger.Log(LogType.Error, $"Connection to {server} failed", "Main()", ex.ToString());
                        sb.Clear();
                    }
                }

            }

        }

    }
}
