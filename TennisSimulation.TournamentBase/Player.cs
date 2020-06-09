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

        public int InitExperience; // initial experience value

        public Skills Skills; // [clay, grass, hard] :: each value between 1...10

        public Player(int id, string hand, int experience, Skills skills)
        {
            this.Id = id;
            this.Hand = hand;
            this.Experience = experience;
            this.InitExperience = experience;
            this.Skills = skills;
        }

        public void GainExperience(int XP)
        {
            this.Experience += XP;
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
