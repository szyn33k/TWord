﻿namespace TWord
{
    [LanguageTransformerAttribute(Language.Polish)]
    internal class PolishNumberTransformerFactory : INumberTransformerFactory
    {
        public INumberTransformer Create()
        {
            return GetDefaultBuilder().Build();
        }

        public NumberTransformerBuilder GetDefaultBuilder()
        {
            var languageNumbersDictionary = new PolishNumbersDictionary();
            var largeNumberNamesDictionary = new PolishLargeNumberNamesDictionary();

            var triplerTransformer = new GenericTripletTransformer(languageNumbersDictionary);
            var nounInflector = new PolishNounInflector();

            return new NumberTransformerBuilder()
                .SetNumbersDictionary(languageNumbersDictionary)
                .SetLargeNumberNamesDictionary(largeNumberNamesDictionary)
                .SetTriplerTransformer(triplerTransformer)
                .InflectNounsBy(nounInflector);
        }
    }
}