using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C4 RID: 708
	public class WarmupSpawningBehavior : SpawningBehaviorBase
	{
		// Token: 0x060026EC RID: 9964 RVA: 0x00094B00 File Offset: 0x00092D00
		public WarmupSpawningBehavior()
		{
			this.IsSpawningEnabled = true;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00094B0F File Offset: 0x00092D0F
		public override void OnTick(float dt)
		{
			if (this.IsSpawningEnabled && this._spawnCheckTimer.Check(base.Mission.CurrentTime))
			{
				this.SpawnAgents();
			}
			base.OnTick(dt);
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00094B40 File Offset: 0x00092D40
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
						IAgentVisual agentVisualForPeer = component.GetAgentVisualForPeer(0);
						BasicCultureObject basicCultureObject = (component.Team.Side == BattleSideEnum.Attacker) ? @object : object2;
						int num = component.SelectedTroopIndex;
						IEnumerable<MultiplayerClassDivisions.MPHeroClass> mpheroClasses = MultiplayerClassDivisions.GetMPHeroClasses(basicCultureObject);
						MultiplayerClassDivisions.MPHeroClass mpheroClass = (num < 0) ? null : mpheroClasses.ElementAt(num);
						if (mpheroClass == null && num < 0)
						{
							mpheroClass = mpheroClasses.First<MultiplayerClassDivisions.MPHeroClass>();
							num = 0;
						}
						BasicCharacterObject heroCharacter = mpheroClass.HeroCharacter;
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
						MatrixFrame matrixFrame;
						if (agentVisualForPeer == null)
						{
							matrixFrame = this.SpawnComponent.GetSpawnFrame(component.Team, heroCharacter.Equipment.Horse.Item != null, false);
						}
						else
						{
							matrixFrame = agentVisualForPeer.GetFrame();
							matrixFrame.rotation.MakeUnit();
						}
						AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).MissionPeer(component).Equipment(equipment).Team(component.Team).TroopOrigin(new BasicBattleAgentOrigin(heroCharacter)).InitialPosition(matrixFrame.origin);
						Vec2 vec = matrixFrame.rotation.f.AsVec2;
						vec = vec.Normalized();
						AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).IsFemale(component.Peer.IsFemale).BodyProperties(base.GetBodyProperties(component, basicCultureObject)).VisualsIndex(0).ClothingColor1((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color : basicCultureObject.ClothAlternativeColor).ClothingColor2((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color2 : basicCultureObject.ClothAlternativeColor2);
						if (this.GameMode.ShouldSpawnVisualsForServer(networkCommunicator))
						{
							base.AgentVisualSpawnComponent.SpawnAgentVisualsForPeer(component, agentBuildData2, num, false, 0);
							if (agentBuildData2.AgentVisualsIndex == 0)
							{
								component.HasSpawnedAgentVisuals = true;
								component.EquipmentUpdatingExpired = false;
							}
						}
						this.GameMode.HandleAgentVisualSpawning(networkCommunicator, agentBuildData2, 0, true);
					}
				}
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00094E8C File Offset: 0x0009308C
		public override bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
		{
			return true;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00094E8F File Offset: 0x0009308F
		public override int GetMaximumReSpawnPeriodForPeer(MissionPeer peer)
		{
			return 3;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00094E92 File Offset: 0x00093092
		protected override bool IsRoundInProgress()
		{
			return Mission.Current.CurrentState == Mission.State.Continuing;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00094EA1 File Offset: 0x000930A1
		public override void Clear()
		{
			base.Clear();
			base.RequestStopSpawnSession();
		}
	}
}
