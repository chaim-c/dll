using System;

namespace Mono.Cecil
{
	// Token: 0x02000035 RID: 53
	public struct MetadataToken
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007D65 File Offset: 0x00005F65
		public uint RID
		{
			get
			{
				return this.token & 16777215U;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007D73 File Offset: 0x00005F73
		public TokenType TokenType
		{
			get
			{
				return (TokenType)(this.token & 4278190080U);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007D81 File Offset: 0x00005F81
		public MetadataToken(uint token)
		{
			this.token = token;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007D8A File Offset: 0x00005F8A
		public MetadataToken(TokenType type)
		{
			this = new MetadataToken(type, 0);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007D94 File Offset: 0x00005F94
		public MetadataToken(TokenType type, uint rid)
		{
			this.token = (uint)(type | (TokenType)rid);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007D9F File Offset: 0x00005F9F
		public MetadataToken(TokenType type, int rid)
		{
			this.token = (uint)(type | (TokenType)rid);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007DAA File Offset: 0x00005FAA
		public int ToInt32()
		{
			return (int)this.token;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007DB2 File Offset: 0x00005FB2
		public uint ToUInt32()
		{
			return this.token;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007DBA File Offset: 0x00005FBA
		public override int GetHashCode()
		{
			return (int)this.token;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public override bool Equals(object obj)
		{
			return obj is MetadataToken && ((MetadataToken)obj).token == this.token;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007DF1 File Offset: 0x00005FF1
		public static bool operator ==(MetadataToken one, MetadataToken other)
		{
			return one.token == other.token;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007E03 File Offset: 0x00006003
		public static bool operator !=(MetadataToken one, MetadataToken other)
		{
			return one.token != other.token;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007E18 File Offset: 0x00006018
		public override string ToString()
		{
			return string.Format("[{0}:0x{1}]", this.TokenType, this.RID.ToString("x4"));
		}

		// Token: 0x040002B9 RID: 697
		private readonly uint token;

		// Token: 0x040002BA RID: 698
		public static readonly MetadataToken Zero = new MetadataToken(0U);
	}
}
