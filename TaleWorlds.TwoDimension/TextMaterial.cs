using System;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000028 RID: 40
	public class TextMaterial : Material
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007351 File Offset: 0x00005551
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00007359 File Offset: 0x00005559
		public Texture Texture { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007362 File Offset: 0x00005562
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000736A File Offset: 0x0000556A
		public Color Color { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007373 File Offset: 0x00005573
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000737B File Offset: 0x0000557B
		public float SmoothingConstant { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007384 File Offset: 0x00005584
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000738C File Offset: 0x0000558C
		public bool Smooth { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007395 File Offset: 0x00005595
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000739D File Offset: 0x0000559D
		public float ScaleFactor { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000073A6 File Offset: 0x000055A6
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000073AE File Offset: 0x000055AE
		public Color GlowColor { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000073B7 File Offset: 0x000055B7
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000073BF File Offset: 0x000055BF
		public Color OutlineColor { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000073C8 File Offset: 0x000055C8
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000073D0 File Offset: 0x000055D0
		public float OutlineAmount { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000073D9 File Offset: 0x000055D9
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000073E1 File Offset: 0x000055E1
		public float GlowRadius { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000073EA File Offset: 0x000055EA
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000073F2 File Offset: 0x000055F2
		public float Blur { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000073FB File Offset: 0x000055FB
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00007403 File Offset: 0x00005603
		public float ShadowOffset { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000740C File Offset: 0x0000560C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00007414 File Offset: 0x00005614
		public float ShadowAngle { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000741D File Offset: 0x0000561D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00007425 File Offset: 0x00005625
		public float ColorFactor { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000742E File Offset: 0x0000562E
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00007436 File Offset: 0x00005636
		public float AlphaFactor { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000743F File Offset: 0x0000563F
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00007447 File Offset: 0x00005647
		public float HueFactor { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007450 File Offset: 0x00005650
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00007458 File Offset: 0x00005658
		public float SaturationFactor { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007461 File Offset: 0x00005661
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00007469 File Offset: 0x00005669
		public float ValueFactor { get; set; }

		// Token: 0x060001B7 RID: 439 RVA: 0x00007472 File Offset: 0x00005672
		public TextMaterial() : this(null, 0)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000747C File Offset: 0x0000567C
		public TextMaterial(Texture texture) : this(texture, 0)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007486 File Offset: 0x00005686
		public TextMaterial(Texture texture, int renderOrder) : this(texture, renderOrder, true)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007494 File Offset: 0x00005694
		public TextMaterial(Texture texture, int renderOrder, bool blending) : base(blending, renderOrder)
		{
			this.Texture = texture;
			this.ScaleFactor = 1f;
			this.SmoothingConstant = 0.47f;
			this.Smooth = true;
			this.Color = new Color(1f, 1f, 1f, 1f);
			this.GlowColor = new Color(0f, 0f, 0f, 1f);
			this.OutlineColor = new Color(0f, 0f, 0f, 1f);
			this.OutlineAmount = 0f;
			this.GlowRadius = 0f;
			this.Blur = 0f;
			this.ShadowOffset = 0f;
			this.ShadowAngle = 0f;
			this.ColorFactor = 1f;
			this.AlphaFactor = 1f;
			this.HueFactor = 0f;
			this.SaturationFactor = 0f;
			this.ValueFactor = 0f;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007598 File Offset: 0x00005798
		public void CopyFrom(TextMaterial sourceMaterial)
		{
			this.Texture = sourceMaterial.Texture;
			this.Color = sourceMaterial.Color;
			this.ScaleFactor = sourceMaterial.ScaleFactor;
			this.SmoothingConstant = sourceMaterial.SmoothingConstant;
			this.Smooth = sourceMaterial.Smooth;
			this.GlowColor = sourceMaterial.GlowColor;
			this.OutlineColor = sourceMaterial.OutlineColor;
			this.OutlineAmount = sourceMaterial.OutlineAmount;
			this.GlowRadius = sourceMaterial.GlowRadius;
			this.Blur = sourceMaterial.Blur;
			this.ShadowOffset = sourceMaterial.ShadowOffset;
			this.ShadowAngle = sourceMaterial.ShadowAngle;
			this.ColorFactor = sourceMaterial.ColorFactor;
			this.AlphaFactor = sourceMaterial.AlphaFactor;
			this.HueFactor = sourceMaterial.HueFactor;
			this.SaturationFactor = sourceMaterial.SaturationFactor;
			this.ValueFactor = sourceMaterial.ValueFactor;
		}
	}
}
