using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A6 RID: 678
	public abstract class MissionMultiplayerGameModeBase : MissionNetwork
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060024BA RID: 9402
		public abstract bool IsGameModeHidingAllAgentVisuals { get; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060024BB RID: 9403
		public abstract bool IsGameModeUsingOpposingTeams { get; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0008A2E6 File Offset: 0x000884E6
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x0008A2EE File Offset: 0x000884EE
		public SpawnComponent SpawnComponent { get; private set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x0008A2F7 File Offset: 0x000884F7
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x0008A2FF File Offset: 0x000884FF
		private protected bool CanGameModeSystemsTickThisFrame { protected get; private set; }

		// Token: 0x060024C0 RID: 9408
		public abstract MultiplayerGameType GetMissionType();

		// Token: 0x060024C1 RID: 9409 RVA: 0x0008A308 File Offset: 0x00088508
		public virtual bool CheckIfOvertime()
		{
			return false;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0008A30C File Offset: 0x0008850C
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this.MultiplayerTeamSelectComponent = base.Mission.GetMissionBehavior<MultiplayerTeamSelectComponent>();
			this.MissionLobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
			this.GameModeBaseClient = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
			this.NotificationsComponent = base.Mission.GetMissionBehavior<MultiplayerGameNotificationsComponent>();
			this.RoundController = base.Mission.GetMissionBehavior<MultiplayerRoundController>();
			this.WarmupComponent = base.Mission.GetMissionBehavior<MultiplayerWarmupComponent>();
			this.TimerComponent = base.Mission.GetMissionBehavior<MultiplayerTimerComponent>();
			this.SpawnComponent = Mission.Current.GetMissionBehavior<SpawnComponent>();
			this._agentVisualSpawnComponent = base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
			this._lastPerkTickTime = Mission.Current.CurrentTime;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x0008A3C8 File Offset: 0x000885C8
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (Mission.Current.CurrentTime - this._lastPerkTickTime >= 1f)
			{
				this._lastPerkTickTime = Mission.Current.CurrentTime;
				MPPerkObject.TickAllPeerPerks((int)(this._lastPerkTickTime / 1f));
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0008A416 File Offset: 0x00088616
		public virtual bool CheckForWarmupEnd()
		{
			return false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0008A419 File Offset: 0x00088619
		public virtual bool CheckForRoundEnd()
		{
			return false;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0008A41C File Offset: 0x0008861C
		public virtual bool CheckForMatchEnd()
		{
			return false;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0008A41F File Offset: 0x0008861F
		public virtual bool UseCultureSelection()
		{
			return false;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0008A422 File Offset: 0x00088622
		public virtual bool UseRoundController()
		{
			return false;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0008A425 File Offset: 0x00088625
		public virtual Team GetWinnerTeam()
		{
			return null;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0008A428 File Offset: 0x00088628
		public virtual void OnPeerChangedTeam(NetworkCommunicator peer, Team oldTeam, Team newTeam)
		{
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0008A42A File Offset: 0x0008862A
		public override void OnClearScene()
		{
			base.OnClearScene();
			if (this.RoundController == null)
			{
				this.ClearPeerCounts();
			}
			this._lastPerkTickTime = Mission.Current.CurrentTime;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0008A450 File Offset: 0x00088650
		public void ClearPeerCounts()
		{
			List<MissionPeer> list = VirtualPlayer.Peers<MissionPeer>();
			for (int i = 0; i < list.Count; i++)
			{
				MissionPeer missionPeer = list[i];
				missionPeer.AssistCount = 0;
				missionPeer.DeathCount = 0;
				missionPeer.KillCount = 0;
				missionPeer.Score = 0;
				missionPeer.ResetRequestedKickPollCount();
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new KillDeathCountChange(missionPeer.GetNetworkPeer(), null, missionPeer.KillCount, missionPeer.AssistCount, missionPeer.DeathCount, missionPeer.Score));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0008A4D4 File Offset: 0x000886D4
		public bool ShouldSpawnVisualsForServer(NetworkCommunicator spawningNetworkPeer)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				return false;
			}
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
			if (missionPeer != null)
			{
				MissionPeer component = spawningNetworkPeer.GetComponent<MissionPeer>();
				return (!this.IsGameModeHidingAllAgentVisuals && component.Team == missionPeer.Team) || spawningNetworkPeer.IsServerPeer;
			}
			return false;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0008A534 File Offset: 0x00088734
		public void HandleAgentVisualSpawning(NetworkCommunicator spawningNetworkPeer, AgentBuildData spawningAgentBuildData, int troopCountInFormation = 0, bool useCosmetics = true)
		{
			MissionPeer component = spawningNetworkPeer.GetComponent<MissionPeer>();
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new SyncPerksForCurrentlySelectedTroop(spawningNetworkPeer, component.Perks[component.SelectedTroopIndex]));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, spawningNetworkPeer);
			component.HasSpawnedAgentVisuals = true;
			component.EquipmentUpdatingExpired = false;
			if (useCosmetics)
			{
				this.AddCosmeticItemsToEquipment(spawningAgentBuildData.AgentOverridenSpawnEquipment, this.GetUsedCosmeticsFromPeer(component, spawningAgentBuildData.AgentCharacter));
			}
			if (!this.IsGameModeHidingAllAgentVisuals)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new CreateAgentVisuals(spawningNetworkPeer, spawningAgentBuildData, component.SelectedTroopIndex, troopCountInFormation));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, spawningNetworkPeer);
				return;
			}
			if (!spawningNetworkPeer.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(spawningNetworkPeer);
				GameNetwork.WriteMessage(new CreateAgentVisuals(spawningNetworkPeer, spawningAgentBuildData, component.SelectedTroopIndex, troopCountInFormation));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0008A5EB File Offset: 0x000887EB
		public virtual bool AllowCustomPlayerBanners()
		{
			return true;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0008A5EE File Offset: 0x000887EE
		public int GetScoreForKill(Agent killedAgent)
		{
			return 20;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0008A5F2 File Offset: 0x000887F2
		public virtual float GetTroopNumberMultiplierForMissingPlayer(MissionPeer spawningPeer)
		{
			return 1f;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0008A5F9 File Offset: 0x000887F9
		public int GetCurrentGoldForPeer(MissionPeer peer)
		{
			return peer.Representative.Gold;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0008A608 File Offset: 0x00088808
		public void ChangeCurrentGoldForPeer(MissionPeer peer, int newAmount)
		{
			if (newAmount >= 0)
			{
				newAmount = MBMath.ClampInt(newAmount, 0, 2000);
			}
			if (peer.Peer.Communicator.IsConnectionActive)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SyncGoldsForSkirmish(peer.Peer, newAmount));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
			if (this.GameModeBaseClient != null)
			{
				this.GameModeBaseClient.OnGoldAmountChangedForRepresentative(peer.Representative, newAmount);
			}
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0008A670 File Offset: 0x00088870
		protected override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (this.GameModeBaseClient.IsGameModeUsingGold)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					if (networkCommunicator != networkPeer)
					{
						MissionRepresentativeBase component = networkCommunicator.GetComponent<MissionRepresentativeBase>();
						if (component != null)
						{
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SyncGoldsForSkirmish(component.Peer, component.Gold));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
			}
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0008A6F8 File Offset: 0x000888F8
		public virtual bool CheckIfPlayerCanDespawn(MissionPeer missionPeer)
		{
			return false;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0008A6FB File Offset: 0x000888FB
		public override void OnPreMissionTick(float dt)
		{
			this.CanGameModeSystemsTickThisFrame = false;
			this._gameModeSystemTickTimer += dt;
			if (this._gameModeSystemTickTimer >= 0.25f)
			{
				this._gameModeSystemTickTimer -= 0.25f;
				this.CanGameModeSystemsTickThisFrame = true;
			}
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0008A738 File Offset: 0x00088938
		public Dictionary<string, string> GetUsedCosmeticsFromPeer(MissionPeer missionPeer, BasicCharacterObject selectedTroopCharacter)
		{
			if (missionPeer.Peer.UsedCosmetics != null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> objectTypeList = MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>();
				int num = -1;
				for (int i = 0; i < objectTypeList.Count; i++)
				{
					if (objectTypeList[i].HeroCharacter == selectedTroopCharacter || objectTypeList[i].TroopCharacter == selectedTroopCharacter)
					{
						num = i;
						break;
					}
				}
				List<int> list;
				missionPeer.Peer.UsedCosmetics.TryGetValue(num, out list);
				if (list != null)
				{
					foreach (int index in list)
					{
						ClothingCosmeticElement clothingCosmeticElement;
						if ((clothingCosmeticElement = (CosmeticsManager.CosmeticElementsList[index] as ClothingCosmeticElement)) != null)
						{
							foreach (string key in clothingCosmeticElement.ReplaceItemsId)
							{
								dictionary.Add(key, CosmeticsManager.CosmeticElementsList[index].Id);
							}
							foreach (Tuple<string, string> tuple in clothingCosmeticElement.ReplaceItemless)
							{
								if (tuple.Item1 == objectTypeList[num].StringId)
								{
									dictionary.Add(tuple.Item2, CosmeticsManager.CosmeticElementsList[index].Id);
									break;
								}
							}
						}
					}
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0008A8EC File Offset: 0x00088AEC
		public void AddCosmeticItemsToEquipment(Equipment equipment, Dictionary<string, string> choosenCosmetics)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ArmorItemEndSlot; equipmentIndex++)
			{
				if (equipment[equipmentIndex].Item == null)
				{
					string key = equipmentIndex.ToString();
					switch (equipmentIndex)
					{
					case EquipmentIndex.NumAllWeaponSlots:
						key = "Head";
						break;
					case EquipmentIndex.Body:
						key = "Body";
						break;
					case EquipmentIndex.Leg:
						key = "Leg";
						break;
					case EquipmentIndex.Gloves:
						key = "Gloves";
						break;
					case EquipmentIndex.Cape:
						key = "Cape";
						break;
					}
					string text = null;
					if (choosenCosmetics != null)
					{
						choosenCosmetics.TryGetValue(key, out text);
					}
					if (text != null)
					{
						ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>(text);
						EquipmentElement value = equipment[equipmentIndex];
						value.CosmeticItem = @object;
						equipment[equipmentIndex] = value;
					}
				}
				else
				{
					string stringId = equipment[equipmentIndex].Item.StringId;
					string text2 = null;
					if (choosenCosmetics != null)
					{
						choosenCosmetics.TryGetValue(stringId, out text2);
					}
					if (text2 != null)
					{
						ItemObject object2 = MBObjectManager.Instance.GetObject<ItemObject>(text2);
						EquipmentElement value2 = equipment[equipmentIndex];
						value2.CosmeticItem = object2;
						equipment[equipmentIndex] = value2;
					}
				}
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0008AA04 File Offset: 0x00088C04
		public bool IsClassAvailable(MultiplayerClassDivisions.MPHeroClass heroClass)
		{
			FormationClass formationClass;
			if (Enum.TryParse<FormationClass>(heroClass.ClassGroup.StringId, out formationClass))
			{
				return this.MissionLobbyComponent.IsClassAvailable(formationClass);
			}
			Debug.FailedAssert("\"" + heroClass.ClassGroup.StringId + "\" does not match with any FormationClass.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameModeLogics\\ServerGameModeLogics\\MissionMultiplayerGameModeBase.cs", "IsClassAvailable", 388);
			return false;
		}

		// Token: 0x04000D85 RID: 3461
		public const int GoldCap = 2000;

		// Token: 0x04000D86 RID: 3462
		public const float PerkTickPeriod = 1f;

		// Token: 0x04000D87 RID: 3463
		public const float GameModeSystemTickPeriod = 0.25f;

		// Token: 0x04000D88 RID: 3464
		private float _lastPerkTickTime;

		// Token: 0x04000D8A RID: 3466
		private MultiplayerMissionAgentVisualSpawnComponent _agentVisualSpawnComponent;

		// Token: 0x04000D8B RID: 3467
		public MultiplayerTeamSelectComponent MultiplayerTeamSelectComponent;

		// Token: 0x04000D8C RID: 3468
		protected MissionLobbyComponent MissionLobbyComponent;

		// Token: 0x04000D8D RID: 3469
		protected MultiplayerGameNotificationsComponent NotificationsComponent;

		// Token: 0x04000D8E RID: 3470
		public MultiplayerRoundController RoundController;

		// Token: 0x04000D8F RID: 3471
		public MultiplayerWarmupComponent WarmupComponent;

		// Token: 0x04000D90 RID: 3472
		public MultiplayerTimerComponent TimerComponent;

		// Token: 0x04000D91 RID: 3473
		protected MissionMultiplayerGameModeBaseClient GameModeBaseClient;

		// Token: 0x04000D93 RID: 3475
		private float _gameModeSystemTickTimer;
	}
}
