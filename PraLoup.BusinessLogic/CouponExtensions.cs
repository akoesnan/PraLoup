using System.Security.Cryptography;
using PraLoup.DataAccess.Entities;


namespace PraLoup.BusinessLogic
{
    public static class CouponExtensions
    {
        public static string GenerateCode(this Coupon c, int length)
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            
            char[] code = new char[length];
            byte [] b = new byte[1];
            for (int i = 0; i < length; ++i)
            {
                do
                {
                    rngCsp.GetBytes(b);
                }
                while (b[0] > 35 || b[0] < 0);

                if (b[0] < 26)
                {
                    code[i] = (char)((int)'A' + b[0]);
                }
                else
                {
                    code[i] = (char)((int)'0' + b[0] - 26);
                }
            }
            return new string(code);
        }
    }
}
