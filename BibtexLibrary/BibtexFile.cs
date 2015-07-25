using System.Collections.Generic;

namespace BibtexLibrary
{
    public class BibtexFile
    {
        private ICollection<BibtexEntry> _entries = new List<BibtexEntry>();
        public Dictionary<string, string> StringDefinitions { get; set; }

        public ICollection<string> Preamble { get; set; }

        public ICollection<BibtexEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }
    }
}
