using System;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000026 RID: 38
	public class PrimitivePolygonMaterial : Material
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000070CE File Offset: 0x000052CE
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000070D6 File Offset: 0x000052D6
		public Color Color { get; private set; }

		// Token: 0x06000160 RID: 352 RVA: 0x000070DF File Offset: 0x000052DF
		public PrimitivePolygonMaterial(Color color) : this(color, 0)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000070E9 File Offset: 0x000052E9
		public PrimitivePolygonMaterial(Color color, int renderOrder) : this(color, renderOrder, true)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000070F4 File Offset: 0x000052F4
		public PrimitivePolygonMaterial(Color color, int renderOrder, bool blending) : base(blending, renderOrder)
		{
			this.Color = color;
		}
	}
}
