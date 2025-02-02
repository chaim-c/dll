using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000025 RID: 37
	public abstract class Material
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007096 File Offset: 0x00005296
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000709E File Offset: 0x0000529E
		public bool Blending { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000070A7 File Offset: 0x000052A7
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000070AF File Offset: 0x000052AF
		public int RenderOrder { get; private set; }

		// Token: 0x0600015D RID: 349 RVA: 0x000070B8 File Offset: 0x000052B8
		protected Material(bool blending, int renderOrder)
		{
			this.Blending = blending;
			this.RenderOrder = renderOrder;
		}
	}
}
