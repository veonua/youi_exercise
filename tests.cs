using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youiTest.Tests
{
    [TestClass()]
    public class AppTests
    {
        [TestMethod()]
        public void AddressTest()
        {
            Assert.AreEqual("Long Lane", new Address("102  Long Lane").streetName);
            Assert.AreEqual("6th Ave", new Address("1335 6th Ave").streetName);
            Assert.AreEqual("邮政编码", new Address("99 邮政编码").streetName);
        }

        public List<Name> MakeNames()
        {
            var names = new List<Name>();
            names.Add(new Name("Peter", "Pan"));
            names.Add(new Name("Maimie", "Mannering"));
            names.Add(new Name("Wendy", "Darling"));
            names.Add(new Name("John", "Darling"));
            names.Add(new Name("Michael", "Darling"));
            names.Add(new Name("Mary", "Darling"));
            names.Add(new Name("George", "Darling"));
            names.Add(new Name("Tiger", "Lily"));
            names.Add(new Name("Tinker", "Bell"));
            names.Add(new Name("Captain", "Hook"));
            return names;
        }

        [TestMethod()]
        public void Task1Test()
        {
            var res1 = Program.task1(MakeNames()).ToList();
            
            Assert.AreEqual("Hook", res1.First().last);
            Assert.AreEqual("Wendy", res1.Last().first);
        }

        [TestMethod()]
        public void Task1_2Test()
        {
            var res2 = Program.task1_2(MakeNames()).ToList();
            Assert.AreEqual("Darling", res2.First().name);
            Assert.AreEqual(5, res2.First().count);
        }


        [TestMethod()]
        public void Task2Test()
        {
            var addresses = new List<Address>();
            addresses.Add(new Address("10 2nd street"));
            addresses.Add(new Address("20 1st avenue"));
            addresses.Add(new Address("20 2nd street"));
            addresses.Add(new Address("30 2nd street"));
            
            var res2 = Program.task2(addresses, true).ToList();
            Assert.AreEqual("1st avenue", res2.First());
            Assert.AreEqual("2nd street", res2.Last());
        }
    }
}
