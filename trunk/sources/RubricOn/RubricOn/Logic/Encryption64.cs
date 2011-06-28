using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;


namespace RubricOn.Logic
{
    public class Encryption64
    {
        private string lastError;
        private byte[] Key = { };
        private byte[] IV = { 0x12, 0xEF, 0x65, 0xBC, 0xFA, 0x6C, 0xDC, 0xBE };
        String encryptionKey = "!R5_6Gfy(";

        public String Decrypt(String stringToDecrypt, String EncryptionKey)
        {
            try
            {
                Key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider cryptoSP = new DESCryptoServiceProvider();
                byte[] inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public String Encrypt(String stringToEncrypt, String EncryptionKey)
        {
            try
            {
                Key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider cryptoSP = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }

}
