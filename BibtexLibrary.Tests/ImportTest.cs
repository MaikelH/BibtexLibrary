using System;
using System.Collections.Generic;
using System.IO;
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

        [Test]
        public void TestIssue1()
        {
            BibtexFile file = BibtexLibrary.BibtexImporter.FromString(@"@Article{ChgfgA,
                                                                                    author = ""Author 1, Author 2"",
                                                                                    title = ""this text should be in double quotes {""}bio-tech{""}"",
                                                                                    journal =""ABC Journal."",
                                                                                    year =""2014"",
                                                                                    volume =""5"",
                                                                                    issue =""23"",
                                                                                    pages =""46-49"",
                                                                                    publisher =""The American Publisher"",
                                                                                    doi ="""",
                                                                                    url ="""",
                                                                                    abstract =""This abstract has comma{,} and double quotes syntax {""}bio-tech{""} how can this be fixed""}");
            
            Assert.AreEqual(1, file.Entries.Count);

            Assert.AreEqual(file.Entries.ToList()[0].Key, "ChgfgA");
            Assert.AreEqual(file.Entries.ToList()[0].Type, "Article");

            Dictionary<string, string> tags = file.Entries.ToList()[0].Tags;
            Assert.AreEqual(11, tags.Count);
            Assert.AreEqual("Author 1, Author 2", tags["author"]);
            Assert.AreEqual("this text should be in double quotes {\"}bio-tech{\"}", tags["title"]);
            Assert.AreEqual("This abstract has comma{,} and double quotes syntax {\"}bio-tech{\"} how can this be fixed", tags["abstract"]);
        }

        [Test]
        public void TestFuzzyMiningFile()
        {
            string fileContent = File.ReadAllText("Test Files\\Fuzzy Mining.bib");

            BibtexFile file = BibtexLibrary.BibtexImporter.FromString(fileContent);

            Assert.AreEqual(3, file.Entries.Count);
        }

        [Test]
        public void TestReferencesFile()
        {
            string fileContent = File.ReadAllText("Test Files\\References.bib");

            BibtexFile file = BibtexLibrary.BibtexImporter.FromString(fileContent);

            Assert.AreEqual(1, file.Entries.Count);
        }

        [Test]
        public void TestBiblatexExamplesFile()
        {
            string fileContent = File.ReadAllText("Test Files\\biblatex-examples.bib");

            BibtexFile file = BibtexLibrary.BibtexImporter.FromString(fileContent);

            Assert.AreEqual(1, file.Entries.Count);
        }
    }
}