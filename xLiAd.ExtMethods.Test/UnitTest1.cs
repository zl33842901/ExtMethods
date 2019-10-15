using System;
using Xunit;

namespace xLiAd.ExtMethods.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var dt = DateTime.Now;
            var r1 = System.ExtMethods.ToTimeStamp(dt, true);
            var r2 = System.ExtMethods.ToTimeStamp(dt, false);
            var ro = System.ExtMethods.ToTimeStamp(dt);
            Assert.Equal(r2, ro);
            Assert.Equal(r1 / 1000, r2);
        }

        [Fact]
        public void TestToTime()
        {
            var t1 = "92556455";
            var rst = t1.ToTime();
        }
    }
}
