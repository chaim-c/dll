using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000069 RID: 105
	[Flags]
	public enum TextFlags
	{
		// Token: 0x0400013B RID: 315
		RglTfHAlignLeft = 1,
		// Token: 0x0400013C RID: 316
		RglTfHAlignRight = 2,
		// Token: 0x0400013D RID: 317
		RglTfHAlignCenter = 3,
		// Token: 0x0400013E RID: 318
		RglTfVAlignTop = 4,
		// Token: 0x0400013F RID: 319
		RglTfVAlignDown = 8,
		// Token: 0x04000140 RID: 320
		RglTfVAlignCenter = 12,
		// Token: 0x04000141 RID: 321
		RglTfSingleLine = 16,
		// Token: 0x04000142 RID: 322
		RglTfMultiline = 32,
		// Token: 0x04000143 RID: 323
		RglTfItalic = 64,
		// Token: 0x04000144 RID: 324
		RglTfCutTextFromLeft = 128,
		// Token: 0x04000145 RID: 325
		RglTfDoubleSpace = 256,
		// Token: 0x04000146 RID: 326
		RglTfWithOutline = 512,
		// Token: 0x04000147 RID: 327
		RglTfHalfSpace = 1024
	}
}
