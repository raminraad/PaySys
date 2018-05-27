using System;
using System.Threading;
using NUnit.Framework;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UnitTest
{
    [TestFixture]
    public class TestUnitTest
    {
        [Test]
        public void TempTest()
        {
            using (var ctx = new PaySysContext())
            {
                for (int i = 0; i < 10; i++)
                {
                    MainGroup mainGroup = new MainGroup() {Title = "MainGroup One"};
                    SubGroup subGroup = new SubGroup() {MainGroup = mainGroup};
                    ctx.MainGroups.Add(mainGroup);
                    ctx.SubGroups.Add(subGroup);
                }
                ctx.SaveChanges();
            }
            Assert.Pass("Assertion done");
        }
    }
}
