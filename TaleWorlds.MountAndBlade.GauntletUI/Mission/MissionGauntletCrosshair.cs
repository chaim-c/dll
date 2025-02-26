﻿using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000025 RID: 37
	[OverrideView(typeof(MissionCrosshair))]
	public class MissionGauntletCrosshair : MissionGauntletBattleUIBase
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00009088 File Offset: 0x00007288
		protected override void OnCreateView()
		{
			CombatLogManager.OnGenerateCombatLog += this.OnCombatLogGenerated;
			this._dataSource = new CrosshairVM();
			this._layer = new GauntletLayer(1, "GauntletLayer", false);
			this._movie = this._layer.LoadMovie("Crosshair", this._dataSource);
			if (base.Mission.Mode != MissionMode.Conversation && base.Mission.Mode != MissionMode.CutScene)
			{
				base.MissionScreen.AddLayer(this._layer);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00009110 File Offset: 0x00007310
		protected override void OnDestroyView()
		{
			CombatLogManager.OnGenerateCombatLog -= this.OnCombatLogGenerated;
			if (base.Mission.Mode != MissionMode.Conversation && base.Mission.Mode != MissionMode.CutScene)
			{
				base.MissionScreen.RemoveLayer(this._layer);
			}
			this._dataSource = null;
			this._movie = null;
			this._layer = null;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009174 File Offset: 0x00007374
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.DebugInput.IsKeyReleased(InputKey.F5) && base.IsViewActive)
			{
				this.OnDestroyView();
				this.OnCreateView();
			}
			if (!base.IsViewActive)
			{
				return;
			}
			this._dataSource.IsVisible = this.GetShouldCrosshairBeVisible();
			bool flag = true;
			bool isTargetInvalid = false;
			for (int i = 0; i < this._targetGadgetOpacities.Length; i++)
			{
				this._targetGadgetOpacities[i] = 0.0;
			}
			if (base.Mission.Mode != MissionMode.Conversation && base.Mission.Mode != MissionMode.CutScene && base.Mission.Mode != MissionMode.Deployment && base.Mission.MainAgent != null && !base.MissionScreen.IsViewingCharacter() && !this.IsMissionScreenUsingCustomCamera())
			{
				this._dataSource.CrosshairType = BannerlordConfig.CrosshairType;
				Agent mainAgent = base.Mission.MainAgent;
				double num = (double)(base.MissionScreen.CameraViewAngle * 0.017453292f);
				double accuracy = 2.0 * Math.Tan((double)(mainAgent.CurrentAimingError + mainAgent.CurrentAimingTurbulance) * (0.5 / Math.Tan(num * 0.5)));
				this._dataSource.SetProperties(accuracy, (double)(1f + (base.MissionScreen.CombatCamera.HorizontalFov - 1.5707964f) / 1.5707964f));
				WeaponInfo wieldedWeaponInfo = mainAgent.GetWieldedWeaponInfo(Agent.HandIndex.MainHand);
				float numberToCheck = MBMath.WrapAngle(mainAgent.LookDirection.AsVec2.RotationInRadians - mainAgent.GetMovementDirection().RotationInRadians);
				if (wieldedWeaponInfo.IsValid && wieldedWeaponInfo.IsRangedWeapon && BannerlordConfig.DisplayTargetingReticule)
				{
					Agent.ActionCodeType currentActionType = mainAgent.GetCurrentActionType(1);
					MissionWeapon wieldedWeapon = mainAgent.WieldedWeapon;
					if (wieldedWeapon.ReloadPhaseCount > 1 && wieldedWeapon.IsReloading && currentActionType == Agent.ActionCodeType.Reload)
					{
						StackArray.StackArray10FloatFloatTuple stackArray10FloatFloatTuple = default(StackArray.StackArray10FloatFloatTuple);
						ActionIndexValueCache itemUsageReloadActionCode = MBItem.GetItemUsageReloadActionCode(wieldedWeapon.CurrentUsageItem.ItemUsage, 9, mainAgent.HasMount, -1, mainAgent.GetIsLeftStance());
						this.FillReloadDurationsFromActions(ref stackArray10FloatFloatTuple, (int)wieldedWeapon.ReloadPhaseCount, mainAgent, itemUsageReloadActionCode);
						float num2 = mainAgent.GetCurrentActionProgress(1);
						ActionIndexValueCache currentActionValue = mainAgent.GetCurrentActionValue(1);
						if (currentActionValue != ActionIndexValueCache.act_none)
						{
							float num3 = 1f - MBActionSet.GetActionBlendOutStartProgress(mainAgent.ActionSet, currentActionValue);
							num2 += num3;
						}
						float animationParameter = MBAnimation.GetAnimationParameter2(mainAgent.AgentVisuals.GetSkeleton().GetAnimationAtChannel(1));
						bool flag2 = num2 > animationParameter;
						float item = flag2 ? 1f : (num2 / animationParameter);
						short reloadPhase = wieldedWeapon.ReloadPhase;
						for (int j = 0; j < (int)reloadPhase; j++)
						{
							stackArray10FloatFloatTuple[j] = new ValueTuple<float, float>(1f, stackArray10FloatFloatTuple[j].Item2);
						}
						if (!flag2)
						{
							stackArray10FloatFloatTuple[(int)reloadPhase] = new ValueTuple<float, float>(item, stackArray10FloatFloatTuple[(int)reloadPhase].Item2);
							this._dataSource.SetReloadProperties(stackArray10FloatFloatTuple, (int)wieldedWeapon.ReloadPhaseCount);
						}
						flag = false;
					}
					if (currentActionType == Agent.ActionCodeType.ReadyRanged)
					{
						Vec2 bodyRotationConstraint = mainAgent.GetBodyRotationConstraint(1);
						isTargetInvalid = (base.Mission.MainAgent.MountAgent != null && !MBMath.IsBetween(numberToCheck, bodyRotationConstraint.x, bodyRotationConstraint.y) && (bodyRotationConstraint.x < -0.1f || bodyRotationConstraint.y > 0.1f));
					}
				}
				else if (!wieldedWeaponInfo.IsValid || wieldedWeaponInfo.IsMeleeWeapon)
				{
					Agent.ActionCodeType currentActionType2 = mainAgent.GetCurrentActionType(1);
					Agent.UsageDirection currentActionDirection = mainAgent.GetCurrentActionDirection(1);
					if (BannerlordConfig.DisplayAttackDirection && (currentActionType2 == Agent.ActionCodeType.ReadyMelee || MBMath.IsBetween((int)currentActionType2, 1, 15)))
					{
						if (currentActionType2 == Agent.ActionCodeType.ReadyMelee)
						{
							switch (mainAgent.AttackDirection)
							{
							case Agent.UsageDirection.AttackUp:
								this._targetGadgetOpacities[0] = 0.7;
								break;
							case Agent.UsageDirection.AttackDown:
								this._targetGadgetOpacities[2] = 0.7;
								break;
							case Agent.UsageDirection.AttackLeft:
								this._targetGadgetOpacities[3] = 0.7;
								break;
							case Agent.UsageDirection.AttackRight:
								this._targetGadgetOpacities[1] = 0.7;
								break;
							}
						}
						else
						{
							isTargetInvalid = true;
							switch (currentActionDirection)
							{
							case Agent.UsageDirection.AttackEnd:
								this._targetGadgetOpacities[0] = 0.7;
								break;
							case Agent.UsageDirection.DefendDown:
								this._targetGadgetOpacities[2] = 0.7;
								break;
							case Agent.UsageDirection.DefendLeft:
								this._targetGadgetOpacities[3] = 0.7;
								break;
							case Agent.UsageDirection.DefendRight:
								this._targetGadgetOpacities[1] = 0.7;
								break;
							}
						}
					}
					else if (BannerlordConfig.DisplayAttackDirection)
					{
						Agent.UsageDirection usageDirection = mainAgent.PlayerAttackDirection();
						if (usageDirection >= Agent.UsageDirection.AttackUp && usageDirection < Agent.UsageDirection.AttackEnd)
						{
							if (usageDirection == Agent.UsageDirection.AttackUp)
							{
								this._targetGadgetOpacities[0] = 0.7;
							}
							else if (usageDirection == Agent.UsageDirection.AttackRight)
							{
								this._targetGadgetOpacities[1] = 0.7;
							}
							else if (usageDirection == Agent.UsageDirection.AttackDown)
							{
								this._targetGadgetOpacities[2] = 0.7;
							}
							else if (usageDirection == Agent.UsageDirection.AttackLeft)
							{
								this._targetGadgetOpacities[3] = 0.7;
							}
						}
					}
				}
			}
			if (flag)
			{
				StackArray.StackArray10FloatFloatTuple stackArray10FloatFloatTuple2 = default(StackArray.StackArray10FloatFloatTuple);
				this._dataSource.SetReloadProperties(stackArray10FloatFloatTuple2, 0);
			}
			this._dataSource.SetArrowProperties(this._targetGadgetOpacities[0], this._targetGadgetOpacities[1], this._targetGadgetOpacities[2], this._targetGadgetOpacities[3]);
			this._dataSource.IsTargetInvalid = isTargetInvalid;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000096F8 File Offset: 0x000078F8
		private bool GetShouldCrosshairBeVisible()
		{
			if (base.Mission.MainAgent != null)
			{
				MissionWeapon wieldedWeapon = base.Mission.MainAgent.WieldedWeapon;
				if (BannerlordConfig.DisplayTargetingReticule && base.Mission.Mode != MissionMode.Conversation && base.Mission.Mode != MissionMode.CutScene && !ScreenManager.GetMouseVisibility() && !wieldedWeapon.IsEmpty && wieldedWeapon.CurrentUsageItem.IsRangedWeapon && !base.MissionScreen.IsViewingCharacter() && !this.IsMissionScreenUsingCustomCamera())
				{
					return wieldedWeapon.CurrentUsageItem.WeaponClass != WeaponClass.Crossbow || !wieldedWeapon.IsReloading;
				}
			}
			return false;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000979B File Offset: 0x0000799B
		private bool IsMissionScreenUsingCustomCamera()
		{
			return base.MissionScreen.CustomCamera != null;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000097B0 File Offset: 0x000079B0
		private void OnCombatLogGenerated(CombatLogData logData)
		{
			bool isAttackerAgentMine = logData.IsAttackerAgentMine;
			bool flag = !logData.IsVictimAgentSameAsAttackerAgent && !logData.IsFriendlyFire;
			bool isHumanoidHeadShot = logData.IsAttackerAgentHuman && logData.BodyPartHit == BoneBodyPartType.Head;
			if (isAttackerAgentMine && flag && logData.TotalDamage > 0)
			{
				CrosshairVM dataSource = this._dataSource;
				if (dataSource == null)
				{
					return;
				}
				dataSource.ShowHitMarker(logData.IsFatalDamage, isHumanoidHeadShot);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009814 File Offset: 0x00007A14
		private void FillReloadDurationsFromActions(ref StackArray.StackArray10FloatFloatTuple reloadPhases, int reloadPhaseCount, Agent mainAgent, ActionIndexValueCache reloadAction)
		{
			float num = 0f;
			for (int i = 0; i < reloadPhaseCount; i++)
			{
				if (reloadAction != ActionIndexValueCache.act_none)
				{
					float num2 = MBAnimation.GetAnimationParameter2(MBActionSet.GetAnimationIndexOfAction(mainAgent.ActionSet, reloadAction)) * MBActionSet.GetActionAnimationDuration(mainAgent.ActionSet, reloadAction);
					reloadPhases[i] = new ValueTuple<float, float>(reloadPhases[i].Item1, num2);
					if (num2 > num)
					{
						num = num2;
					}
					reloadAction = MBActionSet.GetActionAnimationContinueToAction(mainAgent.ActionSet, reloadAction);
				}
			}
			if (num > 1E-05f)
			{
				for (int j = 0; j < reloadPhaseCount; j++)
				{
					reloadPhases[j] = new ValueTuple<float, float>(reloadPhases[j].Item1, reloadPhases[j].Item2 / num);
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000098CC File Offset: 0x00007ACC
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (base.IsViewActive)
			{
				this._layer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000098F1 File Offset: 0x00007AF1
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (base.IsViewActive)
			{
				this._layer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x040000D4 RID: 212
		private GauntletLayer _layer;

		// Token: 0x040000D5 RID: 213
		private CrosshairVM _dataSource;

		// Token: 0x040000D6 RID: 214
		private IGauntletMovie _movie;

		// Token: 0x040000D7 RID: 215
		private double[] _targetGadgetOpacities = new double[4];
	}
}
