using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035A RID: 858
	public class ArrowBarrel : UsableMachine
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x000C0E07 File Offset: 0x000BF007
		private static int _pickupArrowSoundFromBarrelCache
		{
			get
			{
				if (ArrowBarrel._pickupArrowSoundFromBarrel == -1)
				{
					ArrowBarrel._pickupArrowSoundFromBarrel = SoundEvent.GetEventIdFromString("event:/mission/combat/pickup_arrows");
				}
				return ArrowBarrel._pickupArrowSoundFromBarrel;
			}
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000C0E25 File Offset: 0x000BF025
		protected ArrowBarrel()
		{
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000C0E34 File Offset: 0x000BF034
		protected internal override void OnInit()
		{
			base.OnInit();
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				(standingPoint as StandingPointWithWeaponRequirement).InitRequiredWeaponClasses(WeaponClass.Arrow, WeaponClass.Bolt);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
			this.MakeVisibilityCheck = false;
			this._isVisible = base.GameEntity.IsVisibleIncludeParents();
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000C0EB8 File Offset: 0x000BF0B8
		public override void AfterMissionStart()
		{
			if (base.StandingPoints != null)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					standingPoint.LockUserFrames = true;
				}
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000C0F14 File Offset: 0x000BF114
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = new TextObject("{=bNYm3K6b}{KEY} Pick Up", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000C0F3E File Offset: 0x000BF13E
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return new TextObject("{=bWi4aMO9}Arrow Barrel", null).ToString();
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000C0F50 File Offset: 0x000BF150
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (GameNetwork.IsClientOrReplay)
			{
				return base.GetTickRequirement();
			}
			return ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel | base.GetTickRequirement();
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000C0F68 File Offset: 0x000BF168
		protected internal override void OnTickParallel(float dt)
		{
			this.TickAux(true);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000C0F71 File Offset: 0x000BF171
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._needsSingleThreadTickOnce)
			{
				this._needsSingleThreadTickOnce = false;
				this.TickAux(false);
			}
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000C0F90 File Offset: 0x000BF190
		private void TickAux(bool isParallel)
		{
			if (this._isVisible && !GameNetwork.IsClientOrReplay)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					if (standingPoint.HasUser)
					{
						Agent userAgent = standingPoint.UserAgent;
						ActionIndexValueCache currentActionValue = userAgent.GetCurrentActionValue(0);
						ActionIndexValueCache currentActionValue2 = userAgent.GetCurrentActionValue(1);
						if (!(currentActionValue2 == ActionIndexValueCache.act_none) || (!(currentActionValue == ArrowBarrel.act_pickup_down_begin) && !(currentActionValue == ArrowBarrel.act_pickup_down_begin_left_stance)))
						{
							if (currentActionValue2 == ActionIndexValueCache.act_none && (currentActionValue == ArrowBarrel.act_pickup_down_end || currentActionValue == ArrowBarrel.act_pickup_down_end_left_stance))
							{
								if (isParallel)
								{
									this._needsSingleThreadTickOnce = true;
								}
								else
								{
									for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
									{
										if (!userAgent.Equipment[equipmentIndex].IsEmpty && (userAgent.Equipment[equipmentIndex].CurrentUsageItem.WeaponClass == WeaponClass.Arrow || userAgent.Equipment[equipmentIndex].CurrentUsageItem.WeaponClass == WeaponClass.Bolt) && userAgent.Equipment[equipmentIndex].Amount < userAgent.Equipment[equipmentIndex].ModifiedMaxAmount)
										{
											userAgent.SetWeaponAmountInSlot(equipmentIndex, userAgent.Equipment[equipmentIndex].ModifiedMaxAmount, true);
											Mission.Current.MakeSoundOnlyOnRelatedPeer(ArrowBarrel._pickupArrowSoundFromBarrelCache, userAgent.Position, userAgent.Index);
										}
									}
									userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
								}
							}
							else if (currentActionValue2 != ActionIndexValueCache.act_none || !userAgent.SetActionChannel(0, userAgent.GetIsLeftStance() ? ArrowBarrel.act_pickup_down_begin_left_stance : ArrowBarrel.act_pickup_down_begin, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
							{
								if (isParallel)
								{
									this._needsSingleThreadTickOnce = true;
								}
								else
								{
									userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000C11D8 File Offset: 0x000BF3D8
		public override OrderType GetOrder(BattleSideEnum side)
		{
			return OrderType.None;
		}

		// Token: 0x040013E2 RID: 5090
		private static readonly ActionIndexCache act_pickup_down_begin = ActionIndexCache.Create("act_pickup_down_begin");

		// Token: 0x040013E3 RID: 5091
		private static readonly ActionIndexCache act_pickup_down_begin_left_stance = ActionIndexCache.Create("act_pickup_down_begin_left_stance");

		// Token: 0x040013E4 RID: 5092
		private static readonly ActionIndexCache act_pickup_down_end = ActionIndexCache.Create("act_pickup_down_end");

		// Token: 0x040013E5 RID: 5093
		private static readonly ActionIndexCache act_pickup_down_end_left_stance = ActionIndexCache.Create("act_pickup_down_end_left_stance");

		// Token: 0x040013E6 RID: 5094
		private static int _pickupArrowSoundFromBarrel = -1;

		// Token: 0x040013E7 RID: 5095
		private bool _isVisible = true;

		// Token: 0x040013E8 RID: 5096
		private bool _needsSingleThreadTickOnce;
	}
}
