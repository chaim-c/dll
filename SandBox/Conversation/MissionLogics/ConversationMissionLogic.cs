using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Conversation.MissionLogics
{
	// Token: 0x0200009D RID: 157
	public class ConversationMissionLogic : MissionLogic
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0002B1AE File Offset: 0x000293AE
		private bool IsReadyForConversation
		{
			get
			{
				return this._isRenderingStarted && Agent.Main != null && Agent.Main.IsActive();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002B1CB File Offset: 0x000293CB
		public ConversationMissionLogic(ConversationCharacterData playerCharacterData, ConversationCharacterData otherCharacterData)
		{
			this._playerConversationData = playerCharacterData;
			this._otherSideConversationData = otherCharacterData;
			this._isCivilianEquipmentRequiredForLeader = otherCharacterData.IsCivilianEquipmentRequiredForLeader;
			this._isCivilianEquipmentRequiredForBodyGuards = otherCharacterData.IsCivilianEquipmentRequiredForBodyGuardCharacters;
			this._addBloodToAgents = new List<Agent>();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002B204 File Offset: 0x00029404
		public override void AfterStart()
		{
			base.AfterStart();
			this._realCameraController = base.Mission.CameraIsFirstPerson;
			base.Mission.CameraIsFirstPerson = true;
			IEnumerable<GameEntity> source = base.Mission.Scene.FindEntitiesWithTag("binary_conversation_point");
			if (source.Any<GameEntity>())
			{
				this._conversationSet = source.ToMBList<GameEntity>().GetRandomElement<GameEntity>();
			}
			this._usedSpawnPoints = new List<GameEntity>();
			BattleSideEnum battleSideEnum = BattleSideEnum.Attacker;
			if (PlayerSiege.PlayerSiegeEvent != null)
			{
				battleSideEnum = PlayerSiege.PlayerSide;
			}
			else if (PlayerEncounter.Current != null)
			{
				if (PlayerEncounter.InsideSettlement && PlayerEncounter.Current.OpponentSide != BattleSideEnum.Defender)
				{
					battleSideEnum = BattleSideEnum.Defender;
				}
				else
				{
					battleSideEnum = BattleSideEnum.Attacker;
				}
				if (PlayerEncounter.Current.EncounterSettlementAux != null && PlayerEncounter.Current.EncounterSettlementAux.MapFaction == Hero.MainHero.MapFaction)
				{
					if (PlayerEncounter.Current.EncounterSettlementAux.IsUnderSiege)
					{
						battleSideEnum = BattleSideEnum.Defender;
					}
					else
					{
						battleSideEnum = BattleSideEnum.Attacker;
					}
				}
			}
			base.Mission.PlayerTeam = base.Mission.Teams.Add(battleSideEnum, Hero.MainHero.MapFaction.Color, Hero.MainHero.MapFaction.Color2, null, true, false, true);
			bool flag = this._otherSideConversationData.Character.Equipment[10].Item != null && this._otherSideConversationData.Character.Equipment[10].Item.HasHorseComponent && battleSideEnum == BattleSideEnum.Defender;
			MatrixFrame matrixFrame;
			MatrixFrame initialFrame;
			if (this._conversationSet != null)
			{
				if (base.Mission.PlayerTeam.IsDefender)
				{
					matrixFrame = this.GetDefenderSideSpawnFrame();
					initialFrame = this.GetAttackerSideSpawnFrame(flag);
				}
				else
				{
					matrixFrame = this.GetAttackerSideSpawnFrame(flag);
					initialFrame = this.GetDefenderSideSpawnFrame();
				}
			}
			else
			{
				matrixFrame = this.GetPlayerSideSpawnFrameInSettlement();
				initialFrame = this.GetOtherSideSpawnFrameInSettlement(matrixFrame);
			}
			this.SpawnPlayer(this._playerConversationData, matrixFrame);
			this.SpawnOtherSide(this._otherSideConversationData, initialFrame, flag, !base.Mission.PlayerTeam.IsDefender);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002B3F0 File Offset: 0x000295F0
		private void SpawnPlayer(ConversationCharacterData playerConversationData, MatrixFrame initialFrame)
		{
			MatrixFrame initialFrame2 = new MatrixFrame(initialFrame.rotation, initialFrame.origin);
			initialFrame2.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			this.SpawnCharacter(CharacterObject.PlayerCharacter, playerConversationData, initialFrame2, ConversationMissionLogic.act_conversation_normal_loop);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0002B430 File Offset: 0x00029630
		private void SpawnOtherSide(ConversationCharacterData characterData, MatrixFrame initialFrame, bool spawnWithHorse, bool isDefenderSide)
		{
			MatrixFrame matrixFrame = new MatrixFrame(initialFrame.rotation, initialFrame.origin);
			if (Agent.Main != null)
			{
				matrixFrame.rotation.f = Agent.Main.Position - matrixFrame.origin;
			}
			matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(characterData.Character.Race, "_settlement");
			AgentBuildData agentBuildData = new AgentBuildData(characterData.Character).TroopOrigin(new SimpleAgentOrigin(characterData.Character, -1, null, default(UniqueTroopDescriptor))).Team(base.Mission.PlayerTeam).Monster(monsterWithSuffix).InitialPosition(matrixFrame.origin);
			Vec2 asVec = matrixFrame.rotation.f.AsVec2;
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(asVec).NoHorses(!spawnWithHorse).CivilianEquipment(this._isCivilianEquipmentRequiredForLeader);
			PartyBase party = characterData.Party;
			bool flag;
			if (party == null)
			{
				flag = (null != null);
			}
			else
			{
				Hero leaderHero = party.LeaderHero;
				flag = (((leaderHero != null) ? leaderHero.ClanBanner : null) != null);
			}
			if (flag)
			{
				agentBuildData2.Banner(characterData.Party.LeaderHero.ClanBanner);
			}
			else if (characterData.Party != null && characterData.Party.MapFaction != null)
			{
				AgentBuildData agentBuildData3 = agentBuildData2;
				PartyBase party2 = characterData.Party;
				Banner banner;
				if (party2 == null)
				{
					banner = null;
				}
				else
				{
					IFaction mapFaction = party2.MapFaction;
					banner = ((mapFaction != null) ? mapFaction.Banner : null);
				}
				agentBuildData3.Banner(banner);
			}
			if (spawnWithHorse)
			{
				agentBuildData2.MountKey(MountCreationKey.GetRandomMountKeyString(characterData.Character.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, characterData.Character.GetMountKeySeed()));
			}
			if (characterData.Party != null)
			{
				agentBuildData2.TroopOrigin(new PartyAgentOrigin(characterData.Party, characterData.Character, 0, new UniqueTroopDescriptor(FlattenedTroopRoster.GenerateUniqueNoFromParty(characterData.Party.MobileParty, 0)), false));
				agentBuildData2.ClothingColor1(characterData.Party.MapFaction.Color).ClothingColor2(characterData.Party.MapFaction.Color2);
			}
			Agent agent = base.Mission.SpawnAgent(agentBuildData2, false);
			if (characterData.SpawnedAfterFight)
			{
				this._addBloodToAgents.Add(agent);
			}
			if (agent.MountAgent == null)
			{
				agent.SetActionChannel(0, ConversationMissionLogic.act_conversation_normal_loop, false, 0UL, 0f, 1f, 0f, 0.4f, MBRandom.RandomFloat, false, -0.2f, 0, true);
			}
			agent.AgentVisuals.SetAgentLodZeroOrMax(true);
			this._curConversationPartnerAgent = agent;
			bool flag2 = characterData.Character.HeroObject != null && characterData.Character.HeroObject.IsPlayerCompanion;
			if (!characterData.NoBodyguards && !flag2)
			{
				this.SpawnBodyguards(isDefenderSide);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0002B6C4 File Offset: 0x000298C4
		private MatrixFrame GetDefenderSideSpawnFrame()
		{
			MatrixFrame result = MatrixFrame.Identity;
			foreach (GameEntity gameEntity in this._conversationSet.GetChildren())
			{
				if (gameEntity.HasTag("opponent_infantry_spawn"))
				{
					result = gameEntity.GetGlobalFrame();
					break;
				}
			}
			result.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return result;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002B738 File Offset: 0x00029938
		private MatrixFrame GetAttackerSideSpawnFrame(bool hasHorse)
		{
			MatrixFrame result = MatrixFrame.Identity;
			foreach (GameEntity gameEntity in this._conversationSet.GetChildren())
			{
				if (hasHorse && gameEntity.HasTag("player_cavalry_spawn"))
				{
					result = gameEntity.GetGlobalFrame();
					break;
				}
				if (gameEntity.HasTag("player_infantry_spawn"))
				{
					result = gameEntity.GetGlobalFrame();
					break;
				}
			}
			result.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return result;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002B7C8 File Offset: 0x000299C8
		private MatrixFrame GetPlayerSideSpawnFrameInSettlement()
		{
			GameEntity gameEntity;
			if ((gameEntity = base.Mission.Scene.FindEntityWithTag("spawnpoint_player")) == null)
			{
				gameEntity = (base.Mission.Scene.FindEntitiesWithTag("sp_player_conversation").FirstOrDefault<GameEntity>() ?? base.Mission.Scene.FindEntityWithTag("spawnpoint_player_outside"));
			}
			GameEntity gameEntity2 = gameEntity;
			MatrixFrame result = (gameEntity2 != null) ? gameEntity2.GetFrame() : MatrixFrame.Identity;
			result.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return result;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002B840 File Offset: 0x00029A40
		private MatrixFrame GetOtherSideSpawnFrameInSettlement(MatrixFrame playerFrame)
		{
			MatrixFrame result = playerFrame;
			Vec3 v = new Vec3(playerFrame.rotation.f, -1f);
			v.Normalize();
			result.origin = playerFrame.origin + 4f * v;
			result.rotation.RotateAboutUp(3.1415927f);
			return result;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002B89D File Offset: 0x00029A9D
		public override void OnRenderingStarted()
		{
			this._isRenderingStarted = true;
			Debug.Print("\n ConversationMissionLogic::OnRenderingStarted\n", 0, Debug.DebugColor.Cyan, 64UL);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		private void InitializeAfterCreation(Agent conversationPartnerAgent, PartyBase conversationPartnerParty)
		{
			Campaign.Current.ConversationManager.SetupAndStartMapConversation((conversationPartnerParty != null) ? conversationPartnerParty.MobileParty : null, conversationPartnerAgent, Mission.Current.MainAgentServer);
			base.Mission.SetMissionMode(MissionMode.Conversation, true);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0002B8EC File Offset: 0x00029AEC
		public override void OnMissionTick(float dt)
		{
			if (this._addBloodToAgents.Count > 0)
			{
				foreach (Agent agent in this._addBloodToAgents)
				{
					ValueTuple<sbyte, sbyte> randomPairOfRealBloodBurstBoneIndices = agent.GetRandomPairOfRealBloodBurstBoneIndices();
					if (randomPairOfRealBloodBurstBoneIndices.Item1 != -1 && randomPairOfRealBloodBurstBoneIndices.Item2 != -1)
					{
						agent.CreateBloodBurstAtLimb(randomPairOfRealBloodBurstBoneIndices.Item1, 0.1f + MBRandom.RandomFloat * 0.1f);
						agent.CreateBloodBurstAtLimb(randomPairOfRealBloodBurstBoneIndices.Item2, 0.2f + MBRandom.RandomFloat * 0.2f);
					}
				}
				this._addBloodToAgents.Clear();
			}
			if (!this._conversationStarted)
			{
				if (!this.IsReadyForConversation)
				{
					return;
				}
				this.InitializeAfterCreation(this._curConversationPartnerAgent, this._otherSideConversationData.Party);
				this._conversationStarted = true;
			}
			if (base.Mission.InputManager.IsGameKeyPressed(4))
			{
				Campaign.Current.ConversationManager.EndConversation();
			}
			if (!Campaign.Current.ConversationManager.IsConversationInProgress)
			{
				base.Mission.EndMission();
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002BA18 File Offset: 0x00029C18
		private void SpawnBodyguards(bool isDefenderSide)
		{
			int num = 2;
			ConversationCharacterData otherSideConversationData = this._otherSideConversationData;
			if (otherSideConversationData.Party == null)
			{
				return;
			}
			TroopRoster memberRoster = otherSideConversationData.Party.MemberRoster;
			int num2 = memberRoster.TotalManCount;
			if (memberRoster.Contains(CharacterObject.PlayerCharacter))
			{
				num2--;
			}
			if (num2 < num + 1)
			{
				return;
			}
			List<CharacterObject> list = new List<CharacterObject>();
			using (List<TroopRosterElement>.Enumerator enumerator = memberRoster.GetTroopRoster().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TroopRosterElement troopRosterElement = enumerator.Current;
					if (troopRosterElement.Character.IsHero && otherSideConversationData.Character != troopRosterElement.Character && !list.Contains(troopRosterElement.Character) && troopRosterElement.Character.HeroObject.IsWounded && !troopRosterElement.Character.IsPlayerCharacter)
					{
						list.Add(troopRosterElement.Character);
					}
				}
				goto IL_16B;
			}
			IL_D4:
			foreach (TroopRosterElement troopRosterElement2 in from k in memberRoster.GetTroopRoster()
			orderby k.Character.Level descending
			select k)
			{
				if ((!otherSideConversationData.Character.IsHero || otherSideConversationData.Character != troopRosterElement2.Character) && !troopRosterElement2.Character.IsPlayerCharacter)
				{
					list.Add(troopRosterElement2.Character);
				}
				if (list.Count == num)
				{
					break;
				}
			}
			IL_16B:
			if (list.Count >= num)
			{
				List<ActionIndexCache> list2 = new List<ActionIndexCache>
				{
					ActionIndexCache.Create("act_stand_1"),
					ActionIndexCache.Create("act_inventory_idle_start"),
					ActionIndexCache.Create("act_inventory_idle"),
					ConversationMissionLogic.act_conversation_normal_loop,
					ActionIndexCache.Create("act_conversation_warrior_loop"),
					ActionIndexCache.Create("act_conversation_hip_loop"),
					ActionIndexCache.Create("act_conversation_closed_loop"),
					ActionIndexCache.Create("act_conversation_demure_loop")
				};
				for (int i = 0; i < num; i++)
				{
					int index = new Random().Next(0, list.Count);
					int index2 = MBRandom.RandomInt(0, list2.Count);
					this.SpawnCharacter(list[index], otherSideConversationData, this.GetBodyguardSpawnFrame(list[index].HasMount(), isDefenderSide), list2[index2]);
					list2.RemoveAt(index2);
					list.RemoveAt(index);
				}
				return;
			}
			goto IL_D4;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0002BCB0 File Offset: 0x00029EB0
		private void SpawnCharacter(CharacterObject character, ConversationCharacterData characterData, MatrixFrame initialFrame, ActionIndexCache conversationAction)
		{
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(character.Race, "_settlement");
			AgentBuildData agentBuildData = new AgentBuildData(character).TroopOrigin(new SimpleAgentOrigin(character, -1, null, default(UniqueTroopDescriptor))).Team(base.Mission.PlayerTeam).Monster(monsterWithSuffix).InitialPosition(initialFrame.origin);
			Vec2 vec = initialFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).NoHorses(character.HasMount()).NoWeapons(characterData.NoWeapon).CivilianEquipment((character == CharacterObject.PlayerCharacter) ? this._isCivilianEquipmentRequiredForLeader : this._isCivilianEquipmentRequiredForBodyGuards);
			PartyBase party = characterData.Party;
			bool flag;
			if (party == null)
			{
				flag = (null != null);
			}
			else
			{
				Hero leaderHero = party.LeaderHero;
				flag = (((leaderHero != null) ? leaderHero.ClanBanner : null) != null);
			}
			if (flag)
			{
				agentBuildData2.Banner(characterData.Party.LeaderHero.ClanBanner);
			}
			else if (characterData.Party != null)
			{
				PartyBase party2 = characterData.Party;
				if (((party2 != null) ? party2.MapFaction : null) != null)
				{
					agentBuildData2.Banner(characterData.Party.MapFaction.Banner);
				}
			}
			if (characterData.Party != null)
			{
				agentBuildData2.ClothingColor1(characterData.Party.MapFaction.Color).ClothingColor2(characterData.Party.MapFaction.Color2);
			}
			if (characterData.Character == CharacterObject.PlayerCharacter)
			{
				agentBuildData2.Controller(Agent.ControllerType.Player);
			}
			Agent agent = base.Mission.SpawnAgent(agentBuildData2, false);
			agent.AgentVisuals.SetAgentLodZeroOrMax(true);
			agent.SetLookAgent(Agent.Main);
			AnimationSystemData animationSystemData = agentBuildData2.AgentMonster.FillAnimationSystemData(MBGlobals.GetActionSetWithSuffix(agentBuildData2.AgentMonster, agentBuildData2.AgentIsFemale, "_poses"), character.GetStepSize(), false);
			agent.SetActionSet(ref animationSystemData);
			if (characterData.Character == CharacterObject.PlayerCharacter)
			{
				agent.AgentVisuals.GetSkeleton().TickAnimationsAndForceUpdate(0.1f, initialFrame, true);
			}
			if (characterData.SpawnedAfterFight)
			{
				this._addBloodToAgents.Add(agent);
				return;
			}
			if (agent.MountAgent == null)
			{
				agent.SetActionChannel(0, conversationAction, false, 0UL, 0f, 1f, 0f, 0.4f, MBRandom.RandomFloat * 0.8f, false, -0.2f, 0, true);
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0002BEE4 File Offset: 0x0002A0E4
		private MatrixFrame GetBodyguardSpawnFrame(bool spawnWithHorse, bool isDefenderSide)
		{
			MatrixFrame result = MatrixFrame.Identity;
			foreach (GameEntity gameEntity in this._conversationSet.GetChildren())
			{
				if (!isDefenderSide)
				{
					if (spawnWithHorse && gameEntity.HasTag("player_bodyguard_cavalry_spawn") && !this._usedSpawnPoints.Contains(gameEntity))
					{
						this._usedSpawnPoints.Add(gameEntity);
						result = gameEntity.GetGlobalFrame();
						break;
					}
					if (gameEntity.HasTag("player_bodyguard_infantry_spawn") && !this._usedSpawnPoints.Contains(gameEntity))
					{
						this._usedSpawnPoints.Add(gameEntity);
						result = gameEntity.GetGlobalFrame();
						break;
					}
				}
				else
				{
					if (spawnWithHorse && gameEntity.HasTag("opponent_bodyguard_cavalry_spawn") && !this._usedSpawnPoints.Contains(gameEntity))
					{
						this._usedSpawnPoints.Add(gameEntity);
						result = gameEntity.GetGlobalFrame();
						break;
					}
					if (gameEntity.HasTag("opponent_bodyguard_infantry_spawn") && !this._usedSpawnPoints.Contains(gameEntity))
					{
						this._usedSpawnPoints.Add(gameEntity);
						result = gameEntity.GetGlobalFrame();
						break;
					}
				}
			}
			result.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002C018 File Offset: 0x0002A218
		protected override void OnEndMission()
		{
			this._conversationSet = null;
			base.Mission.CameraIsFirstPerson = this._realCameraController;
		}

		// Token: 0x040002A7 RID: 679
		private static readonly ActionIndexCache act_conversation_normal_loop = ActionIndexCache.Create("act_conversation_normal_loop");

		// Token: 0x040002A8 RID: 680
		private ConversationCharacterData _otherSideConversationData;

		// Token: 0x040002A9 RID: 681
		private ConversationCharacterData _playerConversationData;

		// Token: 0x040002AA RID: 682
		private readonly List<Agent> _addBloodToAgents;

		// Token: 0x040002AB RID: 683
		private Agent _curConversationPartnerAgent;

		// Token: 0x040002AC RID: 684
		private bool _isRenderingStarted;

		// Token: 0x040002AD RID: 685
		private bool _conversationStarted;

		// Token: 0x040002AE RID: 686
		private bool _isCivilianEquipmentRequiredForLeader;

		// Token: 0x040002AF RID: 687
		private bool _isCivilianEquipmentRequiredForBodyGuards;

		// Token: 0x040002B0 RID: 688
		private List<GameEntity> _usedSpawnPoints;

		// Token: 0x040002B1 RID: 689
		private GameEntity _conversationSet;

		// Token: 0x040002B2 RID: 690
		private bool _realCameraController;
	}
}
