using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000010 RID: 16
	public interface IConnectionInformation
	{
		// Token: 0x06000050 RID: 80
		string GetAddress(bool isIpv6Compatible = false);
	}
}
