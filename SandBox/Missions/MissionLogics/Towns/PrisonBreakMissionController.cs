using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.CampaignBehaviors;
using SandBox.Missions.AgentBehaviors;
using SandBox.Objects.AnimationPoints;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;

namespace SandBox.Missions.MissionLogics.Towns
{
	// Token: 0x0200006C RID: 108
	public class PrisonBreakMissionController : MissionLogic
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0001BCFA File Offset: 0x00019EFA
		public PrisonBreakMissionController(CharacterObject prisonerCharacter, CharacterObject companionCharacter)
		{
			this._prisonerCharacter = prisonerCharacter;
			this._companionCharacter = companionCharacter;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001BD10 File Offset: 0x00019F10
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = true;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001BD24 File Offset: 0x00019F24
		public override void OnBehaviorInitialize()
		{
			base.Mission.IsAgentInteractionAllowed_AdditionalCondition += this.IsAgentInteractionAllowed_AdditionalCondition;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001BD40 File Offset: 0x00019F40
		public override void AfterStart()
		{
			this._isPrisonerFollowing = true;
			MBTextManager.SetTextVariable("IS_PRISONER_FOLLOWING", this._isPrisonerFollowing ? 1 : 0);
			base.Mission.SetMissionMode(MissionMode.Stealth, true);
			base.Mission.IsInventoryAccessible = false;
			base.Mission.IsQuestScreenAccessible = true;
			LocationCharacter firstLocationCharacterOfCharacter = LocationComplex.Current.GetFirstLocationCharacterOfCharacter(this._prisonerCharacter);
			PlayerEncounter.LocationEncounter.AddAccompanyingCharacter(firstLocationCharacterOfCharacter, true);
			this._areaMarkers = (from area in base.Mission.ActiveMissionObjects.FindAllWithType<AreaMarker>()
			orderby area.AreaIndex
			select area).ToList<AreaMarker>();
			MissionAgentHandler missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			foreach (UsableMachine usableMachine in missionBehavior.TownPassageProps)
			{
				usableMachine.Deactivate();
			}
			missionBehavior.SpawnPlayer(base.Mission.DoesMissionRequireCivilianEquipment, true, false, false, false, "");
			missionBehavior.SpawnLocationCharacters(null);
			this.ArrangeGuardCount();
			this._prisonerAgent = base.Mission.Agents.First((Agent x) => x.Character == this._prisonerCharacter);
			this.PreparePrisonAgent();
			missionBehavior.SimulateAgent(this._prisonerAgent);
			for (int i = 0; i < this._guardAgents.Count; i++)
			{
				Agent agent = this._guardAgents[i];
				agent.GetComponent<CampaignAgentComponent>().AgentNavigator.SpecialTargetTag = this._areaMarkers[i % this._areaMarkers.Count].Tag;
				missionBehavior.SimulateAgent(agent);
			}
			this.SetTeams();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public override void OnMissionTick(float dt)
		{
			SandBoxHelpers.MissionHelper.FadeOutAgents(this._agentsToRemove, true, true);
			this._agentsToRemove.Clear();
			if (this._prisonerAgent != null)
			{
				this.CheckPrisonerSwitchToAlarmState();
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001BF20 File Offset: 0x0001A120
		public override void OnObjectUsed(Agent userAgent, UsableMissionObject usedObject)
		{
			if (this._guardAgents != null && usedObject is AnimationPoint && this._guardAgents.Contains(userAgent))
			{
				userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001BF48 File Offset: 0x0001A148
		public override void OnAgentInteraction(Agent userAgent, Agent agent)
		{
			if (userAgent == Agent.Main && agent == this._prisonerAgent)
			{
				this.SwitchPrisonerFollowingState(false);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001BF62 File Offset: 0x0001A162
		public override bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return userAgent == Agent.Main && otherAgent == this._prisonerAgent;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001BF78 File Offset: 0x0001A178
		private void PreparePrisonAgent()
		{
			this._prisonerAgent.Health = this._prisonerAgent.HealthLimit;
			this._prisonerAgent.Defensiveness = 2f;
			AgentNavigator agentNavigator = this._prisonerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.RemoveBehaviorGroup<AlarmedBehaviorGroup>();
			agentNavigator.SpecialTargetTag = "sp_prison_break_prisoner";
			ItemObject item = (from x in Items.All
			where x.IsCraftedWeapon && x.Type == ItemObject.ItemTypeEnum.OneHandedWeapon && x.WeaponComponent.GetItemType() == ItemObject.ItemTypeEnum.OneHandedWeapon && x.IsCivilian
			select x).MinBy((ItemObject x) => x.Value);
			MissionWeapon missionWeapon = new MissionWeapon(item, null, this._prisonerCharacter.HeroObject.ClanBanner);
			this._prisonerAgent.EquipWeaponWithNewEntity(EquipmentIndex.WeaponItemBeginSlot, ref missionWeapon);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001C040 File Offset: 0x0001A240
		public override void OnAgentAlarmedStateChanged(Agent agent, Agent.AIStateFlag flag)
		{
			if (agent == this._prisonerAgent && flag != Agent.AIStateFlag.Cautious)
			{
				this.SwitchPrisonerFollowingState(true);
			}
			this.UpdateDoorPermission();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001C05C File Offset: 0x0001A25C
		private void ArrangeGuardCount()
		{
			int num = 2 + Settlement.CurrentSettlement.Town.GetWallLevel();
			float security = Settlement.CurrentSettlement.Town.Security;
			if (security < 40f)
			{
				num--;
			}
			else if (security > 70f)
			{
				num++;
			}
			this._guardAgents = base.Mission.Agents.Where(delegate(Agent x)
			{
				CharacterObject characterObject;
				return (characterObject = (x.Character as CharacterObject)) != null && characterObject.IsSoldier;
			}).ToList<Agent>();
			this._agentsToRemove = new List<Agent>();
			int count = this._guardAgents.Count;
			if (count > num)
			{
				int num2 = count - num;
				for (int i = 0; i < count; i++)
				{
					if (num2 <= 0)
					{
						break;
					}
					Agent agent = this._guardAgents[i];
					if (!agent.Character.IsHero)
					{
						this._agentsToRemove.Add(agent);
						num2--;
					}
				}
			}
			else if (count < num)
			{
				List<LocationCharacter> list = (from x in LocationComplex.Current.GetListOfCharactersInLocation("prison")
				where !x.Character.IsHero && x.Character.IsSoldier
				select x).ToList<LocationCharacter>();
				if (list.IsEmpty<LocationCharacter>())
				{
					AgentData agentData = GuardsCampaignBehavior.PrepareGuardAgentDataFromGarrison(PlayerEncounter.LocationEncounter.Settlement.Culture.Guard, true, false);
					LocationCharacter item = new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddStandGuardBehaviors), "sp_guard", true, LocationCharacter.CharacterRelations.Neutral, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_guard"), false, false, null, false, false, true);
					list.Add(item);
				}
				int count2 = list.Count;
				Location locationWithId = LocationComplex.Current.GetLocationWithId("prison");
				int num3 = num - count;
				for (int j = 0; j < num3; j++)
				{
					LocationCharacter locationCharacter = list[j % count2];
					LocationCharacter locationCharacter2 = new LocationCharacter(new AgentData(new SimpleAgentOrigin(locationCharacter.Character, -1, locationCharacter.AgentData.AgentOrigin.Banner, default(UniqueTroopDescriptor))).Equipment(locationCharacter.AgentData.AgentOverridenEquipment).Monster(locationCharacter.AgentData.AgentMonster).NoHorses(true), locationCharacter.AddBehaviors, this._areaMarkers[j % this._areaMarkers.Count].Tag, true, LocationCharacter.CharacterRelations.Enemy, locationCharacter.ActionSetCode, locationCharacter.UseCivilianEquipment, false, null, false, false, true);
					LocationComplex.Current.ChangeLocation(locationCharacter2, null, locationWithId);
				}
			}
			this._guardAgents = (from x in base.Mission.Agents
			where x.Character is CharacterObject && ((CharacterObject)x.Character).IsSoldier && !this._agentsToRemove.Contains(x)
			select x).ToList<Agent>();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001C30C File Offset: 0x0001A50C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (this._guardAgents.Contains(affectedAgent))
			{
				this._guardAgents.Remove(affectedAgent);
				this.UpdateDoorPermission();
				if (this._prisonerAgent != null)
				{
					AgentFlag agentFlags = this._prisonerAgent.GetAgentFlags();
					this._prisonerAgent.SetAgentFlags(agentFlags & ~AgentFlag.CanGetAlarmed);
					return;
				}
			}
			else if (this._prisonerAgent == affectedAgent)
			{
				this._prisonerAgent = null;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001C374 File Offset: 0x0001A574
		private void CheckPrisonerSwitchToAlarmState()
		{
			foreach (Agent agent in this._guardAgents)
			{
				if (this._prisonerAgent.Position.DistanceSquared(agent.Position) < 3f)
				{
					AgentFlag agentFlags = this._prisonerAgent.GetAgentFlags();
					this._prisonerAgent.SetAgentFlags(agentFlags | AgentFlag.CanGetAlarmed);
				}
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001C400 File Offset: 0x0001A600
		private void SwitchPrisonerFollowingState(bool forceFollow = false)
		{
			this._isPrisonerFollowing = (forceFollow || !this._isPrisonerFollowing);
			MBTextManager.SetTextVariable("IS_PRISONER_FOLLOWING", this._isPrisonerFollowing ? 1 : 0);
			FollowAgentBehavior behavior = this._prisonerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().GetBehavior<FollowAgentBehavior>();
			if (this._isPrisonerFollowing)
			{
				this._prisonerAgent.SetCrouchMode(false);
				behavior.SetTargetAgent(Agent.Main);
				AgentFlag agentFlags = this._prisonerAgent.GetAgentFlags();
				this._prisonerAgent.SetAgentFlags(agentFlags & ~AgentFlag.CanGetAlarmed);
			}
			else
			{
				behavior.SetTargetAgent(null);
				this._prisonerAgent.SetCrouchMode(true);
			}
			this._prisonerAgent.AIStateFlags = Agent.AIStateFlag.None;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		public override InquiryData OnEndMissionRequest(out bool canLeave)
		{
			bool flag;
			if (Agent.Main != null && Agent.Main.IsActive() && !this._guardAgents.IsEmpty<Agent>())
			{
				flag = this._guardAgents.All((Agent x) => !x.IsActive());
			}
			else
			{
				flag = true;
			}
			canLeave = flag;
			if (!canLeave)
			{
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_can_not_retreat", null), 0, null, "");
			}
			return null;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001C52C File Offset: 0x0001A72C
		private void SetTeams()
		{
			base.Mission.PlayerTeam.SetIsEnemyOf(base.Mission.PlayerEnemyTeam, true);
			this._prisonerAgent.SetTeam(base.Mission.PlayerTeam, true);
			if (this._companionCharacter != null)
			{
				base.Mission.Agents.First((Agent x) => x.Character == this._companionCharacter).SetTeam(base.Mission.PlayerTeam, true);
			}
			foreach (Agent agent in this._guardAgents)
			{
				agent.SetTeam(base.Mission.PlayerEnemyTeam, true);
				AgentFlag agentFlags = agent.GetAgentFlags();
				agent.SetAgentFlags((agentFlags | AgentFlag.CanGetAlarmed) & ~AgentFlag.CanRetreat);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001C60C File Offset: 0x0001A80C
		protected override void OnEndMission()
		{
			if (PlayerEncounter.LocationEncounter.CharactersAccompanyingPlayer.Any((AccompanyingCharacter x) => x.LocationCharacter.Character == this._prisonerCharacter))
			{
				PlayerEncounter.LocationEncounter.RemoveAccompanyingCharacter(this._prisonerCharacter.HeroObject);
			}
			if (Agent.Main == null || !Agent.Main.IsActive())
			{
				GameMenu.SwitchToMenu("settlement_prison_break_fail_player_unconscious");
			}
			else if (this._prisonerAgent == null || !this._prisonerAgent.IsActive())
			{
				GameMenu.SwitchToMenu("settlement_prison_break_fail_prisoner_unconscious");
			}
			else
			{
				GameMenu.SwitchToMenu("settlement_prison_break_success");
			}
			Campaign.Current.GameMenuManager.NextLocation = null;
			Campaign.Current.GameMenuManager.PreviousLocation = null;
			base.Mission.IsAgentInteractionAllowed_AdditionalCondition -= this.IsAgentInteractionAllowed_AdditionalCondition;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001C6CC File Offset: 0x0001A8CC
		private void UpdateDoorPermission()
		{
			bool flag;
			if (!this._guardAgents.IsEmpty<Agent>())
			{
				flag = this._guardAgents.All((Agent x) => x.CurrentWatchState != Agent.WatchState.Alarmed);
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			foreach (UsableMachine usableMachine in base.Mission.GetMissionBehavior<MissionAgentHandler>().TownPassageProps)
			{
				if (flag2)
				{
					usableMachine.Activate();
				}
				else
				{
					usableMachine.Deactivate();
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001C770 File Offset: 0x0001A970
		private bool IsAgentInteractionAllowed_AdditionalCondition()
		{
			return true;
		}

		// Token: 0x040001D9 RID: 473
		private const int PrisonerSwitchToAlarmedDistance = 3;

		// Token: 0x040001DA RID: 474
		private readonly CharacterObject _prisonerCharacter;

		// Token: 0x040001DB RID: 475
		private readonly CharacterObject _companionCharacter;

		// Token: 0x040001DC RID: 476
		private List<Agent> _guardAgents;

		// Token: 0x040001DD RID: 477
		private List<Agent> _agentsToRemove;

		// Token: 0x040001DE RID: 478
		private Agent _prisonerAgent;

		// Token: 0x040001DF RID: 479
		private List<AreaMarker> _areaMarkers;

		// Token: 0x040001E0 RID: 480
		private bool _isPrisonerFollowing;
	}
}
