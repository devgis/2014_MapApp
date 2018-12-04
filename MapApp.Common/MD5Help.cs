using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace MapApp.Common
{
    public class MD5Help
    {
        public static string MD5Encrypt(string strText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(strText)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        } 
    }
}
