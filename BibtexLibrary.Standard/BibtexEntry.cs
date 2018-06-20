using System.Collections.Generic;

namespace BibtexLibrary
{
    /// <summary>
    /// Single entry in Bibtex file
    /// </summary>
    public class BibtexEntry
    {
        /// <summary>
        /// The tags
        /// </summary>
        private Dictionary<string, string> _tags = new Dictionary<string, string>();
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public Dictionary<string, string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
    }
}
