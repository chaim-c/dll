using System;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000299 RID: 665
	public class MissionLobbyEquipmentNetworkComponent : MissionNetwork
	{
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060022CD RID: 8909 RVA: 0x0007EE10 File Offset: 0x0007D010
		// (remove) Token: 0x060022CE RID: 8910 RVA: 0x0007EE48 File Offset: 0x0007D048
		public event MissionLobbyEquipmentNetworkComponent.OnToggleLoadoutDelegate OnToggleLoadout;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060022CF RID: 8911 RVA: 0x0007EE80 File Offset: 0x0007D080
		// (remove) Token: 0x060022D0 RID: 8912 RVA: 0x0007EEB8 File Offset: 0x0007D0B8
		public event MissionLobbyEquipmentNetworkComponent.OnRefreshEquipmentEventDelegate OnEquipmentRefreshed;

		// Token: 0x060022D1 RID: 8913 RVA: 0x0007EEF0 File Offset: 0x0007D0F0
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (!GameNetwork.IsDedicatedServer)
			{
				this._agentVisualSpawnComponent = Mission.Current.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
				this._agentVisualSpawnComponent.OnMyAgentVisualSpawned += this.OpenLoadout;
				this._agentVisualSpawnComponent.OnMyAgentSpawnedFromVisual += this.CloseLoadout;
				this._agentVisualSpawnComponent.OnMyAgentVisualRemoved += this.CloseLoadout;
			}
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0007EF60 File Offset: 0x0007D160
		protected override void OnEndMission()
		{
			if (!GameNetwork.IsDedicatedServer)
			{
				this._agentVisualSpawnComponent.OnMyAgentVisualSpawned -= this.OpenLoadout;
				this._agentVisualSpawnComponent.OnMyAgentSpawnedFromVisual -= this.CloseLoadout;
				this._agentVisualSpawnComponent.OnMyAgentVisualRemoved -= this.CloseLoadout;
				this._agentVisualSpawnComponent = null;
			}
			base.OnEndMission();
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0007EFC8 File Offset: 0x0007D1C8
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<RequestTroopIndexChange>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventLobbyEquipmentUpdated));
				registerer.RegisterBaseHandler<TeamInitialPerkInfoMessage>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventTeamInitialPerkInfoMessage));
				registerer.RegisterBaseHandler<RequestPerkChange>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestPerkChange));
				return;
			}
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<UpdateSelectedTroopIndex>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventEquipmentIndexUpdated));
				registerer.RegisterBaseHandler<SyncPerksForCurrentlySelectedTroop>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.SyncPerksForCurrentlySelectedTroop));
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0007F040 File Offset: 0x0007D240
		private void HandleServerEventEquipmentIndexUpdated(GameNetworkMessage baseMessage)
		{
			UpdateSelectedTroopIndex updateSelectedTroopIndex = (UpdateSelectedTroopIndex)baseMessage;
			updateSelectedTroopIndex.Peer.GetComponent<MissionPeer>().SelectedTroopIndex = updateSelectedTroopIndex.SelectedTroopIndex;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0007F06C File Offset: 0x0007D26C
		private void SyncPerksForCurrentlySelectedTroop(GameNetworkMessage baseMessage)
		{
			SyncPerksForCurrentlySelectedTroop syncPerksForCurrentlySelectedTroop = (SyncPerksForCurrentlySelectedTroop)baseMessage;
			MissionPeer component = syncPerksForCurrentlySelectedTroop.Peer.GetComponent<MissionPeer>();
			for (int i = 0; i < 3; i++)
			{
				component.SelectPerk(i, syncPerksForCurrentlySelectedTroop.PerkIndices[i], component.SelectedTroopIndex);
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x0007F0B0 File Offset: 0x0007D2B0
		private bool HandleClientEventLobbyEquipmentUpdated(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			RequestTroopIndexChange requestTroopIndexChange = (RequestTroopIndexChange)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			SpawnComponent missionBehavior = base.Mission.GetMissionBehavior<SpawnComponent>();
			if (missionBehavior == null)
			{
				return false;
			}
			if (missionBehavior.AreAgentsSpawning() && component.SelectedTroopIndex != requestTroopIndexChange.SelectedTroopIndex)
			{
				if (component.Culture == null || requestTroopIndexChange.SelectedTroopIndex < 0 || MultiplayerClassDivisions.GetMPHeroClasses(component.Culture).Count<MultiplayerClassDivisions.MPHeroClass>() <= requestTroopIndexChange.SelectedTroopIndex)
				{
					component.SelectedTroopIndex = 0;
				}
				else
				{
					component.SelectedTroopIndex = requestTroopIndexChange.SelectedTroopIndex;
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(peer, component.SelectedTroopIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, peer);
				if (this.OnEquipmentRefreshed != null)
				{
					this.OnEquipmentRefreshed(component);
				}
			}
			return true;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0007F168 File Offset: 0x0007D368
		private bool HandleClientEventTeamInitialPerkInfoMessage(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			TeamInitialPerkInfoMessage teamInitialPerkInfoMessage = (TeamInitialPerkInfoMessage)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			if (base.Mission.GetMissionBehavior<SpawnComponent>() == null)
			{
				return false;
			}
			component.OnTeamInitialPerkInfoReceived(teamInitialPerkInfoMessage.Perks);
			return true;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0007F1A4 File Offset: 0x0007D3A4
		private bool HandleClientEventRequestPerkChange(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			RequestPerkChange requestPerkChange = (RequestPerkChange)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			SpawnComponent missionBehavior = base.Mission.GetMissionBehavior<SpawnComponent>();
			if (missionBehavior == null)
			{
				return false;
			}
			if (component.SelectPerk(requestPerkChange.PerkListIndex, requestPerkChange.PerkIndex, -1) && missionBehavior.AreAgentsSpawning() && this.OnEquipmentRefreshed != null)
			{
				this.OnEquipmentRefreshed(component);
			}
			return true;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0007F208 File Offset: 0x0007D408
		public void PerkUpdated(int perkList, int perkIndex)
		{
			if (GameNetwork.IsServer)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (this.OnEquipmentRefreshed != null)
				{
					this.OnEquipmentRefreshed(component);
					return;
				}
			}
			else
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestPerkChange(perkList, perkIndex));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0007F254 File Offset: 0x0007D454
		public void EquipmentUpdated()
		{
			if (GameNetwork.IsServer)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component.SelectedTroopIndex != component.NextSelectedTroopIndex)
				{
					component.SelectedTroopIndex = component.NextSelectedTroopIndex;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(GameNetwork.MyPeer, component.SelectedTroopIndex));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, GameNetwork.MyPeer);
					if (this.OnEquipmentRefreshed != null)
					{
						this.OnEquipmentRefreshed(component);
						return;
					}
				}
			}
			else
			{
				MissionPeer component2 = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestTroopIndexChange(component2.NextSelectedTroopIndex));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0007F2EB File Offset: 0x0007D4EB
		public void ToggleLoadout(bool isActive)
		{
			if (this.OnToggleLoadout != null)
			{
				this.OnToggleLoadout(isActive);
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0007F301 File Offset: 0x0007D501
		private void OpenLoadout()
		{
			this.ToggleLoadout(true);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0007F30A File Offset: 0x0007D50A
		private void CloseLoadout()
		{
			this.ToggleLoadout(false);
		}

		// Token: 0x04000CF8 RID: 3320
		private MultiplayerMissionAgentVisualSpawnComponent _agentVisualSpawnComponent;

		// Token: 0x02000547 RID: 1351
		// (Invoke) Token: 0x0600392E RID: 14638
		public delegate void OnToggleLoadoutDelegate(bool isActive);

		// Token: 0x02000548 RID: 1352
		// (Invoke) Token: 0x06003932 RID: 14642
		public delegate void OnRefreshEquipmentEventDelegate(MissionPeer lobbyPeer);
	}
}
