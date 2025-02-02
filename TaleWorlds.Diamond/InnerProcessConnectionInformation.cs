using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000011 RID: 17
	public sealed class InnerProcessConnectionInformation : IConnectionInformation
	{
		// Token: 0x06000052 RID: 82 RVA: 0x0000290E File Offset: 0x00000B0E
		string IConnectionInformation.GetAddress(bool isIpv6Compatible)
		{
			return "InnerProcess";
		}
	}
}
