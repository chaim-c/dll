using System;
using System.ComponentModel;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000051 RID: 81
	[DefaultView]
	public class MissionMainAgentController : MissionView
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000377 RID: 887 RVA: 0x0001E684 File Offset: 0x0001C884
		// (remove) Token: 0x06000378 RID: 888 RVA: 0x0001E6BC File Offset: 0x0001C8BC
		public event MissionMainAgentController.OnLockedAgentChangedDelegate OnLockedAgentChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000379 RID: 889 RVA: 0x0001E6F4 File Offset: 0x0001C8F4
		// (remove) Token: 0x0600037A RID: 890 RVA: 0x0001E72C File Offset: 0x0001C92C
		public event MissionMainAgentController.OnPotentialLockedAgentChangedDelegate OnPotentialLockedAgentChanged;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001E761 File Offset: 0x0001C961
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0001E769 File Offset: 0x0001C969
		public bool IsDisabled { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0001E772 File Offset: 0x0001C972
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0001E77A File Offset: 0x0001C97A
		public Vec3 CustomLookDir { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001E784 File Offset: 0x0001C984
		public bool IsPlayerAiming
		{
			get
			{
				if (this._isPlayerAiming)
				{
					return true;
				}
				if (base.Mission.MainAgent == null)
				{
					return false;
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (base.Input != null)
				{
					flag2 = base.Input.IsGameKeyDown(9);
				}
				if (base.Mission.MainAgent != null)
				{
					if (base.Mission.MainAgent.WieldedWeapon.CurrentUsageItem != null)
					{
						flag = (base.Mission.MainAgent.WieldedWeapon.CurrentUsageItem.IsRangedWeapon || base.Mission.MainAgent.WieldedWeapon.CurrentUsageItem.IsAmmo);
					}
					flag3 = base.Mission.MainAgent.MovementFlags.HasAnyFlag(Agent.MovementControlFlag.AttackMask);
				}
				return flag && flag2 && flag3;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0001E84D File Offset: 0x0001CA4D
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0001E855 File Offset: 0x0001CA55
		public Agent LockedAgent
		{
			get
			{
				return this._lockedAgent;
			}
			private set
			{
				if (this._lockedAgent != value)
				{
					this._lockedAgent = value;
					MissionMainAgentController.OnLockedAgentChangedDelegate onLockedAgentChanged = this.OnLockedAgentChanged;
					if (onLockedAgentChanged == null)
					{
						return;
					}
					onLockedAgentChanged(value);
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001E878 File Offset: 0x0001CA78
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0001E880 File Offset: 0x0001CA80
		public Agent PotentialLockTargetAgent
		{
			get
			{
				return this._potentialLockTargetAgent;
			}
			private set
			{
				if (this._potentialLockTargetAgent != value)
				{
					this._potentialLockTargetAgent = value;
					MissionMainAgentController.OnPotentialLockedAgentChangedDelegate onPotentialLockedAgentChanged = this.OnPotentialLockedAgentChanged;
					if (onPotentialLockedAgentChanged == null)
					{
						return;
					}
					onPotentialLockedAgentChanged(value);
				}
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001E8A3 File Offset: 0x0001CAA3
		public MissionMainAgentController()
		{
			this.InteractionComponent = new MissionMainAgentInteractionComponent(this);
			this.CustomLookDir = Vec3.Zero;
			this.IsChatOpen = false;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
		public override void EarlyStart()
		{
			base.EarlyStart();
			Game.Current.EventManager.RegisterEvent<MissionPlayerToggledOrderViewEvent>(new Action<MissionPlayerToggledOrderViewEvent>(this.OnPlayerToggleOrder));
			base.Mission.OnMainAgentChanged += this.Mission_OnMainAgentChanged;
			MissionMultiplayerGameModeBaseClient missionBehavior = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
			if (((missionBehavior != null) ? missionBehavior.RoundComponent : null) != null)
			{
				missionBehavior.RoundComponent.OnRoundStarted += this.Disable;
				missionBehavior.RoundComponent.OnPreparationEnded += this.Enable;
			}
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			this.UpdateLockTargetOption();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			base.Mission.OnMainAgentChanged -= this.Mission_OnMainAgentChanged;
			Game.Current.EventManager.UnregisterEvent<MissionPlayerToggledOrderViewEvent>(new Action<MissionPlayerToggledOrderViewEvent>(this.OnPlayerToggleOrder));
			MissionMultiplayerGameModeBaseClient missionBehavior = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
			if (((missionBehavior != null) ? missionBehavior.RoundComponent : null) != null)
			{
				missionBehavior.RoundComponent.OnRoundStarted -= this.Disable;
				missionBehavior.RoundComponent.OnPreparationEnded -= this.Enable;
			}
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public override bool IsReady()
		{
			bool result = true;
			if (base.Mission.MainAgent != null)
			{
				result = base.Mission.MainAgent.AgentVisuals.CheckResources(true);
			}
			return result;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001EA70 File Offset: 0x0001CC70
		private void Mission_OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (base.Mission.MainAgent != null)
			{
				this._isPlayerAgentAdded = true;
				this._strafeModeActive = false;
				this._autoDismountModeActive = false;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001EA94 File Offset: 0x0001CC94
		public override void OnPreMissionTick(float dt)
		{
			base.OnPreMissionTick(dt);
			if (base.MissionScreen == null)
			{
				return;
			}
			if (base.Mission.MainAgent == null && GameNetwork.MyPeer != null)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component != null)
				{
					if (component.HasSpawnedAgentVisuals)
					{
						this.AgentVisualsMovementCheck();
					}
					else if (component.FollowedAgent != null)
					{
						this.RequestToSpawnAsBotCheck();
					}
				}
			}
			Agent mainAgent = base.Mission.MainAgent;
			if (mainAgent != null && mainAgent.State == AgentState.Active && !base.MissionScreen.IsCheatGhostMode && !base.Mission.MainAgent.IsAIControlled && !this.IsDisabled && this._activated)
			{
				this.InteractionComponent.FocusTick();
				this.InteractionComponent.FocusedItemHealthTick();
				this.ControlTick();
				this.InteractionComponent.FocusStateCheckTick();
				this.LookTick(dt);
				return;
			}
			this.LockedAgent = null;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001EB72 File Offset: 0x0001CD72
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (this.InteractionComponent.CurrentFocusedObject == affectedAgent || affectedAgent == base.Mission.MainAgent)
			{
				this.InteractionComponent.ClearFocus();
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001EB9B File Offset: 0x0001CD9B
		public override void OnAgentDeleted(Agent affectedAgent)
		{
			if (this.InteractionComponent.CurrentFocusedObject == affectedAgent)
			{
				this.InteractionComponent.ClearFocus();
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001EBB6 File Offset: 0x0001CDB6
		public override void OnClearScene()
		{
			this.InteractionComponent.OnClearScene();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		private void LookTick(float dt)
		{
			if (!this.IsDisabled)
			{
				Agent mainAgent = base.Mission.MainAgent;
				if (mainAgent != null)
				{
					if (this._isPlayerAgentAdded)
					{
						this._isPlayerAgentAdded = false;
						mainAgent.LookDirectionAsAngle = mainAgent.MovementDirectionAsAngle;
					}
					if (base.Mission.ClearSceneTimerElapsedTime >= 0f)
					{
						Vec3 lookDirection;
						if (this.LockedAgent != null)
						{
							float num = 0f;
							float agentScale = this.LockedAgent.AgentScale;
							float agentScale2 = mainAgent.AgentScale;
							if (!this.LockedAgent.GetAgentFlags().HasAnyFlag(AgentFlag.IsHumanoid))
							{
								num += this.LockedAgent.Monster.BodyCapsulePoint1.z * agentScale;
							}
							else if (this.LockedAgent.HasMount)
							{
								num += (this.LockedAgent.MountAgent.Monster.RiderCameraHeightAdder + this.LockedAgent.MountAgent.Monster.BodyCapsulePoint1.z + this.LockedAgent.MountAgent.Monster.BodyCapsuleRadius) * this.LockedAgent.MountAgent.AgentScale + this.LockedAgent.Monster.CrouchEyeHeight * agentScale;
							}
							else if (this.LockedAgent.CrouchMode || this.LockedAgent.IsSitting())
							{
								num += (this.LockedAgent.Monster.CrouchEyeHeight + 0.2f) * agentScale;
							}
							else
							{
								num += (this.LockedAgent.Monster.StandingEyeHeight + 0.2f) * agentScale;
							}
							if (!mainAgent.GetAgentFlags().HasAnyFlag(AgentFlag.IsHumanoid))
							{
								num -= this.LockedAgent.Monster.BodyCapsulePoint1.z * agentScale2;
							}
							else if (mainAgent.HasMount)
							{
								num -= (mainAgent.MountAgent.Monster.RiderCameraHeightAdder + mainAgent.MountAgent.Monster.BodyCapsulePoint1.z + mainAgent.MountAgent.Monster.BodyCapsuleRadius) * mainAgent.MountAgent.AgentScale + mainAgent.Monster.CrouchEyeHeight * agentScale2;
							}
							else if (mainAgent.CrouchMode || mainAgent.IsSitting())
							{
								num -= (mainAgent.Monster.CrouchEyeHeight + 0.2f) * agentScale2;
							}
							else
							{
								num -= (mainAgent.Monster.StandingEyeHeight + 0.2f) * agentScale2;
							}
							if (this.LockedAgent.GetAgentFlags().HasAnyFlag(AgentFlag.IsHumanoid))
							{
								num -= 0.3f * agentScale;
							}
							num = MBMath.Lerp(this._lastLockedAgentHeightDifference, num, MathF.Min(8f * dt, 1f), 1E-05f);
							this._lastLockedAgentHeightDifference = num;
							lookDirection = (this.LockedAgent.VisualPosition + ((this.LockedAgent.MountAgent != null) ? (this.LockedAgent.MountAgent.GetMovementDirection().ToVec3(0f) * this.LockedAgent.MountAgent.Monster.RiderBodyCapsuleForwardAdder) : Vec3.Zero) + new Vec3(0f, 0f, num, -1f) - (mainAgent.VisualPosition + ((mainAgent.MountAgent != null) ? (mainAgent.MountAgent.GetMovementDirection().ToVec3(0f) * mainAgent.MountAgent.Monster.RiderBodyCapsuleForwardAdder) : Vec3.Zero))).NormalizedCopy();
						}
						else if (this.CustomLookDir.IsNonZero)
						{
							lookDirection = this.CustomLookDir;
						}
						else
						{
							Mat3 identity = Mat3.Identity;
							identity.RotateAboutUp(base.MissionScreen.CameraBearing);
							identity.RotateAboutSide(base.MissionScreen.CameraElevation);
							lookDirection = identity.f;
						}
						if (!base.MissionScreen.IsViewingCharacter() && !mainAgent.IsLookDirectionLocked && mainAgent.MovementLockedState != AgentMovementLockedState.FrameLocked)
						{
							mainAgent.LookDirection = lookDirection;
						}
						mainAgent.HeadCameraMode = base.Mission.CameraIsFirstPerson;
					}
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001EFB5 File Offset: 0x0001D1B5
		private void AgentVisualsMovementCheck()
		{
			if (base.Input.IsGameKeyReleased(13))
			{
				this.BreakAgentVisualsInvulnerability();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		public void BreakAgentVisualsInvulnerability()
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new AgentVisualsBreakInvulnerability());
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetEarlyAgentVisualsDespawning(GameNetwork.MyPeer.GetComponent<MissionPeer>(), true);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001F004 File Offset: 0x0001D204
		private void RequestToSpawnAsBotCheck()
		{
			if (base.Input.IsGameKeyPressed(13))
			{
				if (GameNetwork.IsClient)
				{
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new RequestToSpawnAsBot());
					GameNetwork.EndModuleEventAsClient();
					return;
				}
				if (GameNetwork.MyPeer.GetComponent<MissionPeer>().HasSpawnTimerExpired)
				{
					GameNetwork.MyPeer.GetComponent<MissionPeer>().WantsToSpawnAsBot = true;
				}
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001F060 File Offset: 0x0001D260
		private Agent FindTargetedLockableAgent(Agent player)
		{
			Vec3 direction = base.MissionScreen.CombatCamera.Direction;
			Vec3 vec = direction;
			Vec3 position = base.MissionScreen.CombatCamera.Position;
			Vec3 visualPosition = player.VisualPosition;
			float num = new Vec3(position.x, position.y, 0f, -1f).Distance(new Vec3(visualPosition.x, visualPosition.y, 0f, -1f));
			Vec3 v = position * (1f - num) + (position + direction) * num;
			float num2 = 0f;
			Agent agent = null;
			foreach (Agent agent2 in base.Mission.Agents)
			{
				if ((agent2.IsMount && agent2.RiderAgent != null && agent2.RiderAgent.IsEnemyOf(player)) || (!agent2.IsMount && agent2.IsEnemyOf(player)))
				{
					Vec3 vec2 = agent2.GetChestGlobalPosition() - v;
					float num3 = vec2.Normalize();
					if (num3 < 20f)
					{
						float num4 = Vec2.DotProduct(vec.AsVec2.Normalized(), vec2.AsVec2.Normalized());
						float num5 = Vec2.DotProduct(new Vec2(vec.AsVec2.Length, vec.z), new Vec2(vec2.AsVec2.Length, vec2.z));
						if (num4 > 0.95f && num5 > 0.95f)
						{
							float num6 = num4 * num4 * num4 / MathF.Pow(num3, 0.15f);
							if (num6 > num2)
							{
								num2 = num6;
								agent = agent2;
							}
						}
					}
				}
			}
			if (agent != null && agent.IsMount && agent.RiderAgent != null)
			{
				return agent.RiderAgent;
			}
			return agent;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001F27C File Offset: 0x0001D47C
		private void ControlTick()
		{
			if (base.MissionScreen != null && base.MissionScreen.IsPhotoModeEnabled)
			{
				return;
			}
			if (this.IsChatOpen)
			{
				return;
			}
			Agent mainAgent = base.Mission.MainAgent;
			bool flag = false;
			if (this.LockedAgent != null && (!base.Mission.Agents.ContainsQ(this.LockedAgent) || !this.LockedAgent.IsActive() || this.LockedAgent.Position.DistanceSquared(mainAgent.Position) > 625f || base.Input.IsGameKeyReleased(26) || base.Input.IsGameKeyDown(25) || (base.Mission.Mode != MissionMode.Battle && base.Mission.Mode != MissionMode.Stealth) || (!mainAgent.WieldedWeapon.IsEmpty && mainAgent.WieldedWeapon.CurrentUsageItem.IsRangedWeapon) || base.MissionScreen == null || base.MissionScreen.GetSpectatingData(base.MissionScreen.CombatCamera.Frame.origin).CameraType != SpectatorCameraTypes.LockToMainPlayer))
			{
				this.LockedAgent = null;
				flag = true;
			}
			if (base.Mission.Mode == MissionMode.Conversation)
			{
				mainAgent.MovementFlags = (Agent.MovementControlFlag)0U;
				mainAgent.MovementInputVector = Vec2.Zero;
				return;
			}
			if (base.Mission.ClearSceneTimerElapsedTime >= 0f && mainAgent.State == AgentState.Active)
			{
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				Vec2 vec = new Vec2(base.Input.GetGameKeyAxis("MovementAxisX"), base.Input.GetGameKeyAxis("MovementAxisY"));
				if (this._autoDismountModeActive)
				{
					if (!base.Input.IsGameKeyDown(0) && mainAgent.MountAgent != null)
					{
						if (mainAgent.GetCurrentVelocity().y > 0f)
						{
							vec.y = -1f;
						}
					}
					else
					{
						this._autoDismountModeActive = false;
					}
				}
				if (MathF.Abs(vec.x) < 0.2f)
				{
					vec.x = 0f;
				}
				if (MathF.Abs(vec.y) < 0.2f)
				{
					vec.y = 0f;
				}
				if (vec.IsNonZero())
				{
					float rotationInRadians = vec.RotationInRadians;
					if (rotationInRadians > -0.7853982f && rotationInRadians < 0.7853982f)
					{
						flag3 = true;
					}
					else if (rotationInRadians < -2.3561945f || rotationInRadians > 2.3561945f)
					{
						flag5 = true;
					}
					else if (rotationInRadians < 0f)
					{
						flag2 = true;
					}
					else
					{
						flag4 = true;
					}
				}
				mainAgent.EventControlFlags = (Agent.EventControlFlag)0U;
				mainAgent.MovementFlags = (Agent.MovementControlFlag)0U;
				mainAgent.MovementInputVector = Vec2.Zero;
				if (!base.MissionScreen.IsRadialMenuActive && !base.Mission.IsOrderMenuOpen)
				{
					if (base.Input.IsGameKeyPressed(14))
					{
						if (mainAgent.MountAgent == null || mainAgent.MovementVelocity.LengthSquared > 0.09f)
						{
							mainAgent.EventControlFlags |= Agent.EventControlFlag.Jump;
						}
						else
						{
							mainAgent.EventControlFlags |= Agent.EventControlFlag.Rear;
						}
					}
					if (base.Input.IsGameKeyPressed(13))
					{
						mainAgent.MovementFlags |= Agent.MovementControlFlag.Action;
					}
				}
				if (mainAgent.MountAgent != null && mainAgent.GetCurrentVelocity().y < 0.5f && (base.Input.IsGameKeyDown(3) || base.Input.IsGameKeyDown(2)))
				{
					if (base.Input.IsGameKeyPressed(16))
					{
						this._strafeModeActive = true;
					}
				}
				else
				{
					this._strafeModeActive = false;
				}
				Agent.MovementControlFlag movementControlFlag = this._lastMovementKeyPressed;
				if (base.Input.IsGameKeyPressed(0))
				{
					movementControlFlag = Agent.MovementControlFlag.Forward;
				}
				else if (base.Input.IsGameKeyPressed(1))
				{
					movementControlFlag = Agent.MovementControlFlag.Backward;
				}
				else if (base.Input.IsGameKeyPressed(2))
				{
					movementControlFlag = Agent.MovementControlFlag.StrafeLeft;
				}
				else if (base.Input.IsGameKeyPressed(3))
				{
					movementControlFlag = Agent.MovementControlFlag.StrafeRight;
				}
				if (movementControlFlag != this._lastMovementKeyPressed)
				{
					this._lastMovementKeyPressed = movementControlFlag;
					Game game = Game.Current;
					if (game != null)
					{
						game.EventManager.TriggerEvent<MissionPlayerMovementFlagsChangeEvent>(new MissionPlayerMovementFlagsChangeEvent(this._lastMovementKeyPressed));
					}
				}
				if (!base.Input.GetIsMouseActive())
				{
					bool flag6 = true;
					if (flag3)
					{
						movementControlFlag = Agent.MovementControlFlag.Forward;
					}
					else if (flag5)
					{
						movementControlFlag = Agent.MovementControlFlag.Backward;
					}
					else if (flag4)
					{
						movementControlFlag = Agent.MovementControlFlag.StrafeLeft;
					}
					else if (flag2)
					{
						movementControlFlag = Agent.MovementControlFlag.StrafeRight;
					}
					else
					{
						flag6 = false;
					}
					if (flag6)
					{
						base.Mission.SetLastMovementKeyPressed(movementControlFlag);
					}
				}
				else
				{
					base.Mission.SetLastMovementKeyPressed(this._lastMovementKeyPressed);
				}
				if (base.Input.IsGameKeyPressed(0))
				{
					if (this._lastForwardKeyPressTime + 0.3f > Time.ApplicationTime)
					{
						mainAgent.EventControlFlags &= ~(Agent.EventControlFlag.DoubleTapToDirectionUp | Agent.EventControlFlag.DoubleTapToDirectionDown | Agent.EventControlFlag.DoubleTapToDirectionRight);
						mainAgent.EventControlFlags |= Agent.EventControlFlag.DoubleTapToDirectionUp;
					}
					this._lastForwardKeyPressTime = Time.ApplicationTime;
				}
				if (base.Input.IsGameKeyPressed(1))
				{
					if (this._lastBackwardKeyPressTime + 0.3f > Time.ApplicationTime)
					{
						mainAgent.EventControlFlags &= ~(Agent.EventControlFlag.DoubleTapToDirectionUp | Agent.EventControlFlag.DoubleTapToDirectionDown | Agent.EventControlFlag.DoubleTapToDirectionRight);
						mainAgent.EventControlFlags |= Agent.EventControlFlag.DoubleTapToDirectionDown;
					}
					this._lastBackwardKeyPressTime = Time.ApplicationTime;
				}
				if (base.Input.IsGameKeyPressed(2))
				{
					if (this._lastLeftKeyPressTime + 0.3f > Time.ApplicationTime)
					{
						mainAgent.EventControlFlags &= ~(Agent.EventControlFlag.DoubleTapToDirectionUp | Agent.EventControlFlag.DoubleTapToDirectionDown | Agent.EventControlFlag.DoubleTapToDirectionRight);
						mainAgent.EventControlFlags |= Agent.EventControlFlag.DoubleTapToDirectionLeft;
					}
					this._lastLeftKeyPressTime = Time.ApplicationTime;
				}
				if (base.Input.IsGameKeyPressed(3))
				{
					if (this._lastRightKeyPressTime + 0.3f > Time.ApplicationTime)
					{
						mainAgent.EventControlFlags &= ~(Agent.EventControlFlag.DoubleTapToDirectionUp | Agent.EventControlFlag.DoubleTapToDirectionDown | Agent.EventControlFlag.DoubleTapToDirectionRight);
						mainAgent.EventControlFlags |= Agent.EventControlFlag.DoubleTapToDirectionRight;
					}
					this._lastRightKeyPressTime = Time.ApplicationTime;
				}
				if (this._isTargetLockEnabled)
				{
					if (base.Input.IsGameKeyDown(26) && this.LockedAgent == null && !base.Input.IsGameKeyDown(25) && (base.Mission.Mode == MissionMode.Battle || base.Mission.Mode == MissionMode.Stealth) && (mainAgent.WieldedWeapon.IsEmpty || !mainAgent.WieldedWeapon.CurrentUsageItem.IsRangedWeapon) && !GameNetwork.IsMultiplayer)
					{
						float applicationTime = Time.ApplicationTime;
						if (this._lastLockKeyPressTime <= 0f)
						{
							this._lastLockKeyPressTime = applicationTime;
						}
						if (applicationTime > this._lastLockKeyPressTime + 0.3f)
						{
							this.PotentialLockTargetAgent = this.FindTargetedLockableAgent(mainAgent);
						}
					}
					else
					{
						this.PotentialLockTargetAgent = null;
					}
					if (this.LockedAgent == null && !flag && base.Input.IsGameKeyReleased(26) && !GameNetwork.IsMultiplayer)
					{
						this._lastLockKeyPressTime = 0f;
						if (!base.Input.IsGameKeyDown(25) && (base.Mission.Mode == MissionMode.Battle || base.Mission.Mode == MissionMode.Stealth) && (mainAgent.WieldedWeapon.IsEmpty || !mainAgent.WieldedWeapon.CurrentUsageItem.IsRangedWeapon) && base.MissionScreen != null && base.MissionScreen.GetSpectatingData(base.MissionScreen.CombatCamera.Frame.origin).CameraType == SpectatorCameraTypes.LockToMainPlayer)
						{
							this.LockedAgent = this.FindTargetedLockableAgent(mainAgent);
						}
					}
				}
				if (mainAgent.MountAgent != null && !this._strafeModeActive)
				{
					if (flag2 || vec.x > 0f)
					{
						mainAgent.MovementFlags |= Agent.MovementControlFlag.TurnRight;
					}
					else if (flag4 || vec.x < 0f)
					{
						mainAgent.MovementFlags |= Agent.MovementControlFlag.TurnLeft;
					}
				}
				mainAgent.MovementInputVector = vec;
				if (!base.MissionScreen.MouseVisible && !base.MissionScreen.IsRadialMenuActive && !this._isPlayerOrderOpen && mainAgent.CombatActionsEnabled)
				{
					WeaponComponentData currentUsageItem = mainAgent.WieldedWeapon.CurrentUsageItem;
					bool flag7 = currentUsageItem != null && currentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.StringHeldByHand);
					WeaponComponentData currentUsageItem2 = mainAgent.WieldedWeapon.CurrentUsageItem;
					if (currentUsageItem2 != null && currentUsageItem2.IsRangedWeapon)
					{
						bool isConsumable = mainAgent.WieldedWeapon.CurrentUsageItem.IsConsumable;
					}
					WeaponComponentData currentUsageItem3 = mainAgent.WieldedWeapon.CurrentUsageItem;
					bool flag8 = currentUsageItem3 != null && currentUsageItem3.IsRangedWeapon && !mainAgent.WieldedWeapon.CurrentUsageItem.IsConsumable && !mainAgent.WieldedWeapon.CurrentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.StringHeldByHand);
					bool flag9 = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableAlternateAiming) != 0f && (flag7 || flag8);
					if (flag9)
					{
						this.HandleRangedWeaponAttackAlternativeAiming(mainAgent);
					}
					else if (base.Input.IsGameKeyDown(9))
					{
						mainAgent.MovementFlags |= mainAgent.AttackDirectionToMovementFlag(mainAgent.GetAttackDirection());
					}
					if (!flag9 && base.Input.IsGameKeyDown(10))
					{
						if (ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.ControlBlockDirection) == 2f && MissionGameModels.Current.AutoBlockModel != null)
						{
							Agent.UsageDirection blockDirection = MissionGameModels.Current.AutoBlockModel.GetBlockDirection(base.Mission);
							if (blockDirection == Agent.UsageDirection.AttackLeft)
							{
								mainAgent.MovementFlags |= Agent.MovementControlFlag.DefendRight;
							}
							else if (blockDirection == Agent.UsageDirection.AttackRight)
							{
								mainAgent.MovementFlags |= Agent.MovementControlFlag.DefendLeft;
							}
							else if (blockDirection == Agent.UsageDirection.AttackUp)
							{
								mainAgent.MovementFlags |= Agent.MovementControlFlag.DefendUp;
							}
							else if (blockDirection == Agent.UsageDirection.AttackDown)
							{
								mainAgent.MovementFlags |= Agent.MovementControlFlag.DefendDown;
							}
						}
						else
						{
							mainAgent.MovementFlags |= mainAgent.GetDefendMovementFlag();
						}
					}
				}
				if (!base.MissionScreen.IsRadialMenuActive && !base.Mission.IsOrderMenuOpen)
				{
					if (base.Input.IsGameKeyPressed(16) && (mainAgent.KickClear() || mainAgent.MountAgent != null))
					{
						mainAgent.EventControlFlags |= Agent.EventControlFlag.Kick;
					}
					if (base.Input.IsGameKeyPressed(18))
					{
						mainAgent.TryToWieldWeaponInSlot(EquipmentIndex.WeaponItemBeginSlot, Agent.WeaponWieldActionType.WithAnimation, false);
					}
					else if (base.Input.IsGameKeyPressed(19))
					{
						mainAgent.TryToWieldWeaponInSlot(EquipmentIndex.Weapon1, Agent.WeaponWieldActionType.WithAnimation, false);
					}
					else if (base.Input.IsGameKeyPressed(20))
					{
						mainAgent.TryToWieldWeaponInSlot(EquipmentIndex.Weapon2, Agent.WeaponWieldActionType.WithAnimation, false);
					}
					else if (base.Input.IsGameKeyPressed(21))
					{
						mainAgent.TryToWieldWeaponInSlot(EquipmentIndex.Weapon3, Agent.WeaponWieldActionType.WithAnimation, false);
					}
					else if (base.Input.IsGameKeyPressed(11) && this._lastWieldNextPrimaryWeaponTriggerTime + 0.2f < Time.ApplicationTime)
					{
						this._lastWieldNextPrimaryWeaponTriggerTime = Time.ApplicationTime;
						mainAgent.WieldNextWeapon(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimation);
					}
					else if (base.Input.IsGameKeyPressed(12) && this._lastWieldNextOffhandWeaponTriggerTime + 0.2f < Time.ApplicationTime)
					{
						this._lastWieldNextOffhandWeaponTriggerTime = Time.ApplicationTime;
						mainAgent.WieldNextWeapon(Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.WithAnimation);
					}
					else if (base.Input.IsGameKeyPressed(23))
					{
						mainAgent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimation);
					}
					if (base.Input.IsGameKeyPressed(17) || this._weaponUsageToggleRequested)
					{
						mainAgent.EventControlFlags |= Agent.EventControlFlag.ToggleAlternativeWeapon;
						this._weaponUsageToggleRequested = false;
					}
					if (base.Input.IsGameKeyPressed(30))
					{
						mainAgent.EventControlFlags |= (mainAgent.WalkMode ? Agent.EventControlFlag.Run : Agent.EventControlFlag.Walk);
					}
					if (mainAgent.MountAgent != null)
					{
						if (base.Input.IsGameKeyPressed(15) || this._autoDismountModeActive)
						{
							if (mainAgent.GetCurrentVelocity().y < 0.5f && mainAgent.MountAgent.GetCurrentActionType(0) != Agent.ActionCodeType.Rear)
							{
								mainAgent.EventControlFlags |= Agent.EventControlFlag.Dismount;
								return;
							}
							if (base.Input.IsGameKeyPressed(15))
							{
								this._autoDismountModeActive = true;
								mainAgent.EventControlFlags &= ~(Agent.EventControlFlag.DoubleTapToDirectionUp | Agent.EventControlFlag.DoubleTapToDirectionDown | Agent.EventControlFlag.DoubleTapToDirectionRight);
								mainAgent.EventControlFlags |= Agent.EventControlFlag.DoubleTapToDirectionDown;
								return;
							}
						}
					}
					else if (base.Input.IsGameKeyPressed(15))
					{
						mainAgent.EventControlFlags |= (mainAgent.CrouchMode ? Agent.EventControlFlag.Stand : Agent.EventControlFlag.Crouch);
					}
				}
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001FE58 File Offset: 0x0001E058
		private void HandleRangedWeaponAttackAlternativeAiming(Agent player)
		{
			if (base.Input.GetKeyState(InputKey.ControllerLTrigger).x > 0.2f)
			{
				if (base.Input.GetKeyState(InputKey.ControllerRTrigger).x < 0.6f)
				{
					player.MovementFlags |= player.AttackDirectionToMovementFlag(player.GetAttackDirection());
				}
				this._isPlayerAiming = true;
				return;
			}
			if (this._isPlayerAiming)
			{
				player.MovementFlags |= Agent.MovementControlFlag.DefendUp;
				this._isPlayerAiming = false;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		private void HandleTriggeredWeaponAttack(Agent player)
		{
			if (base.Input.GetKeyState(InputKey.ControllerRTrigger).x <= 0.2f)
			{
				if (this._isPlayerAiming)
				{
					this._playerShotMissile = false;
					this._isPlayerAiming = false;
					player.MovementFlags |= Agent.MovementControlFlag.DefendUp;
				}
				return;
			}
			if (!this._isPlayerAiming && player.WieldedWeapon.MaxAmmo > 0 && player.WieldedWeapon.Ammo == 0)
			{
				player.MovementFlags |= player.AttackDirectionToMovementFlag(player.GetAttackDirection());
				return;
			}
			if (!this._playerShotMissile && base.Input.GetKeyState(InputKey.ControllerRTrigger).x < 0.99f)
			{
				player.MovementFlags |= player.AttackDirectionToMovementFlag(player.GetAttackDirection());
				this._isPlayerAiming = true;
				return;
			}
			this._isPlayerAiming = true;
			this._playerShotMissile = true;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001FFCB File Offset: 0x0001E1CB
		public override bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return otherAgent.IsMount && otherAgent.IsActive();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001FFDD File Offset: 0x0001E1DD
		public void Disable()
		{
			this._activated = false;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001FFE6 File Offset: 0x0001E1E6
		public void Enable()
		{
			this._activated = true;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001FFEF File Offset: 0x0001E1EF
		private void OnPlayerToggleOrder(MissionPlayerToggledOrderViewEvent obj)
		{
			this._isPlayerOrderOpen = obj.IsOrderEnabled;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001FFFD File Offset: 0x0001E1FD
		public void OnWeaponUsageToggleRequested()
		{
			this._weaponUsageToggleRequested = true;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00020006 File Offset: 0x0001E206
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType optionType)
		{
			if (optionType == ManagedOptions.ManagedOptionsType.LockTarget)
			{
				this.UpdateLockTargetOption();
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00020013 File Offset: 0x0001E213
		private void UpdateLockTargetOption()
		{
			this._isTargetLockEnabled = (ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.LockTarget) == 1f);
			this.LockedAgent = null;
			this.PotentialLockTargetAgent = null;
			this._lastLockKeyPressTime = 0f;
			this._lastLockedAgentHeightDifference = 0f;
		}

		// Token: 0x04000267 RID: 615
		private const float _minValueForAimStart = 0.2f;

		// Token: 0x04000268 RID: 616
		private const float _maxValueForAttackEnd = 0.6f;

		// Token: 0x04000269 RID: 617
		private float _lastForwardKeyPressTime;

		// Token: 0x0400026A RID: 618
		private float _lastBackwardKeyPressTime;

		// Token: 0x0400026B RID: 619
		private float _lastLeftKeyPressTime;

		// Token: 0x0400026C RID: 620
		private float _lastRightKeyPressTime;

		// Token: 0x0400026D RID: 621
		private float _lastWieldNextPrimaryWeaponTriggerTime;

		// Token: 0x0400026E RID: 622
		private float _lastWieldNextOffhandWeaponTriggerTime;

		// Token: 0x0400026F RID: 623
		private bool _activated = true;

		// Token: 0x04000270 RID: 624
		private bool _strafeModeActive;

		// Token: 0x04000271 RID: 625
		private bool _autoDismountModeActive;

		// Token: 0x04000272 RID: 626
		private bool _isPlayerAgentAdded;

		// Token: 0x04000273 RID: 627
		private bool _isPlayerAiming;

		// Token: 0x04000274 RID: 628
		private bool _playerShotMissile;

		// Token: 0x04000275 RID: 629
		private bool _isPlayerOrderOpen;

		// Token: 0x04000276 RID: 630
		private bool _isTargetLockEnabled;

		// Token: 0x04000277 RID: 631
		private Agent.MovementControlFlag _lastMovementKeyPressed = Agent.MovementControlFlag.Forward;

		// Token: 0x04000278 RID: 632
		private Agent _lockedAgent;

		// Token: 0x04000279 RID: 633
		private Agent _potentialLockTargetAgent;

		// Token: 0x0400027A RID: 634
		private float _lastLockKeyPressTime;

		// Token: 0x0400027B RID: 635
		private float _lastLockedAgentHeightDifference;

		// Token: 0x0400027C RID: 636
		public readonly MissionMainAgentInteractionComponent InteractionComponent;

		// Token: 0x0400027D RID: 637
		public bool IsChatOpen;

		// Token: 0x0400027E RID: 638
		private bool _weaponUsageToggleRequested;

		// Token: 0x020000AD RID: 173
		// (Invoke) Token: 0x060004FD RID: 1277
		public delegate void OnLockedAgentChangedDelegate(Agent newAgent);

		// Token: 0x020000AE RID: 174
		// (Invoke) Token: 0x06000501 RID: 1281
		public delegate void OnPotentialLockedAgentChangedDelegate(Agent newPotentialAgent);
	}
}
