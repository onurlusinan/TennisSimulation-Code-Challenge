using Newtonsoft.Json;

namespace TennisSimulation.TournamentBase
{
    public class Player
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("hand")]
        public string Hand; // left or right

        [JsonProperty("experience")]
        public int Experience; // total experience value

        public int InitExperience; // initial and gained experience values
        public int GainedExperience;

        public Skills Skills; // [clay, grass, hard] :: each value between 1...10

        public Player(int id, string hand, int experience, Skills skills)
        {
            Id = id;
            Hand = hand;
            Experience = experience;
            InitExperience = experience;
            Skills = skills;
        }

        public void GainExperience(int XP)
        {
            Experience += XP;
        }

        public int GetGainedExperience()
        {
            return Experience - InitExperience;
        }

        public bool isLeftHanded()
        {
            return Hand == "left";
        }
    }

    public class Skills
    {
        [JsonProperty("clay")]
        public int Clay { get; set; }

        [JsonProperty("grass")]
        public int Grass { get; set; }

        [JsonProperty("hard")]
        public int Hard { get; set; }
    }
}
