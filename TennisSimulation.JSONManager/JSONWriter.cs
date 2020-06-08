using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;
using TennisSimulation.PlayerBase;

namespace TennisSimulation.JSONManager
{
    public class JSONWriter
    {
        List<Result> SortedResultsList = new List<Result>();
        private readonly string FilePath;

        public JSONWriter(List<Player> players, string filePath)
        {
            int order = 1;
            FilePath = filePath;

            // sort the list based on gained experience, if both equal sort by initial experience
            List<Player> SortedList = players.OrderByDescending(p => p.Experience).ThenByDescending(p => p.init_experience).ToList(); 

            foreach (Player p in SortedList) // create a Result object for each player
            {
                Result result = new Result(order, p.Id, p.Experience - p.init_experience, p.Experience);
                SortedResultsList.Add(result);
                order++;
            }

            RootObjectWrite rootObjectWrite = new RootObjectWrite(SortedResultsList); // Serialization
            string OutputJson = JsonConvert.SerializeObject(rootObjectWrite, Formatting.Indented);

            File.WriteAllText(filePath + @"\\output.json", OutputJson); // Write to file
        }
    }

    public class RootObjectWrite
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        public RootObjectWrite(List<Result> results)
        {
            Results = results;
        }
    }

    public class Result
    {
        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("gained_experience")]
        public int GainedExperience { get; set; }

        [JsonProperty("total_experience")]
        public int TotalExperience { get; set; }

        public Result(int order, int playerId, int gainedExperience, int totalExperience)
        {
            Order = order;
            PlayerId = playerId;
            GainedExperience = gainedExperience;
            TotalExperience = totalExperience;
        }
    }
}
