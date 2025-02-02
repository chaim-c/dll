using System;
using SandBox.AI;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.Usables
{
	// Token: 0x0200003C RID: 60
	public class PatrolArea : UsableMachine
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000D4EB File Offset: 0x0000B6EB
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000D4F3 File Offset: 0x0000B6F3
		private int ActiveIndex
		{
			get
			{
				return this._activeIndex;
			}
			set
			{
				if (this._activeIndex != value)
				{
					base.StandingPoints[value].IsDeactivated = false;
					base.StandingPoints[this._activeIndex].IsDeactivated = true;
					this._activeIndex = value;
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D52E File Offset: 0x0000B72E
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			StandingPoint pilotStandingPoint = base.PilotStandingPoint;
			if (pilotStandingPoint == null)
			{
				return null;
			}
			return pilotStandingPoint.ActionMessage;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000D541 File Offset: 0x0000B741
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			StandingPoint pilotStandingPoint = base.PilotStandingPoint;
			if (pilotStandingPoint == null)
			{
				return null;
			}
			return pilotStandingPoint.DescriptionMessage.ToString();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000D559 File Offset: 0x0000B759
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new UsablePlaceAI(this);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000D564 File Offset: 0x0000B764
		protected override void OnInit()
		{
			base.OnInit();
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				standingPoint.IsDeactivated = true;
			}
			this.ActiveIndex = base.StandingPoints.Count - 1;
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (base.StandingPoints[this.ActiveIndex].HasAIUser)
			{
				this.ActiveIndex = ((this.ActiveIndex == 0) ? (base.StandingPoints.Count - 1) : (this.ActiveIndex - 1));
			}
		}

		// Token: 0x040000C4 RID: 196
		public int AreaIndex;

		// Token: 0x040000C5 RID: 197
		private int _activeIndex;
	}
}
