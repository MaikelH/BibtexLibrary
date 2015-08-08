using System;
using System.IO;
using BibtexLibrary.Parser;
using BibtexLibrary.Tokenizer;

namespace BibtexLibrary
{
    public class BibtexImporter
    {
        public static BibtexFile FromString(string text)
        {
            BibtexParser file = new BibtexParser(new Tokenizer.Tokenizer(new ExpressionDictionary(), text));

            return file.Parse();
        }

        public static BibtexFile FromStream(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
