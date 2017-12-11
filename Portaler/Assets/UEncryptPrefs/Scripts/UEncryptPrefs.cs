using UnityEngine;
using System.Security.Cryptography;
using System.Text;

public static class UEncryptPrefs
{
	
	public static void SetInt(string key, int value)
	{
		SetString(key,value.ToString());
	}

	public static void SetFloat(string key, float value)
	{
		SetString(key,value.ToString());
	}

	public static int GetInt(string key)
	{
		return (int.Parse(GetString(key)));
	}

	public static int GetInt(string key,int defaultValue)
	{
		return (int.Parse(GetString(key,defaultValue.ToString())));
	}

	public static float GetFloat(string key)
	{
		return (float.Parse(GetString(key)));
	}

	public static float GetFloat(string key,float defaultValue)
	{
		return (float.Parse(GetString(key,defaultValue.ToString())));
	}

	public static void SetString(string key, string value)
	{
		var desEncryption = new DESEncryption();
		string hashedKey = GenerateMD5(key);
		string encryptedValue = desEncryption.Encrypt(value);
		PlayerPrefs.SetString(hashedKey, encryptedValue);
	}
	
	public static string GetString(string key)
	{
		string hashedKey = GenerateMD5(key);
		if (PlayerPrefs.HasKey(hashedKey))
		{
			var desEncryption = new DESEncryption();
			string encryptedValue = PlayerPrefs.GetString(hashedKey);
			string decryptedValue;
			desEncryption.TryDecrypt(encryptedValue, out decryptedValue);
			return decryptedValue;
		}
		else
		{
			return "";
		}
	}
	
	public static string GetString(string key, string defaultValue)
	{
		if (HasKey(key))
		{
			return GetString(key);
		}
		else
		{
			SetString(key,defaultValue);
			return defaultValue;
		}
	}
	
	public static bool HasKey(string key)
	{
		string hashedKey = GenerateMD5(key);
		bool hasKey = PlayerPrefs.HasKey(hashedKey);
		return hasKey;
	}
	
	/// <summary>
	/// Generates an MD5 hash of the given text.
	/// WARNING. Not safe for storing passwords
	/// </summary>
	/// <returns>MD5 Hashed string</returns>
	/// <param name="text">The text to hash</param>
	static string GenerateMD5(string text)
	{
		var md5 = MD5.Create();
		byte[] inputBytes = Encoding.UTF8.GetBytes(text);
		byte[] hash = md5.ComputeHash(inputBytes);
		
		// step 2, convert byte array to hex string
		var sb = new StringBuilder();
		for (int i = 0; i < hash.Length; i++)
		{
			sb.Append(hash[i].ToString("X2"));
		}
		return sb.ToString();
	}
}