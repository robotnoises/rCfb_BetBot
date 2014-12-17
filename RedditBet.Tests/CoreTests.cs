using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Microsoft.VisualStudio.QualityTools.UnitTestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedditBet.Tests
{
    /// <summary>
    /// Theses are the most critical tests for this Solution
    /// </summary>
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void IsFloridaStateGood()
        {
            var yeah = true;
            Assert.IsTrue(yeah);
        }

        [TestMethod]
        public void IsFloridaStateLike_ReallyGood()
        {
            var yeahDude = true;
            Assert.IsTrue(yeahDude);
        }

        [TestMethod]
        public void WillFloridaStateEverLoseAGame()
        {
            var haha = false;
            Assert.IsFalse(haha);
        }

        [TestMethod]
        public void IsFloridaGoodAtFootball()
        {
            var floridaGood = false;
            Assert.IsFalse(floridaGood);
        }

        [TestMethod]
        public void IsMiamiGoodAtFootball()
        {
            var miamiGood = false;
            Assert.IsFalse(miamiGood);
        }

        [TestMethod]
        public void IsTheAccElite()
        {
            var woooo = true;
            Assert.IsTrue(woooo);
        }

        [TestMethod]
        public void AreThereGoingToBeAnyRealUnitTests()
        {
            var uhh = "maybe";
            Assert.IsNotNull(uhh);
        }
    }
}
