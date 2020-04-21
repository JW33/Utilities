using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskInfo
{
    public class DiskInfoEntity
    {
        public string Description { get; set; }
        public string Server { get; set; }

        public string Name { get; set; }

        public string DeviceId { get; set; }
        public long FreeSpace { get; set; }

        public long Size { get; set; }

    }
}
