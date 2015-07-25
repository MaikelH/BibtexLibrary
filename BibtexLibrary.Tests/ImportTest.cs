using System.Linq;
using BibtexLibrary;
using NUnit.Framework;

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

            Assert.AreEqual(1, file.Entries);
            Assert.AreEqual(file.Entries.ToList()[0].Key, "aaker:1981a");
        }
    }
}