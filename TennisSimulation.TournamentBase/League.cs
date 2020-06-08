using System;
using System.Collections.Generic;

using TennisSimulation.PlayerBase;
using TennisSimulation.Utilities;

namespace TennisSimulation.TournamentBase
{
    public class League : Tournament
    {
        private List<Tuple<Player, Player>> CombinationsList = new List<Tuple<Player, Player>>();
        private List<int> DistinctRandomList = new List<int>();

        public League(int id, string surface, string type, List<Player> players) : base(id, surface, type, players)
        {
            GetCombinations(players);
            DistinctRandomList = RandomGenerator.GenerateRandom(CombinationsList.Count, CombinationsList.Count);

            for (int i = 0; i < CombinationsList.Count; i++)
            { 
                int rand = DistinctRandomList[i]; // get distinct random numbers for the index of CombinationsList
                Match LeagueMatch = new Match(CombinationsList[rand].Item1, CombinationsList[rand].Item2, this);
                LeagueMatch.PlayMatch();
            }
        }

        public void GetCombinations(List<Player> AllPlayers) // create all distinct combinations of players
        {
            for (int i = 0; i < AllPlayers.Count; i++) // The number of matches has to be (# of Players) * (# of Players -1) / 2
            {
                for (int a = i+1; a < AllPlayers.Count; a++)
                {
                    CombinationsList.Add(Tuple.Create(AllPlayers[i], AllPlayers[a]));
                }
            }
        }
    }
}
