using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RizzleDizzle.Interfaces
{
    public interface IRuneScapeKeyboardService
    {
        Task SendMessageAsync(string msg);
        Task SendMessagesAsync(List<string> msg);
    }
}
