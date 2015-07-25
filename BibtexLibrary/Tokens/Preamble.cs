using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class Preamble : AbstractToken
    {
        public Preamble(String Value) :base(Value) { }
        public Preamble(String Value, int Postion) : base(Value, Postion) { }
    }
}
