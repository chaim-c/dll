using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200025C RID: 604
	public interface IAnalyticsFlagInfo : IMissionBehavior
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002013 RID: 8211
		MBReadOnlyList<FlagCapturePoint> AllCapturePoints { get; }

		// Token: 0x06002014 RID: 8212
		Team GetFlagOwnerTeam(FlagCapturePoint flag);
	}
}
