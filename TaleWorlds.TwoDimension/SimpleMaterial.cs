using System;
using System.Numerics;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000027 RID: 39
	public class SimpleMaterial : Material
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007105 File Offset: 0x00005305
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000710D File Offset: 0x0000530D
		public Texture Texture { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007116 File Offset: 0x00005316
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000711E File Offset: 0x0000531E
		public Color Color { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007127 File Offset: 0x00005327
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000712F File Offset: 0x0000532F
		public float ColorFactor { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007138 File Offset: 0x00005338
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00007140 File Offset: 0x00005340
		public float AlphaFactor { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007149 File Offset: 0x00005349
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00007151 File Offset: 0x00005351
		public float HueFactor { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000715A File Offset: 0x0000535A
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00007162 File Offset: 0x00005362
		public float SaturationFactor { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000716B File Offset: 0x0000536B
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00007173 File Offset: 0x00005373
		public float ValueFactor { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000717C File Offset: 0x0000537C
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00007184 File Offset: 0x00005384
		public bool CircularMaskingEnabled { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000718D File Offset: 0x0000538D
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00007195 File Offset: 0x00005395
		public Vector2 CircularMaskingCenter { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000719E File Offset: 0x0000539E
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000071A6 File Offset: 0x000053A6
		public float CircularMaskingRadius { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000071AF File Offset: 0x000053AF
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000071B7 File Offset: 0x000053B7
		public float CircularMaskingSmoothingRadius { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000071C0 File Offset: 0x000053C0
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000071C8 File Offset: 0x000053C8
		public bool OverlayEnabled { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000071D1 File Offset: 0x000053D1
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000071D9 File Offset: 0x000053D9
		public Vector2 StartCoordinate { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000071E2 File Offset: 0x000053E2
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000071EA File Offset: 0x000053EA
		public Vector2 Size { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000071F3 File Offset: 0x000053F3
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000071FB File Offset: 0x000053FB
		public Texture OverlayTexture { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007204 File Offset: 0x00005404
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000720C File Offset: 0x0000540C
		public bool UseOverlayAlphaAsMask { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00007215 File Offset: 0x00005415
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000721D File Offset: 0x0000541D
		public float Scale { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00007226 File Offset: 0x00005426
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000722E File Offset: 0x0000542E
		public float OverlayTextureWidth { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007237 File Offset: 0x00005437
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000723F File Offset: 0x0000543F
		public float OverlayTextureHeight { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007248 File Offset: 0x00005448
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00007250 File Offset: 0x00005450
		public float OverlayXOffset { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007259 File Offset: 0x00005459
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007261 File Offset: 0x00005461
		public float OverlayYOffset { get; set; }

		// Token: 0x0600018D RID: 397 RVA: 0x0000726A File Offset: 0x0000546A
		public SimpleMaterial() : this(null, 0)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007274 File Offset: 0x00005474
		public SimpleMaterial(Texture texture) : this(texture, 0)
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000727E File Offset: 0x0000547E
		public SimpleMaterial(Texture texture, int renderOrder) : this(texture, renderOrder, true)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007289 File Offset: 0x00005489
		public SimpleMaterial(Texture texture, int renderOrder, bool blending) : base(blending, renderOrder)
		{
			this.Reset(texture);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000729C File Offset: 0x0000549C
		public void Reset(Texture texture = null)
		{
			this.Texture = texture;
			this.ColorFactor = 1f;
			this.AlphaFactor = 1f;
			this.HueFactor = 0f;
			this.SaturationFactor = 0f;
			this.ValueFactor = 0f;
			this.Color = new Color(1f, 1f, 1f, 1f);
			this.CircularMaskingEnabled = false;
			this.OverlayEnabled = false;
			this.OverlayTextureWidth = 512f;
			this.OverlayTextureHeight = 512f;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000732A File Offset: 0x0000552A
		public Vec2 GetCircularMaskingCenter()
		{
			return this.CircularMaskingCenter;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007337 File Offset: 0x00005537
		public Vec2 GetOverlayStartCoordinate()
		{
			return this.StartCoordinate;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007344 File Offset: 0x00005544
		public Vec2 GetOverlaySize()
		{
			return this.Size;
		}
	}
}
