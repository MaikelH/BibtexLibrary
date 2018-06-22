using System;
using System.Text;

namespace LexicalAnalyzer
{
    public class MatchException : Exception
    {
        private readonly string _character;
        private readonly int _position;

        public const Int32 windowWidth = 100;
        public Int32 lineNumber { get; set; } = 0;
        public String line { get; set; } = "";
        public String window { get; set; } = "";
        /// <summary>
        /// The whole input
        /// </summary>
        public String input = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchException"/> class.
        /// </summary>
        /// <param name="_input">The input.</param>
        /// <param name="character">The character.</param>
        /// <param name="position">The position.</param>
        public MatchException(String _input, String character, int position)
        {
            _character = character;
            input = _input;
            Int32 windowStart = Math.Max(position - windowWidth, 0);
            Int32 windowEnd = _input.IndexOf(Environment.NewLine, position);

            var inputLines = input.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
            Int32 head = 0;

            while (head < position)
            {
                lineNumber++;
                line = inputLines[lineNumber];
                head += line.Length;
            }
            window = input.Substring(windowStart, windowEnd);

            _position = position;

        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("BibTexLibrary: Could not match character: " + _character + " at position " + _position + " - ln:" + lineNumber);
                sb.AppendLine("> Line: [" + line + "]");
                sb.AppendLine("> Window: [" + window + "]");
                return sb.ToString();
            }
        }
    }
}