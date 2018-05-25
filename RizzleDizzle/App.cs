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

        public App(ILogger<App> log, IRuneScapeKeyboardService rsks)
        {
            _log = log;
            _rsks = rsks; 
        }

        public async Task Run()
        {
            try
            {
                List<string> msgs = new List<string>();
                msgs.Add("Did you ever hear the tragedy of Darth Plagueis The Wise?");
                msgs.Add("I thought not. It’s not a story the Jedi would tell you.");
                msgs.Add("It’s a Sith legend.");
                msgs.Add("Darth Plagueis was a Dark Lord of the Sith,");
                msgs.Add("so powerful and so wise he could use the Force");
                msgs.Add("to influence the midichlorians to create life…");
                msgs.Add("He had such a knowledge of the dark side that");
                msgs.Add("he could even keep the ones he cared about from dying."); 
                msgs.Add("The dark side of the Force is a pathway");
                msgs.Add("to many abilities some consider to be unnatural.");
                msgs.Add("He became so powerful… the only thing he was afraid of was losing his power,");
                msgs.Add("which eventually, of course, he did.");
                msgs.Add("Unfortunately, he taught his apprentice everything he knew,");
                msgs.Add("then his apprentice killed him in his sleep.");
                msgs.Add("Ironic.");
                msgs.Add("He could save others from death, but not himself.");

               
                await _rsks.SendMessagesAsync(msgs);
            }
            catch (Exception ex)
            {

            }

        }
    }
}