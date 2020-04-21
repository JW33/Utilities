using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskInfo
{
    public class Utility
    {
        static Logger logger = new Logger();
        private static readonly Email email = new Email();

        public static List<string> ReadInNamesFile(string filePath)
        {
            try
            {
                List<string> namesFile = new List<string>();

                using(StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        namesFile.Add(line);
                    }
                }
                return namesFile;
            }
            catch(Exception ex)
            {
                logger.Log(LogType.Error, "", "ReadInNamesFile()", "");
                return new List<string>();
            }
        }

        public static bool IsServerProduction(string server)
        {
            try
            {
                List<string> productionServers = new List<string>(ConfigurationManager.AppSettings["prodServers"].Split(new char[] { ',' }));
                return productionServers.Any(s => s.Contains(server));
            }
            catch (Exception ex)
            {
                logger.Log(LogType.Error, "", "IsServerProduction()", ex.ToString());
                return false;
            }
        }

        public static void CalculateServerDiskSpace(DiskInfoEntity entity)
        {
            try
            {
                decimal dValueFreeSpace = entity.FreeSpace;
                while (Math.Round(dValueFreeSpace, 1) >= 1000)
                {
                    dValueFreeSpace /= 1024;
                }

                decimal dValueSizeSpace = entity.Size;
                while (Math.Round(dValueSizeSpace, 1) >= 1000)
                {
                    dValueSizeSpace /= 1024;
                }

                decimal dValueUsedSpace = dValueSizeSpace - dValueFreeSpace;
                decimal usedSpaceTemp = 0;
                if (dValueSizeSpace > 0)
                    usedSpaceTemp = dValueUsedSpace / dValueSizeSpace;

                decimal usedSpacePercentage = Math.Round(usedSpaceTemp * 100, 1);

                StringBuilder sb = new StringBuilder();
                sb.Append($" - Server: {entity.Server.PadRight(15)}");
                sb.Append($"{("Device ID : ").PadRight(5)}");
                sb.Append($"{entity.DeviceId.PadRight(10)}");
                sb.Append($"{("Total Space: ").PadRight(5)}");
                sb.Append($"{(Math.Round(dValueSizeSpace, 1) + "GB ").PadRight(15)}");
                sb.Append($"{("Used Space: ").PadRight(5)}");
                sb.Append($"{(Math.Round(dValueUsedSpace, 1) + "GB ").PadRight(15)}");
                sb.Append($"{("Free Space: ").PadRight(5)}");
                sb.Append($"{(Math.Round(dValueFreeSpace, 1) + "GB ").PadRight(15)}");
                sb.Append($"{(int)usedSpacePercentage + "% of capacity in use"}");

                if (IsDiskSpaceAtThreshold((int)usedSpacePercentage))
                {
                    sb.Append(" <---- Over threshold");
                    //email.SendEmail(EmailType.Alert, $"{server} has {(int)usedSpacePercentage}% usage on the {deviceId} drive");
                }
                sb.Append($"\n");

                Console.WriteLine("------------------------------");
                Console.Write("Device ID".PadRight(20));
                Console.WriteLine(entity.DeviceId);
                Console.Write("Total Space:".PadRight(20));
                Console.WriteLine(Math.Round(dValueSizeSpace, 1) + "GB");
                Console.Write("Used Space:".PadRight(20));
                Console.WriteLine(Math.Round(dValueUsedSpace, 1) + "GB");
                Console.Write("Free Space:".PadRight(20));
                Console.WriteLine((Math.Round(dValueFreeSpace, 1) + "GB").PadRight(20));
                Console.WriteLine((int)usedSpacePercentage + "% of capacity in use");
                Console.WriteLine("------------------------------");
                Console.WriteLine();

                logger.Log(LogType.Info, $"{sb.ToString()}", "CalculateServerDiskSpace()", "");
            }
            catch (Exception ex)
            {
                logger.Log(LogType.Error, "", "CalculateServerDiskSpace()", ex.ToString());
                email.SendEmail(EmailType.Alert, "Error in CalculateServerDiskSpace() method");
            }
        }
        public static bool IsDiskSpaceAtThreshold(int percentage)
        {
            try
            {
                int threshold = 90; // hardcode in case the config setting is incorrect or not set
                bool result = int.TryParse(ConfigurationManager.AppSettings["diskThreshold"], out threshold);
                return threshold <= percentage;
            }
            catch(Exception ex)
            {
                logger.Log(LogType.Error, "", "IsDiskSpaceAtThreshold()", ex.ToString());
                return false;
            }
        }
    }
}
