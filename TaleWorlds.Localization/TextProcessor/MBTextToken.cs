using System;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	internal class MBTextToken
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00005E3D File Offset: 0x0000403D
		internal MBTextToken(TokenType tokenType)
		{
			this.TokenType = tokenType;
			this.Value = string.Empty;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005E57 File Offset: 0x00004057
		internal MBTextToken(TokenType tokenType, string value)
		{
			this.TokenType = tokenType;
			this.Value = value;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005E6D File Offset: 0x0000406D
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005E75 File Offset: 0x00004075
		internal TokenType TokenType { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005E7E File Offset: 0x0000407E
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00005E86 File Offset: 0x00004086
		public string Value { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00005E8F File Offset: 0x0000408F
		public MBTextToken Clone()
		{
			return new MBTextToken(this.TokenType, this.Value);
		}
	}
}
