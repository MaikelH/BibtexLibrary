using System;

namespace LexicalAnalyzer
{
    public class MatchException : Exception
    {
        private readonly string _character;
        private readonly int _position;

        public MatchException(String character, int position)
        {
            _character = character;
            _position = position;

        }

        public override string Message
        {
            get
            {
                return "BibTexLibrary: Could not match character: " + _character + " at position " + _position;
            }
        }
    }
}