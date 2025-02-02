using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CC RID: 716
	public class MonsterMissionData : IMonsterMissionData
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x0009917A File Offset: 0x0009737A
		// (set) Token: 0x060027AD RID: 10157 RVA: 0x00099182 File Offset: 0x00097382
		public Monster Monster { get; private set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x0009918B File Offset: 0x0009738B
		public CapsuleData BodyCapsule
		{
			get
			{
				return new CapsuleData(this.Monster.BodyCapsuleRadius, this.Monster.BodyCapsulePoint1, this.Monster.BodyCapsulePoint2);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x000991B3 File Offset: 0x000973B3
		public CapsuleData CrouchedBodyCapsule
		{
			get
			{
				return new CapsuleData(this.Monster.CrouchedBodyCapsuleRadius, this.Monster.CrouchedBodyCapsulePoint1, this.Monster.CrouchedBodyCapsulePoint2);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x000991DB File Offset: 0x000973DB
		public MBActionSet ActionSet
		{
			get
			{
				if (!this._actionSet.IsValid && !string.IsNullOrEmpty(this.Monster.ActionSetCode))
				{
					this._actionSet = MBActionSet.GetActionSet(this.Monster.ActionSetCode);
				}
				return this._actionSet;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x00099218 File Offset: 0x00097418
		public MBActionSet FemaleActionSet
		{
			get
			{
				if (!this._femaleActionSet.IsValid && !string.IsNullOrEmpty(this.Monster.FemaleActionSetCode))
				{
					this._femaleActionSet = MBActionSet.GetActionSet(this.Monster.FemaleActionSetCode);
				}
				return this._femaleActionSet;
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00099255 File Offset: 0x00097455
		public MonsterMissionData(Monster monster)
		{
			this._actionSet = MBActionSet.InvalidActionSet;
			this._femaleActionSet = MBActionSet.InvalidActionSet;
			this.Monster = monster;
		}

		// Token: 0x04000EAC RID: 3756
		private MBActionSet _actionSet;

		// Token: 0x04000EAD RID: 3757
		private MBActionSet _femaleActionSet;
	}
}
