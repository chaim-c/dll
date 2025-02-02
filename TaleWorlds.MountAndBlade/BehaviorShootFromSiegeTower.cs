using System;
using System.Linq;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200012C RID: 300
	public class BehaviorShootFromSiegeTower : BehaviorComponent
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x00023C67 File Offset: 0x00021E67
		public BehaviorShootFromSiegeTower(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this._siegeTower = Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeTower>().FirstOrDefault((SiegeTower st) => st.WeaponSide == this._behaviorSide);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00023CA8 File Offset: 0x00021EA8
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (base.Formation.AI.Side != this._behaviorSide)
			{
				this._behaviorSide = base.Formation.AI.Side;
				this._siegeTower = Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeTower>().FirstOrDefault((SiegeTower st) => st.WeaponSide == this._behaviorSide);
			}
			if (this._siegeTower == null || this._siegeTower.IsDestroyed)
			{
				return;
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00023D36 File Offset: 0x00021F36
		protected override float GetAiWeight()
		{
			return 0f;
		}

		// Token: 0x04000368 RID: 872
		private SiegeTower _siegeTower;
	}
}
