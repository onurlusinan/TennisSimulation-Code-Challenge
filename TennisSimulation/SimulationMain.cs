using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TennisSimulation.JSONManager;
using TennisSimulation.TournamentBase;

namespace TennisSimulation
{
    class SimulationMain
    {
        private static List<Player> allPlayers;
        private static List<Tournament> allTournaments;

        private static List<Player> SortedPlayerList = new List<Player>();
        private static List<Result> ResultsList = new List<Result>();
       
        private static string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")); // Project filepath for input and output

        static void Main(string[] args)
        {
            DeserializeJson();
            PlayTournaments();
            SortPlayerList(allPlayers);
            SerializeToJson();
        }

        private static void DeserializeJson()
        {
            JSONReader reader = new JSONReader(filePath + @"\\input.json"); //reading the input.json file 
            var deserialized = reader.Deserialize(); // deserialization to player and tournament lists
            allPlayers = deserialized.Item1;
            allTournaments = deserialized.Item2;
        }

        private static void PlayTournaments()
        {
            foreach (Tournament t in allTournaments) // Play all Tournaments based on their type
            {
                if (t.Type == "elimination")
                {
                    Elimination elimination = new Elimination(t.Id, t.Surface, t.Type, allPlayers);
                }
                else if (t.Type == "league")
                {
                    League league = new League(t.Id, t.Surface, t.Type, allPlayers);
                }
            }
        }

        private static void SortPlayerList(List<Player> players)
        {
            int order = 1; // sort the list based on gained experience, if both equal sort by initial experience
            SortedPlayerList = players.OrderByDescending(p => p.Experience).ThenByDescending(p => p.InitExperience).ToList();

            foreach (Player p in SortedPlayerList) // create a Result object for each player
            {
                Result result = new Result(order, p.Id, p.Experience - p.InitExperience, p.Experience);
                ResultsList.Add(result);
                order++;
            }
        }

        private static void SerializeToJson()
        {
            JSONWriter writer = new JSONWriter(ResultsList, filePath);
        }
    }
}
