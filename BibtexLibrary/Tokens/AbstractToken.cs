using System;

namespace BibtexLibrary.Tokens
{
    public class AbstractToken
    {
        protected bool Equals(AbstractToken other)
        {
            return string.Equals(Value, other.Value) && _position == other._position;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Value != null ? Value.GetHashCode() : 0)*397) ^ _position;
            }
        }

        protected readonly string Value;
        private readonly int _position;

        public AbstractToken(String value)
        {
            Value = value;
        }

        public AbstractToken(String value, int position)
        {
            Value = value;
            _position = position;
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
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AbstractToken) obj);
        }
    }
}
