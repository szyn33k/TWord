﻿namespace TWord
{
    internal class EnglishNounInflector : INounInflector
    {
        public string InflectNounByNumber(long number, Noun noun)
        {
            return InflectNounByNumber(number, 0, noun);
        }

        public string InflectNounByNumber(long number, int tripletIndex, Noun noun)
        {
            var units = number % 10;

            if (number == 1)
            {
                return noun.Singular;
            }

            if (units >= 2 && units <= 4)
            {
                return noun.Plural;
            }

            return noun.GenitivePlural;
        }        
    }
}