using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TennisSimulation.TournamentBase;

namespace TennisSimulation
{
    class SimulationMain
    {
        private static List<Player> AllPlayers;
        private static List<Tournament> AllTournaments;

        private static List<Player> SortedPlayerList = new List<Player>();
        private static List<Result> ResultsList = new List<Result>();
       
        private static string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")); // Project filepath for input and output

        static void Main(string[] args)
        {
            DeserializeJson();
            PlayTournaments();
            SortPlayerList(AllPlayers);
            SerializeToJson();
        }

        private static void DeserializeJson()
        {
            JSONManager manager = new JSONManager();
            manager.Deserialize(filePath + @"\\input.json"); // change the name of file from here if needed
            AllPlayers = manager.allPlayers; // deserialization to player and tournament lists
            AllTournaments = manager.allTournaments;
        }

        private static void PlayTournaments()
        {
            foreach (Tournament t in AllTournaments) // Play all Tournaments based on their type
            {
                Tournament tournament = null;
                if (t.isElimination())
                {
                    tournament = new Elimination(t.Id, t.Surface, t.Type);
                }
                else if (t.isLeague())
                {
                    tournament = new League(t.Id, t.Surface, t.Type);
                }
                tournament.Play(AllPlayers);
            }
        }

        private static void SortPlayerList(List<Player> players)
        {
            // sort the list based on gained experience, if equal sort by initial experience
            SortedPlayerList = players.OrderByDescending(p => p.GetGainedExperience()).ThenByDescending(p => p.InitExperience).ToList();

            int order = 1; 
            foreach (Player p in SortedPlayerList) // create a Result object for each player
            {
                Result result = new Result(order, p.Id, p.GetGainedExperience(), p.Experience);
                ResultsList.Add(result);
                order++;
            }
        }

        private static void SerializeToJson()
        {
            JSONManager manager = new JSONManager();
            manager.Serialize(ResultsList, filePath);
        }
    }
}
