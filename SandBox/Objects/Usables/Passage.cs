using System;
using SandBox.AI;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.Usables
{
	// Token: 0x0200003B RID: 59
	public class Passage : UsableMachine
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000D48C File Offset: 0x0000B68C
		public Location ToLocation
		{
			get
			{
				PassageUsePoint passageUsePoint;
				if ((passageUsePoint = (base.PilotStandingPoint as PassageUsePoint)) == null)
				{
					return null;
				}
				return passageUsePoint.ToLocation;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			StandingPoint pilotStandingPoint = base.PilotStandingPoint;
			if (pilotStandingPoint == null)
			{
				return null;
			}
			return pilotStandingPoint.DescriptionMessage.ToString();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			StandingPoint pilotStandingPoint = base.PilotStandingPoint;
			if (pilotStandingPoint == null)
			{
				return null;
			}
			return pilotStandingPoint.ActionMessage;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000D4DB File Offset: 0x0000B6DB
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new PassageAI(this);
		}
	}
}
