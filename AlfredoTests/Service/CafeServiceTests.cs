﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            CafeService.GetRestaurant(day, CafeService.Cafes[0]);
        }
    }
}