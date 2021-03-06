﻿using System;

namespace TennisSimulation.TournamentBase
{
    public class Match
    {
        private readonly Player Player1;
        private readonly Player Player2;
        private readonly Tournament Tournament;

        private static readonly Random rnd = new Random(); // for the probability comparison
        private readonly double RandomDecimal = rnd.NextDouble(); // Get decimal value between 0 and 1, the factor of chance

        public Match(Player player1, Player player2, Tournament tour)
        {
            Player1 = player1;
            Player2 = player2;
            Tournament = tour;
        }

        public Player PlayMatch() // Play the match according to the score rules, returns winner
        {
            int Score1 = 0; 
            int Score2 = 0;
            double Probability1; // Player1's probability, no need to calculate Player2's.

            // Matching with opponent +1 score
            Score1++;
            Score2++;

            // Left hand +2 score
            if (Player1.isLeftHanded()) { Score1 += 2; }
            if (Player2.isLeftHanded()) { Score2 += 2; }

            // Total xperience more than opponent +3 score
            if (Player1.Experience > Player2.Experience) { Score1 += 3; }
            if (Player1.Experience < Player2.Experience) { Score2 += 3; }

            // surface skill more +4 score
            if (Tournament.isClay())
            { 
                if(Player1.Skills.Clay > Player2.Skills.Clay) { Score1 += 4; }
                if(Player1.Skills.Clay < Player2.Skills.Clay) { Score2 += 4; }
            }
            else if (Tournament.isGrass())
            {
                if (Player1.Skills.Grass > Player2.Skills.Grass) { Score1 += 4; }
                if (Player1.Skills.Grass < Player2.Skills.Grass) { Score2 += 4; }
            }
            else if (Tournament.isHard())
            {
                if (Player1.Skills.Hard > Player2.Skills.Hard) { Score1 += 4; }
                if (Player1.Skills.Hard < Player2.Skills.Hard) { Score2 += 4; }
            }

            Probability1 = Score1 / (Score1 + Score2); // Calculating the probability

            if (Probability1 < RandomDecimal) // Winner is Player2, since the random decimal is in Player2's range of probabilities
            {
                CalculateExperience(Player2, Player1);
                return Player2;
            }
            else if(Probability1 > RandomDecimal)
            {
                CalculateExperience(Player1, Player2);
                return Player1;
            }
            else // if Probability1 and randomDecimal are the same value, we calculate with the scores since the scores can not be equal.
            {
                return Score1 > Score2 ? Player1 : Player2;
            }
            
        }

        public void CalculateExperience(Player Winner, Player Loser) 
        {
            if (Tournament.isElimination())
            {
                Winner.GainExperience(20);
                Loser.GainExperience(10);
            }
            else if (Tournament.isLeague())
            {
                Winner.GainExperience(10);
                Loser.GainExperience(1);
            }
        }
    }
}
