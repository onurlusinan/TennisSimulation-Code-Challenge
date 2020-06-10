using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using TennisSimulation.TournamentBase;

namespace TennisSimulation
{
    public class JSONManager
    {
        private string JSONstr;

        public List<Player> allPlayers = new List<Player>();
        public List<Tournament> allTournaments = new List<Tournament>();

        public JSONManager(){}

        public void Deserialize(string Input)
        { 
            JSONstr = File.ReadAllText(Input);
            InputInfo inputInfo = JsonConvert.DeserializeObject<InputInfo>(JSONstr); // Deserialization
            allPlayers = inputInfo.Players;
            allTournaments = inputInfo.Tournaments;
        }

        public void Serialize(List<Result> ResultsList, string filePath)
        {
            OutputInfo outputInfo = new OutputInfo(ResultsList);
            string OutputJson = JsonConvert.SerializeObject(outputInfo, Formatting.Indented); // Serialization

            File.WriteAllText(filePath + @"\\output.json", OutputJson); // Write to file
        }
    }

    public class InputInfo // Modelled class for the deserialization of input.json file format
    {
        public List<Player> Players { get; set; }

        public List<Tournament> Tournaments { get; set; }
    }

    public class OutputInfo // Modelled classes for the output.json file format
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        public OutputInfo(List<Result> results)
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