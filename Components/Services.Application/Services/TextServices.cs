using System;
using System.Linq;

namespace Services.Application.Services
{
    public static class TextServices
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generate text alfanumeric acordangly to the length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
