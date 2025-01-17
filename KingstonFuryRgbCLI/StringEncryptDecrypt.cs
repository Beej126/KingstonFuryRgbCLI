using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KingstonFuryRgbCLI
{
	// Token: 0x0200002C RID: 44
	public class StringEncryptDecrypt
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		public static string Encrypt(string plainText, string passPhrase)
		{
			byte[] array = StringEncryptDecrypt.Generate256BitsOfRandomEntropy();
			byte[] array2 = StringEncryptDecrypt.Generate256BitsOfRandomEntropy();
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			string text;
			using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, array, 1000))
			{
				byte[] bytes2 = rfc2898DeriveBytes.GetBytes(32);
				using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
				{
					rijndaelManaged.BlockSize = 256;
					rijndaelManaged.Mode = CipherMode.CBC;
					rijndaelManaged.Padding = PaddingMode.PKCS7;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(bytes2, array2))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
							{
								cryptoStream.Write(bytes, 0, bytes.Length);
								cryptoStream.FlushFinalBlock();
								byte[] array3 = array.Concat(array2).ToArray<byte>().Concat(memoryStream.ToArray())
									.ToArray<byte>();
								memoryStream.Close();
								cryptoStream.Close();
								text = Convert.ToBase64String(array3);
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		public static string Decrypt(string cipherText, string passPhrase)
		{
			byte[] array = Convert.FromBase64String(cipherText);
			byte[] array2 = array.Take(32).ToArray<byte>();
			byte[] array3 = array.Skip(32).Take(32).ToArray<byte>();
			byte[] array4 = array.Skip(64).Take(array.Length - 64).ToArray<byte>();
			string @string;
			using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, array2, 1000))
			{
				byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
				using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
				{
					rijndaelManaged.BlockSize = 256;
					rijndaelManaged.Mode = CipherMode.CBC;
					rijndaelManaged.Padding = PaddingMode.PKCS7;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(bytes, array3))
					{
						using (MemoryStream memoryStream = new MemoryStream(array4))
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
							{
								byte[] array5 = new byte[array4.Length];
								int num = cryptoStream.Read(array5, 0, array5.Length);
								memoryStream.Close();
								cryptoStream.Close();
								@string = Encoding.UTF8.GetString(array5, 0, num);
							}
						}
					}
				}
			}
			return @string;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000F95C File Offset: 0x0000DB5C
		private static byte[] Generate256BitsOfRandomEntropy()
		{
			byte[] array = new byte[32];
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				rngcryptoServiceProvider.GetBytes(array);
			}
			return array;
		}

		// Token: 0x0400012A RID: 298
		private const int Keysize = 256;

		// Token: 0x0400012B RID: 299
		private const int DerivationIterations = 1000;
	}
}
