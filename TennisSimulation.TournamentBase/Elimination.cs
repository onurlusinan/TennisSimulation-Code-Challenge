using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TennisSimulation.PlayerBase;

namespace TennisSimulation.TournamentBase
{
    public class Elimination : Tournament
    {
        private List<Tuple<Player, Player>> EliminationList = new List<Tuple<Player, Player>>();
        private List<Player> tempList = new List<Player>(); // create a temp list for the recursive model

        public Elimination(int id, string surface, string type, List<Player> players) : base(id, surface, type, players)
        {
            PlayElimination(players);
        }

        public Player PlayElimination(List<Player> players) // returns the winning player
        {
            EliminationList = CreatePlayerDuos(players); // Create the duos
            tempList.Clear(); // clear the temp list

            foreach (Tuple<Player, Player> PlayerDuo in EliminationList) 
            {
                Match EliminationMatch = new Match(PlayerDuo.Item1, PlayerDuo.Item2, this);
                tempList.Add(EliminationMatch.PlayMatch());
            }

            if (tempList.Count == 1)
            {
                return tempList[0]; // Winner!      
            }
            else 
            {
                return PlayElimination(tempList); // recursively update eliminationList
            }
        }

        public List<Tuple<Player, Player>> CreatePlayerDuos(List<Player> players)
        {
            List<Tuple<Player, Player>> eliminationList = new List<Tuple<Player, Player>>();
            for (int i = 0; i<players.Count; i += 2) // Normally, this does not support # of players that is not 2^n. 
            {
                eliminationList.Add(Tuple.Create(players[i], players[i + 1])); // However according to the case document, we can ignore this.
            }
            return eliminationList;
        }
    }
}
