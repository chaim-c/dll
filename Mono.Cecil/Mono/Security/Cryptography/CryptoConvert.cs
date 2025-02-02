using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000F4 RID: 244
	internal static class CryptoConvert
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x0001E05C File Offset: 0x0001C25C
		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return (int)bytes[offset + 3] << 24 | (int)bytes[offset + 2] << 16 | (int)bytes[offset + 1] << 8 | (int)bytes[offset];
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001E07B File Offset: 0x0001C27B
		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)((int)bytes[offset + 3] << 24 | (int)bytes[offset + 2] << 16 | (int)bytes[offset + 1] << 8 | (int)bytes[offset]);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001E09C File Offset: 0x0001C29C
		private static byte[] Trim(byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					byte[] array2 = new byte[array.Length - i];
					Buffer.BlockCopy(array, i, array2, 0, array2.Length);
					return array2;
				}
			}
			return null;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		private static RSA FromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			RSAParameters parameters = default(RSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 843141970U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				byte[] array = new byte[4];
				Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
				Array.Reverse(array);
				parameters.Exponent = CryptoConvert.Trim(array);
				int num2 = offset + 20;
				int num3 = num >> 3;
				parameters.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, parameters.Modulus, 0, num3);
				Array.Reverse(parameters.Modulus);
				num2 += num3;
				int num4 = num3 >> 1;
				parameters.P = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.P, 0, num4);
				Array.Reverse(parameters.P);
				num2 += num4;
				parameters.Q = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.Q, 0, num4);
				Array.Reverse(parameters.Q);
				num2 += num4;
				parameters.DP = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.DP, 0, num4);
				Array.Reverse(parameters.DP);
				num2 += num4;
				parameters.DQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.DQ, 0, num4);
				Array.Reverse(parameters.DQ);
				num2 += num4;
				parameters.InverseQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.InverseQ, 0, num4);
				Array.Reverse(parameters.InverseQ);
				num2 += num4;
				parameters.D = new byte[num3];
				if (num2 + num3 + offset <= blob.Length)
				{
					Buffer.BlockCopy(blob, num2, parameters.D, 0, num3);
					Array.Reverse(parameters.D);
				}
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			RSA rsa = null;
			try
			{
				rsa = RSA.Create();
				rsa.ImportParameters(parameters);
			}
			catch (CryptographicException)
			{
				bool flag = false;
				try
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(parameters);
				}
				catch
				{
					flag = true;
				}
				if (flag)
				{
					throw;
				}
			}
			return rsa;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001E364 File Offset: 0x0001C564
		private static RSA FromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			RSA result;
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 826364754U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				RSAParameters parameters = default(RSAParameters);
				parameters.Exponent = new byte[3];
				parameters.Exponent[0] = blob[offset + 18];
				parameters.Exponent[1] = blob[offset + 17];
				parameters.Exponent[2] = blob[offset + 16];
				int srcOffset = offset + 20;
				int num2 = num >> 3;
				parameters.Modulus = new byte[num2];
				Buffer.BlockCopy(blob, srcOffset, parameters.Modulus, 0, num2);
				Array.Reverse(parameters.Modulus);
				RSA rsa = null;
				try
				{
					rsa = RSA.Create();
					rsa.ImportParameters(parameters);
				}
				catch (CryptographicException)
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(parameters);
				}
				result = rsa;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			return result;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001E494 File Offset: 0x0001C694
		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlob(blob, 0);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
		public static RSA FromCapiKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			byte b = blob[offset];
			if (b != 0)
			{
				switch (b)
				{
				case 6:
					return CryptoConvert.FromCapiPublicKeyBlob(blob, offset);
				case 7:
					return CryptoConvert.FromCapiPrivateKeyBlob(blob, offset);
				}
			}
			else if (blob[offset + 12] == 6)
			{
				return CryptoConvert.FromCapiPublicKeyBlob(blob, offset + 12);
			}
			throw new CryptographicException("Unknown blob format.");
		}
	}
}
