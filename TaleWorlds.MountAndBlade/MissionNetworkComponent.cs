using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029A RID: 666
	public sealed class MissionNetworkComponent : MissionNetwork
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060022DF RID: 8927 RVA: 0x0007F31C File Offset: 0x0007D51C
		// (remove) Token: 0x060022E0 RID: 8928 RVA: 0x0007F354 File Offset: 0x0007D554
		public event Action OnMyClientSynchronized;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060022E1 RID: 8929 RVA: 0x0007F38C File Offset: 0x0007D58C
		// (remove) Token: 0x060022E2 RID: 8930 RVA: 0x0007F3C4 File Offset: 0x0007D5C4
		public event Action<NetworkCommunicator> OnClientSynchronizedEvent;

		// Token: 0x060022E3 RID: 8931 RVA: 0x0007F3FC File Offset: 0x0007D5FC
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<CreateFreeMountAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateFreeMountAgentEvent));
				registerer.RegisterBaseHandler<CreateAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateAgent));
				registerer.RegisterBaseHandler<SynchronizeAgentSpawnEquipment>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSynchronizeAgentEquipment));
				registerer.RegisterBaseHandler<CreateAgentVisuals>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateAgentVisuals));
				registerer.RegisterBaseHandler<RemoveAgentVisualsForPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveAgentVisualsForPeer));
				registerer.RegisterBaseHandler<RemoveAgentVisualsFromIndexForPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveAgentVisualsFromIndexForPeer));
				registerer.RegisterBaseHandler<ReplaceBotWithPlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventReplaceBotWithPlayer));
				registerer.RegisterBaseHandler<SetWieldedItemIndex>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWieldedItemIndex));
				registerer.RegisterBaseHandler<SetWeaponNetworkData>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWeaponNetworkData));
				registerer.RegisterBaseHandler<SetWeaponAmmoData>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWeaponAmmoData));
				registerer.RegisterBaseHandler<SetWeaponReloadPhase>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWeaponReloadPhase));
				registerer.RegisterBaseHandler<WeaponUsageIndexChangeMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventWeaponUsageIndexChangeMessage));
				registerer.RegisterBaseHandler<StartSwitchingWeaponUsageIndex>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventStartSwitchingWeaponUsageIndex));
				registerer.RegisterBaseHandler<InitializeFormation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventInitializeFormation));
				registerer.RegisterBaseHandler<SetSpawnedFormationCount>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSpawnedFormationCount));
				registerer.RegisterBaseHandler<AddTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAddTeam));
				registerer.RegisterBaseHandler<TeamSetIsEnemyOf>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventTeamSetIsEnemyOf));
				registerer.RegisterBaseHandler<AssignFormationToPlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAssignFormationToPlayer));
				registerer.RegisterBaseHandler<ExistingObjectsBegin>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventExistingObjectsBegin));
				registerer.RegisterBaseHandler<ExistingObjectsEnd>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventExistingObjectsEnd));
				registerer.RegisterBaseHandler<ClearMission>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventClearMission));
				registerer.RegisterBaseHandler<CreateMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateMissionObject));
				registerer.RegisterBaseHandler<RemoveMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveMissionObject));
				registerer.RegisterBaseHandler<StopPhysicsAndSetFrameOfMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventStopPhysicsAndSetFrameOfMissionObject));
				registerer.RegisterBaseHandler<BurstMissionObjectParticles>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventBurstMissionObjectParticles));
				registerer.RegisterBaseHandler<SetMissionObjectVisibility>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectVisibility));
				registerer.RegisterBaseHandler<SetMissionObjectDisabled>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectDisabled));
				registerer.RegisterBaseHandler<SetMissionObjectColors>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectColors));
				registerer.RegisterBaseHandler<SetMissionObjectFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectFrame));
				registerer.RegisterBaseHandler<SetMissionObjectGlobalFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectGlobalFrame));
				registerer.RegisterBaseHandler<SetMissionObjectFrameOverTime>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectFrameOverTime));
				registerer.RegisterBaseHandler<SetMissionObjectGlobalFrameOverTime>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectGlobalFrameOverTime));
				registerer.RegisterBaseHandler<SetMissionObjectAnimationAtChannel>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectAnimationAtChannel));
				registerer.RegisterBaseHandler<SetMissionObjectAnimationChannelParameter>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectAnimationChannelParameter));
				registerer.RegisterBaseHandler<SetMissionObjectAnimationPaused>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectAnimationPaused));
				registerer.RegisterBaseHandler<SetMissionObjectVertexAnimation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectVertexAnimation));
				registerer.RegisterBaseHandler<SetMissionObjectVertexAnimationProgress>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectVertexAnimationProgress));
				registerer.RegisterBaseHandler<SetMissionObjectImpulse>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectImpulse));
				registerer.RegisterBaseHandler<AddMissionObjectBodyFlags>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAddMissionObjectBodyFlags));
				registerer.RegisterBaseHandler<RemoveMissionObjectBodyFlags>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveMissionObjectBodyFlags));
				registerer.RegisterBaseHandler<SetMachineTargetRotation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMachineTargetRotation));
				registerer.RegisterBaseHandler<SetUsableMissionObjectIsDeactivated>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetUsableGameObjectIsDeactivated));
				registerer.RegisterBaseHandler<SetUsableMissionObjectIsDisabledForPlayers>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetUsableGameObjectIsDisabledForPlayers));
				registerer.RegisterBaseHandler<SetRangedSiegeWeaponState>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetRangedSiegeWeaponState));
				registerer.RegisterBaseHandler<SetRangedSiegeWeaponAmmo>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetRangedSiegeWeaponAmmo));
				registerer.RegisterBaseHandler<RangedSiegeWeaponChangeProjectile>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRangedSiegeWeaponChangeProjectile));
				registerer.RegisterBaseHandler<SetStonePileAmmo>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetStonePileAmmo));
				registerer.RegisterBaseHandler<SetSiegeMachineMovementDistance>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeMachineMovementDistance));
				registerer.RegisterBaseHandler<SetSiegeLadderState>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeLadderState));
				registerer.RegisterBaseHandler<SetAgentTargetPositionAndDirection>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentTargetPositionAndDirection));
				registerer.RegisterBaseHandler<SetAgentTargetPosition>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentTargetPosition));
				registerer.RegisterBaseHandler<ClearAgentTargetFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventClearAgentTargetFrame));
				registerer.RegisterBaseHandler<AgentTeleportToFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentTeleportToFrame));
				registerer.RegisterBaseHandler<SetSiegeTowerGateState>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeTowerGateState));
				registerer.RegisterBaseHandler<SetSiegeTowerHasArrivedAtTarget>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeTowerHasArrivedAtTarget));
				registerer.RegisterBaseHandler<SetBatteringRamHasArrivedAtTarget>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetBatteringRamHasArrivedAtTarget));
				registerer.RegisterBaseHandler<SetPeerTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetPeerTeam));
				registerer.RegisterBaseHandler<SynchronizeMissionTimeTracker>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSyncMissionTimer));
				registerer.RegisterBaseHandler<SetAgentPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentPeer));
				registerer.RegisterBaseHandler<SetAgentIsPlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentIsPlayer));
				registerer.RegisterBaseHandler<SetAgentHealth>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentHealth));
				registerer.RegisterBaseHandler<AgentSetTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentSetTeam));
				registerer.RegisterBaseHandler<SetAgentActionSet>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentActionSet));
				registerer.RegisterBaseHandler<MakeAgentDead>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventMakeAgentDead));
				registerer.RegisterBaseHandler<AgentSetFormation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentSetFormation));
				registerer.RegisterBaseHandler<AddPrefabComponentToAgentBone>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAddPrefabComponentToAgentBone));
				registerer.RegisterBaseHandler<SetAgentPrefabComponentVisibility>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentPrefabComponentVisibility));
				registerer.RegisterBaseHandler<UseObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUseObject));
				registerer.RegisterBaseHandler<StopUsingObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventStopUsingObject));
				registerer.RegisterBaseHandler<SyncObjectHitpoints>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHitSynchronizeObjectHitpoints));
				registerer.RegisterBaseHandler<SyncObjectDestructionLevel>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHitSynchronizeObjectDestructionLevel));
				registerer.RegisterBaseHandler<BurstAllHeavyHitParticles>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHitBurstAllHeavyHitParticles));
				registerer.RegisterBaseHandler<SynchronizeMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSynchronizeMissionObject));
				registerer.RegisterBaseHandler<SpawnWeaponWithNewEntity>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnWeaponWithNewEntity));
				registerer.RegisterBaseHandler<AttachWeaponToSpawnedWeapon>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAttachWeaponToSpawnedWeapon));
				registerer.RegisterBaseHandler<AttachWeaponToAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAttachWeaponToAgent));
				registerer.RegisterBaseHandler<SpawnWeaponAsDropFromAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnWeaponAsDropFromAgent));
				registerer.RegisterBaseHandler<SpawnAttachedWeaponOnSpawnedWeapon>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnAttachedWeaponOnSpawnedWeapon));
				registerer.RegisterBaseHandler<SpawnAttachedWeaponOnCorpse>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnAttachedWeaponOnCorpse));
				registerer.RegisterBaseHandler<HandleMissileCollisionReaction>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHandleMissileCollisionReaction));
				registerer.RegisterBaseHandler<RemoveEquippedWeapon>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveEquippedWeapon));
				registerer.RegisterBaseHandler<BarkAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventBarkAgent));
				registerer.RegisterBaseHandler<EquipWeaponWithNewEntity>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventEquipWeaponWithNewEntity));
				registerer.RegisterBaseHandler<AttachWeaponToWeaponInAgentEquipmentSlot>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAttachWeaponToWeaponInAgentEquipmentSlot));
				registerer.RegisterBaseHandler<EquipWeaponFromSpawnedItemEntity>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventEquipWeaponFromSpawnedItemEntity));
				registerer.RegisterBaseHandler<CreateMissile>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateMissile));
				registerer.RegisterBaseHandler<CombatLogNetworkMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentHit));
				registerer.RegisterBaseHandler<ConsumeWeaponAmount>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventConsumeWeaponAmount));
				return;
			}
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<SetFollowedAgent>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSetFollowedAgent));
				registerer.RegisterBaseHandler<SetMachineRotation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSetMachineRotation));
				registerer.RegisterBaseHandler<RequestUseObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestUseObject));
				registerer.RegisterBaseHandler<RequestStopUsingObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestStopUsingObject));
				registerer.RegisterBaseHandler<ApplyOrder>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrder));
				registerer.RegisterBaseHandler<ApplySiegeWeaponOrder>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplySiegeWeaponOrder));
				registerer.RegisterBaseHandler<ApplyOrderWithPosition>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithPosition));
				registerer.RegisterBaseHandler<ApplyOrderWithFormation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithFormation));
				registerer.RegisterBaseHandler<ApplyOrderWithFormationAndPercentage>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithFormationAndPercentage));
				registerer.RegisterBaseHandler<ApplyOrderWithFormationAndNumber>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithFormationAndNumber));
				registerer.RegisterBaseHandler<ApplyOrderWithTwoPositions>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithTwoPositions));
				registerer.RegisterBaseHandler<ApplyOrderWithMissionObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithGameEntity));
				registerer.RegisterBaseHandler<ApplyOrderWithAgent>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithAgent));
				registerer.RegisterBaseHandler<SelectAllFormations>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectAllFormations));
				registerer.RegisterBaseHandler<SelectAllSiegeWeapons>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectAllSiegeWeapons));
				registerer.RegisterBaseHandler<ClearSelectedFormations>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventClearSelectedFormations));
				registerer.RegisterBaseHandler<SelectFormation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectFormation));
				registerer.RegisterBaseHandler<SelectSiegeWeapon>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectSiegeWeapon));
				registerer.RegisterBaseHandler<UnselectFormation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventUnselectFormation));
				registerer.RegisterBaseHandler<UnselectSiegeWeapon>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventUnselectSiegeWeapon));
				registerer.RegisterBaseHandler<DropWeapon>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventDropWeapon));
				registerer.RegisterBaseHandler<TauntSelected>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventCheerSelected));
				registerer.RegisterBaseHandler<BarkSelected>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventBarkSelected));
				registerer.RegisterBaseHandler<AgentVisualsBreakInvulnerability>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventBreakAgentVisualsInvulnerability));
				registerer.RegisterBaseHandler<RequestToSpawnAsBot>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestToSpawnAsBot));
			}
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0007FC10 File Offset: 0x0007DE10
		private Team GetTeamOfPeer(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component.ControlledAgent == null)
			{
				MBDebug.Print("peer.ControlledAgent == null", 0, Debug.DebugColor.White, 17592186044416UL);
				return null;
			}
			Team team = component.ControlledAgent.Team;
			if (team == null)
			{
				MBDebug.Print("peersTeam == null", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return team;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0007FC68 File Offset: 0x0007DE68
		private OrderController GetOrderControllerOfPeer(NetworkCommunicator networkPeer)
		{
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			if (teamOfPeer != null)
			{
				return teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent);
			}
			MBDebug.Print("peersTeam == null", 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0007FCA4 File Offset: 0x0007DEA4
		private void HandleServerEventSyncMissionTimer(GameNetworkMessage baseMessage)
		{
			SynchronizeMissionTimeTracker synchronizeMissionTimeTracker = (SynchronizeMissionTimeTracker)baseMessage;
			base.Mission.MissionTimeTracker.UpdateSync(synchronizeMissionTimeTracker.CurrentTime);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0007FCD0 File Offset: 0x0007DED0
		private void HandleServerEventSetPeerTeam(GameNetworkMessage baseMessage)
		{
			SetPeerTeam setPeerTeam = (SetPeerTeam)baseMessage;
			MissionPeer component = setPeerTeam.Peer.GetComponent<MissionPeer>();
			component.Team = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(setPeerTeam.TeamIndex);
			if (setPeerTeam.Peer.IsMine)
			{
				base.Mission.PlayerTeam = component.Team;
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007FD20 File Offset: 0x0007DF20
		private void HandleServerEventCreateFreeMountAgentEvent(GameNetworkMessage baseMessage)
		{
			CreateFreeMountAgent createFreeMountAgent = (CreateFreeMountAgent)baseMessage;
			Mission mission = base.Mission;
			EquipmentElement horseItem = createFreeMountAgent.HorseItem;
			EquipmentElement horseHarnessItem = createFreeMountAgent.HorseHarnessItem;
			Vec3 position = createFreeMountAgent.Position;
			Vec2 vec = createFreeMountAgent.Direction;
			vec = vec.Normalized();
			mission.SpawnMonster(horseItem, horseHarnessItem, position, vec, createFreeMountAgent.AgentIndex);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0007FD6C File Offset: 0x0007DF6C
		private void HandleServerEventCreateAgent(GameNetworkMessage baseMessage)
		{
			CreateAgent createAgent = (CreateAgent)baseMessage;
			BasicCharacterObject character = createAgent.Character;
			NetworkCommunicator peer = createAgent.Peer;
			MissionPeer missionPeer = (peer != null) ? peer.GetComponent<MissionPeer>() : null;
			Team teamFromTeamIndex = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(createAgent.TeamIndex);
			AgentBuildData agentBuildData = new AgentBuildData(character).MissionPeer(createAgent.IsPlayerAgent ? missionPeer : null).Monster(createAgent.Monster).TroopOrigin(new BasicBattleAgentOrigin(character)).Equipment(createAgent.SpawnEquipment).EquipmentSeed(createAgent.BodyPropertiesSeed);
			Vec3 position = createAgent.Position;
			AgentBuildData agentBuildData2 = agentBuildData.InitialPosition(position);
			Vec2 vec = createAgent.Direction;
			vec = vec.Normalized();
			AgentBuildData agentBuildData3 = agentBuildData2.InitialDirection(vec).MissionEquipment(createAgent.MissionEquipment).Team(teamFromTeamIndex).Index(createAgent.AgentIndex).MountIndex(createAgent.MountAgentIndex).IsFemale(createAgent.IsFemale).ClothingColor1(createAgent.ClothingColor1).ClothingColor2(createAgent.ClothingColor2);
			Formation formation = null;
			if (teamFromTeamIndex != null && createAgent.FormationIndex >= 0 && !GameNetwork.IsReplay)
			{
				formation = teamFromTeamIndex.GetFormation((FormationClass)createAgent.FormationIndex);
				agentBuildData3.Formation(formation);
			}
			if (createAgent.IsPlayerAgent)
			{
				agentBuildData3.BodyProperties(missionPeer.Peer.BodyProperties);
				agentBuildData3.Age((int)agentBuildData3.AgentBodyProperties.Age);
			}
			else
			{
				agentBuildData3.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData3.AgentRace, agentBuildData3.AgentIsFemale, character.GetBodyPropertiesMin(false), character.GetBodyPropertiesMax(), (int)agentBuildData3.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData3.AgentEquipmentSeed, character.HairTags, character.BeardTags, character.TattooTags));
			}
			Banner banner = null;
			if (formation != null)
			{
				if (!string.IsNullOrEmpty(formation.BannerCode))
				{
					if (formation.Banner == null)
					{
						banner = new Banner(formation.BannerCode, teamFromTeamIndex.Color, teamFromTeamIndex.Color2);
						formation.Banner = banner;
					}
					else
					{
						banner = formation.Banner;
					}
				}
			}
			else if (missionPeer != null)
			{
				banner = new Banner(missionPeer.Peer.BannerCode, teamFromTeamIndex.Color, teamFromTeamIndex.Color2);
			}
			agentBuildData3.Banner(banner);
			Agent mountAgent = base.Mission.SpawnAgent(agentBuildData3, false).MountAgent;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0007FF98 File Offset: 0x0007E198
		private void HandleServerEventSynchronizeAgentEquipment(GameNetworkMessage baseMessage)
		{
			SynchronizeAgentSpawnEquipment synchronizeAgentSpawnEquipment = (SynchronizeAgentSpawnEquipment)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(synchronizeAgentSpawnEquipment.AgentIndex, false).UpdateSpawnEquipmentAndRefreshVisuals(synchronizeAgentSpawnEquipment.SpawnEquipment);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0007FFC4 File Offset: 0x0007E1C4
		private void HandleServerEventCreateAgentVisuals(GameNetworkMessage baseMessage)
		{
			CreateAgentVisuals createAgentVisuals = (CreateAgentVisuals)baseMessage;
			MissionPeer component = createAgentVisuals.Peer.GetComponent<MissionPeer>();
			BattleSideEnum side = component.Team.Side;
			BasicCharacterObject character = createAgentVisuals.Character;
			BasicCultureObject culture = character.Culture;
			AgentBuildData agentBuildData = new AgentBuildData(character).VisualsIndex(createAgentVisuals.VisualsIndex).Equipment(createAgentVisuals.Equipment).EquipmentSeed(createAgentVisuals.BodyPropertiesSeed).IsFemale(createAgentVisuals.IsFemale).ClothingColor1((side == BattleSideEnum.Attacker) ? culture.Color : culture.ClothAlternativeColor).ClothingColor2((side == BattleSideEnum.Attacker) ? culture.Color2 : culture.ClothAlternativeColor2);
			if (createAgentVisuals.VisualsIndex == 0)
			{
				agentBuildData.BodyProperties(component.Peer.BodyProperties);
			}
			else
			{
				agentBuildData.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData.AgentRace, agentBuildData.AgentIsFemale, character.GetBodyPropertiesMin(false), character.GetBodyPropertiesMax(), (int)agentBuildData.AgentOverridenSpawnEquipment.HairCoverType, createAgentVisuals.BodyPropertiesSeed, character.HairTags, character.BeardTags, character.TattooTags));
			}
			base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().SpawnAgentVisualsForPeer(component, agentBuildData, createAgentVisuals.SelectedEquipmentSetIndex, false, createAgentVisuals.TroopCountInFormation);
			if (agentBuildData.AgentVisualsIndex == 0)
			{
				component.HasSpawnedAgentVisuals = true;
				component.EquipmentUpdatingExpired = false;
			}
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00080108 File Offset: 0x0007E308
		private void HandleServerEventRemoveAgentVisualsForPeer(GameNetworkMessage baseMessage)
		{
			MissionPeer component = ((RemoveAgentVisualsForPeer)baseMessage).Peer.GetComponent<MissionPeer>();
			base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, false);
			component.HasSpawnedAgentVisuals = false;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0008013F File Offset: 0x0007E33F
		private void HandleServerEventRemoveAgentVisualsFromIndexForPeer(GameNetworkMessage baseMessage)
		{
			((RemoveAgentVisualsFromIndexForPeer)baseMessage).Peer.GetComponent<MissionPeer>();
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x00080154 File Offset: 0x0007E354
		private void HandleServerEventReplaceBotWithPlayer(GameNetworkMessage baseMessage)
		{
			ReplaceBotWithPlayer replaceBotWithPlayer = (ReplaceBotWithPlayer)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(replaceBotWithPlayer.BotAgentIndex, false);
			if (agentFromIndex.Formation != null)
			{
				agentFromIndex.Formation.PlayerOwner = agentFromIndex;
			}
			MissionPeer component = replaceBotWithPlayer.Peer.GetComponent<MissionPeer>();
			agentFromIndex.MissionPeer = replaceBotWithPlayer.Peer.GetComponent<MissionPeer>();
			agentFromIndex.Formation = component.ControlledFormation;
			agentFromIndex.Health = (float)replaceBotWithPlayer.Health;
			if (agentFromIndex.MountAgent != null)
			{
				agentFromIndex.MountAgent.Health = (float)replaceBotWithPlayer.MountHealth;
			}
			if (agentFromIndex.Formation != null)
			{
				agentFromIndex.Team.AssignPlayerAsSergeantOfFormation(component, component.ControlledFormation.FormationIndex);
			}
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000801F8 File Offset: 0x0007E3F8
		private void HandleServerEventSetWieldedItemIndex(GameNetworkMessage baseMessage)
		{
			SetWieldedItemIndex setWieldedItemIndex = (SetWieldedItemIndex)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setWieldedItemIndex.AgentIndex, false);
			if (agentFromIndex != null)
			{
				agentFromIndex.SetWieldedItemIndexAsClient(setWieldedItemIndex.IsLeftHand ? Agent.HandIndex.OffHand : Agent.HandIndex.MainHand, setWieldedItemIndex.WieldedItemIndex, setWieldedItemIndex.IsWieldedInstantly, setWieldedItemIndex.IsWieldedOnSpawn, setWieldedItemIndex.MainHandCurrentUsageIndex);
				agentFromIndex.UpdateAgentStats();
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0008024C File Offset: 0x0007E44C
		private void HandleServerEventSetWeaponNetworkData(GameNetworkMessage baseMessage)
		{
			SetWeaponNetworkData setWeaponNetworkData = (SetWeaponNetworkData)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setWeaponNetworkData.AgentIndex, false);
			ItemObject item = agentFromIndex.Equipment[setWeaponNetworkData.WeaponEquipmentIndex].Item;
			WeaponComponentData weaponComponentData = (item != null) ? item.PrimaryWeapon : null;
			if (weaponComponentData != null)
			{
				if (weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.HasHitPoints))
				{
					agentFromIndex.ChangeWeaponHitPoints(setWeaponNetworkData.WeaponEquipmentIndex, setWeaponNetworkData.DataValue);
					return;
				}
				if (weaponComponentData.IsConsumable)
				{
					agentFromIndex.SetWeaponAmountInSlot(setWeaponNetworkData.WeaponEquipmentIndex, setWeaponNetworkData.DataValue, true);
				}
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000802D8 File Offset: 0x0007E4D8
		private void HandleServerEventSetWeaponAmmoData(GameNetworkMessage baseMessage)
		{
			SetWeaponAmmoData setWeaponAmmoData = (SetWeaponAmmoData)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setWeaponAmmoData.AgentIndex, false);
			if (agentFromIndex.Equipment[setWeaponAmmoData.WeaponEquipmentIndex].CurrentUsageItem.IsRangedWeapon)
			{
				agentFromIndex.SetWeaponAmmoAsClient(setWeaponAmmoData.WeaponEquipmentIndex, setWeaponAmmoData.AmmoEquipmentIndex, setWeaponAmmoData.Ammo);
				return;
			}
			Debug.FailedAssert("Invalid item type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionNetworkComponent.cs", "HandleServerEventSetWeaponAmmoData", 463);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0008034C File Offset: 0x0007E54C
		private void HandleServerEventSetWeaponReloadPhase(GameNetworkMessage baseMessage)
		{
			SetWeaponReloadPhase setWeaponReloadPhase = (SetWeaponReloadPhase)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setWeaponReloadPhase.AgentIndex, false).SetWeaponReloadPhaseAsClient(setWeaponReloadPhase.EquipmentIndex, setWeaponReloadPhase.ReloadPhase);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00080380 File Offset: 0x0007E580
		private void HandleServerEventWeaponUsageIndexChangeMessage(GameNetworkMessage baseMessage)
		{
			WeaponUsageIndexChangeMessage weaponUsageIndexChangeMessage = (WeaponUsageIndexChangeMessage)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(weaponUsageIndexChangeMessage.AgentIndex, false).SetUsageIndexOfWeaponInSlotAsClient(weaponUsageIndexChangeMessage.SlotIndex, weaponUsageIndexChangeMessage.UsageIndex);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000803B4 File Offset: 0x0007E5B4
		private void HandleServerEventStartSwitchingWeaponUsageIndex(GameNetworkMessage baseMessage)
		{
			StartSwitchingWeaponUsageIndex startSwitchingWeaponUsageIndex = (StartSwitchingWeaponUsageIndex)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(startSwitchingWeaponUsageIndex.AgentIndex, false).StartSwitchingWeaponUsageIndexAsClient(startSwitchingWeaponUsageIndex.EquipmentIndex, startSwitchingWeaponUsageIndex.UsageIndex, startSwitchingWeaponUsageIndex.CurrentMovementFlagUsageDirection);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000803EC File Offset: 0x0007E5EC
		private void HandleServerEventInitializeFormation(GameNetworkMessage baseMessage)
		{
			InitializeFormation initializeFormation = (InitializeFormation)baseMessage;
			Mission.MissionNetworkHelper.GetTeamFromTeamIndex(initializeFormation.TeamIndex).GetFormation((FormationClass)initializeFormation.FormationIndex).BannerCode = initializeFormation.BannerCode;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00080424 File Offset: 0x0007E624
		private void HandleServerEventSetSpawnedFormationCount(GameNetworkMessage baseMessage)
		{
			SetSpawnedFormationCount setSpawnedFormationCount = (SetSpawnedFormationCount)baseMessage;
			base.Mission.NumOfFormationsSpawnedTeamOne = setSpawnedFormationCount.NumOfFormationsTeamOne;
			base.Mission.NumOfFormationsSpawnedTeamTwo = setSpawnedFormationCount.NumOfFormationsTeamTwo;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0008045C File Offset: 0x0007E65C
		private void HandleServerEventAddTeam(GameNetworkMessage baseMessage)
		{
			AddTeam addTeam = (AddTeam)baseMessage;
			Banner banner = string.IsNullOrEmpty(addTeam.BannerCode) ? null : new Banner(addTeam.BannerCode, addTeam.Color, addTeam.Color2);
			base.Mission.Teams.Add(addTeam.Side, addTeam.Color, addTeam.Color2, banner, addTeam.IsPlayerGeneral, addTeam.IsPlayerSergeant, true);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000804CC File Offset: 0x0007E6CC
		private void HandleServerEventTeamSetIsEnemyOf(GameNetworkMessage baseMessage)
		{
			TeamSetIsEnemyOf teamSetIsEnemyOf = (TeamSetIsEnemyOf)baseMessage;
			Team teamFromTeamIndex = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(teamSetIsEnemyOf.Team1Index);
			Team teamFromTeamIndex2 = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(teamSetIsEnemyOf.Team2Index);
			teamFromTeamIndex.SetIsEnemyOf(teamFromTeamIndex2, teamSetIsEnemyOf.IsEnemyOf);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00080504 File Offset: 0x0007E704
		private void HandleServerEventAssignFormationToPlayer(GameNetworkMessage baseMessage)
		{
			AssignFormationToPlayer assignFormationToPlayer = (AssignFormationToPlayer)baseMessage;
			MissionPeer component = assignFormationToPlayer.Peer.GetComponent<MissionPeer>();
			component.Team.AssignPlayerAsSergeantOfFormation(component, assignFormationToPlayer.FormationClass);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00080536 File Offset: 0x0007E736
		private void HandleServerEventExistingObjectsBegin(GameNetworkMessage baseMessage)
		{
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00080538 File Offset: 0x0007E738
		private void HandleServerEventExistingObjectsEnd(GameNetworkMessage baseMessage)
		{
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0008053A File Offset: 0x0007E73A
		private void HandleServerEventClearMission(GameNetworkMessage baseMessage)
		{
			base.Mission.ResetMission();
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00080548 File Offset: 0x0007E748
		private void HandleServerEventCreateMissionObject(GameNetworkMessage baseMessage)
		{
			CreateMissionObject createMissionObject = (CreateMissionObject)baseMessage;
			GameEntity gameEntity = GameEntity.Instantiate(base.Mission.Scene, createMissionObject.Prefab, createMissionObject.Frame);
			MissionObject firstScriptOfType = gameEntity.GetFirstScriptOfType<MissionObject>();
			if (firstScriptOfType != null)
			{
				firstScriptOfType.Id = createMissionObject.ObjectId;
				int num = 0;
				using (IEnumerator<GameEntity> enumerator = gameEntity.GetChildren().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MissionObject firstScriptOfType2;
						if ((firstScriptOfType2 = enumerator.Current.GetFirstScriptOfType<MissionObject>()) != null)
						{
							firstScriptOfType2.Id = createMissionObject.ChildObjectIds[num++];
						}
					}
				}
			}
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000805F4 File Offset: 0x0007E7F4
		private void HandleServerEventRemoveMissionObject(GameNetworkMessage baseMessage)
		{
			RemoveMissionObject message = (RemoveMissionObject)baseMessage;
			MissionObject missionObject = base.Mission.MissionObjects.FirstOrDefault((MissionObject mo) => mo.Id == message.ObjectId);
			if (missionObject == null)
			{
				return;
			}
			missionObject.GameEntity.Remove(82);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x00080640 File Offset: 0x0007E840
		private void HandleServerEventStopPhysicsAndSetFrameOfMissionObject(GameNetworkMessage baseMessage)
		{
			StopPhysicsAndSetFrameOfMissionObject message = (StopPhysicsAndSetFrameOfMissionObject)baseMessage;
			SpawnedItemEntity spawnedItemEntity = (SpawnedItemEntity)base.Mission.MissionObjects.FirstOrDefault((MissionObject mo) => mo.Id == message.ObjectId);
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(message.ParentId);
			if (spawnedItemEntity == null)
			{
				return;
			}
			spawnedItemEntity.StopPhysicsAndSetFrameForClient(message.Frame, (missionObjectFromMissionObjectId != null) ? missionObjectFromMissionObjectId.GameEntity : null);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000806B4 File Offset: 0x0007E8B4
		private void HandleServerEventBurstMissionObjectParticles(GameNetworkMessage baseMessage)
		{
			BurstMissionObjectParticles burstMissionObjectParticles = (BurstMissionObjectParticles)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(burstMissionObjectParticles.MissionObjectId) as SynchedMissionObject).BurstParticlesSynched(burstMissionObjectParticles.DoChildren);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000806E4 File Offset: 0x0007E8E4
		private void HandleServerEventSetMissionObjectVisibility(GameNetworkMessage baseMessage)
		{
			SetMissionObjectVisibility setMissionObjectVisibility = (SetMissionObjectVisibility)baseMessage;
			Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectVisibility.MissionObjectId).GameEntity.SetVisibilityExcludeParents(setMissionObjectVisibility.Visible);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00080713 File Offset: 0x0007E913
		private void HandleServerEventSetMissionObjectDisabled(GameNetworkMessage baseMessage)
		{
			Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((SetMissionObjectDisabled)baseMessage).MissionObjectId).SetDisabledAndMakeInvisible(false);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0008072C File Offset: 0x0007E92C
		private void HandleServerEventSetMissionObjectColors(GameNetworkMessage baseMessage)
		{
			SetMissionObjectColors setMissionObjectColors = (SetMissionObjectColors)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectColors.MissionObjectId) as SynchedMissionObject;
			if (synchedMissionObject != null)
			{
				synchedMissionObject.SetTeamColors(setMissionObjectColors.Color, setMissionObjectColors.Color2);
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00080768 File Offset: 0x0007E968
		private void HandleServerEventSetMissionObjectFrame(GameNetworkMessage baseMessage)
		{
			SetMissionObjectFrame setMissionObjectFrame = (SetMissionObjectFrame)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectFrame.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectFrame.Frame;
			synchedMissionObject.SetFrameSynched(ref frame, true);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0008079C File Offset: 0x0007E99C
		private void HandleServerEventSetMissionObjectGlobalFrame(GameNetworkMessage baseMessage)
		{
			SetMissionObjectGlobalFrame setMissionObjectGlobalFrame = (SetMissionObjectGlobalFrame)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectGlobalFrame.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectGlobalFrame.Frame;
			synchedMissionObject.SetGlobalFrameSynched(ref frame, true);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000807D0 File Offset: 0x0007E9D0
		private void HandleServerEventSetMissionObjectFrameOverTime(GameNetworkMessage baseMessage)
		{
			SetMissionObjectFrameOverTime setMissionObjectFrameOverTime = (SetMissionObjectFrameOverTime)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectFrameOverTime.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectFrameOverTime.Frame;
			synchedMissionObject.SetFrameSynchedOverTime(ref frame, setMissionObjectFrameOverTime.Duration, true);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0008080C File Offset: 0x0007EA0C
		private void HandleServerEventSetMissionObjectGlobalFrameOverTime(GameNetworkMessage baseMessage)
		{
			SetMissionObjectGlobalFrameOverTime setMissionObjectGlobalFrameOverTime = (SetMissionObjectGlobalFrameOverTime)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectGlobalFrameOverTime.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectGlobalFrameOverTime.Frame;
			synchedMissionObject.SetGlobalFrameSynchedOverTime(ref frame, setMissionObjectGlobalFrameOverTime.Duration, true);
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00080848 File Offset: 0x0007EA48
		private void HandleServerEventSetMissionObjectAnimationAtChannel(GameNetworkMessage baseMessage)
		{
			SetMissionObjectAnimationAtChannel setMissionObjectAnimationAtChannel = (SetMissionObjectAnimationAtChannel)baseMessage;
			Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectAnimationAtChannel.MissionObjectId).GameEntity.Skeleton.SetAnimationAtChannel(setMissionObjectAnimationAtChannel.AnimationIndex, setMissionObjectAnimationAtChannel.ChannelNo, setMissionObjectAnimationAtChannel.AnimationSpeed, -1f, 0f);
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00080894 File Offset: 0x0007EA94
		private void HandleServerEventSetRangedSiegeWeaponAmmo(GameNetworkMessage baseMessage)
		{
			SetRangedSiegeWeaponAmmo setRangedSiegeWeaponAmmo = (SetRangedSiegeWeaponAmmo)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setRangedSiegeWeaponAmmo.RangedSiegeWeaponId) as RangedSiegeWeapon).SetAmmo(setRangedSiegeWeaponAmmo.AmmoCount);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000808C4 File Offset: 0x0007EAC4
		private void HandleServerEventRangedSiegeWeaponChangeProjectile(GameNetworkMessage baseMessage)
		{
			RangedSiegeWeaponChangeProjectile rangedSiegeWeaponChangeProjectile = (RangedSiegeWeaponChangeProjectile)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(rangedSiegeWeaponChangeProjectile.RangedSiegeWeaponId) as RangedSiegeWeapon).ChangeProjectileEntityClient(rangedSiegeWeaponChangeProjectile.Index);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000808F4 File Offset: 0x0007EAF4
		private void HandleServerEventSetStonePileAmmo(GameNetworkMessage baseMessage)
		{
			SetStonePileAmmo setStonePileAmmo = (SetStonePileAmmo)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setStonePileAmmo.StonePileId) as StonePile).SetAmmo(setStonePileAmmo.AmmoCount);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00080924 File Offset: 0x0007EB24
		private void HandleServerEventSetRangedSiegeWeaponState(GameNetworkMessage baseMessage)
		{
			SetRangedSiegeWeaponState setRangedSiegeWeaponState = (SetRangedSiegeWeaponState)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setRangedSiegeWeaponState.RangedSiegeWeaponId) as RangedSiegeWeapon).State = setRangedSiegeWeaponState.State;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00080954 File Offset: 0x0007EB54
		private void HandleServerEventSetSiegeLadderState(GameNetworkMessage baseMessage)
		{
			SetSiegeLadderState setSiegeLadderState = (SetSiegeLadderState)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setSiegeLadderState.SiegeLadderId) as SiegeLadder).State = setSiegeLadderState.State;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00080984 File Offset: 0x0007EB84
		private void HandleServerEventSetSiegeTowerGateState(GameNetworkMessage baseMessage)
		{
			SetSiegeTowerGateState setSiegeTowerGateState = (SetSiegeTowerGateState)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setSiegeTowerGateState.SiegeTowerId) as SiegeTower).State = setSiegeTowerGateState.State;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000809B3 File Offset: 0x0007EBB3
		private void HandleServerEventSetSiegeTowerHasArrivedAtTarget(GameNetworkMessage baseMessage)
		{
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((SetSiegeTowerHasArrivedAtTarget)baseMessage).SiegeTowerId) as SiegeTower).HasArrivedAtTarget = true;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000809D0 File Offset: 0x0007EBD0
		private void HandleServerEventSetBatteringRamHasArrivedAtTarget(GameNetworkMessage baseMessage)
		{
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((SetBatteringRamHasArrivedAtTarget)baseMessage).BatteringRamId) as BatteringRam).HasArrivedAtTarget = true;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000809F0 File Offset: 0x0007EBF0
		private void HandleServerEventSetSiegeMachineMovementDistance(GameNetworkMessage baseMessage)
		{
			SetSiegeMachineMovementDistance setSiegeMachineMovementDistance = (SetSiegeMachineMovementDistance)baseMessage;
			UsableMachine usableMachine = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setSiegeMachineMovementDistance.UsableMachineId) as UsableMachine;
			if (usableMachine != null)
			{
				if (usableMachine is SiegeTower)
				{
					((SiegeTower)usableMachine).MovementComponent.SetDistanceTraveledAsClient(setSiegeMachineMovementDistance.Distance);
					return;
				}
				((BatteringRam)usableMachine).MovementComponent.SetDistanceTraveledAsClient(setSiegeMachineMovementDistance.Distance);
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00080A50 File Offset: 0x0007EC50
		private void HandleServerEventSetMissionObjectAnimationChannelParameter(GameNetworkMessage baseMessage)
		{
			SetMissionObjectAnimationChannelParameter setMissionObjectAnimationChannelParameter = (SetMissionObjectAnimationChannelParameter)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectAnimationChannelParameter.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.Skeleton.SetAnimationParameterAtChannel(setMissionObjectAnimationChannelParameter.ChannelNo, setMissionObjectAnimationChannelParameter.Parameter);
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00080A90 File Offset: 0x0007EC90
		private void HandleServerEventSetMissionObjectVertexAnimation(GameNetworkMessage baseMessage)
		{
			SetMissionObjectVertexAnimation setMissionObjectVertexAnimation = (SetMissionObjectVertexAnimation)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectVertexAnimation.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				(missionObjectFromMissionObjectId as VertexAnimator).SetAnimationSynched(setMissionObjectVertexAnimation.BeginKey, setMissionObjectVertexAnimation.EndKey, setMissionObjectVertexAnimation.Speed);
			}
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00080AD0 File Offset: 0x0007ECD0
		private void HandleServerEventSetMissionObjectVertexAnimationProgress(GameNetworkMessage baseMessage)
		{
			SetMissionObjectVertexAnimationProgress setMissionObjectVertexAnimationProgress = (SetMissionObjectVertexAnimationProgress)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectVertexAnimationProgress.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				(missionObjectFromMissionObjectId as VertexAnimator).SetProgressSynched(setMissionObjectVertexAnimationProgress.Progress);
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00080B04 File Offset: 0x0007ED04
		private void HandleServerEventSetMissionObjectAnimationPaused(GameNetworkMessage baseMessage)
		{
			SetMissionObjectAnimationPaused setMissionObjectAnimationPaused = (SetMissionObjectAnimationPaused)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectAnimationPaused.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				if (setMissionObjectAnimationPaused.IsPaused)
				{
					missionObjectFromMissionObjectId.GameEntity.PauseSkeletonAnimation();
					return;
				}
				missionObjectFromMissionObjectId.GameEntity.ResumeSkeletonAnimation();
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00080B48 File Offset: 0x0007ED48
		private void HandleServerEventAddMissionObjectBodyFlags(GameNetworkMessage baseMessage)
		{
			AddMissionObjectBodyFlags addMissionObjectBodyFlags = (AddMissionObjectBodyFlags)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(addMissionObjectBodyFlags.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.AddBodyFlags(addMissionObjectBodyFlags.BodyFlags, addMissionObjectBodyFlags.ApplyToChildren);
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00080B84 File Offset: 0x0007ED84
		private void HandleServerEventRemoveMissionObjectBodyFlags(GameNetworkMessage baseMessage)
		{
			RemoveMissionObjectBodyFlags removeMissionObjectBodyFlags = (RemoveMissionObjectBodyFlags)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(removeMissionObjectBodyFlags.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.RemoveBodyFlags(removeMissionObjectBodyFlags.BodyFlags, removeMissionObjectBodyFlags.ApplyToChildren);
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00080BC0 File Offset: 0x0007EDC0
		private void HandleServerEventSetMachineTargetRotation(GameNetworkMessage baseMessage)
		{
			SetMachineTargetRotation setMachineTargetRotation = (SetMachineTargetRotation)baseMessage;
			UsableMachine usableMachine = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMachineTargetRotation.UsableMachineId) as UsableMachine;
			if (usableMachine != null && usableMachine.PilotAgent != null)
			{
				((RangedSiegeWeapon)usableMachine).AimAtRotation(setMachineTargetRotation.HorizontalRotation, setMachineTargetRotation.VerticalRotation);
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00080C08 File Offset: 0x0007EE08
		private void HandleServerEventSetUsableGameObjectIsDeactivated(GameNetworkMessage baseMessage)
		{
			SetUsableMissionObjectIsDeactivated setUsableMissionObjectIsDeactivated = (SetUsableMissionObjectIsDeactivated)baseMessage;
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setUsableMissionObjectIsDeactivated.UsableGameObjectId) as UsableMissionObject;
			if (usableMissionObject != null)
			{
				usableMissionObject.IsDeactivated = setUsableMissionObjectIsDeactivated.IsDeactivated;
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00080C3C File Offset: 0x0007EE3C
		private void HandleServerEventSetUsableGameObjectIsDisabledForPlayers(GameNetworkMessage baseMessage)
		{
			SetUsableMissionObjectIsDisabledForPlayers setUsableMissionObjectIsDisabledForPlayers = (SetUsableMissionObjectIsDisabledForPlayers)baseMessage;
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setUsableMissionObjectIsDisabledForPlayers.UsableGameObjectId) as UsableMissionObject;
			if (usableMissionObject != null)
			{
				usableMissionObject.IsDisabledForPlayers = setUsableMissionObjectIsDisabledForPlayers.IsDisabledForPlayers;
			}
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00080C70 File Offset: 0x0007EE70
		private void HandleServerEventSetMissionObjectImpulse(GameNetworkMessage baseMessage)
		{
			SetMissionObjectImpulse setMissionObjectImpulse = (SetMissionObjectImpulse)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectImpulse.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				Vec3 position = setMissionObjectImpulse.Position;
				missionObjectFromMissionObjectId.GameEntity.ApplyLocalImpulseToDynamicBody(position, setMissionObjectImpulse.Impulse);
			}
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00080CAC File Offset: 0x0007EEAC
		private void HandleServerEventSetAgentTargetPositionAndDirection(GameNetworkMessage baseMessage)
		{
			SetAgentTargetPositionAndDirection setAgentTargetPositionAndDirection = (SetAgentTargetPositionAndDirection)baseMessage;
			Vec2 position = setAgentTargetPositionAndDirection.Position;
			Vec3 direction = setAgentTargetPositionAndDirection.Direction;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentTargetPositionAndDirection.AgentIndex, false).SetTargetPositionAndDirectionSynched(ref position, ref direction);
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		private void HandleServerEventSetAgentTargetPosition(GameNetworkMessage baseMessage)
		{
			SetAgentTargetPosition setAgentTargetPosition = (SetAgentTargetPosition)baseMessage;
			Vec2 position = setAgentTargetPosition.Position;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentTargetPosition.AgentIndex, false).SetTargetPositionSynched(ref position);
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00080D10 File Offset: 0x0007EF10
		private void HandleServerEventClearAgentTargetFrame(GameNetworkMessage baseMessage)
		{
			Mission.MissionNetworkHelper.GetAgentFromIndex(((ClearAgentTargetFrame)baseMessage).AgentIndex, false).ClearTargetFrame();
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00080D28 File Offset: 0x0007EF28
		private void HandleServerEventAgentTeleportToFrame(GameNetworkMessage baseMessage)
		{
			AgentTeleportToFrame agentTeleportToFrame = (AgentTeleportToFrame)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(agentTeleportToFrame.AgentIndex, false);
			agentFromIndex.TeleportToPosition(agentTeleportToFrame.Position);
			Vec2 vec = agentTeleportToFrame.Direction.Normalized();
			agentFromIndex.SetMovementDirection(vec);
			agentFromIndex.LookDirection = vec.ToVec3(0f);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00080D7C File Offset: 0x0007EF7C
		private void HandleServerEventSetAgentPeer(GameNetworkMessage baseMessage)
		{
			SetAgentPeer setAgentPeer = (SetAgentPeer)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentPeer.AgentIndex, true);
			if (agentFromIndex != null)
			{
				NetworkCommunicator peer = setAgentPeer.Peer;
				MissionPeer missionPeer = (peer != null) ? peer.GetComponent<MissionPeer>() : null;
				agentFromIndex.MissionPeer = missionPeer;
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00080DBC File Offset: 0x0007EFBC
		private void HandleServerEventSetAgentIsPlayer(GameNetworkMessage baseMessage)
		{
			SetAgentIsPlayer setAgentIsPlayer = (SetAgentIsPlayer)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentIsPlayer.AgentIndex, false);
			if (agentFromIndex.Controller == Agent.ControllerType.Player != setAgentIsPlayer.IsPlayer)
			{
				if (!agentFromIndex.IsMine)
				{
					agentFromIndex.Controller = Agent.ControllerType.None;
					return;
				}
				agentFromIndex.Controller = Agent.ControllerType.Player;
			}
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x00080E08 File Offset: 0x0007F008
		private void HandleServerEventSetAgentHealth(GameNetworkMessage baseMessage)
		{
			SetAgentHealth setAgentHealth = (SetAgentHealth)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentHealth.AgentIndex, false).Health = (float)setAgentHealth.Health;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00080E34 File Offset: 0x0007F034
		private void HandleServerEventAgentSetTeam(GameNetworkMessage baseMessage)
		{
			AgentSetTeam agentSetTeam = (AgentSetTeam)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(agentSetTeam.AgentIndex, false);
			MBTeam mbteamFromTeamIndex = Mission.MissionNetworkHelper.GetMBTeamFromTeamIndex(agentSetTeam.TeamIndex);
			agentFromIndex.SetTeam(base.Mission.Teams.Find(mbteamFromTeamIndex), false);
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00080E78 File Offset: 0x0007F078
		private void HandleServerEventSetAgentActionSet(GameNetworkMessage baseMessage)
		{
			SetAgentActionSet setAgentActionSet = (SetAgentActionSet)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentActionSet.AgentIndex, false);
			AnimationSystemData animationSystemData = agentFromIndex.Monster.FillAnimationSystemData(setAgentActionSet.ActionSet, setAgentActionSet.StepSize, false);
			animationSystemData.NumPaces = setAgentActionSet.NumPaces;
			animationSystemData.MonsterUsageSetIndex = setAgentActionSet.MonsterUsageSetIndex;
			animationSystemData.WalkingSpeedLimit = setAgentActionSet.WalkingSpeedLimit;
			animationSystemData.CrouchWalkingSpeedLimit = setAgentActionSet.CrouchWalkingSpeedLimit;
			agentFromIndex.SetActionSet(ref animationSystemData);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00080EEC File Offset: 0x0007F0EC
		private void HandleServerEventMakeAgentDead(GameNetworkMessage baseMessage)
		{
			MakeAgentDead makeAgentDead = (MakeAgentDead)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(makeAgentDead.AgentIndex, false).MakeDead(makeAgentDead.IsKilled, makeAgentDead.ActionCodeIndex);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00080F20 File Offset: 0x0007F120
		private void HandleServerEventAddPrefabComponentToAgentBone(GameNetworkMessage baseMessage)
		{
			AddPrefabComponentToAgentBone addPrefabComponentToAgentBone = (AddPrefabComponentToAgentBone)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(addPrefabComponentToAgentBone.AgentIndex, false).AddSynchedPrefabComponentToBone(addPrefabComponentToAgentBone.PrefabName, addPrefabComponentToAgentBone.BoneIndex);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00080F54 File Offset: 0x0007F154
		private void HandleServerEventSetAgentPrefabComponentVisibility(GameNetworkMessage baseMessage)
		{
			SetAgentPrefabComponentVisibility setAgentPrefabComponentVisibility = (SetAgentPrefabComponentVisibility)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentPrefabComponentVisibility.AgentIndex, false).SetSynchedPrefabComponentVisibility(setAgentPrefabComponentVisibility.ComponentIndex, setAgentPrefabComponentVisibility.Visibility);
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00080F88 File Offset: 0x0007F188
		private void HandleServerEventAgentSetFormation(GameNetworkMessage baseMessage)
		{
			AgentSetFormation agentSetFormation = (AgentSetFormation)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(agentSetFormation.AgentIndex, false);
			Team team = agentFromIndex.Team;
			Formation formation = null;
			if (team != null)
			{
				formation = ((agentSetFormation.FormationIndex >= 0) ? team.GetFormation((FormationClass)agentSetFormation.FormationIndex) : null);
			}
			agentFromIndex.Formation = formation;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00080FD4 File Offset: 0x0007F1D4
		private void HandleServerEventUseObject(GameNetworkMessage baseMessage)
		{
			UseObject useObject = (UseObject)baseMessage;
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(useObject.UsableGameObjectId) as UsableMissionObject;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(useObject.AgentIndex, false);
			if (usableMissionObject != null)
			{
				usableMissionObject.SetUserForClient(agentFromIndex);
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00081010 File Offset: 0x0007F210
		private void HandleServerEventStopUsingObject(GameNetworkMessage baseMessage)
		{
			StopUsingObject stopUsingObject = (StopUsingObject)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(stopUsingObject.AgentIndex, false);
			if (agentFromIndex == null)
			{
				return;
			}
			agentFromIndex.StopUsingGameObject(stopUsingObject.IsSuccessful, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00081044 File Offset: 0x0007F244
		private void HandleServerEventHitSynchronizeObjectHitpoints(GameNetworkMessage baseMessage)
		{
			SyncObjectHitpoints syncObjectHitpoints = (SyncObjectHitpoints)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(syncObjectHitpoints.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.GetFirstScriptOfType<DestructableComponent>().HitPoint = syncObjectHitpoints.Hitpoints;
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00081080 File Offset: 0x0007F280
		private void HandleServerEventHitSynchronizeObjectDestructionLevel(GameNetworkMessage baseMessage)
		{
			SyncObjectDestructionLevel syncObjectDestructionLevel = (SyncObjectDestructionLevel)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(syncObjectDestructionLevel.MissionObjectId);
			if (missionObjectFromMissionObjectId == null)
			{
				return;
			}
			missionObjectFromMissionObjectId.GameEntity.GetFirstScriptOfType<DestructableComponent>().SetDestructionLevel(syncObjectDestructionLevel.DestructionLevel, syncObjectDestructionLevel.ForcedIndex, syncObjectDestructionLevel.BlowMagnitude, syncObjectDestructionLevel.BlowPosition, syncObjectDestructionLevel.BlowDirection, false);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000810D2 File Offset: 0x0007F2D2
		private void HandleServerEventHitBurstAllHeavyHitParticles(GameNetworkMessage baseMessage)
		{
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((BurstAllHeavyHitParticles)baseMessage).MissionObjectId);
			if (missionObjectFromMissionObjectId == null)
			{
				return;
			}
			missionObjectFromMissionObjectId.GameEntity.GetFirstScriptOfType<DestructableComponent>().BurstHeavyHitParticles();
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000810F8 File Offset: 0x0007F2F8
		private void HandleServerEventSynchronizeMissionObject(GameNetworkMessage baseMessage)
		{
			SynchronizeMissionObject synchronizeMissionObject = (SynchronizeMissionObject)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(synchronizeMissionObject.MissionObjectId) as SynchedMissionObject).OnAfterReadFromNetwork(synchronizeMissionObject.RecordPair);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00081128 File Offset: 0x0007F328
		private void HandleServerEventSpawnWeaponWithNewEntity(GameNetworkMessage baseMessage)
		{
			SpawnWeaponWithNewEntity spawnWeaponWithNewEntity = (SpawnWeaponWithNewEntity)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(spawnWeaponWithNewEntity.ParentMissionObjectId);
			GameEntity gameEntity = base.Mission.SpawnWeaponWithNewEntityAux(spawnWeaponWithNewEntity.Weapon, spawnWeaponWithNewEntity.WeaponSpawnFlags, spawnWeaponWithNewEntity.Frame, spawnWeaponWithNewEntity.ForcedIndex, missionObjectFromMissionObjectId, spawnWeaponWithNewEntity.HasLifeTime);
			if (!spawnWeaponWithNewEntity.IsVisible)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00081184 File Offset: 0x0007F384
		private void HandleServerEventAttachWeaponToSpawnedWeapon(GameNetworkMessage baseMessage)
		{
			AttachWeaponToSpawnedWeapon attachWeaponToSpawnedWeapon = (AttachWeaponToSpawnedWeapon)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(attachWeaponToSpawnedWeapon.MissionObjectId);
			base.Mission.AttachWeaponWithNewEntityToSpawnedWeapon(attachWeaponToSpawnedWeapon.Weapon, missionObjectFromMissionObjectId as SpawnedItemEntity, attachWeaponToSpawnedWeapon.AttachLocalFrame);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000811C4 File Offset: 0x0007F3C4
		private void HandleServerEventAttachWeaponToAgent(GameNetworkMessage baseMessage)
		{
			AttachWeaponToAgent attachWeaponToAgent = (AttachWeaponToAgent)baseMessage;
			MatrixFrame attachLocalFrame = attachWeaponToAgent.AttachLocalFrame;
			Mission.MissionNetworkHelper.GetAgentFromIndex(attachWeaponToAgent.AgentIndex, false).AttachWeaponToBone(attachWeaponToAgent.Weapon, null, attachWeaponToAgent.BoneIndex, ref attachLocalFrame);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00081200 File Offset: 0x0007F400
		private void HandleServerEventHandleMissileCollisionReaction(GameNetworkMessage baseMessage)
		{
			HandleMissileCollisionReaction handleMissileCollisionReaction = (HandleMissileCollisionReaction)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(handleMissileCollisionReaction.AttachedMissionObjectId);
			base.Mission.HandleMissileCollisionReaction(handleMissileCollisionReaction.MissileIndex, handleMissileCollisionReaction.CollisionReaction, handleMissileCollisionReaction.AttachLocalFrame, handleMissileCollisionReaction.IsAttachedFrameLocal, Mission.MissionNetworkHelper.GetAgentFromIndex(handleMissileCollisionReaction.AttackerAgentIndex, true), Mission.MissionNetworkHelper.GetAgentFromIndex(handleMissileCollisionReaction.AttachedAgentIndex, true), handleMissileCollisionReaction.AttachedToShield, handleMissileCollisionReaction.AttachedBoneIndex, missionObjectFromMissionObjectId, handleMissileCollisionReaction.BounceBackVelocity, handleMissileCollisionReaction.BounceBackAngularVelocity, handleMissileCollisionReaction.ForcedSpawnIndex);
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0008127C File Offset: 0x0007F47C
		private void HandleServerEventSpawnWeaponAsDropFromAgent(GameNetworkMessage baseMessage)
		{
			SpawnWeaponAsDropFromAgent spawnWeaponAsDropFromAgent = (SpawnWeaponAsDropFromAgent)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(spawnWeaponAsDropFromAgent.AgentIndex, false);
			Vec3 velocity = spawnWeaponAsDropFromAgent.Velocity;
			Vec3 angularVelocity = spawnWeaponAsDropFromAgent.AngularVelocity;
			base.Mission.SpawnWeaponAsDropFromAgentAux(agentFromIndex, spawnWeaponAsDropFromAgent.EquipmentIndex, ref velocity, ref angularVelocity, spawnWeaponAsDropFromAgent.WeaponSpawnFlags, spawnWeaponAsDropFromAgent.ForcedIndex);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000812D0 File Offset: 0x0007F4D0
		private void HandleServerEventSpawnAttachedWeaponOnSpawnedWeapon(GameNetworkMessage baseMessage)
		{
			SpawnAttachedWeaponOnSpawnedWeapon spawnAttachedWeaponOnSpawnedWeapon = (SpawnAttachedWeaponOnSpawnedWeapon)baseMessage;
			SpawnedItemEntity spawnedWeapon = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(spawnAttachedWeaponOnSpawnedWeapon.SpawnedWeaponId) as SpawnedItemEntity;
			base.Mission.SpawnAttachedWeaponOnSpawnedWeapon(spawnedWeapon, spawnAttachedWeaponOnSpawnedWeapon.AttachmentIndex, spawnAttachedWeaponOnSpawnedWeapon.ForcedIndex);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00081310 File Offset: 0x0007F510
		private void HandleServerEventSpawnAttachedWeaponOnCorpse(GameNetworkMessage baseMessage)
		{
			SpawnAttachedWeaponOnCorpse spawnAttachedWeaponOnCorpse = (SpawnAttachedWeaponOnCorpse)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(spawnAttachedWeaponOnCorpse.AgentIndex, false);
			base.Mission.SpawnAttachedWeaponOnCorpse(agentFromIndex, spawnAttachedWeaponOnCorpse.AttachedIndex, spawnAttachedWeaponOnCorpse.ForcedIndex);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x0008134C File Offset: 0x0007F54C
		private void HandleServerEventRemoveEquippedWeapon(GameNetworkMessage baseMessage)
		{
			RemoveEquippedWeapon removeEquippedWeapon = (RemoveEquippedWeapon)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(removeEquippedWeapon.AgentIndex, false).RemoveEquippedWeapon(removeEquippedWeapon.SlotIndex);
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00081378 File Offset: 0x0007F578
		private void HandleServerEventBarkAgent(GameNetworkMessage baseMessage)
		{
			BarkAgent barkAgent = (BarkAgent)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(barkAgent.AgentIndex, false);
			agentFromIndex.HandleBark(barkAgent.IndexOfBark);
			if (!this._chatBox.IsPlayerMuted(agentFromIndex.MissionPeer.Peer.Id))
			{
				GameTexts.SetVariable("LEFT", agentFromIndex.Name);
				GameTexts.SetVariable("RIGHT", SkinVoiceManager.VoiceType.MpBarks[barkAgent.IndexOfBark].GetName());
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString(), Color.White, "Bark"));
			}
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00081418 File Offset: 0x0007F618
		private void HandleServerEventEquipWeaponWithNewEntity(GameNetworkMessage baseMessage)
		{
			EquipWeaponWithNewEntity equipWeaponWithNewEntity = (EquipWeaponWithNewEntity)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(equipWeaponWithNewEntity.AgentIndex, false);
			if (agentFromIndex != null)
			{
				MissionWeapon weapon = equipWeaponWithNewEntity.Weapon;
				agentFromIndex.EquipWeaponWithNewEntity(equipWeaponWithNewEntity.SlotIndex, ref weapon);
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00081454 File Offset: 0x0007F654
		private void HandleServerEventAttachWeaponToWeaponInAgentEquipmentSlot(GameNetworkMessage baseMessage)
		{
			AttachWeaponToWeaponInAgentEquipmentSlot attachWeaponToWeaponInAgentEquipmentSlot = (AttachWeaponToWeaponInAgentEquipmentSlot)baseMessage;
			MatrixFrame attachLocalFrame = attachWeaponToWeaponInAgentEquipmentSlot.AttachLocalFrame;
			Mission.MissionNetworkHelper.GetAgentFromIndex(attachWeaponToWeaponInAgentEquipmentSlot.AgentIndex, false).AttachWeaponToWeapon(attachWeaponToWeaponInAgentEquipmentSlot.SlotIndex, attachWeaponToWeaponInAgentEquipmentSlot.Weapon, null, ref attachLocalFrame);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00081490 File Offset: 0x0007F690
		private void HandleServerEventEquipWeaponFromSpawnedItemEntity(GameNetworkMessage baseMessage)
		{
			EquipWeaponFromSpawnedItemEntity equipWeaponFromSpawnedItemEntity = (EquipWeaponFromSpawnedItemEntity)baseMessage;
			SpawnedItemEntity spawnedItemEntity = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(equipWeaponFromSpawnedItemEntity.SpawnedItemEntityId) as SpawnedItemEntity;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(equipWeaponFromSpawnedItemEntity.AgentIndex, true);
			if (agentFromIndex == null)
			{
				return;
			}
			agentFromIndex.EquipWeaponFromSpawnedItemEntity(equipWeaponFromSpawnedItemEntity.SlotIndex, spawnedItemEntity, equipWeaponFromSpawnedItemEntity.RemoveWeapon);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000814D8 File Offset: 0x0007F6D8
		private void HandleServerEventCreateMissile(GameNetworkMessage baseMessage)
		{
			CreateMissile createMissile = (CreateMissile)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(createMissile.AgentIndex, false);
			if (createMissile.WeaponIndex != EquipmentIndex.None)
			{
				Vec3 velocity = createMissile.Direction * createMissile.Speed;
				base.Mission.OnAgentShootMissile(agentFromIndex, createMissile.WeaponIndex, createMissile.Position, velocity, createMissile.Orientation, createMissile.HasRigidBody, createMissile.IsPrimaryWeaponShot, createMissile.MissileIndex);
				return;
			}
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(createMissile.MissionObjectToIgnoreId);
			base.Mission.AddCustomMissile(agentFromIndex, createMissile.Weapon, createMissile.Position, createMissile.Direction, createMissile.Orientation, createMissile.Speed, createMissile.Speed, createMissile.HasRigidBody, missionObjectFromMissionObjectId, createMissile.MissileIndex);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0008158F File Offset: 0x0007F78F
		private void HandleServerEventAgentHit(GameNetworkMessage baseMessage)
		{
			CombatLogManager.GenerateCombatLog(Mission.MissionNetworkHelper.GetCombatLogDataForCombatLogNetworkMessage((CombatLogNetworkMessage)baseMessage));
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000815A4 File Offset: 0x0007F7A4
		private void HandleServerEventConsumeWeaponAmount(GameNetworkMessage baseMessage)
		{
			ConsumeWeaponAmount consumeWeaponAmount = (ConsumeWeaponAmount)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(consumeWeaponAmount.SpawnedItemEntityId) as SpawnedItemEntity).ConsumeWeaponAmount(consumeWeaponAmount.ConsumedAmount);
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000815D4 File Offset: 0x0007F7D4
		private bool HandleClientEventSetFollowedAgent(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SetFollowedAgent setFollowedAgent = (SetFollowedAgent)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setFollowedAgent.AgentIndex, true);
			component.FollowedAgent = agentFromIndex;
			return true;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x00081604 File Offset: 0x0007F804
		private bool HandleClientEventSetMachineRotation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SetMachineRotation setMachineRotation = (SetMachineRotation)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			UsableMachine usableMachine = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMachineRotation.UsableMachineId) as UsableMachine;
			if (component.IsControlledAgentActive && usableMachine is RangedSiegeWeapon)
			{
				RangedSiegeWeapon rangedSiegeWeapon = usableMachine as RangedSiegeWeapon;
				if (component.ControlledAgent == rangedSiegeWeapon.PilotAgent && rangedSiegeWeapon.PilotAgent != null)
				{
					rangedSiegeWeapon.AimAtRotation(setMachineRotation.HorizontalRotation, setMachineRotation.VerticalRotation);
				}
			}
			return true;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00081670 File Offset: 0x0007F870
		private bool HandleClientEventRequestUseObject(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			RequestUseObject requestUseObject = (RequestUseObject)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(requestUseObject.UsableMissionObjectId) as UsableMissionObject;
			if (usableMissionObject != null && component.ControlledAgent != null && component.ControlledAgent.IsActive())
			{
				Vec3 position = component.ControlledAgent.Position;
				Vec3 globalPosition = usableMissionObject.InteractionEntity.GlobalPosition;
				float num;
				if (usableMissionObject is StandingPoint)
				{
					num = usableMissionObject.GetUserFrameForAgent(component.ControlledAgent).Origin.AsVec2.DistanceSquared(component.ControlledAgent.Position.AsVec2);
				}
				else
				{
					Vec3 v;
					Vec3 v2;
					usableMissionObject.InteractionEntity.GetPhysicsMinMax(true, out v, out v2, false);
					float a = globalPosition.Distance(v);
					float b = globalPosition.Distance(v2);
					float num2 = MathF.Max(a, b);
					num = globalPosition.Distance(new Vec3(position.x, position.y, position.z + component.ControlledAgent.GetEyeGlobalHeight(), -1f));
					num -= num2;
					num = MathF.Max(num, 0f);
				}
				if (component.ControlledAgent.CurrentlyUsedGameObject != usableMissionObject && component.ControlledAgent.CanReachAndUseObject(usableMissionObject, num * num * 0.9f * 0.9f) && component.ControlledAgent.ObjectHasVacantPosition(usableMissionObject))
				{
					component.ControlledAgent.UseGameObject(usableMissionObject, requestUseObject.UsedObjectPreferenceIndex);
				}
			}
			return true;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000817E4 File Offset: 0x0007F9E4
		private bool HandleClientEventRequestStopUsingObject(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			RequestStopUsingObject requestStopUsingObject = (RequestStopUsingObject)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			Agent controlledAgent = component.ControlledAgent;
			if (((controlledAgent != null) ? controlledAgent.CurrentlyUsedGameObject : null) != null)
			{
				component.ControlledAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
			return true;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x00081824 File Offset: 0x0007FA24
		private bool HandleClientEventApplyOrder(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrder applyOrder = (ApplyOrder)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SetOrder(applyOrder.OrderType);
			}
			return true;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00081850 File Offset: 0x0007FA50
		private bool HandleClientEventApplySiegeWeaponOrder(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplySiegeWeaponOrder applySiegeWeaponOrder = (ApplySiegeWeaponOrder)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SiegeWeaponController.SetOrder(applySiegeWeaponOrder.OrderType);
			}
			return true;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00081884 File Offset: 0x0007FA84
		private bool HandleClientEventApplyOrderWithPosition(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithPosition applyOrderWithPosition = (ApplyOrderWithPosition)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				WorldPosition orderPosition = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, applyOrderWithPosition.Position, false);
				orderControllerOfPeer.SetOrderWithPosition(applyOrderWithPosition.OrderType, orderPosition);
			}
			return true;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000818D0 File Offset: 0x0007FAD0
		private bool HandleClientEventApplyOrderWithFormation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithFormation message = (ApplyOrderWithFormation)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null;
			Formation formation = (teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SetOrderWithFormation(message.OrderType, formation);
			}
			return true;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00081944 File Offset: 0x0007FB44
		private bool HandleClientEventApplyOrderWithFormationAndPercentage(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithFormationAndPercentage message = (ApplyOrderWithFormationAndPercentage)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null;
			Formation formation = (teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null;
			float percentage = (float)message.Percentage * 0.01f;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SetOrderWithFormationAndPercentage(message.OrderType, formation, percentage);
			}
			return true;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000819CC File Offset: 0x0007FBCC
		private bool HandleClientEventApplyOrderWithFormationAndNumber(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithFormationAndNumber message = (ApplyOrderWithFormationAndNumber)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null;
			Formation formation = (teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null;
			int number = message.Number;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SetOrderWithFormationAndNumber(message.OrderType, formation, number);
			}
			return true;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x00081A50 File Offset: 0x0007FC50
		private bool HandleClientEventApplyOrderWithTwoPositions(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithTwoPositions applyOrderWithTwoPositions = (ApplyOrderWithTwoPositions)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				WorldPosition position = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, applyOrderWithTwoPositions.Position1, false);
				WorldPosition position2 = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, applyOrderWithTwoPositions.Position2, false);
				orderControllerOfPeer.SetOrderWithTwoPositions(applyOrderWithTwoPositions.OrderType, position, position2);
			}
			return true;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00081ABC File Offset: 0x0007FCBC
		private bool HandleClientEventApplyOrderWithGameEntity(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			IOrderable orderWithOrderableObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((ApplyOrderWithMissionObject)baseMessage).MissionObjectId) as IOrderable;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SetOrderWithOrderableObject(orderWithOrderableObject);
			}
			return true;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00081AF4 File Offset: 0x0007FCF4
		private bool HandleClientEventApplyOrderWithAgent(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithAgent applyOrderWithAgent = (ApplyOrderWithAgent)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(applyOrderWithAgent.AgentIndex, false);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SetOrderWithAgent(applyOrderWithAgent.OrderType, agentFromIndex);
			}
			return true;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00081B2F File Offset: 0x0007FD2F
		private bool HandleClientEventSelectAllFormations(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectAllFormations selectAllFormations = (SelectAllFormations)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SelectAllFormations(false);
			}
			return true;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00081B4C File Offset: 0x0007FD4C
		private bool HandleClientEventSelectAllSiegeWeapons(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectAllSiegeWeapons selectAllSiegeWeapons = (SelectAllSiegeWeapons)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SiegeWeaponController.SelectAll();
			}
			return true;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00081B6D File Offset: 0x0007FD6D
		private bool HandleClientEventClearSelectedFormations(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ClearSelectedFormations clearSelectedFormations = (ClearSelectedFormations)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.ClearSelectedFormations();
			}
			return true;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x00081B8C File Offset: 0x0007FD8C
		private bool HandleClientEventSelectFormation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectFormation message = (SelectFormation)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null;
			Formation formation = (teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault((Formation f) => f.Index == message.FormationIndex && f.CountOfUnits > 0) : null;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SelectFormation(formation);
			}
			return true;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x00081BF8 File Offset: 0x0007FDF8
		private bool HandleClientEventSelectSiegeWeapon(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectSiegeWeapon selectSiegeWeapon = (SelectSiegeWeapon)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			SiegeWeaponController siegeWeaponController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent).SiegeWeaponController : null;
			SiegeWeapon siegeWeapon = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(selectSiegeWeapon.SiegeWeaponId) as SiegeWeapon;
			if (teamOfPeer != null && siegeWeaponController != null && siegeWeapon != null)
			{
				siegeWeaponController.Select(siegeWeapon);
			}
			return true;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00081C50 File Offset: 0x0007FE50
		private bool HandleClientEventUnselectFormation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			UnselectFormation message = (UnselectFormation)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null;
			Formation formation = (teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.DeselectFormation(formation);
			}
			return true;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00081CBC File Offset: 0x0007FEBC
		private bool HandleClientEventUnselectSiegeWeapon(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			UnselectSiegeWeapon unselectSiegeWeapon = (UnselectSiegeWeapon)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			SiegeWeaponController siegeWeaponController = (teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent).SiegeWeaponController : null;
			SiegeWeapon siegeWeapon = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(unselectSiegeWeapon.SiegeWeaponId) as SiegeWeapon;
			if (teamOfPeer != null && siegeWeaponController != null && siegeWeapon != null)
			{
				siegeWeaponController.Deselect(siegeWeapon);
			}
			return true;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00081D14 File Offset: 0x0007FF14
		private bool HandleClientEventDropWeapon(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			DropWeapon dropWeapon = (DropWeapon)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (((component != null) ? component.ControlledAgent : null) != null && component.ControlledAgent.IsActive())
			{
				component.ControlledAgent.HandleDropWeapon(dropWeapon.IsDefendPressed, dropWeapon.ForcedSlotIndexToDropWeaponFrom);
			}
			return true;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00081D64 File Offset: 0x0007FF64
		private bool HandleClientEventCheerSelected(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			TauntSelected tauntSelected = (TauntSelected)baseMessage;
			bool result = false;
			if (networkPeer.ControlledAgent != null)
			{
				networkPeer.ControlledAgent.HandleTaunt(tauntSelected.IndexOfTaunt, false);
				result = true;
			}
			return result;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00081D98 File Offset: 0x0007FF98
		private bool HandleClientEventBarkSelected(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			BarkSelected barkSelected = (BarkSelected)baseMessage;
			bool result = false;
			if (networkPeer.ControlledAgent != null)
			{
				networkPeer.ControlledAgent.HandleBark(barkSelected.IndexOfBark);
				result = true;
			}
			return result;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00081DCA File Offset: 0x0007FFCA
		private bool HandleClientEventBreakAgentVisualsInvulnerability(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			AgentVisualsBreakInvulnerability agentVisualsBreakInvulnerability = (AgentVisualsBreakInvulnerability)baseMessage;
			if (base.Mission == null || base.Mission.GetMissionBehavior<SpawnComponent>() == null || networkPeer.GetComponent<MissionPeer>() == null)
			{
				return false;
			}
			base.Mission.GetMissionBehavior<SpawnComponent>().SetEarlyAgentVisualsDespawning(networkPeer.GetComponent<MissionPeer>(), true);
			return true;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00081E0C File Offset: 0x0008000C
		private bool HandleClientEventRequestToSpawnAsBot(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			RequestToSpawnAsBot requestToSpawnAsBot = (RequestToSpawnAsBot)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			if (component.HasSpawnTimerExpired)
			{
				component.WantsToSpawnAsBot = true;
			}
			return true;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00081E3C File Offset: 0x0008003C
		private void SendExistingObjectsToPeer(NetworkCommunicator networkPeer)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				"Sending all existing objects to peer: ",
				networkPeer.UserName,
				" with index: ",
				networkPeer.Index
			}), 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new ExistingObjectsBegin());
			GameNetwork.EndModuleEventAsServer();
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new SynchronizeMissionTimeTracker((float)MissionTime.Now.ToSeconds));
			GameNetwork.EndModuleEventAsServer();
			this.SendTeamsToPeer(networkPeer);
			this.SendTeamRelationsToPeer(networkPeer);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (component != null)
				{
					if (component.Team != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new SetPeerTeam(networkCommunicator, component.Team.TeamIndex));
						GameNetwork.EndModuleEventAsServer();
					}
					if (component.Culture != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new ChangeCulture(component, component.Culture));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			this.SendFormationInformation(networkPeer);
			this.SendAgentsToPeer(networkPeer);
			this.SendSpawnedMissionObjectsToPeer(networkPeer);
			this.SynchronizeMissionObjectsToPeer(networkPeer);
			this.SendMissilesToPeer(networkPeer);
			this.SendTroopSelectionInformation(networkPeer);
			networkPeer.SendExistingObjects(base.Mission);
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new ExistingObjectsEnd());
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00081FAC File Offset: 0x000801AC
		private void SendTroopSelectionInformation(NetworkCommunicator networkPeer)
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (component != null && component.SelectedTroopIndex != 0)
				{
					GameNetwork.BeginModuleEventAsServer(networkPeer);
					GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(networkCommunicator, component.SelectedTroopIndex));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x00082020 File Offset: 0x00080220
		private void SendTeamsToPeer(NetworkCommunicator networkPeer)
		{
			foreach (Team team in base.Mission.Teams)
			{
				MBDebug.Print(string.Concat(new object[]
				{
					"Syncing a team to peer: ",
					networkPeer.UserName,
					" with index: ",
					networkPeer.Index
				}), 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new AddTeam(team.TeamIndex, team.Side, team.Color, team.Color2, (team.Banner != null) ? BannerCode.CreateFrom(team.Banner).Code : string.Empty, team.IsPlayerGeneral, team.IsPlayerSergeant));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x00082114 File Offset: 0x00080314
		private void SendTeamRelationsToPeer(NetworkCommunicator networkPeer)
		{
			int count = base.Mission.Teams.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = i; j < count; j++)
				{
					Team team = base.Mission.Teams[i];
					Team team2 = base.Mission.Teams[j];
					if (team.IsEnemyOf(team2))
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new TeamSetIsEnemyOf(team.TeamIndex, team2.TeamIndex, true));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0008219C File Offset: 0x0008039C
		private void SendFormationInformation(NetworkCommunicator networkPeer)
		{
			MBDebug.Print("formations sending begin-", 0, Debug.DebugColor.White, 17179869184UL);
			foreach (Team team in base.Mission.Teams)
			{
				if (team.IsValid && team.Side != BattleSideEnum.None)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						if (!string.IsNullOrEmpty(formation.BannerCode))
						{
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new InitializeFormation(formation, team.TeamIndex, formation.BannerCode));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
			}
			if (!networkPeer.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new SetSpawnedFormationCount(base.Mission.NumOfFormationsSpawnedTeamOne, base.Mission.NumOfFormationsSpawnedTeamTwo));
				GameNetwork.EndModuleEventAsServer();
			}
			MBDebug.Print("formations sending end-", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000822C8 File Offset: 0x000804C8
		private void SendAgentVisualsToPeer(NetworkCommunicator networkPeer, Team peerTeam)
		{
			MBDebug.Print("agentvisuals sending begin-", 0, Debug.DebugColor.White, 17179869184UL);
			foreach (MissionPeer missionPeer in from p in GameNetwork.NetworkPeers
			select p.GetComponent<MissionPeer>() into pr
			where pr != null
			select pr)
			{
				if (missionPeer.Team == peerTeam)
				{
					int amountOfAgentVisualsForPeer = missionPeer.GetAmountOfAgentVisualsForPeer();
					for (int i = 0; i < amountOfAgentVisualsForPeer; i++)
					{
						PeerVisualsHolder visuals = missionPeer.GetVisuals(i);
						IAgentVisual agentVisuals = visuals.AgentVisuals;
						MatrixFrame frame = agentVisuals.GetFrame();
						AgentBuildData agentBuildData = new AgentBuildData(MBObjectManager.Instance.GetObject<BasicCharacterObject>(agentVisuals.GetCharacterObjectID())).MissionPeer(missionPeer).Equipment(agentVisuals.GetEquipment()).VisualsIndex(visuals.VisualsIndex).Team(missionPeer.Team).InitialPosition(frame.origin);
						Vec2 vec = frame.rotation.f.AsVec2;
						vec = vec.Normalized();
						AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).IsFemale(agentVisuals.GetIsFemale()).BodyProperties(agentVisuals.GetBodyProperties());
						networkPeer.GetComponent<MissionPeer>();
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new CreateAgentVisuals(missionPeer.GetNetworkPeer(), agentBuildData2, missionPeer.SelectedTroopIndex, 0));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			MBDebug.Print("agentvisuals sending end-", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00082488 File Offset: 0x00080688
		private void SendAgentsToPeer(NetworkCommunicator networkPeer)
		{
			MBDebug.Print("agents sending begin-", 0, Debug.DebugColor.White, 17179869184UL);
			using (List<Agent>.Enumerator enumerator = base.Mission.AllAgents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Agent agent = enumerator.Current;
					bool isMount = agent.IsMount;
					AgentState state = agent.State;
					if (state == AgentState.Active || ((state == AgentState.Killed || state == AgentState.Unconscious) && (agent.GetAttachedWeaponsCount() > 0 || (!isMount && (agent.GetWieldedItemIndex(Agent.HandIndex.MainHand) >= EquipmentIndex.WeaponItemBeginSlot || agent.GetWieldedItemIndex(Agent.HandIndex.OffHand) >= EquipmentIndex.WeaponItemBeginSlot)) || base.Mission.IsAgentInProximityMap(agent))) || (state != AgentState.Active && base.Mission.Missiles.Any((Mission.Missile m) => m.ShooterAgent == agent)))
					{
						if (isMount && agent.RiderAgent == null)
						{
							MBDebug.Print("mount sending " + agent.Index, 0, Debug.DebugColor.White, 17179869184UL);
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new CreateFreeMountAgent(agent, agent.Position, agent.GetMovementDirection()));
							GameNetwork.EndModuleEventAsServer();
							agent.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkPeer);
							int attachedWeaponsCount = agent.GetAttachedWeaponsCount();
							for (int i = 0; i < attachedWeaponsCount; i++)
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new AttachWeaponToAgent(agent.GetAttachedWeapon(i), agent.Index, agent.GetAttachedWeaponBoneIndex(i), agent.GetAttachedWeaponFrame(i)));
								GameNetwork.EndModuleEventAsServer();
							}
							if (!agent.IsActive())
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new MakeAgentDead(agent.Index, state == AgentState.Killed, agent.GetCurrentActionValue(0)));
								GameNetwork.EndModuleEventAsServer();
							}
						}
						else if (!isMount)
						{
							MBDebug.Print("human sending " + agent.Index, 0, Debug.DebugColor.White, 17179869184UL);
							Agent agent2 = agent.MountAgent;
							if (agent2 != null && agent2.RiderAgent == null)
							{
								agent2 = null;
							}
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							int index = agent.Index;
							BasicCharacterObject character = agent.Character;
							Monster monster = agent.Monster;
							Equipment spawnEquipment = agent.SpawnEquipment;
							MissionEquipment equipment = agent.Equipment;
							BodyProperties bodyPropertiesValue = agent.BodyPropertiesValue;
							int bodyPropertiesSeed = agent.BodyPropertiesSeed;
							bool isFemale = agent.IsFemale;
							Team team = agent.Team;
							int agentTeamIndex = (team != null) ? team.TeamIndex : -1;
							Formation formation = agent.Formation;
							int agentFormationIndex = (formation != null) ? formation.Index : -1;
							uint clothingColor = agent.ClothingColor1;
							uint clothingColor2 = agent.ClothingColor2;
							int mountAgentIndex = (agent2 != null) ? agent2.Index : -1;
							Agent mountAgent = agent.MountAgent;
							Equipment mountAgentSpawnEquipment = (mountAgent != null) ? mountAgent.SpawnEquipment : null;
							bool isPlayerAgent = agent.MissionPeer != null && agent.OwningAgentMissionPeer == null;
							Vec3 position = agent.Position;
							Vec2 movementDirection = agent.GetMovementDirection();
							MissionPeer missionPeer = agent.MissionPeer;
							NetworkCommunicator peer;
							if ((peer = ((missionPeer != null) ? missionPeer.GetNetworkPeer() : null)) == null)
							{
								MissionPeer owningAgentMissionPeer = agent.OwningAgentMissionPeer;
								peer = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.GetNetworkPeer() : null);
							}
							GameNetwork.WriteMessage(new CreateAgent(index, character, monster, spawnEquipment, equipment, bodyPropertiesValue, bodyPropertiesSeed, isFemale, agentTeamIndex, agentFormationIndex, clothingColor, clothingColor2, mountAgentIndex, mountAgentSpawnEquipment, isPlayerAgent, position, movementDirection, peer));
							GameNetwork.EndModuleEventAsServer();
							agent.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkPeer);
							if (agent2 != null)
							{
								agent2.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkPeer);
							}
							for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
							{
								for (int j = 0; j < agent.Equipment[equipmentIndex].GetAttachedWeaponsCount(); j++)
								{
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new AttachWeaponToWeaponInAgentEquipmentSlot(agent.Equipment[equipmentIndex].GetAttachedWeapon(j), agent.Index, equipmentIndex, agent.Equipment[equipmentIndex].GetAttachedWeaponFrame(j)));
									GameNetwork.EndModuleEventAsServer();
								}
							}
							int attachedWeaponsCount2 = agent.GetAttachedWeaponsCount();
							for (int k = 0; k < attachedWeaponsCount2; k++)
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new AttachWeaponToAgent(agent.GetAttachedWeapon(k), agent.Index, agent.GetAttachedWeaponBoneIndex(k), agent.GetAttachedWeaponFrame(k)));
								GameNetwork.EndModuleEventAsServer();
							}
							if (agent2 != null)
							{
								attachedWeaponsCount2 = agent2.GetAttachedWeaponsCount();
								for (int l = 0; l < attachedWeaponsCount2; l++)
								{
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new AttachWeaponToAgent(agent2.GetAttachedWeapon(l), agent2.Index, agent2.GetAttachedWeaponBoneIndex(l), agent2.GetAttachedWeaponFrame(l)));
									GameNetwork.EndModuleEventAsServer();
								}
							}
							EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
							int mainHandCurUsageIndex = (wieldedItemIndex != EquipmentIndex.None) ? agent.Equipment[wieldedItemIndex].CurrentUsageIndex : 0;
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SetWieldedItemIndex(agent.Index, false, true, true, wieldedItemIndex, mainHandCurUsageIndex));
							GameNetwork.EndModuleEventAsServer();
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SetWieldedItemIndex(agent.Index, true, true, true, agent.GetWieldedItemIndex(Agent.HandIndex.OffHand), mainHandCurUsageIndex));
							GameNetwork.EndModuleEventAsServer();
							MBActionSet actionSet = agent.ActionSet;
							if (actionSet.IsValid)
							{
								AnimationSystemData animationSystemData = agent.Monster.FillAnimationSystemData(actionSet, agent.Character.GetStepSize(), false);
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new SetAgentActionSet(agent.Index, animationSystemData));
								GameNetwork.EndModuleEventAsServer();
								if (!agent.IsActive())
								{
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new MakeAgentDead(agent.Index, state == AgentState.Killed, agent.GetCurrentActionValue(0)));
									GameNetwork.EndModuleEventAsServer();
								}
							}
							else
							{
								Debug.FailedAssert("Checking to see if we enter this condition.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionNetworkComponent.cs", "SendAgentsToPeer", 1975);
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new MakeAgentDead(agent.Index, state == AgentState.Killed, ActionIndexValueCache.act_none));
								GameNetwork.EndModuleEventAsServer();
							}
						}
						else
						{
							MBDebug.Print("agent not sending " + agent.Index, 0, Debug.DebugColor.White, 17179869184UL);
						}
					}
				}
			}
			MBDebug.Print("agents sending end-", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00082B6C File Offset: 0x00080D6C
		private void SendSpawnedMissionObjectsToPeer(NetworkCommunicator networkPeer)
		{
			using (List<MissionObject>.Enumerator enumerator = base.Mission.MissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MissionObject missionObject = enumerator.Current;
					SpawnedItemEntity spawnedItemEntity;
					if ((spawnedItemEntity = (missionObject as SpawnedItemEntity)) != null)
					{
						GameEntity gameEntity = spawnedItemEntity.GameEntity;
						if (gameEntity.Parent == null || !gameEntity.Parent.HasScriptOfType<SpawnedItemEntity>())
						{
							MissionObject missionObject2 = null;
							if (spawnedItemEntity.GameEntity.Parent != null)
							{
								missionObject2 = gameEntity.Parent.GetFirstScriptOfType<MissionObject>();
							}
							MatrixFrame matrixFrame = gameEntity.GetGlobalFrame();
							if (missionObject2 != null)
							{
								matrixFrame = missionObject2.GameEntity.GetGlobalFrame().TransformToLocalNonOrthogonal(ref matrixFrame);
							}
							matrixFrame.origin.z = MathF.Max(matrixFrame.origin.z, CompressionBasic.PositionCompressionInfo.GetMinimumValue() + 1f);
							Mission.WeaponSpawnFlags weaponSpawnFlags = spawnedItemEntity.SpawnFlags;
							if (weaponSpawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics) && !gameEntity.GetPhysicsState())
							{
								weaponSpawnFlags = ((weaponSpawnFlags & ~Mission.WeaponSpawnFlags.WithPhysics) | Mission.WeaponSpawnFlags.WithStaticPhysics);
							}
							bool hasLifeTime = true;
							bool isVisible = gameEntity.Parent == null || missionObject2 != null;
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SpawnWeaponWithNewEntity(spawnedItemEntity.WeaponCopy, weaponSpawnFlags, spawnedItemEntity.Id.Id, matrixFrame, (missionObject2 != null) ? missionObject2.Id : MissionObjectId.Invalid, isVisible, hasLifeTime));
							GameNetwork.EndModuleEventAsServer();
							for (int i = 0; i < spawnedItemEntity.WeaponCopy.GetAttachedWeaponsCount(); i++)
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new AttachWeaponToSpawnedWeapon(spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i), spawnedItemEntity.Id, spawnedItemEntity.WeaponCopy.GetAttachedWeaponFrame(i)));
								GameNetwork.EndModuleEventAsServer();
								if (spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i).Item.ItemFlags.HasAnyFlag(ItemFlags.CanBePickedUpFromCorpse))
								{
									if (gameEntity.GetChild(i) == null)
									{
										Debug.Print(string.Concat(new object[]
										{
											"spawnedItemGameEntity child is null. item: ",
											spawnedItemEntity.WeaponCopy.Item.StringId,
											" attached item: ",
											spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i).Item.StringId,
											" attachment index: ",
											i
										}), 0, Debug.DebugColor.White, 17592186044416UL);
									}
									else if (gameEntity.GetChild(i).GetFirstScriptOfType<SpawnedItemEntity>() == null)
									{
										Debug.Print(string.Concat(new object[]
										{
											"spawnedItemGameEntity child SpawnedItemEntity script is null. item: ",
											spawnedItemEntity.WeaponCopy.Item.StringId,
											" attached item: ",
											spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i).Item.StringId,
											" attachment index: ",
											i
										}), 0, Debug.DebugColor.White, 17592186044416UL);
									}
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new SpawnAttachedWeaponOnSpawnedWeapon(spawnedItemEntity.Id, i, gameEntity.GetChild(i).GetFirstScriptOfType<SpawnedItemEntity>().Id.Id));
									GameNetwork.EndModuleEventAsServer();
								}
							}
						}
					}
					else if (missionObject.CreatedAtRuntime)
					{
						Mission.DynamicallyCreatedEntity dynamicallyCreatedEntity = base.Mission.AddedEntitiesInfo.SingleOrDefault((Mission.DynamicallyCreatedEntity x) => x.ObjectId == missionObject.Id);
						if (dynamicallyCreatedEntity != null)
						{
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new CreateMissionObject(dynamicallyCreatedEntity.ObjectId, dynamicallyCreatedEntity.Prefab, dynamicallyCreatedEntity.Frame, dynamicallyCreatedEntity.ChildObjectIds));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00082F3C File Offset: 0x0008113C
		private void SynchronizeMissionObjectsToPeer(NetworkCommunicator networkPeer)
		{
			using (List<MissionObject>.Enumerator enumerator = base.Mission.MissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SynchedMissionObject synchedMissionObject;
					if ((synchedMissionObject = (enumerator.Current as SynchedMissionObject)) != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new SynchronizeMissionObject(synchedMissionObject));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00082FAC File Offset: 0x000811AC
		private void SendMissilesToPeer(NetworkCommunicator networkPeer)
		{
			foreach (Mission.Missile missile in base.Mission.Missiles)
			{
				Vec3 velocity = missile.GetVelocity();
				float num = velocity.Normalize();
				Mat3 identity = Mat3.Identity;
				identity.f = velocity;
				identity.Orthonormalize();
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				int index = missile.Index;
				int index2 = missile.ShooterAgent.Index;
				EquipmentIndex weaponIndex = EquipmentIndex.None;
				MissionWeapon weapon = missile.Weapon;
				Vec3 position = missile.GetPosition();
				Vec3 direction = velocity;
				float speed = num;
				Mat3 orientation = identity;
				bool hasRigidBody = missile.GetHasRigidBody();
				MissionObject missionObjectToIgnore = missile.MissionObjectToIgnore;
				GameNetwork.WriteMessage(new CreateMissile(index, index2, weaponIndex, weapon, position, direction, speed, orientation, hasRigidBody, (missionObjectToIgnore != null) ? missionObjectToIgnore.Id : MissionObjectId.Invalid, false));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00083078 File Offset: 0x00081278
		public override void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null && component.HasSpawnedAgentVisuals)
			{
				base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, false);
				component.HasSpawnedAgentVisuals = false;
			}
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000830B0 File Offset: 0x000812B0
		protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					if (networkCommunicator.IsSynchronized || networkCommunicator.JustReconnecting)
					{
						networkCommunicator.VirtualPlayer.SynchronizeComponentsTo(networkPeer.VirtualPlayer);
					}
				}
				foreach (NetworkCommunicator networkCommunicator2 in GameNetwork.DisconnectedNetworkPeers)
				{
					networkCommunicator2.VirtualPlayer.SynchronizeComponentsTo(networkPeer.VirtualPlayer);
				}
			}
			MissionPeer missionPeer = networkPeer.AddComponent<MissionPeer>();
			if (networkPeer.JustReconnecting && missionPeer.Team != null)
			{
				MBAPI.IMBPeer.SetTeam(networkPeer.Index, missionPeer.Team.MBTeam.Index);
			}
			missionPeer.JoinTime = DateTime.Now;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000831B4 File Offset: 0x000813B4
		protected override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				this.SendExistingObjectsToPeer(networkPeer);
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000831C8 File Offset: 0x000813C8
		protected override void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				Mission mission = base.Mission;
				if (mission != null)
				{
					mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, true);
				}
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(component.GetNetworkPeer()));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				component.HasSpawnedAgentVisuals = false;
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00083221 File Offset: 0x00081421
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0008322C File Offset: 0x0008142C
		protected override void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				if (component.ControlledAgent != null)
				{
					Agent controlledAgent = component.ControlledAgent;
					Blow b = new Blow(controlledAgent.Index);
					b.WeaponRecord = default(BlowWeaponRecord);
					b.DamageType = DamageTypes.Invalid;
					b.BaseMagnitude = 10000f;
					b.WeaponRecord.WeaponClass = WeaponClass.Undefined;
					b.GlobalPosition = controlledAgent.Position;
					b.DamagedPercentage = 1f;
					controlledAgent.Die(b, Agent.KillInfo.Invalid);
				}
				if (base.Mission.AllAgents != null)
				{
					foreach (Agent agent in base.Mission.AllAgents)
					{
						if (agent.MissionPeer == component)
						{
							agent.MissionPeer = null;
						}
						if (agent.OwningAgentMissionPeer == component)
						{
							agent.OwningAgentMissionPeer = null;
						}
					}
				}
				if (component.ControlledFormation != null)
				{
					component.ControlledFormation.PlayerOwner = null;
				}
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0008333C File Offset: 0x0008153C
		public override void OnAddTeam(Team team)
		{
			base.OnAddTeam(team);
			if (GameNetwork.IsServerOrRecorder)
			{
				MBDebug.Print("----------OnAddTeam-", 0, Debug.DebugColor.White, 17592186044416UL);
				MBDebug.Print("Adding a team and sending it to all clients", 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new AddTeam(team.TeamIndex, team.Side, team.Color, team.Color2, (team.Banner != null) ? BannerCode.CreateFrom(team.Banner).Code : string.Empty, team.IsPlayerGeneral, team.IsPlayerSergeant));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				return;
			}
			if (team.Side != BattleSideEnum.Attacker && team.Side != BattleSideEnum.Defender && base.Mission.SpectatorTeam == null)
			{
				base.Mission.SpectatorTeam = team;
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0008340B File Offset: 0x0008160B
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._chatBox = Game.Current.GetGameHandler<ChatBox>();
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x00083423 File Offset: 0x00081623
		public override void OnClearScene()
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				MBDebug.Print("I am clearing the scene, and sending this message to all clients", 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new ClearMission());
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0008345C File Offset: 0x0008165C
		public override void OnMissionTick(float dt)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				this._accumulatedTimeSinceLastTimerSync += dt;
				if (this._accumulatedTimeSinceLastTimerSync > 2f)
				{
					this._accumulatedTimeSinceLastTimerSync -= 2f;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SynchronizeMissionTimeTracker((float)MissionTime.Now.ToSeconds));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
			}
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				MissionRepresentativeBase component = networkCommunicator.GetComponent<MissionRepresentativeBase>();
				if (component != null)
				{
					component.Tick(dt);
				}
				if (GameNetwork.IsServer && !networkCommunicator.IsServerPeer && !MultiplayerOptions.OptionType.DisableInactivityKick.GetBoolValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
				{
					MissionPeer component2 = networkCommunicator.GetComponent<MissionPeer>();
					if (component2 != null)
					{
						component2.TickInactivityStatus();
					}
				}
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0008353C File Offset: 0x0008173C
		protected override void OnEndMission()
		{
			if (GameNetwork.IsServer)
			{
				foreach (MissionPeer missionPeer in VirtualPlayer.Peers<MissionPeer>())
				{
					missionPeer.ControlledAgent = null;
				}
				foreach (Agent agent in base.Mission.AllAgents)
				{
					agent.MissionPeer = null;
				}
			}
			base.OnEndMission();
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000835E0 File Offset: 0x000817E0
		public void OnPeerSelectedTeam(MissionPeer missionPeer)
		{
			this.SendAgentVisualsToPeer(missionPeer.GetNetworkPeer(), missionPeer.Team);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000835F4 File Offset: 0x000817F4
		public void OnClientSynchronized(NetworkCommunicator networkPeer)
		{
			Action<NetworkCommunicator> onClientSynchronizedEvent = this.OnClientSynchronizedEvent;
			if (onClientSynchronizedEvent != null)
			{
				onClientSynchronizedEvent(networkPeer);
			}
			if (networkPeer.IsMine)
			{
				Action onMyClientSynchronized = this.OnMyClientSynchronized;
				if (onMyClientSynchronized == null)
				{
					return;
				}
				onMyClientSynchronized();
			}
		}

		// Token: 0x04000CFD RID: 3325
		private float _accumulatedTimeSinceLastTimerSync;

		// Token: 0x04000CFE RID: 3326
		private const float TimerSyncPeriod = 2f;

		// Token: 0x04000CFF RID: 3327
		private ChatBox _chatBox;
	}
}
