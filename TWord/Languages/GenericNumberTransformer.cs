﻿using System;
using System.Collections;
using System.Linq;

namespace TWord
{
    internal class GenericNumberTransformer : INumberTransformer
    {
        private readonly ILanguageNumbersDictionary _numbersDictionary;
        private readonly ITriplerTransformer _triplerTransformer;
        private readonly ILargeNumberNamesDictionary _largeNumberNamesDictionary;
        private readonly INounInflector _nounInflector;

        private readonly string _separator;

        public GenericNumberTransformer(
            ILanguageNumbersDictionary numbersDictionary,
            ITriplerTransformer triplerTransformer,
            ILargeNumberNamesDictionary largeNumberNamesDictionary,
            INounInflector nounInflector,
            string separator = " ")
        {
            _numbersDictionary = numbersDictionary;
            _triplerTransformer = triplerTransformer;
            _largeNumberNamesDictionary = largeNumberNamesDictionary;
            _nounInflector = nounInflector;

            _separator = separator;
        }

        public string ToWords(long value)
        {
            if (value == 0)
                return _numbersDictionary.Zero;

            ArrayList words = new ArrayList();

            if (value < 0)
            {
                words.Add(_numbersDictionary.Minus);
                value = Math.Abs(value);
            }

            words.AddRange(GetWordsBySplitNumberToTriples(value));

            return string.Join(_separator, words.OfType<string>()).Trim();
        }

        private ArrayList GetWordsBySplitNumberToTriples(long number)
        {
            ArrayList words = new ArrayList();

            var triples = number.ToTriples();

            for (var tripleIndex = 0; tripleIndex < triples.Length; tripleIndex++)
            {
                var triple = triples[tripleIndex];
                var phrase = _triplerTransformer.ToPhrase(triple);

                var noun = _largeNumberNamesDictionary.GetLargeNumberNoun(tripleIndex);

                if(noun != Noun.Empty)
                {
                    var largeNumberName = noun.Singular;

                    if (_nounInflector != null)
                    {
                        largeNumberName = _nounInflector.InflectNounByNumber(triple,
                            noun.Singular,
                            noun.Plural,
                            noun.GenitivePlural);
                    }

                    words.Add(largeNumberName);
                }                

                words.Add(phrase);
            }

            words.Reverse();

            return words;
        }
    }
}