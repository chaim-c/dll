using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000284 RID: 644
	public class SallyOutEndLogic : MissionLogic
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0007B219 File Offset: 0x00079419
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x0007B221 File Offset: 0x00079421
		public bool IsSallyOutOver { get; private set; }

		// Token: 0x060021CE RID: 8654 RVA: 0x0007B22C File Offset: 0x0007942C
		public override void OnMissionTick(float dt)
		{
			if (this.CheckTimer(dt))
			{
				if (this._checkState == SallyOutEndLogic.EndConditionCheckState.Deactive)
				{
					using (IEnumerator<Team> enumerator = (from t in base.Mission.Teams
					where t.Side == BattleSideEnum.Defender
					select t).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Team team = enumerator.Current;
							foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
							{
								if (formation.CountOfUnits > 0 && formation.CountOfUnits > 0 && !TeamAISiegeComponent.IsFormationInsideCastle(formation, true, 0.1f))
								{
									this._checkState = SallyOutEndLogic.EndConditionCheckState.Active;
									return;
								}
							}
						}
						return;
					}
				}
				if (this._checkState == SallyOutEndLogic.EndConditionCheckState.Idle)
				{
					this._checkState = SallyOutEndLogic.EndConditionCheckState.Active;
				}
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0007B324 File Offset: 0x00079524
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			if (this.IsSallyOutOver)
			{
				missionResult = MissionResult.CreateSuccessful(base.Mission, false);
				return true;
			}
			if (this._checkState != SallyOutEndLogic.EndConditionCheckState.Active)
			{
				return false;
			}
			foreach (Team team in base.Mission.Teams)
			{
				BattleSideEnum side = team.Side;
				if (side != BattleSideEnum.Defender)
				{
					if (side == BattleSideEnum.Attacker && TeamAISiegeComponent.IsFormationGroupInsideCastle(team.FormationsIncludingSpecialAndEmpty, false, 0.1f))
					{
						this._checkState = SallyOutEndLogic.EndConditionCheckState.Idle;
						return false;
					}
				}
				else if (team.FormationsIncludingEmpty.Any((Formation f) => f.CountOfUnits > 0 && !TeamAISiegeComponent.IsFormationInsideCastle(f, false, 0.9f)))
				{
					this._checkState = SallyOutEndLogic.EndConditionCheckState.Idle;
					return false;
				}
			}
			this.IsSallyOutOver = true;
			missionResult = MissionResult.CreateSuccessful(base.Mission, false);
			return true;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0007B414 File Offset: 0x00079614
		private bool CheckTimer(float dt)
		{
			this._dtSum += dt;
			if (this._dtSum < this._nextCheckTime)
			{
				return false;
			}
			this._dtSum = 0f;
			this._nextCheckTime = 0.8f + MBRandom.RandomFloat * 0.4f;
			return true;
		}

		// Token: 0x04000C98 RID: 3224
		private SallyOutEndLogic.EndConditionCheckState _checkState;

		// Token: 0x04000C9A RID: 3226
		private float _nextCheckTime;

		// Token: 0x04000C9B RID: 3227
		private float _dtSum;

		// Token: 0x02000537 RID: 1335
		private enum EndConditionCheckState
		{
			// Token: 0x04001C91 RID: 7313
			Deactive,
			// Token: 0x04001C92 RID: 7314
			Active,
			// Token: 0x04001C93 RID: 7315
			Idle
		}
	}
}
