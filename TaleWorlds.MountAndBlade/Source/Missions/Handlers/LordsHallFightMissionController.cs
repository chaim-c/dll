using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003B9 RID: 953
	public class LordsHallFightMissionController : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x060032E9 RID: 13033 RVA: 0x000D36B4 File Offset: 0x000D18B4
		public LordsHallFightMissionController(IMissionTroopSupplier[] suppliers, float areaLostRatio, float attackerDefenderTroopCountRatio, int attackerSideTroopCountMax, int defenderSideTroopCountMax, BattleSideEnum playerSide)
		{
			this._areaLostRatio = areaLostRatio;
			this._attackerDefenderTroopCountRatio = attackerDefenderTroopCountRatio;
			this._attackerSideTroopCountMax = attackerSideTroopCountMax;
			this._defenderSideTroopCountMax = defenderSideTroopCountMax;
			this._missionSides = new LordsHallFightMissionController.MissionSide[2];
			for (int i = 0; i < 2; i++)
			{
				IMissionTroopSupplier troopSupplier = suppliers[i];
				bool isPlayerSide = i == (int)playerSide;
				this._missionSides[i] = new LordsHallFightMissionController.MissionSide((BattleSideEnum)i, troopSupplier, isPlayerSide);
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000D3717 File Offset: 0x000D1917
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.Mission.GetAgentTroopClass_Override += this.GetLordsHallFightTroopClass;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000D3736 File Offset: 0x000D1936
		public override void OnMissionStateFinalized()
		{
			base.OnMissionStateFinalized();
			base.Mission.GetAgentTroopClass_Override -= this.GetLordsHallFightTroopClass;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000D3755 File Offset: 0x000D1955
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = false;
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000D376C File Offset: 0x000D196C
		public override void OnMissionTick(float dt)
		{
			if (!this._isMissionInitialized)
			{
				this.InitializeMission();
				this._isMissionInitialized = true;
				return;
			}
			if (!this._troopsInitialized)
			{
				this._troopsInitialized = true;
			}
			if (this._setChargeOrderNextFrame)
			{
				if (base.Mission.PlayerTeam.ActiveAgents.Count > 0)
				{
					base.Mission.PlayerTeam.PlayerOrderController.SelectAllFormations(false);
					base.Mission.PlayerTeam.PlayerOrderController.SetOrder(OrderType.Charge);
				}
				this._setChargeOrderNextFrame = false;
			}
			this.CheckForReinforcement();
			this.CheckIfAnyAreaIsLostByDefender();
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000D37FD File Offset: 0x000D19FD
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (!affectedAgent.Team.IsDefender)
			{
				this._setChargeOrderNextFrame = affectedAgent.IsMainAgent;
				this._spawnReinforcements = true;
				return;
			}
			Tuple<int, LordsHallFightMissionController.AreaEntityData> tuple = this.FindAgentMachine(affectedAgent);
			if (tuple == null)
			{
				return;
			}
			tuple.Item2.StopUse();
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000D3838 File Offset: 0x000D1A38
		private Tuple<int, LordsHallFightMissionController.AreaEntityData> FindAgentMachine(Agent agent)
		{
			Tuple<int, LordsHallFightMissionController.AreaEntityData> tuple = null;
			foreach (KeyValuePair<int, Dictionary<int, LordsHallFightMissionController.AreaData>> keyValuePair in this._dividedAreaDictionary)
			{
				if (tuple != null)
				{
					break;
				}
				foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair2 in keyValuePair.Value)
				{
					LordsHallFightMissionController.AreaEntityData areaEntityData = keyValuePair2.Value.FindAgentMachine(agent);
					if (areaEntityData != null)
					{
						tuple = new Tuple<int, LordsHallFightMissionController.AreaEntityData>(keyValuePair.Key, areaEntityData);
						break;
					}
				}
			}
			return tuple;
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000D38F0 File Offset: 0x000D1AF0
		private void InitializeMission()
		{
			this._areaIndexList = new List<int>();
			this._dividedAreaDictionary = new Dictionary<int, Dictionary<int, LordsHallFightMissionController.AreaData>>();
			IEnumerable<FightAreaMarker> enumerable = from area in base.Mission.ActiveMissionObjects.FindAllWithType<FightAreaMarker>()
			orderby area.AreaIndex
			select area;
			base.Mission.MakeDefaultDeploymentPlans();
			foreach (FightAreaMarker fightAreaMarker in enumerable)
			{
				if (!this._dividedAreaDictionary.ContainsKey(fightAreaMarker.AreaIndex))
				{
					this._dividedAreaDictionary.Add(fightAreaMarker.AreaIndex, new Dictionary<int, LordsHallFightMissionController.AreaData>());
				}
				if (!this._dividedAreaDictionary[fightAreaMarker.AreaIndex].ContainsKey(fightAreaMarker.SubAreaIndex))
				{
					this._dividedAreaDictionary[fightAreaMarker.AreaIndex].Add(fightAreaMarker.SubAreaIndex, new LordsHallFightMissionController.AreaData(new List<FightAreaMarker>
					{
						fightAreaMarker
					}));
				}
				else
				{
					this._dividedAreaDictionary[fightAreaMarker.AreaIndex][fightAreaMarker.SubAreaIndex].AddAreaMarker(fightAreaMarker);
				}
			}
			this._areaIndexList = this._dividedAreaDictionary.Keys.ToList<int>();
			this._missionSides[0].SpawnTroops(this._dividedAreaDictionary, this._defenderSideTroopCountMax);
			int numberOfActiveTroops = this._missionSides[0].NumberOfActiveTroops;
			this._defenderTeams = new Team[2];
			this._defenderTeams[0] = Mission.Current.DefenderTeam;
			this._defenderTeams[1] = Mission.Current.DefenderAllyTeam;
			int spawnCount = MathF.Max(1, MathF.Min(this._attackerSideTroopCountMax, MathF.Round((float)numberOfActiveTroops * this._attackerDefenderTroopCountRatio)));
			this._missionSides[1].SpawnTroops(spawnCount, false);
			bool flag = Mission.Current.AttackerTeam == Mission.Current.PlayerTeam || (Mission.Current.AttackerAllyTeam != null && Mission.Current.AttackerAllyTeam == Mission.Current.PlayerTeam);
			this._attackerTeams = new Team[2];
			this._attackerTeams[0] = Mission.Current.AttackerTeam;
			this._attackerTeams[1] = Mission.Current.AttackerAllyTeam;
			foreach (Team team in this._attackerTeams)
			{
				if (team != null)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						if (formation.CountOfUnits > 0)
						{
							formation.ArrangementOrder = ArrangementOrder.ArrangementOrderSquare;
							formation.FormOrder = FormOrder.FormOrderDeep;
						}
						formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
						formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
						if (flag)
						{
							formation.PlayerOwner = Mission.Current.MainAgent;
						}
					}
				}
			}
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000D3BF4 File Offset: 0x000D1DF4
		private void CheckForReinforcement()
		{
			if (this._spawnReinforcements)
			{
				this._missionSides[1].SpawnTroops(1, true);
				this._spawnReinforcements = false;
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000D3C14 File Offset: 0x000D1E14
		public void StartSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(true);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000D3C24 File Offset: 0x000D1E24
		public void StopSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(false);
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000D3C34 File Offset: 0x000D1E34
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return this._missionSides[(int)side].TroopSpawningActive;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000D3C43 File Offset: 0x000D1E43
		public float GetReinforcementInterval()
		{
			return 0f;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000D3C4A File Offset: 0x000D1E4A
		public bool IsSideDepleted(BattleSideEnum side)
		{
			return this._missionSides[(int)side].NumberOfActiveTroops == 0;
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000D3C5C File Offset: 0x000D1E5C
		private void CheckIfAnyAreaIsLostByDefender()
		{
			int num = -1;
			for (int i = 0; i < this._areaIndexList.Count; i++)
			{
				int num2 = this._areaIndexList[i];
				if (num2 > this._lastAreaLostByDefender && num < 0)
				{
					foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in this._dividedAreaDictionary[num2])
					{
						if (this.IsAreaLostByDefender(keyValuePair.Value))
						{
							num = num2;
							break;
						}
					}
				}
			}
			if (num > 0)
			{
				this.OnAreaLost(num);
			}
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000D3D00 File Offset: 0x000D1F00
		private void OnAreaLost(int areaIndex)
		{
			int num = MathF.Min(this._areaIndexList.IndexOf(areaIndex) + 1, this._areaIndexList.Count - 1);
			for (int i = MathF.Max(0, this._areaIndexList.IndexOf(this._lastAreaLostByDefender)); i < num; i++)
			{
				int key = this._areaIndexList[i];
				foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in this._dividedAreaDictionary[key])
				{
					this.StartAreaPullBack(keyValuePair.Value, this._areaIndexList[num]);
				}
			}
			this._lastAreaLostByDefender = areaIndex;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000D3DC4 File Offset: 0x000D1FC4
		private void StartAreaPullBack(LordsHallFightMissionController.AreaData areaData, int nextAreaIndex)
		{
			foreach (LordsHallFightMissionController.AreaEntityData areaEntityData in areaData.ArcherUsablePoints)
			{
				if (areaEntityData.InUse)
				{
					Agent userAgent = areaEntityData.UserAgent;
					areaEntityData.StopUse();
					LordsHallFightMissionController.AreaEntityData areaEntityData2 = this.FindPosition(nextAreaIndex, true);
					if (areaEntityData2 != null)
					{
						areaEntityData2.AssignAgent(userAgent);
					}
				}
			}
			foreach (LordsHallFightMissionController.AreaEntityData areaEntityData3 in areaData.InfantryUsablePoints)
			{
				if (areaEntityData3.InUse)
				{
					Agent userAgent2 = areaEntityData3.UserAgent;
					areaEntityData3.StopUse();
					LordsHallFightMissionController.AreaEntityData areaEntityData4 = this.FindPosition(nextAreaIndex, false);
					if (areaEntityData4 != null)
					{
						areaEntityData4.AssignAgent(userAgent2);
					}
				}
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000D3E94 File Offset: 0x000D2094
		private LordsHallFightMissionController.AreaEntityData FindPosition(int nextAreaIndex, bool isArcher)
		{
			int num = this.SelectBestSubArea(nextAreaIndex, isArcher);
			if (num < 0)
			{
				isArcher = !isArcher;
				num = this.SelectBestSubArea(nextAreaIndex, isArcher);
			}
			return this._dividedAreaDictionary[nextAreaIndex][num].GetAvailableMachines(isArcher).GetRandomElementInefficiently<LordsHallFightMissionController.AreaEntityData>();
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000D3EDC File Offset: 0x000D20DC
		private int SelectBestSubArea(int areaIndex, bool isArcher)
		{
			int result = -1;
			float num = 0f;
			foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in this._dividedAreaDictionary[areaIndex])
			{
				float areaAvailabilityRatio = this.GetAreaAvailabilityRatio(keyValuePair.Value, isArcher);
				if (areaAvailabilityRatio > num)
				{
					num = areaAvailabilityRatio;
					result = keyValuePair.Key;
				}
			}
			return result;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000D3F58 File Offset: 0x000D2158
		private float GetAreaAvailabilityRatio(LordsHallFightMissionController.AreaData areaData, bool isArcher)
		{
			int num = isArcher ? areaData.ArcherUsablePoints.Count<LordsHallFightMissionController.AreaEntityData>() : areaData.InfantryUsablePoints.Count<LordsHallFightMissionController.AreaEntityData>();
			int num2;
			if (!isArcher)
			{
				num2 = areaData.InfantryUsablePoints.Count((LordsHallFightMissionController.AreaEntityData x) => !x.InUse);
			}
			else
			{
				num2 = areaData.ArcherUsablePoints.Count((LordsHallFightMissionController.AreaEntityData x) => !x.InUse);
			}
			int num3 = num2;
			if (num != 0)
			{
				return (float)num3 / (float)num;
			}
			return 0f;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000D3FEC File Offset: 0x000D21EC
		private bool IsAreaLostByDefender(LordsHallFightMissionController.AreaData areaData)
		{
			int num = 0;
			foreach (Team team in this._defenderTeams)
			{
				if (team != null)
				{
					foreach (Agent agent in team.ActiveAgents)
					{
						if (this.IsAgentInArea(agent, areaData))
						{
							num++;
						}
					}
				}
			}
			int num2 = MathF.Round((float)num * this._areaLostRatio);
			bool flag = num2 == 0;
			if (!flag)
			{
				foreach (Team team2 in this._attackerTeams)
				{
					if (team2 != null)
					{
						foreach (Agent agent2 in team2.ActiveAgents)
						{
							if (this.IsAgentInArea(agent2, areaData))
							{
								num2--;
								if (num2 == 0)
								{
									flag = true;
									break;
								}
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000D4104 File Offset: 0x000D2304
		private bool IsAgentInArea(Agent agent, LordsHallFightMissionController.AreaData areaData)
		{
			bool result = false;
			Vec3 position = agent.Position;
			using (IEnumerator<FightAreaMarker> enumerator = areaData.AreaList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsPositionInRange(position))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000D4160 File Offset: 0x000D2360
		private FormationClass GetLordsHallFightTroopClass(BattleSideEnum side, BasicCharacterObject agentCharacter)
		{
			return agentCharacter.GetFormationClass().DismountedClass();
		}

		// Token: 0x0400160E RID: 5646
		private readonly float _areaLostRatio;

		// Token: 0x0400160F RID: 5647
		private readonly float _attackerDefenderTroopCountRatio;

		// Token: 0x04001610 RID: 5648
		private readonly int _attackerSideTroopCountMax;

		// Token: 0x04001611 RID: 5649
		private readonly int _defenderSideTroopCountMax;

		// Token: 0x04001612 RID: 5650
		private readonly LordsHallFightMissionController.MissionSide[] _missionSides;

		// Token: 0x04001613 RID: 5651
		private Team[] _attackerTeams;

		// Token: 0x04001614 RID: 5652
		private Team[] _defenderTeams;

		// Token: 0x04001615 RID: 5653
		private Dictionary<int, Dictionary<int, LordsHallFightMissionController.AreaData>> _dividedAreaDictionary;

		// Token: 0x04001616 RID: 5654
		private List<int> _areaIndexList;

		// Token: 0x04001617 RID: 5655
		private int _lastAreaLostByDefender;

		// Token: 0x04001618 RID: 5656
		private bool _troopsInitialized;

		// Token: 0x04001619 RID: 5657
		private bool _isMissionInitialized;

		// Token: 0x0400161A RID: 5658
		private bool _spawnReinforcements;

		// Token: 0x0400161B RID: 5659
		private bool _setChargeOrderNextFrame;

		// Token: 0x0200065A RID: 1626
		private class MissionSide
		{
			// Token: 0x17000A2A RID: 2602
			// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000ED154 File Offset: 0x000EB354
			public bool TroopSpawningActive
			{
				get
				{
					return this._troopSpawningActive;
				}
			}

			// Token: 0x17000A2B RID: 2603
			// (get) Token: 0x06003D61 RID: 15713 RVA: 0x000ED15C File Offset: 0x000EB35C
			public int NumberOfActiveTroops
			{
				get
				{
					return this._numberOfSpawnedTroops - this._troopSupplier.NumRemovedTroops;
				}
			}

			// Token: 0x06003D62 RID: 15714 RVA: 0x000ED170 File Offset: 0x000EB370
			public MissionSide(BattleSideEnum side, IMissionTroopSupplier troopSupplier, bool isPlayerSide)
			{
				this._side = side;
				this._isPlayerSide = isPlayerSide;
				this._troopSupplier = troopSupplier;
			}

			// Token: 0x06003D63 RID: 15715 RVA: 0x000ED194 File Offset: 0x000EB394
			public void SpawnTroops(Dictionary<int, Dictionary<int, LordsHallFightMissionController.AreaData>> areaMarkerDictionary, int spawnCount)
			{
				List<IAgentOriginBase> list = this._troopSupplier.SupplyTroops(spawnCount).OrderByDescending(delegate(IAgentOriginBase x)
				{
					FormationClass agentTroopClass = Mission.Current.GetAgentTroopClass(this._side, x.Troop);
					if (agentTroopClass != FormationClass.Ranged && agentTroopClass != FormationClass.HorseArcher)
					{
						return 0;
					}
					return 1;
				}).ToList<IAgentOriginBase>();
				for (int i = 0; i < list.Count; i++)
				{
					IAgentOriginBase agentOriginBase = list[i];
					bool flag = Mission.Current.GetAgentTroopClass(this._side, agentOriginBase.Troop).IsRanged();
					List<KeyValuePair<int, LordsHallFightMissionController.AreaData>> list2 = areaMarkerDictionary.ElementAt(i % areaMarkerDictionary.Count).Value.ToList<KeyValuePair<int, LordsHallFightMissionController.AreaData>>();
					List<ValueTuple<KeyValuePair<int, LordsHallFightMissionController.AreaData>, float>> list3 = new List<ValueTuple<KeyValuePair<int, LordsHallFightMissionController.AreaData>, float>>();
					foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in list2)
					{
						int num = 1000 * keyValuePair.Value.GetAvailableMachines(flag).Count<LordsHallFightMissionController.AreaEntityData>() + keyValuePair.Value.GetAvailableMachines(!flag).Count<LordsHallFightMissionController.AreaEntityData>();
						list3.Add(new ValueTuple<KeyValuePair<int, LordsHallFightMissionController.AreaData>, float>(new KeyValuePair<int, LordsHallFightMissionController.AreaData>(keyValuePair.Key, keyValuePair.Value), (float)num));
					}
					KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair2 = MBRandom.ChooseWeighted<KeyValuePair<int, LordsHallFightMissionController.AreaData>>(list3);
					LordsHallFightMissionController.AreaEntityData areaEntityData = keyValuePair2.Value.GetAvailableMachines(flag).GetRandomElementInefficiently<LordsHallFightMissionController.AreaEntityData>() ?? keyValuePair2.Value.GetAvailableMachines(!flag).GetRandomElementInefficiently<LordsHallFightMissionController.AreaEntityData>();
					MatrixFrame globalFrame = areaEntityData.Entity.GetGlobalFrame();
					Agent agent = Mission.Current.SpawnTroop(agentOriginBase, false, false, false, false, 0, 0, false, false, false, new Vec3?(globalFrame.origin), new Vec2?(globalFrame.rotation.f.AsVec2.Normalized()), null, null, FormationClass.NumberOfAllFormations, false);
					this._numberOfSpawnedTroops++;
					AgentFlag agentFlags = agent.GetAgentFlags();
					agent.SetAgentFlags(agentFlags & ~AgentFlag.CanRetreat);
					agent.WieldInitialWeapons(Agent.WeaponWieldActionType.Instant, Equipment.InitialWeaponEquipPreference.Any);
					agent.SetWatchState(Agent.WatchState.Alarmed);
					agent.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefensiveArrangementMove);
					areaEntityData.AssignAgent(agent);
				}
			}

			// Token: 0x06003D64 RID: 15716 RVA: 0x000ED37C File Offset: 0x000EB57C
			public void SpawnTroops(int spawnCount, bool isReinforcement)
			{
				if (this._troopSpawningActive)
				{
					List<IAgentOriginBase> list = this._troopSupplier.SupplyTroops(spawnCount).ToList<IAgentOriginBase>();
					for (int i = 0; i < list.Count; i++)
					{
						if (BattleSideEnum.Attacker == this._side)
						{
							Mission.Current.SpawnTroop(list[i], this._isPlayerSide, true, false, isReinforcement, spawnCount, i, true, true, true, null, null, null, null, FormationClass.NumberOfAllFormations, false);
							this._numberOfSpawnedTroops++;
						}
					}
				}
			}

			// Token: 0x06003D65 RID: 15717 RVA: 0x000ED402 File Offset: 0x000EB602
			public void SetSpawnTroops(bool spawnTroops)
			{
				this._troopSpawningActive = spawnTroops;
			}

			// Token: 0x0400210F RID: 8463
			private readonly BattleSideEnum _side;

			// Token: 0x04002110 RID: 8464
			private readonly IMissionTroopSupplier _troopSupplier;

			// Token: 0x04002111 RID: 8465
			private readonly bool _isPlayerSide;

			// Token: 0x04002112 RID: 8466
			private bool _troopSpawningActive = true;

			// Token: 0x04002113 RID: 8467
			private int _numberOfSpawnedTroops;
		}

		// Token: 0x0200065B RID: 1627
		private class AreaData
		{
			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000ED43B File Offset: 0x000EB63B
			public IEnumerable<FightAreaMarker> AreaList
			{
				get
				{
					return this._areaList;
				}
			}

			// Token: 0x17000A2D RID: 2605
			// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000ED443 File Offset: 0x000EB643
			public IEnumerable<LordsHallFightMissionController.AreaEntityData> ArcherUsablePoints
			{
				get
				{
					return this._archerUsablePoints;
				}
			}

			// Token: 0x17000A2E RID: 2606
			// (get) Token: 0x06003D69 RID: 15721 RVA: 0x000ED44B File Offset: 0x000EB64B
			public IEnumerable<LordsHallFightMissionController.AreaEntityData> InfantryUsablePoints
			{
				get
				{
					return this._infantryUsablePoints;
				}
			}

			// Token: 0x06003D6A RID: 15722 RVA: 0x000ED454 File Offset: 0x000EB654
			public AreaData(List<FightAreaMarker> areaList)
			{
				this._areaList = new List<FightAreaMarker>();
				this._archerUsablePoints = new List<LordsHallFightMissionController.AreaEntityData>();
				this._infantryUsablePoints = new List<LordsHallFightMissionController.AreaEntityData>();
				foreach (FightAreaMarker marker in areaList)
				{
					this.AddAreaMarker(marker);
				}
			}

			// Token: 0x06003D6B RID: 15723 RVA: 0x000ED4CC File Offset: 0x000EB6CC
			public IEnumerable<LordsHallFightMissionController.AreaEntityData> GetAvailableMachines(bool isArcher)
			{
				List<LordsHallFightMissionController.AreaEntityData> list = isArcher ? this._archerUsablePoints : this._infantryUsablePoints;
				foreach (LordsHallFightMissionController.AreaEntityData areaEntityData in list)
				{
					if (!areaEntityData.InUse)
					{
						yield return areaEntityData;
					}
				}
				List<LordsHallFightMissionController.AreaEntityData>.Enumerator enumerator = default(List<LordsHallFightMissionController.AreaEntityData>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06003D6C RID: 15724 RVA: 0x000ED4E4 File Offset: 0x000EB6E4
			public void AddAreaMarker(FightAreaMarker marker)
			{
				this._areaList.Add(marker);
				using (List<GameEntity>.Enumerator enumerator = marker.GetGameEntitiesWithTagInRange("defender_archer").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameEntity entity = enumerator.Current;
						PathFaceRecord nullFaceRecord = PathFaceRecord.NullFaceRecord;
						Mission.Current.Scene.GetNavMeshFaceIndex(ref nullFaceRecord, entity.GetGlobalFrame().origin, true);
						if (nullFaceRecord.FaceIndex != -1 && this._archerUsablePoints.All((LordsHallFightMissionController.AreaEntityData x) => x.Entity != entity))
						{
							this._archerUsablePoints.Add(new LordsHallFightMissionController.AreaEntityData(entity));
						}
					}
				}
				using (List<GameEntity>.Enumerator enumerator = marker.GetGameEntitiesWithTagInRange("defender_infantry").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameEntity entity = enumerator.Current;
						if (this._infantryUsablePoints.All((LordsHallFightMissionController.AreaEntityData x) => x.Entity != entity))
						{
							this._infantryUsablePoints.Add(new LordsHallFightMissionController.AreaEntityData(entity));
						}
					}
				}
			}

			// Token: 0x06003D6D RID: 15725 RVA: 0x000ED630 File Offset: 0x000EB830
			public LordsHallFightMissionController.AreaEntityData FindAgentMachine(Agent agent)
			{
				return this._infantryUsablePoints.FirstOrDefault((LordsHallFightMissionController.AreaEntityData x) => x.UserAgent == agent) ?? this._archerUsablePoints.FirstOrDefault((LordsHallFightMissionController.AreaEntityData x) => x.UserAgent == agent);
			}

			// Token: 0x04002114 RID: 8468
			private const string ArcherSpawnPointTag = "defender_archer";

			// Token: 0x04002115 RID: 8469
			private const string InfantrySpawnPointTag = "defender_infantry";

			// Token: 0x04002116 RID: 8470
			private readonly List<FightAreaMarker> _areaList;

			// Token: 0x04002117 RID: 8471
			private readonly List<LordsHallFightMissionController.AreaEntityData> _archerUsablePoints;

			// Token: 0x04002118 RID: 8472
			private readonly List<LordsHallFightMissionController.AreaEntityData> _infantryUsablePoints;
		}

		// Token: 0x0200065C RID: 1628
		private class AreaEntityData
		{
			// Token: 0x17000A2F RID: 2607
			// (get) Token: 0x06003D6E RID: 15726 RVA: 0x000ED67C File Offset: 0x000EB87C
			// (set) Token: 0x06003D6F RID: 15727 RVA: 0x000ED684 File Offset: 0x000EB884
			public Agent UserAgent { get; private set; }

			// Token: 0x17000A30 RID: 2608
			// (get) Token: 0x06003D70 RID: 15728 RVA: 0x000ED68D File Offset: 0x000EB88D
			public bool InUse
			{
				get
				{
					return this.UserAgent != null;
				}
			}

			// Token: 0x06003D71 RID: 15729 RVA: 0x000ED698 File Offset: 0x000EB898
			public AreaEntityData(GameEntity entity)
			{
				this.Entity = entity;
			}

			// Token: 0x06003D72 RID: 15730 RVA: 0x000ED6A8 File Offset: 0x000EB8A8
			public void AssignAgent(Agent agent)
			{
				this.UserAgent = agent;
				MatrixFrame globalFrame = this.Entity.GetGlobalFrame();
				agent.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefaultMove);
				this.UserAgent.SetFormationFrameEnabled(new WorldPosition(agent.Mission.Scene, globalFrame.origin), globalFrame.rotation.f.AsVec2.Normalized(), Vec2.Zero, 0f);
			}

			// Token: 0x06003D73 RID: 15731 RVA: 0x000ED713 File Offset: 0x000EB913
			public void StopUse()
			{
				if (this.UserAgent.IsActive())
				{
					this.UserAgent.SetFormationFrameDisabled();
				}
				this.UserAgent = null;
			}

			// Token: 0x04002119 RID: 8473
			public readonly GameEntity Entity;
		}
	}
}
