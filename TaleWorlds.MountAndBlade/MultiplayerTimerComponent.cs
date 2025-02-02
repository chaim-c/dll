using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B2 RID: 690
	public class MultiplayerTimerComponent : MissionNetwork
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x0008FC70 File Offset: 0x0008DE70
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x0008FC78 File Offset: 0x0008DE78
		public bool IsTimerRunning { get; private set; }

		// Token: 0x060025BC RID: 9660 RVA: 0x0008FC81 File Offset: 0x0008DE81
		public void StartTimerAsServer(float duration)
		{
			this._missionTimer = new MissionTimer(duration);
			this.IsTimerRunning = true;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0008FC96 File Offset: 0x0008DE96
		public void StartTimerAsClient(float startTime, float duration)
		{
			this._missionTimer = MissionTimer.CreateSynchedTimerClient(startTime, duration);
			this.IsTimerRunning = true;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0008FCAC File Offset: 0x0008DEAC
		public float GetRemainingTime(bool isSynched)
		{
			if (!this.IsTimerRunning)
			{
				return 0f;
			}
			float remainingTimeInSeconds = this._missionTimer.GetRemainingTimeInSeconds(isSynched);
			if (isSynched)
			{
				return MathF.Min(remainingTimeInSeconds, this._missionTimer.GetTimerDuration());
			}
			return remainingTimeInSeconds;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0008FCEA File Offset: 0x0008DEEA
		public bool CheckIfTimerPassed()
		{
			return this.IsTimerRunning && this._missionTimer.Check(false);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0008FD02 File Offset: 0x0008DF02
		public MissionTime GetCurrentTimerStartTime()
		{
			return this._missionTimer.GetStartTime();
		}

		// Token: 0x04000E04 RID: 3588
		private MissionTimer _missionTimer;
	}
}
