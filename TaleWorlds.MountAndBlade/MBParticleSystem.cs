using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D3 RID: 467
	public struct MBParticleSystem
	{
		// Token: 0x06001A91 RID: 6801 RVA: 0x0005CF40 File Offset: 0x0005B140
		internal MBParticleSystem(int i)
		{
			this.index = i;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0005CF49 File Offset: 0x0005B149
		public bool Equals(MBParticleSystem a)
		{
			return this.index == a.index;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0005CF59 File Offset: 0x0005B159
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x04000846 RID: 2118
		private int index;
	}
}
