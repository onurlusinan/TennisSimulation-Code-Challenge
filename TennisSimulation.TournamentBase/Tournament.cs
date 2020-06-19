using System.Collections.Generic;

using Newtonsoft.Json;

namespace TennisSimulation.TournamentBase
{
    public abstract class Tournament
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("surface")]
        public string Surface { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public Tournament(int id, string surface, string type)
        {
            Id = id;
            Surface = surface;
            Type = type;
        }

        public abstract void Play(List<Player> players);

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
