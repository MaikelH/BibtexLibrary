using System;

namespace BibtexLibrary.Parser
{
    public class ParseException : Exception
    {
        public ParseException(string s) : base(s)
        {
            

        }
    }
}