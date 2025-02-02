using System;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.NameMarker
{
	// Token: 0x020000EC RID: 236
	public class MarkerRect
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002223F File Offset: 0x0002043F
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x00022247 File Offset: 0x00020447
		public float Left { get; private set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00022250 File Offset: 0x00020450
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00022258 File Offset: 0x00020458
		public float Right { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00022261 File Offset: 0x00020461
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00022269 File Offset: 0x00020469
		public float Top { get; private set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00022272 File Offset: 0x00020472
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0002227A File Offset: 0x0002047A
		public float Bottom { get; private set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00022283 File Offset: 0x00020483
		public float CenterX
		{
			get
			{
				return this.Left + (this.Right - this.Left) / 2f;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0002229F File Offset: 0x0002049F
		public float CenterY
		{
			get
			{
				return this.Top + (this.Bottom - this.Top) / 2f;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x000222BB File Offset: 0x000204BB
		public float Width
		{
			get
			{
				return this.Right - this.Left;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000222CA File Offset: 0x000204CA
		public float Height
		{
			get
			{
				return this.Bottom - this.Top;
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000222D9 File Offset: 0x000204D9
		public MarkerRect()
		{
			this.Reset();
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000222E7 File Offset: 0x000204E7
		public void Reset()
		{
			this.Left = 0f;
			this.Right = 0f;
			this.Top = 0f;
			this.Bottom = 0f;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00022315 File Offset: 0x00020515
		public void UpdatePoints(float left, float right, float top, float bottom)
		{
			this.Left = left;
			this.Right = right;
			this.Top = top;
			this.Bottom = bottom;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00022334 File Offset: 0x00020534
		public bool IsOverlapping(MarkerRect other)
		{
			return other.Left <= this.Right && other.Right >= this.Left && other.Top <= this.Bottom && other.Bottom >= this.Top;
		}
	}
}
