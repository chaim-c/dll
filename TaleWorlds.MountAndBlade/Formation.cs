using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers.Logic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021A RID: 538
	public sealed class Formation : IFormation
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001D79 RID: 7545 RVA: 0x00067544 File Offset: 0x00065744
		// (remove) Token: 0x06001D7A RID: 7546 RVA: 0x0006757C File Offset: 0x0006577C
		public event Action<Formation, Agent> OnUnitAdded;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001D7B RID: 7547 RVA: 0x000675B4 File Offset: 0x000657B4
		// (remove) Token: 0x06001D7C RID: 7548 RVA: 0x000675EC File Offset: 0x000657EC
		public event Action<Formation, Agent> OnUnitRemoved;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001D7D RID: 7549 RVA: 0x00067624 File Offset: 0x00065824
		// (remove) Token: 0x06001D7E RID: 7550 RVA: 0x0006765C File Offset: 0x0006585C
		public event Action<Formation> OnUnitCountChanged;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001D7F RID: 7551 RVA: 0x00067694 File Offset: 0x00065894
		// (remove) Token: 0x06001D80 RID: 7552 RVA: 0x000676CC File Offset: 0x000658CC
		public event Action<Formation> OnUnitSpacingChanged;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001D81 RID: 7553 RVA: 0x00067704 File Offset: 0x00065904
		// (remove) Token: 0x06001D82 RID: 7554 RVA: 0x0006773C File Offset: 0x0006593C
		public event Action<Formation> OnTick;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001D83 RID: 7555 RVA: 0x00067774 File Offset: 0x00065974
		// (remove) Token: 0x06001D84 RID: 7556 RVA: 0x000677AC File Offset: 0x000659AC
		public event Action<Formation> OnWidthChanged;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001D85 RID: 7557 RVA: 0x000677E4 File Offset: 0x000659E4
		// (remove) Token: 0x06001D86 RID: 7558 RVA: 0x0006781C File Offset: 0x00065A1C
		public event Action<Formation, MovementOrder.MovementOrderEnum> OnBeforeMovementOrderApplied;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001D87 RID: 7559 RVA: 0x00067854 File Offset: 0x00065A54
		// (remove) Token: 0x06001D88 RID: 7560 RVA: 0x0006788C File Offset: 0x00065A8C
		public event Action<Formation, ArrangementOrder.ArrangementOrderEnum> OnAfterArrangementOrderApplied;

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x000678C1 File Offset: 0x00065AC1
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x000678C9 File Offset: 0x00065AC9
		public Formation.RetreatPositionCacheSystem RetreatPositionCache { get; private set; } = new Formation.RetreatPositionCacheSystem(2);

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x000678D2 File Offset: 0x00065AD2
		public int CountOfUnits
		{
			get
			{
				return this.Arrangement.UnitCount + this._detachedUnits.Count;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000678EB File Offset: 0x00065AEB
		public int CountOfDetachedUnits
		{
			get
			{
				return this._detachedUnits.Count;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x000678F8 File Offset: 0x00065AF8
		public int CountOfUndetachableNonPlayerUnits
		{
			get
			{
				return this._undetachableNonPlayerUnitCount;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00067900 File Offset: 0x00065B00
		public int CountOfUnitsWithoutDetachedOnes
		{
			get
			{
				return this.Arrangement.UnitCount + this._looseDetachedUnits.Count;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00067919 File Offset: 0x00065B19
		public MBReadOnlyList<IFormationUnit> UnitsWithoutLooseDetachedOnes
		{
			get
			{
				return this.Arrangement.GetAllUnits();
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00067926 File Offset: 0x00065B26
		public int CountOfUnitsWithoutLooseDetachedOnes
		{
			get
			{
				return this.Arrangement.UnitCount;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00067933 File Offset: 0x00065B33
		public int CountOfDetachableNonplayerUnits
		{
			get
			{
				return this.Arrangement.UnitCount - ((this.IsPlayerTroopInFormation || this.HasPlayerControlledTroop) ? 1 : 0) - this.CountOfUndetachableNonPlayerUnits;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0006795C File Offset: 0x00065B5C
		public Vec2 OrderPosition
		{
			get
			{
				return this._orderPosition.AsVec2;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x00067969 File Offset: 0x00065B69
		public Vec3 OrderGroundPosition
		{
			get
			{
				return this._orderPosition.GetGroundVec3();
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00067976 File Offset: 0x00065B76
		public bool OrderPositionIsValid
		{
			get
			{
				return this._orderPosition.IsValid;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00067983 File Offset: 0x00065B83
		public float Depth
		{
			get
			{
				return this.Arrangement.Depth;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x00067990 File Offset: 0x00065B90
		public float MinimumWidth
		{
			get
			{
				return this.Arrangement.MinimumWidth;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0006799D File Offset: 0x00065B9D
		public float MaximumWidth
		{
			get
			{
				return this.Arrangement.MaximumWidth;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x000679AA File Offset: 0x00065BAA
		public float UnitDiameter
		{
			get
			{
				return Formation.GetDefaultUnitDiameter(this.CalculateHasSignificantNumberOfMounted && !(this.RidingOrder == RidingOrder.RidingOrderDismount));
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x000679CF File Offset: 0x00065BCF
		public Vec2 Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000679D8 File Offset: 0x00065BD8
		public Vec2 CurrentDirection
		{
			get
			{
				return (this.QuerySystem.EstimatedDirection * 0.8f + this.Direction * 0.2f).Normalized();
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00067A17 File Offset: 0x00065C17
		public Vec2 SmoothedAverageUnitPosition
		{
			get
			{
				return this._smoothedAverageUnitPosition;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x00067A1F File Offset: 0x00065C1F
		public int UnitSpacing
		{
			get
			{
				return this._unitSpacing;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x00067A27 File Offset: 0x00065C27
		public MBReadOnlyList<Agent> LooseDetachedUnits
		{
			get
			{
				return this._looseDetachedUnits;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x00067A2F File Offset: 0x00065C2F
		public MBReadOnlyList<Agent> DetachedUnits
		{
			get
			{
				return this._detachedUnits;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00067A37 File Offset: 0x00065C37
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x00067A3F File Offset: 0x00065C3F
		public AttackEntityOrderDetachment AttackEntityOrderDetachment { get; private set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00067A48 File Offset: 0x00065C48
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x00067A50 File Offset: 0x00065C50
		public FormationAI AI { get; private set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x00067A59 File Offset: 0x00065C59
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x00067A64 File Offset: 0x00065C64
		public Formation TargetFormation
		{
			get
			{
				return this._targetFormation;
			}
			private set
			{
				if (this._targetFormation != value)
				{
					this._targetFormation = value;
					this.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						Formation value2 = value;
						agent.SetTargetFormationIndex((value2 != null) ? value2.Index : -1);
					}, null);
				}
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00067AAB File Offset: 0x00065CAB
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x00067AB3 File Offset: 0x00065CB3
		public FormationQuerySystem QuerySystem { get; private set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00067ABC File Offset: 0x00065CBC
		public MBReadOnlyList<IDetachment> Detachments
		{
			get
			{
				return this._detachments;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00067AC4 File Offset: 0x00065CC4
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x00067ACC File Offset: 0x00065CCC
		public int? OverridenUnitCount { get; private set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00067AD5 File Offset: 0x00065CD5
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x00067ADD File Offset: 0x00065CDD
		public bool IsSpawning { get; private set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00067AE6 File Offset: 0x00065CE6
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x00067AEE File Offset: 0x00065CEE
		public bool IsAITickedAfterSplit { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00067AF7 File Offset: 0x00065CF7
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x00067AFF File Offset: 0x00065CFF
		public bool HasPlayerControlledTroop { get; private set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00067B08 File Offset: 0x00065D08
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00067B10 File Offset: 0x00065D10
		public bool IsPlayerTroopInFormation { get; private set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00067B19 File Offset: 0x00065D19
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x00067B21 File Offset: 0x00065D21
		public bool ContainsAgentVisuals { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00067B2A File Offset: 0x00065D2A
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x00067B32 File Offset: 0x00065D32
		public Agent PlayerOwner
		{
			get
			{
				return this._playerOwner;
			}
			set
			{
				this._playerOwner = value;
				this.SetControlledByAI(value == null, false);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00067B7E File Offset: 0x00065D7E
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x00067B46 File Offset: 0x00065D46
		public string BannerCode
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				this._bannerCode = value;
				if (GameNetwork.IsServer)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new InitializeFormation(this, this.Team.TeamIndex, this._bannerCode));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00067B86 File Offset: 0x00065D86
		public bool IsSplittableByAI
		{
			get
			{
				return this.IsAIOwned && this.IsConvenientForTransfer;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00067B98 File Offset: 0x00065D98
		public bool IsAIOwned
		{
			get
			{
				return !this._enforceNotSplittableByAI && (this.IsAIControlled || (!this.Team.IsPlayerGeneral && (!this.Team.IsPlayerSergeant || this.PlayerOwner != Agent.Main)));
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x00067BE7 File Offset: 0x00065DE7
		public bool IsConvenientForTransfer
		{
			get
			{
				return Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege || this.Team.Side != BattleSideEnum.Attacker || this.QuerySystem.InsideCastleUnitCountIncludingUnpositioned == 0;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00067C14 File Offset: 0x00065E14
		public bool EnforceNotSplittableByAI
		{
			get
			{
				return this._enforceNotSplittableByAI;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x00067C1C File Offset: 0x00065E1C
		public bool IsAIControlled
		{
			get
			{
				return this._isAIControlled;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x00067C24 File Offset: 0x00065E24
		public Vec2 OrderLocalAveragePosition
		{
			get
			{
				if (this._orderLocalAveragePositionIsDirty)
				{
					this._orderLocalAveragePositionIsDirty = false;
					this._orderLocalAveragePosition = default(Vec2);
					if (this.UnitsWithoutLooseDetachedOnes.Count > 0)
					{
						int num = 0;
						foreach (IFormationUnit unit in this.UnitsWithoutLooseDetachedOnes)
						{
							Vec2? localPositionOfUnitOrDefault = this.Arrangement.GetLocalPositionOfUnitOrDefault(unit);
							if (localPositionOfUnitOrDefault != null)
							{
								this._orderLocalAveragePosition += localPositionOfUnitOrDefault.Value;
								num++;
							}
						}
						if (num > 0)
						{
							this._orderLocalAveragePosition *= 1f / (float)num;
						}
					}
				}
				return this._orderLocalAveragePosition;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x00067CF8 File Offset: 0x00065EF8
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x00067D00 File Offset: 0x00065F00
		public FacingOrder FacingOrder
		{
			get
			{
				return this._facingOrder;
			}
			set
			{
				this._facingOrder = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x00067D09 File Offset: 0x00065F09
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x00067D14 File Offset: 0x00065F14
		public ArrangementOrder ArrangementOrder
		{
			get
			{
				return this._arrangementOrder;
			}
			set
			{
				if (value.OrderType == this._arrangementOrder.OrderType)
				{
					this._arrangementOrder.SoftUpdate(this);
					return;
				}
				this._arrangementOrder.OnCancel(this);
				int arrangementOrderDefensivenessChange = ArrangementOrder.GetArrangementOrderDefensivenessChange(this._arrangementOrder.OrderEnum, value.OrderEnum);
				if (arrangementOrderDefensivenessChange != 0 && MovementOrder.GetMovementOrderDefensiveness(this._movementOrder.OrderEnum) != 0)
				{
					this._formationOrderDefensivenessFactor += arrangementOrderDefensivenessChange;
					this.UpdateAgentDrivenPropertiesBasedOnOrderDefensiveness();
				}
				if (this.FormOrder.OrderEnum == FormOrder.FormOrderEnum.Custom)
				{
					this.FormOrder = FormOrder.FormOrderCustom(Formation.TransformCustomWidthBetweenArrangementOrientations(this._arrangementOrder.OrderEnum, value.OrderEnum, this.Arrangement.FlankWidth));
				}
				this._arrangementOrder = value;
				this._arrangementOrder.OnApply(this);
				Action<Formation, ArrangementOrder.ArrangementOrderEnum> onAfterArrangementOrderApplied = this.OnAfterArrangementOrderApplied;
				if (onAfterArrangementOrderApplied == null)
				{
					return;
				}
				onAfterArrangementOrderApplied(this, this._arrangementOrder.OrderEnum);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x00067DFA File Offset: 0x00065FFA
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x00067E02 File Offset: 0x00066002
		public FormOrder FormOrder
		{
			get
			{
				return this._formOrder;
			}
			set
			{
				this._formOrder = value;
				this._formOrder.OnApply(this);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00067E17 File Offset: 0x00066017
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x00067E20 File Offset: 0x00066020
		public RidingOrder RidingOrder
		{
			get
			{
				return this._ridingOrder;
			}
			set
			{
				if (this._ridingOrder != value)
				{
					this._ridingOrder = value;
					this.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.SetRidingOrder(value.OrderEnum);
					}, null);
					this.Arrangement_OnShapeChanged();
				}
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x00067E72 File Offset: 0x00066072
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x00067E7C File Offset: 0x0006607C
		public FiringOrder FiringOrder
		{
			get
			{
				return this._firingOrder;
			}
			set
			{
				if (this._firingOrder != value)
				{
					this._firingOrder = value;
					this.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.SetFiringOrder(value.OrderEnum);
					}, null);
				}
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x00067EC8 File Offset: 0x000660C8
		private bool IsSimulationFormation
		{
			get
			{
				return this.Team == null;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00067ED4 File Offset: 0x000660D4
		public bool HasAnyMountedUnit
		{
			get
			{
				if (this._overridenHasAnyMountedUnit != null)
				{
					return this._overridenHasAnyMountedUnit.Value;
				}
				int num = (int)(this.QuerySystem.GetRangedCavalryUnitRatioWithoutExpiration * (float)this.CountOfUnits + 1E-05f);
				int num2 = (int)(this.QuerySystem.GetCavalryUnitRatioWithoutExpiration * (float)this.CountOfUnits + 1E-05f);
				return num + num2 > 0;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x00067F34 File Offset: 0x00066134
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x00067F41 File Offset: 0x00066141
		public float Width
		{
			get
			{
				return this.Arrangement.Width;
			}
			private set
			{
				this.Arrangement.Width = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x00067F4F File Offset: 0x0006614F
		public bool IsDeployment
		{
			get
			{
				return Mission.Current.GetMissionBehavior<BattleDeploymentHandler>() != null;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00067F5E File Offset: 0x0006615E
		public FormationClass RepresentativeClass
		{
			get
			{
				return this._representativeClass;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x00067F66 File Offset: 0x00066166
		public FormationClass LogicalClass
		{
			get
			{
				return this._logicalClass;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x00067F6E File Offset: 0x0006616E
		public IEnumerable<FormationClass> SecondaryLogicalClasses
		{
			get
			{
				FormationClass primaryLogicalClass = this.LogicalClass;
				if (primaryLogicalClass == FormationClass.NumberOfAllFormations)
				{
					yield break;
				}
				List<ValueTuple<FormationClass, int>> list = new List<ValueTuple<FormationClass, int>>();
				for (int i = 0; i < this._logicalClassCounts.Length; i++)
				{
					if (this._logicalClassCounts[i] > 0)
					{
						list.Add(new ValueTuple<FormationClass, int>((FormationClass)i, this._logicalClassCounts[i]));
					}
				}
				if (list.Count > 0)
				{
					list.Sort(Comparer<ValueTuple<FormationClass, int>>.Create(delegate([TupleElementNames(new string[]
					{
						"fClass",
						"count"
					})] ValueTuple<FormationClass, int> x, [TupleElementNames(new string[]
					{
						"fClass",
						"count"
					})] ValueTuple<FormationClass, int> y)
					{
						if (x.Item2 < y.Item2)
						{
							return 1;
						}
						if (x.Item2 <= y.Item2)
						{
							return 0;
						}
						return -1;
					}));
					foreach (ValueTuple<FormationClass, int> valueTuple in list)
					{
						if (valueTuple.Item1 != primaryLogicalClass)
						{
							yield return valueTuple.Item1;
						}
					}
					List<ValueTuple<FormationClass, int>>.Enumerator enumerator = default(List<ValueTuple<FormationClass, int>>.Enumerator);
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00067F7E File Offset: 0x0006617E
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x00067F88 File Offset: 0x00066188
		public IFormationArrangement Arrangement
		{
			get
			{
				return this._arrangement;
			}
			set
			{
				if (this._arrangement != null)
				{
					this._arrangement.OnWidthChanged -= this.Arrangement_OnWidthChanged;
					this._arrangement.OnShapeChanged -= this.Arrangement_OnShapeChanged;
				}
				this._arrangement = value;
				if (this._arrangement != null)
				{
					this._arrangement.OnWidthChanged += this.Arrangement_OnWidthChanged;
					this._arrangement.OnShapeChanged += this.Arrangement_OnShapeChanged;
				}
				this.Arrangement_OnWidthChanged();
				this.Arrangement_OnShapeChanged();
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x00068014 File Offset: 0x00066214
		public FormationClass PhysicalClass
		{
			get
			{
				return this.QuerySystem.MainClass;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x00068021 File Offset: 0x00066221
		public IEnumerable<FormationClass> SecondaryPhysicalClasses
		{
			get
			{
				FormationClass primaryPhysicalClass = this.PhysicalClass;
				if (primaryPhysicalClass != FormationClass.Infantry && this.QuerySystem.InfantryUnitRatio > 0f)
				{
					yield return FormationClass.Infantry;
				}
				if (primaryPhysicalClass != FormationClass.Ranged && this.QuerySystem.RangedUnitRatio > 0f)
				{
					yield return FormationClass.Ranged;
				}
				if (primaryPhysicalClass != FormationClass.Cavalry && this.QuerySystem.CavalryUnitRatio > 0f)
				{
					yield return FormationClass.Cavalry;
				}
				if (primaryPhysicalClass != FormationClass.HorseArcher && this.QuerySystem.RangedCavalryUnitRatio > 0f)
				{
					yield return FormationClass.HorseArcher;
				}
				yield break;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00068031 File Offset: 0x00066231
		public float Interval
		{
			get
			{
				if (this.CalculateHasSignificantNumberOfMounted && !(this.RidingOrder == RidingOrder.RidingOrderDismount))
				{
					return Formation.CavalryInterval(this.UnitSpacing);
				}
				return Formation.InfantryInterval(this.UnitSpacing);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x00068064 File Offset: 0x00066264
		public bool CalculateHasSignificantNumberOfMounted
		{
			get
			{
				if (this._overridenHasAnyMountedUnit != null)
				{
					return this._overridenHasAnyMountedUnit.Value;
				}
				return this.QuerySystem.CavalryUnitRatio + this.QuerySystem.RangedCavalryUnitRatio >= 0.1f;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000680A0 File Offset: 0x000662A0
		public float Distance
		{
			get
			{
				if (this.CalculateHasSignificantNumberOfMounted && !(this.RidingOrder == RidingOrder.RidingOrderDismount))
				{
					return Formation.CavalryDistance(this.UnitSpacing);
				}
				return Formation.InfantryDistance(this.UnitSpacing);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000680D4 File Offset: 0x000662D4
		public Vec2 CurrentPosition
		{
			get
			{
				return this.QuerySystem.GetAveragePositionWithMaxAge(0.1f) + this.CurrentDirection.TransformToParentUnitF(-this.OrderLocalAveragePosition);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0006810F File Offset: 0x0006630F
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x00068117 File Offset: 0x00066317
		public Agent Captain
		{
			get
			{
				return this._captain;
			}
			set
			{
				if (this._captain != value)
				{
					this._captain = value;
					this.OnCaptainChanged();
				}
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0006812F File Offset: 0x0006632F
		public float MinimumDistance
		{
			get
			{
				return Formation.GetDefaultMinimumDistance(this.HasAnyMountedUnit && !(this.RidingOrder == RidingOrder.RidingOrderDismount));
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00068154 File Offset: 0x00066354
		public bool IsLoose
		{
			get
			{
				return ArrangementOrder.GetUnitLooseness(this.ArrangementOrder.OrderEnum);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00068166 File Offset: 0x00066366
		public float MinimumInterval
		{
			get
			{
				return Formation.GetDefaultMinimumInterval(this.HasAnyMountedUnit && !(this.RidingOrder == RidingOrder.RidingOrderDismount));
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0006818B File Offset: 0x0006638B
		public float MaximumInterval
		{
			get
			{
				return Formation.GetDefaultMaximumInterval(this.HasAnyMountedUnit && !(this.RidingOrder == RidingOrder.RidingOrderDismount));
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000681B0 File Offset: 0x000663B0
		public float MaximumDistance
		{
			get
			{
				return Formation.GetDefaultMaximumDistance(this.HasAnyMountedUnit && !(this.RidingOrder == RidingOrder.RidingOrderDismount));
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000681D5 File Offset: 0x000663D5
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x000681DD File Offset: 0x000663DD
		internal bool PostponeCostlyOperations { get; private set; }

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000681E8 File Offset: 0x000663E8
		public Formation(Team team, int index)
		{
			this.Team = team;
			this.Index = index;
			this.FormationIndex = (FormationClass)index;
			this.IsSpawning = false;
			this.Reset();
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0006826C File Offset: 0x0006646C
		~Formation()
		{
			if (!this.IsSimulationFormation)
			{
				Formation._simulationFormationTemp = null;
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000682A0 File Offset: 0x000664A0
		bool IFormation.GetIsLocalPositionAvailable(Vec2 localPosition, Vec2? nearestAvailableUnitPositionLocal)
		{
			Vec2 v = this.Direction.TransformToParentUnitF(localPosition);
			WorldPosition worldPosition = this.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.NavMeshVec3);
			worldPosition.SetVec2(this.OrderPosition + v);
			WorldPosition worldPosition2 = WorldPosition.Invalid;
			if (nearestAvailableUnitPositionLocal != null)
			{
				v = this.Direction.TransformToParentUnitF(nearestAvailableUnitPositionLocal.Value);
				worldPosition2 = this.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.NavMeshVec3);
				worldPosition2.SetVec2(this.OrderPosition + v);
			}
			float manhattanDistance = MathF.Abs(localPosition.x) + MathF.Abs(localPosition.y) + (this.Interval + this.Distance) * 2f;
			return Mission.Current.IsFormationUnitPositionAvailable(ref this._orderPosition, ref worldPosition, ref worldPosition2, manhattanDistance, this.Team);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00068364 File Offset: 0x00066564
		IFormationUnit IFormation.GetClosestUnitTo(Vec2 localPosition, MBList<IFormationUnit> unitsWithSpaces, float? maxDistance)
		{
			Vec2 v = this.Direction.TransformToParentUnitF(localPosition);
			Vec2 position = this.OrderPosition + v;
			return this.GetClosestUnitToAux(position, unitsWithSpaces, maxDistance);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00068398 File Offset: 0x00066598
		IFormationUnit IFormation.GetClosestUnitTo(IFormationUnit targetUnit, MBList<IFormationUnit> unitsWithSpaces, float? maxDistance)
		{
			return this.GetClosestUnitToAux(((Agent)targetUnit).Position.AsVec2, unitsWithSpaces, maxDistance);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000683C0 File Offset: 0x000665C0
		void IFormation.SetUnitToFollow(IFormationUnit unit, IFormationUnit toFollow, Vec2 vector)
		{
			Agent agent = unit as Agent;
			Agent followAgent = toFollow as Agent;
			agent.SetColumnwiseFollowAgent(followAgent, ref vector);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000683E4 File Offset: 0x000665E4
		bool IFormation.BatchUnitPositions(MBArrayList<Vec2i> orderedPositionIndices, MBArrayList<Vec2> orderedLocalPositions, MBList2D<int> availabilityTable, MBList2D<WorldPosition> globalPositionTable, int fileCount, int rankCount)
		{
			if (this._orderPosition.IsValid && this._orderPosition.GetNavMesh() != UIntPtr.Zero)
			{
				Mission.Current.BatchFormationUnitPositions(orderedPositionIndices, orderedLocalPositions, availabilityTable, globalPositionTable, this._orderPosition, this.Direction, fileCount, rankCount);
				return true;
			}
			return false;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00068438 File Offset: 0x00066638
		public WorldPosition CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache worldPositionEnforcedCache)
		{
			if (!this.OrderPositionIsValid)
			{
				Debug.Print(string.Concat(new object[]
				{
					"Formation order position is not valid. Team: ",
					this.Team.TeamIndex,
					", Formation: ",
					(int)this.FormationIndex
				}), 0, Debug.DebugColor.Yellow, 17592186044416UL);
			}
			if (worldPositionEnforcedCache != WorldPosition.WorldPositionEnforcedCache.NavMeshVec3)
			{
				if (worldPositionEnforcedCache == WorldPosition.WorldPositionEnforcedCache.GroundVec3)
				{
					this._orderPosition.GetGroundVec3();
				}
			}
			else
			{
				this._orderPosition.GetNavMeshVec3();
			}
			return this._orderPosition;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000684C4 File Offset: 0x000666C4
		public void SetMovementOrder(MovementOrder input)
		{
			Action<Formation, MovementOrder.MovementOrderEnum> onBeforeMovementOrderApplied = this.OnBeforeMovementOrderApplied;
			if (onBeforeMovementOrderApplied != null)
			{
				onBeforeMovementOrderApplied(this, input.OrderEnum);
			}
			if (input.OrderEnum == MovementOrder.MovementOrderEnum.Invalid)
			{
				input = MovementOrder.MovementOrderStop;
			}
			bool flag = !this._movementOrder.AreOrdersPracticallySame(this._movementOrder, input, this.IsAIControlled);
			if (flag)
			{
				this._movementOrder.OnCancel(this);
			}
			if (flag)
			{
				if (MovementOrder.GetMovementOrderDefensivenessChange(this._movementOrder.OrderEnum, input.OrderEnum) != 0)
				{
					if (MovementOrder.GetMovementOrderDefensiveness(input.OrderEnum) == 0)
					{
						this._formationOrderDefensivenessFactor = 0;
					}
					else
					{
						this._formationOrderDefensivenessFactor = MovementOrder.GetMovementOrderDefensiveness(input.OrderEnum) + ArrangementOrder.GetArrangementOrderDefensiveness(this._arrangementOrder.OrderEnum);
					}
					this.UpdateAgentDrivenPropertiesBasedOnOrderDefensiveness();
				}
				this._movementOrder = input;
				this._movementOrder.OnApply(this);
			}
			this.SetTargetFormation(null);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00068594 File Offset: 0x00066794
		public void SetControlledByAI(bool isControlledByAI, bool enforceNotSplittableByAI = false)
		{
			if (this._isAIControlled != isControlledByAI)
			{
				this._isAIControlled = isControlledByAI;
				if (this._isAIControlled)
				{
					if (this.AI.ActiveBehavior != null && this.CountOfUnits > 0)
					{
						bool forceTickOccasionally = Mission.Current.ForceTickOccasionally;
						Mission.Current.ForceTickOccasionally = true;
						BehaviorComponent activeBehavior = this.AI.ActiveBehavior;
						this.AI.Tick();
						Mission.Current.ForceTickOccasionally = forceTickOccasionally;
						if (activeBehavior == this.AI.ActiveBehavior)
						{
							this.AI.ActiveBehavior.OnBehaviorActivated();
						}
						this.SetMovementOrder(this.AI.ActiveBehavior.CurrentOrder);
					}
					this._enforceNotSplittableByAI = enforceNotSplittableByAI;
					return;
				}
				this._enforceNotSplittableByAI = false;
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0006864E File Offset: 0x0006684E
		public void SetTargetFormation(Formation targetFormation)
		{
			this.TargetFormation = targetFormation;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00068657 File Offset: 0x00066857
		public void OnDeploymentFinished()
		{
			FormationAI ai = this.AI;
			if (ai == null)
			{
				return;
			}
			ai.OnDeploymentFinished();
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00068669 File Offset: 0x00066869
		public void ResetArrangementOrderTickTimer()
		{
			this._arrangementOrderTickOccasionallyTimer = new Timer(Mission.Current.CurrentTime, 0.5f, true);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00068688 File Offset: 0x00066888
		public void SetPositioning(WorldPosition? position = null, Vec2? direction = null, int? unitSpacing = null)
		{
			Vec2 orderPosition = this.OrderPosition;
			Vec2 direction2 = this.Direction;
			WorldPosition? newPosition = null;
			bool flag = false;
			bool flag2 = false;
			if (position != null && position.Value.IsValid)
			{
				if (!this.HasBeenPositioned && !this.IsSimulationFormation)
				{
					this.HasBeenPositioned = true;
				}
				if (position.Value.AsVec2 != this.OrderPosition)
				{
					if (!Mission.Current.IsPositionInsideBoundaries(position.Value.AsVec2))
					{
						Vec2 closestBoundaryPosition = Mission.Current.GetClosestBoundaryPosition(position.Value.AsVec2);
						if (this.OrderPosition != closestBoundaryPosition)
						{
							WorldPosition value = position.Value;
							value.SetVec2(closestBoundaryPosition);
							newPosition = new WorldPosition?(value);
						}
					}
					else
					{
						newPosition = position;
					}
				}
			}
			if (direction != null && this.Direction != direction.Value)
			{
				flag = true;
			}
			if (unitSpacing != null && this.UnitSpacing != unitSpacing.Value)
			{
				flag2 = true;
			}
			if (newPosition != null || flag || flag2)
			{
				this.Arrangement.BeforeFormationFrameChange();
				if (newPosition != null)
				{
					this._orderPosition = newPosition.Value;
				}
				if (flag)
				{
					this._direction = direction.Value;
				}
				if (flag2)
				{
					this._unitSpacing = unitSpacing.Value;
					Action<Formation> onUnitSpacingChanged = this.OnUnitSpacingChanged;
					if (onUnitSpacingChanged != null)
					{
						onUnitSpacingChanged(this);
					}
					this.Arrangement_OnShapeChanged();
					this.Arrangement.AreLocalPositionsDirty = true;
				}
				if (!this.IsSimulationFormation && this.Arrangement.IsTurnBackwardsNecessary(orderPosition, newPosition, direction2, flag, direction))
				{
					this.Arrangement.TurnBackwards();
				}
				this.Arrangement.OnFormationFrameChanged();
				if (newPosition != null)
				{
					this.ArrangementOrder.OnOrderPositionChanged(this, orderPosition);
				}
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00068868 File Offset: 0x00066A68
		public int GetCountOfUnitsWithCondition(Func<Agent, bool> function)
		{
			int num = 0;
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				if (function((Agent)formationUnit))
				{
					num++;
				}
			}
			foreach (Agent arg in this._detachedUnits)
			{
				if (function(arg))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00068918 File Offset: 0x00066B18
		public ref readonly MovementOrder GetReadonlyMovementOrderReference()
		{
			return ref this._movementOrder;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00068920 File Offset: 0x00066B20
		public Agent GetFirstUnit()
		{
			return this.GetUnitWithIndex(0);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00068929 File Offset: 0x00066B29
		public int GetCountOfUnitsBelongingToLogicalClass(FormationClass logicalClass)
		{
			return this._logicalClassCounts[(int)logicalClass];
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00068934 File Offset: 0x00066B34
		public int GetCountOfUnitsBelongingToPhysicalClass(FormationClass physicalClass, bool excludeBannerBearers)
		{
			int num = 0;
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				bool flag = false;
				switch (physicalClass)
				{
				case FormationClass.Infantry:
					flag = (excludeBannerBearers ? QueryLibrary.IsInfantryWithoutBanner((Agent)formationUnit) : QueryLibrary.IsInfantry((Agent)formationUnit));
					break;
				case FormationClass.Ranged:
					flag = (excludeBannerBearers ? QueryLibrary.IsRangedWithoutBanner((Agent)formationUnit) : QueryLibrary.IsRanged((Agent)formationUnit));
					break;
				case FormationClass.Cavalry:
					flag = (excludeBannerBearers ? QueryLibrary.IsCavalryWithoutBanner((Agent)formationUnit) : QueryLibrary.IsCavalry((Agent)formationUnit));
					break;
				case FormationClass.HorseArcher:
					flag = (excludeBannerBearers ? QueryLibrary.IsRangedCavalryWithoutBanner((Agent)formationUnit) : QueryLibrary.IsRangedCavalry((Agent)formationUnit));
					break;
				}
				if (flag)
				{
					num++;
				}
			}
			foreach (Agent a in this._detachedUnits)
			{
				bool flag2 = false;
				switch (physicalClass)
				{
				case FormationClass.Infantry:
					flag2 = (excludeBannerBearers ? QueryLibrary.IsInfantryWithoutBanner(a) : QueryLibrary.IsInfantry(a));
					break;
				case FormationClass.Ranged:
					flag2 = (excludeBannerBearers ? QueryLibrary.IsRangedWithoutBanner(a) : QueryLibrary.IsRanged(a));
					break;
				case FormationClass.Cavalry:
					flag2 = (excludeBannerBearers ? QueryLibrary.IsCavalryWithoutBanner(a) : QueryLibrary.IsCavalry(a));
					break;
				case FormationClass.HorseArcher:
					flag2 = (excludeBannerBearers ? QueryLibrary.IsRangedCavalryWithoutBanner(a) : QueryLibrary.IsRangedCavalry(a));
					break;
				}
				if (flag2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00068AE8 File Offset: 0x00066CE8
		public void SetSpawnIndex(int value = 0)
		{
			this._currentSpawnIndex = value;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00068AF1 File Offset: 0x00066CF1
		public int GetNextSpawnIndex()
		{
			int currentSpawnIndex = this._currentSpawnIndex;
			this._currentSpawnIndex++;
			return currentSpawnIndex;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00068B08 File Offset: 0x00066D08
		public Agent GetUnitWithIndex(int unitIndex)
		{
			if (this.Arrangement.GetAllUnits().Count > unitIndex)
			{
				return (Agent)this.Arrangement.GetAllUnits()[unitIndex];
			}
			unitIndex -= this.Arrangement.GetAllUnits().Count;
			if (this._detachedUnits.Count > unitIndex)
			{
				return this._detachedUnits[unitIndex];
			}
			return null;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00068B70 File Offset: 0x00066D70
		public Vec2 GetAveragePositionOfUnits(bool excludeDetachedUnits, bool excludePlayer)
		{
			int num = excludeDetachedUnits ? this.CountOfUnitsWithoutDetachedOnes : this.CountOfUnits;
			if (num > 0)
			{
				Vec2 vec = Vec2.Zero;
				foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
				{
					Agent agent = (Agent)formationUnit;
					if (!excludePlayer || !agent.IsMainAgent)
					{
						vec += agent.Position.AsVec2;
					}
					else
					{
						num--;
					}
				}
				if (excludeDetachedUnits)
				{
					for (int i = 0; i < this._looseDetachedUnits.Count; i++)
					{
						vec += this._looseDetachedUnits[i].Position.AsVec2;
					}
				}
				else
				{
					for (int j = 0; j < this._detachedUnits.Count; j++)
					{
						vec += this._detachedUnits[j].Position.AsVec2;
					}
				}
				if (num > 0)
				{
					return vec * (1f / (float)num);
				}
			}
			return Vec2.Invalid;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00068CA4 File Offset: 0x00066EA4
		public Agent GetMedianAgent(bool excludeDetachedUnits, bool excludePlayer, Vec2 averagePosition)
		{
			excludeDetachedUnits = (excludeDetachedUnits && this.CountOfUnitsWithoutDetachedOnes > 0);
			excludePlayer = (excludePlayer && (this.CountOfUndetachableNonPlayerUnits > 0 || this.CountOfDetachableNonplayerUnits > 0));
			float num = float.MaxValue;
			Agent result = null;
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				Agent agent = (Agent)formationUnit;
				if (!excludePlayer || !agent.IsMainAgent)
				{
					float num2 = agent.Position.AsVec2.DistanceSquared(averagePosition);
					if (num2 <= num)
					{
						result = agent;
						num = num2;
					}
				}
			}
			if (excludeDetachedUnits)
			{
				for (int i = 0; i < this._looseDetachedUnits.Count; i++)
				{
					float num3 = this._looseDetachedUnits[i].Position.AsVec2.DistanceSquared(averagePosition);
					if (num3 <= num)
					{
						result = this._looseDetachedUnits[i];
						num = num3;
					}
				}
			}
			else
			{
				for (int j = 0; j < this._detachedUnits.Count; j++)
				{
					float num4 = this._detachedUnits[j].Position.AsVec2.DistanceSquared(averagePosition);
					if (num4 <= num)
					{
						result = this._detachedUnits[j];
						num = num4;
					}
				}
			}
			return result;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00068E14 File Offset: 0x00067014
		public Agent.UnderAttackType GetUnderAttackTypeOfUnits(float timeLimit = 3f)
		{
			float num = float.MinValue;
			float num2 = float.MinValue;
			timeLimit += MBCommon.GetTotalMissionTime();
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				num = MathF.Max(num, ((Agent)formationUnit).LastMeleeHitTime);
				num2 = MathF.Max(num2, ((Agent)formationUnit).LastRangedHitTime);
				if (num2 >= 0f && num2 < timeLimit)
				{
					return Agent.UnderAttackType.UnderRangedAttack;
				}
				if (num >= 0f && num < timeLimit)
				{
					return Agent.UnderAttackType.UnderMeleeAttack;
				}
			}
			for (int i = 0; i < this._detachedUnits.Count; i++)
			{
				num = MathF.Max(num, this._detachedUnits[i].LastMeleeHitTime);
				num2 = MathF.Max(num2, this._detachedUnits[i].LastRangedHitTime);
				if (num2 >= 0f && num2 < timeLimit)
				{
					return Agent.UnderAttackType.UnderRangedAttack;
				}
				if (num >= 0f && num < timeLimit)
				{
					return Agent.UnderAttackType.UnderMeleeAttack;
				}
			}
			return Agent.UnderAttackType.NotUnderAttack;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00068F34 File Offset: 0x00067134
		public Agent.MovementBehaviorType GetMovementTypeOfUnits()
		{
			float curMissionTime = MBCommon.GetTotalMissionTime();
			int retreatingCount = 0;
			int attackingCount = 0;
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				if (agent.IsAIControlled && (agent.IsRetreating() || (agent.Formation != null && agent.Formation._movementOrder.OrderType == OrderType.Retreat)))
				{
					int num = retreatingCount;
					retreatingCount = num + 1;
				}
				if (curMissionTime - agent.LastMeleeAttackTime < 3f)
				{
					int num = attackingCount;
					attackingCount = num + 1;
				}
			}, null);
			if (this.CountOfUnits > 0 && (float)retreatingCount / (float)this.CountOfUnits > 0.3f)
			{
				return Agent.MovementBehaviorType.Flee;
			}
			if (attackingCount > 0)
			{
				return Agent.MovementBehaviorType.Engaged;
			}
			return Agent.MovementBehaviorType.Idle;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00068FA0 File Offset: 0x000671A0
		public IEnumerable<Agent> GetUnitsWithoutDetachedOnes()
		{
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				yield return formationUnit as Agent;
			}
			List<IFormationUnit>.Enumerator enumerator = default(List<IFormationUnit>.Enumerator);
			int num;
			for (int i = 0; i < this._looseDetachedUnits.Count; i = num + 1)
			{
				yield return this._looseDetachedUnits[i];
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00068FB0 File Offset: 0x000671B0
		public Vec2 GetWallDirectionOfRelativeFormationLocation(Agent unit)
		{
			if (unit.IsDetachedFromFormation)
			{
				return Vec2.Invalid;
			}
			Vec2? localWallDirectionOfRelativeFormationLocation = this.Arrangement.GetLocalWallDirectionOfRelativeFormationLocation(unit);
			if (localWallDirectionOfRelativeFormationLocation != null)
			{
				return this.Direction.TransformToParentUnitF(localWallDirectionOfRelativeFormationLocation.Value);
			}
			return Vec2.Invalid;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00068FFC File Offset: 0x000671FC
		public Vec2 GetDirectionOfUnit(Agent unit)
		{
			if (unit.IsDetachedFromFormation)
			{
				return unit.GetMovementDirection();
			}
			Vec2? localDirectionOfUnitOrDefault = this.Arrangement.GetLocalDirectionOfUnitOrDefault(unit);
			if (localDirectionOfUnitOrDefault != null)
			{
				return this.Direction.TransformToParentUnitF(localDirectionOfUnitOrDefault.Value);
			}
			return unit.GetMovementDirection();
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0006904C File Offset: 0x0006724C
		private WorldPosition GetOrderPositionOfUnitAux(Agent unit)
		{
			WorldPosition? worldPositionOfUnitOrDefault = this.Arrangement.GetWorldPositionOfUnitOrDefault(unit);
			if (worldPositionOfUnitOrDefault != null)
			{
				return worldPositionOfUnitOrDefault.Value;
			}
			if (!this.OrderPositionIsValid)
			{
				WorldPosition worldPosition = unit.GetWorldPosition();
				Debug.Print(string.Concat(new object[]
				{
					"Formation order position is not valid. Team: ",
					this.Team.TeamIndex,
					", Formation: ",
					(int)this.FormationIndex,
					"Unit Pos: ",
					worldPosition.GetGroundVec3(),
					"Mission Mode: ",
					Mission.Current.Mode.ToString()
				}), 0, Debug.DebugColor.Yellow, 17592186044416UL);
			}
			WorldPosition result = this._movementOrder.CreateNewOrderWorldPosition(this, WorldPosition.WorldPositionEnforcedCache.NavMeshVec3);
			if (result.GetNavMesh() == UIntPtr.Zero || !Mission.Current.IsPositionInsideBoundaries(result.AsVec2))
			{
				return unit.GetWorldPosition();
			}
			return result;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0006914C File Offset: 0x0006734C
		public WorldPosition GetOrderPositionOfUnit(Agent unit)
		{
			if (unit.IsDetachedFromFormation && (this._movementOrder.MovementState != MovementOrder.MovementStateEnum.Charge || !unit.Detachment.IsLoose || unit.Mission.Mode == MissionMode.Deployment))
			{
				WorldFrame? detachmentFrame = this.GetDetachmentFrame(unit);
				if (detachmentFrame != null)
				{
					return detachmentFrame.Value.Origin;
				}
				return WorldPosition.Invalid;
			}
			else
			{
				switch (this._movementOrder.MovementState)
				{
				case MovementOrder.MovementStateEnum.Charge:
					if (unit.Mission.Mode == MissionMode.Deployment)
					{
						return this.GetOrderPositionOfUnitAux(unit);
					}
					if (!this.OrderPositionIsValid)
					{
						WorldPosition worldPosition = unit.GetWorldPosition();
						Debug.Print(string.Concat(new object[]
						{
							"Formation order position is not valid. Team: ",
							this.Team.TeamIndex,
							", Formation: ",
							(int)this.FormationIndex,
							"Unit Pos: ",
							worldPosition.GetGroundVec3()
						}), 0, Debug.DebugColor.Yellow, 17592186044416UL);
					}
					return this._movementOrder.CreateNewOrderWorldPosition(this, WorldPosition.WorldPositionEnforcedCache.None);
				case MovementOrder.MovementStateEnum.Hold:
					return this.GetOrderPositionOfUnitAux(unit);
				case MovementOrder.MovementStateEnum.Retreat:
					return WorldPosition.Invalid;
				case MovementOrder.MovementStateEnum.StandGround:
					return unit.GetWorldPosition();
				default:
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Formation.cs", "GetOrderPositionOfUnit", 1567);
					return WorldPosition.Invalid;
				}
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000692A0 File Offset: 0x000674A0
		public Vec2 GetCurrentGlobalPositionOfUnit(Agent unit, bool blendWithOrderDirection)
		{
			if (unit.IsDetachedFromFormation)
			{
				return unit.Position.AsVec2;
			}
			Vec2? localPositionOfUnitOrDefaultWithAdjustment = this.Arrangement.GetLocalPositionOfUnitOrDefaultWithAdjustment(unit, blendWithOrderDirection ? ((this.QuerySystem.EstimatedInterval - this.Interval) * 0.9f) : 0f);
			if (localPositionOfUnitOrDefaultWithAdjustment != null)
			{
				return (blendWithOrderDirection ? this.CurrentDirection : this.QuerySystem.EstimatedDirection).TransformToParentUnitF(localPositionOfUnitOrDefaultWithAdjustment.Value) + this.CurrentPosition;
			}
			return unit.Position.AsVec2;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0006933C File Offset: 0x0006753C
		public float GetAverageMaximumMovementSpeedOfUnits()
		{
			if (this.CountOfUnitsWithoutDetachedOnes == 0)
			{
				return 0.1f;
			}
			float num = 0f;
			foreach (Agent agent in this.GetUnitsWithoutDetachedOnes())
			{
				num += agent.RunSpeedCached;
			}
			return num / (float)this.CountOfUnitsWithoutDetachedOnes;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000693A8 File Offset: 0x000675A8
		public float GetMovementSpeedOfUnits()
		{
			float? num;
			float? num2;
			this.ArrangementOrder.GetMovementSpeedRestriction(out num, out num2);
			if (num == null && num2 == null)
			{
				num = new float?(1f);
			}
			if (num2 != null)
			{
				if (this.CountOfUnits == 0)
				{
					return 0.1f;
				}
				IEnumerable<Agent> source;
				if (this.CountOfUnitsWithoutDetachedOnes != 0)
				{
					source = this.GetUnitsWithoutDetachedOnes();
				}
				else
				{
					IEnumerable<Agent> detachedUnits = this._detachedUnits;
					source = detachedUnits;
				}
				return source.Min((Agent u) => u.WalkSpeedCached) * num2.Value;
			}
			else
			{
				if (this.CountOfUnits == 0)
				{
					return 0.1f;
				}
				IEnumerable<Agent> source2;
				if (this.CountOfUnitsWithoutDetachedOnes != 0)
				{
					source2 = this.GetUnitsWithoutDetachedOnes();
				}
				else
				{
					IEnumerable<Agent> detachedUnits = this._detachedUnits;
					source2 = detachedUnits;
				}
				float num3 = source2.Average((Agent u) => u.RunSpeedCached);
				FormationQuerySystem.FormationIntegrityDataGroup formationIntegrityData = this.QuerySystem.FormationIntegrityData;
				if (formationIntegrityData.DeviationOfPositionsExcludeFarAgents < formationIntegrityData.AverageMaxUnlimitedSpeedExcludeFarAgents * 0.25f)
				{
					return num3 * num.Value;
				}
				return num3;
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000694C0 File Offset: 0x000676C0
		public float GetFormationPower()
		{
			float sum = 0f;
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				sum += agent.CharacterPowerCached;
			}, null);
			return sum;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000694F8 File Offset: 0x000676F8
		public float GetFormationMeleeFightingPower()
		{
			float sum = 0f;
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				sum += agent.CharacterPowerCached * ((this.FormationIndex == FormationClass.Ranged || this.FormationIndex == FormationClass.HorseArcher) ? 0.4f : 1f);
			}, null);
			return sum;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00069538 File Offset: 0x00067738
		internal IDetachment GetDetachmentForDebug(Agent agent)
		{
			return this.Detachments.FirstOrDefault((IDetachment d) => d.IsAgentUsingOrInterested(agent));
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00069569 File Offset: 0x00067769
		public IDetachment GetDetachmentOrDefault(Agent agent)
		{
			return agent.Detachment;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00069571 File Offset: 0x00067771
		public WorldFrame? GetDetachmentFrame(Agent agent)
		{
			return agent.Detachment.GetAgentFrame(agent);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00069580 File Offset: 0x00067780
		public Vec2 GetMiddleFrontUnitPositionOffset()
		{
			Vec2 localPositionOfReservedUnitPosition = this.Arrangement.GetLocalPositionOfReservedUnitPosition();
			return this.Direction.TransformToParentUnitF(localPositionOfReservedUnitPosition);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000695A8 File Offset: 0x000677A8
		public List<IFormationUnit> GetUnitsToPopWithReferencePosition(int count, Vec3 targetPosition)
		{
			int num = MathF.Min(count, this.Arrangement.UnitCount);
			List<IFormationUnit> list = (num == 0) ? new List<IFormationUnit>() : this.Arrangement.GetUnitsToPop(num, targetPosition);
			int num2 = count - list.Count;
			if (num2 > 0)
			{
				List<Agent> list2 = this._looseDetachedUnits.Take(num2).ToList<Agent>();
				num2 -= list2.Count;
				list.AddRange(list2);
			}
			if (num2 > 0)
			{
				IEnumerable<Agent> enumerable = this._detachedUnits.Take(num2);
				num2 -= enumerable.Count<Agent>();
				list.AddRange(enumerable);
			}
			return list;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00069634 File Offset: 0x00067834
		public List<IFormationUnit> GetUnitsToPop(int count)
		{
			int num = MathF.Min(count, this.Arrangement.UnitCount);
			List<IFormationUnit> list = (num == 0) ? new List<IFormationUnit>() : this.Arrangement.GetUnitsToPop(num);
			int num2 = count - list.Count;
			if (num2 > 0)
			{
				List<Agent> list2 = this._looseDetachedUnits.Take(num2).ToList<Agent>();
				num2 -= list2.Count;
				list.AddRange(list2);
			}
			if (num2 > 0)
			{
				IEnumerable<Agent> enumerable = this._detachedUnits.Take(num2);
				num2 -= enumerable.Count<Agent>();
				list.AddRange(enumerable);
			}
			return list;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000696BE File Offset: 0x000678BE
		public IEnumerable<ValueTuple<WorldPosition, Vec2>> GetUnavailableUnitPositionsAccordingToNewOrder(Formation simulationFormation, in WorldPosition position, in Vec2 direction, float width, int unitSpacing)
		{
			return Formation.GetUnavailableUnitPositionsAccordingToNewOrder(this, simulationFormation, position, direction, this.Arrangement, width, unitSpacing);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000696E0 File Offset: 0x000678E0
		public void GetUnitSpawnFrameWithIndex(int unitIndex, in WorldPosition formationPosition, in Vec2 formationDirection, float width, int unitCount, int unitSpacing, bool isMountedFormation, out WorldPosition? unitSpawnPosition, out Vec2? unitSpawnDirection)
		{
			float num;
			Formation.GetUnitPositionWithIndexAccordingToNewOrder(null, unitIndex, formationPosition, formationDirection, this.Arrangement, width, unitSpacing, unitCount, isMountedFormation, this.Index, out unitSpawnPosition, out unitSpawnDirection, out num);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00069710 File Offset: 0x00067910
		public void GetUnitPositionWithIndexAccordingToNewOrder(Formation simulationFormation, int unitIndex, in WorldPosition formationPosition, in Vec2 formationDirection, float width, int unitSpacing, out WorldPosition? unitSpawnPosition, out Vec2? unitSpawnDirection)
		{
			float num;
			Formation.GetUnitPositionWithIndexAccordingToNewOrder(simulationFormation, unitIndex, formationPosition, formationDirection, this.Arrangement, width, unitSpacing, this.Arrangement.UnitCount, this.HasAnyMountedUnit, this.Index, out unitSpawnPosition, out unitSpawnDirection, out num);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00069750 File Offset: 0x00067950
		public void GetUnitPositionWithIndexAccordingToNewOrder(Formation simulationFormation, int unitIndex, in WorldPosition formationPosition, in Vec2 formationDirection, float width, int unitSpacing, int overridenUnitCount, out WorldPosition? unitPosition, out Vec2? unitDirection)
		{
			float num;
			Formation.GetUnitPositionWithIndexAccordingToNewOrder(simulationFormation, unitIndex, formationPosition, formationDirection, this.Arrangement, width, unitSpacing, overridenUnitCount, this.HasAnyMountedUnit, this.Index, out unitPosition, out unitDirection, out num);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00069788 File Offset: 0x00067988
		public void GetUnitPositionWithIndexAccordingToNewOrder(Formation simulationFormation, int unitIndex, in WorldPosition formationPosition, in Vec2 formationDirection, float width, int unitSpacing, out WorldPosition? unitSpawnPosition, out Vec2? unitSpawnDirection, out float actualWidth)
		{
			Formation.GetUnitPositionWithIndexAccordingToNewOrder(simulationFormation, unitIndex, formationPosition, formationDirection, this.Arrangement, width, unitSpacing, this.Arrangement.UnitCount, this.HasAnyMountedUnit, this.Index, out unitSpawnPosition, out unitSpawnDirection, out actualWidth);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000697C8 File Offset: 0x000679C8
		public bool HasUnitsWithCondition(Func<Agent, bool> function)
		{
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				if (function((Agent)formationUnit))
				{
					return true;
				}
			}
			for (int i = 0; i < this._detachedUnits.Count; i++)
			{
				if (function(this._detachedUnits[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0006985C File Offset: 0x00067A5C
		public bool HasUnitsWithCondition(Func<Agent, bool> function, out Agent result)
		{
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				if (function((Agent)formationUnit))
				{
					result = (Agent)formationUnit;
					return true;
				}
			}
			for (int i = 0; i < this._detachedUnits.Count; i++)
			{
				if (function(this._detachedUnits[i]))
				{
					result = this._detachedUnits[i];
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00069908 File Offset: 0x00067B08
		public bool HasAnyEnemyFormationsThatIsNotEmpty()
		{
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.IsEnemyOf(this.Team))
				{
					using (List<Formation>.Enumerator enumerator2 = team.FormationsIncludingSpecialAndEmpty.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CountOfUnits > 0)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000699B0 File Offset: 0x00067BB0
		public bool HasUnitWithConditionLimitedRandom(Func<Agent, bool> function, int startingIndex, int willBeCheckedUnitCount, out Agent resultAgent)
		{
			int unitCount = this.Arrangement.UnitCount;
			int count = this._detachedUnits.Count;
			if (unitCount + count <= willBeCheckedUnitCount)
			{
				return this.HasUnitsWithCondition(function, out resultAgent);
			}
			for (int i = 0; i < willBeCheckedUnitCount; i++)
			{
				if (startingIndex < unitCount)
				{
					int index = MBRandom.RandomInt(unitCount);
					if (function((Agent)this.Arrangement.GetAllUnits()[index]))
					{
						resultAgent = (Agent)this.Arrangement.GetAllUnits()[index];
						return true;
					}
				}
				else if (count > 0)
				{
					int index = MBRandom.RandomInt(count);
					if (function(this._detachedUnits[index]))
					{
						resultAgent = this._detachedUnits[index];
						return true;
					}
				}
			}
			resultAgent = null;
			return false;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00069A6C File Offset: 0x00067C6C
		public int[] CollectUnitIndices()
		{
			if (this._agentIndicesCache == null || this._agentIndicesCache.Length != this.CountOfUnits)
			{
				this._agentIndicesCache = new int[this.CountOfUnits];
			}
			int num = 0;
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				this._agentIndicesCache[num] = ((Agent)formationUnit).Index;
				num++;
			}
			for (int i = 0; i < this._detachedUnits.Count; i++)
			{
				this._agentIndicesCache[num] = this._detachedUnits[i].Index;
				num++;
			}
			return this._agentIndicesCache;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00069B38 File Offset: 0x00067D38
		public void ApplyActionOnEachUnit(Action<Agent> action, Agent ignoreAgent = null)
		{
			if (ignoreAgent == null)
			{
				foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
				{
					Agent obj = (Agent)formationUnit;
					action(obj);
				}
				for (int i = 0; i < this._detachedUnits.Count; i++)
				{
					action(this._detachedUnits[i]);
				}
				return;
			}
			foreach (IFormationUnit formationUnit2 in this.Arrangement.GetAllUnits())
			{
				Agent agent = (Agent)formationUnit2;
				if (agent != ignoreAgent)
				{
					action(agent);
				}
			}
			for (int j = 0; j < this._detachedUnits.Count; j++)
			{
				Agent agent2 = this._detachedUnits[j];
				if (agent2 != ignoreAgent)
				{
					action(agent2);
				}
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00069C48 File Offset: 0x00067E48
		public void ApplyActionOnEachAttachedUnit(Action<Agent> action)
		{
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				Agent obj = (Agent)formationUnit;
				action(obj);
			}
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00069CA8 File Offset: 0x00067EA8
		public void ApplyActionOnEachDetachedUnit(Action<Agent> action)
		{
			for (int i = 0; i < this._detachedUnits.Count; i++)
			{
				action(this._detachedUnits[i]);
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00069CE0 File Offset: 0x00067EE0
		public void ApplyActionOnEachUnitViaBackupList(Action<Agent> action)
		{
			if (this.Arrangement.GetAllUnits().Count > 0)
			{
				foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits().ToArray())
				{
					action((Agent)formationUnit);
				}
			}
			if (this._detachedUnits.Count > 0)
			{
				foreach (Agent obj in this._detachedUnits.ToArray())
				{
					action(obj);
				}
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00069D64 File Offset: 0x00067F64
		public void ApplyActionOnEachUnit(Action<Agent, List<WorldPosition>> action, List<WorldPosition> list)
		{
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				action((Agent)formationUnit, list);
			}
			for (int i = 0; i < this._detachedUnits.Count; i++)
			{
				action(this._detachedUnits[i], list);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00069DEC File Offset: 0x00067FEC
		public int CountUnitsOnNavMeshIDMod10(int navMeshID, bool includeOnlyPositionedUnits)
		{
			int num = 0;
			foreach (IFormationUnit formationUnit in this.Arrangement.GetAllUnits())
			{
				if (((Agent)formationUnit).GetCurrentNavigationFaceId() % 10 == navMeshID && (!includeOnlyPositionedUnits || this.Arrangement.GetUnpositionedUnits() == null || this.Arrangement.GetUnpositionedUnits().IndexOf(formationUnit) < 0))
				{
					num++;
				}
			}
			if (!includeOnlyPositionedUnits)
			{
				using (List<Agent>.Enumerator enumerator2 = this._detachedUnits.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.GetCurrentNavigationFaceId() % 10 == navMeshID)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00069EC8 File Offset: 0x000680C8
		public void OnAgentControllerChanged(Agent agent, Agent.ControllerType oldController)
		{
			Agent.ControllerType controller = agent.Controller;
			if (oldController != Agent.ControllerType.Player && controller == Agent.ControllerType.Player)
			{
				this.HasPlayerControlledTroop = true;
				if (!GameNetwork.IsMultiplayer)
				{
					this.TryRelocatePlayerUnit();
				}
				if (!agent.IsDetachableFromFormation)
				{
					this.OnUndetachableNonPlayerUnitRemoved(agent);
					return;
				}
			}
			else if (oldController == Agent.ControllerType.Player && controller != Agent.ControllerType.Player)
			{
				this.HasPlayerControlledTroop = false;
				if (!agent.IsDetachableFromFormation)
				{
					this.OnUndetachableNonPlayerUnitAdded(agent);
				}
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00069F26 File Offset: 0x00068126
		public void OnMassUnitTransferStart()
		{
			this.PostponeCostlyOperations = true;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00069F30 File Offset: 0x00068130
		public void OnMassUnitTransferEnd()
		{
			this.ReapplyFormOrder();
			this.QuerySystem.Expire();
			this.Team.QuerySystem.ExpireAfterUnitAddRemove();
			if (this._logicalClassNeedsUpdate)
			{
				this.CalculateLogicalClass();
			}
			if (this.CountOfUnits == 0)
			{
				this._representativeClass = FormationClass.NumberOfAllFormations;
			}
			if (Mission.Current.IsTeleportingAgents)
			{
				this.SetPositioning(new WorldPosition?(this._orderPosition), null, null);
				this.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					agent.UpdateCachedAndFormationValues(true, false);
				}, null);
			}
			this.PostponeCostlyOperations = false;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00069FD8 File Offset: 0x000681D8
		public void OnBatchUnitRemovalStart()
		{
			this.PostponeCostlyOperations = true;
			this.Arrangement.OnBatchRemoveStart();
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00069FEC File Offset: 0x000681EC
		public void OnBatchUnitRemovalEnd()
		{
			this.Arrangement.OnBatchRemoveEnd();
			this.ReapplyFormOrder();
			this.QuerySystem.ExpireAfterUnitAddRemove();
			this.Team.QuerySystem.ExpireAfterUnitAddRemove();
			if (this._logicalClassNeedsUpdate)
			{
				this.CalculateLogicalClass();
			}
			this.PostponeCostlyOperations = false;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0006A03C File Offset: 0x0006823C
		public void OnUnitAddedOrRemoved()
		{
			if (!this.PostponeCostlyOperations)
			{
				this.ReapplyFormOrder();
				this.QuerySystem.ExpireAfterUnitAddRemove();
				Team team = this.Team;
				if (team != null)
				{
					team.QuerySystem.ExpireAfterUnitAddRemove();
				}
			}
			Action<Formation> onUnitCountChanged = this.OnUnitCountChanged;
			if (onUnitCountChanged == null)
			{
				return;
			}
			onUnitCountChanged(this);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0006A089 File Offset: 0x00068289
		public void OnAgentLostMount(Agent agent)
		{
			if (!agent.IsDetachedFromFormation)
			{
				this._arrangement.OnUnitLostMount(agent);
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0006A09F File Offset: 0x0006829F
		public void OnFormationDispersed()
		{
			this.Arrangement.OnFormationDispersed();
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				agent.UpdateCachedAndFormationValues(true, false);
			}, null);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0006A0D2 File Offset: 0x000682D2
		public void OnUnitDetachmentChanged(Agent unit, bool isOldDetachmentLoose, bool isNewDetachmentLoose)
		{
			if (isOldDetachmentLoose && !isNewDetachmentLoose)
			{
				this._looseDetachedUnits.Remove(unit);
				return;
			}
			if (!isOldDetachmentLoose && isNewDetachmentLoose)
			{
				this._looseDetachedUnits.Add(unit);
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0006A0FC File Offset: 0x000682FC
		public void OnUndetachableNonPlayerUnitAdded(Agent unit)
		{
			if (unit.Formation == this && !unit.IsPlayerControlled)
			{
				this._undetachableNonPlayerUnitCount++;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0006A123 File Offset: 0x00068323
		public void OnUndetachableNonPlayerUnitRemoved(Agent unit)
		{
			if (unit.Formation == this && !unit.IsPlayerControlled)
			{
				this._undetachableNonPlayerUnitCount--;
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0006A14A File Offset: 0x0006834A
		public void ResetMovementOrderPositionCache()
		{
			this._movementOrder.ResetPositionCache();
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0006A158 File Offset: 0x00068358
		public void Reset()
		{
			this.Arrangement = new LineFormation(this, true);
			this._arrangementOrderTickOccasionallyTimer = new Timer(Mission.Current.CurrentTime, 0.5f, true);
			this.ResetAux();
			this.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			this._enforceNotSplittableByAI = false;
			this.ContainsAgentVisuals = false;
			this.PlayerOwner = null;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0006A1B4 File Offset: 0x000683B4
		public IEnumerable<Formation> Split(int count = 2)
		{
			foreach (Formation formation in this.Team.FormationsIncludingEmpty)
			{
				formation.PostponeCostlyOperations = true;
			}
			IEnumerable<Formation> enumerable = this.Team.MasterOrderController.SplitFormation(this, count);
			if (enumerable.Count<Formation>() > 1 && this.Team != null)
			{
				foreach (Formation formation2 in enumerable)
				{
					formation2.QuerySystem.Expire();
				}
			}
			foreach (Formation formation3 in this.Team.FormationsIncludingEmpty)
			{
				formation3.CalculateLogicalClass();
				formation3.PostponeCostlyOperations = false;
			}
			if (this.CountOfUnits == 0)
			{
				this._representativeClass = FormationClass.NumberOfAllFormations;
			}
			return enumerable;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0006A2C4 File Offset: 0x000684C4
		public void TransferUnits(Formation target, int unitCount)
		{
			this.PostponeCostlyOperations = true;
			target.PostponeCostlyOperations = true;
			this.Team.MasterOrderController.TransferUnits(this, target, unitCount);
			this.CalculateLogicalClass();
			target.CalculateLogicalClass();
			if (this.CountOfUnits == 0)
			{
				this._representativeClass = FormationClass.NumberOfAllFormations;
			}
			this.PostponeCostlyOperations = false;
			target.PostponeCostlyOperations = false;
			this.QuerySystem.Expire();
			target.QuerySystem.Expire();
			this.Team.QuerySystem.ExpireAfterUnitAddRemove();
			target.Team.QuerySystem.ExpireAfterUnitAddRemove();
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0006A354 File Offset: 0x00068554
		public void TransferUnitsAux(Formation target, int unitCount, bool isPlayerOrder, bool useSelectivePop)
		{
			if (!isPlayerOrder && !this.IsSplittableByAI)
			{
				return;
			}
			MBDebug.Print(string.Concat(new object[]
			{
				this.FormationIndex.GetName(),
				" has ",
				this.CountOfUnits,
				" units, ",
				target.FormationIndex.GetName(),
				" has ",
				target.CountOfUnits,
				" units"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			MBDebug.Print(string.Concat(new object[]
			{
				this.Team.Side,
				" ",
				this.FormationIndex.GetName(),
				" transfers ",
				unitCount,
				" units to ",
				target.FormationIndex.GetName()
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			if (unitCount == 0)
			{
				return;
			}
			if (target.CountOfUnits == 0)
			{
				target.CopyOrdersFrom(this);
				target.SetPositioning(new WorldPosition?(this._orderPosition), new Vec2?(this._direction), new int?(this._unitSpacing));
			}
			BattleBannerBearersModel battleBannerBearersModel = MissionGameModels.Current.BattleBannerBearersModel;
			List<IFormationUnit> list;
			if (battleBannerBearersModel.GetFormationBanner(this) == null)
			{
				list = (useSelectivePop ? this.GetUnitsToPopWithReferencePosition(unitCount, target.OrderPositionIsValid ? target.OrderPosition.ToVec3(0f) : target.QuerySystem.MedianPosition.GetGroundVec3()) : this.GetUnitsToPop(unitCount).ToList<IFormationUnit>());
			}
			else
			{
				List<Agent> formationBannerBearers = battleBannerBearersModel.GetFormationBannerBearers(this);
				int count = Math.Min(this.CountOfUnits, unitCount + formationBannerBearers.Count);
				list = (useSelectivePop ? this.GetUnitsToPopWithReferencePosition(count, target.OrderPositionIsValid ? target.OrderPosition.ToVec3(0f) : target.QuerySystem.MedianPosition.GetGroundVec3()) : this.GetUnitsToPop(count).ToList<IFormationUnit>());
				foreach (Agent item in formationBannerBearers)
				{
					if (list.Count <= unitCount)
					{
						break;
					}
					list.Remove(item);
				}
				if (list.Count > unitCount)
				{
					int num = list.Count - unitCount;
					list.RemoveRange(list.Count - num, num);
				}
			}
			if (battleBannerBearersModel.GetFormationBanner(target) != null)
			{
				foreach (Agent agent in battleBannerBearersModel.GetFormationBannerBearers(target))
				{
					if (agent.Formation == this && !list.Contains(agent))
					{
						int num2 = list.FindIndex(delegate(IFormationUnit unit)
						{
							Agent agent2;
							return (agent2 = (unit as Agent)) != null && agent2.Banner == null;
						});
						if (num2 < 0)
						{
							break;
						}
						list[num2] = agent;
					}
				}
			}
			foreach (IFormationUnit formationUnit in list)
			{
				((Agent)formationUnit).Formation = target;
			}
			this.Team.TriggerOnFormationsChanged(this);
			this.Team.TriggerOnFormationsChanged(target);
			MBDebug.Print(string.Concat(new object[]
			{
				this.FormationIndex.GetName(),
				" has ",
				this.CountOfUnits,
				" units, ",
				target.FormationIndex.GetName(),
				" has ",
				target.CountOfUnits,
				" units"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0006A728 File Offset: 0x00068928
		[Conditional("DEBUG")]
		public void DebugArrangements()
		{
			foreach (Team team in Mission.Current.Teams)
			{
				foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						formation.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							agent.AgentVisuals.SetContourColor(null, true);
						}, null);
					}
				}
			}
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				agent.AgentVisuals.SetContourColor(new uint?(4294901760U), true);
			}, null);
			Vec3 v = this.Direction.ToVec3(0f);
			v.RotateAboutZ(1.5707964f);
			bool isSimulationFormation = this.IsSimulationFormation;
			v * this.Width * 0.5f;
			this.Direction.ToVec3(0f) * this.Depth * 0.5f;
			bool orderPositionIsValid = this.OrderPositionIsValid;
			this.QuerySystem.MedianPosition.SetVec2(this.CurrentPosition);
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				WorldPosition orderPositionOfUnit = this.GetOrderPositionOfUnit(agent);
				if (orderPositionOfUnit.IsValid)
				{
					Vec2 v2 = this.GetDirectionOfUnit(agent);
					v2.Normalize();
					v2 *= 0.1f;
					orderPositionOfUnit.GetGroundVec3() + v2.ToVec3(0f);
					orderPositionOfUnit.GetGroundVec3() - v2.LeftVec().ToVec3(0f);
					orderPositionOfUnit.GetGroundVec3() + v2.LeftVec().ToVec3(0f);
					string.Concat(new object[]
					{
						"(",
						((IFormationUnit)agent).FormationFileIndex,
						",",
						((IFormationUnit)agent).FormationRankIndex,
						")"
					});
				}
			}, null);
			bool orderPositionIsValid2 = this.OrderPositionIsValid;
			foreach (IDetachment detachment in this.Detachments)
			{
				UsableMachine usableMachine = detachment as UsableMachine;
				RangedSiegeWeapon rangedSiegeWeapon = detachment as RangedSiegeWeapon;
			}
			if (this.Arrangement is ColumnFormation)
			{
				this.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					agent.GetFollowedUnit();
					string.Concat(new object[]
					{
						"(",
						((IFormationUnit)agent).FormationFileIndex,
						",",
						((IFormationUnit)agent).FormationRankIndex,
						")"
					});
				}, null);
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0006A928 File Offset: 0x00068B28
		public void AddUnit(Agent unit)
		{
			bool countOfUnits = this.CountOfUnits != 0;
			if (this.Arrangement.AddUnit(unit) && Mission.Current.HasMissionBehavior<AmmoSupplyLogic>() && Mission.Current.GetMissionBehavior<AmmoSupplyLogic>().IsAgentEligibleForAmmoSupply(unit))
			{
				unit.SetScriptedCombatFlags(unit.GetScriptedCombatFlags() | Agent.AISpecialCombatModeFlags.IgnoreAmmoLimitForRangeCalculation);
				unit.ResetAiWaitBeforeShootFactor();
				unit.UpdateAgentStats();
			}
			if (unit.IsPlayerControlled)
			{
				this.HasPlayerControlledTroop = true;
			}
			if (unit.IsPlayerTroop)
			{
				this.IsPlayerTroopInFormation = true;
			}
			if (!unit.IsDetachableFromFormation && !unit.IsPlayerControlled)
			{
				this.OnUndetachableNonPlayerUnitAdded(unit);
			}
			if (unit.Character != null)
			{
				FormationClass formationClass = this.Team.Mission.GetAgentTroopClass(this.Team.Side, unit.Character).DefaultClass();
				this._logicalClassCounts[(int)formationClass]++;
				if (this._logicalClass != formationClass)
				{
					if (this.PostponeCostlyOperations)
					{
						this._logicalClassNeedsUpdate = true;
					}
					else
					{
						this.CalculateLogicalClass();
						this._logicalClassNeedsUpdate = false;
					}
				}
			}
			this._movementOrder.OnUnitJoinOrLeave(this, unit, true);
			Formation targetFormation = this.TargetFormation;
			unit.SetTargetFormationIndex((targetFormation != null) ? targetFormation.Index : -1);
			unit.SetFiringOrder(this.FiringOrder.OrderEnum);
			unit.SetRidingOrder(this.RidingOrder.OrderEnum);
			this.OnUnitAddedOrRemoved();
			Action<Formation, Agent> onUnitAdded = this.OnUnitAdded;
			if (onUnitAdded != null)
			{
				onUnitAdded(this, unit);
			}
			if (!countOfUnits && this.CountOfUnits > 0)
			{
				TeamAIComponent teamAI = this.Team.TeamAI;
				if (teamAI == null)
				{
					return;
				}
				teamAI.OnUnitAddedToFormationForTheFirstTime(this);
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0006AAA4 File Offset: 0x00068CA4
		public void RemoveUnit(Agent unit)
		{
			if (unit.IsDetachedFromFormation)
			{
				unit.Detachment.RemoveAgent(unit);
				this._detachedUnits.Remove(unit);
				this._looseDetachedUnits.Remove(unit);
				unit.Detachment = null;
				unit.DetachmentWeight = -1f;
			}
			else
			{
				this.Arrangement.RemoveUnit(unit);
			}
			if (unit.Character != null)
			{
				FormationClass formationClass = this.Team.Mission.GetAgentTroopClass(this.Team.Side, unit.Character).DefaultClass();
				this._logicalClassCounts[(int)formationClass]--;
				if (this._logicalClass == formationClass)
				{
					if (this.PostponeCostlyOperations)
					{
						this._logicalClassNeedsUpdate = true;
					}
					else
					{
						this.CalculateLogicalClass();
						this._logicalClassNeedsUpdate = false;
					}
				}
			}
			if (unit.IsPlayerTroop)
			{
				this.IsPlayerTroopInFormation = false;
			}
			if (unit.IsPlayerControlled)
			{
				this.HasPlayerControlledTroop = false;
			}
			if (unit == this.Captain && !unit.CanLeadFormationsRemotely)
			{
				this.Captain = null;
			}
			if (!unit.IsDetachableFromFormation && !unit.IsPlayerControlled)
			{
				this.OnUndetachableNonPlayerUnitRemoved(unit);
			}
			if (Mission.Current.Mode != MissionMode.Deployment && !this.IsAIControlled && this.CountOfUnits == 0)
			{
				this.SetControlledByAI(true, false);
			}
			this._movementOrder.OnUnitJoinOrLeave(this, unit, false);
			unit.SetTargetFormationIndex(-1);
			unit.SetFiringOrder(FiringOrder.RangedWeaponUsageOrderEnum.FireAtWill);
			unit.SetRidingOrder(RidingOrder.RidingOrderEnum.Free);
			this.OnUnitAddedOrRemoved();
			Action<Formation, Agent> onUnitRemoved = this.OnUnitRemoved;
			if (onUnitRemoved == null)
			{
				return;
			}
			onUnitRemoved(this, unit);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0006AC13 File Offset: 0x00068E13
		public void DetachUnit(Agent unit, bool isLoose)
		{
			this.Arrangement.RemoveUnit(unit);
			this._detachedUnits.Add(unit);
			if (isLoose)
			{
				this._looseDetachedUnits.Add(unit);
			}
			unit.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefaultDetached);
			this.OnUnitAttachedOrDetached();
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0006AC4C File Offset: 0x00068E4C
		public void AttachUnit(Agent unit)
		{
			this._detachedUnits.Remove(unit);
			this._looseDetachedUnits.Remove(unit);
			this.Arrangement.AddUnit(unit);
			unit.Detachment = null;
			unit.DetachmentWeight = -1f;
			this._movementOrder.OnUnitJoinOrLeave(this, unit, true);
			this.OnUnitAttachedOrDetached();
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0006ACA8 File Offset: 0x00068EA8
		public void SwitchUnitLocations(Agent firstUnit, Agent secondUnit)
		{
			if (!firstUnit.IsDetachedFromFormation && !secondUnit.IsDetachedFromFormation && (((IFormationUnit)firstUnit).FormationFileIndex != -1 || ((IFormationUnit)secondUnit).FormationFileIndex != -1))
			{
				if (((IFormationUnit)firstUnit).FormationFileIndex == -1)
				{
					this.Arrangement.SwitchUnitLocationsWithUnpositionedUnit(secondUnit, firstUnit);
					return;
				}
				if (((IFormationUnit)secondUnit).FormationFileIndex == -1)
				{
					this.Arrangement.SwitchUnitLocationsWithUnpositionedUnit(firstUnit, secondUnit);
					return;
				}
				this.Arrangement.SwitchUnitLocations(firstUnit, secondUnit);
			}
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0006AD14 File Offset: 0x00068F14
		public void Tick(float dt)
		{
			if (this.Team.HasTeamAi && (this.IsAIControlled || this.Team.IsPlayerSergeant) && this.CountOfUnitsWithoutDetachedOnes > 0)
			{
				this.AI.Tick();
			}
			else
			{
				this.IsAITickedAfterSplit = true;
			}
			int num = 0;
			while (!this._movementOrder.IsApplicable(this) && num++ < 10)
			{
				this.SetMovementOrder(this._movementOrder.GetSubstituteOrder(this));
			}
			Formation targetFormation = this.TargetFormation;
			if (targetFormation != null && targetFormation.CountOfUnits <= 0)
			{
				this.TargetFormation = null;
			}
			if (this._arrangementOrderTickOccasionallyTimer.Check(Mission.Current.CurrentTime))
			{
				this._arrangementOrder.TickOccasionally(this);
			}
			this._movementOrder.Tick(this);
			WorldPosition value = this._movementOrder.CreateNewOrderWorldPosition(this, WorldPosition.WorldPositionEnforcedCache.None);
			Vec2 direction = this._facingOrder.GetDirection(this, this._movementOrder._targetAgent);
			if (value.IsValid || direction.IsValid)
			{
				this.SetPositioning(new WorldPosition?(value), new Vec2?(direction), null);
			}
			this.TickDetachments(dt);
			Action<Formation> onTick = this.OnTick;
			if (onTick != null)
			{
				onTick(this);
			}
			this.SmoothAverageUnitPosition(dt);
			if (this._isArrangementShapeChanged)
			{
				this._isArrangementShapeChanged = false;
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0006AE68 File Offset: 0x00069068
		public void JoinDetachment(IDetachment detachment)
		{
			if (!this.Team.DetachmentManager.ContainsDetachment(detachment))
			{
				this.Team.DetachmentManager.MakeDetachment(detachment);
			}
			this._detachments.Add(detachment);
			this.Team.DetachmentManager.OnFormationJoinDetachment(this, detachment);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0006AEB7 File Offset: 0x000690B7
		public void FormAttackEntityDetachment(GameEntity targetEntity)
		{
			this.AttackEntityOrderDetachment = new AttackEntityOrderDetachment(targetEntity);
			this.JoinDetachment(this.AttackEntityOrderDetachment);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0006AED1 File Offset: 0x000690D1
		public void LeaveDetachment(IDetachment detachment)
		{
			detachment.OnFormationLeave(this);
			this._detachments.Remove(detachment);
			this.Team.DetachmentManager.OnFormationLeaveDetachment(this, detachment);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0006AEF9 File Offset: 0x000690F9
		public void DisbandAttackEntityDetachment()
		{
			if (this.AttackEntityOrderDetachment != null)
			{
				this.Team.DetachmentManager.DestroyDetachment(this.AttackEntityOrderDetachment);
				this.AttackEntityOrderDetachment = null;
			}
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0006AF20 File Offset: 0x00069120
		public void Rearrange(IFormationArrangement arrangement)
		{
			if (this.Arrangement.GetType() == arrangement.GetType())
			{
				return;
			}
			IFormationArrangement arrangement2 = this.Arrangement;
			this.Arrangement = arrangement;
			arrangement2.RearrangeTo(arrangement);
			arrangement.RearrangeFrom(arrangement2);
			arrangement2.RearrangeTransferUnits(arrangement);
			this.ReapplyFormOrder();
			this._movementOrder.OnArrangementChanged(this);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0006AF80 File Offset: 0x00069180
		public void TickForColumnArrangementInitialPositioning(Formation formation)
		{
			if ((this.ReferencePosition.Value - this.OrderPosition).LengthSquared >= 1f && !this.IsDeployment)
			{
				this.ArrangementOrder.RearrangeAux(this, true);
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0006AFCC File Offset: 0x000691CC
		public float CalculateFormationDirectionEnforcingFactorForRank(int rankIndex)
		{
			if (rankIndex == -1)
			{
				return 0f;
			}
			return this.ArrangementOrder.CalculateFormationDirectionEnforcingFactorForRank(rankIndex, this.Arrangement.RankCount);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0006AFFD File Offset: 0x000691FD
		public void BeginSpawn(int unitCount, bool isMounted)
		{
			this.IsSpawning = true;
			this.OverridenUnitCount = new int?(unitCount);
			this._overridenHasAnyMountedUnit = new bool?(isMounted);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0006B020 File Offset: 0x00069220
		public void EndSpawn()
		{
			this.IsSpawning = false;
			this.OverridenUnitCount = null;
			this._overridenHasAnyMountedUnit = null;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0006B04F File Offset: 0x0006924F
		public override int GetHashCode()
		{
			return (int)(this.Team.TeamIndex * 10 + this.FormationIndex);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0006B066 File Offset: 0x00069266
		internal bool IsUnitDetachedForDebug(Agent unit)
		{
			return this._detachedUnits.Contains(unit);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0006B074 File Offset: 0x00069274
		internal IEnumerable<IFormationUnit> GetUnitsToPopWithPriorityFunction(int count, Func<Agent, int> priorityFunction, List<Agent> excludedHeroes, bool excludeBannerman)
		{
			Formation.<>c__DisplayClass329_0 CS$<>8__locals1 = new Formation.<>c__DisplayClass329_0();
			CS$<>8__locals1.excludedHeroes = excludedHeroes;
			CS$<>8__locals1.excludeBannerman = excludeBannerman;
			CS$<>8__locals1.priorityFunction = priorityFunction;
			List<IFormationUnit> list = new List<IFormationUnit>();
			if (count <= 0)
			{
				return list;
			}
			CS$<>8__locals1.selectCondition = ((Agent agent) => !CS$<>8__locals1.excludedHeroes.Contains(agent) && (!CS$<>8__locals1.excludeBannerman || agent.Banner == null));
			List<Agent> list2 = (from unit in this._arrangement.GetAllUnits().Concat(this._detachedUnits).Where(delegate(IFormationUnit unit)
			{
				Agent arg;
				return (arg = (unit as Agent)) != null && CS$<>8__locals1.selectCondition(arg);
			})
			select unit as Agent).ToList<Agent>();
			if (list2.IsEmpty<Agent>())
			{
				return list;
			}
			int num = count;
			CS$<>8__locals1.bestFit = int.MaxValue;
			while (num > 0 && CS$<>8__locals1.bestFit > 0 && list2.Count > 0)
			{
				Formation.<>c__DisplayClass329_1 CS$<>8__locals2 = new Formation.<>c__DisplayClass329_1();
				Formation.<>c__DisplayClass329_0 CS$<>8__locals3 = CS$<>8__locals1;
				IEnumerable<Agent> source = list2;
				Func<Agent, int> selector;
				if ((selector = CS$<>8__locals1.<>9__3) == null)
				{
					selector = (CS$<>8__locals1.<>9__3 = ((Agent unit) => CS$<>8__locals1.priorityFunction(unit)));
				}
				CS$<>8__locals3.bestFit = source.Max(selector);
				Formation.<>c__DisplayClass329_1 CS$<>8__locals4 = CS$<>8__locals2;
				Func<IFormationUnit, bool> bestFitCondition;
				if ((bestFitCondition = CS$<>8__locals1.<>9__4) == null)
				{
					bestFitCondition = (CS$<>8__locals1.<>9__4 = delegate(IFormationUnit unit)
					{
						Agent arg;
						return (arg = (unit as Agent)) != null && CS$<>8__locals1.selectCondition(arg) && CS$<>8__locals1.priorityFunction(arg) == CS$<>8__locals1.bestFit;
					});
				}
				CS$<>8__locals4.bestFitCondition = bestFitCondition;
				int num2 = Math.Min(num, this._arrangement.GetAllUnits().Count((IFormationUnit unit) => CS$<>8__locals2.bestFitCondition(unit)));
				if (num2 > 0)
				{
					IEnumerable<IFormationUnit> toPop = this._arrangement.GetUnitsToPopWithCondition(num2, CS$<>8__locals2.bestFitCondition);
					if (!toPop.IsEmpty<IFormationUnit>())
					{
						list.AddRange(toPop);
						num -= toPop.Count<IFormationUnit>();
						list2.RemoveAll((Agent unit) => toPop.Contains(unit));
					}
				}
				if (num > 0)
				{
					IEnumerable<Agent> toPop = (from agent in this._looseDetachedUnits
					where CS$<>8__locals2.bestFitCondition(agent)
					select agent).Take(num);
					if (!toPop.IsEmpty<Agent>())
					{
						list.AddRange(toPop);
						num -= toPop.Count<Agent>();
						list2.RemoveAll((Agent unit) => toPop.Contains(unit));
					}
				}
				if (num > 0)
				{
					IEnumerable<Agent> toPop = (from agent in this._detachedUnits
					where CS$<>8__locals2.bestFitCondition(agent)
					select agent).Take(num);
					if (!toPop.IsEmpty<Agent>())
					{
						list.AddRange(toPop);
						num -= toPop.Count<Agent>();
						list2.RemoveAll((Agent unit) => toPop.Contains(unit));
					}
				}
			}
			return list;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0006B304 File Offset: 0x00069504
		internal void TransferUnitsWithPriorityFunction(Formation target, int unitCount, Func<Agent, int> priorityFunction, bool excludeBannerman, List<Agent> excludedAgents)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				this.FormationIndex.GetName(),
				" has ",
				this.CountOfUnits,
				" units, ",
				target.FormationIndex.GetName(),
				" has ",
				target.CountOfUnits,
				" units"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			MBDebug.Print(string.Concat(new object[]
			{
				this.Team.Side.ToString(),
				" ",
				this.FormationIndex.GetName(),
				" transfers ",
				unitCount,
				" units to ",
				target.FormationIndex.GetName()
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			if (unitCount == 0)
			{
				return;
			}
			if (target.CountOfUnits == 0)
			{
				target.CopyOrdersFrom(this);
				target.SetPositioning(new WorldPosition?(this._orderPosition), new Vec2?(this._direction), new int?(this._unitSpacing));
			}
			foreach (IFormationUnit formationUnit in new List<IFormationUnit>(this.GetUnitsToPopWithPriorityFunction(unitCount, priorityFunction, excludedAgents, excludeBannerman)))
			{
				((Agent)formationUnit).Formation = target;
			}
			this.Team.TriggerOnFormationsChanged(this);
			this.Team.TriggerOnFormationsChanged(target);
			MBDebug.Print(string.Concat(new object[]
			{
				this.FormationIndex.GetName(),
				" has ",
				this.CountOfUnits,
				" units, ",
				target.FormationIndex.GetName(),
				" has ",
				target.CountOfUnits,
				" units"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0006B514 File Offset: 0x00069714
		private IFormationUnit GetClosestUnitToAux(Vec2 position, MBReadOnlyList<IFormationUnit> unitsWithSpaces, float? maxDistance)
		{
			if (unitsWithSpaces == null)
			{
				unitsWithSpaces = this.Arrangement.GetAllUnits();
			}
			IFormationUnit result = null;
			float num = (maxDistance != null) ? (maxDistance.Value * maxDistance.Value) : float.MaxValue;
			for (int i = 0; i < unitsWithSpaces.Count; i++)
			{
				IFormationUnit formationUnit = unitsWithSpaces[i];
				if (formationUnit != null)
				{
					float num2 = ((Agent)formationUnit).Position.AsVec2.DistanceSquared(position);
					if (num > num2)
					{
						num = num2;
						result = formationUnit;
					}
				}
			}
			return result;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0006B59C File Offset: 0x0006979C
		private void CopyOrdersFrom(Formation target)
		{
			this.SetMovementOrder(target._movementOrder);
			this.FormOrder = target.FormOrder;
			this.SetPositioning(null, null, new int?(target.UnitSpacing));
			this.RidingOrder = target.RidingOrder;
			this.FiringOrder = target.FiringOrder;
			this.SetControlledByAI(target.IsAIControlled || !target.Team.IsPlayerGeneral, false);
			if (target.AI.Side != FormationAI.BehaviorSide.BehaviorSideNotSet)
			{
				this.AI.Side = target.AI.Side;
			}
			this.SetMovementOrder(target._movementOrder);
			this.TargetFormation = target.TargetFormation;
			this.FacingOrder = target.FacingOrder;
			this.ArrangementOrder = target.ArrangementOrder;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0006B670 File Offset: 0x00069870
		private void TickDetachments(float dt)
		{
			if (!this.IsDeployment)
			{
				for (int i = this._detachments.Count - 1; i >= 0; i--)
				{
					IDetachment detachment = this._detachments[i];
					UsableMachine usableMachine = detachment as UsableMachine;
					if (((usableMachine != null) ? usableMachine.Ai : null) != null)
					{
						usableMachine.Ai.Tick(null, this, this.Team, dt);
						if (usableMachine.Ai.HasActionCompleted || (usableMachine.IsDisabledForBattleSideAI(this.Team.Side) && usableMachine.ShouldAutoLeaveDetachmentWhenDisabled(this.Team.Side)))
						{
							this.LeaveDetachment(detachment);
						}
					}
				}
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0006B710 File Offset: 0x00069910
		[Conditional("DEBUG")]
		private void TickOrderDebug()
		{
			WorldPosition medianPosition = this.QuerySystem.MedianPosition;
			WorldPosition worldPosition = this.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.GroundVec3);
			medianPosition.SetVec2(this.QuerySystem.AveragePosition);
			if (worldPosition.IsValid)
			{
				if (!this._movementOrder.GetPosition(this).IsValid)
				{
					if (this.AI != null)
					{
						BehaviorComponent activeBehavior = this.AI.ActiveBehavior;
						return;
					}
				}
				else if (this.AI != null)
				{
					BehaviorComponent activeBehavior2 = this.AI.ActiveBehavior;
					return;
				}
			}
			else if (this.AI != null)
			{
				BehaviorComponent activeBehavior3 = this.AI.ActiveBehavior;
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0006B7A0 File Offset: 0x000699A0
		[Conditional("DEBUG")]
		private void TickDebug(float dt)
		{
			if (!MBDebug.IsDisplayingHighLevelAI)
			{
				return;
			}
			if (!this.IsSimulationFormation && this._movementOrder.OrderEnum == MovementOrder.MovementOrderEnum.FollowEntity)
			{
				string name = this._movementOrder.TargetEntity.Name;
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0006B7D1 File Offset: 0x000699D1
		private void OnUnitAttachedOrDetached()
		{
			this.ReapplyFormOrder();
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0006B7D9 File Offset: 0x000699D9
		[Conditional("DEBUG")]
		private void DebugAssertDetachments()
		{
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0006B7DB File Offset: 0x000699DB
		private void SetOrderPosition(WorldPosition pos)
		{
			this._orderPosition = pos;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0006B7E4 File Offset: 0x000699E4
		private int GetHeroPointForCaptainSelection(Agent agent)
		{
			return agent.Character.Level + 100 * agent.Character.GetSkillValue(DefaultSkills.Charm);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0006B805 File Offset: 0x00069A05
		private void OnCaptainChanged()
		{
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				agent.UpdateAgentProperties();
			}, null);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0006B82D File Offset: 0x00069A2D
		private void UpdateAgentDrivenPropertiesBasedOnOrderDefensiveness()
		{
			this.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				agent.Defensiveness = (float)this._formationOrderDefensivenessFactor;
			}, null);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0006B844 File Offset: 0x00069A44
		private void ResetAux()
		{
			if (this._detachments != null)
			{
				for (int i = this._detachments.Count - 1; i >= 0; i--)
				{
					this.LeaveDetachment(this._detachments[i]);
				}
			}
			else
			{
				this._detachments = new MBList<IDetachment>();
			}
			this._detachedUnits = new MBList<Agent>();
			this._looseDetachedUnits = new MBList<Agent>();
			this.AttackEntityOrderDetachment = null;
			this.AI = new FormationAI(this);
			this.QuerySystem = new FormationQuerySystem(this);
			this.SetPositioning(null, new Vec2?(Vec2.Forward), new int?(1));
			this.SetMovementOrder(MovementOrder.MovementOrderStop);
			if (this._overridenHasAnyMountedUnit != null)
			{
				bool? overridenHasAnyMountedUnit = this._overridenHasAnyMountedUnit;
				bool flag = true;
				if (overridenHasAnyMountedUnit.GetValueOrDefault() == flag & overridenHasAnyMountedUnit != null)
				{
					this.ArrangementOrder = ArrangementOrder.ArrangementOrderSkein;
					goto IL_EB;
				}
			}
			this.FormOrder = FormOrder.FormOrderWide;
			this.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			IL_EB:
			this.RidingOrder = RidingOrder.RidingOrderFree;
			this.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			this.Width = 0f * (this.Interval + this.UnitDiameter) + this.UnitDiameter;
			this.HasBeenPositioned = false;
			this._currentSpawnIndex = 0;
			this.IsPlayerTroopInFormation = false;
			this.HasPlayerControlledTroop = false;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0006B98E File Offset: 0x00069B8E
		private void ResetForSimulation()
		{
			this.Arrangement.Reset();
			this.ResetAux();
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0006B9A4 File Offset: 0x00069BA4
		private void TryRelocatePlayerUnit()
		{
			if (this.HasPlayerControlledTroop || this.IsPlayerTroopInFormation)
			{
				IFormationUnit playerUnit = this.Arrangement.GetPlayerUnit();
				if (playerUnit != null && playerUnit.FormationFileIndex >= 0 && playerUnit.FormationRankIndex >= 0)
				{
					this.Arrangement.SwitchUnitLocationsWithBackMostUnit(playerUnit);
				}
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0006B9F0 File Offset: 0x00069BF0
		private void ReapplyFormOrder()
		{
			FormOrder formOrder = this.FormOrder;
			if (this.FormOrder.OrderEnum == FormOrder.FormOrderEnum.Custom && this.ArrangementOrder.OrderEnum != ArrangementOrder.ArrangementOrderEnum.Circle)
			{
				formOrder.CustomFlankWidth = this.Arrangement.FlankWidth;
			}
			this.FormOrder = formOrder;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0006BA38 File Offset: 0x00069C38
		private void CalculateLogicalClass()
		{
			int num = 0;
			FormationClass logicalClass = FormationClass.NumberOfAllFormations;
			for (int i = 0; i < this._logicalClassCounts.Length; i++)
			{
				FormationClass formationClass = (FormationClass)i;
				int num2 = this._logicalClassCounts[i];
				if (num2 > num)
				{
					num = num2;
					logicalClass = formationClass;
				}
			}
			this._logicalClass = logicalClass;
			if (this._logicalClass != FormationClass.NumberOfAllFormations)
			{
				this._representativeClass = this._logicalClass;
			}
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0006BA90 File Offset: 0x00069C90
		private void SmoothAverageUnitPosition(float dt)
		{
			this._smoothedAverageUnitPosition = ((!this._smoothedAverageUnitPosition.IsValid) ? this.QuerySystem.AveragePosition : Vec2.Lerp(this._smoothedAverageUnitPosition, this.QuerySystem.AveragePosition, dt * 3f));
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0006BACF File Offset: 0x00069CCF
		private void Arrangement_OnWidthChanged()
		{
			Action<Formation> onWidthChanged = this.OnWidthChanged;
			if (onWidthChanged == null)
			{
				return;
			}
			onWidthChanged(this);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0006BAE2 File Offset: 0x00069CE2
		private void Arrangement_OnShapeChanged()
		{
			this._orderLocalAveragePositionIsDirty = true;
			this._isArrangementShapeChanged = true;
			if (!GameNetwork.IsMultiplayer)
			{
				this.TryRelocatePlayerUnit();
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0006BB00 File Offset: 0x00069D00
		public static float GetLastSimulatedFormationsOccupationWidthIfLesserThanActualWidth(Formation simulationFormation)
		{
			float occupationWidth = simulationFormation.Arrangement.GetOccupationWidth(simulationFormation.OverridenUnitCount.GetValueOrDefault());
			if (simulationFormation.Width > occupationWidth)
			{
				return occupationWidth;
			}
			return -1f;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0006BB38 File Offset: 0x00069D38
		public static List<WorldFrame> GetFormationFramesForBeforeFormationCreation(float width, int manCount, bool areMounted, WorldPosition spawnOrigin, Mat3 spawnRotation)
		{
			List<Formation.AgentArrangementData> list = new List<Formation.AgentArrangementData>();
			Formation formation = new Formation(null, -1);
			formation.SetOrderPosition(spawnOrigin);
			formation._direction = spawnRotation.f.AsVec2;
			LineFormation lineFormation = new LineFormation(formation, true);
			lineFormation.Width = width;
			for (int i = 0; i < manCount; i++)
			{
				list.Add(new Formation.AgentArrangementData(i, lineFormation));
			}
			lineFormation.OnFormationFrameChanged();
			foreach (Formation.AgentArrangementData unit in list)
			{
				lineFormation.AddUnit(unit);
			}
			List<WorldFrame> list2 = new List<WorldFrame>();
			int cachedOrderedAndAvailableUnitPositionIndicesCount = lineFormation.GetCachedOrderedAndAvailableUnitPositionIndicesCount();
			for (int j = 0; j < cachedOrderedAndAvailableUnitPositionIndicesCount; j++)
			{
				Vec2i cachedOrderedAndAvailableUnitPositionIndexAt = lineFormation.GetCachedOrderedAndAvailableUnitPositionIndexAt(j);
				WorldPosition globalPositionAtIndex = lineFormation.GetGlobalPositionAtIndex(cachedOrderedAndAvailableUnitPositionIndexAt.X, cachedOrderedAndAvailableUnitPositionIndexAt.Y);
				list2.Add(new WorldFrame(spawnRotation, globalPositionAtIndex));
			}
			return list2;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0006BC30 File Offset: 0x00069E30
		public static float GetDefaultUnitDiameter(bool isMounted)
		{
			if (isMounted)
			{
				return ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.QuadrupedalRadius) * 2f;
			}
			return ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRadius) * 2f;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0006BC58 File Offset: 0x00069E58
		public static float GetDefaultMinimumInterval(bool isMounted)
		{
			if (!isMounted)
			{
				return Formation.InfantryInterval(0);
			}
			return Formation.CavalryInterval(0);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0006BC6A File Offset: 0x00069E6A
		public static float GetDefaultMaximumInterval(bool isMounted)
		{
			if (!isMounted)
			{
				return Formation.InfantryInterval(2);
			}
			return Formation.CavalryInterval(2);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0006BC7C File Offset: 0x00069E7C
		public static float GetDefaultMinimumDistance(bool isMounted)
		{
			if (!isMounted)
			{
				return Formation.InfantryDistance(0);
			}
			return Formation.CavalryDistance(0);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0006BC8E File Offset: 0x00069E8E
		public static float GetDefaultMaximumDistance(bool isMounted)
		{
			if (!isMounted)
			{
				return Formation.InfantryDistance(2);
			}
			return Formation.CavalryDistance(2);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0006BCA0 File Offset: 0x00069EA0
		public static float InfantryInterval(int unitSpacing)
		{
			return 0.38f * (float)unitSpacing;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0006BCAA File Offset: 0x00069EAA
		public static float CavalryInterval(int unitSpacing)
		{
			return 0.18f + 0.32f * (float)unitSpacing;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0006BCBA File Offset: 0x00069EBA
		public static float InfantryDistance(int unitSpacing)
		{
			return 0.4f * (float)unitSpacing;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0006BCC4 File Offset: 0x00069EC4
		public static float CavalryDistance(int unitSpacing)
		{
			return 1.7f + 0.3f * (float)unitSpacing;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0006BCD4 File Offset: 0x00069ED4
		public static bool IsDefenseRelatedAIDrivenComponent(DrivenProperty drivenProperty)
		{
			return drivenProperty == DrivenProperty.AIDecideOnAttackChance || drivenProperty == DrivenProperty.AIAttackOnDecideChance || drivenProperty == DrivenProperty.AIAttackOnParryChance || drivenProperty == DrivenProperty.AiUseShieldAgainstEnemyMissileProbability || drivenProperty == DrivenProperty.AiDefendWithShieldDecisionChanceValue;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0006BCF0 File Offset: 0x00069EF0
		private static void GetUnitPositionWithIndexAccordingToNewOrder(Formation simulationFormation, int unitIndex, in WorldPosition formationPosition, in Vec2 formationDirection, IFormationArrangement arrangement, float width, int unitSpacing, int unitCount, bool isMounted, int index, out WorldPosition? unitPosition, out Vec2? unitDirection, out float actualWidth)
		{
			unitPosition = null;
			unitDirection = null;
			if (simulationFormation == null)
			{
				if (Formation._simulationFormationTemp == null || Formation._simulationFormationUniqueIdentifier != index)
				{
					Formation._simulationFormationTemp = new Formation(null, -1);
				}
				simulationFormation = Formation._simulationFormationTemp;
			}
			if (simulationFormation.UnitSpacing == unitSpacing && MathF.Abs(simulationFormation.Width - width + 1E-05f) < simulationFormation.Interval + simulationFormation.UnitDiameter - 1E-05f && simulationFormation.OrderPositionIsValid)
			{
				Vec3 orderGroundPosition = simulationFormation.OrderGroundPosition;
				WorldPosition worldPosition = formationPosition;
				if (orderGroundPosition.NearlyEquals(worldPosition.GetGroundVec3(), 0.1f) && simulationFormation.Direction.NearlyEquals(formationDirection, 0.1f) && !(simulationFormation.Arrangement.GetType() != arrangement.GetType()))
				{
					goto IL_15E;
				}
			}
			simulationFormation._overridenHasAnyMountedUnit = new bool?(isMounted);
			simulationFormation.ResetForSimulation();
			simulationFormation.SetPositioning(null, null, new int?(unitSpacing));
			simulationFormation.OverridenUnitCount = new int?(unitCount);
			simulationFormation.SetPositioning(new WorldPosition?(formationPosition), new Vec2?(formationDirection), null);
			simulationFormation.Rearrange(arrangement.Clone(simulationFormation));
			simulationFormation.Arrangement.DeepCopyFrom(arrangement);
			simulationFormation.Width = width;
			Formation._simulationFormationUniqueIdentifier = index;
			IL_15E:
			actualWidth = simulationFormation.Width;
			if (width >= actualWidth)
			{
				Vec2? vec = simulationFormation.Arrangement.GetLocalPositionOfUnitOrDefault(unitIndex);
				if (vec == null)
				{
					vec = simulationFormation.Arrangement.CreateNewPosition(unitIndex);
				}
				if (vec != null)
				{
					Vec2 v = simulationFormation.Direction.TransformToParentUnitF(vec.Value);
					WorldPosition value = simulationFormation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
					value.SetVec2(value.AsVec2 + v);
					unitPosition = new WorldPosition?(value);
					unitDirection = new Vec2?(formationDirection);
				}
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0006BEF1 File Offset: 0x0006A0F1
		private static IEnumerable<ValueTuple<WorldPosition, Vec2>> GetUnavailableUnitPositionsAccordingToNewOrder(Formation formation, Formation simulationFormation, WorldPosition position, Vec2 direction, IFormationArrangement arrangement, float width, int unitSpacing)
		{
			if (simulationFormation == null)
			{
				if (Formation._simulationFormationTemp == null || Formation._simulationFormationUniqueIdentifier != formation.Index)
				{
					Formation._simulationFormationTemp = new Formation(null, -1);
				}
				simulationFormation = Formation._simulationFormationTemp;
			}
			if (simulationFormation.UnitSpacing != unitSpacing || MathF.Abs(simulationFormation.Width - width) >= simulationFormation.Interval + simulationFormation.UnitDiameter || !simulationFormation.OrderPositionIsValid || !simulationFormation.OrderGroundPosition.NearlyEquals(position.GetGroundVec3(), 0.1f) || !simulationFormation.Direction.NearlyEquals(direction, 0.1f) || simulationFormation.Arrangement.GetType() != arrangement.GetType())
			{
				simulationFormation._overridenHasAnyMountedUnit = new bool?(formation.HasAnyMountedUnit);
				simulationFormation.ResetForSimulation();
				simulationFormation.SetPositioning(null, null, new int?(unitSpacing));
				simulationFormation.OverridenUnitCount = new int?(formation.CountOfUnitsWithoutDetachedOnes);
				simulationFormation.SetPositioning(new WorldPosition?(position), new Vec2?(direction), null);
				simulationFormation.Rearrange(arrangement.Clone(simulationFormation));
				simulationFormation.Arrangement.DeepCopyFrom(arrangement);
				simulationFormation.Width = width;
				Formation._simulationFormationUniqueIdentifier = formation.Index;
			}
			IEnumerable<Vec2> unavailableUnitPositions = simulationFormation.Arrangement.GetUnavailableUnitPositions();
			foreach (Vec2 a in unavailableUnitPositions)
			{
				Vec2 v = simulationFormation.Direction.TransformToParentUnitF(a);
				WorldPosition item = simulationFormation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
				item.SetVec2(item.AsVec2 + v);
				yield return new ValueTuple<WorldPosition, Vec2>(item, direction);
			}
			IEnumerator<Vec2> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0006BF2E File Offset: 0x0006A12E
		private static float TransformCustomWidthBetweenArrangementOrientations(ArrangementOrder.ArrangementOrderEnum orderTypeOld, ArrangementOrder.ArrangementOrderEnum orderTypeNew, float currentCustomWidth)
		{
			if (orderTypeOld != ArrangementOrder.ArrangementOrderEnum.Column && orderTypeNew == ArrangementOrder.ArrangementOrderEnum.Column)
			{
				return currentCustomWidth * 0.1f;
			}
			if (orderTypeOld == ArrangementOrder.ArrangementOrderEnum.Column && orderTypeNew != ArrangementOrder.ArrangementOrderEnum.Column)
			{
				return currentCustomWidth / 0.1f;
			}
			return currentCustomWidth;
		}

		// Token: 0x040009AA RID: 2474
		public const float AveragePositionCalculatePeriod = 0.05f;

		// Token: 0x040009AB RID: 2475
		public const int MinimumUnitSpacing = 0;

		// Token: 0x040009AC RID: 2476
		public const int MaximumUnitSpacing = 2;

		// Token: 0x040009AD RID: 2477
		public const int RetreatPositionDistanceCacheCount = 2;

		// Token: 0x040009AE RID: 2478
		public const float RetreatPositionCacheUseDistanceSquared = 400f;

		// Token: 0x040009AF RID: 2479
		private static Formation _simulationFormationTemp;

		// Token: 0x040009B0 RID: 2480
		private static int _simulationFormationUniqueIdentifier;

		// Token: 0x040009BA RID: 2490
		public readonly Team Team;

		// Token: 0x040009BB RID: 2491
		public readonly int Index;

		// Token: 0x040009BC RID: 2492
		public readonly FormationClass FormationIndex;

		// Token: 0x040009BD RID: 2493
		public Banner Banner;

		// Token: 0x040009BE RID: 2494
		public bool HasBeenPositioned;

		// Token: 0x040009BF RID: 2495
		public Vec2? ReferencePosition;

		// Token: 0x040009C0 RID: 2496
		private FormationClass _representativeClass = FormationClass.NumberOfAllFormations;

		// Token: 0x040009C1 RID: 2497
		private bool _logicalClassNeedsUpdate;

		// Token: 0x040009C2 RID: 2498
		private FormationClass _logicalClass = FormationClass.NumberOfAllFormations;

		// Token: 0x040009C3 RID: 2499
		private int[] _logicalClassCounts = new int[4];

		// Token: 0x040009C4 RID: 2500
		private Agent _playerOwner;

		// Token: 0x040009C5 RID: 2501
		private string _bannerCode;

		// Token: 0x040009C6 RID: 2502
		private bool _isAIControlled = true;

		// Token: 0x040009C7 RID: 2503
		private bool _enforceNotSplittableByAI = true;

		// Token: 0x040009C8 RID: 2504
		private WorldPosition _orderPosition;

		// Token: 0x040009C9 RID: 2505
		private Vec2 _direction;

		// Token: 0x040009CA RID: 2506
		private int _unitSpacing;

		// Token: 0x040009CB RID: 2507
		private Vec2 _orderLocalAveragePosition;

		// Token: 0x040009CC RID: 2508
		private bool _orderLocalAveragePositionIsDirty = true;

		// Token: 0x040009CD RID: 2509
		private int _formationOrderDefensivenessFactor = 2;

		// Token: 0x040009CE RID: 2510
		private MovementOrder _movementOrder;

		// Token: 0x040009CF RID: 2511
		private FacingOrder _facingOrder;

		// Token: 0x040009D0 RID: 2512
		private ArrangementOrder _arrangementOrder;

		// Token: 0x040009D1 RID: 2513
		private Timer _arrangementOrderTickOccasionallyTimer;

		// Token: 0x040009D2 RID: 2514
		private FormOrder _formOrder;

		// Token: 0x040009D3 RID: 2515
		private RidingOrder _ridingOrder;

		// Token: 0x040009D4 RID: 2516
		private FiringOrder _firingOrder;

		// Token: 0x040009D5 RID: 2517
		private Agent _captain;

		// Token: 0x040009D6 RID: 2518
		private Vec2 _smoothedAverageUnitPosition = Vec2.Invalid;

		// Token: 0x040009D7 RID: 2519
		private MBList<IDetachment> _detachments;

		// Token: 0x040009D8 RID: 2520
		private IFormationArrangement _arrangement;

		// Token: 0x040009D9 RID: 2521
		private int[] _agentIndicesCache;

		// Token: 0x040009DA RID: 2522
		private MBList<Agent> _detachedUnits;

		// Token: 0x040009DB RID: 2523
		private int _undetachableNonPlayerUnitCount;

		// Token: 0x040009DC RID: 2524
		private MBList<Agent> _looseDetachedUnits;

		// Token: 0x040009DD RID: 2525
		private bool? _overridenHasAnyMountedUnit;

		// Token: 0x040009DE RID: 2526
		private bool _isArrangementShapeChanged;

		// Token: 0x040009DF RID: 2527
		private int _currentSpawnIndex;

		// Token: 0x040009E2 RID: 2530
		private Formation _targetFormation;

		// Token: 0x020004ED RID: 1261
		private class AgentArrangementData : IFormationUnit
		{
			// Token: 0x1700096A RID: 2410
			// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000E05EB File Offset: 0x000DE7EB
			// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000E05F3 File Offset: 0x000DE7F3
			public IFormationArrangement Formation { get; private set; }

			// Token: 0x1700096B RID: 2411
			// (get) Token: 0x060037BA RID: 14266 RVA: 0x000E05FC File Offset: 0x000DE7FC
			// (set) Token: 0x060037BB RID: 14267 RVA: 0x000E0604 File Offset: 0x000DE804
			public int FormationFileIndex { get; set; } = -1;

			// Token: 0x1700096C RID: 2412
			// (get) Token: 0x060037BC RID: 14268 RVA: 0x000E060D File Offset: 0x000DE80D
			// (set) Token: 0x060037BD RID: 14269 RVA: 0x000E0615 File Offset: 0x000DE815
			public int FormationRankIndex { get; set; } = -1;

			// Token: 0x1700096D RID: 2413
			// (get) Token: 0x060037BE RID: 14270 RVA: 0x000E061E File Offset: 0x000DE81E
			public IFormationUnit FollowedUnit { get; }

			// Token: 0x1700096E RID: 2414
			// (get) Token: 0x060037BF RID: 14271 RVA: 0x000E0626 File Offset: 0x000DE826
			public bool IsShieldUsageEncouraged
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700096F RID: 2415
			// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000E0629 File Offset: 0x000DE829
			public bool IsPlayerUnit
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060037C1 RID: 14273 RVA: 0x000E062C File Offset: 0x000DE82C
			public AgentArrangementData(int index, IFormationArrangement arrangement)
			{
				this.Formation = arrangement;
			}
		}

		// Token: 0x020004EE RID: 1262
		public class RetreatPositionCacheSystem
		{
			// Token: 0x060037C2 RID: 14274 RVA: 0x000E0649 File Offset: 0x000DE849
			public RetreatPositionCacheSystem(int cacheCount)
			{
				this._retreatPositionDistance = new List<ValueTuple<Vec2, WorldPosition>>(2);
			}

			// Token: 0x060037C3 RID: 14275 RVA: 0x000E0660 File Offset: 0x000DE860
			public WorldPosition GetRetreatPositionFromCache(Vec2 agentPosition)
			{
				for (int i = this._retreatPositionDistance.Count - 1; i >= 0; i--)
				{
					if (this._retreatPositionDistance[i].Item1.DistanceSquared(agentPosition) < 400f)
					{
						return this._retreatPositionDistance[i].Item2;
					}
				}
				return WorldPosition.Invalid;
			}

			// Token: 0x060037C4 RID: 14276 RVA: 0x000E06BD File Offset: 0x000DE8BD
			public void AddNewPositionToCache(Vec2 agentPostion, WorldPosition retreatingPosition)
			{
				if (this._retreatPositionDistance.Count >= 2)
				{
					this._retreatPositionDistance.RemoveAt(0);
				}
				this._retreatPositionDistance.Add(new ValueTuple<Vec2, WorldPosition>(agentPostion, retreatingPosition));
			}

			// Token: 0x04001B7C RID: 7036
			private List<ValueTuple<Vec2, WorldPosition>> _retreatPositionDistance;
		}
	}
}
