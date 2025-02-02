using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000118 RID: 280
	public class DisconnectInfo
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000830B File Offset: 0x0000650B
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00008313 File Offset: 0x00006513
		public DisconnectType Type { get; set; }

		// Token: 0x06000627 RID: 1575 RVA: 0x0000831C File Offset: 0x0000651C
		public DisconnectInfo()
		{
			this.Type = DisconnectType.Unknown;
		}
	}
}
