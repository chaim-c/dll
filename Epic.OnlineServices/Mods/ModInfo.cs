using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F9 RID: 761
	public struct ModInfo
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0001E7BF File Offset: 0x0001C9BF
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0001E7C7 File Offset: 0x0001C9C7
		public ModIdentifier[] Mods { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		public ModEnumerationType Type { get; set; }

		// Token: 0x0600148E RID: 5262 RVA: 0x0001E7E1 File Offset: 0x0001C9E1
		internal void Set(ref ModInfoInternal other)
		{
			this.Mods = other.Mods;
			this.Type = other.Type;
		}
	}
}
