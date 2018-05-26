using Microsoft.Extensions.Options;
using RizzleDizzle.Config.Settings;
using RizzleDizzle.Interfaces;
using RizzleDizzle.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RizzleDizzle.Services
{
    public class RuneScapeKeyboardService : IRuneScapeKeyboardService
    {
        private readonly KeyboardInterface _keyboard;
        private readonly RuneScapeSettings _config;

        public RuneScapeKeyboardService(IOptions<RuneScapeSettings> config)
        {
            _config = config.Value;
            _keyboard = new KeyboardInterface(_config.ProcessName);
        }

        public async Task SendMessageAsync(string msg)
        {
            List<string> msgQueue = new List<string>();
            CancellationToken cancellation = new CancellationToken();

            Random rand = new Random(); 
            for (int i = 0; i <= msg.Length / 80; i++)
            {
                var delay = Task.Run(async () =>
                {
                    await _keyboard.SendKeysAsync(msg.Substring((i * 80), (msg.Length - (i * 80))), cancellation);
                    var waitTime = rand.Next(5000, 10000); 
                    await Task.Delay(waitTime);
                });
                await delay;
            }
        }

        public async Task SendMessagesAsync(List<string> msgs)
        {
            foreach(var message in msgs)
                await SendMessageAsync(message); 
        }
    }
}
