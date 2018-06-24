using System;

namespace BibtexLibrary.Tokens
{
    public class Text : AbstractToken
    {
        public Text(String value)
            : base(value)
        {
        }

        public Text(String value, int postion)
            : base(value, postion)
        {

        }
    }
}
