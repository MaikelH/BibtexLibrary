using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BibtexLibrary.Tokenizer;
using BibtexLibrary.Tokens;
using NUnit.Framework;
using Text = BibtexLibrary.Tokens.Text;

namespace BibtexImporter.Tests
{
    [TestFixture]
    public class TokenizerTest
    {
        [Test]
        public void TestTokenizer1()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), "test");

            Assert.AreEqual(new BibtexLibrary.Tokens.Text("test"),tokenizer.NextToken());
        }

        [Test]
        public void TestTokenizerSimple()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), "@book{ }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(4, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new ClosingBrace(" }" ,6), tokens[3]);
        }

        [Test]
        public void TestTokenizerKey()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), "@book{ aaker:1912, }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(6, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new Text(" aaker:1912", 6), tokens[3]);
            Assert.AreEqual(new Comma(",", 17), tokens[4]);
            Assert.AreEqual(new ClosingBrace(" }", 18), tokens[5]);
        }

        [Test]
        public void TestTokenizerTags()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {David A. Aaker}
                                                                            }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(11, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new Text("aaker:1912", 6), tokens[3]);
            Assert.AreEqual(new Comma(",", 17), tokens[4]);
            Assert.AreEqual(new Text("author", 18), tokens[5]);
            Assert.AreEqual(new Equals("=", 107), tokens[6]);
            Assert.AreEqual(new OpeningBrace(" {", 108), tokens[7]);
            Assert.AreEqual(new Text("David A. Aaker", 110), tokens[8]);
            Assert.AreEqual(new ClosingBrace("}", 124), tokens[9]);
            Assert.AreEqual(new ClosingBrace("}", 125), tokens[10]);
        }

        [Test]
        public void TestTokenizerMultipleTags()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {David A. Aaker},
                                                                                title = {Multivariate Analysis in Marketing}
                                                                            }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(17, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new Text("aaker:1912", 6), tokens[3]);
            Assert.AreEqual(new Comma(",", 17), tokens[4]);
            
            Assert.AreEqual(new Text("author", 18), tokens[5]);
            Assert.AreEqual(new Equals("=", 107), tokens[6]);
            Assert.AreEqual(new OpeningBrace(" {", 108), tokens[7]);
            Assert.AreEqual(new Text("David A. Aaker", 110), tokens[8]);
            Assert.AreEqual(new ClosingBrace("}", 124), tokens[9]);
            Assert.AreEqual(new Comma(",", 125), tokens[10]);

            Assert.AreEqual(new Text("title", 126), tokens[11]);
            Assert.AreEqual(new Equals("=", 214), tokens[12]);
            Assert.AreEqual(new OpeningBrace(" {", 215), tokens[13]);
            Assert.AreEqual(new Text("Multivariate Analysis in Marketing", 217), tokens[14]);
            Assert.AreEqual(new ClosingBrace("}", 251), tokens[15]);

            Assert.AreEqual(new ClosingBrace("}", 252), tokens[16]);
        }

        [Test]
        public void TestCommaInTagValue()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = {Günther, C.W. and Van Der Aalst, W.M.P.}
                                                                            }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(9, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new Text("aaker:1912", 6), tokens[3]);
            Assert.AreEqual(new Comma(",", 17), tokens[4]);

            Assert.AreEqual(new Text("author", 18), tokens[5]);
            Assert.AreEqual(new Equals("=", 107), tokens[6]);
            Assert.AreEqual(new OpeningBrace(" {", 108), tokens[7]);
            Assert.AreEqual(new Text("Günther, C.W. and Van Der Aalst, W.M.P.", 110), tokens[8]);
            Assert.AreEqual(new ClosingBrace("}", 124), tokens[9]);

            Assert.AreEqual(new ClosingBrace("}", 252), tokens[16]);
        }

        [Test]
        public void TestHyphenInTagValue()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = { test-test }
                                                                            }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(11, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new Text("aaker:1912", 6), tokens[3]);
            Assert.AreEqual(new Comma(",", 17), tokens[4]);

            Assert.AreEqual(new Text("author", 18), tokens[5]);
            Assert.AreEqual(new Equals("=", 107), tokens[6]);
            Assert.AreEqual(new OpeningBrace(" {", 108), tokens[7]);
            Assert.AreEqual(new Text("test-test", 110), tokens[8]);
            Assert.AreEqual(new ClosingBrace("}", 121), tokens[9]);

            Assert.AreEqual(new ClosingBrace("}", 122), tokens[10]);
        }

        [Test]
        public void TestSemiColonInTagValue()
        {
            Tokenizer tokenizer = new Tokenizer(new ExpressionDictionary(), @"@book{ aaker:1912,
                                                                                author = { test;test }
                                                                            }");
            List<AbstractToken> tokens = tokenizer.GetAllTokens().ToList();

            Assert.AreEqual(11, tokens.Count());
            Assert.AreEqual(new At("@"), tokens[0]);
            Assert.AreEqual(new Text("book", 1), tokens[1]);
            Assert.AreEqual(new OpeningBrace("{", 5), tokens[2]);
            Assert.AreEqual(new Text("aaker:1912", 6), tokens[3]);
            Assert.AreEqual(new Comma(",", 17), tokens[4]);

            Assert.AreEqual(new Text("author", 18), tokens[5]);
            Assert.AreEqual(new Equals("=", 107), tokens[6]);
            Assert.AreEqual(new OpeningBrace(" {", 108), tokens[7]);
            Assert.AreEqual(new Text("test;test", 110), tokens[8]);
            Assert.AreEqual(new ClosingBrace("}", 121), tokens[9]);

            Assert.AreEqual(new ClosingBrace("}", 122), tokens[10]);
        }
    }
}
