using Microsoft.Extensions.Logging;
using RizzleDizzle.Interfaces;
using RizzleDizzle.Utility;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using System.Linq;
using PacketDotNet;

namespace RizzleDizzle
{
    public class App
    {
        private readonly ILogger<App> _log;
        private readonly IRuneScapeKeyboardService _rsks;
        private readonly IRuneScapeStoryTimeService _rssts;

        public App(ILogger<App> log, IRuneScapeKeyboardService rsks, IRuneScapeStoryTimeService rssts)
        {
            _log = log;
            _rsks = rsks;
            _rssts = rssts; 
        }

        public async Task Run()
        {
            try
            {
                string interfaceDesc = "Network adapter 'Intel(R) Ethernet Connection (2) I219-V' on local host";

                ICaptureDevice device = CaptureDeviceList.Instance.Where(x => x.Description == interfaceDesc).FirstOrDefault();
                string name = device.Name;
                string desc = device.Description; 
                

                device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(HandlePacket);
                device.Open(DeviceMode.Promiscuous, 30000);
                device.Filter = "tcp port 43594 || 43595";


                device.StartCapture();

                Console.ReadLine(); 

                device.StopCapture(); 
                device.Close(); 
                

                
            }
            catch (Exception ex)
            {

            }

        }

        private void HandlePacket(object sender, CaptureEventArgs e)
        {
            
            var data = Encoding.UTF8.GetString(e.Packet.Data);
            if (data.Contains("You"))
                Console.WriteLine(data);
        }
    }
}