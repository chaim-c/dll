using System;

namespace TaleWorlds.MountAndBlade.DividableTasks
{
	// Token: 0x020003D0 RID: 976
	public class FormationSearchThreatTask : DividableTask
	{
		// Token: 0x0600339C RID: 13212 RVA: 0x000D5C04 File Offset: 0x000D3E04
		protected override bool UpdateExtra()
		{
			this._result = this._formation.HasUnitWithConditionLimitedRandom((Agent agent) => this._weapon.CanShootAtAgent(agent), this._storedIndex, this._checkCountPerTick, out this._targetAgent);
			this._storedIndex += this._checkCountPerTick;
			return this._storedIndex >= this._formation.CountOfUnits || this._result;
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000D5C6E File Offset: 0x000D3E6E
		public void Prepare(Formation formation, RangedSiegeWeapon weapon)
		{
			base.ResetTaskStatus();
			this._formation = formation;
			this._weapon = weapon;
			this._storedIndex = 0;
			this._checkCountPerTick = (int)((float)this._formation.CountOfUnits * 0.1f) + 1;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000D5CA6 File Offset: 0x000D3EA6
		public bool GetResult(out Agent targetAgent)
		{
			targetAgent = this._targetAgent;
			return this._result;
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000D5CB6 File Offset: 0x000D3EB6
		public FormationSearchThreatTask() : base(null)
		{
		}

		// Token: 0x04001658 RID: 5720
		private Agent _targetAgent;

		// Token: 0x04001659 RID: 5721
		private const float CheckCountRatio = 0.1f;

		// Token: 0x0400165A RID: 5722
		private RangedSiegeWeapon _weapon;

		// Token: 0x0400165B RID: 5723
		private Formation _formation;

		// Token: 0x0400165C RID: 5724
		private int _storedIndex;

		// Token: 0x0400165D RID: 5725
		private int _checkCountPerTick;

		// Token: 0x0400165E RID: 5726
		private bool _result;
	}
}
