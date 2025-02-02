using System;
using System.Security.Cryptography;

namespace Jose
{
	// Token: 0x02000025 RID: 37
	public class HmacUsingSha : IJwsAlgorithm
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00005044 File Offset: 0x00003244
		public HmacUsingSha(string hashMethod)
		{
			this.hashMethod = hashMethod;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005054 File Offset: 0x00003254
		public byte[] Sign(byte[] securedInput, object key)
		{
			byte[] key2 = Ensure.Type<byte[]>(key, "HmacUsingSha alg expectes key to be byte[] array.", Array.Empty<object>());
			byte[] result;
			using (KeyedHashAlgorithm keyedHashAlgorithm = this.KeyedHash(key2))
			{
				result = keyedHashAlgorithm.ComputeHash(securedInput);
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000050A0 File Offset: 0x000032A0
		public bool Verify(byte[] signature, byte[] securedInput, object key)
		{
			byte[] actual = this.Sign(securedInput, key);
			return Arrays.ConstantTimeEquals(signature, actual);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000050C0 File Offset: 0x000032C0
		private KeyedHashAlgorithm KeyedHash(byte[] key)
		{
			if ("SHA256".Equals(this.hashMethod))
			{
				return new HMACSHA256(key);
			}
			if ("SHA384".Equals(this.hashMethod))
			{
				return new HMACSHA384(key);
			}
			if ("SHA512".Equals(this.hashMethod))
			{
				return new HMACSHA512(key);
			}
			throw new ArgumentException("Unsupported hashing algorithm: '{0}'", this.hashMethod);
		}

		// Token: 0x04000059 RID: 89
		private string hashMethod;
	}
}
