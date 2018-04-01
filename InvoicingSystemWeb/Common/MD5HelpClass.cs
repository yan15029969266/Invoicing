﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MD5HelpClass
    {
        /// <summary>  
        /// 获取文件的MD5码  
        /// </summary>  
        /// <param name="fileName">传入的文件名（含路径及后缀名）</param>  
        /// <returns></returns>  
        public static string GetMD5HashFromFile(string fileName)  
        {  
            try  
            {  
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);  
                MD5 md5 = new MD5CryptoServiceProvider();  
                byte[] retVal = md5.ComputeHash(file);  
                file.Close();  
                StringBuilder sb = new StringBuilder();  
                for (int i = 0; i < retVal.Length; i++)  
                {  
                    sb.Append(retVal[i].ToString("x2"));  
                }  
                return sb.ToString();  
            }  
            catch (Exception ex)  
            {  
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);  
            }  
        }
        //获取字符串的MD5码
        public static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }
    }
}
