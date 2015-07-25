using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class OpeningBrace : AbstractToken
    {
        public OpeningBrace(String Value) :base(Value) { }
        public OpeningBrace(String Value, int Postion) : base(Value, Postion) { }
    }
}
