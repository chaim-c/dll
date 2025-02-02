using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Missions.AgentBehaviors;
using SandBox.Missions.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;

namespace SandBox.Conversation.MissionLogics
{
	// Token: 0x0200009E RID: 158
	public class MissionConversationLogic : MissionLogic
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0002C043 File Offset: 0x0002A243
		public static MissionConversationLogic Current
		{
			get
			{
				return Mission.Current.GetMissionBehavior<MissionConversationLogic>();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0002C04F File Offset: 0x0002A24F
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0002C057 File Offset: 0x0002A257
		public MissionState State { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0002C060 File Offset: 0x0002A260
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0002C068 File Offset: 0x0002A268
		public ConversationManager ConversationManager { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0002C071 File Offset: 0x0002A271
		public bool IsReadyForConversation
		{
			get
			{
				return this.ConversationAgent != null && this.ConversationManager != null && Agent.Main != null && Agent.Main.IsActive();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0002C098 File Offset: 0x0002A298
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0002C0A0 File Offset: 0x0002A2A0
		public Agent ConversationAgent { get; private set; }

		// Token: 0x0600060A RID: 1546 RVA: 0x0002C0A9 File Offset: 0x0002A2A9
		public MissionConversationLogic(CharacterObject teleportNearChar)
		{
			this._teleportNearCharacter = teleportNearChar;
			this._conversationPoints = new Dictionary<string, MBList<GameEntity>>();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002C0C3 File Offset: 0x0002A2C3
		public MissionConversationLogic()
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002C0CB File Offset: 0x0002A2CB
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			CampaignEvents.LocationCharactersSimulatedEvent.AddNonSerializedListener(this, new Action(this.OnLocationCharactersSimulated));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002C0EA File Offset: 0x0002A2EA
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.ClearListeners(this);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002C0FD File Offset: 0x0002A2FD
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (this._teleportNearCharacter != null && agent.Character == this._teleportNearCharacter)
			{
				this.ConversationAgent = agent;
				this._conversationAgentFound = true;
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002C123 File Offset: 0x0002A323
		public void SetSpawnArea(Alley alley)
		{
			this._customSpawnTag = alley.Tag;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002C131 File Offset: 0x0002A331
		public void SetSpawnArea(Workshop workshop)
		{
			this._customSpawnTag = workshop.Tag;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002C13F File Offset: 0x0002A33F
		public void SetSpawnArea(string customTag)
		{
			this._customSpawnTag = customTag;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002C148 File Offset: 0x0002A348
		private void OnLocationCharactersSimulated()
		{
			if (this._conversationAgentFound)
			{
				if (this.FillConversationPointList())
				{
					this.DetermineSpawnPoint();
					this._teleported = this.TryToTeleportBothToCertainPoints();
					return;
				}
				MissionAgentHandler missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>();
				if (missionBehavior == null)
				{
					return;
				}
				missionBehavior.TeleportTargetAgentNearReferenceAgent(this.ConversationAgent, Agent.Main, true, false);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002C19C File Offset: 0x0002A39C
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this.IsReadyForConversation)
			{
				return;
			}
			if (!this._teleported)
			{
				base.Mission.GetMissionBehavior<MissionAgentHandler>().TeleportTargetAgentNearReferenceAgent(this.ConversationAgent, Agent.Main, true, false);
				this._teleported = true;
			}
			if (this._teleportNearCharacter != null && !this._conversationStarted)
			{
				this.StartConversation(this.ConversationAgent, true, true);
				if (this.ConversationManager.NeedsToActivateForMapConversation && !GameNetwork.IsReplay)
				{
					this.ConversationManager.BeginConversation();
				}
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002C224 File Offset: 0x0002A424
		private bool TryToTeleportBothToCertainPoints()
		{
			bool missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>() != null;
			bool flag = Agent.Main.MountAgent != null;
			WorldFrame worldFrame = new WorldFrame(this._selectedConversationPoint.GetGlobalFrame().rotation, new WorldPosition(Agent.Main.Mission.Scene, this._selectedConversationPoint.GetGlobalFrame().origin));
			worldFrame.Origin.SetVec2(worldFrame.Origin.AsVec2 + worldFrame.Rotation.f.AsVec2 * (flag ? 1f : 0.5f));
			WorldFrame worldFrame2 = new WorldFrame(this._selectedConversationPoint.GetGlobalFrame().rotation, new WorldPosition(Agent.Main.Mission.Scene, this._selectedConversationPoint.GetGlobalFrame().origin));
			worldFrame2.Origin.SetVec2(worldFrame2.Origin.AsVec2 - worldFrame2.Rotation.f.AsVec2 * (flag ? 1f : 0.5f));
			Vec3 vec = new Vec3(worldFrame.Origin.AsVec2 - worldFrame2.Origin.AsVec2, 0f, -1f);
			Vec3 vec2 = new Vec3(worldFrame2.Origin.AsVec2 - worldFrame.Origin.AsVec2, 0f, -1f);
			worldFrame.Rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			this.ConversationAgent.LookDirection = vec2.NormalizedCopy();
			this.ConversationAgent.TeleportToPosition(worldFrame.Origin.GetGroundVec3());
			worldFrame2.Rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			if (Agent.Main.MountAgent != null)
			{
				Vec2 vec3 = vec2.AsVec2;
				vec3 = vec3.RightVec();
				Vec3 vec4 = vec3.ToVec3(0f);
				Agent.Main.MountAgent.LookDirection = vec4.NormalizedCopy();
			}
			base.Mission.MainAgent.LookDirection = vec.NormalizedCopy();
			base.Mission.MainAgent.TeleportToPosition(worldFrame2.Origin.GetGroundVec3());
			this.SetConversationAgentAnimations(this.ConversationAgent);
			WorldPosition origin = worldFrame2.Origin;
			origin.SetVec2(origin.AsVec2 - worldFrame2.Rotation.s.AsVec2 * 2f);
			if (missionBehavior)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					LocationCharacter locationCharacter = LocationComplex.Current.FindCharacter(agent);
					AccompanyingCharacter accompanyingCharacter = PlayerEncounter.LocationEncounter.GetAccompanyingCharacter(locationCharacter);
					if (accompanyingCharacter != null && accompanyingCharacter.IsFollowingPlayerAtMissionStart)
					{
						if (agent.MountAgent != null && Agent.Main.MountAgent != null)
						{
							agent.MountAgent.LookDirection = Agent.Main.MountAgent.LookDirection;
						}
						if (accompanyingCharacter.LocationCharacter.Character == this._teleportNearCharacter)
						{
							agent.LookDirection = vec2.NormalizedCopy();
							Agent agent2 = agent;
							Vec2 vec3 = worldFrame2.Rotation.f.AsVec2;
							agent2.SetMovementDirection(vec3);
							agent.TeleportToPosition(worldFrame.Origin.GetGroundVec3());
						}
						else
						{
							agent.LookDirection = vec.NormalizedCopy();
							Agent agent3 = agent;
							Vec2 vec3 = worldFrame.Rotation.f.AsVec2;
							agent3.SetMovementDirection(vec3);
							agent.TeleportToPosition(origin.GetGroundVec3());
						}
					}
				}
			}
			this._teleported = true;
			return true;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002C5E8 File Offset: 0x0002A7E8
		private void SetConversationAgentAnimations(Agent conversationAgent)
		{
			CampaignAgentComponent component = conversationAgent.GetComponent<CampaignAgentComponent>();
			AgentNavigator agentNavigator = component.AgentNavigator;
			AgentBehavior agentBehavior = (agentNavigator != null) ? agentNavigator.GetActiveBehavior() : null;
			if (agentBehavior != null)
			{
				agentBehavior.IsActive = false;
				component.AgentNavigator.ForceThink(0f);
				conversationAgent.SetActionChannel(0, ActionIndexCache.act_none, false, (ulong)((long)conversationAgent.GetCurrentActionPriority(0)), 0f, 1f, 0f, 0.4f, 0f, false, -0.2f, 0, true);
				conversationAgent.TickActionChannels(0.1f);
				conversationAgent.AgentVisuals.GetSkeleton().TickAnimationsAndForceUpdate(0.1f, conversationAgent.AgentVisuals.GetGlobalFrame(), true);
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002C68C File Offset: 0x0002A88C
		private void OnConversationEnd()
		{
			foreach (IAgent agent in this.ConversationManager.ConversationAgents)
			{
				Agent agent2 = (Agent)agent;
				agent2.AgentVisuals.SetVisible(true);
				agent2.AgentVisuals.SetClothComponentKeepStateOfAllMeshes(false);
				Agent mountAgent = agent2.MountAgent;
				if (mountAgent != null)
				{
					mountAgent.AgentVisuals.SetVisible(true);
				}
			}
			if (base.Mission.Mode == MissionMode.Conversation)
			{
				base.Mission.SetMissionMode(this._oldMissionMode, false);
			}
			if (Agent.Main != null)
			{
				Agent.Main.AgentVisuals.SetVisible(true);
				Agent.Main.AgentVisuals.SetClothComponentKeepStateOfAllMeshes(false);
				if (Agent.Main.MountAgent != null)
				{
					Agent.Main.MountAgent.AgentVisuals.SetVisible(true);
				}
			}
			base.Mission.MainAgentServer.Controller = Agent.ControllerType.Player;
			this.ConversationManager.ConversationEnd -= this.OnConversationEnd;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002C79C File Offset: 0x0002A99C
		public override void EarlyStart()
		{
			this.State = (Game.Current.GameStateManager.ActiveState as MissionState);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002C7B8 File Offset: 0x0002A9B8
		protected override void OnEndMission()
		{
			if (this.ConversationManager != null && this.ConversationManager.IsConversationInProgress)
			{
				this.ConversationManager.EndConversation();
			}
			this.State = null;
			CampaignEvents.LocationCharactersSimulatedEvent.ClearListeners(this);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		public override void OnAgentInteraction(Agent userAgent, Agent agent)
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				if (Game.Current.GameStateManager.ActiveState is MissionState)
				{
					if (this.IsThereAgentAction(userAgent, agent))
					{
						this.StartConversation(agent, false, false);
						return;
					}
				}
				else
				{
					Debug.FailedAssert("Agent interaction must occur in MissionState.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Conversation\\Logics\\MissionConversationLogic.cs", "OnAgentInteraction", 288);
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002C84C File Offset: 0x0002AA4C
		public void StartConversation(Agent agent, bool setActionsInstantly, bool isInitialization = false)
		{
			this._oldMissionMode = base.Mission.Mode;
			this.ConversationManager = Campaign.Current.ConversationManager;
			this.ConversationManager.SetupAndStartMissionConversation(agent, base.Mission.MainAgent, setActionsInstantly);
			this.ConversationManager.ConversationEnd += this.OnConversationEnd;
			this._conversationStarted = true;
			foreach (IAgent agent2 in this.ConversationManager.ConversationAgents)
			{
				Agent agent3 = (Agent)agent2;
				agent3.ForceAiBehaviorSelection();
				agent3.AgentVisuals.SetClothComponentKeepStateOfAllMeshes(true);
			}
			base.Mission.MainAgentServer.AgentVisuals.SetClothComponentKeepStateOfAllMeshes(true);
			base.Mission.SetMissionMode(MissionMode.Conversation, setActionsInstantly);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002C928 File Offset: 0x0002AB28
		public override bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return base.Mission.Mode != MissionMode.Battle && base.Mission.Mode != MissionMode.Duel && base.Mission.Mode != MissionMode.Conversation && !this._disableStartConversation && otherAgent.IsActive() && !otherAgent.IsEnemyOf(userAgent);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002C97B File Offset: 0x0002AB7B
		public override void OnRenderingStarted()
		{
			this.ConversationManager = Campaign.Current.ConversationManager;
			if (this.ConversationManager == null)
			{
				throw new ArgumentNullException("conversationManager");
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002C9A0 File Offset: 0x0002ABA0
		public void DisableStartConversation(bool isDisabled)
		{
			this._disableStartConversation = isDisabled;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		private bool FillConversationPointList()
		{
			List<GameEntity> list = base.Mission.Scene.FindEntitiesWithTag("sp_player_conversation").ToList<GameEntity>();
			bool result = false;
			if (!list.IsEmpty<GameEntity>())
			{
				List<AreaMarker> list2 = base.Mission.ActiveMissionObjects.FindAllWithType<AreaMarker>().ToList<AreaMarker>();
				foreach (GameEntity gameEntity in list)
				{
					bool flag = false;
					foreach (AreaMarker areaMarker in list2)
					{
						if (areaMarker.IsPositionInRange(gameEntity.GlobalPosition))
						{
							if (this._conversationPoints.ContainsKey(areaMarker.Tag))
							{
								this._conversationPoints[areaMarker.Tag].Add(gameEntity);
							}
							else
							{
								this._conversationPoints.Add(areaMarker.Tag, new MBList<GameEntity>
								{
									gameEntity
								});
							}
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						if (this._conversationPoints.ContainsKey("CenterConversationPoint"))
						{
							this._conversationPoints["CenterConversationPoint"].Add(gameEntity);
						}
						else
						{
							this._conversationPoints.Add("CenterConversationPoint", new MBList<GameEntity>
							{
								gameEntity
							});
						}
					}
				}
				result = true;
			}
			else
			{
				Debug.FailedAssert("Scene must have at least one 'sp_player_conversation' game entity. Scene Name: " + Mission.Current.Scene.GetName(), "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Conversation\\Logics\\MissionConversationLogic.cs", "FillConversationPointList", 382);
			}
			return result;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002CB54 File Offset: 0x0002AD54
		private void DetermineSpawnPoint()
		{
			MBList<GameEntity> e;
			if (this._customSpawnTag != null && this._conversationPoints.TryGetValue(this._customSpawnTag, out e))
			{
				this._selectedConversationPoint = e.GetRandomElement<GameEntity>();
				return;
			}
			string agentsTag = this.ConversationAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.SpecialTargetTag;
			if (agentsTag != null)
			{
				MBList<GameEntity> value = this._conversationPoints.FirstOrDefault((KeyValuePair<string, MBList<GameEntity>> x) => agentsTag.Contains(x.Key)).Value;
				this._selectedConversationPoint = ((value != null) ? value.GetRandomElement<GameEntity>() : null);
			}
			if (this._selectedConversationPoint == null)
			{
				if (this._conversationPoints.ContainsKey("CenterConversationPoint"))
				{
					this._selectedConversationPoint = this._conversationPoints["CenterConversationPoint"].GetRandomElement<GameEntity>();
					return;
				}
				this._selectedConversationPoint = this._conversationPoints.GetRandomElementInefficiently<KeyValuePair<string, MBList<GameEntity>>>().Value.GetRandomElement<GameEntity>();
			}
		}

		// Token: 0x040002B3 RID: 691
		private const string CenterConversationPointMappingTag = "CenterConversationPoint";

		// Token: 0x040002B6 RID: 694
		private MissionMode _oldMissionMode;

		// Token: 0x040002B7 RID: 695
		private readonly CharacterObject _teleportNearCharacter;

		// Token: 0x040002B8 RID: 696
		private GameEntity _selectedConversationPoint;

		// Token: 0x040002B9 RID: 697
		private bool _conversationStarted;

		// Token: 0x040002BA RID: 698
		private bool _teleported;

		// Token: 0x040002BB RID: 699
		private bool _conversationAgentFound;

		// Token: 0x040002BC RID: 700
		private bool _disableStartConversation;

		// Token: 0x040002BD RID: 701
		private readonly Dictionary<string, MBList<GameEntity>> _conversationPoints;

		// Token: 0x040002BE RID: 702
		private string _customSpawnTag;
	}
}
