using System;
using System.Collections.Generic;
using BibtexLibrary.Tokens;

namespace BibtexLibrary.Tokenizer
{
    public class ExpressionDictionary : Dictionary<Type, String>
    {
        public ExpressionDictionary()
        {
            Init();
        }

        private void Init()
        {
            Add(typeof(At), "^(\\s)*@");
            Add(typeof(Preamble), "(\\s)*Preamble");
            Add(typeof(NewLine), "^(\\s)*$");
            Add(typeof(OpeningBrace), "^(\\s)*{");
            Add(typeof(ClosingBrace), "^(\\s)*}");
            Add(typeof(Equals), "^\\s*=");
            Add(typeof(Text), "^\\s*[\\w\\d:\\.\\s-;]+");
            Add(typeof(Comma), "^\\s*,");
        }
    }
}
