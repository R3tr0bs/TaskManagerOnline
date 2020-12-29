using System;

namespace TMEAddons
{
    public static class Encryption
    {
        public static DateTime today = DateTime.Today;
        private static int day = today.Day;
        private static int month = today.Month;
        public static string Encrypt(string enc) 
        {
            string ret = "";
            foreach(char ch in enc) 
            {
                ret += (char)((int)ch + (day%10 + month));
            }
            return ret;
        }
        public static string Decrypt(string dec) 
        {
            string ret = "";
            foreach (char ch in dec)
            {
                ret += (char)((int)ch - (day % 10 + month));
            }
            return ret;
        }
    }
}
