using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class ClosingBrace : AbstractToken
    {
        public ClosingBrace(String Value) : base(Value)
        {
            this.Value = Value.Trim();
        }

        public ClosingBrace(String Value, int Postion) : base(Value, Postion)
        {
            this.Value = Value.Trim();
        }
    }
}
