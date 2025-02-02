using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200025B RID: 603
	public interface ICommanderInfo : IMissionBehavior
	{
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600200A RID: 8202
		// (remove) Token: 0x0600200B RID: 8203
		event Action<BattleSideEnum, float> OnMoraleChangedEvent;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600200C RID: 8204
		// (remove) Token: 0x0600200D RID: 8205
		event Action OnFlagNumberChangedEvent;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600200E RID: 8206
		// (remove) Token: 0x0600200F RID: 8207
		event Action<FlagCapturePoint, Team> OnCapturePointOwnerChangedEvent;

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002010 RID: 8208
		IEnumerable<FlagCapturePoint> AllCapturePoints { get; }

		// Token: 0x06002011 RID: 8209
		Team GetFlagOwner(FlagCapturePoint flag);

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002012 RID: 8210
		bool AreMoralesIndependent { get; }
	}
}
