using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Day1New.Controllers
{
    public class MD5Controller : Controller
    {
        public string GetMD5SumHash(string inputString)
        {

            MD5 md5 = MD5.Create();

            var input = Encoding.ASCII.GetBytes(inputString);
            var hash = md5.ComputeHash(input);

            StringBuilder tempString = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                tempString.Append(hash[i].ToString("X2"));
            }

            return tempString.ToString();

        }
    }
}
