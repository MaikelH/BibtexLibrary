using System;
using System.Collections.Generic;
using System.Linq;
using BibtexLibrary;
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

        [Test]
        public void TestOneEntryWithQuotesSimple()
        {
            BibtexFile file = BibtexLibrary.BibtexImporter.FromString(@"@Article{Martin:1982:CGS,
                                                                                      author =       ""J. W. Martin"",
                                                                                      title =        ""Computer Graphics Software Workshop Report"",
                                                                                      volume =       ""1"",
                                                                                      number =       ""1"",
                                                                                      pages =        ""10--13"",
                                                                                    }");

            Assert.AreEqual(1, file.Entries.Count);
            Assert.AreEqual(file.Entries.ToList()[0].Key, "Martin:1982:CGS");
            Assert.AreEqual(file.Entries.ToList()[0].Type, "Article");

            Dictionary<string, string> tags = file.Entries.ToList()[0].Tags;
            Assert.AreEqual("J. W. Martin", tags["author"]);
            Assert.AreEqual("Computer Graphics Software Workshop Report", tags["title"]);
            Assert.AreEqual("1", tags["volume"]);
            Assert.AreEqual("1", tags["number"]);
            Assert.AreEqual("10--13", tags["pages"]);
        }
    }
}