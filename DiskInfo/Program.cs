using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskInfo
{
    class Program
    {
        static Logger logger = new Logger();
        static void Main(string[] args)
        {
            try
            {
                SAL sal = new SAL();

                ManagementObjectCollection moc = sal.GetServiceInfo();

                foreach(ManagementObject mo in moc)
                {
                    if (mo["Description"].ToString() != "Local Fixed Disk")
                        continue; // skip CD drives

                    DiskInfoEntity entity = new DiskInfoEntity
                    {
                        Description = mo["Description"].ToString(),
                        Server = mo["SystemName"].ToString(),
                        Name = mo["Name"].ToString(),
                        DeviceId = mo["DeviceID"].ToString(),
                        FreeSpace = Convert.ToInt64(mo["FreeSpace"]),
                        Size = Convert.ToInt64(mo["Size"])
                    };

                    Utility.CalculateServerDiskSpace(entity);
                }
                logger.Log(LogType.Info, "Run successfully completed", "", "");
            }
            catch(Exception ex)
            {
                logger.Log(LogType.Error, "", "Main()", ex.ToString());
            }
        }
    }
}
