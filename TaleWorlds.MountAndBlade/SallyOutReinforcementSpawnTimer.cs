using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000287 RID: 647
	public class SallyOutReinforcementSpawnTimer : ICustomReinforcementSpawnTimer
	{
		// Token: 0x060021F3 RID: 8691 RVA: 0x0007C001 File Offset: 0x0007A201
		public SallyOutReinforcementSpawnTimer(float besiegedInterval, float besiegerInterval, float besiegerIntervalChange, int besiegerIntervalChangeCount)
		{
			this._besiegedSideTimer = new BasicMissionTimer();
			this._besiegedInterval = besiegedInterval;
			this._besiegerSideTimer = new BasicMissionTimer();
			this._besiegerInterval = besiegerInterval;
			this._besiegerIntervalChange = besiegerIntervalChange;
			this._besiegerRemainingIntervalChanges = besiegerIntervalChangeCount;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0007C03C File Offset: 0x0007A23C
		public bool Check(BattleSideEnum side)
		{
			if (side == BattleSideEnum.Attacker)
			{
				if (this._besiegerSideTimer.ElapsedTime >= this._besiegerInterval)
				{
					if (this._besiegerRemainingIntervalChanges > 0)
					{
						this._besiegerInterval -= this._besiegerIntervalChange;
						this._besiegerRemainingIntervalChanges--;
					}
					this._besiegerSideTimer.Reset();
					return true;
				}
			}
			else if (side == BattleSideEnum.Defender && this._besiegedSideTimer.ElapsedTime >= this._besiegedInterval)
			{
				this._besiegedSideTimer.Reset();
				return true;
			}
			return false;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0007C0BB File Offset: 0x0007A2BB
		public void ResetTimer(BattleSideEnum side)
		{
			if (side == BattleSideEnum.Attacker)
			{
				this._besiegerSideTimer.Reset();
				return;
			}
			if (side == BattleSideEnum.Defender)
			{
				this._besiegedSideTimer.Reset();
			}
		}

		// Token: 0x04000CB8 RID: 3256
		private BasicMissionTimer _besiegedSideTimer;

		// Token: 0x04000CB9 RID: 3257
		private BasicMissionTimer _besiegerSideTimer;

		// Token: 0x04000CBA RID: 3258
		private float _besiegedInterval;

		// Token: 0x04000CBB RID: 3259
		private float _besiegerInterval;

		// Token: 0x04000CBC RID: 3260
		private float _besiegerIntervalChange;

		// Token: 0x04000CBD RID: 3261
		private int _besiegerRemainingIntervalChanges;
	}
}
