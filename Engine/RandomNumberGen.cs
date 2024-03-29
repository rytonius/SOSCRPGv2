﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Engine.ViewModels;

namespace Engine
{
    public static class RandomNumberGen
    {
        private static RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];
            _rng.GetBytes(randomNumber);
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
            // We are using Math.Max, and substracting 0.00000000001,
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;
            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }

        public static double CurrencyBetween(double minimumValue, double maximumValue)
        {
            byte[] randomNumber = new byte[1];
            _rng.GetBytes(randomNumber);
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
            // We are using Math.Max, and substracting 0.00000000001,
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255.0d) - 0.00000000001d);
            // We need to add one to the range, to allow for the rounding done with Math.Floor
            double range = maximumValue - minimumValue + 1;
            double randomValueInRange = Math.Floor(multiplier * range);

            return (double)(minimumValue + randomValueInRange);
        }

        public static int DiceRollDamageCalculator(int dice, int roll, int bonusDamage)
        {
            int TotalDamage = 0;
            GameSession GS = new GameSession();

            for (int i = 0; i < dice; i++)
            {
                TotalDamage += NumberBetween(1, roll);
            }

            return TotalDamage + bonusDamage;
        }
        
    }

}

    

