using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Tokens
{
    public class AbstractToken
    {
        protected string Value;
        private int _position;

        public AbstractToken(String value)
        {
            Value = value;
        }

        public AbstractToken(String value, int Position)
        {
            Value = value;
            _position = Position;
        }

        public String GetValue()
        {
            return Value;
        }

        public int Position
        {
            get { return _position; }
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            AbstractToken token = (AbstractToken) obj;

            return (token.GetValue() == GetValue()) && (token.Position == Position);
        }
    }
}
