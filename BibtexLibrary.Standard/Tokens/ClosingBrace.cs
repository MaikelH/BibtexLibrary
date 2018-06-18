using System;

namespace BibtexLibrary.Tokens
{
    public class ClosingBrace : AbstractToken
    {
        public ClosingBrace(String value)
            : base(value)
        {
        }

        public ClosingBrace(String value, int postion)
            : base(value, postion)
        {

        }
    }
}
