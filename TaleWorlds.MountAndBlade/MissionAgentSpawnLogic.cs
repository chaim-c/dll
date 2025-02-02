using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000279 RID: 633
	public class MissionAgentSpawnLogic : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00078C19 File Offset: 0x00076E19
		public static int MaxNumberOfAgentsForMission
		{
			get
			{
				if (MissionAgentSpawnLogic._maxNumberOfAgentsForMissionCache == 0)
				{
					MissionAgentSpawnLogic._maxNumberOfAgentsForMissionCache = MBAPI.IMBAgent.GetMaximumNumberOfAgents();
				}
				return MissionAgentSpawnLogic._maxNumberOfAgentsForMissionCache;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00078C36 File Offset: 0x00076E36
		private static int MaxNumberOfTroopsForMission
		{
			get
			{
				return MissionAgentSpawnLogic.MaxNumberOfAgentsForMission / 2;
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002143 RID: 8515 RVA: 0x00078C40 File Offset: 0x00076E40
		// (remove) Token: 0x06002144 RID: 8516 RVA: 0x00078C78 File Offset: 0x00076E78
		public event Action<BattleSideEnum, int> OnReinforcementsSpawned;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002145 RID: 8517 RVA: 0x00078CB0 File Offset: 0x00076EB0
		// (remove) Token: 0x06002146 RID: 8518 RVA: 0x00078CE8 File Offset: 0x00076EE8
		public event Action<BattleSideEnum, int> OnInitialTroopsSpawned;

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x00078D1D File Offset: 0x00076F1D
		public int NumberOfAgents
		{
			get
			{
				return base.Mission.AllAgents.Count;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00078D2F File Offset: 0x00076F2F
		public int NumberOfRemainingTroops
		{
			get
			{
				MissionAgentSpawnLogic.SpawnPhase defenderActivePhase = this.DefenderActivePhase;
				int num = (defenderActivePhase != null) ? defenderActivePhase.RemainingSpawnNumber : 0;
				MissionAgentSpawnLogic.SpawnPhase attackerActivePhase = this.AttackerActivePhase;
				return num + ((attackerActivePhase != null) ? attackerActivePhase.RemainingSpawnNumber : 0);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x00078D56 File Offset: 0x00076F56
		public int NumberOfActiveDefenderTroops
		{
			get
			{
				MissionAgentSpawnLogic.SpawnPhase defenderActivePhase = this.DefenderActivePhase;
				if (defenderActivePhase == null)
				{
					return 0;
				}
				return defenderActivePhase.NumberActiveTroops;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x00078D69 File Offset: 0x00076F69
		public int NumberOfActiveAttackerTroops
		{
			get
			{
				MissionAgentSpawnLogic.SpawnPhase attackerActivePhase = this.AttackerActivePhase;
				if (attackerActivePhase == null)
				{
					return 0;
				}
				return attackerActivePhase.NumberActiveTroops;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x00078D7C File Offset: 0x00076F7C
		public int NumberOfRemainingDefenderTroops
		{
			get
			{
				MissionAgentSpawnLogic.SpawnPhase defenderActivePhase = this.DefenderActivePhase;
				if (defenderActivePhase == null)
				{
					return 0;
				}
				return defenderActivePhase.RemainingSpawnNumber;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x00078D8F File Offset: 0x00076F8F
		public int NumberOfRemainingAttackerTroops
		{
			get
			{
				MissionAgentSpawnLogic.SpawnPhase attackerActivePhase = this.AttackerActivePhase;
				if (attackerActivePhase == null)
				{
					return 0;
				}
				return attackerActivePhase.RemainingSpawnNumber;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x00078DA2 File Offset: 0x00076FA2
		public int BattleSize
		{
			get
			{
				return this._battleSize;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x00078DAA File Offset: 0x00076FAA
		public bool IsInitialSpawnOver
		{
			get
			{
				return this.DefenderActivePhase.InitialSpawnNumber + this.AttackerActivePhase.InitialSpawnNumber == 0;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00078DC6 File Offset: 0x00076FC6
		public bool IsDeploymentOver
		{
			get
			{
				return base.Mission.GetMissionBehavior<BattleDeploymentHandler>() == null && this.IsInitialSpawnOver;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00078DDD File Offset: 0x00076FDD
		public ref readonly MissionSpawnSettings ReinforcementSpawnSettings
		{
			get
			{
				return ref this._spawnSettings;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00078DE5 File Offset: 0x00076FE5
		private int TotalSpawnNumber
		{
			get
			{
				MissionAgentSpawnLogic.SpawnPhase defenderActivePhase = this.DefenderActivePhase;
				int num = (defenderActivePhase != null) ? defenderActivePhase.TotalSpawnNumber : 0;
				MissionAgentSpawnLogic.SpawnPhase attackerActivePhase = this.AttackerActivePhase;
				return num + ((attackerActivePhase != null) ? attackerActivePhase.TotalSpawnNumber : 0);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x00078E0C File Offset: 0x0007700C
		private MissionAgentSpawnLogic.SpawnPhase DefenderActivePhase
		{
			get
			{
				return this._phases[0].FirstOrDefault<MissionAgentSpawnLogic.SpawnPhase>();
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00078E1B File Offset: 0x0007701B
		private MissionAgentSpawnLogic.SpawnPhase AttackerActivePhase
		{
			get
			{
				return this._phases[1].FirstOrDefault<MissionAgentSpawnLogic.SpawnPhase>();
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00078E2C File Offset: 0x0007702C
		public override void AfterStart()
		{
			this._bannerBearerLogic = base.Mission.GetMissionBehavior<BannerBearerLogic>();
			if (this._bannerBearerLogic != null)
			{
				for (int i = 0; i < 2; i++)
				{
					this._missionSides[i].SetBannerBearerLogic(this._bannerBearerLogic);
				}
			}
			MissionGameModels.Current.BattleSpawnModel.OnMissionStart();
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00078E80 File Offset: 0x00077080
		public int GetNumberOfPlayerControllableTroops()
		{
			MissionAgentSpawnLogic.MissionSide playerSide = this._playerSide;
			if (playerSide == null)
			{
				return 0;
			}
			return playerSide.GetNumberOfPlayerControllableTroops();
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00078E93 File Offset: 0x00077093
		public void InitWithSinglePhase(int defenderTotalSpawn, int attackerTotalSpawn, int defenderInitialSpawn, int attackerInitialSpawn, bool spawnDefenders, bool spawnAttackers, in MissionSpawnSettings spawnSettings)
		{
			this.AddPhase(BattleSideEnum.Defender, defenderTotalSpawn, defenderInitialSpawn);
			this.AddPhase(BattleSideEnum.Attacker, attackerTotalSpawn, attackerInitialSpawn);
			this.Init(spawnDefenders, spawnAttackers, spawnSettings);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00078EB4 File Offset: 0x000770B4
		public IEnumerable<IAgentOriginBase> GetAllTroopsForSide(BattleSideEnum side)
		{
			return this._missionSides[(int)side].GetAllTroops();
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00078ED0 File Offset: 0x000770D0
		public override void OnMissionTick(float dt)
		{
			if (!GameNetwork.IsClient && !this.CheckDeployment())
			{
				return;
			}
			this.PhaseTick();
			if (this._reinforcementSpawnEnabled)
			{
				if (this._spawnSettings.ReinforcementTroopsTimingMethod == MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer)
				{
					this.CheckGlobalReinforcementBatch();
				}
				else if (this._spawnSettings.ReinforcementTroopsTimingMethod == MissionSpawnSettings.ReinforcementTimingMethod.CustomTimer)
				{
					this.CheckCustomReinforcementBatch();
				}
			}
			if (this._spawningReinforcements)
			{
				this.CheckReinforcementSpawn();
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00078F34 File Offset: 0x00077134
		public MissionAgentSpawnLogic(IMissionTroopSupplier[] suppliers, BattleSideEnum playerSide, Mission.BattleSizeType battleSizeType)
		{
			switch (battleSizeType)
			{
			case Mission.BattleSizeType.Battle:
				this._battleSize = BannerlordConfig.GetRealBattleSize();
				break;
			case Mission.BattleSizeType.Siege:
				this._battleSize = BannerlordConfig.GetRealBattleSizeForSiege();
				break;
			case Mission.BattleSizeType.SallyOut:
				this._battleSize = BannerlordConfig.GetRealBattleSizeForSallyOut();
				break;
			}
			this._battleSize = MathF.Min(this._battleSize, MissionAgentSpawnLogic.MaxNumberOfTroopsForMission);
			this._globalReinforcementSpawnTimer = new BasicMissionTimer();
			this._spawnSettings = MissionSpawnSettings.CreateDefaultSpawnSettings();
			this._globalReinforcementInterval = this._spawnSettings.GlobalReinforcementInterval;
			this._missionSides = new MissionAgentSpawnLogic.MissionSide[2];
			for (int i = 0; i < 2; i++)
			{
				IMissionTroopSupplier troopSupplier = suppliers[i];
				bool flag = i == (int)playerSide;
				MissionAgentSpawnLogic.MissionSide missionSide = new MissionAgentSpawnLogic.MissionSide(this, (BattleSideEnum)i, troopSupplier, flag);
				if (flag)
				{
					this._playerSide = missionSide;
				}
				this._missionSides[i] = missionSide;
			}
			this._numberOfTroopsInTotal = new int[2];
			this._formationSpawnData = new MissionAgentSpawnLogic.FormationSpawnData[11];
			this._phases = new List<MissionAgentSpawnLogic.SpawnPhase>[2];
			for (int j = 0; j < 2; j++)
			{
				this._phases[j] = new List<MissionAgentSpawnLogic.SpawnPhase>();
			}
			this._reinforcementSpawnEnabled = false;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00079063 File Offset: 0x00077263
		public void SetCustomReinforcementSpawnTimer(ICustomReinforcementSpawnTimer timer)
		{
			this._customReinforcementSpawnTimer = timer;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0007906C File Offset: 0x0007726C
		public void SetSpawnTroops(BattleSideEnum side, bool spawnTroops, bool enforceSpawning = false)
		{
			this._missionSides[(int)side].SetSpawnTroops(spawnTroops);
			if (spawnTroops && enforceSpawning)
			{
				this.CheckDeployment();
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00079088 File Offset: 0x00077288
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MissionGameModels.Current.BattleInitializationModel.InitializeModel();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0007909F File Offset: 0x0007729F
		protected override void OnEndMission()
		{
			MissionGameModels.Current.BattleSpawnModel.OnMissionEnd();
			MissionGameModels.Current.BattleInitializationModel.FinalizeModel();
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000790BF File Offset: 0x000772BF
		public void SetSpawnHorses(BattleSideEnum side, bool spawnHorses)
		{
			this._missionSides[(int)side].SetSpawnWithHorses(spawnHorses);
			base.Mission.SetDeploymentPlanSpawnWithHorses(side, spawnHorses);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000790DC File Offset: 0x000772DC
		public void StartSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(true);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000790EC File Offset: 0x000772EC
		public void StopSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(false);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000790FC File Offset: 0x000772FC
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return this._missionSides[(int)side].TroopSpawnActive;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0007910C File Offset: 0x0007730C
		public void OnBattleSideDeployed(BattleSideEnum side)
		{
			foreach (Team team in base.Mission.Teams)
			{
				if (team.Side == side)
				{
					team.OnDeployed();
				}
			}
			foreach (Team team2 in base.Mission.Teams)
			{
				if (team2.Side == side)
				{
					foreach (Formation formation in team2.FormationsIncludingEmpty)
					{
						if (formation.CountOfUnits > 0)
						{
							formation.QuerySystem.EvaluateAllPreliminaryQueryData();
						}
					}
					team2.MasterOrderController.OnOrderIssued += this.OrderController_OnOrderIssued;
					for (int i = 8; i < 10; i++)
					{
						Formation formation2 = team2.FormationsIncludingSpecialAndEmpty[i];
						if (formation2.CountOfUnits > 0)
						{
							team2.MasterOrderController.SelectFormation(formation2);
							team2.MasterOrderController.SetOrderWithAgent(OrderType.FollowMe, team2.GeneralAgent);
							team2.MasterOrderController.ClearSelectedFormations();
							formation2.SetControlledByAI(true, false);
						}
					}
					team2.MasterOrderController.OnOrderIssued -= this.OrderController_OnOrderIssued;
				}
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0007929C File Offset: 0x0007749C
		public float GetReinforcementInterval()
		{
			return this._globalReinforcementInterval;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000792A4 File Offset: 0x000774A4
		public void SetReinforcementsSpawnEnabled(bool value, bool resetTimers = true)
		{
			if (this._reinforcementSpawnEnabled != value)
			{
				this._reinforcementSpawnEnabled = value;
				if (resetTimers)
				{
					if (this._spawnSettings.ReinforcementTroopsTimingMethod == MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer)
					{
						this._globalReinforcementSpawnTimer.Reset();
						return;
					}
					if (this._spawnSettings.ReinforcementTroopsTimingMethod == MissionSpawnSettings.ReinforcementTimingMethod.CustomTimer)
					{
						for (int i = 0; i < 2; i++)
						{
							this._customReinforcementSpawnTimer.ResetTimer((BattleSideEnum)i);
						}
					}
				}
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00079303 File Offset: 0x00077503
		public int GetTotalNumberOfTroopsForSide(BattleSideEnum side)
		{
			return this._numberOfTroopsInTotal[(int)side];
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00079310 File Offset: 0x00077510
		public BasicCharacterObject GetGeneralCharacterOfSide(BattleSideEnum side)
		{
			if (side >= BattleSideEnum.Defender && side < BattleSideEnum.NumSides)
			{
				this._missionSides[(int)side].GetGeneralCharacter();
			}
			return null;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00079336 File Offset: 0x00077536
		public bool GetSpawnHorses(BattleSideEnum side)
		{
			return this._missionSides[(int)side].SpawnWithHorses;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00079348 File Offset: 0x00077548
		private bool CheckMinimumBatchQuotaRequirement()
		{
			int num = MissionAgentSpawnLogic.MaxNumberOfAgentsForMission - this.NumberOfAgents;
			int num2 = 0;
			for (int i = 0; i < 2; i++)
			{
				num2 += this._missionSides[i].ReinforcementQuotaRequirement;
			}
			return num >= num2;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x00079388 File Offset: 0x00077588
		private void CheckGlobalReinforcementBatch()
		{
			if (this._globalReinforcementSpawnTimer.ElapsedTime >= this._globalReinforcementInterval)
			{
				bool flag = false;
				for (int i = 0; i < 2; i++)
				{
					BattleSideEnum battleSide = (BattleSideEnum)i;
					this.NotifyReinforcementTroopsSpawned(battleSide, false);
					bool flag2 = this._missionSides[i].CheckReinforcementBatch();
					flag = (flag || flag2);
				}
				this._spawningReinforcements = (flag && this.CheckMinimumBatchQuotaRequirement());
				this._globalReinforcementSpawnTimer.Reset();
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000793F0 File Offset: 0x000775F0
		private void CheckCustomReinforcementBatch()
		{
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				BattleSideEnum battleSideEnum = (BattleSideEnum)i;
				if (this._customReinforcementSpawnTimer.Check(battleSideEnum))
				{
					flag = true;
					this.NotifyReinforcementTroopsSpawned(battleSideEnum, false);
					this._missionSides[i].CheckReinforcementBatch();
				}
			}
			if (flag)
			{
				bool flag2 = false;
				for (int j = 0; j < 2; j++)
				{
					flag2 = (flag2 || this._missionSides[j].ReinforcementSpawnActive);
				}
				this._spawningReinforcements = (flag2 && this.CheckMinimumBatchQuotaRequirement());
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0007946F File Offset: 0x0007766F
		public bool IsSideDepleted(BattleSideEnum side)
		{
			return this._phases[(int)side].Count == 1 && this._missionSides[(int)side].NumberOfActiveTroops == 0 && this.GetActivePhaseForSide(side).RemainingSpawnNumber == 0;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000794A1 File Offset: 0x000776A1
		public void AddPhaseChangeAction(BattleSideEnum side, MissionAgentSpawnLogic.OnPhaseChangedDelegate onPhaseChanged)
		{
			MissionAgentSpawnLogic.OnPhaseChangedDelegate[] onPhaseChanged2 = this._onPhaseChanged;
			onPhaseChanged2[(int)side] = (MissionAgentSpawnLogic.OnPhaseChangedDelegate)Delegate.Combine(onPhaseChanged2[(int)side], onPhaseChanged);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000794C0 File Offset: 0x000776C0
		private void Init(bool spawnDefenders, bool spawnAttackers, in MissionSpawnSettings reinforcementSpawnSettings)
		{
			List<MissionAgentSpawnLogic.SpawnPhase>[] phases = this._phases;
			for (int i = 0; i < phases.Length; i++)
			{
				if (phases[i].Count <= 0)
				{
					return;
				}
			}
			this._spawnSettings = reinforcementSpawnSettings;
			int num = 0;
			int num2 = 1;
			this._globalReinforcementInterval = this._spawnSettings.GlobalReinforcementInterval;
			int[] array = new int[2];
			array[0] = this._phases[num].Sum((MissionAgentSpawnLogic.SpawnPhase p) => p.TotalSpawnNumber);
			array[1] = this._phases[num2].Sum((MissionAgentSpawnLogic.SpawnPhase p) => p.TotalSpawnNumber);
			int[] array2 = array;
			int num3 = array2.Sum();
			if (this._spawnSettings.InitialTroopsSpawnMethod == MissionSpawnSettings.InitialSpawnMethod.BattleSizeAllocating)
			{
				float[] array3 = new float[]
				{
					(float)array2[num] / (float)num3,
					(float)array2[num2] / (float)num3
				};
				array3[num] = MathF.Min(this._spawnSettings.MaximumBattleSideRatio, array3[num] * this._spawnSettings.DefenderAdvantageFactor);
				array3[num2] = 1f - array3[num];
				int num4 = (array3[num] < array3[num2]) ? 0 : 1;
				int oppositeSide = (int)((BattleSideEnum)num4).GetOppositeSide();
				int num5 = num4;
				if (array3[oppositeSide] > this._spawnSettings.MaximumBattleSideRatio)
				{
					array3[oppositeSide] = this._spawnSettings.MaximumBattleSideRatio;
					array3[num5] = 1f - this._spawnSettings.MaximumBattleSideRatio;
				}
				int[] array4 = new int[2];
				int val = MathF.Ceiling(array3[num5] * (float)this._battleSize);
				array4[num5] = Math.Min(val, array2[num5]);
				array4[oppositeSide] = this._battleSize - array4[num5];
				for (int j = 0; j < 2; j++)
				{
					foreach (MissionAgentSpawnLogic.SpawnPhase spawnPhase in this._phases[j])
					{
						if (spawnPhase.InitialSpawnNumber > array4[j])
						{
							int num6 = array4[j];
							int num7 = spawnPhase.InitialSpawnNumber - num6;
							spawnPhase.InitialSpawnNumber = num6;
							spawnPhase.RemainingSpawnNumber += num7;
						}
					}
				}
			}
			else if (this._spawnSettings.InitialTroopsSpawnMethod == MissionSpawnSettings.InitialSpawnMethod.FreeAllocation)
			{
				this._phases[num].Max((MissionAgentSpawnLogic.SpawnPhase p) => p.InitialSpawnNumber);
				this._phases[num2].Max((MissionAgentSpawnLogic.SpawnPhase p) => p.InitialSpawnNumber);
			}
			if (this._spawnSettings.ReinforcementTroopsSpawnMethod == MissionSpawnSettings.ReinforcementSpawnMethod.Wave)
			{
				for (int k = 0; k < 2; k++)
				{
					foreach (MissionAgentSpawnLogic.SpawnPhase spawnPhase2 in this._phases[k])
					{
						int num8 = (int)Math.Max(1f, (float)spawnPhase2.InitialSpawnNumber * this._spawnSettings.ReinforcementWavePercentage);
						if (this._spawnSettings.MaximumReinforcementWaveCount > 0)
						{
							int num9 = Math.Min(spawnPhase2.RemainingSpawnNumber, num8 * this._spawnSettings.MaximumReinforcementWaveCount);
							int num10 = Math.Max(0, spawnPhase2.RemainingSpawnNumber - num9);
							this._numberOfTroopsInTotal[k] -= num10;
							array2[k] -= num10;
							spawnPhase2.RemainingSpawnNumber = num9;
							spawnPhase2.TotalSpawnNumber = spawnPhase2.RemainingSpawnNumber + spawnPhase2.InitialSpawnNumber;
						}
					}
				}
			}
			base.Mission.SetBattleAgentCount(MathF.Min(this.DefenderActivePhase.InitialSpawnNumber, this.AttackerActivePhase.InitialSpawnNumber));
			base.Mission.SetInitialAgentCountForSide(BattleSideEnum.Defender, array2[num]);
			base.Mission.SetInitialAgentCountForSide(BattleSideEnum.Attacker, array2[num2]);
			this._missionSides[num].SetSpawnTroops(spawnDefenders);
			this._missionSides[num2].SetSpawnTroops(spawnAttackers);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000798D4 File Offset: 0x00077AD4
		private void AddPhase(BattleSideEnum side, int totalSpawn, int initialSpawn)
		{
			MissionAgentSpawnLogic.SpawnPhase item = new MissionAgentSpawnLogic.SpawnPhase
			{
				TotalSpawnNumber = totalSpawn,
				InitialSpawnNumber = initialSpawn,
				RemainingSpawnNumber = totalSpawn - initialSpawn
			};
			this._phases[(int)side].Add(item);
			this._numberOfTroopsInTotal[(int)side] += totalSpawn;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x00079920 File Offset: 0x00077B20
		private void PhaseTick()
		{
			for (int i = 0; i < 2; i++)
			{
				MissionAgentSpawnLogic.SpawnPhase activePhaseForSide = this.GetActivePhaseForSide((BattleSideEnum)i);
				activePhaseForSide.NumberActiveTroops = this._missionSides[i].NumberOfActiveTroops;
				if (activePhaseForSide.NumberActiveTroops == 0 && activePhaseForSide.RemainingSpawnNumber == 0 && this._phases[i].Count > 1)
				{
					this._phases[i].Remove(activePhaseForSide);
					BattleSideEnum battleSideEnum = (BattleSideEnum)i;
					if (this.GetActivePhaseForSide(battleSideEnum) != null)
					{
						if (this._onPhaseChanged[i] != null)
						{
							this._onPhaseChanged[i]();
						}
						IMissionDeploymentPlan deploymentPlan = base.Mission.DeploymentPlan;
						if (deploymentPlan.IsPlanMadeForBattleSide(battleSideEnum, DeploymentPlanType.Initial))
						{
							base.Mission.ClearAddedTroopsInDeploymentPlan(battleSideEnum, DeploymentPlanType.Initial);
							base.Mission.ClearDeploymentPlanForSide(battleSideEnum, DeploymentPlanType.Initial);
						}
						if (deploymentPlan.IsPlanMadeForBattleSide(battleSideEnum, DeploymentPlanType.Reinforcement))
						{
							base.Mission.ClearAddedTroopsInDeploymentPlan(battleSideEnum, DeploymentPlanType.Reinforcement);
							base.Mission.ClearDeploymentPlanForSide(battleSideEnum, DeploymentPlanType.Reinforcement);
						}
						Debug.Print("New spawn phase!", 0, Debug.DebugColor.Green, 64UL);
					}
				}
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x00079A18 File Offset: 0x00077C18
		private bool CheckDeployment()
		{
			bool isDeploymentOver = this.IsDeploymentOver;
			if (!isDeploymentOver)
			{
				for (int i = 0; i < 2; i++)
				{
					BattleSideEnum battleSideEnum = (BattleSideEnum)i;
					MissionAgentSpawnLogic.SpawnPhase activePhaseForSide = this.GetActivePhaseForSide(battleSideEnum);
					if (!base.Mission.DeploymentPlan.IsPlanMadeForBattleSide(battleSideEnum, DeploymentPlanType.Initial))
					{
						if (activePhaseForSide.InitialSpawnNumber > 0)
						{
							this._missionSides[i].ReserveTroops(activePhaseForSide.InitialSpawnNumber);
							this._missionSides[i].GetFormationSpawnData(this._formationSpawnData);
							for (int j = 0; j < this._formationSpawnData.Length; j++)
							{
								if (this._formationSpawnData[j].NumTroops > 0)
								{
									base.Mission.AddTroopsToDeploymentPlan(battleSideEnum, DeploymentPlanType.Initial, (FormationClass)j, this._formationSpawnData[j].FootTroopCount, this._formationSpawnData[j].MountedTroopCount);
								}
							}
						}
						float spawnPathOffset = 0f;
						if (base.Mission.HasSpawnPath)
						{
							int battleSizeForActivePhase = this.GetBattleSizeForActivePhase();
							Path initialSpawnPath = base.Mission.GetInitialSpawnPath();
							spawnPathOffset = Mission.GetBattleSizeOffset(battleSizeForActivePhase, initialSpawnPath);
						}
						base.Mission.MakeDeploymentPlanForSide(battleSideEnum, DeploymentPlanType.Initial, spawnPathOffset);
					}
					if (!base.Mission.DeploymentPlan.IsPlanMadeForBattleSide(battleSideEnum, DeploymentPlanType.Reinforcement))
					{
						int num = Math.Max(this._battleSize / (2 * this._formationSpawnData.Length), 1);
						for (int k = 0; k < this._formationSpawnData.Length; k++)
						{
							if (((FormationClass)k).IsMounted())
							{
								base.Mission.AddTroopsToDeploymentPlan(battleSideEnum, DeploymentPlanType.Reinforcement, (FormationClass)k, 0, num);
							}
							else
							{
								base.Mission.AddTroopsToDeploymentPlan(battleSideEnum, DeploymentPlanType.Reinforcement, (FormationClass)k, num, 0);
							}
						}
						base.Mission.MakeDeploymentPlanForSide(battleSideEnum, DeploymentPlanType.Reinforcement, 0f);
					}
				}
				for (int l = 0; l < 2; l++)
				{
					BattleSideEnum battleSideEnum2 = (BattleSideEnum)l;
					MissionAgentSpawnLogic.SpawnPhase activePhaseForSide2 = this.GetActivePhaseForSide(battleSideEnum2);
					if (base.Mission.DeploymentPlan.IsPlanMadeForBattleSide(battleSideEnum2, DeploymentPlanType.Initial) && activePhaseForSide2.InitialSpawnNumber > 0 && this._missionSides[l].TroopSpawnActive)
					{
						int initialSpawnNumber = activePhaseForSide2.InitialSpawnNumber;
						this._missionSides[l].SpawnTroops(initialSpawnNumber, false);
						this.GetActivePhaseForSide(battleSideEnum2).OnInitialTroopsSpawned();
						this._missionSides[l].OnInitialSpawnOver();
						if (!this._sidesWhereSpawnOccured.Contains(battleSideEnum2))
						{
							this._sidesWhereSpawnOccured.Add(battleSideEnum2);
						}
						Action<BattleSideEnum, int> onInitialTroopsSpawned = this.OnInitialTroopsSpawned;
						if (onInitialTroopsSpawned != null)
						{
							onInitialTroopsSpawned(battleSideEnum2, initialSpawnNumber);
						}
					}
				}
				isDeploymentOver = this.IsDeploymentOver;
				if (isDeploymentOver)
				{
					foreach (BattleSideEnum side in this._sidesWhereSpawnOccured)
					{
						this.OnBattleSideDeployed(side);
					}
				}
			}
			return isDeploymentOver;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00079CCC File Offset: 0x00077ECC
		private void CheckReinforcementSpawn()
		{
			int num = 0;
			int num2 = 1;
			MissionAgentSpawnLogic.MissionSide missionSide = this._missionSides[num];
			MissionAgentSpawnLogic.MissionSide missionSide2 = this._missionSides[num2];
			bool flag = missionSide.HasSpawnableReinforcements && ((float)missionSide.ReinforcementsSpawnedInLastBatch < missionSide.ReinforcementBatchSize || missionSide.ReinforcementBatchPriority >= missionSide2.ReinforcementBatchPriority);
			bool flag2 = missionSide2.HasSpawnableReinforcements && ((float)missionSide2.ReinforcementsSpawnedInLastBatch < missionSide2.ReinforcementBatchSize || missionSide2.ReinforcementBatchPriority >= missionSide.ReinforcementBatchPriority);
			int num3 = 0;
			if (flag && flag2)
			{
				if (missionSide.ReinforcementBatchPriority >= missionSide2.ReinforcementBatchPriority)
				{
					int num4 = missionSide.TryReinforcementSpawn();
					this.DefenderActivePhase.RemainingSpawnNumber -= num4;
					num3 += num4;
					num4 = missionSide2.TryReinforcementSpawn();
					this.AttackerActivePhase.RemainingSpawnNumber -= num4;
					num3 += num4;
				}
				else
				{
					int num4 = missionSide2.TryReinforcementSpawn();
					this.AttackerActivePhase.RemainingSpawnNumber -= num4;
					num3 += num4;
					num4 = missionSide.TryReinforcementSpawn();
					this.DefenderActivePhase.RemainingSpawnNumber -= num4;
					num3 += num4;
				}
			}
			else if (flag)
			{
				int num4 = missionSide.TryReinforcementSpawn();
				this.DefenderActivePhase.RemainingSpawnNumber -= num4;
				num3 += num4;
			}
			else if (flag2)
			{
				int num4 = missionSide2.TryReinforcementSpawn();
				this.AttackerActivePhase.RemainingSpawnNumber -= num4;
				num3 += num4;
			}
			if (num3 > 0)
			{
				for (int i = 0; i < 2; i++)
				{
					this.NotifyReinforcementTroopsSpawned((BattleSideEnum)i, true);
				}
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00079E70 File Offset: 0x00078070
		private void NotifyReinforcementTroopsSpawned(BattleSideEnum battleSide, bool checkEmptyReserves = false)
		{
			MissionAgentSpawnLogic.MissionSide missionSide = this._missionSides[(int)battleSide];
			int reinforcementsSpawnedInLastBatch = missionSide.ReinforcementsSpawnedInLastBatch;
			if (!missionSide.ReinforcementsNotifiedOnLastBatch && reinforcementsSpawnedInLastBatch > 0 && (!checkEmptyReserves || (checkEmptyReserves && !missionSide.HasReservedTroops)))
			{
				Action<BattleSideEnum, int> onReinforcementsSpawned = this.OnReinforcementsSpawned;
				if (onReinforcementsSpawned != null)
				{
					onReinforcementsSpawned(battleSide, reinforcementsSpawnedInLastBatch);
				}
				missionSide.SetReinforcementsNotifiedOnLastBatch(true);
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00079EC1 File Offset: 0x000780C1
		private void OrderController_OnOrderIssued(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController, params object[] delegateParams)
		{
			DeploymentHandler.OrderController_OnOrderIssued_Aux(orderType, appliedFormations, orderController, delegateParams);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x00079ECD File Offset: 0x000780CD
		private int GetBattleSizeForActivePhase()
		{
			return MathF.Max(this.DefenderActivePhase.TotalSpawnNumber, this.AttackerActivePhase.TotalSpawnNumber);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00079EEA File Offset: 0x000780EA
		private MissionAgentSpawnLogic.SpawnPhase GetActivePhaseForSide(BattleSideEnum side)
		{
			if (side == BattleSideEnum.Defender)
			{
				return this.DefenderActivePhase;
			}
			if (side != BattleSideEnum.Attacker)
			{
				Debug.FailedAssert("Wrong Side", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\MissionAgentSpawnLogic.cs", "GetActivePhaseForSide", 1510);
				return null;
			}
			return this.AttackerActivePhase;
		}

		// Token: 0x04000C61 RID: 3169
		private static int _maxNumberOfAgentsForMissionCache;

		// Token: 0x04000C64 RID: 3172
		private readonly MissionAgentSpawnLogic.OnPhaseChangedDelegate[] _onPhaseChanged = new MissionAgentSpawnLogic.OnPhaseChangedDelegate[2];

		// Token: 0x04000C65 RID: 3173
		private readonly List<MissionAgentSpawnLogic.SpawnPhase>[] _phases;

		// Token: 0x04000C66 RID: 3174
		private readonly int[] _numberOfTroopsInTotal;

		// Token: 0x04000C67 RID: 3175
		private readonly MissionAgentSpawnLogic.FormationSpawnData[] _formationSpawnData;

		// Token: 0x04000C68 RID: 3176
		private readonly int _battleSize;

		// Token: 0x04000C69 RID: 3177
		private bool _reinforcementSpawnEnabled = true;

		// Token: 0x04000C6A RID: 3178
		private bool _spawningReinforcements;

		// Token: 0x04000C6B RID: 3179
		private readonly BasicMissionTimer _globalReinforcementSpawnTimer;

		// Token: 0x04000C6C RID: 3180
		private ICustomReinforcementSpawnTimer _customReinforcementSpawnTimer;

		// Token: 0x04000C6D RID: 3181
		private float _globalReinforcementInterval;

		// Token: 0x04000C6E RID: 3182
		private MissionSpawnSettings _spawnSettings;

		// Token: 0x04000C6F RID: 3183
		private readonly MissionAgentSpawnLogic.MissionSide[] _missionSides;

		// Token: 0x04000C70 RID: 3184
		private BannerBearerLogic _bannerBearerLogic;

		// Token: 0x04000C71 RID: 3185
		private List<BattleSideEnum> _sidesWhereSpawnOccured = new List<BattleSideEnum>();

		// Token: 0x04000C72 RID: 3186
		private readonly MissionAgentSpawnLogic.MissionSide _playerSide;

		// Token: 0x0200052C RID: 1324
		private struct FormationSpawnData
		{
			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x060038C7 RID: 14535 RVA: 0x000E30D3 File Offset: 0x000E12D3
			public int NumTroops
			{
				get
				{
					return this.FootTroopCount + this.MountedTroopCount;
				}
			}

			// Token: 0x04001C5F RID: 7263
			public int FootTroopCount;

			// Token: 0x04001C60 RID: 7264
			public int MountedTroopCount;
		}

		// Token: 0x0200052D RID: 1325
		private class MissionSide
		{
			// Token: 0x1700098A RID: 2442
			// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000E30E2 File Offset: 0x000E12E2
			// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000E30EA File Offset: 0x000E12EA
			public bool TroopSpawnActive { get; private set; }

			// Token: 0x1700098B RID: 2443
			// (get) Token: 0x060038CA RID: 14538 RVA: 0x000E30F3 File Offset: 0x000E12F3
			public bool IsPlayerSide { get; }

			// Token: 0x1700098C RID: 2444
			// (get) Token: 0x060038CB RID: 14539 RVA: 0x000E30FB File Offset: 0x000E12FB
			// (set) Token: 0x060038CC RID: 14540 RVA: 0x000E3103 File Offset: 0x000E1303
			public bool ReinforcementSpawnActive { get; private set; }

			// Token: 0x1700098D RID: 2445
			// (get) Token: 0x060038CD RID: 14541 RVA: 0x000E310C File Offset: 0x000E130C
			public bool SpawnWithHorses
			{
				get
				{
					return this._spawnWithHorses;
				}
			}

			// Token: 0x1700098E RID: 2446
			// (get) Token: 0x060038CE RID: 14542 RVA: 0x000E3114 File Offset: 0x000E1314
			// (set) Token: 0x060038CF RID: 14543 RVA: 0x000E311C File Offset: 0x000E131C
			public bool ReinforcementsNotifiedOnLastBatch { get; private set; }

			// Token: 0x1700098F RID: 2447
			// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000E3125 File Offset: 0x000E1325
			public int NumberOfActiveTroops
			{
				get
				{
					return this._numSpawnedTroops - this._troopSupplier.NumRemovedTroops;
				}
			}

			// Token: 0x17000990 RID: 2448
			// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000E3139 File Offset: 0x000E1339
			public int ReinforcementQuotaRequirement
			{
				get
				{
					return this._reinforcementQuotaRequirement;
				}
			}

			// Token: 0x17000991 RID: 2449
			// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000E3141 File Offset: 0x000E1341
			public int ReinforcementsSpawnedInLastBatch
			{
				get
				{
					return this._reinforcementsSpawnedInLastBatch;
				}
			}

			// Token: 0x17000992 RID: 2450
			// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000E3149 File Offset: 0x000E1349
			public float ReinforcementBatchSize
			{
				get
				{
					return (float)this._reinforcementBatchSize;
				}
			}

			// Token: 0x17000993 RID: 2451
			// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000E3152 File Offset: 0x000E1352
			public bool HasReservedTroops
			{
				get
				{
					return this._reservedTroops.Count > 0;
				}
			}

			// Token: 0x17000994 RID: 2452
			// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000E3162 File Offset: 0x000E1362
			public float ReinforcementBatchPriority
			{
				get
				{
					return this._reinforcementBatchPriority;
				}
			}

			// Token: 0x060038D6 RID: 14550 RVA: 0x000E316A File Offset: 0x000E136A
			public int GetNumberOfPlayerControllableTroops()
			{
				return this._troopSupplier.GetNumberOfPlayerControllableTroops();
			}

			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000E3177 File Offset: 0x000E1377
			public int ReservedTroopsCount
			{
				get
				{
					return this._reservedTroops.Count;
				}
			}

			// Token: 0x060038D8 RID: 14552 RVA: 0x000E3184 File Offset: 0x000E1384
			public MissionSide(MissionAgentSpawnLogic spawnLogic, BattleSideEnum side, IMissionTroopSupplier troopSupplier, bool isPlayerSide)
			{
				this._spawnLogic = spawnLogic;
				this._side = side;
				this._spawnWithHorses = true;
				this._spawnedFormations = new MBArrayList<Formation>();
				this._troopSupplier = troopSupplier;
				this._reinforcementQuotaRequirement = 0;
				this._reinforcementBatchSize = 0;
				this._reinforcementSpawnedUnitCountPerFormation = new ValueTuple<int, int>[8];
				this._reinforcementTroopFormationAssignments = new Dictionary<IAgentOriginBase, int>();
				this.IsPlayerSide = isPlayerSide;
				this.ReinforcementsNotifiedOnLastBatch = false;
			}

			// Token: 0x060038D9 RID: 14553 RVA: 0x000E3200 File Offset: 0x000E1400
			public int TryReinforcementSpawn()
			{
				int num = 0;
				if (this.ReinforcementSpawnActive && this.TroopSpawnActive && this._reservedTroops.Count > 0)
				{
					int num2 = MissionAgentSpawnLogic.MaxNumberOfAgentsForMission - this._spawnLogic.NumberOfAgents;
					int reservedTroopQuota = this.GetReservedTroopQuota(0);
					if (num2 >= reservedTroopQuota)
					{
						num = this.SpawnTroops(1, true);
						if (num > 0)
						{
							this._reinforcementQuotaRequirement -= reservedTroopQuota;
							if (this._reservedTroops.Count >= this._reinforcementBatchSize)
							{
								this._reinforcementQuotaRequirement += this.GetReservedTroopQuota(this._reinforcementBatchSize - 1);
							}
							this._reinforcementBatchPriority /= 2f;
						}
					}
				}
				this._reinforcementsSpawnedInLastBatch += num;
				return num;
			}

			// Token: 0x060038DA RID: 14554 RVA: 0x000E32BC File Offset: 0x000E14BC
			public void GetFormationSpawnData(MissionAgentSpawnLogic.FormationSpawnData[] formationSpawnData)
			{
				if (formationSpawnData != null && formationSpawnData.Length == 11)
				{
					for (int i = 0; i < formationSpawnData.Length; i++)
					{
						formationSpawnData[i].FootTroopCount = 0;
						formationSpawnData[i].MountedTroopCount = 0;
					}
					using (List<IAgentOriginBase>.Enumerator enumerator = this._reservedTroops.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IAgentOriginBase agentOriginBase = enumerator.Current;
							FormationClass agentTroopClass = Mission.Current.GetAgentTroopClass(this._side, agentOriginBase.Troop);
							if (agentOriginBase.Troop.HasMount())
							{
								FormationClass formationClass = agentTroopClass;
								formationSpawnData[(int)formationClass].MountedTroopCount = formationSpawnData[(int)formationClass].MountedTroopCount + 1;
							}
							else
							{
								FormationClass formationClass2 = agentTroopClass;
								formationSpawnData[(int)formationClass2].FootTroopCount = formationSpawnData[(int)formationClass2].FootTroopCount + 1;
							}
						}
						return;
					}
				}
				Debug.FailedAssert("Formation troop counts parameter is not set correctly.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\MissionAgentSpawnLogic.cs", "GetFormationSpawnData", 155);
			}

			// Token: 0x060038DB RID: 14555 RVA: 0x000E33A4 File Offset: 0x000E15A4
			public void ReserveTroops(int number)
			{
				if (number > 0 && this._troopSupplier.AnyTroopRemainsToBeSupplied)
				{
					this._reservedTroops.AddRange(this._troopSupplier.SupplyTroops(number));
				}
			}

			// Token: 0x17000996 RID: 2454
			// (get) Token: 0x060038DC RID: 14556 RVA: 0x000E33CE File Offset: 0x000E15CE
			public bool HasSpawnableReinforcements
			{
				get
				{
					return this.ReinforcementSpawnActive && this.HasReservedTroops && this.ReinforcementBatchSize > 0f;
				}
			}

			// Token: 0x060038DD RID: 14557 RVA: 0x000E33EF File Offset: 0x000E15EF
			public BasicCharacterObject GetGeneralCharacter()
			{
				return this._troopSupplier.GetGeneralCharacter();
			}

			// Token: 0x060038DE RID: 14558 RVA: 0x000E33FC File Offset: 0x000E15FC
			public unsafe bool CheckReinforcementBatch()
			{
				MissionAgentSpawnLogic.SpawnPhase spawnPhase = (this._side == BattleSideEnum.Defender) ? this._spawnLogic.DefenderActivePhase : this._spawnLogic.AttackerActivePhase;
				this._reinforcementsSpawnedInLastBatch = 0;
				this.ReinforcementsNotifiedOnLastBatch = false;
				int num = 0;
				MissionSpawnSettings missionSpawnSettings = *this._spawnLogic.ReinforcementSpawnSettings;
				switch (missionSpawnSettings.ReinforcementTroopsSpawnMethod)
				{
				case MissionSpawnSettings.ReinforcementSpawnMethod.Balanced:
					num = this.ComputeBalancedBatch(spawnPhase);
					break;
				case MissionSpawnSettings.ReinforcementSpawnMethod.Wave:
					num = this.ComputeWaveBatch(spawnPhase);
					break;
				case MissionSpawnSettings.ReinforcementSpawnMethod.Fixed:
					num = this.ComputeFixedBatch(spawnPhase);
					break;
				}
				num = Math.Min(num, spawnPhase.RemainingSpawnNumber);
				num -= this._reservedTroops.Count;
				if (num > 0)
				{
					int count = this._reservedTroops.Count;
					this.ReserveTroops(num);
					if (count < this._reinforcementBatchSize)
					{
						int num2 = Math.Min(this._reservedTroops.Count, this._reinforcementBatchSize);
						for (int i = count; i < num2; i++)
						{
							this._reinforcementQuotaRequirement += this.GetReservedTroopQuota(i);
						}
					}
				}
				this._reinforcementBatchPriority = (float)this._reservedTroops.Count;
				bool reinforcementSpawnActive;
				if (missionSpawnSettings.ReinforcementTroopsSpawnMethod == MissionSpawnSettings.ReinforcementSpawnMethod.Wave)
				{
					reinforcementSpawnActive = (this._reservedTroops.Count > 0);
				}
				else
				{
					reinforcementSpawnActive = (this._reservedTroops.Count > 0 && (this._reservedTroops.Count >= this._reinforcementBatchSize || spawnPhase.RemainingSpawnNumber <= this._reinforcementBatchSize));
				}
				this.ReinforcementSpawnActive = reinforcementSpawnActive;
				if (this.ReinforcementSpawnActive)
				{
					this.ResetReinforcementSpawnedUnitCountsPerFormation();
					Mission.Current.UpdateReinforcementPlan(this._side);
				}
				return this.ReinforcementSpawnActive;
			}

			// Token: 0x060038DF RID: 14559 RVA: 0x000E3594 File Offset: 0x000E1794
			public IEnumerable<IAgentOriginBase> GetAllTroops()
			{
				return this._troopSupplier.GetAllTroops();
			}

			// Token: 0x060038E0 RID: 14560 RVA: 0x000E35A4 File Offset: 0x000E17A4
			public int SpawnTroops(int number, bool isReinforcement)
			{
				if (number <= 0)
				{
					return 0;
				}
				List<IAgentOriginBase> list = new List<IAgentOriginBase>();
				int num = MathF.Min(this._reservedTroops.Count, number);
				if (num > 0)
				{
					for (int i = 0; i < num; i++)
					{
						IAgentOriginBase item = this._reservedTroops[i];
						list.Add(item);
					}
					this._reservedTroops.RemoveRange(0, num);
				}
				int numberToAllocate = number - num;
				list.AddRange(this._troopSupplier.SupplyTroops(numberToAllocate));
				Mission mission = Mission.Current;
				if (this._troopOriginsToSpawnPerTeam == null)
				{
					this._troopOriginsToSpawnPerTeam = new List<ValueTuple<Team, List<IAgentOriginBase>>>();
					using (List<Team>.Enumerator enumerator = mission.Teams.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Team team = enumerator.Current;
							bool flag = team.Side == mission.PlayerTeam.Side;
							if ((this.IsPlayerSide && flag) || (!this.IsPlayerSide && !flag))
							{
								this._troopOriginsToSpawnPerTeam.Add(new ValueTuple<Team, List<IAgentOriginBase>>(team, new List<IAgentOriginBase>()));
							}
						}
						goto IL_136;
					}
				}
				foreach (ValueTuple<Team, List<IAgentOriginBase>> valueTuple in this._troopOriginsToSpawnPerTeam)
				{
					valueTuple.Item2.Clear();
				}
				IL_136:
				int num2 = 0;
				foreach (IAgentOriginBase agentOriginBase in list)
				{
					Team agentTeam = Mission.GetAgentTeam(agentOriginBase, this.IsPlayerSide);
					foreach (ValueTuple<Team, List<IAgentOriginBase>> valueTuple2 in this._troopOriginsToSpawnPerTeam)
					{
						if (agentTeam == valueTuple2.Item1)
						{
							num2++;
							valueTuple2.Item2.Add(agentOriginBase);
						}
					}
				}
				int num3 = 0;
				List<IAgentOriginBase> list2 = new List<IAgentOriginBase>();
				foreach (ValueTuple<Team, List<IAgentOriginBase>> valueTuple3 in this._troopOriginsToSpawnPerTeam)
				{
					if (!valueTuple3.Item2.IsEmpty<IAgentOriginBase>())
					{
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						List<ValueTuple<IAgentOriginBase, int>> list3 = null;
						if (isReinforcement)
						{
							list3 = new List<ValueTuple<IAgentOriginBase, int>>();
							using (List<IAgentOriginBase>.Enumerator enumerator3 = valueTuple3.Item2.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									IAgentOriginBase agentOriginBase2 = enumerator3.Current;
									int item2;
									this._reinforcementTroopFormationAssignments.TryGetValue(agentOriginBase2, out item2);
									list3.Add(new ValueTuple<IAgentOriginBase, int>(agentOriginBase2, item2));
								}
								goto IL_280;
							}
							goto IL_262;
						}
						goto IL_262;
						IL_280:
						for (int j = 0; j < 8; j++)
						{
							list2.Clear();
							IAgentOriginBase agentOriginBase3 = null;
							foreach (ValueTuple<IAgentOriginBase, int> valueTuple4 in list3)
							{
								IAgentOriginBase item3 = valueTuple4.Item1;
								int item4 = valueTuple4.Item2;
								if (j == item4)
								{
									if (item3.Troop == Game.Current.PlayerTroop)
									{
										agentOriginBase3 = item3;
									}
									else
									{
										if (item3.Troop.HasMount())
										{
											num5++;
										}
										else
										{
											num6++;
										}
										list2.Add(item3);
									}
								}
							}
							if (agentOriginBase3 != null)
							{
								if (agentOriginBase3.Troop.HasMount())
								{
									num5++;
								}
								else
								{
									num6++;
								}
								list2.Add(agentOriginBase3);
							}
							int count = list2.Count;
							if (count > 0)
							{
								bool isMounted = this._spawnWithHorses && MissionDeploymentPlan.HasSignificantMountedTroops(num6, num5);
								int num7 = 0;
								int num8 = count;
								if (this.ReinforcementSpawnActive)
								{
									num7 = this._reinforcementSpawnedUnitCountPerFormation[j].Item1;
									num8 = this._reinforcementSpawnedUnitCountPerFormation[j].Item2;
								}
								Formation formation = valueTuple3.Item1.GetFormation((FormationClass)j);
								if (!formation.HasBeenPositioned)
								{
									formation.BeginSpawn(num8, isMounted);
									mission.SetFormationPositioningFromDeploymentPlan(formation);
									this._spawnedFormations.Add(formation);
								}
								foreach (IAgentOriginBase agentOriginBase4 in list2)
								{
									if (!agentOriginBase4.Troop.IsHero && this._bannerBearerLogic != null && mission.Mode != MissionMode.Deployment && this._bannerBearerLogic.GetMissingBannerCount(formation) > 0)
									{
										this._bannerBearerLogic.SpawnBannerBearer(agentOriginBase4, this.IsPlayerSide, formation, this._spawnWithHorses, isReinforcement, num8, num7, true, true, false, null, null, null, mission.IsSallyOutBattle);
									}
									else
									{
										mission.SpawnTroop(agentOriginBase4, this.IsPlayerSide, true, this._spawnWithHorses, isReinforcement, num8, num7, true, true, false, null, null, null, null, formation.FormationIndex, mission.IsSallyOutBattle);
									}
									this._numSpawnedTroops++;
									num7++;
									num4++;
								}
								if (this.ReinforcementSpawnActive)
								{
									this._reinforcementSpawnedUnitCountPerFormation[j].Item1 = num7;
								}
							}
						}
						if (num4 > 0)
						{
							valueTuple3.Item1.QuerySystem.Expire();
						}
						num3 += num4;
						foreach (Formation formation2 in valueTuple3.Item1.FormationsIncludingEmpty)
						{
							if (formation2.CountOfUnits > 0 && formation2.IsSpawning)
							{
								formation2.EndSpawn();
							}
						}
						continue;
						IL_262:
						list3 = MissionGameModels.Current.BattleSpawnModel.GetInitialSpawnAssignments(this._side, valueTuple3.Item2);
						goto IL_280;
					}
				}
				return num3;
			}

			// Token: 0x060038E1 RID: 14561 RVA: 0x000E3C14 File Offset: 0x000E1E14
			public void SetSpawnWithHorses(bool spawnWithHorses)
			{
				this._spawnWithHorses = spawnWithHorses;
			}

			// Token: 0x060038E2 RID: 14562 RVA: 0x000E3C20 File Offset: 0x000E1E20
			private unsafe int ComputeBalancedBatch(MissionAgentSpawnLogic.SpawnPhase activePhase)
			{
				int num = 0;
				if (activePhase != null && activePhase.RemainingSpawnNumber > 0)
				{
					MissionSpawnSettings missionSpawnSettings = *this._spawnLogic.ReinforcementSpawnSettings;
					int reinforcementBatchSize = this._reinforcementBatchSize;
					this._reinforcementBatchSize = (int)((float)this._spawnLogic.BattleSize * missionSpawnSettings.ReinforcementBatchPercentage);
					if (reinforcementBatchSize != this._reinforcementBatchSize)
					{
						this.UpdateReinforcementQuotaRequirement(reinforcementBatchSize);
					}
					int num2 = activePhase.TotalSpawnNumber - activePhase.InitialSpawnedNumber;
					num = MathF.Max(1, this._reservedTroops.Count + (int)((float)num2 * missionSpawnSettings.DesiredReinforcementPercentage));
					num = MathF.Min(num, activePhase.InitialSpawnedNumber - this.NumberOfActiveTroops);
				}
				return num;
			}

			// Token: 0x060038E3 RID: 14563 RVA: 0x000E3CC8 File Offset: 0x000E1EC8
			private unsafe int ComputeFixedBatch(MissionAgentSpawnLogic.SpawnPhase activePhase)
			{
				int result = 0;
				if (activePhase != null && activePhase.RemainingSpawnNumber > 0)
				{
					MissionSpawnSettings missionSpawnSettings = *this._spawnLogic.ReinforcementSpawnSettings;
					float num = (this._side == BattleSideEnum.Defender) ? missionSpawnSettings.DefenderReinforcementBatchPercentage : missionSpawnSettings.AttackerReinforcementBatchPercentage;
					int reinforcementBatchSize = this._reinforcementBatchSize;
					this._reinforcementBatchSize = (int)((float)this._spawnLogic.TotalSpawnNumber * num);
					if (reinforcementBatchSize != this._reinforcementBatchSize)
					{
						this.UpdateReinforcementQuotaRequirement(reinforcementBatchSize);
					}
					result = MathF.Max(1, this._reinforcementBatchSize);
				}
				return result;
			}

			// Token: 0x060038E4 RID: 14564 RVA: 0x000E3D48 File Offset: 0x000E1F48
			private unsafe int ComputeWaveBatch(MissionAgentSpawnLogic.SpawnPhase activePhase)
			{
				int result = 0;
				if (activePhase != null && activePhase.RemainingSpawnNumber > 0 && this._reservedTroops.IsEmpty<IAgentOriginBase>())
				{
					MissionSpawnSettings missionSpawnSettings = *this._spawnLogic.ReinforcementSpawnSettings;
					int reinforcementBatchSize = this._reinforcementBatchSize;
					int num = (int)Math.Max(1f, (float)activePhase.InitialSpawnedNumber * missionSpawnSettings.ReinforcementWavePercentage);
					this._reinforcementBatchSize = num;
					if (reinforcementBatchSize != this._reinforcementBatchSize)
					{
						this.UpdateReinforcementQuotaRequirement(reinforcementBatchSize);
					}
					if (activePhase.InitialSpawnedNumber - activePhase.NumberActiveTroops >= num)
					{
						result = num;
					}
				}
				return result;
			}

			// Token: 0x060038E5 RID: 14565 RVA: 0x000E3DCD File Offset: 0x000E1FCD
			public void SetBannerBearerLogic(BannerBearerLogic bannerBearerLogic)
			{
				this._bannerBearerLogic = bannerBearerLogic;
			}

			// Token: 0x060038E6 RID: 14566 RVA: 0x000E3DD8 File Offset: 0x000E1FD8
			private void UpdateReinforcementQuotaRequirement(int previousBatchSize)
			{
				if (this._reinforcementBatchSize < previousBatchSize)
				{
					for (int i = MathF.Min(this._reservedTroops.Count - 1, previousBatchSize - 1); i >= this._reinforcementBatchSize; i--)
					{
						this._reinforcementQuotaRequirement -= this.GetReservedTroopQuota(i);
					}
					return;
				}
				if (this._reinforcementBatchSize > previousBatchSize)
				{
					int num = MathF.Min(this._reservedTroops.Count - 1, this._reinforcementBatchSize - 1);
					for (int j = previousBatchSize; j <= num; j++)
					{
						this._reinforcementQuotaRequirement += this.GetReservedTroopQuota(j);
					}
				}
			}

			// Token: 0x060038E7 RID: 14567 RVA: 0x000E3E6C File Offset: 0x000E206C
			public void SetReinforcementsNotifiedOnLastBatch(bool value)
			{
				this.ReinforcementsNotifiedOnLastBatch = value;
			}

			// Token: 0x060038E8 RID: 14568 RVA: 0x000E3E78 File Offset: 0x000E2078
			private void ResetReinforcementSpawnedUnitCountsPerFormation()
			{
				for (int i = 0; i < 8; i++)
				{
					this._reinforcementSpawnedUnitCountPerFormation[i].Item1 = 0;
					this._reinforcementSpawnedUnitCountPerFormation[i].Item2 = 0;
				}
				this._reinforcementTroopFormationAssignments.Clear();
				foreach (ValueTuple<IAgentOriginBase, int> valueTuple in MissionGameModels.Current.BattleSpawnModel.GetReinforcementAssignments(this._side, this._reservedTroops))
				{
					int item = valueTuple.Item2;
					this._reinforcementTroopFormationAssignments.Add(valueTuple.Item1, valueTuple.Item2);
					ValueTuple<int, int>[] reinforcementSpawnedUnitCountPerFormation = this._reinforcementSpawnedUnitCountPerFormation;
					int num = item;
					reinforcementSpawnedUnitCountPerFormation[num].Item2 = reinforcementSpawnedUnitCountPerFormation[num].Item2 + 1;
				}
			}

			// Token: 0x060038E9 RID: 14569 RVA: 0x000E3F48 File Offset: 0x000E2148
			public void SetSpawnTroops(bool spawnTroops)
			{
				this.TroopSpawnActive = spawnTroops;
			}

			// Token: 0x060038EA RID: 14570 RVA: 0x000E3F51 File Offset: 0x000E2151
			private int GetReservedTroopQuota(int index)
			{
				if (!this._spawnWithHorses || !this._reservedTroops[index].Troop.IsMounted)
				{
					return 1;
				}
				return 2;
			}

			// Token: 0x060038EB RID: 14571 RVA: 0x000E3F78 File Offset: 0x000E2178
			public void OnInitialSpawnOver()
			{
				foreach (Formation formation in this._spawnedFormations)
				{
					formation.EndSpawn();
				}
			}

			// Token: 0x04001C61 RID: 7265
			private readonly MissionAgentSpawnLogic _spawnLogic;

			// Token: 0x04001C62 RID: 7266
			private readonly BattleSideEnum _side;

			// Token: 0x04001C63 RID: 7267
			private readonly IMissionTroopSupplier _troopSupplier;

			// Token: 0x04001C64 RID: 7268
			private BannerBearerLogic _bannerBearerLogic;

			// Token: 0x04001C65 RID: 7269
			private readonly MBArrayList<Formation> _spawnedFormations;

			// Token: 0x04001C66 RID: 7270
			private bool _spawnWithHorses;

			// Token: 0x04001C67 RID: 7271
			private float _reinforcementBatchPriority;

			// Token: 0x04001C68 RID: 7272
			private int _reinforcementQuotaRequirement;

			// Token: 0x04001C69 RID: 7273
			private int _reinforcementBatchSize;

			// Token: 0x04001C6A RID: 7274
			private int _reinforcementsSpawnedInLastBatch;

			// Token: 0x04001C6B RID: 7275
			private int _numSpawnedTroops;

			// Token: 0x04001C6C RID: 7276
			private readonly List<IAgentOriginBase> _reservedTroops = new List<IAgentOriginBase>();

			// Token: 0x04001C6D RID: 7277
			[TupleElementNames(new string[]
			{
				"team",
				"origins"
			})]
			private List<ValueTuple<Team, List<IAgentOriginBase>>> _troopOriginsToSpawnPerTeam;

			// Token: 0x04001C72 RID: 7282
			[TupleElementNames(new string[]
			{
				"currentTroopIndex",
				"troopCount"
			})]
			private readonly ValueTuple<int, int>[] _reinforcementSpawnedUnitCountPerFormation;

			// Token: 0x04001C73 RID: 7283
			private readonly Dictionary<IAgentOriginBase, int> _reinforcementTroopFormationAssignments;
		}

		// Token: 0x0200052E RID: 1326
		private class SpawnPhase
		{
			// Token: 0x060038EC RID: 14572 RVA: 0x000E3FC4 File Offset: 0x000E21C4
			public void OnInitialTroopsSpawned()
			{
				this.InitialSpawnedNumber = this.InitialSpawnNumber;
				this.InitialSpawnNumber = 0;
			}

			// Token: 0x04001C74 RID: 7284
			public int TotalSpawnNumber;

			// Token: 0x04001C75 RID: 7285
			public int InitialSpawnedNumber;

			// Token: 0x04001C76 RID: 7286
			public int InitialSpawnNumber;

			// Token: 0x04001C77 RID: 7287
			public int RemainingSpawnNumber;

			// Token: 0x04001C78 RID: 7288
			public int NumberActiveTroops;
		}

		// Token: 0x0200052F RID: 1327
		// (Invoke) Token: 0x060038EF RID: 14575
		public delegate void OnPhaseChangedDelegate();
	}
}
