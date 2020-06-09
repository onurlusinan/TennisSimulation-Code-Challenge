using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using TennisSimulation.TournamentBase;

namespace TennisSimulation.JSONManager
{
    public class JSONWriter
    {
        public JSONWriter(List<Result> ResultsList, string filePath)
        {
            RootObjectResult rootObjectWrite = new RootObjectResult(ResultsList); 
            string OutputJson = JsonConvert.SerializeObject(rootObjectWrite, Formatting.Indented); // Serialization

            File.WriteAllText(filePath + @"\\output.json", OutputJson); // Write to file
        }
    }

    public class RootObjectResult
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        public RootObjectResult(List<Result> results)
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
