using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace RizzleDizzle.Utility
{
    public class KeyboardInterface
    {
        private readonly Process _process;
        private InputSimulator _input;
        //It's probably okay to set the foreground window here, I'm not entirely sure though...
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool SetFocus(IntPtr hWnd);
        //this is a lot more work than I'm willing to do for a simple bot... nuget package it is.
        //[DllImport("user32.dll")]
        //static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);


        public KeyboardInterface(string process)
        {
            _input = new InputSimulator();
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

        public Task SendKeysAsync(string msg, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                SetForegroundWindow(_process.MainWindowHandle);
                SetFocus(_process.MainWindowHandle);

                for (int i = 0; i < msg.Count(); i++)
                {
                    _input.Keyboard.TextEntry(msg[i]);
                }
                _input.Keyboard.KeyDown(VirtualKeyCode.RETURN);
            }, cancellationToken);
        }
    }
}
