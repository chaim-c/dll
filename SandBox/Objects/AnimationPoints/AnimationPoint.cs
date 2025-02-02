using System;
using System.Collections.Generic;
using SandBox.Conversation;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace SandBox.Objects.AnimationPoints
{
	// Token: 0x02000043 RID: 67
	public class AnimationPoint : StandingPoint
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000EE2A File Offset: 0x0000D02A
		public override bool PlayerStopsUsingWhenInteractsWithOther
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000EE2D File Offset: 0x0000D02D
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000EE35 File Offset: 0x0000D035
		public bool IsArriveActionFinished { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000EE3E File Offset: 0x0000D03E
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000EE48 File Offset: 0x0000D048
		protected string SelectedRightHandItem
		{
			get
			{
				return this._selectedRightHandItem;
			}
			set
			{
				if (value != this._selectedRightHandItem)
				{
					AnimationPoint.ItemForBone newItem = new AnimationPoint.ItemForBone(this.RightHandItemBone, value, false);
					this.AssignItemToBone(newItem);
					this._selectedRightHandItem = value;
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000EE80 File Offset: 0x0000D080
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000EE88 File Offset: 0x0000D088
		protected string SelectedLeftHandItem
		{
			get
			{
				return this._selectedLeftHandItem;
			}
			set
			{
				if (value != this._selectedLeftHandItem)
				{
					AnimationPoint.ItemForBone newItem = new AnimationPoint.ItemForBone(this.LeftHandItemBone, value, false);
					this.AssignItemToBone(newItem);
					this._selectedLeftHandItem = value;
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		public bool IsActive { get; private set; } = true;

		// Token: 0x0600025C RID: 604 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public AnimationPoint()
		{
			this._greetingTimer = null;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000F041 File Offset: 0x0000D241
		public override bool DisableCombatActionsOnUse
		{
			get
			{
				return !base.IsInstantUse;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F04C File Offset: 0x0000D24C
		private void CreateVisualizer()
		{
			if (this.PairLoopStartActionCode != ActionIndexCache.act_none || this.LoopStartActionCode != ActionIndexCache.act_none)
			{
				this._animatedEntity = GameEntity.CreateEmpty(base.GameEntity.Scene, false);
				this._animatedEntity.EntityFlags = (this._animatedEntity.EntityFlags | EntityFlags.DontSaveToScene);
				this._animatedEntity.Name = "ap_visual_entity";
				MBActionSet actionSet = MBActionSet.GetActionSetWithIndex(0);
				ActionIndexCache actionIndexCache = ActionIndexCache.act_none;
				int numberOfActionSets = MBActionSet.GetNumberOfActionSets();
				for (int i = 0; i < numberOfActionSets; i++)
				{
					MBActionSet actionSetWithIndex = MBActionSet.GetActionSetWithIndex(i);
					if (this.ArriveActionCode == ActionIndexCache.act_none || MBActionSet.CheckActionAnimationClipExists(actionSetWithIndex, this.ArriveActionCode))
					{
						if (this.PairLoopStartActionCode != ActionIndexCache.act_none && MBActionSet.CheckActionAnimationClipExists(actionSetWithIndex, this.PairLoopStartActionCode))
						{
							actionSet = actionSetWithIndex;
							actionIndexCache = this.PairLoopStartActionCode;
							break;
						}
						if (this.LoopStartActionCode != ActionIndexCache.act_none && MBActionSet.CheckActionAnimationClipExists(actionSetWithIndex, this.LoopStartActionCode))
						{
							actionSet = actionSetWithIndex;
							actionIndexCache = this.LoopStartActionCode;
							break;
						}
					}
				}
				if (actionIndexCache == null || actionIndexCache == ActionIndexCache.act_none)
				{
					actionIndexCache = ActionIndexCache.Create("act_jump_loop");
				}
				this._animatedEntity.CreateAgentSkeleton("human_skeleton", true, actionSet, "human", MBObjectManager.Instance.GetObject<Monster>("human"));
				this._animatedEntity.Skeleton.SetAgentActionChannel(0, actionIndexCache, 0f, -0.2f, true);
				this._animatedEntity.AddMultiMeshToSkeleton(MetaMesh.GetCopy("roman_cloth_tunic_a", true, false));
				this._animatedEntity.AddMultiMeshToSkeleton(MetaMesh.GetCopy("casual_02_boots", true, false));
				this._animatedEntity.AddMultiMeshToSkeleton(MetaMesh.GetCopy("hands_male_a", true, false));
				this._animatedEntity.AddMultiMeshToSkeleton(MetaMesh.GetCopy("head_male_a", true, false));
				this._animatedEntityDisplacement = Vec3.Zero;
				if (this.ArriveActionCode != ActionIndexCache.act_none && (MBActionSet.GetActionAnimationFlags(actionSet, this.ArriveActionCode) & AnimFlags.anf_displace_position) != (AnimFlags)0UL)
				{
					this._animatedEntityDisplacement = MBActionSet.GetActionDisplacementVector(actionSet, this.ArriveActionCode);
				}
				this.UpdateAnimatedEntityFrame();
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F278 File Offset: 0x0000D478
		private void UpdateAnimatedEntityFrame()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			MatrixFrame matrixFrame = globalFrame.TransformToParent(new MatrixFrame
			{
				rotation = Mat3.Identity,
				origin = this._animatedEntityDisplacement
			});
			globalFrame.origin = matrixFrame.origin;
			this._animatedEntity.SetFrame(ref globalFrame);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		protected override void OnEditModeVisibilityChanged(bool currentVisibility)
		{
			if (this._animatedEntity != null)
			{
				this._animatedEntity.SetVisibilityExcludeParents(currentVisibility);
				if (!base.GameEntity.IsGhostObject())
				{
					this._resyncAnimations = true;
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F308 File Offset: 0x0000D508
		protected override void OnEditorTick(float dt)
		{
			if (this._animatedEntity != null)
			{
				if (this._resyncAnimations)
				{
					this.ResetAnimations();
					this._resyncAnimations = false;
				}
				bool flag = this._animatedEntity.IsVisibleIncludeParents();
				if (flag && !MBEditor.HelpersEnabled())
				{
					this._animatedEntity.SetVisibilityExcludeParents(false);
					flag = false;
				}
				if (flag)
				{
					this.UpdateAnimatedEntityFrame();
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F365 File Offset: 0x0000D565
		protected override void OnEditorInit()
		{
			this._itemsForBones = new List<AnimationPoint.ItemForBone>();
			this.SetActionCodes();
			this.InitParameters();
			if (!base.GameEntity.IsGhostObject())
			{
				this.CreateVisualizer();
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000F394 File Offset: 0x0000D594
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			if (this._animatedEntity != null && this._animatedEntity.Scene == base.GameEntity.Scene)
			{
				this._animatedEntity.Remove(removeReason);
				this._animatedEntity = null;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		protected void ResetAnimations()
		{
			ActionIndexCache actionIndexCache = ActionIndexCache.act_none;
			int numberOfActionSets = MBActionSet.GetNumberOfActionSets();
			for (int i = 0; i < numberOfActionSets; i++)
			{
				MBActionSet actionSetWithIndex = MBActionSet.GetActionSetWithIndex(i);
				if (this.ArriveActionCode == ActionIndexCache.act_none || MBActionSet.CheckActionAnimationClipExists(actionSetWithIndex, this.ArriveActionCode))
				{
					if (this.PairLoopStartActionCode != ActionIndexCache.act_none && MBActionSet.CheckActionAnimationClipExists(actionSetWithIndex, this.PairLoopStartActionCode))
					{
						actionIndexCache = this.PairLoopStartActionCode;
						break;
					}
					if (this.LoopStartActionCode != ActionIndexCache.act_none && MBActionSet.CheckActionAnimationClipExists(actionSetWithIndex, this.LoopStartActionCode))
					{
						actionIndexCache = this.LoopStartActionCode;
						break;
					}
				}
			}
			if (actionIndexCache != null && actionIndexCache != ActionIndexCache.act_none)
			{
				ActionIndexCache actionIndex = ActionIndexCache.Create("act_jump_loop");
				this._animatedEntity.Skeleton.SetAgentActionChannel(0, actionIndex, 0f, -0.2f, true);
				this._animatedEntity.Skeleton.SetAgentActionChannel(0, actionIndexCache, 0f, -0.2f, true);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000F4E6 File Offset: 0x0000D6E6
		protected override void OnEditorVariableChanged(string variableName)
		{
			if (this.ShouldUpdateOnEditorVariableChanged(variableName))
			{
				if (this._animatedEntity != null)
				{
					this._animatedEntity.Remove(91);
				}
				this.SetActionCodes();
				this.CreateVisualizer();
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F518 File Offset: 0x0000D718
		public void RequestResync()
		{
			this._resyncAnimations = true;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000F521 File Offset: 0x0000D721
		public override void AfterMissionStart()
		{
			if (Agent.Main != null && this.LoopStartActionCode != ActionIndexCache.act_none && !MBActionSet.CheckActionAnimationClipExists(Agent.Main.ActionSet, this.LoopStartActionCode))
			{
				base.IsDisabledForPlayers = true;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000F55A File Offset: 0x0000D75A
		protected virtual bool ShouldUpdateOnEditorVariableChanged(string variableName)
		{
			return variableName == "ArriveAction" || variableName == "LoopStartAction" || variableName == "PairLoopStartAction";
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000F583 File Offset: 0x0000D783
		protected void ClearAssignedItems()
		{
			this.SetAgentItemsVisibility(false);
			this._itemsForBones.Clear();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000F597 File Offset: 0x0000D797
		protected void AssignItemToBone(AnimationPoint.ItemForBone newItem)
		{
			if (!string.IsNullOrEmpty(newItem.ItemPrefabName) && !this._itemsForBones.Contains(newItem))
			{
				this._itemsForBones.Add(newItem);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public override bool IsDisabledForAgent(Agent agent)
		{
			if (base.HasUser && base.UserAgent == agent)
			{
				return !this.IsActive || base.IsDeactivated;
			}
			if (!this.IsActive || agent.MountAgent != null || base.IsDeactivated || !agent.IsOnLand() || (!agent.IsAIControlled && (base.IsDisabledForPlayers || base.HasUser)))
			{
				return true;
			}
			GameEntity parent = base.GameEntity.Parent;
			if (parent == null || !parent.HasScriptOfType<UsableMachine>() || !base.GameEntity.HasTag("alternative"))
			{
				return base.IsDisabledForAgent(agent);
			}
			if (agent.IsAIControlled && parent.HasTag("reserved"))
			{
				return true;
			}
			CampaignAgentComponent component = agent.GetComponent<CampaignAgentComponent>();
			string text = (((component != null) ? component.AgentNavigator : null) != null) ? agent.GetComponent<CampaignAgentComponent>().AgentNavigator.SpecialTargetTag : string.Empty;
			if (!string.IsNullOrEmpty(text) && !parent.HasTag(text))
			{
				return true;
			}
			using (List<StandingPoint>.Enumerator enumerator = parent.GetFirstScriptOfType<UsableMachine>().StandingPoints.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AnimationPoint animationPoint;
					if ((animationPoint = (enumerator.Current as AnimationPoint)) != null && this.GroupId == animationPoint.GroupId && !animationPoint.IsDeactivated && (animationPoint.HasUser || (animationPoint.HasAIMovingTo && !animationPoint.IsAIMovingTo(agent))) && animationPoint.GameEntity.HasTag("alternative"))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000F74C File Offset: 0x0000D94C
		protected override void OnInit()
		{
			base.OnInit();
			this._itemsForBones = new List<AnimationPoint.ItemForBone>();
			this.SetActionCodes();
			this.InitParameters();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000F778 File Offset: 0x0000D978
		private void InitParameters()
		{
			this._greetingTimer = null;
			this._pointRotation = Vec3.Zero;
			this._state = AnimationPoint.State.NotUsing;
			this._pairPoints = this.GetPairs(this.PairEntity);
			if (this.ActivatePairs)
			{
				this.SetPairsActivity(false);
			}
			this.LockUserPositions = true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		protected virtual void SetActionCodes()
		{
			this.ArriveActionCode = ActionIndexCache.Create(this.ArriveAction);
			this.LoopStartActionCode = ActionIndexCache.Create(this.LoopStartAction);
			this.PairLoopStartActionCode = ActionIndexCache.Create(this.PairLoopStartAction);
			this.LeaveActionCode = ActionIndexCache.Create(this.LeaveAction);
			this.SelectedRightHandItem = this.RightHandItem;
			this.SelectedLeftHandItem = this.LeftHandItem;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000F831 File Offset: 0x0000DA31
		protected override bool DoesActionTypeStopUsingGameObject(Agent.ActionCodeType actionType)
		{
			return false;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000F834 File Offset: 0x0000DA34
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.HasUser)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000F84D File Offset: 0x0000DA4D
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			this.Tick(dt, false);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000F860 File Offset: 0x0000DA60
		private List<AnimationPoint> GetPairs(GameEntity entity)
		{
			List<AnimationPoint> list = new List<AnimationPoint>();
			if (entity != null)
			{
				if (entity.HasScriptOfType<AnimationPoint>())
				{
					AnimationPoint firstScriptOfType = entity.GetFirstScriptOfType<AnimationPoint>();
					list.Add(firstScriptOfType);
				}
				else
				{
					foreach (GameEntity entity2 in entity.GetChildren())
					{
						list.AddRange(this.GetPairs(entity2));
					}
				}
			}
			if (list.Contains(this))
			{
				list.Remove(this);
			}
			return list;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		public override WorldFrame GetUserFrameForAgent(Agent agent)
		{
			WorldFrame userFrameForAgent = base.GetUserFrameForAgent(agent);
			float agentScale = agent.AgentScale;
			userFrameForAgent.Origin.SetVec2(userFrameForAgent.Origin.AsVec2 + (userFrameForAgent.Rotation.f.AsVec2 * -this.ForwardDistanceToPivotPoint + userFrameForAgent.Rotation.s.AsVec2 * this.SideDistanceToPivotPoint) * (1f - agentScale));
			return userFrameForAgent;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000F970 File Offset: 0x0000DB70
		private void Tick(float dt, bool isSimulation = false)
		{
			if (base.HasUser)
			{
				if (Game.Current != null && Game.Current.IsDevelopmentMode)
				{
					base.UserAgent.GetTargetPosition().IsNonZero();
				}
				ActionIndexValueCache currentActionValue = base.UserAgent.GetCurrentActionValue(0);
				switch (this._state)
				{
				case AnimationPoint.State.NotUsing:
					if (this.IsTargetReached() && base.UserAgent.MovementVelocity.LengthSquared < 0.1f && base.UserAgent.IsOnLand())
					{
						if (this.ArriveActionCode != ActionIndexCache.act_none)
						{
							Agent userAgent = base.UserAgent;
							int channelNo = 0;
							ActionIndexCache arriveActionCode = this.ArriveActionCode;
							bool ignorePriority = false;
							ulong additionalFlags = 0UL;
							float blendWithNextActionFactor = 0f;
							float blendInPeriod = (float)(isSimulation ? 0 : 0);
							userAgent.SetActionChannel(channelNo, arriveActionCode, ignorePriority, additionalFlags, blendWithNextActionFactor, MBRandom.RandomFloatRanged(0.8f, 1f), blendInPeriod, 0.4f, 0f, false, -0.2f, 0, true);
						}
						this._state = AnimationPoint.State.StartToUse;
						return;
					}
					break;
				case AnimationPoint.State.StartToUse:
					if (this.ArriveActionCode != ActionIndexCache.act_none && isSimulation)
					{
						this.SimulateAnimations(0.1f);
					}
					if (this.ArriveActionCode == ActionIndexCache.act_none || currentActionValue == this.ArriveActionCode || base.UserAgent.ActionSet.AreActionsAlternatives(currentActionValue, this.ArriveActionCode))
					{
						base.UserAgent.ClearTargetFrame();
						WorldFrame userFrameForAgent = this.GetUserFrameForAgent(base.UserAgent);
						this._pointRotation = userFrameForAgent.Rotation.f;
						this._pointRotation.Normalize();
						if (base.UserAgent != Agent.Main)
						{
							base.UserAgent.SetScriptedPositionAndDirection(ref userFrameForAgent.Origin, userFrameForAgent.Rotation.f.AsVec2.RotationInRadians, false, Agent.AIScriptedFrameFlags.DoNotRun);
						}
						this._state = AnimationPoint.State.Using;
						return;
					}
					break;
				case AnimationPoint.State.Using:
					if (isSimulation)
					{
						float dt2 = 0.1f;
						if (currentActionValue != this.ArriveActionCode)
						{
							dt2 = 0.01f + MBRandom.RandomFloat * 0.09f;
						}
						this.SimulateAnimations(dt2);
					}
					if (!this.IsArriveActionFinished && (this.ArriveActionCode == ActionIndexCache.act_none || base.UserAgent.GetCurrentActionValue(0) != this.ArriveActionCode))
					{
						this.IsArriveActionFinished = true;
						this.AddItemsToAgent();
					}
					if (this.IsRotationCorrectDuringUsage())
					{
						base.UserAgent.SetActionChannel(0, this.LoopStartActionCode, false, 0UL, 0f, (this.ActionSpeed < 0.8f) ? this.ActionSpeed : MBRandom.RandomFloatRanged(0.8f, this.ActionSpeed), (float)(isSimulation ? 0 : 0), 0.4f, isSimulation ? MBRandom.RandomFloatRanged(0f, 0.5f) : 0f, false, -0.2f, 0, true);
					}
					if (this.IsArriveActionFinished && base.UserAgent != Agent.Main)
					{
						this.PairTick(isSimulation);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		private void PairTick(bool isSimulation)
		{
			MBList<Agent> pairEntityUsers = this.GetPairEntityUsers();
			if (this.PairEntity != null)
			{
				bool agentItemsVisibility = base.UserAgent != ConversationMission.OneToOneConversationAgent && pairEntityUsers.Count + 1 >= this.MinUserToStartInteraction;
				this.SetAgentItemsVisibility(agentItemsVisibility);
			}
			if (this._pairState != AnimationPoint.PairState.NoPair && pairEntityUsers.Count < this.MinUserToStartInteraction)
			{
				this._pairState = AnimationPoint.PairState.NoPair;
				if (base.UserAgent != ConversationMission.OneToOneConversationAgent)
				{
					base.UserAgent.SetActionChannel(0, this._lastAction, false, (ulong)((long)base.UserAgent.GetCurrentActionPriority(0)), 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					base.UserAgent.ResetLookAgent();
				}
				this._greetingTimer = null;
			}
			else if (this._pairState == AnimationPoint.PairState.NoPair && pairEntityUsers.Count >= this.MinUserToStartInteraction && this.IsRotationCorrectDuringUsage())
			{
				this._lastAction = base.UserAgent.GetCurrentActionValue(0);
				if (this._startPairAnimationWithGreeting)
				{
					this._pairState = AnimationPoint.PairState.BecomePair;
					this._greetingTimer = new Timer(Mission.Current.CurrentTime, (float)MBRandom.RandomInt(5) * 0.3f, true);
				}
				else
				{
					this._pairState = AnimationPoint.PairState.StartPairAnimation;
				}
			}
			else if (this._pairState == AnimationPoint.PairState.BecomePair && this._greetingTimer.Check(Mission.Current.CurrentTime))
			{
				this._greetingTimer = null;
				this._pairState = AnimationPoint.PairState.Greeting;
				Vec3 eyeGlobalPosition = pairEntityUsers.GetRandomElement<Agent>().GetEyeGlobalPosition();
				Vec3 eyeGlobalPosition2 = base.UserAgent.GetEyeGlobalPosition();
				Vec3 v = eyeGlobalPosition - eyeGlobalPosition2;
				v.Normalize();
				Mat3 rotation = base.UserAgent.Frame.rotation;
				if (Vec3.DotProduct(rotation.f, v) > 0f)
				{
					ActionIndexCache greetingActionId = this.GetGreetingActionId(eyeGlobalPosition2, eyeGlobalPosition, rotation);
					base.UserAgent.SetActionChannel(1, greetingActionId, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				}
			}
			else if (this._pairState == AnimationPoint.PairState.Greeting && base.UserAgent.GetCurrentActionValue(1) == ActionIndexCache.act_none)
			{
				this._pairState = AnimationPoint.PairState.StartPairAnimation;
			}
			if (this._pairState == AnimationPoint.PairState.StartPairAnimation)
			{
				this._pairState = AnimationPoint.PairState.Pair;
				base.UserAgent.SetActionChannel(0, this.PairLoopStartActionCode, false, 0UL, 0f, MBRandom.RandomFloatRanged(0.8f, this.ActionSpeed), (float)(isSimulation ? 0 : 0), 0.4f, isSimulation ? MBRandom.RandomFloatRanged(0f, 0.5f) : 0f, false, -0.2f, 0, true);
			}
			if (this._pairState == AnimationPoint.PairState.Pair && this.IsRotationCorrectDuringUsage())
			{
				base.UserAgent.SetActionChannel(0, this.PairLoopStartActionCode, false, 0UL, 0f, MBRandom.RandomFloatRanged(0.8f, this.ActionSpeed), (float)(isSimulation ? 0 : 0), 0.4f, isSimulation ? MBRandom.RandomFloatRanged(0f, 0.5f) : 0f, false, -0.2f, 0, true);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000FF50 File Offset: 0x0000E150
		private ActionIndexCache GetGreetingActionId(Vec3 userAgentGlobalEyePoint, Vec3 lookTarget, Mat3 userAgentRot)
		{
			Vec3 vec = lookTarget - userAgentGlobalEyePoint;
			vec.Normalize();
			float num = Vec3.DotProduct(userAgentRot.f, vec);
			if (num > 0.8f)
			{
				return this._greetingFrontActions[MBRandom.RandomInt(this._greetingFrontActions.Length)];
			}
			if (num <= 0f)
			{
				return ActionIndexCache.act_none;
			}
			if (Vec3.DotProduct(Vec3.CrossProduct(vec, userAgentRot.f), userAgentRot.u) > 0f)
			{
				return this._greetingRightActions[MBRandom.RandomInt(this._greetingRightActions.Length)];
			}
			return this._greetingLeftActions[MBRandom.RandomInt(this._greetingLeftActions.Length)];
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		private MBList<Agent> GetPairEntityUsers()
		{
			MBList<Agent> mblist = new MBList<Agent>();
			if (base.UserAgent != ConversationMission.OneToOneConversationAgent)
			{
				foreach (AnimationPoint animationPoint in this._pairPoints)
				{
					if (animationPoint.HasUser && animationPoint._state == AnimationPoint.State.Using && animationPoint.UserAgent != ConversationMission.OneToOneConversationAgent)
					{
						mblist.Add(animationPoint.UserAgent);
					}
				}
			}
			return mblist;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00010078 File Offset: 0x0000E278
		private void SetPairsActivity(bool isActive)
		{
			foreach (AnimationPoint animationPoint in this._pairPoints)
			{
				animationPoint.IsActive = isActive;
				if (!isActive)
				{
					if (animationPoint.HasAIUser)
					{
						animationPoint.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					Agent movingAgent = animationPoint.MovingAgent;
					if (movingAgent != null)
					{
						movingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public override bool IsUsableByAgent(Agent userAgent)
		{
			return this.IsActive && base.IsUsableByAgent(userAgent);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001010C File Offset: 0x0000E30C
		public override void OnUse(Agent userAgent)
		{
			base.OnUse(userAgent);
			this._equipmentIndexMainHand = base.UserAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			this._equipmentIndexOffHand = base.UserAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			this._state = AnimationPoint.State.NotUsing;
			if (this.ActivatePairs)
			{
				this.SetPairsActivity(true);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0001015C File Offset: 0x0000E35C
		private void RevertWeaponWieldSheathState()
		{
			if (this._equipmentIndexMainHand != EquipmentIndex.None && this.AutoSheathWeapons)
			{
				base.UserAgent.TryToWieldWeaponInSlot(this._equipmentIndexMainHand, Agent.WeaponWieldActionType.WithAnimation, false);
			}
			else if (this._equipmentIndexMainHand == EquipmentIndex.None && this.AutoWieldWeapons)
			{
				base.UserAgent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimation);
			}
			if (this._equipmentIndexOffHand != EquipmentIndex.None && this.AutoSheathWeapons)
			{
				base.UserAgent.TryToWieldWeaponInSlot(this._equipmentIndexOffHand, Agent.WeaponWieldActionType.WithAnimation, false);
				return;
			}
			if (this._equipmentIndexOffHand == EquipmentIndex.None && this.AutoWieldWeapons)
			{
				base.UserAgent.TryToSheathWeaponInHand(Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.WithAnimation);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public override void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			this.SetAgentItemsVisibility(false);
			this.RevertWeaponWieldSheathState();
			if (base.UserAgent.IsActive())
			{
				if (this.LeaveActionCode == ActionIndexCache.act_none)
				{
					base.UserAgent.SetActionChannel(0, this.LeaveActionCode, false, (ulong)((long)base.UserAgent.GetCurrentActionPriority(0)), 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				}
				else if (this.IsArriveActionFinished)
				{
					ActionIndexValueCache currentActionValue = base.UserAgent.GetCurrentActionValue(0);
					if (currentActionValue != this.LeaveActionCode && !base.UserAgent.ActionSet.AreActionsAlternatives(currentActionValue, this.LeaveActionCode))
					{
						base.UserAgent.SetActionChannel(0, this.LeaveActionCode, false, (ulong)((long)base.UserAgent.GetCurrentActionPriority(0)), 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					}
				}
				else
				{
					ActionIndexValueCache currentActionValue2 = userAgent.GetCurrentActionValue(0);
					if (currentActionValue2 == this.ArriveActionCode && this.ArriveActionCode != ActionIndexCache.act_none)
					{
						MBActionSet actionSet = userAgent.ActionSet;
						float currentActionProgress = userAgent.GetCurrentActionProgress(0);
						float actionBlendOutStartProgress = MBActionSet.GetActionBlendOutStartProgress(actionSet, currentActionValue2);
						if (currentActionProgress < actionBlendOutStartProgress)
						{
							float num = (actionBlendOutStartProgress - currentActionProgress) / actionBlendOutStartProgress;
							MBActionSet.GetActionBlendOutStartProgress(actionSet, this.LeaveActionCode);
						}
					}
				}
			}
			this._pairState = AnimationPoint.PairState.NoPair;
			this._lastAction = ActionIndexValueCache.act_none;
			if (base.UserAgent.GetLookAgent() != null)
			{
				base.UserAgent.ResetLookAgent();
			}
			this.IsArriveActionFinished = false;
			base.OnUseStopped(userAgent, isSuccessful, preferenceIndex);
			if (this.ActivatePairs)
			{
				this.SetPairsActivity(false);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000103A6 File Offset: 0x0000E5A6
		public override void SimulateTick(float dt)
		{
			this.Tick(dt, true);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000103B0 File Offset: 0x0000E5B0
		public override bool HasAlternative()
		{
			return this.GroupId >= 0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000103C0 File Offset: 0x0000E5C0
		public float GetRandomWaitInSeconds()
		{
			if (this.MinWaitinSeconds < 0f || this.MaxWaitInSeconds < 0f)
			{
				return -1f;
			}
			if (MathF.Abs(this.MinWaitinSeconds - this.MaxWaitInSeconds) >= 1E-45f)
			{
				return this.MinWaitinSeconds + MBRandom.RandomFloat * (this.MaxWaitInSeconds - this.MinWaitinSeconds);
			}
			return this.MinWaitinSeconds;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00010428 File Offset: 0x0000E628
		public List<AnimationPoint> GetAlternatives()
		{
			List<AnimationPoint> list = new List<AnimationPoint>();
			IEnumerable<GameEntity> children = base.GameEntity.Parent.GetChildren();
			if (children != null)
			{
				foreach (GameEntity gameEntity in children)
				{
					AnimationPoint firstScriptOfType = gameEntity.GetFirstScriptOfType<AnimationPoint>();
					if (firstScriptOfType != null && firstScriptOfType.HasAlternative() && this.GroupId == firstScriptOfType.GroupId)
					{
						list.Add(firstScriptOfType);
					}
				}
			}
			return list;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000104AC File Offset: 0x0000E6AC
		private void SimulateAnimations(float dt)
		{
			base.UserAgent.TickActionChannels(dt);
			Vec3 v = base.UserAgent.ComputeAnimationDisplacement(dt);
			if (v.LengthSquared > 0f)
			{
				base.UserAgent.TeleportToPosition(base.UserAgent.Position + v);
			}
			base.UserAgent.AgentVisuals.GetSkeleton().TickAnimations(dt, base.UserAgent.AgentVisuals.GetGlobalFrame(), true);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00010524 File Offset: 0x0000E724
		private bool IsTargetReached()
		{
			float num = Vec2.DotProduct(base.UserAgent.GetTargetDirection().AsVec2, base.UserAgent.GetMovementDirection());
			return (base.UserAgent.Position.AsVec2 - base.UserAgent.GetTargetPosition()).LengthSquared < 0.040000003f && num > 0.99f;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00010591 File Offset: 0x0000E791
		public bool IsRotationCorrectDuringUsage()
		{
			return this._pointRotation.IsNonZero && Vec2.DotProduct(this._pointRotation.AsVec2, base.UserAgent.GetMovementDirection()) > 0.99f;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000105C4 File Offset: 0x0000E7C4
		protected bool CanAgentUseItem(Agent agent)
		{
			return agent.GetComponent<CampaignAgentComponent>() != null && agent.GetComponent<CampaignAgentComponent>().AgentNavigator != null;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000105E0 File Offset: 0x0000E7E0
		protected void AddItemsToAgent()
		{
			if (this.CanAgentUseItem(base.UserAgent) && this.IsArriveActionFinished)
			{
				if (this._itemsForBones.Count != 0)
				{
					base.UserAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.HoldAndHideRecentlyUsedMeshes();
				}
				foreach (AnimationPoint.ItemForBone itemForBone in this._itemsForBones)
				{
					ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(itemForBone.ItemPrefabName);
					if (@object != null)
					{
						EquipmentIndex equipmentIndex = this.FindProperSlot(@object);
						if (!base.UserAgent.Equipment[equipmentIndex].IsEmpty)
						{
							base.UserAgent.DropItem(equipmentIndex, WeaponClass.Undefined);
						}
						ItemObject item = @object;
						ItemModifier itemModifier = null;
						IAgentOriginBase origin = base.UserAgent.Origin;
						MissionWeapon missionWeapon = new MissionWeapon(item, itemModifier, (origin != null) ? origin.Banner : null);
						base.UserAgent.EquipWeaponWithNewEntity(equipmentIndex, ref missionWeapon);
						base.UserAgent.TryToWieldWeaponInSlot(equipmentIndex, Agent.WeaponWieldActionType.Instant, false);
					}
					else
					{
						sbyte realBoneIndex = base.UserAgent.AgentVisuals.GetRealBoneIndex(itemForBone.HumanBone);
						base.UserAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.SetPrefabVisibility(realBoneIndex, itemForBone.ItemPrefabName, true);
					}
				}
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00010730 File Offset: 0x0000E930
		public override void OnUserConversationStart()
		{
			this._pointRotation = base.UserAgent.Frame.rotation.f;
			this._pointRotation.Normalize();
			if (!this.KeepOldVisibility)
			{
				foreach (AnimationPoint.ItemForBone itemForBone in this._itemsForBones)
				{
					itemForBone.OldVisibility = itemForBone.IsVisible;
				}
				this.SetAgentItemsVisibility(false);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000107C0 File Offset: 0x0000E9C0
		public override void OnUserConversationEnd()
		{
			base.UserAgent.ResetLookAgent();
			base.UserAgent.LookDirection = this._pointRotation;
			base.UserAgent.SetActionChannel(0, this.LoopStartActionCode, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
			foreach (AnimationPoint.ItemForBone itemForBone in this._itemsForBones)
			{
				if (itemForBone.OldVisibility)
				{
					this.SetAgentItemVisibility(itemForBone, true);
				}
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00010870 File Offset: 0x0000EA70
		public void SetAgentItemsVisibility(bool isVisible)
		{
			if (!base.UserAgent.IsMainAgent)
			{
				foreach (AnimationPoint.ItemForBone itemForBone in this._itemsForBones)
				{
					sbyte realBoneIndex = base.UserAgent.AgentVisuals.GetRealBoneIndex(itemForBone.HumanBone);
					base.UserAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.SetPrefabVisibility(realBoneIndex, itemForBone.ItemPrefabName, isVisible);
					AnimationPoint.ItemForBone itemForBone2 = itemForBone;
					itemForBone2.IsVisible = isVisible;
				}
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00010908 File Offset: 0x0000EB08
		private void SetAgentItemVisibility(AnimationPoint.ItemForBone item, bool isVisible)
		{
			sbyte realBoneIndex = base.UserAgent.AgentVisuals.GetRealBoneIndex(item.HumanBone);
			base.UserAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.SetPrefabVisibility(realBoneIndex, item.ItemPrefabName, isVisible);
			item.IsVisible = isVisible;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00010954 File Offset: 0x0000EB54
		private EquipmentIndex FindProperSlot(ItemObject item)
		{
			EquipmentIndex result = EquipmentIndex.Weapon3;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex <= EquipmentIndex.Weapon3; equipmentIndex++)
			{
				if (base.UserAgent.Equipment[equipmentIndex].IsEmpty)
				{
					result = equipmentIndex;
				}
				else if (base.UserAgent.Equipment[equipmentIndex].Item == item)
				{
					return equipmentIndex;
				}
			}
			return result;
		}

		// Token: 0x040000F3 RID: 243
		private const string AlternativeTag = "alternative";

		// Token: 0x040000F4 RID: 244
		private const float RangeThreshold = 0.2f;

		// Token: 0x040000F5 RID: 245
		private const float RotationScoreThreshold = 0.99f;

		// Token: 0x040000F6 RID: 246
		private const float ActionSpeedRandomMinValue = 0.8f;

		// Token: 0x040000F7 RID: 247
		private const float AnimationRandomProgressMaxValue = 0.5f;

		// Token: 0x040000F8 RID: 248
		public string ArriveAction = "";

		// Token: 0x040000F9 RID: 249
		public string LoopStartAction = "";

		// Token: 0x040000FA RID: 250
		public string PairLoopStartAction = "";

		// Token: 0x040000FB RID: 251
		public string LeaveAction = "";

		// Token: 0x040000FC RID: 252
		public int GroupId = -1;

		// Token: 0x040000FD RID: 253
		public string RightHandItem = "";

		// Token: 0x040000FE RID: 254
		public HumanBone RightHandItemBone = HumanBone.ItemR;

		// Token: 0x040000FF RID: 255
		public string LeftHandItem = "";

		// Token: 0x04000100 RID: 256
		public HumanBone LeftHandItemBone = HumanBone.ItemL;

		// Token: 0x04000101 RID: 257
		public GameEntity PairEntity;

		// Token: 0x04000102 RID: 258
		public int MinUserToStartInteraction = 1;

		// Token: 0x04000103 RID: 259
		public bool ActivatePairs;

		// Token: 0x04000104 RID: 260
		public float MinWaitinSeconds = 30f;

		// Token: 0x04000105 RID: 261
		public float MaxWaitInSeconds = 120f;

		// Token: 0x04000106 RID: 262
		public float ForwardDistanceToPivotPoint;

		// Token: 0x04000107 RID: 263
		public float SideDistanceToPivotPoint;

		// Token: 0x04000108 RID: 264
		private bool _startPairAnimationWithGreeting;

		// Token: 0x04000109 RID: 265
		protected float ActionSpeed = 1f;

		// Token: 0x0400010A RID: 266
		public bool KeepOldVisibility;

		// Token: 0x0400010C RID: 268
		private ActionIndexCache ArriveActionCode;

		// Token: 0x0400010D RID: 269
		protected ActionIndexCache LoopStartActionCode;

		// Token: 0x0400010E RID: 270
		protected ActionIndexCache PairLoopStartActionCode;

		// Token: 0x0400010F RID: 271
		private ActionIndexCache LeaveActionCode;

		// Token: 0x04000110 RID: 272
		protected ActionIndexCache DefaultActionCode;

		// Token: 0x04000111 RID: 273
		private bool _resyncAnimations;

		// Token: 0x04000112 RID: 274
		private string _selectedRightHandItem;

		// Token: 0x04000113 RID: 275
		private string _selectedLeftHandItem;

		// Token: 0x04000114 RID: 276
		private AnimationPoint.State _state;

		// Token: 0x04000115 RID: 277
		private AnimationPoint.PairState _pairState;

		// Token: 0x04000116 RID: 278
		private Vec3 _pointRotation;

		// Token: 0x04000117 RID: 279
		private List<AnimationPoint> _pairPoints;

		// Token: 0x04000118 RID: 280
		private List<AnimationPoint.ItemForBone> _itemsForBones;

		// Token: 0x04000119 RID: 281
		private ActionIndexValueCache _lastAction;

		// Token: 0x0400011A RID: 282
		private Timer _greetingTimer;

		// Token: 0x0400011B RID: 283
		private GameEntity _animatedEntity;

		// Token: 0x0400011C RID: 284
		private Vec3 _animatedEntityDisplacement = Vec3.Zero;

		// Token: 0x0400011D RID: 285
		private EquipmentIndex _equipmentIndexMainHand;

		// Token: 0x0400011E RID: 286
		private EquipmentIndex _equipmentIndexOffHand;

		// Token: 0x04000120 RID: 288
		private readonly ActionIndexCache[] _greetingFrontActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_greeting_front_1"),
			ActionIndexCache.Create("act_greeting_front_2"),
			ActionIndexCache.Create("act_greeting_front_3"),
			ActionIndexCache.Create("act_greeting_front_4")
		};

		// Token: 0x04000121 RID: 289
		private readonly ActionIndexCache[] _greetingRightActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_greeting_right_1"),
			ActionIndexCache.Create("act_greeting_right_2"),
			ActionIndexCache.Create("act_greeting_right_3"),
			ActionIndexCache.Create("act_greeting_right_4")
		};

		// Token: 0x04000122 RID: 290
		private readonly ActionIndexCache[] _greetingLeftActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_greeting_left_1"),
			ActionIndexCache.Create("act_greeting_left_2"),
			ActionIndexCache.Create("act_greeting_left_3"),
			ActionIndexCache.Create("act_greeting_left_4")
		};

		// Token: 0x0200011F RID: 287
		protected struct ItemForBone
		{
			// Token: 0x06000BAF RID: 2991 RVA: 0x000525CF File Offset: 0x000507CF
			public ItemForBone(HumanBone bone, string name, bool isVisible)
			{
				this.HumanBone = bone;
				this.ItemPrefabName = name;
				this.IsVisible = isVisible;
				this.OldVisibility = isVisible;
			}

			// Token: 0x0400051E RID: 1310
			public HumanBone HumanBone;

			// Token: 0x0400051F RID: 1311
			public string ItemPrefabName;

			// Token: 0x04000520 RID: 1312
			public bool IsVisible;

			// Token: 0x04000521 RID: 1313
			public bool OldVisibility;
		}

		// Token: 0x02000120 RID: 288
		private enum State
		{
			// Token: 0x04000523 RID: 1315
			NotUsing,
			// Token: 0x04000524 RID: 1316
			StartToUse,
			// Token: 0x04000525 RID: 1317
			Using
		}

		// Token: 0x02000121 RID: 289
		private enum PairState
		{
			// Token: 0x04000527 RID: 1319
			NoPair,
			// Token: 0x04000528 RID: 1320
			BecomePair,
			// Token: 0x04000529 RID: 1321
			Greeting,
			// Token: 0x0400052A RID: 1322
			StartPairAnimation,
			// Token: 0x0400052B RID: 1323
			Pair
		}
	}
}
