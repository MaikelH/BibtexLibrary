using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class ValueQuote : AbstractToken
    {
        public ValueQuote(string value) : base(value)
        {
        }

        public ValueQuote(string value, int position) : base(value, position)
        {
        }
    }
}
