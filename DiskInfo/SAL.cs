using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskInfo
{
    public class SAL
    {
        static Logger logger = new Logger();
        public ManagementObjectCollection GetServiceInfo()
        {
            ManagementObjectCollection moc = null;

            try
            {
                ManagementScope scope = new ManagementScope($"\\root\\cimv2");

                scope.Connect();

                ObjectQuery query = new ObjectQuery("SELECT Description, DeviceID, FreeSpace, Name, Size, SystemName FROM WIN32_LogicalDisk");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                moc = searcher.Get();

                return moc;
            }
            catch(Exception ex)
            {
                logger.Log(LogType.Error, "", "GetServiceInfo()", ex.ToString());
                return moc;
            }
        }
    }
}
