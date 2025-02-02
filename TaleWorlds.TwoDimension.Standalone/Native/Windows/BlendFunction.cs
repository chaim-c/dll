using System;

namespace TaleWorlds.TwoDimension.Standalone.Native.Windows
{
	// Token: 0x02000015 RID: 21
	public struct BlendFunction
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00004E5D File Offset: 0x0000305D
		public BlendFunction(AlphaFormatFlags op, byte flags, byte alpha, AlphaFormatFlags format)
		{
			this.BlendOp = (byte)op;
			this.BlendFlags = flags;
			this.SourceConstantAlpha = alpha;
			this.AlphaFormat = (byte)format;
		}

		// Token: 0x0400006E RID: 110
		public byte BlendOp;

		// Token: 0x0400006F RID: 111
		public byte BlendFlags;

		// Token: 0x04000070 RID: 112
		public byte SourceConstantAlpha;

		// Token: 0x04000071 RID: 113
		public byte AlphaFormat;

		// Token: 0x04000072 RID: 114
		public static readonly BlendFunction Default = new BlendFunction(AlphaFormatFlags.Over, 0, byte.MaxValue, AlphaFormatFlags.Alpha);
	}
}
