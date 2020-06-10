using System.Collections.Generic;

using Newtonsoft.Json;

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

        private readonly List<Player> Players; // The list of players entering the tournament taken from input.json

        public Tournament(int id, string surface, string type, List<Player> players)
        {
            Id = id;
            Surface = surface;
            Type = type;
            Players = players;
        }

        public bool isElimination()
        {
            return Type == "elimination";
        }

        public bool isLeague()
        {
            return Type == "league";
        }
        public bool isClay()
        {
            return Surface == "clay";
        }
        public bool isGrass()
        {
            return Surface == "grass";
        }
        public bool isHard()
        {
            return Surface == "hard";
        }
    }
}
