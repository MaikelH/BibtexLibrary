using System.Collections.Generic;
using System.Linq;
using BibtexLibrary.Parser.Nodes;
using NUnit.Framework;
using BibtexFile = BibtexLibrary.BibtexFile;

namespace BibtexImporter.Tests
{
    [TestFixture]
    public class ImportTest
    {
        [Test]
        public void TestOneEntrySimple()
        {
            BibtexFile file = BibtexLibrary.BibtexImporter.FromString(@"@book{ aaker:1981a,
                                                                                  author = {David A. Aaker},
                                                                                  title = {Multivariate Analysis in Marketing},
                                                                                  edition = {2},
                                                                                  publisher = {The Scientific Press},
                                                                                  year = {1981},
                                                                                  address = {Palo Alto},
                                                                                  topic = {multivariate-statistics;market-research;}
                                                                                 }");

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual(file.Entries.ToList()[0].Key, "aaker:1981a");
            Assert.AreEqual(file.Entries.ToList()[0].Type, "book");

            Dictionary<string, string> tags = file.Entries.ToList()[0].Tags;
            Assert.AreEqual("David A. Aaker", tags["author"]);
            Assert.AreEqual("The Scientific Press", tags["publisher"]);
            Assert.AreEqual("1981", tags["year"]);
            Assert.AreEqual("multivariate-statistics;market-research;", tags["topic"]);
            Assert.AreEqual("Multivariate Analysis in Marketing", tags["title"]);
        }
    }
}