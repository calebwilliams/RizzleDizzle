using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RizzleDizzle.Interfaces
{
    public interface IRuneScapeStoryTimeService
    {
        Task InsertTestStory();
        Task TellAllStories();
    }
}
