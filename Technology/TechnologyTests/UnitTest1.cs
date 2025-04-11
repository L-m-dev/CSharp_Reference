using Technology.Classes;
namespace TechnologyTests {

    [TestClass]
    public class UnitTest1 {
        Computer computer;
        Laptop laptop;
        Smartphone smartphone;

        [TestInitialize]
        public void Initialize() {
            double wattage = 200;
            double power = 13;
            double laptopWeight = 20;
            double smartphoneSize = 5;

            computer = new Computer("Microsoft", wattage, power);
            laptop = new Laptop("Acer", power, wattage, laptopWeight);
            smartphone = new Smartphone("Apple", power, wattage, smartphoneSize);
        }

        [TestMethod]
        public void CreateComputer_ShouldSetupAccurateCurrentPowerRatio() {
            double wattage = 200;
            double power = 13;
            double cpwToBeCompared = power / wattage;

            Assert.AreEqual(cpwToBeCompared, computer.CurrentPowerRatio);
        }

        [TestMethod]
        public void CreateBatteryObjects_OnBatteryShouldBeFalse() {
            Assert.IsFalse(laptop.IsOnBattery);
            Assert.IsFalse(smartphone.IsOnBattery);
        }
        [TestMethod]
        public void SmarthphoneIsCompact_ShouldReturnTrueForValidPocketSizes() {
            double pocketSize = smartphone._size;
            Assert.IsTrue(smartphone.IsCompact(pocketSize));
        }

        [TestMethod]
        public void TestSmartPhoneIsCompact_BoundarySizes() {
            double pocketSize = smartphone._size;

            Assert.IsTrue(smartphone.IsCompact(pocketSize + 1));
            Assert.IsTrue(smartphone.IsCompact(pocketSize));
            Assert.IsFalse(smartphone.IsCompact(pocketSize - 1));
        }

        [TestMethod]
        public void TestIdAssignment_ShouldIncrementProperly() {

            //reuse object assignment for clarity:
            double wattage = 200;
            double power = 13;
            double laptopWeight = 20;
            double smartphoneSize = 5;

            int idToTest = AbstractEntity.NextId;

            computer = new Computer("Microsoft", wattage, power);
            laptop = new Laptop("Acer", power, wattage, laptopWeight);
            smartphone = new Smartphone("Apple", power, wattage, smartphoneSize);

            //3 objects were setup on BeforeAll/TestInitialize.
            //Therefore, tests will begin at the current NextId.

            Assert.AreEqual(idToTest, computer.Id);
            idToTest++;
            Assert.AreEqual(idToTest, laptop.Id);
            idToTest++;
            Assert.AreEqual(idToTest, smartphone.Id);
        }
    }
}