using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000292 RID: 658
	public interface IRoundComponent : IMissionBehavior
	{
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002270 RID: 8816
		// (remove) Token: 0x06002271 RID: 8817
		event Action OnRoundStarted;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06002272 RID: 8818
		// (remove) Token: 0x06002273 RID: 8819
		event Action OnPreparationEnded;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002274 RID: 8820
		// (remove) Token: 0x06002275 RID: 8821
		event Action OnPreRoundEnding;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002276 RID: 8822
		// (remove) Token: 0x06002277 RID: 8823
		event Action OnRoundEnding;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06002278 RID: 8824
		// (remove) Token: 0x06002279 RID: 8825
		event Action OnPostRoundEnded;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600227A RID: 8826
		// (remove) Token: 0x0600227B RID: 8827
		event Action OnCurrentRoundStateChanged;

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600227C RID: 8828
		float LastRoundEndRemainingTime { get; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600227D RID: 8829
		float RemainingRoundTime { get; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600227E RID: 8830
		MultiplayerRoundState CurrentRoundState { get; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600227F RID: 8831
		int RoundCount { get; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002280 RID: 8832
		BattleSideEnum RoundWinner { get; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002281 RID: 8833
		RoundEndReason RoundEndReason { get; }
	}
}
