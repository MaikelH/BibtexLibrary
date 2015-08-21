using System.IO;
using System.Linq;
using BibtexLibrary;
using BibtexLibrary.Parser;
using BibtexLibrary.Tokenizer;
using NUnit.Framework;

namespace BibtexImporter.Tests
{
    [TestFixture]
    class ParserTest
    {
        [Test]
        public void SimpleParserText()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {David A. Aaker}
                                                                            }");
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(1, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("David A. Aaker", file.Entries.First().Tags.First().Value);
        }

        [Test]
        public void MultipleTagsTest()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {David A. Aaker},
                                                                                title = {Multivariate statistics}
                                                                            }");
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(2, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("David A. Aaker", file.Entries.First().Tags.First().Value);
            Assert.AreEqual("title", file.Entries.First().Tags.ToList()[1].Key);
            Assert.AreEqual("Multivariate statistics", file.Entries.First().Tags.ToList()[1].Value);
        }

        [Test]
        public void BracesInValueTest()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {David A. ()ker},
                                                                            }");
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(1, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("David A. ()ker", file.Entries.First().Tags.First().Value);
        }

        [Test]
        public void MultipleEntriesTest()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {David A. Aaker},
                                                                                title = {Multivariate statistics}
                                                                            }
                                                                            @book{ baker:1912,
                                                                                author = {David A. Baker},
                                                                                title = {Multivariate statistics 2}
                                                                            }");
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(2, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(2, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("David A. Aaker", file.Entries.First().Tags.First().Value);
            Assert.AreEqual("title", file.Entries.First().Tags.ToList()[1].Key);
            Assert.AreEqual("Multivariate statistics", file.Entries.First().Tags.ToList()[1].Value);

            Assert.AreEqual("baker:1912", file.Entries.ToList()[1].Key);
            Assert.AreEqual("book", file.Entries.ToList()[1].Type);
            Assert.AreEqual(2, file.Entries.ToList()[1].Tags.Count);
            Assert.AreEqual("author", file.Entries.ToList()[1].Tags.First().Key);
            Assert.AreEqual("David A. Baker", file.Entries.ToList()[1].Tags.First().Value);
            Assert.AreEqual("title", file.Entries.ToList()[1].Tags.ToList()[1].Key);
            Assert.AreEqual("Multivariate statistics 2", file.Entries.ToList()[1].Tags.ToList()[1].Value);
        }


        [Test]
        public void CommaInTagValueParseTest()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {Günther, C.W. and Van Der Aalst, W.M.P.}
                                                                            }");
            
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(1, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("Günther, C.W. and Van Der Aalst, W.M.P.", file.Entries.First().Tags.First().Value);
        }

        [Test]
        public void FuzzyMiningTestFileTest()
        {
            using (StreamReader reader = new StreamReader("Test Files\\Fuzzy Mining.bib"))
            {
                Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), reader.ReadToEnd());
                BibtexParser parser = new BibtexParser(tokenizer);
                BibtexFile file = parser.Parse();

                Assert.AreEqual(3, file.Entries.Count);
            }
        }

        [Test]
        public void SimpleParserWithDoubleQuoteTest()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = ""David A. Aaker""
                                                                              }");
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(1, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("David A. Aaker", file.Entries.First().Tags.First().Value);
        }

        [Test]
        public void ParseBracesInQuotedStringTest()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = ""David {A.} Aaker""
                                                                              }");
            BibtexParser parser = new BibtexParser(tokenizer);
            BibtexFile file = parser.Parse();

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual("aaker:1912", file.Entries.First().Key);
            Assert.AreEqual("book", file.Entries.First().Type);
            Assert.AreEqual(1, file.Entries.First().Tags.Count);
            Assert.AreEqual("author", file.Entries.First().Tags.First().Key);
            Assert.AreEqual("David {A.} Aaker", file.Entries.First().Tags.First().Value);
        }
    }
}
