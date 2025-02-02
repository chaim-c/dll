using System;
using System.Numerics;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000021 RID: 33
	public interface IText
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000139 RID: 313
		// (set) Token: 0x0600013A RID: 314
		string Value { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013B RID: 315
		// (set) Token: 0x0600013C RID: 316
		TextHorizontalAlignment HorizontalAlignment { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		TextVerticalAlignment VerticalAlignment { get; set; }

		// Token: 0x0600013F RID: 319
		Vector2 GetPreferredSize(bool fixedWidth, float widthSize, bool fixedHeight, float heightSize, SpriteData spriteData, float renderScale);
	}
}
