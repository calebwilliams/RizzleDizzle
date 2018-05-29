using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RizzleDizzle.Utility
{
    public class SharpPcapInterface
    {
        private ICaptureDevice _device; 

        public SharpPcapInterface(string interfaceName)
        {
            var _device = CaptureDeviceList.Instance.Where(x => x.Name == interfaceName); 
        }


    }
}
