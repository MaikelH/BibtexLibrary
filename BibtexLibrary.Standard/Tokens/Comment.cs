using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibtexLibrary.Tokens
{
    class Comment : AbstractToken
    {
        public Comment(String value)
            : base(value)
        {
        }

        public Comment(String value, int postion)
            : base(value, postion)
        {
            {
            }
        }
    }
}
