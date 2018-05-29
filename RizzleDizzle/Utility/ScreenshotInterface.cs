using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RizzleDizzle.Utility
{
    public class ScreenshotInterface
    {
        //move this to it's own class and just pass the handle around... 
        private readonly Process _process; 
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);


        private Rectangle _window;

        public ScreenshotInterface(string process)
        {
            _process = Process.GetProcessesByName(process).FirstOrDefault();
            GetWindowRect(_process.MainWindowHandle, out _window); 

        }

        //public Bitmap TakeScreenshot()
        //{
            
        //}
    }
}
