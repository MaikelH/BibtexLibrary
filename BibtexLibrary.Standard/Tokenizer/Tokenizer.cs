using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using BibtexLibrary.Tokens;
using LexicalAnalyzer;

namespace BibtexLibrary.Tokenizer
{
    public class Tokenizer
    {
        private readonly ExpressionDictionary _dictionary;
        private readonly string _input;
        private int _counter;

        public Tokenizer(ExpressionDictionary dictionary, string input)
        {
            _dictionary = dictionary;
            _input = input;
        }

        public AbstractToken NextToken()
        {

            // Loop through all tokens and check if they match the input string
            foreach (KeyValuePair<Type, string> pair in _dictionary)
            {
                Match match;

                if (pair.Key == typeof (Comment))
                {
                    match = Regex.Match(_input.Substring(_counter), pair.Value, RegexOptions.Multiline);
                }
                else
                {
                    // TODO: See if substring does not impose a to harsh performance drop
                    match = Regex.Match(_input.Substring(_counter), pair.Value);
                }

                if (!match.Success)
                {
                    continue;
                }
                _counter += match.Value.Length;

                if (!pair.Key.IsSubclassOf(typeof (AbstractToken)))
                {
                    continue;
                }
                    
                // Create new instance of the specified type with the found value as parameter
                AbstractToken token = (AbstractToken)Activator.CreateInstance(pair.Key, new object[] { match.Value, _counter - match.Value.Length }, null);

                return token;
            }

            throw new MatchException(_input[_counter].ToString(CultureInfo.InvariantCulture), _counter);
        }

        public AbstractToken Peek()
        {
            // Loop through all tokens and check if they match the input string
            foreach (KeyValuePair<Type, string> pair in _dictionary)
            {
                var test = _input.Length;
                // TODO: See if substring does not impose a to harsh performance drop
                Match match = Regex.Match(_input.Substring(_counter), pair.Value);

                if (match.Success)
                {
                    if (pair.Key.IsSubclassOf(typeof(AbstractToken)))
                    {
                        // Create new instance of the specified type with the found value as parameter
                        AbstractToken token = (AbstractToken)Activator.CreateInstance(pair.Key, new object[] { match.Value, _counter }, null);

                        return token;
                    }

                }
            }

            throw new MatchException(_input[_counter].ToString(CultureInfo.InvariantCulture), _counter);     
        }

        /// <summary>
        /// Return all the tokens in the inputstring.
        /// </summary>
        /// <returns>List of tokens</returns>
        public ICollection<AbstractToken> GetAllTokens()
        {
            List<AbstractToken> tokens = new List<AbstractToken>();

            while (!EndOfInput)
            {
                tokens.Add(NextToken());
            }

            return tokens;
        }

        /// <summary>
        /// Checks whether the scanner is at the end of the input.
        /// </summary>
        public bool EndOfInput
        {
            get { return (_counter >= (_input.Length)); }
        }

        /// <summary>
        /// Returns the previous n characters. 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Previous characters in the tokenizer</returns>
        public string GetPreviousCharacters(int n)
        {
            return _input.Substring(_counter - n, n);
        }


    }
}
