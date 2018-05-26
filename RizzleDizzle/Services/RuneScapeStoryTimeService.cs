using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDBService.Interfaces;
using RizzleDizzle.Interfaces;
using RizzleDizzle.Models;

namespace RizzleDizzle.Services
{
    public class RuneScapeStoryTimeService : IRuneScapeStoryTimeService
    {
        private readonly IMongoDBService _mongo;
        private readonly IRuneScapeKeyboardService _rsks;

        public RuneScapeStoryTimeService(IRuneScapeKeyboardService rsks, IMongoDBService mongo)
        {
            _mongo = mongo;
            _rsks = rsks;
        }

        public async Task InsertTestStory()
        {
            try
            {
                StoryModel story = new StoryModel();
                story.Name = "Story of Darth Plageuis";
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
                story.Messages = msgs;
                await _mongo.Insert("runescape", "stories", story);
            }
            catch(Exception ex)
            {

            }

        }

        #region
        public async Task TellAllStories()
        {
            //random element to break up up story text 
            Random rand = new Random();
            List<StoryModel> stories = await _mongo.Fetch<StoryModel>("runescape", "stories", Builders<StoryModel>.Filter.Empty);

            foreach (var story in stories)
            {

                await _rsks.SendMessagesAsync(story.Messages);
                var waitTime = rand.Next(240000, 400000);
                await Task.Delay(waitTime);
            }
          
        }
        #endregion

    }
}
