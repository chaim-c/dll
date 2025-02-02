using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Cecil
{
	// Token: 0x0200005E RID: 94
	public class AssemblyNameReference : IMetadataScope, IMetadataTokenProvider
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C501 File Offset: 0x0000A701
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000C509 File Offset: 0x0000A709
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				this.full_name = null;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000C519 File Offset: 0x0000A719
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000C521 File Offset: 0x0000A721
		public string Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
				this.full_name = null;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000C531 File Offset: 0x0000A731
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000C539 File Offset: 0x0000A739
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				this.full_name = null;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000C549 File Offset: 0x0000A749
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000C551 File Offset: 0x0000A751
		public AssemblyAttributes Attributes
		{
			get
			{
				return (AssemblyAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C55A File Offset: 0x0000A75A
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000C568 File Offset: 0x0000A768
		public bool HasPublicKey
		{
			get
			{
				return this.attributes.GetAttributes(1U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1U, value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000C57D File Offset: 0x0000A77D
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000C58B File Offset: 0x0000A78B
		public bool IsSideBySideCompatible
		{
			get
			{
				return this.attributes.GetAttributes(0U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(0U, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000C5B2 File Offset: 0x0000A7B2
		public bool IsRetargetable
		{
			get
			{
				return this.attributes.GetAttributes(256U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256U, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000C5DD File Offset: 0x0000A7DD
		public bool IsWindowsRuntime
		{
			get
			{
				return this.attributes.GetAttributes(512U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512U, value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000C607 File Offset: 0x0000A807
		public byte[] PublicKey
		{
			get
			{
				return this.public_key ?? Empty<byte>.Array;
			}
			set
			{
				this.public_key = value;
				this.HasPublicKey = !this.public_key.IsNullOrEmpty<byte>();
				this.public_key_token = Empty<byte>.Array;
				this.full_name = null;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C638 File Offset: 0x0000A838
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000C699 File Offset: 0x0000A899
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.public_key_token.IsNullOrEmpty<byte>() && !this.public_key.IsNullOrEmpty<byte>())
				{
					byte[] array = this.HashPublicKey();
					byte[] array2 = new byte[8];
					Array.Copy(array, array.Length - 8, array2, 0, 8);
					Array.Reverse(array2, 0, 8);
					this.public_key_token = array2;
				}
				return this.public_key_token ?? Empty<byte>.Array;
			}
			set
			{
				this.public_key_token = value;
				this.full_name = null;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		private byte[] HashPublicKey()
		{
			AssemblyHashAlgorithm assemblyHashAlgorithm = this.hash_algorithm;
			HashAlgorithm hashAlgorithm;
			if (assemblyHashAlgorithm == AssemblyHashAlgorithm.Reserved)
			{
				hashAlgorithm = MD5.Create();
			}
			else
			{
				hashAlgorithm = SHA1.Create();
			}
			byte[] result;
			using (hashAlgorithm)
			{
				result = hashAlgorithm.ComputeHash(this.public_key);
			}
			return result;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C704 File Offset: 0x0000A904
		public virtual MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.AssemblyNameReference;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C708 File Offset: 0x0000A908
		public string FullName
		{
			get
			{
				if (this.full_name != null)
				{
					return this.full_name;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.name);
				if (this.version != null)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append("Version=");
					stringBuilder.Append(this.version.ToString());
				}
				stringBuilder.Append(", ");
				stringBuilder.Append("Culture=");
				stringBuilder.Append(string.IsNullOrEmpty(this.culture) ? "neutral" : this.culture);
				stringBuilder.Append(", ");
				stringBuilder.Append("PublicKeyToken=");
				byte[] publicKeyToken = this.PublicKeyToken;
				if (!publicKeyToken.IsNullOrEmpty<byte>() && publicKeyToken.Length > 0)
				{
					for (int i = 0; i < publicKeyToken.Length; i++)
					{
						stringBuilder.Append(publicKeyToken[i].ToString("x2"));
					}
				}
				else
				{
					stringBuilder.Append("null");
				}
				return this.full_name = stringBuilder.ToString();
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C818 File Offset: 0x0000AA18
		public static AssemblyNameReference Parse(string fullName)
		{
			if (fullName == null)
			{
				throw new ArgumentNullException("fullName");
			}
			if (fullName.Length == 0)
			{
				throw new ArgumentException("Name can not be empty");
			}
			AssemblyNameReference assemblyNameReference = new AssemblyNameReference();
			string[] array = fullName.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (i == 0)
				{
					assemblyNameReference.Name = text;
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'='
					});
					if (array2.Length != 2)
					{
						throw new ArgumentException("Malformed name");
					}
					string a;
					if ((a = array2[0].ToLowerInvariant()) != null)
					{
						if (!(a == "version"))
						{
							if (!(a == "culture"))
							{
								if (a == "publickeytoken")
								{
									string text2 = array2[1];
									if (!(text2 == "null"))
									{
										assemblyNameReference.PublicKeyToken = new byte[text2.Length / 2];
										for (int j = 0; j < assemblyNameReference.PublicKeyToken.Length; j++)
										{
											assemblyNameReference.PublicKeyToken[j] = byte.Parse(text2.Substring(j * 2, 2), NumberStyles.HexNumber);
										}
									}
								}
							}
							else
							{
								assemblyNameReference.Culture = array2[1];
							}
						}
						else
						{
							assemblyNameReference.Version = new Version(array2[1]);
						}
					}
				}
			}
			return assemblyNameReference;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C972 File Offset: 0x0000AB72
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000C97A File Offset: 0x0000AB7A
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hash_algorithm;
			}
			set
			{
				this.hash_algorithm = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C983 File Offset: 0x0000AB83
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000C98B File Offset: 0x0000AB8B
		public virtual byte[] Hash
		{
			get
			{
				return this.hash;
			}
			set
			{
				this.hash = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C994 File Offset: 0x0000AB94
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C9A5 File Offset: 0x0000ABA5
		internal AssemblyNameReference()
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C9AD File Offset: 0x0000ABAD
		public AssemblyNameReference(string name, Version version)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.version = version;
			this.hash_algorithm = AssemblyHashAlgorithm.None;
			this.token = new MetadataToken(TokenType.AssemblyRef);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000398 RID: 920
		private string name;

		// Token: 0x04000399 RID: 921
		private string culture;

		// Token: 0x0400039A RID: 922
		private Version version;

		// Token: 0x0400039B RID: 923
		private uint attributes;

		// Token: 0x0400039C RID: 924
		private byte[] public_key;

		// Token: 0x0400039D RID: 925
		private byte[] public_key_token;

		// Token: 0x0400039E RID: 926
		private AssemblyHashAlgorithm hash_algorithm;

		// Token: 0x0400039F RID: 927
		private byte[] hash;

		// Token: 0x040003A0 RID: 928
		internal MetadataToken token;

		// Token: 0x040003A1 RID: 929
		private string full_name;
	}
}
