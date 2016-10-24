using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
Develop a console application to encrypt and decrypt data using private and public keys.
*/

namespace SixteenthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string enteredString;
            string publicKeyXML;
            string privateKeyXML;
            byte[] dataToEncrypt;
            byte[] encryptedData, decryptedData;

            Console.WriteLine("Enter data to encrypt:");
            enteredString = Convert.ToString(Console.ReadLine());

            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            dataToEncrypt = ByteConverter.GetBytes(enteredString);

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                publicKeyXML = RSA.ToXmlString(false);
                privateKeyXML = RSA.ToXmlString(true);
            }

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKeyXML);
                encryptedData = RSA.Encrypt(dataToEncrypt, false);
            }

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKeyXML);
                decryptedData = RSA.Decrypt(encryptedData, false);
            }

            string decryptedString = ByteConverter.GetString(decryptedData);
            Console.WriteLine("Decrypted data:");
            Console.WriteLine(decryptedString);
        }
    }
}
