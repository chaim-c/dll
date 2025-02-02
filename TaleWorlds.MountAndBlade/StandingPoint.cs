using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000346 RID: 838
	public class StandingPoint : UsableMissionObject
	{
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000BB448 File Offset: 0x000B9648
		public virtual Agent.AIScriptedFrameFlags DisableScriptedFrameFlags
		{
			get
			{
				return Agent.AIScriptedFrameFlags.None;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000BB44B File Offset: 0x000B964B
		public override bool DisableCombatActionsOnUse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000BB44E File Offset: 0x000B964E
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x000BB456 File Offset: 0x000B9656
		[EditableScriptComponentVariable(false)]
		public Agent FavoredUser { get; set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000BB45F File Offset: 0x000B965F
		public virtual bool PlayerStopsUsingWhenInteractsWithOther
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000BB462 File Offset: 0x000B9662
		public StandingPoint() : base(false)
		{
			this.AutoSheathWeapons = true;
			this.TranslateUser = true;
			this._autoAttachOnUsingStopped = true;
			this._needsSingleThreadTickOnce = false;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000BB490 File Offset: 0x000B9690
		protected internal override void OnInit()
		{
			base.OnInit();
			this._cachedAgentDistances = new Dictionary<Agent, StandingPoint.AgentDistanceCache>();
			bool flag = base.GameEntity.HasTag("attacker");
			bool flag2 = base.GameEntity.HasTag("defender");
			if (flag && !flag2)
			{
				this.StandingPointSide = BattleSideEnum.Attacker;
			}
			else if (!flag && flag2)
			{
				this.StandingPointSide = BattleSideEnum.Defender;
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000BB4FA File Offset: 0x000B96FA
		public void OnParentMachinePhysicsStateChanged()
		{
			base.GameEntityWithWorldPosition.InvalidateWorldPosition();
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000BB507 File Offset: 0x000B9707
		public override bool IsDisabledForAgent(Agent agent)
		{
			return base.IsDisabledForAgent(agent) || (this.StandingPointSide != BattleSideEnum.None && agent.IsAIControlled && agent.Team != null && agent.Team.Side != this.StandingPointSide);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000BB545 File Offset: 0x000B9745
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (!GameNetwork.IsClientOrReplay && base.HasUser)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel2;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000BB568 File Offset: 0x000B9768
		private void TickAux(bool isParallel)
		{
			if (!GameNetwork.IsClientOrReplay && base.HasUser)
			{
				if (!base.UserAgent.IsActive() || this.DoesActionTypeStopUsingGameObject(MBAnimation.GetActionType(base.UserAgent.GetCurrentAction(0))))
				{
					if (isParallel)
					{
						this._needsSingleThreadTickOnce = true;
						return;
					}
					Agent userAgent = base.UserAgent;
					Agent.StopUsingGameObjectFlags stopUsingGameObjectFlags = Agent.StopUsingGameObjectFlags.None;
					if (this._autoAttachOnUsingStopped)
					{
						stopUsingGameObjectFlags |= Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject;
					}
					userAgent.StopUsingGameObject(false, stopUsingGameObjectFlags);
					Action<Agent, bool> onUsingStoppedAction = this._onUsingStoppedAction;
					if (onUsingStoppedAction == null)
					{
						return;
					}
					onUsingStoppedAction(userAgent, true);
					return;
				}
				else if (this.AutoSheathWeapons)
				{
					if (base.UserAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand) != EquipmentIndex.None)
					{
						if (isParallel)
						{
							this._needsSingleThreadTickOnce = true;
						}
						else
						{
							base.UserAgent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.Instant);
						}
					}
					if (base.UserAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand) != EquipmentIndex.None)
					{
						if (isParallel)
						{
							this._needsSingleThreadTickOnce = true;
							return;
						}
						base.UserAgent.TryToSheathWeaponInHand(Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.Instant);
						return;
					}
				}
				else if (this.AutoWieldWeapons && base.UserAgent.Equipment.HasAnyWeapon() && base.UserAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand) == EquipmentIndex.None && base.UserAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand) == EquipmentIndex.None)
				{
					if (isParallel)
					{
						this._needsSingleThreadTickOnce = true;
						return;
					}
					base.UserAgent.WieldInitialWeapons(Agent.WeaponWieldActionType.Instant, Equipment.InitialWeaponEquipPreference.Any);
				}
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000BB693 File Offset: 0x000B9893
		protected internal override void OnTickParallel2(float dt)
		{
			base.OnTickParallel2(dt);
			this.TickAux(true);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000BB6A3 File Offset: 0x000B98A3
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._needsSingleThreadTickOnce)
			{
				this._needsSingleThreadTickOnce = false;
				this.TickAux(false);
			}
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000BB6C2 File Offset: 0x000B98C2
		protected virtual bool DoesActionTypeStopUsingGameObject(Agent.ActionCodeType actionType)
		{
			return actionType == Agent.ActionCodeType.Jump || actionType == Agent.ActionCodeType.Kick || actionType == Agent.ActionCodeType.WeaponBash;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000BB6D8 File Offset: 0x000B98D8
		public override void OnUse(Agent userAgent)
		{
			if (!this._autoAttachOnUsingStopped && this.MovingAgent != null)
			{
				Agent movingAgent = this.MovingAgent;
				movingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.None);
				Action<Agent, bool> onUsingStoppedAction = this._onUsingStoppedAction;
				if (onUsingStoppedAction != null)
				{
					onUsingStoppedAction(movingAgent, false);
				}
			}
			base.OnUse(userAgent);
			if (this.LockUserFrames)
			{
				WorldFrame userFrameForAgent = this.GetUserFrameForAgent(userAgent);
				userAgent.SetTargetPositionAndDirection(userFrameForAgent.Origin.AsVec2, userFrameForAgent.Rotation.f);
				return;
			}
			if (this.LockUserPositions)
			{
				userAgent.SetTargetPosition(this.GetUserFrameForAgent(userAgent).Origin.AsVec2);
			}
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000BB76E File Offset: 0x000B996E
		public override void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			base.OnUseStopped(userAgent, isSuccessful, preferenceIndex);
			if (this.LockUserFrames || this.LockUserPositions)
			{
				userAgent.ClearTargetFrame();
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000BB790 File Offset: 0x000B9990
		public override WorldFrame GetUserFrameForAgent(Agent agent)
		{
			if (!Mission.Current.IsTeleportingAgents && !this.TranslateUser)
			{
				return agent.GetWorldFrame();
			}
			if (!Mission.Current.IsTeleportingAgents && (this.LockUserFrames || this.LockUserPositions))
			{
				return base.GetUserFrameForAgent(agent);
			}
			WorldFrame userFrameForAgent = base.GetUserFrameForAgent(agent);
			MatrixFrame lookFrame = agent.LookFrame;
			Vec2 v = (lookFrame.origin.AsVec2 - userFrameForAgent.Origin.AsVec2).Normalized();
			Vec2 vec = userFrameForAgent.Origin.AsVec2 + agent.GetInteractionDistanceToUsable(this) * 0.5f * v;
			Mat3 rotation = lookFrame.rotation;
			userFrameForAgent.Origin.SetVec2(vec);
			userFrameForAgent.Rotation = rotation;
			return userFrameForAgent;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000BB856 File Offset: 0x000B9A56
		public virtual bool HasAlternative()
		{
			return false;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000BB85C File Offset: 0x000B9A5C
		public virtual float GetUsageScoreForAgent(Agent agent)
		{
			WorldPosition origin = this.GetUserFrameForAgent(agent).Origin;
			WorldPosition worldPosition = agent.GetWorldPosition();
			float pathDistance = this.GetPathDistance(agent, ref origin, ref worldPosition);
			float num = (pathDistance < 0f) ? float.MinValue : (-pathDistance);
			if (agent == this.FavoredUser)
			{
				num *= 0.5f;
			}
			return num;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000BB8B0 File Offset: 0x000B9AB0
		public virtual float GetUsageScoreForAgent(ValueTuple<Agent, float> agentPair)
		{
			float item = agentPair.Item2;
			float num = (item < 0f) ? float.MinValue : (-item);
			if (agentPair.Item1 == this.FavoredUser)
			{
				num *= 0.5f;
			}
			return num;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000BB8ED File Offset: 0x000B9AED
		public void SetupOnUsingStoppedBehavior(bool autoAttach, Action<Agent, bool> action)
		{
			this._autoAttachOnUsingStopped = autoAttach;
			this._onUsingStoppedAction = action;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000BB900 File Offset: 0x000B9B00
		private float GetPathDistance(Agent agent, ref WorldPosition userPosition, ref WorldPosition agentPosition)
		{
			StandingPoint.AgentDistanceCache agentDistanceCache;
			float num;
			if (this._cachedAgentDistances.TryGetValue(agent, out agentDistanceCache))
			{
				if (agentDistanceCache.AgentPosition.DistanceSquared(agentPosition.AsVec2) < 1f && agentDistanceCache.StandingPointPosition.DistanceSquared(userPosition.AsVec2) < 1f)
				{
					num = agentDistanceCache.PathDistance;
				}
				else
				{
					if (!Mission.Current.Scene.GetPathDistanceBetweenPositions(ref userPosition, ref agentPosition, agent.Monster.BodyCapsuleRadius, out num))
					{
						num = float.MaxValue;
					}
					agentDistanceCache = new StandingPoint.AgentDistanceCache
					{
						AgentPosition = agentPosition.AsVec2,
						StandingPointPosition = userPosition.AsVec2,
						PathDistance = num
					};
					this._cachedAgentDistances[agent] = agentDistanceCache;
				}
			}
			else
			{
				if (!Mission.Current.Scene.GetPathDistanceBetweenPositions(ref userPosition, ref agentPosition, agent.Monster.BodyCapsuleRadius, out num))
				{
					num = float.MaxValue;
				}
				agentDistanceCache = new StandingPoint.AgentDistanceCache
				{
					AgentPosition = agentPosition.AsVec2,
					StandingPointPosition = userPosition.AsVec2,
					PathDistance = num
				};
				this._cachedAgentDistances[agent] = agentDistanceCache;
			}
			return num;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000BBA1F File Offset: 0x000B9C1F
		public override void OnEndMission()
		{
			base.OnEndMission();
			this.FavoredUser = null;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000BBA2E File Offset: 0x000B9C2E
		protected internal virtual bool IsUsableBySide(BattleSideEnum side)
		{
			return !base.IsDeactivated && (base.IsInstantUse || !base.HasUser) && (this.StandingPointSide == BattleSideEnum.None || side == this.StandingPointSide);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000BBA5E File Offset: 0x000B9C5E
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return string.Empty;
		}

		// Token: 0x04001325 RID: 4901
		public bool AutoSheathWeapons;

		// Token: 0x04001326 RID: 4902
		public bool AutoEquipWeaponsOnUseStopped;

		// Token: 0x04001327 RID: 4903
		private bool _autoAttachOnUsingStopped;

		// Token: 0x04001328 RID: 4904
		private Action<Agent, bool> _onUsingStoppedAction;

		// Token: 0x04001329 RID: 4905
		public bool AutoWieldWeapons;

		// Token: 0x0400132A RID: 4906
		public readonly bool TranslateUser;

		// Token: 0x0400132B RID: 4907
		public bool HasRecentlyBeenRechecked;

		// Token: 0x0400132D RID: 4909
		private Dictionary<Agent, StandingPoint.AgentDistanceCache> _cachedAgentDistances;

		// Token: 0x0400132E RID: 4910
		private bool _needsSingleThreadTickOnce;

		// Token: 0x0400132F RID: 4911
		protected BattleSideEnum StandingPointSide = BattleSideEnum.None;

		// Token: 0x02000608 RID: 1544
		public struct StackArray8StandingPoint
		{
			// Token: 0x170009F0 RID: 2544
			public StandingPoint this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return this._element0;
					case 1:
						return this._element1;
					case 2:
						return this._element2;
					case 3:
						return this._element3;
					case 4:
						return this._element4;
					case 5:
						return this._element5;
					case 6:
						return this._element6;
					case 7:
						return this._element7;
					default:
						return null;
					}
				}
				set
				{
					switch (index)
					{
					case 0:
						this._element0 = value;
						return;
					case 1:
						this._element1 = value;
						return;
					case 2:
						this._element2 = value;
						return;
					case 3:
						this._element3 = value;
						return;
					case 4:
						this._element4 = value;
						return;
					case 5:
						this._element5 = value;
						return;
					case 6:
						this._element6 = value;
						return;
					case 7:
						this._element7 = value;
						return;
					default:
						return;
					}
				}
			}

			// Token: 0x04001F57 RID: 8023
			private StandingPoint _element0;

			// Token: 0x04001F58 RID: 8024
			private StandingPoint _element1;

			// Token: 0x04001F59 RID: 8025
			private StandingPoint _element2;

			// Token: 0x04001F5A RID: 8026
			private StandingPoint _element3;

			// Token: 0x04001F5B RID: 8027
			private StandingPoint _element4;

			// Token: 0x04001F5C RID: 8028
			private StandingPoint _element5;

			// Token: 0x04001F5D RID: 8029
			private StandingPoint _element6;

			// Token: 0x04001F5E RID: 8030
			private StandingPoint _element7;

			// Token: 0x04001F5F RID: 8031
			public const int Length = 8;
		}

		// Token: 0x02000609 RID: 1545
		private struct AgentDistanceCache
		{
			// Token: 0x04001F60 RID: 8032
			public Vec2 AgentPosition;

			// Token: 0x04001F61 RID: 8033
			public Vec2 StandingPointPosition;

			// Token: 0x04001F62 RID: 8034
			public float PathDistance;
		}
	}
}
