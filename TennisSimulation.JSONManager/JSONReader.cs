using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using TennisSimulation.TournamentBase;

namespace TennisSimulation.JSONManager
{
    public class JSONReader
    {
        private string JSONstr;

        public List<Player> allPlayers = new List<Player>();
        public List<Tournament> allTournaments = new List<Tournament>();

        public JSONReader(string Input)
        {
            JSONstr = File.ReadAllText(Input); // What if Input is null
        }

        public void Deserialize()
        {
            InputInfo inputInfo = JsonConvert.DeserializeObject<InputInfo>(JSONstr);
            allPlayers = inputInfo.Players;
            allTournaments = inputInfo.Tournaments;
        }
    }

    public class InputInfo
    {
        public List<Player> Players { get; set; }

        public List<Tournament> Tournaments { get; set; }
    }
}