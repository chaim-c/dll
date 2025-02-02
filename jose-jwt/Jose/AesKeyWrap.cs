using System;
using System.IO;
using System.Security.Cryptography;

namespace Jose
{
	// Token: 0x02000012 RID: 18
	public static class AesKeyWrap
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003B04 File Offset: 0x00001D04
		public static byte[] Wrap(byte[] cek, byte[] kek)
		{
			Ensure.MinBitSize(cek, 128L, "AesKeyWrap.Wrap() expects content length not less than 128 bits, but was {0}", new object[]
			{
				(long)cek.Length * 8L
			});
			Ensure.Divisible(cek.Length, 8, "AesKeyWrap.Wrap() expects content length to be divisable by 8, but was given a content of {0} bit size.", new object[]
			{
				(long)cek.Length * 8L
			});
			byte[] array = AesKeyWrap.DefaultIV;
			byte[][] array2 = Arrays.Slice(cek, 8);
			long num = (long)array2.Length;
			for (long num2 = 0L; num2 < 6L; num2 += 1L)
			{
				for (long num3 = 0L; num3 < num; num3 += 1L)
				{
					long right = num * num2 + num3 + 1L;
					checked
					{
						byte[] arr = AesKeyWrap.AesEnc(kek, Arrays.Concat(new byte[][]
						{
							array,
							array2[(int)((IntPtr)num3)]
						}));
						array = Arrays.FirstHalf(arr);
						array2[(int)((IntPtr)num3)] = Arrays.SecondHalf(arr);
						array = Arrays.Xor(array, right);
					}
				}
			}
			byte[][] array3 = new byte[num + 1L][];
			array3[0] = array;
			for (long num4 = 1L; num4 <= num; num4 += 1L)
			{
				checked
				{
					array3[(int)((IntPtr)num4)] = array2[(int)((IntPtr)(unchecked(num4 - 1L)))];
				}
			}
			return Arrays.Concat(array3);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003C10 File Offset: 0x00001E10
		public static byte[] Unwrap(byte[] encryptedCek, byte[] kek)
		{
			Ensure.MinBitSize(encryptedCek, 128L, "AesKeyWrap.Unwrap() expects content length not less than 128 bits, but was {0}", new object[]
			{
				(long)encryptedCek.Length * 8L
			});
			Ensure.Divisible(encryptedCek.Length, 8, "AesKeyWrap.Unwrap() expects content length to be divisable by 8, but was given a content of {0} bit size.", new object[]
			{
				(long)encryptedCek.Length * 8L
			});
			byte[][] array = Arrays.Slice(encryptedCek, 8);
			byte[] array2 = array[0];
			byte[][] array3 = new byte[array.Length - 1][];
			for (int i = 1; i < array.Length; i++)
			{
				array3[i - 1] = array[i];
			}
			long num = (long)array3.Length;
			for (long num2 = 5L; num2 >= 0L; num2 -= 1L)
			{
				for (long num3 = num - 1L; num3 >= 0L; num3 -= 1L)
				{
					long right = num * num2 + num3 + 1L;
					array2 = Arrays.Xor(array2, right);
					checked
					{
						byte[] arr = AesKeyWrap.AesDec(kek, Arrays.Concat(new byte[][]
						{
							array2,
							array3[(int)((IntPtr)num3)]
						}));
						array2 = Arrays.FirstHalf(arr);
						array3[(int)((IntPtr)num3)] = Arrays.SecondHalf(arr);
					}
				}
			}
			if (!Arrays.ConstantTimeEquals(AesKeyWrap.DefaultIV, array2))
			{
				throw new IntegrityException("AesKeyWrap integrity check failed.");
			}
			return Arrays.Concat(array3);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003D2C File Offset: 0x00001F2C
		private static byte[] AesDec(byte[] sharedKey, byte[] cipherText)
		{
			byte[] result;
			using (Aes aes = Aes.Create())
			{
				aes.Key = sharedKey;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(cipherText, 0, cipherText.Length);
							cryptoStream.FlushFinalBlock();
							result = memoryStream.ToArray();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003DF0 File Offset: 0x00001FF0
		private static byte[] AesEnc(byte[] sharedKey, byte[] plainText)
		{
			byte[] result;
			using (Aes aes = Aes.Create())
			{
				aes.Key = sharedKey;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(plainText, 0, plainText.Length);
							cryptoStream.FlushFinalBlock();
							result = memoryStream.ToArray();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0400004D RID: 77
		private static readonly byte[] DefaultIV = new byte[]
		{
			166,
			166,
			166,
			166,
			166,
			166,
			166,
			166
		};
	}
}
