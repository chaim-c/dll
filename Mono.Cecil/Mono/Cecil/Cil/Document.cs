using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200000A RID: 10
	public sealed class Document
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004496 File Offset: 0x00002696
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000449E File Offset: 0x0000269E
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000044A7 File Offset: 0x000026A7
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000044AF File Offset: 0x000026AF
		public DocumentType Type
		{
			get
			{
				return (DocumentType)this.type;
			}
			set
			{
				this.type = (byte)value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000044B9 File Offset: 0x000026B9
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000044C1 File Offset: 0x000026C1
		public DocumentHashAlgorithm HashAlgorithm
		{
			get
			{
				return (DocumentHashAlgorithm)this.hash_algorithm;
			}
			set
			{
				this.hash_algorithm = (byte)value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000044CB File Offset: 0x000026CB
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000044D3 File Offset: 0x000026D3
		public DocumentLanguage Language
		{
			get
			{
				return (DocumentLanguage)this.language;
			}
			set
			{
				this.language = (byte)value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000044DD File Offset: 0x000026DD
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000044E5 File Offset: 0x000026E5
		public DocumentLanguageVendor LanguageVendor
		{
			get
			{
				return (DocumentLanguageVendor)this.language_vendor;
			}
			set
			{
				this.language_vendor = (byte)value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000044EF File Offset: 0x000026EF
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000044F7 File Offset: 0x000026F7
		public byte[] Hash
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

		// Token: 0x0600007D RID: 125 RVA: 0x00004500 File Offset: 0x00002700
		public Document(string url)
		{
			this.url = url;
			this.hash = Empty<byte>.Array;
		}

		// Token: 0x04000103 RID: 259
		private string url;

		// Token: 0x04000104 RID: 260
		private byte type;

		// Token: 0x04000105 RID: 261
		private byte hash_algorithm;

		// Token: 0x04000106 RID: 262
		private byte language;

		// Token: 0x04000107 RID: 263
		private byte language_vendor;

		// Token: 0x04000108 RID: 264
		private byte[] hash;
	}
}
