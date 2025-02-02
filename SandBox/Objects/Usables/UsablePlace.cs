using System;
using SandBox.AI;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.Usables
{
	// Token: 0x0200003E RID: 62
	public class UsablePlace : UsableMachine
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000DC81 File Offset: 0x0000BE81
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			StandingPoint pilotStandingPoint = base.PilotStandingPoint;
			if (pilotStandingPoint == null)
			{
				return null;
			}
			return pilotStandingPoint.DescriptionMessage.ToString();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000DC99 File Offset: 0x0000BE99
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			StandingPoint pilotStandingPoint = base.PilotStandingPoint;
			if (pilotStandingPoint == null)
			{
				return null;
			}
			return pilotStandingPoint.ActionMessage;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new UsablePlaceAI(this);
		}
	}
}
