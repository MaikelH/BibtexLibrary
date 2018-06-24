using System.Collections.Generic;

namespace BibtexLibrary
{
    /// <summary>
    /// Collection of Bibtex entries
    /// </summary>
    public class BibtexFile
    {
        private ICollection<BibtexEntry> _entries = new List<BibtexEntry>();
        private Dictionary<string, string> _stringDefinitions = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the string definitions.
        /// </summary>
        /// <value>
        /// The string definitions.
        /// </value>
        public Dictionary<string, string> StringDefinitions
        {
            get { return _stringDefinitions; }
            set { _stringDefinitions = value; }
        }

        /// <summary>
        /// Gets or sets the preamble.
        /// </summary>
        /// <value>
        /// The preamble.
        /// </value>
        public ICollection<string> Preamble { get; set; }

        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        /// <value>
        /// The entries.
        /// </value>
        public ICollection<BibtexEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }
    }
}
