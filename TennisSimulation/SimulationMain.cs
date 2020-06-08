using System;
using System.Collections.Generic;
using System.IO;

using TennisSimulation.JSONManager;
using TennisSimulation.PlayerBase;
using TennisSimulation.TournamentBase;

namespace TennisSimulation
{
    class SimulationMain
    {
        private static List<Player> allPlayers;
        private static List<Tournament> allTournaments;

        static void Main(string[] args)
        {   
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")); // Project filepath for input and output

            #region Read Input File

            JSONReader reader = new JSONReader(filePath + @"\\input.json"); //reading the input.json file 

            #endregion

            #region JSON deserialization

            var deserialized = reader.Deserialize(); // deserialization to player and tournament lists
            allPlayers = deserialized.Item1;
            allTournaments = deserialized.Item2;

            #endregion

            #region Play the Tournaments

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

            #endregion

            #region Serialization to Json

            JSONWriter writer = new JSONWriter(allPlayers, filePath); // serialize the results to Json using the filepath

            #endregion
        }
    }
}
