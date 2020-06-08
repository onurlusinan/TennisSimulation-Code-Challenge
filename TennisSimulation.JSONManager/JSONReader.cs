using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using TennisSimulation.PlayerBase;
using TennisSimulation.TournamentBase;

namespace TennisSimulation.JSONManager
{
    public class JSONReader
    {
        private string JSONstr;

        public JSONReader(string Input)
        {
            this.JSONstr = File.ReadAllText(Input); // What if Input is null
        }

        public Tuple<List<Player>, List<Tournament>> Deserialize()
        {
            List<Player> allPlayers = new List<Player>();
            List<Tournament> allTournaments = new List<Tournament>();

            List<Tournament> LeagueTournaments = new List<Tournament>();
            List<Tournament> EliminationTournaments = new List<Tournament>();

            var rootObj = JsonConvert.DeserializeObject<RootObject>(JSONstr);
          
            foreach (Player p in rootObj.Players)
            {
                allPlayers.Add(p);
            }

            foreach (Tournament t in rootObj.Tournaments)
            {
                allTournaments.Add(t);
            }

            return Tuple.Create(allPlayers, allTournaments);
        }
    }

    public class RootObject
    {
        public List<Player> Players { get; set; }

        public List<Tournament> Tournaments { get; set; }
    }
}