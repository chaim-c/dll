using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036B RID: 875
	public abstract class UsableMissionObject : SynchedMissionObject, IFocusable, IUsable, IVisible
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002FFF RID: 12287 RVA: 0x000C6AB0 File Offset: 0x000C4CB0
		public virtual FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.Item;
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000C6AB3 File Offset: 0x000C4CB3
		public virtual void OnUserConversationStart()
		{
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000C6AB5 File Offset: 0x000C4CB5
		public virtual void OnUserConversationEnd()
		{
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x000C6AB7 File Offset: 0x000C4CB7
		// (set) Token: 0x06003003 RID: 12291 RVA: 0x000C6ABF File Offset: 0x000C4CBF
		public Agent UserAgent
		{
			get
			{
				return this._userAgent;
			}
			private set
			{
				if (this._userAgent != value)
				{
					this.PreviousUserAgent = this._userAgent;
					this._userAgent = value;
					base.SetScriptComponentToTickMT(this.GetTickRequirement());
				}
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003004 RID: 12292 RVA: 0x000C6AE9 File Offset: 0x000C4CE9
		// (set) Token: 0x06003005 RID: 12293 RVA: 0x000C6AF1 File Offset: 0x000C4CF1
		public Agent PreviousUserAgent { get; private set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x000C6AFA File Offset: 0x000C4CFA
		// (set) Token: 0x06003007 RID: 12295 RVA: 0x000C6B02 File Offset: 0x000C4D02
		public GameEntityWithWorldPosition GameEntityWithWorldPosition { get; private set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000C6B0B File Offset: 0x000C4D0B
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x000C6B13 File Offset: 0x000C4D13
		public virtual Agent MovingAgent { get; private set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000C6B1C File Offset: 0x000C4D1C
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x000C6B24 File Offset: 0x000C4D24
		public List<Agent> DefendingAgents { get; private set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000C6B2D File Offset: 0x000C4D2D
		public bool HasDefendingAgent
		{
			get
			{
				return this.DefendingAgents != null && this.GetDefendingAgentCount() > 0;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000C6B42 File Offset: 0x000C4D42
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x000C6B4A File Offset: 0x000C4D4A
		public bool IsInstantUse { get; protected set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000C6B53 File Offset: 0x000C4D53
		// (set) Token: 0x06003010 RID: 12304 RVA: 0x000C6B5C File Offset: 0x000C4D5C
		public bool IsDeactivated
		{
			get
			{
				return this._isDeactivated;
			}
			set
			{
				if (value != this._isDeactivated)
				{
					this._isDeactivated = value;
					if (this._isDeactivated && !GameNetwork.IsClientOrReplay)
					{
						Agent userAgent = this.UserAgent;
						if (userAgent != null)
						{
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
						bool flag = false;
						while (this.HasAIMovingTo)
						{
							this.MovingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							flag = true;
						}
						while (this.HasDefendingAgent)
						{
							this.DefendingAgents[0].StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							flag = true;
						}
						if (flag)
						{
							base.SetScriptComponentToTick(this.GetTickRequirement());
						}
					}
				}
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x000C6BE4 File Offset: 0x000C4DE4
		// (set) Token: 0x06003012 RID: 12306 RVA: 0x000C6BEC File Offset: 0x000C4DEC
		public bool IsDisabledForPlayers
		{
			get
			{
				return this._isDisabledForPlayers;
			}
			set
			{
				if (value != this._isDisabledForPlayers)
				{
					this._isDisabledForPlayers = value;
					if (this._isDisabledForPlayers && !GameNetwork.IsClientOrReplay && this.UserAgent != null && !this.UserAgent.IsAIControlled)
					{
						this.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000C6C3A File Offset: 0x000C4E3A
		public void SetIsDeactivatedSynched(bool value)
		{
			if (this.IsDeactivated != value)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetUsableMissionObjectIsDeactivated(base.Id, value));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this.IsDeactivated = value;
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000C6C70 File Offset: 0x000C4E70
		public void SetIsDisabledForPlayersSynched(bool value)
		{
			if (this.IsDisabledForPlayers != value)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetUsableMissionObjectIsDisabledForPlayers(base.Id, value));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this.IsDisabledForPlayers = value;
			}
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000C6CA6 File Offset: 0x000C4EA6
		public virtual bool IsDisabledForAgent(Agent agent)
		{
			return this.IsDeactivated || agent.MountAgent != null || (this.IsDisabledForPlayers && !agent.IsAIControlled) || !agent.IsOnLand();
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000C6CD3 File Offset: 0x000C4ED3
		protected UsableMissionObject(bool isInstantUse = false)
		{
			this._components = new List<UsableMissionObjectComponent>();
			this.IsInstantUse = isInstantUse;
			this.GameEntityWithWorldPosition = null;
			this._needsSingleThreadTickOnce = false;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000C6D11 File Offset: 0x000C4F11
		public void AddComponent(UsableMissionObjectComponent component)
		{
			this._components.Add(component);
			component.OnAdded(base.Scene);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000C6D37 File Offset: 0x000C4F37
		public void RemoveComponent(UsableMissionObjectComponent component)
		{
			component.OnRemoved();
			this._components.Remove(component);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000C6D58 File Offset: 0x000C4F58
		public T GetComponent<T>() where T : UsableMissionObjectComponent
		{
			return this._components.Find((UsableMissionObjectComponent c) => c is T) as T;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000C6D8E File Offset: 0x000C4F8E
		private void CollectChildEntities()
		{
			this.CollectChildEntitiesAux(base.GameEntity);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000C6D9C File Offset: 0x000C4F9C
		private void CollectChildEntitiesAux(GameEntity entity)
		{
			foreach (GameEntity gameEntity in entity.GetChildren())
			{
				this.CollectChildEntity(gameEntity);
				if (gameEntity.GetScriptComponents().IsEmpty<ScriptComponentBehavior>())
				{
					this.CollectChildEntitiesAux(gameEntity);
				}
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000C6E00 File Offset: 0x000C5000
		public void RefreshGameEntityWithWorldPosition()
		{
			this.GameEntityWithWorldPosition = new GameEntityWithWorldPosition(base.GameEntity);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000C6E13 File Offset: 0x000C5013
		protected virtual void CollectChildEntity(GameEntity childEntity)
		{
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000C6E15 File Offset: 0x000C5015
		protected virtual bool VerifyChildEntities(ref string errorMessage)
		{
			return true;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000C6E18 File Offset: 0x000C5018
		protected internal override void OnInit()
		{
			base.OnInit();
			this.CollectChildEntities();
			this.LockUserFrames = !this.IsInstantUse;
			this.RefreshGameEntityWithWorldPosition();
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000C6E3B File Offset: 0x000C503B
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.CollectChildEntities();
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000C6E4C File Offset: 0x000C504C
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnMissionReset();
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000C6EA4 File Offset: 0x000C50A4
		public virtual void OnFocusGain(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusGain(userAgent);
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000C6EF8 File Offset: 0x000C50F8
		public virtual void OnFocusLose(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusLose(userAgent);
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000C6F4C File Offset: 0x000C514C
		public virtual TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return TextObject.Empty;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000C6F53 File Offset: 0x000C5153
		public virtual void SetUserForClient(Agent userAgent)
		{
			Agent userAgent2 = this.UserAgent;
			if (userAgent2 != null)
			{
				userAgent2.SetUsedGameObjectForClient(null);
			}
			this.UserAgent = userAgent;
			if (userAgent != null)
			{
				userAgent.SetUsedGameObjectForClient(this);
			}
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000C6F78 File Offset: 0x000C5178
		public virtual void OnUse(Agent userAgent)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				if (!userAgent.IsAIControlled && this.HasAIUser)
				{
					this.UserAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				if (this.IsAIMovingTo(userAgent))
				{
					Formation formation = userAgent.Formation;
					if (formation != null)
					{
						formation.Team.DetachmentManager.RemoveAgentAsMovingToDetachment(userAgent);
					}
					this.RemoveMovingAgent(userAgent);
					base.SetScriptComponentToTick(this.GetTickRequirement());
				}
				while (this.HasAIMovingTo && !this.IsInstantUse)
				{
					this.MovingAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
				{
					usableMissionObjectComponent.OnUse(userAgent);
				}
				this.UserAgent = userAgent;
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new UseObject(userAgent.Index, base.Id));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					return;
				}
			}
			else
			{
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
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000C70CC File Offset: 0x000C52CC
		public virtual void OnAIMoveToUse(Agent userAgent, IDetachment detachment)
		{
			this.AddMovingAgent(userAgent);
			Formation formation = userAgent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.AddAgentAsMovingToDetachment(userAgent, detachment);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000C7100 File Offset: 0x000C5300
		public virtual void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnUseStopped(userAgent, isSuccessful);
			}
			this.UserAgent = null;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000C715C File Offset: 0x000C535C
		public virtual void OnMoveToStopped(Agent movingAgent)
		{
			Formation formation = movingAgent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.RemoveAgentAsMovingToDetachment(movingAgent);
			}
			this.RemoveMovingAgent(movingAgent);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000C718D File Offset: 0x000C538D
		public virtual int GetMovingAgentCount()
		{
			if (this.MovingAgent == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000C719A File Offset: 0x000C539A
		public virtual Agent GetMovingAgentWithIndex(int index)
		{
			return this.MovingAgent;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000C71A2 File Offset: 0x000C53A2
		public virtual void RemoveMovingAgent(Agent movingAgent)
		{
			this.MovingAgent = null;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000C71AB File Offset: 0x000C53AB
		public virtual void AddMovingAgent(Agent movingAgent)
		{
			this.MovingAgent = movingAgent;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000C71B4 File Offset: 0x000C53B4
		public void OnAIDefendBegin(Agent agent, IDetachment detachment)
		{
			this.AddDefendingAgent(agent);
			Formation formation = agent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.AddAgentAsDefendingToDetachment(agent, detachment);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000C71E6 File Offset: 0x000C53E6
		public void OnAIDefendEnd(Agent agent)
		{
			Formation formation = agent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.RemoveAgentAsDefendingToDetachment(agent);
			}
			this.RemoveDefendingAgent(agent);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000C7217 File Offset: 0x000C5417
		public void InitializeDefendingAgents()
		{
			if (this.DefendingAgents == null)
			{
				this.DefendingAgents = new List<Agent>();
			}
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000C722C File Offset: 0x000C542C
		public int GetDefendingAgentCount()
		{
			return this.DefendingAgents.Count;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000C7239 File Offset: 0x000C5439
		public void AddDefendingAgent(Agent agent)
		{
			this.DefendingAgents.Add(agent);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000C7247 File Offset: 0x000C5447
		public void RemoveDefendingAgent(Agent agent)
		{
			this.DefendingAgents.Remove(agent);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000C7256 File Offset: 0x000C5456
		public bool IsAgentDefending(Agent agent)
		{
			return this.DefendingAgents.Contains(agent);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000C7264 File Offset: 0x000C5464
		public virtual void SimulateTick(float dt)
		{
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000C7268 File Offset: 0x000C5468
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (this.HasUser || this.HasAIMovingTo)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel2;
			}
			if (this.HasDefendingAgent)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
			}
			using (List<UsableMissionObjectComponent>.Enumerator enumerator = this._components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOnTickRequired())
					{
						return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
					}
				}
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000C72FC File Offset: 0x000C54FC
		protected internal override void OnTickParallel2(float dt)
		{
			for (int i = this.GetMovingAgentCount() - 1; i >= 0; i--)
			{
				if (!this.GetMovingAgentWithIndex(i).IsActive())
				{
					this._needsSingleThreadTickOnce = true;
				}
			}
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000C7334 File Offset: 0x000C5534
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnTick(dt);
			}
			if (this.HasUser && this.HasUserPositionsChanged(this.UserAgent))
			{
				if (this.LockUserFrames)
				{
					WorldFrame userFrameForAgent = this.GetUserFrameForAgent(this.UserAgent);
					this.UserAgent.SetTargetPositionAndDirection(userFrameForAgent.Origin.AsVec2, userFrameForAgent.Rotation.f);
				}
				else if (this.LockUserPositions)
				{
					this.UserAgent.SetTargetPosition(this.GetUserFrameForAgent(this.UserAgent).Origin.AsVec2);
				}
			}
			if (this._needsSingleThreadTickOnce)
			{
				this._needsSingleThreadTickOnce = false;
				for (int i = this.GetMovingAgentCount() - 1; i >= 0; i--)
				{
					Agent movingAgentWithIndex = this.GetMovingAgentWithIndex(i);
					if (!movingAgentWithIndex.IsActive())
					{
						Formation formation = movingAgentWithIndex.Formation;
						if (formation != null)
						{
							formation.Team.DetachmentManager.RemoveAgentAsMovingToDetachment(movingAgentWithIndex);
						}
						this.RemoveMovingAgent(movingAgentWithIndex);
						base.SetScriptComponentToTick(this.GetTickRequirement());
					}
				}
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000C7470 File Offset: 0x000C5670
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorTick(dt);
			}
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000C74C8 File Offset: 0x000C56C8
		protected internal override void OnEditorValidate()
		{
			base.OnEditorValidate();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorValidate();
			}
			string message = null;
			if (!this.VerifyChildEntities(ref message))
			{
				MBDebug.ShowWarning(message);
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000C7530 File Offset: 0x000C5730
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnRemoved();
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000C7588 File Offset: 0x000C5788
		public virtual GameEntity InteractionEntity
		{
			get
			{
				return base.GameEntity;
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000C7590 File Offset: 0x000C5790
		public virtual WorldFrame GetUserFrameForAgent(Agent agent)
		{
			return this.GameEntityWithWorldPosition.WorldFrame;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000C75A0 File Offset: 0x000C57A0
		public override string ToString()
		{
			string text = base.GetType() + " with Components:";
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				text = string.Concat(new object[]
				{
					text,
					"[",
					usableMissionObjectComponent,
					"]"
				});
			}
			return text;
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000C7624 File Offset: 0x000C5824
		public bool HasAIUser
		{
			get
			{
				return this.HasUser && this.UserAgent.IsAIControlled;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000C763B File Offset: 0x000C583B
		public bool HasUser
		{
			get
			{
				return this.UserAgent != null;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000C7646 File Offset: 0x000C5846
		public virtual bool HasAIMovingTo
		{
			get
			{
				return this.MovingAgent != null;
			}
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000C7651 File Offset: 0x000C5851
		public virtual bool IsAIMovingTo(Agent agent)
		{
			return this.MovingAgent == agent;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000C765C File Offset: 0x000C585C
		public virtual bool HasUserPositionsChanged(Agent agent)
		{
			return (this.LockUserFrames || this.LockUserPositions) && base.GameEntity.GetHasFrameChanged();
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000C767B File Offset: 0x000C587B
		public virtual bool DisableCombatActionsOnUse
		{
			get
			{
				return !this.IsInstantUse;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x000C7686 File Offset: 0x000C5886
		// (set) Token: 0x06003046 RID: 12358 RVA: 0x000C768E File Offset: 0x000C588E
		protected internal virtual bool LockUserFrames { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000C7697 File Offset: 0x000C5897
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x000C769F File Offset: 0x000C589F
		protected internal virtual bool LockUserPositions { get; set; }

		// Token: 0x06003049 RID: 12361 RVA: 0x000C76A8 File Offset: 0x000C58A8
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteBoolToPacket(this.IsDeactivated);
			GameNetworkMessage.WriteBoolToPacket(this.IsDisabledForPlayers);
			GameNetworkMessage.WriteBoolToPacket(this.UserAgent != null);
			if (this.UserAgent != null)
			{
				GameNetworkMessage.WriteAgentIndexToPacket(this.UserAgent.Index);
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000C76F7 File Offset: 0x000C58F7
		public virtual bool IsUsableByAgent(Agent userAgent)
		{
			return true;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x000C76FA File Offset: 0x000C58FA
		// (set) Token: 0x0600304C RID: 12364 RVA: 0x000C7708 File Offset: 0x000C5908
		public bool IsVisible
		{
			get
			{
				return base.GameEntity.IsVisibleIncludeParents();
			}
			set
			{
				base.GameEntity.SetVisibilityExcludeParents(value);
				foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
				{
					if (usableMissionObjectComponent is IVisible)
					{
						Debug.FailedAssert("Unexpected component in UsableMissionObject", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Usables\\UsableMissionObject.cs", "IsVisible", 746);
						((IVisible)usableMissionObjectComponent).IsVisible = value;
					}
				}
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000C7790 File Offset: 0x000C5990
		public override void OnEndMission()
		{
			this.UserAgent = null;
			for (int i = this.GetMovingAgentCount() - 1; i >= 0; i--)
			{
				this.RemoveMovingAgent(this.GetMovingAgentWithIndex(i));
			}
			if (this.HasDefendingAgent)
			{
				for (int j = this.GetDefendingAgentCount() - 1; j >= 0; j--)
				{
					this.DefendingAgents.RemoveAt(j);
				}
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000C77F8 File Offset: 0x000C59F8
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord);
			UsableMissionObject.UsableMissionObjectRecord usableMissionObjectRecord = (UsableMissionObject.UsableMissionObjectRecord)synchedMissionObjectReadableRecord.Item2;
			this.IsDeactivated = usableMissionObjectRecord.IsDeactivated;
			this.IsDisabledForPlayers = usableMissionObjectRecord.IsDisabledForPlayers;
			if (usableMissionObjectRecord.IsUserAgentExists)
			{
				Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(usableMissionObjectRecord.AgentIndex, false);
				if (agentFromIndex != null)
				{
					this.SetUserForClient(agentFromIndex);
				}
			}
		}

		// Token: 0x0600304F RID: 12367
		public abstract string GetDescriptionText(GameEntity gameEntity = null);

		// Token: 0x04001475 RID: 5237
		private Agent _userAgent;

		// Token: 0x0400147A RID: 5242
		private readonly List<UsableMissionObjectComponent> _components;

		// Token: 0x0400147B RID: 5243
		[EditableScriptComponentVariable(false)]
		public TextObject DescriptionMessage = TextObject.Empty;

		// Token: 0x0400147C RID: 5244
		[EditableScriptComponentVariable(false)]
		public TextObject ActionMessage = TextObject.Empty;

		// Token: 0x0400147D RID: 5245
		private bool _needsSingleThreadTickOnce;

		// Token: 0x0400147F RID: 5247
		private bool _isDeactivated;

		// Token: 0x04001480 RID: 5248
		private bool _isDisabledForPlayers;

		// Token: 0x02000623 RID: 1571
		[DefineSynchedMissionObjectType(typeof(UsableMissionObject))]
		public struct UsableMissionObjectRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x170009F2 RID: 2546
			// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000E9F4B File Offset: 0x000E814B
			// (set) Token: 0x06003C3A RID: 15418 RVA: 0x000E9F53 File Offset: 0x000E8153
			public bool IsDeactivated { get; private set; }

			// Token: 0x170009F3 RID: 2547
			// (get) Token: 0x06003C3B RID: 15419 RVA: 0x000E9F5C File Offset: 0x000E815C
			// (set) Token: 0x06003C3C RID: 15420 RVA: 0x000E9F64 File Offset: 0x000E8164
			public bool IsDisabledForPlayers { get; private set; }

			// Token: 0x170009F4 RID: 2548
			// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000E9F6D File Offset: 0x000E816D
			// (set) Token: 0x06003C3E RID: 15422 RVA: 0x000E9F75 File Offset: 0x000E8175
			public bool IsUserAgentExists { get; private set; }

			// Token: 0x170009F5 RID: 2549
			// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000E9F7E File Offset: 0x000E817E
			// (set) Token: 0x06003C40 RID: 15424 RVA: 0x000E9F86 File Offset: 0x000E8186
			public int AgentIndex { get; private set; }

			// Token: 0x06003C41 RID: 15425 RVA: 0x000E9F8F File Offset: 0x000E818F
			public UsableMissionObjectRecord(bool isDeactivated, bool isDisabledForPlayers, bool isUserAgentExists, int agentIndex)
			{
				this.IsDeactivated = isDeactivated;
				this.IsDisabledForPlayers = isDisabledForPlayers;
				this.IsUserAgentExists = isUserAgentExists;
				this.AgentIndex = agentIndex;
			}

			// Token: 0x06003C42 RID: 15426 RVA: 0x000E9FAE File Offset: 0x000E81AE
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.IsDeactivated = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.IsDisabledForPlayers = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.IsUserAgentExists = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				if (this.IsUserAgentExists)
				{
					this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref bufferReadValid);
				}
				return bufferReadValid;
			}
		}
	}
}
