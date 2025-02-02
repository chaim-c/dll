using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000121 RID: 289
	internal class BannerlordConfig
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00008767 File Offset: 0x00006967
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0000876F File Offset: 0x0000696F
		public int AdmittancePercentage { get; private set; }

		// Token: 0x06000673 RID: 1651 RVA: 0x00008778 File Offset: 0x00006978
		public BannerlordConfig(int admittancePercentage)
		{
			this.AdmittancePercentage = admittancePercentage;
		}
	}
}
