using SpinningDiscs;
using System.Text;
namespace TestDisc {
    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void TestWriteDataCD() {
            CD disc = new CD("LG");
            string testString = "Hello Man";
            byte[] stringConverted = Encoding.ASCII.GetBytes(testString);
            disc.WriteData(stringConverted);
            Assert.AreEqual(stringConverted.Length - 1, disc.LastIndexUsedPointer);
        }

        [TestMethod]
        public void TestWriteDataDVD() {
            DVD disc = new DVD("LG");
            string testString = "Hello Man";
            byte[] stringConverted = Encoding.ASCII.GetBytes(testString);
            disc.WriteData(stringConverted);
            Assert.AreEqual(stringConverted.Length-1, disc.LastIndexUsedPointer);         
        }

        [TestMethod]
        public void TestDiscCreation2() {
            CD cdDisk = new CD("Elk");
            DVD dvdDisk = new DVD("Sony");

            Assert.AreEqual(0, cdDisk.LastIndexUsedPointer);
            Assert.AreEqual(0, cdDisk.CurrentUsedCapacity);
            Assert.AreEqual(cdDisk.MINIMUM_ARRAY_SIZE, cdDisk.Data.Length);
            Assert.AreEqual(200000000, cdDisk.ReadRate);
            Assert.AreEqual(50000000, cdDisk.WriteRate);
            Assert.IsNotNull(cdDisk.Data);

            Assert.AreEqual(0, dvdDisk.LastIndexUsedPointer);
            Assert.AreEqual(0, dvdDisk.CurrentUsedCapacity);
            Assert.AreEqual(dvdDisk.MINIMUM_ARRAY_SIZE, dvdDisk.Data.Length);
            Assert.AreEqual(500000000, dvdDisk.ReadRate);
            Assert.AreEqual(100000000, dvdDisk.WriteRate);
            Assert.IsNotNull(dvdDisk.Data);
        }

        [TestMethod]
        public void TestResetStateThenWriteDataDVD() {
            DVD disc = new DVD("LG");
            string testString = "Hello Man";
            byte[] stringConverted = Encoding.ASCII.GetBytes(testString);
            disc.WriteData(stringConverted);
            Assert.AreEqual(stringConverted.Length - 1, disc.LastIndexUsedPointer);

            bool successFormat = disc.FormatData();
            Assert.IsTrue(successFormat);

            Assert.AreEqual(0, disc.LastIndexUsedPointer);
            Assert.AreEqual(0, disc.CurrentUsedCapacity);
            Assert.AreEqual(disc.MINIMUM_ARRAY_SIZE, disc.Data.Length);
            Assert.AreEqual(500000000, disc.ReadRate);
            Assert.AreEqual(100000000, disc.WriteRate);
            Assert.IsNotNull(disc.Data);

        }
    }
}