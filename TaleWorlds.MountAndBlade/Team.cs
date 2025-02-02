using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000378 RID: 888
	public class Team : IMissionTeam
	{
		// Token: 0x14000097 RID: 151
		// (add) Token: 0x060030C1 RID: 12481 RVA: 0x000CA008 File Offset: 0x000C8208
		// (remove) Token: 0x060030C2 RID: 12482 RVA: 0x000CA040 File Offset: 0x000C8240
		public event Action<Team, Formation> OnFormationsChanged;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x060030C3 RID: 12483 RVA: 0x000CA078 File Offset: 0x000C8278
		// (remove) Token: 0x060030C4 RID: 12484 RVA: 0x000CA0B0 File Offset: 0x000C82B0
		public event OnOrderIssuedDelegate OnOrderIssued;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060030C5 RID: 12485 RVA: 0x000CA0E8 File Offset: 0x000C82E8
		// (remove) Token: 0x060030C6 RID: 12486 RVA: 0x000CA120 File Offset: 0x000C8320
		public event Action<Formation> OnFormationAIActiveBehaviorChanged;

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000CA155 File Offset: 0x000C8355
		public BattleSideEnum Side { get; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000CA15D File Offset: 0x000C835D
		public Mission Mission { get; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000CA165 File Offset: 0x000C8365
		// (set) Token: 0x060030CA RID: 12490 RVA: 0x000CA16D File Offset: 0x000C836D
		public MBList<Formation> FormationsIncludingEmpty { get; private set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000CA176 File Offset: 0x000C8376
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x000CA17E File Offset: 0x000C837E
		public MBList<Formation> FormationsIncludingSpecialAndEmpty { get; private set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000CA187 File Offset: 0x000C8387
		// (set) Token: 0x060030CE RID: 12494 RVA: 0x000CA18F File Offset: 0x000C838F
		public TeamAIComponent TeamAI { get; private set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000CA198 File Offset: 0x000C8398
		public bool IsPlayerTeam
		{
			get
			{
				return this.Mission.PlayerTeam == this;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000CA1A8 File Offset: 0x000C83A8
		public bool IsPlayerAlly
		{
			get
			{
				return this.Mission.PlayerTeam != null && this.Mission.PlayerTeam.Side == this.Side;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000CA1D1 File Offset: 0x000C83D1
		public bool IsDefender
		{
			get
			{
				return this.Side == BattleSideEnum.Defender;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000CA1DC File Offset: 0x000C83DC
		public bool IsAttacker
		{
			get
			{
				return this.Side == BattleSideEnum.Attacker;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000CA1E7 File Offset: 0x000C83E7
		// (set) Token: 0x060030D4 RID: 12500 RVA: 0x000CA1EF File Offset: 0x000C83EF
		public uint Color { get; private set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000CA1F8 File Offset: 0x000C83F8
		// (set) Token: 0x060030D6 RID: 12502 RVA: 0x000CA200 File Offset: 0x000C8400
		public uint Color2 { get; private set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000CA209 File Offset: 0x000C8409
		public Banner Banner { get; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000CA211 File Offset: 0x000C8411
		public OrderController MasterOrderController
		{
			get
			{
				return this._orderControllers[0];
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x000CA21F File Offset: 0x000C841F
		public OrderController PlayerOrderController
		{
			get
			{
				return this._orderControllers[1];
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000CA22D File Offset: 0x000C842D
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x000CA235 File Offset: 0x000C8435
		public TeamQuerySystem QuerySystem { get; private set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000CA23E File Offset: 0x000C843E
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x000CA246 File Offset: 0x000C8446
		public DetachmentManager DetachmentManager { get; private set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x000CA24F File Offset: 0x000C844F
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x000CA257 File Offset: 0x000C8457
		public bool IsPlayerGeneral { get; private set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x000CA260 File Offset: 0x000C8460
		// (set) Token: 0x060030E1 RID: 12513 RVA: 0x000CA268 File Offset: 0x000C8468
		public bool IsPlayerSergeant { get; private set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000CA271 File Offset: 0x000C8471
		public MBReadOnlyList<Agent> ActiveAgents
		{
			get
			{
				return this._activeAgents;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000CA279 File Offset: 0x000C8479
		public MBReadOnlyList<Agent> TeamAgents
		{
			get
			{
				return this._teamAgents;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000CA281 File Offset: 0x000C8481
		public MBReadOnlyList<ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>> CachedEnemyDataForFleeing
		{
			get
			{
				return this._cachedEnemyDataForFleeing;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000CA289 File Offset: 0x000C8489
		public int TeamIndex
		{
			get
			{
				return this.MBTeam.Index;
			}
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000CA298 File Offset: 0x000C8498
		public Team(MBTeam mbTeam, BattleSideEnum side, Mission mission, uint color = 4294967295U, uint color2 = 4294967295U, Banner banner = null)
		{
			this.MBTeam = mbTeam;
			this.Side = side;
			this.Mission = mission;
			this.Color = color;
			this.Color2 = color2;
			this.Banner = banner;
			this.IsPlayerGeneral = true;
			this.IsPlayerSergeant = false;
			if (this != Team._invalid)
			{
				this.Initialize();
			}
			this.MoraleChangeFactor = 1f;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000CA300 File Offset: 0x000C8500
		public void UpdateCachedEnemyDataForFleeing()
		{
			if (this._cachedEnemyDataForFleeing.IsEmpty<ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>>())
			{
				foreach (Team team in this.Mission.Teams)
				{
					if (team.IsEnemyOf(this))
					{
						foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
						{
							int countOfUnits = formation.CountOfUnits;
							if (countOfUnits > 0)
							{
								WorldPosition medianPosition = formation.QuerySystem.MedianPosition;
								float movementSpeedMaximum = formation.QuerySystem.MovementSpeedMaximum;
								bool item = (formation.QuerySystem.IsCavalryFormation || formation.QuerySystem.IsRangedCavalryFormation) && formation.HasAnyMountedUnit;
								if (countOfUnits == 1)
								{
									Vec2 asVec = medianPosition.AsVec2;
									this._cachedEnemyDataForFleeing.Add(new ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>(movementSpeedMaximum, medianPosition, countOfUnits, asVec, asVec, item));
								}
								else
								{
									Vec2 v = formation.QuerySystem.EstimatedDirection.LeftVec();
									float f = formation.Width / 2f;
									Vec2 item2 = medianPosition.AsVec2 - v * f;
									Vec2 item3 = medianPosition.AsVec2 + v * f;
									this._cachedEnemyDataForFleeing.Add(new ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>(movementSpeedMaximum, medianPosition, countOfUnits, item2, item3, item));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000CA4B8 File Offset: 0x000C86B8
		// (set) Token: 0x060030E9 RID: 12521 RVA: 0x000CA4C0 File Offset: 0x000C86C0
		public float MoraleChangeFactor { get; private set; }

		// Token: 0x060030EA RID: 12522 RVA: 0x000CA4CC File Offset: 0x000C86CC
		private void Initialize()
		{
			this._activeAgents = new MBList<Agent>();
			this._teamAgents = new MBList<Agent>();
			this._cachedEnemyDataForFleeing = new MBList<ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>>();
			if (!GameNetwork.IsReplay)
			{
				this.FormationsIncludingSpecialAndEmpty = new MBList<Formation>(10);
				this.FormationsIncludingEmpty = new MBList<Formation>(8);
				for (int i = 0; i < 10; i++)
				{
					Formation formation = new Formation(this, i);
					this.FormationsIncludingSpecialAndEmpty.Add(formation);
					if (i < 8)
					{
						this.FormationsIncludingEmpty.Add(formation);
					}
					formation.AI.OnActiveBehaviorChanged += this.FormationAI_OnActiveBehaviorChanged;
				}
				if (this.Mission != null)
				{
					this._orderControllers = new List<OrderController>();
					OrderController orderController = new OrderController(this.Mission, this, null);
					this._orderControllers.Add(orderController);
					orderController.OnOrderIssued += this.OrderController_OnOrderIssued;
					OrderController orderController2 = new OrderController(this.Mission, this, null);
					this._orderControllers.Add(orderController2);
					orderController2.OnOrderIssued += this.OrderController_OnOrderIssued;
				}
				this.QuerySystem = new TeamQuerySystem(this);
				this.DetachmentManager = new DetachmentManager(this);
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000CA5E8 File Offset: 0x000C87E8
		public void Reset()
		{
			if (!GameNetwork.IsReplay)
			{
				foreach (Formation formation in this.FormationsIncludingSpecialAndEmpty)
				{
					formation.Reset();
				}
				List<OrderController> orderControllers = this._orderControllers;
				if (orderControllers != null && orderControllers.Count > 2)
				{
					for (int i = this._orderControllers.Count - 1; i >= 2; i--)
					{
						this._orderControllers[i].OnOrderIssued -= this.OrderController_OnOrderIssued;
						this._orderControllers.RemoveAt(i);
					}
				}
				this.QuerySystem = new TeamQuerySystem(this);
			}
			this._teamAgents.Clear();
			this._activeAgents.Clear();
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000CA6BC File Offset: 0x000C88BC
		public void Clear()
		{
			if (!GameNetwork.IsReplay)
			{
				foreach (Formation formation in this.FormationsIncludingSpecialAndEmpty)
				{
					formation.AI.OnActiveBehaviorChanged -= this.FormationAI_OnActiveBehaviorChanged;
				}
			}
			this.Reset();
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000CA72C File Offset: 0x000C892C
		private void OrderController_OnOrderIssued(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController, params object[] delegateParams)
		{
			OnOrderIssuedDelegate onOrderIssued = this.OnOrderIssued;
			if (onOrderIssued == null)
			{
				return;
			}
			onOrderIssued(orderType, appliedFormations, orderController, delegateParams);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000CA743 File Offset: 0x000C8943
		public static bool DoesFirstFormationClassContainSecond(FormationClass f1, FormationClass f2)
		{
			return (f1 & f2) == f2;
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000CA74B File Offset: 0x000C894B
		public static FormationClass GetFormationFormationClass(Formation f)
		{
			if (f.QuerySystem.IsRangedCavalryFormation)
			{
				return FormationClass.HorseArcher;
			}
			if (f.QuerySystem.IsCavalryFormation)
			{
				return FormationClass.Cavalry;
			}
			if (!f.QuerySystem.IsRangedFormation)
			{
				return FormationClass.Infantry;
			}
			return FormationClass.Ranged;
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000CA77B File Offset: 0x000C897B
		public static FormationClass GetPlayerTeamFormationClass(Agent mainAgent)
		{
			if (mainAgent.IsRangedCached && mainAgent.HasMount)
			{
				return FormationClass.HorseArcher;
			}
			if (mainAgent.IsRangedCached)
			{
				return FormationClass.Ranged;
			}
			if (mainAgent.HasMount)
			{
				return FormationClass.Cavalry;
			}
			return FormationClass.Infantry;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000CA7A4 File Offset: 0x000C89A4
		public void AssignPlayerAsSergeantOfFormation(MissionPeer peer, FormationClass formationClass)
		{
			Formation formation = this.GetFormation(formationClass);
			formation.PlayerOwner = peer.ControlledAgent;
			formation.BannerCode = peer.Peer.BannerCode;
			if (peer.IsMine)
			{
				this.PlayerOrderController.Owner = peer.ControlledAgent;
			}
			else
			{
				this.GetOrderControllerOf(peer.ControlledAgent).Owner = peer.ControlledAgent;
			}
			formation.SetControlledByAI(false, false);
			foreach (MissionBehavior missionBehavior in this.Mission.MissionBehaviors)
			{
				missionBehavior.OnAssignPlayerAsSergeantOfFormation(peer.ControlledAgent);
			}
			if (peer.IsMine)
			{
				this.PlayerOrderController.SelectAllFormations(false);
			}
			peer.ControlledFormation = formation;
			if (GameNetwork.IsServer)
			{
				peer.ControlledAgent.UpdateCachedAndFormationValues(false, false);
				if (!peer.IsMine)
				{
					GameNetwork.BeginModuleEventAsServer(peer.GetNetworkPeer());
					GameNetwork.WriteMessage(new AssignFormationToPlayer(peer.GetNetworkPeer(), formationClass));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000CA8B8 File Offset: 0x000C8AB8
		private void FormationAI_OnActiveBehaviorChanged(Formation formation)
		{
			if (formation.CountOfUnits > 0)
			{
				Action<Formation> onFormationAIActiveBehaviorChanged = this.OnFormationAIActiveBehaviorChanged;
				if (onFormationAIActiveBehaviorChanged == null)
				{
					return;
				}
				onFormationAIActiveBehaviorChanged(formation);
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000CA8D4 File Offset: 0x000C8AD4
		public void AddTacticOption(TacticComponent tacticOption)
		{
			if (this.HasTeamAi)
			{
				this.TeamAI.AddTacticOption(tacticOption);
			}
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000CA8EA File Offset: 0x000C8AEA
		public void RemoveTacticOption(Type tacticType)
		{
			if (this.HasTeamAi)
			{
				this.TeamAI.RemoveTacticOption(tacticType);
			}
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000CA900 File Offset: 0x000C8B00
		public void ClearTacticOptions()
		{
			if (this.HasTeamAi)
			{
				this.TeamAI.ClearTacticOptions();
			}
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000CA915 File Offset: 0x000C8B15
		public void ResetTactic()
		{
			if (this.HasTeamAi)
			{
				this.TeamAI.ResetTactic(true);
			}
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000CA92C File Offset: 0x000C8B2C
		public void AddTeamAI(TeamAIComponent teamAI, bool forceNotAIControlled = false)
		{
			this.TeamAI = teamAI;
			foreach (Formation formation in this.FormationsIncludingSpecialAndEmpty)
			{
				formation.SetControlledByAI(!forceNotAIControlled && (this != this.Mission.PlayerTeam || !this.IsPlayerGeneral), false);
			}
			this.TeamAI.InitializeDetachments(this.Mission);
			this.TeamAI.CreateMissionSpecificBehaviors();
			this.TeamAI.ResetTactic(true);
			foreach (Formation formation2 in this.FormationsIncludingSpecialAndEmpty)
			{
				if (formation2.CountOfUnits > 0)
				{
					formation2.AI.Tick();
				}
			}
			this.TeamAI.TickOccasionally();
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000CAA28 File Offset: 0x000C8C28
		public void DelegateCommandToAI()
		{
			foreach (Formation formation in this.FormationsIncludingEmpty)
			{
				formation.SetControlledByAI(true, false);
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000CAA7C File Offset: 0x000C8C7C
		public void RearrangeFormationsAccordingToFilters(List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> MassTransferData)
		{
			List<Formation> list = new List<Formation>();
			foreach (Tuple<Formation, int, Team.TroopFilter, List<Agent>> tuple in MassTransferData)
			{
				tuple.Item1.OnMassUnitTransferStart();
				if (tuple.Item1.GetReadonlyMovementOrderReference() == MovementOrder.MovementOrderStop && tuple.Item1.CountOfUnits > 0)
				{
					list.Add(tuple.Item1);
					tuple.Item1.SetMovementOrder(MovementOrder.MovementOrderMove(tuple.Item1.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None)));
				}
			}
			List<Agent>[] array = new List<Agent>[MassTransferData.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new List<Agent>();
			}
			List<Team.FormationPocket> list2 = new List<Team.FormationPocket>();
			for (int j = 0; j < MassTransferData.Count; j++)
			{
				Team.TroopFilter filter = MassTransferData[j].Item3;
				Func<Agent, int> func = delegate(Agent agent)
				{
					if (((agent != null) ? agent.Character : null) != null)
					{
						return (((filter & Team.TroopFilter.HighTier) == Team.TroopFilter.HighTier) ? agent.Character.GetBattleTier() : 0) + (((filter & Team.TroopFilter.LowTier) == Team.TroopFilter.LowTier) ? (7 - agent.Character.GetBattleTier()) : 0) + (((filter & Team.TroopFilter.Shield) == Team.TroopFilter.Shield && agent.HasShieldCached) ? 10 : 0) + (((filter & Team.TroopFilter.Spear) == Team.TroopFilter.Spear && agent.HasSpearCached) ? 10 : 0) + (((filter & Team.TroopFilter.Thrown) == Team.TroopFilter.Thrown && agent.HasThrownCached) ? 10 : 0) + (((filter & Team.TroopFilter.Armor) == Team.TroopFilter.Armor && MissionGameModels.Current.AgentStatCalculateModel.HasHeavyArmor(agent)) ? 10 : 0) + ((((filter & Team.TroopFilter.Melee) == Team.TroopFilter.Melee && (filter & Team.TroopFilter.Ranged) == (Team.TroopFilter)0 && !agent.IsRangedCached) || ((filter & Team.TroopFilter.Ranged) == Team.TroopFilter.Ranged && (filter & Team.TroopFilter.Melee) == (Team.TroopFilter)0 && agent.IsRangedCached)) ? 100 : 0) + (((filter & Team.TroopFilter.Mount) == Team.TroopFilter.Mount == agent.HasMount) ? 1000 : 0);
					}
					return (((filter & Team.TroopFilter.HighTier) == Team.TroopFilter.HighTier) ? 7 : 0) + (((filter & Team.TroopFilter.LowTier) == Team.TroopFilter.LowTier) ? 7 : 0) + (((filter & Team.TroopFilter.Shield) == Team.TroopFilter.Shield) ? 10 : 0) + (((filter & Team.TroopFilter.Spear) == Team.TroopFilter.Spear) ? 10 : 0) + (((filter & Team.TroopFilter.Thrown) == Team.TroopFilter.Thrown) ? 10 : 0) + (((filter & Team.TroopFilter.Armor) == Team.TroopFilter.Armor) ? 10 : 0) + (((filter & Team.TroopFilter.Melee) == (Team.TroopFilter)0 || (filter & Team.TroopFilter.Ranged) == (Team.TroopFilter)0) ? 100 : 0) + 1000;
				};
				int maxValue = func(null);
				list2.Add(new Team.FormationPocket(func, maxValue, MassTransferData[j].Item2, j));
			}
			list2.RemoveAll((Team.FormationPocket pfamv) => pfamv.TroopCount <= 0);
			list2 = (from pfamv in list2
			orderby pfamv.TroopCount
			select pfamv).ToList<Team.FormationPocket>();
			list2 = (from pfamv in list2
			orderby pfamv.ScoreToSeek descending
			select pfamv).ToList<Team.FormationPocket>();
			List<IFormationUnit> list3 = new List<IFormationUnit>();
			list3 = MassTransferData.SelectMany((Tuple<Formation, int, Team.TroopFilter, List<Agent>> mtd) => mtd.Item1.DetachedUnits.Concat(mtd.Item1.Arrangement.GetAllUnits()).Except(mtd.Item4)).ToList<IFormationUnit>();
			int num = MassTransferData.Sum((Tuple<Formation, int, Team.TroopFilter, List<Agent>> mtd) => mtd.Item4.Count);
			int k = MassTransferData.Sum((Tuple<Formation, int, Team.TroopFilter, List<Agent>> mtd) => mtd.Item1.CountOfUnits) - num;
			int scoreToSeek = list2[0].ScoreToSeek;
			while (k > 0)
			{
				for (int l = 0; l < k; l++)
				{
					Agent agent3 = list3[l] as Agent;
					for (int m = 0; m < list2.Count; m++)
					{
						Team.FormationPocket formationPocket = list2[m];
						int num2 = formationPocket.PriorityFunction(agent3);
						if (scoreToSeek <= formationPocket.ScoreToSeek && num2 >= scoreToSeek)
						{
							array[formationPocket.Index].Add(agent3);
							formationPocket.AddTroop();
							if (formationPocket.IsFormationPocketFilled())
							{
								list2.RemoveAt(m);
							}
							k--;
							list3[l] = list3[k];
							l--;
							break;
						}
						if (num2 > formationPocket.BestFitSoFar)
						{
							formationPocket.BestFitSoFar = num2;
						}
					}
				}
				if (list2.Count == 0)
				{
					break;
				}
				for (int n = 0; n < list2.Count; n++)
				{
					list2[n].UpdateScoreToSeek();
				}
				from pfamv in list2
				orderby pfamv.ScoreToSeek descending
				select pfamv;
				scoreToSeek = list2[0].ScoreToSeek;
			}
			for (int num3 = 0; num3 < array.Length; num3++)
			{
				foreach (Agent agent2 in array[num3])
				{
					agent2.Formation = MassTransferData[num3].Item1;
				}
			}
			foreach (Tuple<Formation, int, Team.TroopFilter, List<Agent>> tuple2 in MassTransferData)
			{
				this.TriggerOnFormationsChanged(tuple2.Item1);
				tuple2.Item1.OnMassUnitTransferEnd();
				if (tuple2.Item1.CountOfUnits > 0 && !tuple2.Item1.OrderPositionIsValid)
				{
					Vec2 averagePositionOfUnits = tuple2.Item1.GetAveragePositionOfUnits(false, false);
					float terrainHeight = this.Mission.Scene.GetTerrainHeight(averagePositionOfUnits, true);
					this.Mission.Scene.GetHeightAtPoint(averagePositionOfUnits, BodyFlags.None, ref terrainHeight);
					Vec3 position = new Vec3(averagePositionOfUnits, terrainHeight, -1f);
					WorldPosition value = new WorldPosition(this.Mission.Scene, UIntPtr.Zero, position, false);
					tuple2.Item1.SetPositioning(new WorldPosition?(value), null, null);
				}
			}
			foreach (Formation formation in list)
			{
				formation.SetMovementOrder(MovementOrder.MovementOrderStop);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000CAF98 File Offset: 0x000C9198
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x000CAFA0 File Offset: 0x000C91A0
		public Formation GeneralsFormation { get; set; }

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000CAFA9 File Offset: 0x000C91A9
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x000CAFB1 File Offset: 0x000C91B1
		public Formation BodyGuardFormation { get; set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000CAFBA File Offset: 0x000C91BA
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x000CAFC2 File Offset: 0x000C91C2
		public Agent GeneralAgent { get; set; }

		// Token: 0x06003100 RID: 12544 RVA: 0x000CAFCC File Offset: 0x000C91CC
		public void OnDeployed()
		{
			foreach (MissionBehavior missionBehavior in this.Mission.MissionBehaviors)
			{
				missionBehavior.OnTeamDeployed(this);
			}
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000CB024 File Offset: 0x000C9224
		public void Tick(float dt)
		{
			if (!this._cachedEnemyDataForFleeing.IsEmpty<ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>>())
			{
				this._cachedEnemyDataForFleeing.Clear();
			}
			if (this.Mission.AllowAiTicking)
			{
				if (this.Mission.RetreatSide != BattleSideEnum.None && this.Side == this.Mission.RetreatSide)
				{
					using (List<Formation>.Enumerator enumerator = this.FormationsIncludingSpecialAndEmpty.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Formation formation = enumerator.Current;
							if (formation.CountOfUnits > 0)
							{
								formation.SetMovementOrder(MovementOrder.MovementOrderRetreat);
							}
						}
						goto IL_A8;
					}
				}
				if (this.TeamAI != null && this.HasBots)
				{
					this.TeamAI.Tick(dt);
				}
			}
			IL_A8:
			if (!GameNetwork.IsReplay)
			{
				this.DetachmentManager.TickDetachments();
				foreach (Formation formation2 in this.FormationsIncludingSpecialAndEmpty)
				{
					if (formation2.CountOfUnits > 0)
					{
						formation2.Tick(dt);
					}
				}
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000CB148 File Offset: 0x000C9348
		public Formation GetFormation(FormationClass formationClass)
		{
			return this.FormationsIncludingSpecialAndEmpty[(int)formationClass];
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000CB158 File Offset: 0x000C9358
		public void SetIsEnemyOf(Team otherTeam, bool isEnemyOf)
		{
			this.MBTeam.SetIsEnemyOf(otherTeam.MBTeam, isEnemyOf);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new TeamSetIsEnemyOf(this.TeamIndex, otherTeam.TeamIndex, isEnemyOf));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000CB1A8 File Offset: 0x000C93A8
		public bool IsEnemyOf(Team otherTeam)
		{
			return this.MBTeam.IsEnemyOf(otherTeam.MBTeam);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000CB1CC File Offset: 0x000C93CC
		public bool IsFriendOf(Team otherTeam)
		{
			return this == otherTeam || !this.MBTeam.IsEnemyOf(otherTeam.MBTeam);
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000CB1F6 File Offset: 0x000C93F6
		public IEnumerable<Agent> Heroes
		{
			get
			{
				Agent main = Agent.Main;
				if (main != null && main.Team == this)
				{
					yield return main;
				}
				yield break;
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000CB206 File Offset: 0x000C9406
		public void AddAgentToTeam(Agent unit)
		{
			this._teamAgents.Add(unit);
			this._activeAgents.Add(unit);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000CB220 File Offset: 0x000C9420
		public void RemoveAgentFromTeam(Agent unit)
		{
			this._teamAgents.Remove(unit);
			this._activeAgents.Remove(unit);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000CB23C File Offset: 0x000C943C
		public void DeactivateAgent(Agent agent)
		{
			this._activeAgents.Remove(agent);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000CB24C File Offset: 0x000C944C
		public void OnAgentRemoved(Agent agent)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				foreach (Formation formation in this.FormationsIncludingSpecialAndEmpty)
				{
					formation.AI.OnAgentRemoved(agent);
				}
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000CB2AC File Offset: 0x000C94AC
		public bool HasBots
		{
			get
			{
				bool result = false;
				for (int i = 0; i < this.ActiveAgents.Count; i++)
				{
					if (!this.ActiveAgents[i].IsMount && !this.ActiveAgents[i].IsPlayerControlled)
					{
						result = true;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000CB2FC File Offset: 0x000C94FC
		public Agent Leader
		{
			get
			{
				if (Agent.Main != null && Agent.Main.Team == this)
				{
					return Agent.Main;
				}
				Agent agent = null;
				for (int i = 0; i < this.ActiveAgents.Count; i++)
				{
					if (agent == null || this.ActiveAgents[i].IsHero)
					{
						agent = this.ActiveAgents[i];
						if (agent.IsHero)
						{
							break;
						}
					}
				}
				return agent;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000CB367 File Offset: 0x000C9567
		// (set) Token: 0x0600310E RID: 12558 RVA: 0x000CB389 File Offset: 0x000C9589
		public static Team Invalid
		{
			get
			{
				if (Team._invalid == null)
				{
					Team._invalid = new Team(MBTeam.InvalidTeam, BattleSideEnum.None, null, uint.MaxValue, uint.MaxValue, null);
				}
				return Team._invalid;
			}
			internal set
			{
				Team._invalid = value;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000CB394 File Offset: 0x000C9594
		public bool IsValid
		{
			get
			{
				return this.MBTeam.IsValid;
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000CB3B0 File Offset: 0x000C95B0
		public override string ToString()
		{
			return this.MBTeam.ToString();
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000CB3D1 File Offset: 0x000C95D1
		public bool HasTeamAi
		{
			get
			{
				return this.TeamAI != null;
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000CB3DC File Offset: 0x000C95DC
		public void OnMissionEnded()
		{
			if (this.HasTeamAi)
			{
				this.TeamAI.OnMissionEnded();
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000CB3F1 File Offset: 0x000C95F1
		public void TriggerOnFormationsChanged(Formation formation)
		{
			Action<Team, Formation> onFormationsChanged = this.OnFormationsChanged;
			if (onFormationsChanged == null)
			{
				return;
			}
			onFormationsChanged(this, formation);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000CB408 File Offset: 0x000C9608
		public OrderController GetOrderControllerOf(Agent agent)
		{
			OrderController orderController = this._orderControllers.FirstOrDefault((OrderController oc) => oc.Owner == agent);
			if (orderController == null)
			{
				orderController = new OrderController(this.Mission, this, agent);
				this._orderControllers.Add(orderController);
				orderController.OnOrderIssued += this.OrderController_OnOrderIssued;
			}
			return orderController;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000CB46F File Offset: 0x000C966F
		public void ExpireAIQuerySystem()
		{
			this.QuerySystem.Expire();
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000CB47C File Offset: 0x000C967C
		public void SetPlayerRole(bool isPlayerGeneral, bool isPlayerSergeant)
		{
			this.IsPlayerGeneral = isPlayerGeneral;
			this.IsPlayerSergeant = isPlayerSergeant;
			foreach (Formation formation in this.FormationsIncludingSpecialAndEmpty)
			{
				formation.SetControlledByAI(this != this.Mission.PlayerTeam || !this.IsPlayerGeneral, false);
			}
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000CB4F8 File Offset: 0x000C96F8
		public bool HasAnyEnemyTeamsWithAgents(bool ignoreMountedAgents)
		{
			foreach (Team team in this.Mission.Teams)
			{
				if (team != this && team.IsEnemyOf(this) && team.ActiveAgents.Count > 0)
				{
					if (ignoreMountedAgents)
					{
						using (List<Agent>.Enumerator enumerator2 = team.ActiveAgents.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (!enumerator2.Current.HasMount)
								{
									return true;
								}
							}
							continue;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000CB5B4 File Offset: 0x000C97B4
		public bool HasAnyFormationsIncludingSpecialThatIsNotEmpty()
		{
			using (List<Formation>.Enumerator enumerator = this.FormationsIncludingSpecialAndEmpty.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CountOfUnits > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000CB610 File Offset: 0x000C9810
		public int GetFormationCount()
		{
			int num = 0;
			using (List<Formation>.Enumerator enumerator = this.FormationsIncludingEmpty.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CountOfUnits > 0)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000CB66C File Offset: 0x000C986C
		public int GetAIControlledFormationCount()
		{
			int num = 0;
			foreach (Formation formation in this.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0 && formation.IsAIControlled)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000CB6D0 File Offset: 0x000C98D0
		public Vec2 GetAveragePositionOfEnemies()
		{
			Vec2 vec = Vec2.Zero;
			int num = 0;
			foreach (Team team in this.Mission.Teams)
			{
				if (team.MBTeam.IsValid && this.IsEnemyOf(team))
				{
					foreach (Agent agent in team.ActiveAgents)
					{
						vec += agent.Position.AsVec2;
						num++;
					}
				}
			}
			if (num > 0)
			{
				vec *= 1f / (float)num;
				return vec;
			}
			return Vec2.Invalid;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000CB7B8 File Offset: 0x000C99B8
		public Vec2 GetAveragePosition()
		{
			Vec2 vec = Vec2.Zero;
			List<Agent> activeAgents = this.ActiveAgents;
			int num = 0;
			foreach (Agent agent in activeAgents)
			{
				vec += agent.Position.AsVec2;
				num++;
			}
			if (num > 0)
			{
				vec *= 1f / (float)num;
			}
			else
			{
				vec = Vec2.Invalid;
			}
			return vec;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000CB844 File Offset: 0x000C9A44
		public WorldPosition GetMedianPosition(Vec2 averagePosition)
		{
			float num = float.MaxValue;
			Agent agent = null;
			foreach (Agent agent2 in this.ActiveAgents)
			{
				float num2 = agent2.Position.AsVec2.DistanceSquared(averagePosition);
				if (num2 <= num)
				{
					agent = agent2;
					num = num2;
				}
			}
			if (agent == null)
			{
				return WorldPosition.Invalid;
			}
			return agent.GetWorldPosition();
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000CB8CC File Offset: 0x000C9ACC
		public Vec2 GetWeightedAverageOfEnemies(Vec2 basePoint)
		{
			Vec2 vec = Vec2.Zero;
			float num = 0f;
			foreach (Team team in this.Mission.Teams)
			{
				if (team.MBTeam.IsValid && this.IsEnemyOf(team))
				{
					foreach (Agent agent in team.ActiveAgents)
					{
						Vec2 asVec = agent.Position.AsVec2;
						float lengthSquared = (basePoint - asVec).LengthSquared;
						float num2 = 1f / lengthSquared;
						vec += asVec * num2;
						num += num2;
					}
				}
			}
			if (num > 0f)
			{
				vec *= 1f / num;
				return vec;
			}
			return Vec2.Invalid;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000CB9E4 File Offset: 0x000C9BE4
		[Conditional("DEBUG")]
		private void TickStandingPointDebug()
		{
		}

		// Token: 0x040014C9 RID: 5321
		public readonly MBTeam MBTeam;

		// Token: 0x040014D2 RID: 5330
		private List<OrderController> _orderControllers;

		// Token: 0x040014D7 RID: 5335
		private MBList<Agent> _activeAgents;

		// Token: 0x040014D8 RID: 5336
		private MBList<Agent> _teamAgents;

		// Token: 0x040014D9 RID: 5337
		private MBList<ValueTuple<float, WorldPosition, int, Vec2, Vec2, bool>> _cachedEnemyDataForFleeing;

		// Token: 0x040014DE RID: 5342
		private static Team _invalid;

		// Token: 0x0200062D RID: 1581
		[Flags]
		public enum TroopFilter
		{
			// Token: 0x04002048 RID: 8264
			HighTier = 256,
			// Token: 0x04002049 RID: 8265
			LowTier = 128,
			// Token: 0x0400204A RID: 8266
			Mount = 64,
			// Token: 0x0400204B RID: 8267
			Ranged = 32,
			// Token: 0x0400204C RID: 8268
			Melee = 16,
			// Token: 0x0400204D RID: 8269
			Shield = 8,
			// Token: 0x0400204E RID: 8270
			Spear = 4,
			// Token: 0x0400204F RID: 8271
			Thrown = 2,
			// Token: 0x04002050 RID: 8272
			Armor = 1
		}

		// Token: 0x0200062E RID: 1582
		private class FormationPocket
		{
			// Token: 0x170009FC RID: 2556
			// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000EA540 File Offset: 0x000E8740
			// (set) Token: 0x06003C62 RID: 15458 RVA: 0x000EA548 File Offset: 0x000E8748
			public Func<Agent, int> PriorityFunction { get; private set; }

			// Token: 0x170009FD RID: 2557
			// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000EA551 File Offset: 0x000E8751
			// (set) Token: 0x06003C64 RID: 15460 RVA: 0x000EA559 File Offset: 0x000E8759
			public int MaxValue { get; private set; }

			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000EA562 File Offset: 0x000E8762
			// (set) Token: 0x06003C66 RID: 15462 RVA: 0x000EA56A File Offset: 0x000E876A
			public int TroopCount { get; private set; }

			// Token: 0x170009FF RID: 2559
			// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000EA573 File Offset: 0x000E8773
			// (set) Token: 0x06003C68 RID: 15464 RVA: 0x000EA57B File Offset: 0x000E877B
			public int Index { get; private set; }

			// Token: 0x17000A00 RID: 2560
			// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000EA584 File Offset: 0x000E8784
			// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000EA58C File Offset: 0x000E878C
			public int AddedTroopCount { get; private set; }

			// Token: 0x06003C6B RID: 15467 RVA: 0x000EA595 File Offset: 0x000E8795
			public FormationPocket(Func<Agent, int> priorityFunction, int maxValue, int troopCount, int index)
			{
				this.PriorityFunction = priorityFunction;
				this.MaxValue = maxValue;
				this.TroopCount = troopCount;
				this.Index = index;
				this.AddedTroopCount = 0;
				this.ScoreToSeek = maxValue;
				this.BestFitSoFar = 0;
			}

			// Token: 0x06003C6C RID: 15468 RVA: 0x000EA5D0 File Offset: 0x000E87D0
			public void AddTroop()
			{
				int addedTroopCount = this.AddedTroopCount;
				this.AddedTroopCount = addedTroopCount + 1;
			}

			// Token: 0x06003C6D RID: 15469 RVA: 0x000EA5ED File Offset: 0x000E87ED
			public bool IsFormationPocketFilled()
			{
				return this.AddedTroopCount >= this.TroopCount;
			}

			// Token: 0x06003C6E RID: 15470 RVA: 0x000EA600 File Offset: 0x000E8800
			public void UpdateScoreToSeek()
			{
				this.ScoreToSeek = this.BestFitSoFar;
				this.BestFitSoFar = 0;
			}

			// Token: 0x04002056 RID: 8278
			public int ScoreToSeek;

			// Token: 0x04002057 RID: 8279
			public int BestFitSoFar;
		}
	}
}
