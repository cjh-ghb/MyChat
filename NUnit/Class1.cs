using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void testA()
        {
            int d, i;
            for (i = 0; i < 10; i++)
                d = i;
            Assert.AreEqual(10, i);
        }
    }
}
