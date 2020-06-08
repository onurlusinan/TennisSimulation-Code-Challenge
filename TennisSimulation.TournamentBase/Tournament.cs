using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using TennisSimulation.PlayerBase;

namespace TennisSimulation.TournamentBase
{
    public class Tournament
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("surface")]
        public string Surface { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public List<Player> Players; // The list of players entering the tournament taken from input.json

        public Tournament(int id, string surface, string type, List<Player> players)
        {
            Id = id;
            Surface = surface;
            Type = type;
            Players = players;
        }   
    }
}
