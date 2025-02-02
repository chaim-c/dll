using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031F RID: 799
	public class ExitDoor : UsableMachine
	{
		// Token: 0x06002B0D RID: 11021 RVA: 0x000A6938 File Offset: 0x000A4B38
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = new TextObject("{=gqQPSAQZ}{KEY} Leave Area", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000A6962 File Offset: 0x000A4B62
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return string.Empty;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000A6969 File Offset: 0x000A4B69
		protected internal override void OnInit()
		{
			base.OnInit();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000A697D File Offset: 0x000A4B7D
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000A6988 File Offset: 0x000A4B88
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (standingPoint.HasUser)
				{
					Agent userAgent = standingPoint.UserAgent;
					ActionIndexValueCache currentActionValue = userAgent.GetCurrentActionValue(0);
					ActionIndexValueCache currentActionValue2 = userAgent.GetCurrentActionValue(1);
					if (!(currentActionValue2 == ActionIndexValueCache.act_none) || (!(currentActionValue == ExitDoor.act_pickup_middle_begin) && !(currentActionValue == ExitDoor.act_pickup_middle_begin_left_stance)))
					{
						if (currentActionValue2 == ActionIndexValueCache.act_none && (currentActionValue == ExitDoor.act_pickup_middle_end || currentActionValue == ExitDoor.act_pickup_middle_end_left_stance))
						{
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							Mission.Current.EndMission();
						}
						else if (currentActionValue2 != ActionIndexValueCache.act_none || !userAgent.SetActionChannel(0, userAgent.GetIsLeftStance() ? ExitDoor.act_pickup_middle_begin_left_stance : ExitDoor.act_pickup_middle_begin, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
						{
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
					}
				}
			}
		}

		// Token: 0x040010A4 RID: 4260
		private static readonly ActionIndexCache act_pickup_middle_begin = ActionIndexCache.Create("act_pickup_middle_begin");

		// Token: 0x040010A5 RID: 4261
		private static readonly ActionIndexCache act_pickup_middle_begin_left_stance = ActionIndexCache.Create("act_pickup_middle_begin_left_stance");

		// Token: 0x040010A6 RID: 4262
		private static readonly ActionIndexCache act_pickup_middle_end = ActionIndexCache.Create("act_pickup_middle_end");

		// Token: 0x040010A7 RID: 4263
		private static readonly ActionIndexCache act_pickup_middle_end_left_stance = ActionIndexCache.Create("act_pickup_middle_end_left_stance");
	}
}
