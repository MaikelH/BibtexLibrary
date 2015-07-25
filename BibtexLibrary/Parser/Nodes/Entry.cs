using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexLibrary.Parser.Nodes
{
    public class Entry : ParseNode
    {
        public String Type { get; set; }

        public String Key { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
