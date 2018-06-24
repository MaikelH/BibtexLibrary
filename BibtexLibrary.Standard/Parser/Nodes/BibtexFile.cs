using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Parser.Nodes
{
    public class BibtexFile : ParseNode
    {
        private ICollection<Entry> _entries =  new List<Entry>();

        public ICollection<Entry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }
    }
}
