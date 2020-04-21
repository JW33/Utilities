using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseChecker
{
    public static class Utility
    {
        static Logger logger = new Logger();

        public static List<string> ReadInNamesFile(string filePath)
        {
            try
            {
                List<string> namesList = new List<string>();

                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string line;
                    while((line = streamReader.ReadLine()) != null)
                    {
                        namesList.Add(line);
                    }
                }
                return namesList;
            }
            catch(Exception ex)
            {
                logger.Log(LogType.Error, "", "ReadInNamesFile()", ex.ToString());
                return new List<string>();
            }
        }
        public static bool IsSizeAtThreshold(decimal usage)
        {
            decimal threshold = Convert.ToDecimal(ConfigurationManager.AppSettings["threshold"].ToString());

            if (usage >= threshold)
                return true;

            return false;
        }
    }
}
