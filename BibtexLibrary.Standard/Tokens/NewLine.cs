using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    class NewLine : AbstractToken
    {
        public NewLine(String Value) :base(Value) { }
        public NewLine(String Value, int Postion) : base(Value, Postion) { }
    }
}
