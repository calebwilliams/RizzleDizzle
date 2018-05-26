using Microsoft.Extensions.Logging;
using RizzleDizzle.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

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
                await _rssts.TellAllStories(); 

               
                //await _rsks.SendMessagesAsync(msgs);
            }
            catch (Exception ex)
            {

            }

        }
    }
}