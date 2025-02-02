using System;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000133 RID: 307
	public class BehaviorUseMurderHole : BehaviorComponent
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x00026370 File Offset: 0x00024570
		public BehaviorUseMurderHole(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			WorldPosition position = new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, (formation.Team.TeamAI as TeamAISiegeDefender).MurderHolePosition, false);
			this._outerGate = (formation.Team.TeamAI as TeamAISiegeDefender).OuterGate;
			this._innerGate = (formation.Team.TeamAI as TeamAISiegeDefender).InnerGate;
			this._batteringRam = base.Formation.Team.Mission.ActiveMissionObjects.FindAllWithType<BatteringRam>().FirstOrDefault<BatteringRam>();
			base.CurrentOrder = MovementOrder.MovementOrderMove(position);
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0002643E File Offset: 0x0002463E
		public override void TickOccasionally()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00026454 File Offset: 0x00024654
		public bool IsMurderHoleActive()
		{
			return (this._batteringRam != null && this._batteringRam.HasArrivedAtTarget && !this._innerGate.IsDestroyed) || (this._outerGate.IsDestroyed && !this._innerGate.IsDestroyed);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000264A2 File Offset: 0x000246A2
		protected override float GetAiWeight()
		{
			return 10f * (this.IsMurderHoleActive() ? 1f : 0f);
		}

		// Token: 0x0400037E RID: 894
		private readonly CastleGate _outerGate;

		// Token: 0x0400037F RID: 895
		private readonly CastleGate _innerGate;

		// Token: 0x04000380 RID: 896
		private readonly BatteringRam _batteringRam;
	}
}
