using System.Collections.Generic;
using NUnit.Framework;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public class CouponTest : BaseBusinessLogicTestFixture
    {
        [Test]
        public void CodeGenerator()
        {
            Coupon c = new Coupon();
            HashSet<string> set = new HashSet<string>();
            for (int i = 0; i < 1000; ++i)
            {
                string x = c.GenerateCode(8);
                Assert.IsTrue(!set.Contains(x));
                set.Add(x);

                char[] f = x.ToCharArray();
                Assert.IsTrue(f.Length == 8);
            //    Log.Error(x);
                foreach (char z in f)
                {
                    Assert.IsTrue(((int)z - (int)'A' >= 0 && (int)z - (int)'A' < 26) ||
                        ((int)z - (int)'0' >= 0 && (int)z - (int)'0' < 10), x);
                }
            }
        }

    }
}
