using System;
using System.Numerics;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x02000051 RID: 81
	public abstract class CanvasLineElement : CanvasObject
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00016FD0 File Offset: 0x000151D0
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00016FD8 File Offset: 0x000151D8
		public CanvasLine Line { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00016FE1 File Offset: 0x000151E1
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00016FE9 File Offset: 0x000151E9
		public int SegmentIndex { get; set; }

		// Token: 0x06000550 RID: 1360 RVA: 0x00016FF2 File Offset: 0x000151F2
		protected CanvasLineElement(CanvasLine line, int segmentIndex, FontFactory fontFactory, SpriteData spriteData) : base(line, fontFactory, spriteData)
		{
			this.Line = line;
			this.SegmentIndex = segmentIndex;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001700C File Offset: 0x0001520C
		protected sealed override Vector2 Layout()
		{
			Vector2 zero = Vector2.Zero;
			zero.X = this.Line.GetHorizontalPositionOf(this.SegmentIndex);
			zero.Y = this.Line.Height - base.Height;
			return zero;
		}
	}
}
