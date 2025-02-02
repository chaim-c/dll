using System;
using System.Linq;
using SandBox.AI;
using SandBox.Objects.AnimationPoints;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.Usables
{
	// Token: 0x02000039 RID: 57
	public class Chair : UsableMachine
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000CE1C File Offset: 0x0000B01C
		protected override void OnInit()
		{
			base.OnInit();
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				standingPoint.AutoSheathWeapons = true;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000CE74 File Offset: 0x0000B074
		public bool IsAgentFullySitting(Agent usingAgent)
		{
			return base.StandingPoints.Count > 0 && base.StandingPoints.Contains(usingAgent.CurrentlyUsedGameObject) && usingAgent.IsSitting();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CE9F File Offset: 0x0000B09F
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new UsablePlaceAI(this);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CEA7 File Offset: 0x0000B0A7
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = new TextObject(this.IsAgentFullySitting(Agent.Main) ? "{=QGdaakYW}{KEY} Get Up" : "{=bl2aRW8f}{KEY} Sit", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			switch (this.ChairType)
			{
			case Chair.SittableType.Log:
				return new TextObject("{=9pgOGq7X}Log", null).ToString();
			case Chair.SittableType.Sofa:
				return new TextObject("{=GvLZKQ1U}Sofa", null).ToString();
			case Chair.SittableType.Ground:
				return new TextObject("{=L7ZQtIuM}Ground", null).ToString();
			default:
				return new TextObject("{=OgTUrRlR}Chair", null).ToString();
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CF58 File Offset: 0x0000B158
		public override StandingPoint GetBestPointAlternativeTo(StandingPoint standingPoint, Agent agent)
		{
			AnimationPoint animationPoint = standingPoint as AnimationPoint;
			if (animationPoint == null || animationPoint.GroupId < 0)
			{
				return animationPoint;
			}
			WorldFrame userFrameForAgent = standingPoint.GetUserFrameForAgent(agent);
			float num = userFrameForAgent.Origin.GetGroundVec3().DistanceSquared(agent.Position);
			foreach (StandingPoint standingPoint2 in base.StandingPoints)
			{
				AnimationPoint animationPoint2;
				if ((animationPoint2 = (standingPoint2 as AnimationPoint)) != null && standingPoint != standingPoint2 && animationPoint.GroupId == animationPoint2.GroupId && !animationPoint2.IsDisabledForAgent(agent))
				{
					userFrameForAgent = animationPoint2.GetUserFrameForAgent(agent);
					float num2 = userFrameForAgent.Origin.GetGroundVec3().DistanceSquared(agent.Position);
					if (num2 < num)
					{
						num = num2;
						animationPoint = animationPoint2;
					}
				}
			}
			return animationPoint;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000D03C File Offset: 0x0000B23C
		public override OrderType GetOrder(BattleSideEnum side)
		{
			return OrderType.None;
		}

		// Token: 0x040000B9 RID: 185
		public Chair.SittableType ChairType;

		// Token: 0x02000117 RID: 279
		public enum SittableType
		{
			// Token: 0x040004F3 RID: 1267
			Chair,
			// Token: 0x040004F4 RID: 1268
			Log,
			// Token: 0x040004F5 RID: 1269
			Sofa,
			// Token: 0x040004F6 RID: 1270
			Ground
		}
	}
}
