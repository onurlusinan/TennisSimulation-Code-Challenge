using System;
using System.Collections.Generic;

namespace TennisSimulation.TournamentBase
{
    public class Elimination : Tournament
    {
        private List<Match> MatchList = new List<Match>();
        private List<Player> tempList = new List<Player>(); // create a temp list for the recursive model

        public Elimination(int id, string surface, string type, List<Player> players) : base(id, surface, type, players)
        {
            Play(players);
        }

        private void Play(List<Player> players) 
        {
            MatchList = CreatePlayerMatches(players); // Create the first matches
            tempList.Clear(); // clear the temp list

            foreach (var Match in MatchList) 
            {
                tempList.Add(Match.PlayMatch());
            }

            if (tempList.Count == 1) // Winner!     
            {
                // Do nothing, only winner left
            }
            else 
            {
                Play(tempList); // recursively update eliminationList
            }
        }

        private List<Match> CreatePlayerMatches(List<Player> players)
        {
            var MatchList = new List<Match>();
            for (int i = 0; i<players.Count; i += 2) // Normally, this does not support # of players that is not 2^n. 
            {                                               // However according to the case document, we can ignore this.
                var match = new Match(players[i], players[i + 1], this);
                MatchList.Add(match); 
            }
            return MatchList;
        }
    }
}
