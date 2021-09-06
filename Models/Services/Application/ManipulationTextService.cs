using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using API_Cifra_Decifra_Testo.Models.InputModels;
using API_Cifra_Decifra_Testo.Models.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API_Cifra_Decifra_Testo.Models.Services.Application
{
    public class ManipulationTextService : IManipulationTextService
    {
        private readonly ILogger<ManipulationTextService> logger;
        private readonly IOptionsMonitor<SecurityOptions> securityOptionsMonitor;

        public ManipulationTextService(ILogger<ManipulationTextService> logger, IOptionsMonitor<SecurityOptions> securityOptionsMonitor)
        {
            this.logger = logger;
            this.securityOptionsMonitor = securityOptionsMonitor;
        }

        //Customized code starting from the example: https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/
        public string TestoCifratoGeneratorAsync(InputTesto model, IWebHostEnvironment env)
        {
            var options = this.securityOptionsMonitor.CurrentValue;
            string EncryptionKey = options.EncryptKey;

            byte[] iv = new byte[16];  
            byte[] array;  
  
            using (Aes aes = Aes.Create())  
            {  
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);  
                aes.IV = iv;  
  
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
  
                using (MemoryStream memoryStream = new MemoryStream())  
                {  
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))  
                    {  
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))  
                        {  
                            streamWriter.Write(model.Testo);  
                        }  
  
                        array = memoryStream.ToArray();  
                    }  
                }  
            }  
  
            return Convert.ToBase64String(array);
        }

        public string TestoCifratoRestoreAsync(InputTesto model, IWebHostEnvironment env)
        {
            var options = this.securityOptionsMonitor.CurrentValue;
            string EncryptionKey = options.EncryptKey;

            byte[] iv = new byte[16];  
            byte[] buffer = Convert.FromBase64String(model.Testo);  
  
            using (Aes aes = Aes.Create())  
            {  
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);  
                aes.IV = iv;  

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);  
  
                using (MemoryStream memoryStream = new MemoryStream(buffer))  
                {  
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))  
                    {  
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))  
                        {  
                            return streamReader.ReadToEnd();  
                        }  
                    }  
                }  
            } 
        }
    }
}