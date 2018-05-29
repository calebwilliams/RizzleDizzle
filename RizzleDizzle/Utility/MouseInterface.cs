using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WindowsInput;

namespace RizzleDizzle.Utility
{
    public class MouseInterface
    {
        //move this to it's own class and just pass the handle around... 
        private readonly Process _process;
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, out Point lpPoint);
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        private InputSimulator _sim;
        private Point _currentCursor;
        private Point _screenToClient; 


        public MouseInterface(string process)
        {
            _sim = new InputSimulator(); 
            try
            {
                _process = Process.GetProcessesByName(process).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                //after this is migrated to its' own service... 
                //_log.LogCritical($"Error finding {process} | ex.Message");
            }
        }

        public void Move(Point point)
        {
            SetCursorPos(point.X, point.Y); 
        }
        
        public void LeftClick()
        {
            _sim.Mouse.LeftButtonClick(); 
        }

        public void LeftClick(Point point)
        {
            Move(point);
            _sim.Mouse.LeftButtonClick(); 
        }

        public Point GetCursorPosition()
        {
            GetCursorPos(out _currentCursor);
            return _currentCursor; 
        }
    }

}
