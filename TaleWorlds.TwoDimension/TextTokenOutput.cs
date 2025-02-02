using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000019 RID: 25
	internal class TextTokenOutput
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000063C7 File Offset: 0x000045C7
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000063CF File Offset: 0x000045CF
		public float X { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000063D8 File Offset: 0x000045D8
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000063E0 File Offset: 0x000045E0
		public float Y { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000063E9 File Offset: 0x000045E9
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000063F1 File Offset: 0x000045F1
		public float Width { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000063FA File Offset: 0x000045FA
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006402 File Offset: 0x00004602
		public float Height { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000640B File Offset: 0x0000460B
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006413 File Offset: 0x00004613
		public float Scale { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000641C File Offset: 0x0000461C
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00006424 File Offset: 0x00004624
		public Rectangle Rectangle { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000642D File Offset: 0x0000462D
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00006435 File Offset: 0x00004635
		public TextToken Token { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000643E File Offset: 0x0000463E
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006446 File Offset: 0x00004646
		public string Style { get; private set; }

		// Token: 0x060000FF RID: 255 RVA: 0x00006450 File Offset: 0x00004650
		public TextTokenOutput(TextToken token, float width, float height, string style, float scaleValue)
		{
			this.Token = token;
			this.Width = width;
			this.Height = height;
			this.Rectangle = new Rectangle(0f, 0f, this.Width, this.Height);
			this.Style = style;
			this.Scale = scaleValue;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000064A9 File Offset: 0x000046A9
		public void SetPosition(float x, float y)
		{
			this.X = x;
			this.Y = y;
			this.Rectangle = new Rectangle(x, y, this.Width, this.Height);
		}
	}
}
