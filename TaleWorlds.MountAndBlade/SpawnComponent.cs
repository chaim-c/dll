using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B4 RID: 692
	public class SpawnComponent : MissionLogic
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x00090366 File Offset: 0x0008E566
		// (set) Token: 0x060025DB RID: 9691 RVA: 0x0009036E File Offset: 0x0008E56E
		public SpawnFrameBehaviorBase SpawnFrameBehavior { get; private set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00090377 File Offset: 0x0008E577
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x0009037F File Offset: 0x0008E57F
		public SpawningBehaviorBase SpawningBehavior { get; private set; }

		// Token: 0x060025DE RID: 9694 RVA: 0x00090388 File Offset: 0x0008E588
		public SpawnComponent(SpawnFrameBehaviorBase spawnFrameBehavior, SpawningBehaviorBase spawningBehavior)
		{
			this.SpawnFrameBehavior = spawnFrameBehavior;
			this.SpawningBehavior = spawningBehavior;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0009039E File Offset: 0x0008E59E
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionMultiplayerGameModeBase = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000903B7 File Offset: 0x0008E5B7
		public bool AreAgentsSpawning()
		{
			return this.SpawningBehavior.AreAgentsSpawning();
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000903C4 File Offset: 0x0008E5C4
		public void SetNewSpawnFrameBehavior(SpawnFrameBehaviorBase spawnFrameBehavior)
		{
			this.SpawnFrameBehavior = spawnFrameBehavior;
			if (this.SpawnFrameBehavior != null)
			{
				this.SpawnFrameBehavior.Initialize();
			}
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000903E0 File Offset: 0x0008E5E0
		public void SetNewSpawningBehavior(SpawningBehaviorBase spawningBehavior)
		{
			this.SpawningBehavior = spawningBehavior;
			if (this.SpawningBehavior != null)
			{
				this.SpawningBehavior.Initialize(this);
			}
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000903FD File Offset: 0x0008E5FD
		protected override void OnEndMission()
		{
			base.OnEndMission();
			this.SpawningBehavior.Clear();
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00090410 File Offset: 0x0008E610
		public static void SetSiegeSpawningBehavior()
		{
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new SiegeSpawnFrameBehavior());
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new SiegeSpawningBehavior());
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0009043A File Offset: 0x0008E63A
		public static void SetFlagDominationSpawningBehavior()
		{
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new FlagDominationSpawnFrameBehavior());
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new FlagDominationSpawningBehavior());
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00090464 File Offset: 0x0008E664
		public static void SetWarmupSpawningBehavior()
		{
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new FFASpawnFrameBehavior());
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new WarmupSpawningBehavior());
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0009048E File Offset: 0x0008E68E
		public static void SetSpawningBehaviorForCurrentGameType(MultiplayerGameType currentGameType)
		{
			if (currentGameType == MultiplayerGameType.Siege)
			{
				SpawnComponent.SetSiegeSpawningBehavior();
				return;
			}
			if (currentGameType - MultiplayerGameType.Battle > 2)
			{
				return;
			}
			SpawnComponent.SetFlagDominationSpawningBehavior();
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000904A6 File Offset: 0x0008E6A6
		public override void AfterStart()
		{
			base.AfterStart();
			this.SetNewSpawnFrameBehavior(this.SpawnFrameBehavior);
			this.SetNewSpawningBehavior(this.SpawningBehavior);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000904C6 File Offset: 0x0008E6C6
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this.SpawningBehavior.OnTick(dt);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000904DB File Offset: 0x0008E6DB
		protected void StartSpawnSession()
		{
			this.SpawningBehavior.RequestStartSpawnSession();
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000904E8 File Offset: 0x0008E6E8
		public MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn = false)
		{
			SpawnFrameBehaviorBase spawnFrameBehavior = this.SpawnFrameBehavior;
			if (spawnFrameBehavior == null)
			{
				return MatrixFrame.Identity;
			}
			return spawnFrameBehavior.GetSpawnFrame(team, hasMount, isInitialSpawn);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00090502 File Offset: 0x0008E702
		protected void SpawnEquipmentUpdated(MissionPeer lobbyPeer, Equipment equipment)
		{
			if (GameNetwork.IsServer && lobbyPeer != null && this.SpawningBehavior.CanUpdateSpawnEquipment(lobbyPeer) && lobbyPeer.HasSpawnedAgentVisuals)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new EquipEquipmentToPeer(lobbyPeer.GetNetworkPeer(), equipment));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00090541 File Offset: 0x0008E741
		public void SetEarlyAgentVisualsDespawning(MissionPeer missionPeer, bool canDespawnEarly = true)
		{
			if (missionPeer != null && this.AllowEarlyAgentVisualsDespawning(missionPeer))
			{
				missionPeer.EquipmentUpdatingExpired = canDespawnEarly;
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x00090556 File Offset: 0x0008E756
		public void ToggleUpdatingSpawnEquipment(bool canUpdate)
		{
			this.SpawningBehavior.ToggleUpdatingSpawnEquipment(canUpdate);
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00090564 File Offset: 0x0008E764
		public bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
		{
			MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(lobbyPeer, false);
			return this._missionMultiplayerGameModeBase.IsClassAvailable(mpheroClassForPeer) && this.SpawningBehavior.AllowEarlyAgentVisualsDespawning(lobbyPeer);
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00090595 File Offset: 0x0008E795
		public int GetMaximumReSpawnPeriodForPeer(MissionPeer lobbyPeer)
		{
			return this.SpawningBehavior.GetMaximumReSpawnPeriodForPeer(lobbyPeer);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000905A3 File Offset: 0x0008E7A3
		public override void OnClearScene()
		{
			base.OnClearScene();
			this.SpawningBehavior.OnClearScene();
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000905B6 File Offset: 0x0008E7B6
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			this.SpawningBehavior.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
			this.SpawnFrameBehavior.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
		}

		// Token: 0x04000E10 RID: 3600
		private MissionMultiplayerGameModeBase _missionMultiplayerGameModeBase;
	}
}
