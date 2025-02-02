using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x0200004E RID: 78
	[DefaultView]
	public class MissionGamepadEffectsView : MissionView
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0001D360 File Offset: 0x0001B560
		public override void OnMissionStateActivated()
		{
			base.OnMissionStateActivated();
			this.ResetTriggerFeedback();
			this.ResetTriggerVibration();
			this._isAdaptiveTriggerEnabled = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableVibration) != 0f);
			this._usingAlternativeAiming = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableAlternateAiming) != 0f);
			NativeOptions.OnNativeOptionChanged = (NativeOptions.OnNativeOptionChangedDelegate)Delegate.Combine(NativeOptions.OnNativeOptionChanged, new NativeOptions.OnNativeOptionChangedDelegate(this.OnNativeOptionChanged));
			TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001D3ED File Offset: 0x0001B5ED
		private void OnGamepadActiveStateChanged()
		{
			if (!TaleWorlds.InputSystem.Input.IsGamepadActive)
			{
				this.ResetTriggerFeedback();
				this.ResetTriggerVibration();
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001D404 File Offset: 0x0001B604
		public override void OnMissionStateDeactivated()
		{
			base.OnMissionStateDeactivated();
			this.ResetTriggerFeedback();
			this.ResetTriggerVibration();
			NativeOptions.OnNativeOptionChanged = (NativeOptions.OnNativeOptionChangedDelegate)Delegate.Remove(NativeOptions.OnNativeOptionChanged, new NativeOptions.OnNativeOptionChangedDelegate(this.OnNativeOptionChanged));
			TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001D464 File Offset: 0x0001B664
		public override void OnPreMissionTick(float dt)
		{
			base.OnPreMissionTick(dt);
			Agent mainAgent = base.Mission.MainAgent;
			if (this._isAdaptiveTriggerEnabled)
			{
				if (mainAgent != null && mainAgent.State == AgentState.Active && mainAgent.CombatActionsEnabled && !mainAgent.IsCheering && !base.Mission.IsOrderMenuOpen && this.IsMissionModeApplicableForAdaptiveTrigger(base.Mission.Mode))
				{
					MissionWeapon wieldedWeapon = mainAgent.WieldedWeapon;
					WeaponComponentData currentUsageItem = wieldedWeapon.CurrentUsageItem;
					bool flag = currentUsageItem != null && currentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.StringHeldByHand);
					WeaponComponentData currentUsageItem2 = wieldedWeapon.CurrentUsageItem;
					bool flag2 = currentUsageItem2 != null && currentUsageItem2.WeaponFlags.HasAllFlags(WeaponFlags.HasString) && !wieldedWeapon.CurrentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.StringHeldByHand);
					WeaponComponentData currentUsageItem3 = wieldedWeapon.CurrentUsageItem;
					bool flag3 = currentUsageItem3 != null && currentUsageItem3.IsRangedWeapon && wieldedWeapon.CurrentUsageItem.IsConsumable;
					WeaponComponentData currentUsageItem4 = wieldedWeapon.CurrentUsageItem;
					bool flag4 = (currentUsageItem4 != null && currentUsageItem4.WeaponFlags.HasAllFlags(WeaponFlags.MeleeWeapon)) || mainAgent.WieldedOffhandWeapon.IsShield();
					if (flag)
					{
						this.HandleBowAdaptiveTriggers();
						return;
					}
					if (flag2)
					{
						this.HandleCrossbowAdaptiveTriggers();
						return;
					}
					if (flag3)
					{
						this.HandleThrowableAdaptiveTriggers();
						return;
					}
					if (flag4)
					{
						this.HandleMeleeAdaptiveTriggers();
						return;
					}
					if (mainAgent.CurrentlyUsedGameObject == null)
					{
						this._currentlyUsedSiegeWeapon = null;
						this._currentlyUsedMissionObject = null;
						this.ResetTriggerFeedback();
						this.ResetTriggerVibration();
						return;
					}
					if (mainAgent.CurrentlyUsedGameObject != this._currentlyUsedMissionObject)
					{
						RangedSiegeWeapon currentlyUsedSiegeWeapon;
						if ((currentlyUsedSiegeWeapon = (this.GetUsableMachineFromUsableMissionObject(mainAgent.CurrentlyUsedGameObject) as RangedSiegeWeapon)) != null)
						{
							this._currentlyUsedSiegeWeapon = currentlyUsedSiegeWeapon;
						}
						this._currentlyUsedMissionObject = mainAgent.CurrentlyUsedGameObject;
					}
					if (this._currentlyUsedSiegeWeapon != null)
					{
						this.HandleRangedSiegeEngineAdaptiveTriggers(this._currentlyUsedSiegeWeapon);
						return;
					}
				}
				else
				{
					this.ResetTriggerFeedback();
					this.ResetTriggerVibration();
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001D63C File Offset: 0x0001B83C
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			base.OnAgentHit(affectedAgent, affectorAgent, affectorWeapon, blow, attackCollisionData);
			if (affectedAgent == Agent.Main)
			{
				AttackCollisionData attackCollisionData2 = attackCollisionData;
				if (attackCollisionData2.CollisionResult != CombatCollisionResult.Blocked)
				{
					attackCollisionData2 = attackCollisionData;
					if (attackCollisionData2.CollisionResult != CombatCollisionResult.ChamberBlocked)
					{
						attackCollisionData2 = attackCollisionData;
						if (attackCollisionData2.CollisionResult != CombatCollisionResult.Parried)
						{
							goto IL_92;
						}
					}
				}
				float[] leftTriggerAmplitudes = new float[]
				{
					0.5f
				};
				float[] leftTriggerFrequencies = new float[]
				{
					0.3f
				};
				float[] array = new float[]
				{
					0.3f
				};
				this.SetTriggerVibration(leftTriggerAmplitudes, leftTriggerFrequencies, array, array.Length, null, null, null, 0);
				this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
				IL_92:
				if (affectedAgent.WieldedOffhandWeapon.IsEmpty)
				{
					attackCollisionData2 = attackCollisionData;
					if (attackCollisionData2.AttackBlockedWithShield)
					{
						this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
						return;
					}
				}
			}
			else if (affectorAgent == Agent.Main)
			{
				AttackCollisionData attackCollisionData2 = attackCollisionData;
				if (attackCollisionData2.CollisionResult != CombatCollisionResult.StrikeAgent)
				{
					attackCollisionData2 = attackCollisionData;
					if (attackCollisionData2.CollisionResult != CombatCollisionResult.Blocked)
					{
						return;
					}
				}
				MissionWeapon missionWeapon = affectorWeapon;
				if (!missionWeapon.IsEmpty)
				{
					missionWeapon = affectorWeapon;
					if (missionWeapon.IsShield())
					{
						float[] leftTriggerAmplitudes2 = new float[]
						{
							1f
						};
						float[] leftTriggerFrequencies2 = new float[]
						{
							0.1f
						};
						float[] array2 = new float[]
						{
							0.35f
						};
						this.SetTriggerVibration(leftTriggerAmplitudes2, leftTriggerFrequencies2, array2, array2.Length, null, null, null, 0);
					}
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001D7A0 File Offset: 0x0001B9A0
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
			if (affectedAgent.IsMainAgent)
			{
				this.ResetTriggerFeedback();
				this.ResetTriggerVibration();
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001D7C1 File Offset: 0x0001B9C1
		protected override void OnEndMission()
		{
			base.OnEndMission();
			this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		private void OnNativeOptionChanged(NativeOptions.NativeOptionsType changedNativeOptionsType)
		{
			if (changedNativeOptionsType == NativeOptions.NativeOptionsType.EnableVibration)
			{
				bool isAdaptiveTriggerEnabled = this._isAdaptiveTriggerEnabled;
				this._isAdaptiveTriggerEnabled = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableVibration) != 0f);
				this._usingAlternativeAiming = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableAlternateAiming) != 0f);
				if (isAdaptiveTriggerEnabled && !this._isAdaptiveTriggerEnabled)
				{
					this.ResetTriggerFeedback();
					this.ResetTriggerVibration();
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001D82C File Offset: 0x0001BA2C
		private bool IsMissionModeApplicableForAdaptiveTrigger(MissionMode mode)
		{
			switch (mode)
			{
			case MissionMode.StartUp:
			case MissionMode.Battle:
			case MissionMode.Duel:
			case MissionMode.Stealth:
			case MissionMode.Tournament:
				return true;
			}
			return false;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001D868 File Offset: 0x0001BA68
		private void HandleBowAdaptiveTriggers()
		{
			Agent mainAgent = base.Mission.MainAgent;
			Agent.ActionStage actionStage = (mainAgent != null) ? mainAgent.GetCurrentActionStage(1) : Agent.ActionStage.None;
			if (actionStage == Agent.ActionStage.None || actionStage == Agent.ActionStage.ReloadMidPhase || actionStage == Agent.ActionStage.ReloadLastPhase)
			{
				this.SetTriggerState(this._usingAlternativeAiming ? MissionGamepadEffectsView.TriggerState.SoftTriggerFeedbackLeft : MissionGamepadEffectsView.TriggerState.SoftTriggerFeedbackRight);
				return;
			}
			if (actionStage == Agent.ActionStage.AttackReady)
			{
				float num = mainAgent.GetAimingTimer() - mainAgent.AgentDrivenProperties.WeaponUnsteadyBeginTime;
				if (num > 0f)
				{
					float num2 = mainAgent.AgentDrivenProperties.WeaponUnsteadyEndTime - mainAgent.AgentDrivenProperties.WeaponUnsteadyBeginTime;
					float amount = MBMath.ClampFloat(num / num2, 0f, 1f);
					float num3 = MBMath.Lerp(0f, 1f, amount, 1E-05f);
					float[] array = new float[]
					{
						num3
					};
					float num4 = MBMath.ClampFloat(1f - num3, 0.1f, 1f);
					float[] array2 = new float[]
					{
						num4
					};
					float[] array3 = new float[]
					{
						0.05f
					};
					if (this._usingAlternativeAiming)
					{
						this.SetTriggerVibration(array, array2, array3, array3.Length, null, null, null, 0);
					}
					else
					{
						this.SetTriggerVibration(null, null, null, 0, array, array2, array3, array3.Length);
					}
					this._triggerState = MissionGamepadEffectsView.TriggerState.Vibration;
				}
				else
				{
					this.SetTriggerState(this._usingAlternativeAiming ? MissionGamepadEffectsView.TriggerState.SoftTriggerFeedbackLeft : MissionGamepadEffectsView.TriggerState.SoftTriggerFeedbackRight);
					float[] array4 = new float[]
					{
						0.07f
					};
					float[] array5 = new float[]
					{
						0.5f
					};
					float[] array6 = new float[]
					{
						0.5f
					};
					if (this._usingAlternativeAiming)
					{
						this.SetTriggerVibration(array4, array5, array6, array6.Length, null, null, null, 0);
					}
					else
					{
						this.SetTriggerVibration(null, null, null, 0, array4, array5, array6, array6.Length);
					}
				}
				if (this._usingAlternativeAiming)
				{
					this.SetTriggerWeaponEffect(0, 0, 0, 3, 7, 8);
					return;
				}
				this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
				return;
			}
			else
			{
				if (actionStage == Agent.ActionStage.AttackRelease)
				{
					this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
					return;
				}
				this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
				return;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001DA40 File Offset: 0x0001BC40
		private void HandleCrossbowAdaptiveTriggers()
		{
			Agent mainAgent = base.Mission.MainAgent;
			Agent.ActionStage actionStage = (mainAgent != null) ? mainAgent.GetCurrentActionStage(1) : Agent.ActionStage.None;
			if (actionStage == Agent.ActionStage.ReloadMidPhase)
			{
				this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
				return;
			}
			if (actionStage == Agent.ActionStage.AttackRelease)
			{
				float[] rightTriggerAmplitudes = new float[]
				{
					0.01f
				};
				float[] rightTriggerFrequencies = new float[]
				{
					0.08f
				};
				float[] array = new float[]
				{
					0.05f
				};
				this.SetTriggerVibration(null, null, null, 0, rightTriggerAmplitudes, rightTriggerFrequencies, array, array.Length);
				this.SetTriggerState(MissionGamepadEffectsView.TriggerState.Off);
				return;
			}
			if (actionStage == Agent.ActionStage.AttackReady)
			{
				if (this._usingAlternativeAiming)
				{
					this.SetTriggerWeaponEffect(0, 0, 0, 3, 7, 8);
					return;
				}
			}
			else if (!this._usingAlternativeAiming)
			{
				this.SetTriggerWeaponEffect(0, 0, 0, 3, 7, 8);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001DAE8 File Offset: 0x0001BCE8
		private void HandleThrowableAdaptiveTriggers()
		{
			WeaponComponentData currentUsageItem = base.Mission.MainAgent.WieldedOffhandWeapon.CurrentUsageItem;
			bool flag = currentUsageItem != null && currentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.CanBlockRanged);
			this._triggerFeedback[2] = 0;
			this._triggerFeedback[3] = 3;
			if (flag)
			{
				this._triggerFeedback[0] = 4;
				this._triggerFeedback[1] = 2;
			}
			else
			{
				this._triggerFeedback[0] = 0;
				this._triggerFeedback[1] = 0;
			}
			this.SetTriggerFeedback(this._triggerFeedback[0], this._triggerFeedback[1], this._triggerFeedback[2], this._triggerFeedback[3]);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001DB84 File Offset: 0x0001BD84
		private void HandleMeleeAdaptiveTriggers()
		{
			Agent mainAgent = base.Mission.MainAgent;
			MissionWeapon wieldedWeapon = mainAgent.WieldedWeapon;
			WeaponComponentData currentUsageItem = wieldedWeapon.CurrentUsageItem;
			bool flag = currentUsageItem != null && currentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.NotUsableWithOneHand);
			WeaponComponentData currentUsageItem2 = mainAgent.WieldedOffhandWeapon.CurrentUsageItem;
			bool flag2 = currentUsageItem2 != null && currentUsageItem2.WeaponFlags.HasAnyFlag(WeaponFlags.CanBlockRanged);
			if (flag)
			{
				this._triggerFeedback[2] = 3;
				this._triggerFeedback[3] = 0;
			}
			else if (wieldedWeapon.CurrentUsageItem == null)
			{
				this._triggerFeedback[2] = 0;
				this._triggerFeedback[3] = 0;
			}
			else
			{
				this._triggerFeedback[2] = 4;
				this._triggerFeedback[3] = 1;
			}
			if (flag2 || flag || wieldedWeapon.CurrentUsageItem != null)
			{
				this._triggerFeedback[0] = 4;
				this._triggerFeedback[1] = 2;
			}
			else
			{
				this._triggerFeedback[0] = 0;
				this._triggerFeedback[1] = 0;
			}
			this.SetTriggerFeedback(this._triggerFeedback[0], this._triggerFeedback[1], this._triggerFeedback[2], this._triggerFeedback[3]);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001DC84 File Offset: 0x0001BE84
		private void HandleRangedSiegeEngineAdaptiveTriggers(RangedSiegeWeapon rangedSiegeWeapon)
		{
			if (!(rangedSiegeWeapon is Ballista) && !(rangedSiegeWeapon is FireBallista))
			{
				this.ResetTriggerFeedback();
				this.ResetTriggerVibration();
				return;
			}
			if (rangedSiegeWeapon.State == RangedSiegeWeapon.WeaponState.Idle)
			{
				this.SetTriggerWeaponEffect(0, 0, 0, 4, 6, 10);
				return;
			}
			if (rangedSiegeWeapon.State == RangedSiegeWeapon.WeaponState.Shooting || rangedSiegeWeapon.State == RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving)
			{
				this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
				float[] rightTriggerAmplitudes = new float[]
				{
					0.2f,
					0.4f,
					0.2f
				};
				float[] rightTriggerFrequencies = new float[]
				{
					0.2f,
					0.4f,
					0.2f
				};
				float[] array = new float[]
				{
					0.2f,
					0.3f,
					0.2f
				};
				this.SetTriggerVibration(null, null, null, 0, rightTriggerAmplitudes, rightTriggerFrequencies, array, array.Length);
				return;
			}
			this.ResetTriggerFeedback();
			this.ResetTriggerVibration();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001DD38 File Offset: 0x0001BF38
		private UsableMachine GetUsableMachineFromUsableMissionObject(UsableMissionObject usableMissionObject)
		{
			StandingPoint standingPoint;
			if ((standingPoint = (usableMissionObject as StandingPoint)) != null)
			{
				GameEntity gameEntity = standingPoint.GameEntity;
				while (gameEntity != null && !gameEntity.HasScriptOfType<UsableMachine>())
				{
					gameEntity = gameEntity.Parent;
				}
				if (gameEntity != null)
				{
					UsableMachine firstScriptOfType = gameEntity.GetFirstScriptOfType<UsableMachine>();
					if (firstScriptOfType != null)
					{
						return firstScriptOfType;
					}
				}
			}
			return null;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001DD88 File Offset: 0x0001BF88
		private void SetTriggerState(MissionGamepadEffectsView.TriggerState triggerState)
		{
			if (this._triggerState != triggerState)
			{
				switch (triggerState)
				{
				case MissionGamepadEffectsView.TriggerState.Off:
					this.ResetTriggerFeedback();
					this.ResetTriggerVibration();
					break;
				case MissionGamepadEffectsView.TriggerState.SoftTriggerFeedbackLeft:
					this.SetTriggerFeedback(0, 2, 0, 0);
					this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
					break;
				case MissionGamepadEffectsView.TriggerState.SoftTriggerFeedbackRight:
					this.SetTriggerFeedback(0, 0, 0, 2);
					this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
					break;
				case MissionGamepadEffectsView.TriggerState.HardTriggerFeedbackLeft:
					this.SetTriggerFeedback(0, 4, 0, 0);
					this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
					break;
				case MissionGamepadEffectsView.TriggerState.HardTriggerFeedbackRight:
					this.SetTriggerFeedback(0, 0, 0, 4);
					this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
					break;
				case MissionGamepadEffectsView.TriggerState.WeaponEffect:
					this.SetTriggerWeaponEffect(0, 0, 0, 4, 7, 7);
					break;
				default:
					Debug.FailedAssert("Unexpected trigger state:" + triggerState, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\MissionViews\\MissionGamepadEffectsView.cs", "SetTriggerState", 500);
					break;
				}
				this._triggerState = triggerState;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001DE6C File Offset: 0x0001C06C
		private void ResetTriggerFeedback()
		{
			this._triggerFeedback[0] = 0;
			this._triggerFeedback[1] = 0;
			this._triggerFeedback[2] = 0;
			this._triggerFeedback[3] = 0;
			this.SetTriggerFeedback(0, 0, 0, 0);
			this.SetTriggerWeaponEffect(0, 0, 0, 0, 0, 0);
			this._triggerState = MissionGamepadEffectsView.TriggerState.Off;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001DEBA File Offset: 0x0001C0BA
		private void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength)
		{
			TaleWorlds.InputSystem.Input.SetTriggerFeedback(leftTriggerPosition, leftTriggerStrength, rightTriggerPosition, rightTriggerStrength);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001DEC6 File Offset: 0x0001C0C6
		private void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength)
		{
			TaleWorlds.InputSystem.Input.SetTriggerWeaponEffect(leftStartPosition, leftEnd_position, leftStrength, rightStartPosition, rightEndPosition, rightStrength);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		private void ResetTriggerVibration()
		{
			float[] array = new float[1];
			this.SetTriggerVibration(array, array, array, 0, array, array, array, 0);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001DEFA File Offset: 0x0001C0FA
		private void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements)
		{
			TaleWorlds.InputSystem.Input.SetTriggerVibration(leftTriggerAmplitudes, leftTriggerFrequencies, leftTriggerDurations, numLeftTriggerElements, rightTriggerAmplitudes, rightTriggerFrequencies, rightTriggerDurations, numRightTriggerElements);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001DF0E File Offset: 0x0001C10E
		private static void SetLightbarColor(float red, float green, float blue)
		{
			TaleWorlds.InputSystem.Input.SetLightbarColor(red, green, blue);
		}

		// Token: 0x0400024F RID: 591
		private MissionGamepadEffectsView.TriggerState _triggerState;

		// Token: 0x04000250 RID: 592
		private readonly byte[] _triggerFeedback = new byte[4];

		// Token: 0x04000251 RID: 593
		private bool _isAdaptiveTriggerEnabled;

		// Token: 0x04000252 RID: 594
		private bool _usingAlternativeAiming;

		// Token: 0x04000253 RID: 595
		private RangedSiegeWeapon _currentlyUsedSiegeWeapon;

		// Token: 0x04000254 RID: 596
		private UsableMissionObject _currentlyUsedMissionObject;

		// Token: 0x020000AB RID: 171
		private enum TriggerState
		{
			// Token: 0x04000382 RID: 898
			Off,
			// Token: 0x04000383 RID: 899
			SoftTriggerFeedbackLeft,
			// Token: 0x04000384 RID: 900
			SoftTriggerFeedbackRight,
			// Token: 0x04000385 RID: 901
			HardTriggerFeedbackLeft,
			// Token: 0x04000386 RID: 902
			HardTriggerFeedbackRight,
			// Token: 0x04000387 RID: 903
			WeaponEffect,
			// Token: 0x04000388 RID: 904
			Vibration
		}
	}
}
