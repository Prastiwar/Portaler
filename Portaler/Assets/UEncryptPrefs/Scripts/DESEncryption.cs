using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public interface IEncryption
{
	string Encrypt(string plainText);
	bool TryDecrypt(string cipherText,out string plainText);
}

public class DESEncryption : IEncryption
{
	const int Iterations = 1000;
	private string media = "d0ff2d67d042926d1db7e428c35f9bea8713866250cab36f";

	//public string Encrypt(string plainText, string password)
	public string Encrypt(string plainText)
	{
		if (plainText == null)
		{
			throw new ArgumentNullException("plainText");
		}
		
		// create instance of the DES crypto provider
		var des = new DESCryptoServiceProvider();
		
		// generate a random IV will be used a salt value for generating key
		des.GenerateIV();
		
		// use derive bytes to generate a key from the password and IV
		var rfc2898DeriveBytes = new Rfc2898DeriveBytes(media, des.IV, Iterations);
		
		// generate a key from the password provided
		byte[] key = rfc2898DeriveBytes.GetBytes(8);
		
		// encrypt the plainText
		using (var memoryStream = new MemoryStream())
			using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, des.IV), CryptoStreamMode.Write))
		{
			// write the salt first not encrypted
			memoryStream.Write(des.IV, 0, des.IV.Length);
			
			// convert the plain text string into a byte array
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			
			// write the bytes into the crypto stream so that they are encrypted bytes
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}
	
	//public bool TryDecrypt(string cipherText, string password, out string plainText)
	public bool TryDecrypt(string cipherText, out string plainText)
	{	
		try
		{   
			byte[] cipherBytes = Convert.FromBase64String(cipherText);
			
			using (var memoryStream = new MemoryStream(cipherBytes))
			{
				// create instance of the DES crypto provider
				var des = new DESCryptoServiceProvider();
				
				// get the IV
				byte[] iv = new byte[8];
				memoryStream.Read(iv, 0, iv.Length);
				
				// use derive bytes to generate key from password and IV
				var rfc2898DeriveBytes = new Rfc2898DeriveBytes(media, iv, Iterations);
				
				byte[] key = rfc2898DeriveBytes.GetBytes(8);
				
				using (var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
					using (var streamReader = new StreamReader(cryptoStream))
				{
					plainText = streamReader.ReadToEnd();
					return true;
				}
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex);
			
			plainText = "";
			return false;
		}
	}
}