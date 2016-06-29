using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alfredo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfredo.Service.Tests
{
    [TestClass()]
    public class CafeServiceTests
    {
        [TestMethod()]
        public void GetListTest()
        {
            var day = DateTime.Now;

            CafeService.GetCafe(day, "Cafe 31");

            CafeService.GetCafe(day, "café 31");

            CafeService.GetCafe(day, "cafe 31");
        }

        [TestMethod()]
        public void GetFoodIndexTest()
        {
            var day = DateTime.Now;
            var result1 = CafeService.GetFoodIndex(day, "Cafe 31");
            Assert.AreNotEqual(0, result1.Count);

            var result2 = CafeService.GetFoodIndex(day, "café 31");
            Assert.AreNotEqual(0, result2.Count);

            var result3 = CafeService.GetFoodIndex(day, "cafe 31");
            Assert.AreNotEqual(0, result3.Count);
        }
    }
}