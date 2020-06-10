using System;
using System.Collections.Generic;

using TennisSimulation.Utilities;

namespace TennisSimulation.TournamentBase
{
    public class League : Tournament
    {
        private List<Match> MatchList = new List<Match>();
        private List<int> DistinctRandomList = new List<int>();

        public League(int id, string surface, string type, List<Player> players) : base(id, surface, type, players)
        {
            Play(players);
        }

        private void Play(List<Player> players)
        {
            GetCombinations(players);
            DistinctRandomList = RandomGenerator.GenerateRandomList(MatchList.Count, MatchList.Count);

            for (int i = 0; i < MatchList.Count; i++)
            { 
                int rand = DistinctRandomList[i]; // get distinct random numbers for the indices of MatchList
                MatchList[rand].PlayMatch();
            }
        }

        private void GetCombinations(List<Player> AllPlayers) // create all distinct combinations of players
        {
            for (int i = 0; i < AllPlayers.Count; i++) // The number of matches has to be (# of Players) * (# of Players -1) / 2
            {
                for (int a = i+1; a < AllPlayers.Count; a++)
                {
                    Match match = new Match(AllPlayers[i], AllPlayers[a], this); 
                    MatchList.Add(match);
                }
            }
        }
    }
}
