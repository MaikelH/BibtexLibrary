using System.Collections.Generic;

namespace BibtexLibrary
{
    public class BibtexEntry
    {
        private Dictionary<string, string> _tags = new Dictionary<string, string>();
        public string Type { get; set; }

        public string Key { get; set; }

        public Dictionary<string, string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
    }
}
