using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class Comma : AbstractToken
    {
        public Comma(string value) : base(value)
        {
        }

        public Comma(string value, int position) : base(value, position)
        {
        }
    }
}
