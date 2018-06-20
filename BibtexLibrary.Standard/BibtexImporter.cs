using BibtexLibrary.Parser;
using BibtexLibrary.Tokenizer;
using System;
using System.IO;

namespace BibtexLibrary
{
    /// <summary>
    /// Parser 
    /// </summary>
    public class BibtexImporter
    {


        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static BibtexFile FromString(string text)
        {
            BibtexParser file = new BibtexParser(new Tokenizer.Tokenizer(new ExpressionDictionary(), text));

            return file.Parse();
        }

        /// <summary>
        /// Froms the stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static BibtexFile FromStream(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
