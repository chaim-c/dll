using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.DividableTasks
{
	// Token: 0x020003CF RID: 975
	public class FindMostDangerousThreat : DividableTask
	{
		// Token: 0x06003397 RID: 13207 RVA: 0x000D59AE File Offset: 0x000D3BAE
		public FindMostDangerousThreat(DividableTask continueToTask = null) : base(continueToTask)
		{
			base.SetTaskFinished(false);
			this._formationSearchThreatTask = new FormationSearchThreatTask();
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000D59CC File Offset: 0x000D3BCC
		protected override bool UpdateExtra()
		{
			bool flag = false;
			if (this._hasOngoingThreatTask)
			{
				if (this._formationSearchThreatTask.Update())
				{
					this._hasOngoingThreatTask = false;
					if (!(flag = this._formationSearchThreatTask.GetResult(out this._targetAgent)))
					{
						this._threats.Remove(this._currentThreat);
						this._currentThreat = null;
					}
				}
			}
			else
			{
				for (;;)
				{
					flag = true;
					int num = -1;
					float num2 = float.MinValue;
					for (int i = 0; i < this._threats.Count; i++)
					{
						Threat threat = this._threats[i];
						if (threat.ThreatValue > num2)
						{
							num2 = threat.ThreatValue;
							num = i;
						}
					}
					if (num >= 0)
					{
						this._currentThreat = this._threats[num];
						if (this._currentThreat.Formation != null)
						{
							break;
						}
						if ((this._currentThreat.WeaponEntity == null && this._currentThreat.Agent == null) || !this._weapon.CanShootAtThreat(this._currentThreat))
						{
							this._currentThreat = null;
							this._threats.RemoveAt(num);
							flag = false;
						}
					}
					if (flag)
					{
						goto IL_12E;
					}
				}
				this._formationSearchThreatTask.Prepare(this._currentThreat.Formation, this._weapon);
				this._hasOngoingThreatTask = true;
				flag = false;
			}
			IL_12E:
			return flag || this._threats.Count == 0;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000D5B1C File Offset: 0x000D3D1C
		public void Prepare(List<Threat> threats, RangedSiegeWeapon weapon)
		{
			base.ResetTaskStatus();
			this._hasOngoingThreatTask = false;
			this._weapon = weapon;
			this._threats = threats;
			foreach (Threat threat in this._threats)
			{
				threat.ThreatValue *= 0.9f + MBRandom.RandomFloat * 0.2f;
			}
			if (this._currentThreat != null)
			{
				this._currentThreat = this._threats.SingleOrDefault((Threat t) => t.Equals(this._currentThreat));
				if (this._currentThreat != null)
				{
					this._currentThreat.ThreatValue *= 2f;
				}
			}
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000D5BE4 File Offset: 0x000D3DE4
		public Threat GetResult(out Agent targetAgent)
		{
			targetAgent = this._targetAgent;
			return this._currentThreat;
		}

		// Token: 0x04001652 RID: 5714
		private Agent _targetAgent;

		// Token: 0x04001653 RID: 5715
		private FormationSearchThreatTask _formationSearchThreatTask;

		// Token: 0x04001654 RID: 5716
		private List<Threat> _threats;

		// Token: 0x04001655 RID: 5717
		private RangedSiegeWeapon _weapon;

		// Token: 0x04001656 RID: 5718
		private Threat _currentThreat;

		// Token: 0x04001657 RID: 5719
		private bool _hasOngoingThreatTask;
	}
}
