using System;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200004D RID: 77
	public class HeroSkillHandler : MissionLogic
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00012CCA File Offset: 0x00010ECA
		public override void AfterStart()
		{
			this._nextCaptainSkillMoraleBoostTime = MissionTime.SecondsFromNow(10f);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00012CDC File Offset: 0x00010EDC
		public override void OnMissionTick(float dt)
		{
			if (this._nextCaptainSkillMoraleBoostTime.IsPast)
			{
				this._boostMorale = true;
				this._nextMoraleTeam = 0;
				this._nextCaptainSkillMoraleBoostTime = MissionTime.SecondsFromNow(10f);
			}
			if (this._boostMorale)
			{
				if (this._nextMoraleTeam >= base.Mission.Teams.Count)
				{
					this._boostMorale = false;
					return;
				}
				Team team = base.Mission.Teams[this._nextMoraleTeam];
				this.BoostMoraleForTeam(team);
				this._nextMoraleTeam++;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00012D68 File Offset: 0x00010F68
		private void BoostMoraleForTeam(Team team)
		{
		}

		// Token: 0x0400015A RID: 346
		private MissionTime _nextCaptainSkillMoraleBoostTime;

		// Token: 0x0400015B RID: 347
		private bool _boostMorale;

		// Token: 0x0400015C RID: 348
		private int _nextMoraleTeam;
	}
}
