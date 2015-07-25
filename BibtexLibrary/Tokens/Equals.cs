using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class Equals : AbstractToken
    {
        public Equals(string value) : base(value)
        {
        }

        public Equals(string value, int Position) : base(value, Position)
        {
        }
    }
}
