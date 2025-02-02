using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C2 RID: 706
	public class SiegeSpawningBehavior : SpawningBehaviorBase
	{
		// Token: 0x060026C4 RID: 9924 RVA: 0x000932D4 File Offset: 0x000914D4
		public override void Initialize(SpawnComponent spawnComponent)
		{
			base.Initialize(spawnComponent);
			base.OnAllAgentsFromPeerSpawnedFromVisuals += this.OnAllAgentsFromPeerSpawnedFromVisuals;
			if (this.GameMode.WarmupComponent == null)
			{
				this.RequestStartSpawnSession();
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x00093302 File Offset: 0x00091502
		public override void Clear()
		{
			base.Clear();
			base.OnAllAgentsFromPeerSpawnedFromVisuals -= this.OnAllAgentsFromPeerSpawnedFromVisuals;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0009331C File Offset: 0x0009151C
		public override void OnTick(float dt)
		{
			if (this.IsSpawningEnabled && this._spawnCheckTimer.Check(base.Mission.CurrentTime))
			{
				this.SpawnAgents();
			}
			base.OnTick(dt);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0009334C File Offset: 0x0009154C
		protected override void SpawnAgents()
		{
			BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			BasicCultureObject object2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.IsSynchronized)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && component.ControlledAgent == null && !component.HasSpawnedAgentVisuals && component.Team != null && component.Team != base.Mission.SpectatorTeam && component.TeamInitialPerkInfoReady && component.SpawnTimer.Check(base.Mission.CurrentTime))
					{
						BasicCultureObject basicCultureObject = (component.Team.Side == BattleSideEnum.Attacker) ? @object : object2;
						MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(component, false);
						if (mpheroClassForPeer == null || mpheroClassForPeer.TroopCasualCost > this.GameMode.GetCurrentGoldForPeer(component))
						{
							if (component.SelectedTroopIndex != 0)
							{
								component.SelectedTroopIndex = 0;
								GameNetwork.BeginBroadcastModuleEvent();
								GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(networkCommunicator, 0));
								GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, networkCommunicator);
							}
						}
						else
						{
							BasicCharacterObject heroCharacter = mpheroClassForPeer.HeroCharacter;
							Equipment equipment = heroCharacter.Equipment.Clone(false);
							MPPerkObject.MPOnSpawnPerkHandler onSpawnPerkHandler = MPPerkObject.GetOnSpawnPerkHandler(component);
							IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> enumerable = (onSpawnPerkHandler != null) ? onSpawnPerkHandler.GetAlternativeEquipments(true) : null;
							if (enumerable != null)
							{
								foreach (ValueTuple<EquipmentIndex, EquipmentElement> valueTuple in enumerable)
								{
									equipment[valueTuple.Item1] = valueTuple.Item2;
								}
							}
							AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).MissionPeer(component).Equipment(equipment).Team(component.Team).TroopOrigin(new BasicBattleAgentOrigin(heroCharacter)).IsFemale(component.Peer.IsFemale).BodyProperties(base.GetBodyProperties(component, (component.Team == base.Mission.AttackerTeam) ? @object : object2)).VisualsIndex(0).ClothingColor1((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color : basicCultureObject.ClothAlternativeColor).ClothingColor2((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color2 : basicCultureObject.ClothAlternativeColor2);
							if (this.GameMode.ShouldSpawnVisualsForServer(networkCommunicator))
							{
								base.AgentVisualSpawnComponent.SpawnAgentVisualsForPeer(component, agentBuildData, component.SelectedTroopIndex, false, 0);
								if (agentBuildData.AgentVisualsIndex == 0)
								{
									component.HasSpawnedAgentVisuals = true;
									component.EquipmentUpdatingExpired = false;
								}
							}
							this.GameMode.HandleAgentVisualSpawning(networkCommunicator, agentBuildData, 0, true);
						}
					}
				}
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00093650 File Offset: 0x00091850
		public override bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
		{
			return true;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00093654 File Offset: 0x00091854
		public override int GetMaximumReSpawnPeriodForPeer(MissionPeer peer)
		{
			if (this.GameMode.WarmupComponent != null && this.GameMode.WarmupComponent.IsInWarmup)
			{
				return 3;
			}
			if (peer.Team != null)
			{
				if (peer.Team.Side == BattleSideEnum.Attacker)
				{
					return MultiplayerOptions.OptionType.RespawnPeriodTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				}
				if (peer.Team.Side == BattleSideEnum.Defender)
				{
					return MultiplayerOptions.OptionType.RespawnPeriodTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				}
			}
			return -1;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000936B8 File Offset: 0x000918B8
		protected override bool IsRoundInProgress()
		{
			return Mission.Current.CurrentState == Mission.State.Continuing;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000936C8 File Offset: 0x000918C8
		private new void OnAllAgentsFromPeerSpawnedFromVisuals(MissionPeer peer)
		{
			bool flag = peer.Team == base.Mission.AttackerTeam;
			Team defenderTeam = base.Mission.DefenderTeam;
			MultiplayerClassDivisions.MPHeroClass mpheroClass = MultiplayerClassDivisions.GetMPHeroClasses(MBObjectManager.Instance.GetObject<BasicCultureObject>(flag ? MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))).ElementAt(peer.SelectedTroopIndex);
			this.GameMode.ChangeCurrentGoldForPeer(peer, this.GameMode.GetCurrentGoldForPeer(peer) - mpheroClass.TroopCasualCost);
		}
	}
}
