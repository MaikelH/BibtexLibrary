using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class At : AbstractToken
    {
            public At(String Value) :base(Value) { }

            public At(String Value, int Postion) : base(Value, Postion) { }
    }
}
