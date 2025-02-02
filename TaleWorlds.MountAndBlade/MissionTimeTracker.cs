using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000290 RID: 656
	public class MissionTimeTracker
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x0007D3A2 File Offset: 0x0007B5A2
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x0007D3AA File Offset: 0x0007B5AA
		public long NumberOfTicks { get; private set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0007D3B3 File Offset: 0x0007B5B3
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0007D3BB File Offset: 0x0007B5BB
		public long DeltaTimeInTicks { get; private set; }

		// Token: 0x0600226A RID: 8810 RVA: 0x0007D3C4 File Offset: 0x0007B5C4
		public MissionTimeTracker(MissionTime initialMapTime)
		{
			this.NumberOfTicks = initialMapTime.NumberOfTicks;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0007D3D9 File Offset: 0x0007B5D9
		public MissionTimeTracker()
		{
			this.NumberOfTicks = 0L;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0007D3E9 File Offset: 0x0007B5E9
		public void Tick(float seconds)
		{
			this.DeltaTimeInTicks = (long)(seconds * 10000000f);
			this.NumberOfTicks += this.DeltaTimeInTicks;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0007D40C File Offset: 0x0007B60C
		public void UpdateSync(float newValue)
		{
			long num = (long)(newValue * 10000000f);
			this._lastSyncDifference = num - this.NumberOfTicks;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0007D430 File Offset: 0x0007B630
		public float GetLastSyncDifference()
		{
			return (float)this._lastSyncDifference / 10000000f;
		}

		// Token: 0x04000CD5 RID: 3285
		private long _lastSyncDifference;
	}
}
