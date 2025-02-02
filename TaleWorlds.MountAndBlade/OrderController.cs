using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200014D RID: 333
	public class OrderController
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00030934 File Offset: 0x0002EB34
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x0003093C File Offset: 0x0002EB3C
		public SiegeWeaponController SiegeWeaponController { get; private set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00030945 File Offset: 0x0002EB45
		public MBReadOnlyList<Formation> SelectedFormations
		{
			get
			{
				return this._selectedFormations;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0003094D File Offset: 0x0002EB4D
		public bool FormationUpdateEnabledAfterSetOrder
		{
			get
			{
				return this._formationUpdateEnabledAfterSetOrder;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001090 RID: 4240 RVA: 0x00030958 File Offset: 0x0002EB58
		// (remove) Token: 0x06001091 RID: 4241 RVA: 0x00030990 File Offset: 0x0002EB90
		public event OnOrderIssuedDelegate OnOrderIssued;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001092 RID: 4242 RVA: 0x000309C8 File Offset: 0x0002EBC8
		// (remove) Token: 0x06001093 RID: 4243 RVA: 0x00030A00 File Offset: 0x0002EC00
		public event Action OnSelectedFormationsChanged;

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00030A35 File Offset: 0x0002EC35
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x00030A3D File Offset: 0x0002EC3D
		public Dictionary<Formation, Formation> simulationFormations { get; private set; }

		// Token: 0x06001096 RID: 4246 RVA: 0x00030A48 File Offset: 0x0002EC48
		public OrderController(Mission mission, Team team, Agent owner)
		{
			this._mission = mission;
			this._team = team;
			this.Owner = owner;
			this._gesturesEnabled = true;
			this._selectedFormations = new MBList<Formation>();
			this.SiegeWeaponController = new SiegeWeaponController(mission, this._team);
			this.simulationFormations = new Dictionary<Formation, Formation>();
			this.actualWidths = new Dictionary<Formation, float>();
			this.actualUnitSpacings = new Dictionary<Formation, int>();
			foreach (Formation formation in this._team.FormationsIncludingEmpty)
			{
				formation.OnWidthChanged += this.Formation_OnWidthChanged;
				formation.OnUnitSpacingChanged += this.Formation_OnUnitSpacingChanged;
			}
			if (this._team.IsPlayerGeneral)
			{
				foreach (Formation formation2 in this._team.FormationsIncludingEmpty)
				{
					formation2.PlayerOwner = owner;
				}
			}
			this.CreateDefaultOrderOverrides();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00030B7C File Offset: 0x0002ED7C
		private void Formation_OnUnitSpacingChanged(Formation formation)
		{
			this.actualUnitSpacings.Remove(formation);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00030B8B File Offset: 0x0002ED8B
		private void Formation_OnWidthChanged(Formation formation)
		{
			this.actualWidths.Remove(formation);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00030B9C File Offset: 0x0002ED9C
		private void OnSelectedFormationsCollectionChanged()
		{
			Action onSelectedFormationsChanged = this.OnSelectedFormationsChanged;
			if (onSelectedFormationsChanged != null)
			{
				onSelectedFormationsChanged();
			}
			foreach (Formation key in this.SelectedFormations.Except(this.simulationFormations.Keys))
			{
				this.simulationFormations[key] = new Formation(null, -1);
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00030C18 File Offset: 0x0002EE18
		private void SelectFormation(Formation formation, Agent selectorAgent)
		{
			if (!this._selectedFormations.Contains(formation) && this.IsFormationSelectable(formation, selectorAgent))
			{
				if (GameNetwork.IsClient)
				{
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new SelectFormation(formation.Index));
					GameNetwork.EndModuleEventAsClient();
				}
				if (selectorAgent != null && this.AreGesturesEnabled())
				{
					OrderController.PlayFormationSelectedGesture(formation, selectorAgent);
				}
				MBDebug.Print(((formation != null) ? new FormationClass?(formation.RepresentativeClass) : null) + " added to selected formations.", 0, Debug.DebugColor.White, 17592186044416UL);
				this._selectedFormations.Add(formation);
				this.OnSelectedFormationsCollectionChanged();
				return;
			}
			Debug.FailedAssert("Formation already selected or is not selectable", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SelectFormation", 208);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00030CD8 File Offset: 0x0002EED8
		public void SelectFormation(Formation formation)
		{
			this.SelectFormation(formation, this.Owner);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00030CE8 File Offset: 0x0002EEE8
		public void DeselectFormation(Formation formation)
		{
			if (this._selectedFormations.Contains(formation))
			{
				if (GameNetwork.IsClient)
				{
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new UnselectFormation(formation.Index));
					GameNetwork.EndModuleEventAsClient();
				}
				MBDebug.Print(((formation != null) ? new FormationClass?(formation.RepresentativeClass) : null) + " is removed from selected formations.", 0, Debug.DebugColor.White, 17592186044416UL);
				this._selectedFormations.Remove(formation);
				this.OnSelectedFormationsCollectionChanged();
				return;
			}
			Debug.FailedAssert("Trying to deselect an unselected formation", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "DeselectFormation", 234);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00030D8A File Offset: 0x0002EF8A
		public bool IsFormationListening(Formation formation)
		{
			return this.SelectedFormations.Contains(formation);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00030D98 File Offset: 0x0002EF98
		public bool IsFormationSelectable(Formation formation)
		{
			return this.IsFormationSelectable(formation, this.Owner);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00030DA7 File Offset: 0x0002EFA7
		public bool BackupAndDisableGesturesEnabled()
		{
			bool gesturesEnabled = this._gesturesEnabled;
			this._gesturesEnabled = false;
			return gesturesEnabled;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00030DB6 File Offset: 0x0002EFB6
		public void RestoreGesturesEnabled(bool oldValue)
		{
			this._gesturesEnabled = oldValue;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00030DBF File Offset: 0x0002EFBF
		private bool IsFormationSelectable(Formation formation, Agent selectorAgent)
		{
			return (selectorAgent == null || formation.PlayerOwner == selectorAgent) && formation.CountOfUnits > 0;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00030DD8 File Offset: 0x0002EFD8
		private bool AreGesturesEnabled()
		{
			return this._gesturesEnabled && this._mission.IsOrderGesturesEnabled() && !GameNetwork.IsClientOrReplay;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00030DFC File Offset: 0x0002EFFC
		private void SelectAllFormations(Agent selectorAgent, bool uiFeedback)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new SelectAllFormations());
				GameNetwork.EndModuleEventAsClient();
			}
			if (uiFeedback && selectorAgent != null && this.AreGesturesEnabled())
			{
				selectorAgent.MakeVoice(SkinVoiceManager.VoiceType.Everyone, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
			}
			MBDebug.Print("Selected formations being cleared. Select all formations:", 0, Debug.DebugColor.White, 17592186044416UL);
			this._selectedFormations.Clear();
			IEnumerable<Formation> formationsIncludingEmpty = this._team.FormationsIncludingEmpty;
			Func<Formation, bool> <>9__0;
			Func<Formation, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((Formation f) => f.CountOfUnits > 0 && this.IsFormationSelectable(f, selectorAgent)));
			}
			foreach (Formation formation in formationsIncludingEmpty.Where(predicate))
			{
				MBDebug.Print(formation.RepresentativeClass + " added to selected formations.", 0, Debug.DebugColor.White, 17592186044416UL);
				this._selectedFormations.Add(formation);
			}
			this.OnSelectedFormationsCollectionChanged();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00030F18 File Offset: 0x0002F118
		public void SelectAllFormations(bool uiFeedback = false)
		{
			this.SelectAllFormations(this.Owner, uiFeedback);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00030F28 File Offset: 0x0002F128
		public void ClearSelectedFormations()
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ClearSelectedFormations());
				GameNetwork.EndModuleEventAsClient();
			}
			MBDebug.Print("Selected formations being cleared.", 0, Debug.DebugColor.White, 17592186044416UL);
			this._selectedFormations.Clear();
			this.OnSelectedFormationsCollectionChanged();
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00030F78 File Offset: 0x0002F178
		public unsafe void SetOrder(OrderType orderType)
		{
			MBDebug.Print("SetOrder " + orderType + "on team", 0, Debug.DebugColor.White, 17592186044416UL);
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrder(orderType));
				GameNetwork.EndModuleEventAsClient();
			}
			switch (orderType)
			{
			case OrderType.Charge:
				using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation = enumerator.Current;
						formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
					}
					goto IL_79A;
				}
				break;
			case OrderType.ChargeWithTarget:
			case OrderType.FollowMe:
			case OrderType.FollowEntity:
			case OrderType.GuardMe:
			case OrderType.LookAtDirection:
			case OrderType.FormCustom:
			case OrderType.CohesionHigh:
			case OrderType.CohesionMedium:
			case OrderType.CohesionLow:
			case OrderType.RideFree:
				goto IL_781;
			case OrderType.StandYourGround:
				break;
			case OrderType.Retreat:
				goto IL_29C;
			case OrderType.AdvanceTenPaces:
				goto IL_158;
			case OrderType.FallBackTenPaces:
				goto IL_1BE;
			case OrderType.Advance:
				goto IL_226;
			case OrderType.FallBack:
				goto IL_261;
			case OrderType.LookAtEnemy:
				goto IL_2D7;
			case OrderType.ArrangementLine:
				goto IL_4BC;
			case OrderType.ArrangementCloseOrder:
				goto IL_4FD;
			case OrderType.ArrangementLoose:
				goto IL_53E;
			case OrderType.ArrangementCircular:
				goto IL_57F;
			case OrderType.ArrangementSchiltron:
				goto IL_5C0;
			case OrderType.ArrangementVee:
				goto IL_601;
			case OrderType.ArrangementColumn:
				goto IL_642;
			case OrderType.ArrangementScatter:
				goto IL_683;
			case OrderType.FormDeep:
				goto IL_6C4;
			case OrderType.FormWide:
				goto IL_705;
			case OrderType.FormWider:
				goto IL_743;
			case OrderType.HoldFire:
				goto IL_31C;
			case OrderType.FireAtWill:
				goto IL_357;
			case OrderType.Mount:
				goto IL_3EF;
			case OrderType.Dismount:
				goto IL_392;
			case OrderType.AIControlOn:
				goto IL_44C;
			case OrderType.AIControlOff:
				goto IL_484;
			default:
				goto IL_781;
			}
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation2 = enumerator.Current;
					formation2.SetMovementOrder(MovementOrder.MovementOrderStop);
				}
				goto IL_79A;
			}
			IL_158:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation3 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation3);
					if (formation3.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.Move)
					{
						MovementOrder movementOrder = *formation3.GetReadonlyMovementOrderReference();
						movementOrder.Advance(formation3, 7f);
						formation3.SetMovementOrder(movementOrder);
					}
				}
				goto IL_79A;
			}
			IL_1BE:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation4 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation4);
					if (formation4.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.Move)
					{
						MovementOrder movementOrder2 = *formation4.GetReadonlyMovementOrderReference();
						movementOrder2.FallBack(formation4, 7f);
						formation4.SetMovementOrder(movementOrder2);
					}
				}
				goto IL_79A;
			}
			IL_226:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation5 = enumerator.Current;
					formation5.SetMovementOrder(MovementOrder.MovementOrderAdvance);
				}
				goto IL_79A;
			}
			IL_261:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation6 = enumerator.Current;
					formation6.SetMovementOrder(MovementOrder.MovementOrderFallBack);
				}
				goto IL_79A;
			}
			IL_29C:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation7 = enumerator.Current;
					formation7.SetMovementOrder(MovementOrder.MovementOrderRetreat);
				}
				goto IL_79A;
			}
			IL_2D7:
			FacingOrder facingOrderLookAtEnemy = FacingOrder.FacingOrderLookAtEnemy;
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation8 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation8);
					formation8.FacingOrder = facingOrderLookAtEnemy;
				}
				goto IL_79A;
			}
			IL_31C:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation9 = enumerator.Current;
					formation9.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
				}
				goto IL_79A;
			}
			IL_357:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation10 = enumerator.Current;
					formation10.FiringOrder = FiringOrder.FiringOrderFireAtWill;
				}
				goto IL_79A;
			}
			IL_392:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation11 = enumerator.Current;
					if (formation11.PhysicalClass.IsMounted() || formation11.HasAnyMountedUnit)
					{
						OrderController.TryCancelStopOrder(formation11);
					}
					formation11.RidingOrder = RidingOrder.RidingOrderDismount;
				}
				goto IL_79A;
			}
			IL_3EF:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation12 = enumerator.Current;
					if (formation12.PhysicalClass.IsMounted() || formation12.HasAnyMountedUnit)
					{
						OrderController.TryCancelStopOrder(formation12);
					}
					formation12.RidingOrder = RidingOrder.RidingOrderMount;
				}
				goto IL_79A;
			}
			IL_44C:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation13 = enumerator.Current;
					formation13.SetControlledByAI(true, false);
				}
				goto IL_79A;
			}
			IL_484:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation14 = enumerator.Current;
					formation14.SetControlledByAI(false, false);
				}
				goto IL_79A;
			}
			IL_4BC:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation15 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation15);
					formation15.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				}
				goto IL_79A;
			}
			IL_4FD:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation16 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation16);
					formation16.ArrangementOrder = ArrangementOrder.ArrangementOrderShieldWall;
				}
				goto IL_79A;
			}
			IL_53E:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation17 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation17);
					formation17.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
				}
				goto IL_79A;
			}
			IL_57F:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation18 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation18);
					formation18.ArrangementOrder = ArrangementOrder.ArrangementOrderCircle;
				}
				goto IL_79A;
			}
			IL_5C0:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation19 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation19);
					formation19.ArrangementOrder = ArrangementOrder.ArrangementOrderSquare;
				}
				goto IL_79A;
			}
			IL_601:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation20 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation20);
					formation20.ArrangementOrder = ArrangementOrder.ArrangementOrderSkein;
				}
				goto IL_79A;
			}
			IL_642:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation21 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation21);
					formation21.ArrangementOrder = ArrangementOrder.ArrangementOrderColumn;
				}
				goto IL_79A;
			}
			IL_683:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation22 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation22);
					formation22.ArrangementOrder = ArrangementOrder.ArrangementOrderScatter;
				}
				goto IL_79A;
			}
			IL_6C4:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation23 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation23);
					formation23.FormOrder = FormOrder.FormOrderDeep;
				}
				goto IL_79A;
			}
			IL_705:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation24 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation24);
					formation24.FormOrder = FormOrder.FormOrderWide;
				}
				goto IL_79A;
			}
			IL_743:
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation25 = enumerator.Current;
					OrderController.TryCancelStopOrder(formation25);
					formation25.FormOrder = FormOrder.FormOrderWider;
				}
				goto IL_79A;
			}
			IL_781:
			Debug.FailedAssert("[DEBUG]Invalid order type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrder", 634);
			IL_79A:
			this.AfterSetOrder(orderType);
			if (this.OnOrderIssued != null)
			{
				this.OnOrderIssued(orderType, this.SelectedFormations, this, Array.Empty<object>());
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000319A4 File Offset: 0x0002FBA4
		private static void PlayOrderGestures(OrderType orderType, Agent agent, MBList<Formation> selectedFormations)
		{
			switch (orderType)
			{
			case OrderType.Move:
			case OrderType.MoveToLineSegment:
			case OrderType.MoveToLineSegmentWithHorizontalLayout:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Move, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.Charge:
			case OrderType.ChargeWithTarget:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Charge, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.StandYourGround:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Stop, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.FollowMe:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Follow, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.Retreat:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Retreat, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.AdvanceTenPaces:
			case OrderType.Advance:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Advance, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.FallBackTenPaces:
			case OrderType.FallBack:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FallBack, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.LookAtEnemy:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FaceEnemy, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.LookAtDirection:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FaceDirection, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementLine:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormLine, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementCloseOrder:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormShieldWall, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementLoose:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormLoose, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementCircular:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormCircle, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementSchiltron:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormSquare, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementVee:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormSkein, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementColumn:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormColumn, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.ArrangementScatter:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FormScatter, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.HoldFire:
				agent.MakeVoice(SkinVoiceManager.VoiceType.HoldFire, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.FireAtWill:
				agent.MakeVoice(SkinVoiceManager.VoiceType.FireAtWill, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.Mount:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Mount, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.Dismount:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Dismount, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.AIControlOn:
				agent.MakeVoice(SkinVoiceManager.VoiceType.CommandDelegate, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			case OrderType.AIControlOff:
				agent.MakeVoice(SkinVoiceManager.VoiceType.CommandUndelegate, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				break;
			}
			if (selectedFormations.Count > 0 && agent != null && agent.Controller != Agent.ControllerType.AI)
			{
				MissionWeapon wieldedWeapon = agent.WieldedWeapon;
				switch (wieldedWeapon.IsEmpty ? WeaponClass.Undefined : wieldedWeapon.Item.PrimaryWeapon.WeaponClass)
				{
				case WeaponClass.Undefined:
				case WeaponClass.Stone:
					if (agent.MountAgent == null)
					{
						agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? (agent.GetIsLeftStance() ? OrderController.act_command_follow_unarmed_leftstance : OrderController.act_command_follow_unarmed) : (agent.GetIsLeftStance() ? OrderController.act_command_unarmed_leftstance : OrderController.act_command_unarmed), false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
						return;
					}
					agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? OrderController.act_horse_command_follow_unarmed : OrderController.act_horse_command_unarmed, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					return;
				case WeaponClass.Dagger:
				case WeaponClass.OneHandedSword:
				case WeaponClass.OneHandedAxe:
				case WeaponClass.Mace:
				case WeaponClass.Pick:
				case WeaponClass.OneHandedPolearm:
				case WeaponClass.ThrowingAxe:
				case WeaponClass.ThrowingKnife:
					if (agent.MountAgent == null)
					{
						agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? (agent.GetIsLeftStance() ? OrderController.act_command_follow_leftstance : OrderController.act_command_follow) : (agent.GetIsLeftStance() ? OrderController.act_command_leftstance : OrderController.act_command), false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
						return;
					}
					agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? OrderController.act_horse_command_follow : OrderController.act_horse_command, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					return;
				case WeaponClass.TwoHandedSword:
				case WeaponClass.TwoHandedAxe:
				case WeaponClass.TwoHandedMace:
				case WeaponClass.TwoHandedPolearm:
				case WeaponClass.LowGripPolearm:
				case WeaponClass.Crossbow:
				case WeaponClass.Javelin:
				case WeaponClass.Pistol:
				case WeaponClass.Musket:
					if (agent.MountAgent == null)
					{
						agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? (agent.GetIsLeftStance() ? OrderController.act_command_follow_2h_leftstance : OrderController.act_command_follow_2h) : (agent.GetIsLeftStance() ? OrderController.act_command_2h_leftstance : OrderController.act_command_2h), false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
						return;
					}
					agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? OrderController.act_horse_command_follow_2h : OrderController.act_horse_command_2h, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					return;
				case WeaponClass.Bow:
					if (agent.MountAgent == null)
					{
						agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? OrderController.act_command_follow_bow : OrderController.act_command_bow, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
						return;
					}
					agent.SetActionChannel(1, (orderType == OrderType.FollowMe) ? OrderController.act_horse_command_follow_bow : OrderController.act_horse_command_bow, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					return;
				case WeaponClass.Boulder:
					return;
				}
				Debug.FailedAssert("Unexpected weapon class.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "PlayOrderGestures", 811);
			}
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00031EE8 File Offset: 0x000300E8
		private static void PlayFormationSelectedGesture(Formation formation, Agent agent)
		{
			if (formation.SecondaryLogicalClasses.Any<FormationClass>())
			{
				agent.MakeVoice(SkinVoiceManager.VoiceType.MixedFormation, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				return;
			}
			switch (formation.LogicalClass)
			{
			case FormationClass.Infantry:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Infantry, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				return;
			case FormationClass.Ranged:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Archers, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				return;
			case FormationClass.Cavalry:
				agent.MakeVoice(SkinVoiceManager.VoiceType.Cavalry, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				return;
			case FormationClass.HorseArcher:
				agent.MakeVoice(SkinVoiceManager.VoiceType.HorseArchers, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
				return;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "PlayFormationSelectedGesture", 847);
				return;
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00031F7C File Offset: 0x0003017C
		private unsafe void AfterSetOrder(OrderType orderType)
		{
			MBDebug.Print("After set order called, number of selected formations: " + this.SelectedFormations.Count, 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (Formation formation in this.SelectedFormations)
			{
				MBDebug.Print(((formation != null) ? new FormationClass?(formation.FormationIndex) : null) + " formation being processed.", 0, Debug.DebugColor.White, 17592186044416UL);
				if (this._formationUpdateEnabledAfterSetOrder)
				{
					bool flag = false;
					if (formation.IsPlayerTroopInFormation)
					{
						flag = (formation.GetReadonlyMovementOrderReference()->OrderEnum == MovementOrder.MovementOrderEnum.Follow);
					}
					formation.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.UpdateCachedAndFormationValues(false, false);
					}, flag ? Mission.Current.MainAgent : null);
				}
				MBDebug.Print("Update cached and formation values on each agent complete, number of selected formations: " + this.SelectedFormations.Count, 0, Debug.DebugColor.White, 17592186044416UL);
				this._mission.SetRandomDecideTimeOfAgentsWithIndices(formation.CollectUnitIndices(), null, null);
				MBDebug.Print("Set random decide time of agents with indices complete, number of selected formations: " + this.SelectedFormations.Count, 0, Debug.DebugColor.White, 17592186044416UL);
			}
			MBDebug.Print("After set order loop complete, number of selected formations: " + this.SelectedFormations.Count, 0, Debug.DebugColor.White, 17592186044416UL);
			if (this.Owner != null && this.AreGesturesEnabled())
			{
				OrderController.PlayOrderGestures(orderType, this.Owner, this._selectedFormations);
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00032164 File Offset: 0x00030364
		private void BeforeSetOrder(OrderType orderType)
		{
			foreach (Formation formation in (from f in this.SelectedFormations
			where !this.IsFormationSelectable(f, this.Owner)
			select f).ToList<Formation>())
			{
				this.DeselectFormation(formation);
			}
			if (!GameNetwork.IsClientOrReplay && orderType != OrderType.AIControlOff && orderType != OrderType.AIControlOn)
			{
				foreach (Formation formation2 in this.SelectedFormations)
				{
					if (formation2.IsAIControlled && formation2.PlayerOwner != null)
					{
						formation2.SetControlledByAI(false, false);
					}
				}
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00032234 File Offset: 0x00030434
		public void SetOrderWithAgent(OrderType orderType, Agent agent)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				"SetOrderWithAgent ",
				orderType,
				" ",
				agent.Name,
				"on team"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithAgent(orderType, agent.Index));
				GameNetwork.EndModuleEventAsClient();
			}
			if (orderType != OrderType.FollowMe)
			{
				if (orderType != OrderType.GuardMe)
				{
					goto IL_EC;
				}
			}
			else
			{
				using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation = enumerator.Current;
						formation.SetMovementOrder(MovementOrder.MovementOrderFollow(agent));
					}
					goto IL_105;
				}
			}
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation2 = enumerator.Current;
					formation2.SetMovementOrder(MovementOrder.MovementOrderGuard(agent));
				}
				goto IL_105;
			}
			IL_EC:
			Debug.FailedAssert("[DEBUG]Invalid order type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrderWithAgent", 947);
			IL_105:
			this.AfterSetOrder(orderType);
			OnOrderIssuedDelegate onOrderIssued = this.OnOrderIssued;
			if (onOrderIssued == null)
			{
				return;
			}
			onOrderIssued(orderType, this.SelectedFormations, this, new object[]
			{
				agent
			});
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0003238C File Offset: 0x0003058C
		public void SetOrderWithPosition(OrderType orderType, WorldPosition orderPosition)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				"SetOrderWithPosition ",
				orderType,
				" ",
				orderPosition,
				"on team"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithPosition(orderType, orderPosition.GetGroundVec3()));
				GameNetwork.EndModuleEventAsClient();
			}
			if (orderType != OrderType.Move)
			{
				if (orderType != OrderType.LookAtDirection)
				{
					if (orderType != OrderType.FormCustom)
					{
						goto IL_15A;
					}
					goto IL_10E;
				}
			}
			else
			{
				using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation = enumerator.Current;
						formation.SetMovementOrder(MovementOrder.MovementOrderMove(orderPosition));
					}
					goto IL_173;
				}
			}
			FacingOrder facingOrder = FacingOrder.FacingOrderLookAtDirection(OrderController.GetOrderLookAtDirection(this.SelectedFormations, orderPosition.AsVec2));
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation2 = enumerator.Current;
					formation2.FacingOrder = facingOrder;
				}
				goto IL_173;
			}
			IL_10E:
			float orderFormCustomWidth = OrderController.GetOrderFormCustomWidth(this.SelectedFormations, orderPosition.GetGroundVec3());
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation3 = enumerator.Current;
					formation3.FormOrder = FormOrder.FormOrderCustom(orderFormCustomWidth);
				}
				goto IL_173;
			}
			IL_15A:
			Debug.FailedAssert("[DEBUG]Invalid order type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrderWithPosition", 997);
			IL_173:
			this.AfterSetOrder(orderType);
			if (this.OnOrderIssued != null)
			{
				this.OnOrderIssued(orderType, this.SelectedFormations, this, new object[]
				{
					orderPosition
				});
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00032568 File Offset: 0x00030768
		public void SetOrderWithFormation(OrderType orderType, Formation orderFormation)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				"SetOrderWithFormation ",
				orderType,
				" ",
				orderFormation,
				"on team"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithFormation(orderType, orderFormation.Index));
				GameNetwork.EndModuleEventAsClient();
			}
			if (orderType != OrderType.Charge)
			{
				if (orderType != OrderType.Advance)
				{
					goto IL_F3;
				}
			}
			else
			{
				using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation = enumerator.Current;
						formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
						formation.SetTargetFormation(orderFormation);
					}
					goto IL_10C;
				}
			}
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation2 = enumerator.Current;
					formation2.SetMovementOrder(MovementOrder.MovementOrderAdvance);
					formation2.SetTargetFormation(orderFormation);
				}
				goto IL_10C;
			}
			IL_F3:
			Debug.FailedAssert("Invalid order type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrderWithFormation", 1040);
			IL_10C:
			this.AfterSetOrder(orderType);
			if (this.OnOrderIssued != null)
			{
				this.OnOrderIssued(orderType, this.SelectedFormations, this, new object[]
				{
					orderFormation
				});
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000326CC File Offset: 0x000308CC
		public void SetOrderWithFormationAndPercentage(OrderType orderType, Formation orderFormation, float percentage)
		{
			int num = (int)(percentage * 100f);
			num = MBMath.ClampInt(num, 0, 100);
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithFormationAndPercentage(orderType, orderFormation.Index, num));
				GameNetwork.EndModuleEventAsClient();
			}
			Debug.FailedAssert("Invalid order type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrderWithFormationAndPercentage", 1081);
			this.AfterSetOrder(orderType);
			if (this.OnOrderIssued != null)
			{
				this.OnOrderIssued(orderType, this.SelectedFormations, this, new object[]
				{
					orderFormation,
					percentage
				});
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00032764 File Offset: 0x00030964
		public void TransferUnitWithPriorityFunction(Formation orderFormation, int number, bool hasShield, bool hasSpear, bool hasThrown, bool isHeavy, bool isRanged, bool isMounted, bool excludeBannerman, List<Agent> excludedAgents)
		{
			OrderController.<>c__DisplayClass76_0 CS$<>8__locals1 = new OrderController.<>c__DisplayClass76_0();
			CS$<>8__locals1.hasShield = hasShield;
			CS$<>8__locals1.hasSpear = hasSpear;
			CS$<>8__locals1.hasThrown = hasThrown;
			CS$<>8__locals1.isHeavy = isHeavy;
			CS$<>8__locals1.isRanged = isRanged;
			CS$<>8__locals1.isMounted = isMounted;
			this.BeforeSetOrder(OrderType.Transfer);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithFormationAndNumber(OrderType.Transfer, orderFormation.Index, number));
				GameNetwork.EndModuleEventAsClient();
			}
			List<int> list = null;
			int num = this.SelectedFormations.Sum((Formation f) => f.CountOfUnits);
			int num2 = number;
			int num3 = 0;
			if (this.SelectedFormations.Count > 1)
			{
				list = new List<int>();
			}
			foreach (Formation formation in this.SelectedFormations)
			{
				int countOfUnits = formation.CountOfUnits;
				int num4 = num2 * countOfUnits / num;
				if (!GameNetwork.IsClientOrReplay)
				{
					formation.OnMassUnitTransferStart();
					orderFormation.OnMassUnitTransferStart();
					formation.TransferUnitsWithPriorityFunction(orderFormation, num4, new Func<Agent, int>(CS$<>8__locals1.<TransferUnitWithPriorityFunction>g__priorityFunction|0), excludeBannerman, excludedAgents);
					formation.OnMassUnitTransferEnd();
					orderFormation.OnMassUnitTransferEnd();
				}
				if (list != null)
				{
					list.Add(num4);
				}
				num2 -= num4;
				num -= countOfUnits;
				num3 += num4;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				orderFormation.QuerySystem.Expire();
			}
			this.AfterSetOrder(OrderType.Transfer);
			if (this.OnOrderIssued != null)
			{
				if (list != null)
				{
					object[] array = new object[list.Count + 1];
					array[0] = number;
					for (int i = 0; i < list.Count; i++)
					{
						array[i + 1] = list[i];
					}
					this.OnOrderIssued(OrderType.Transfer, this.SelectedFormations, this, new object[]
					{
						orderFormation,
						array
					});
					return;
				}
				this.OnOrderIssued(OrderType.Transfer, this.SelectedFormations, this, new object[]
				{
					orderFormation,
					number
				});
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00032978 File Offset: 0x00030B78
		public void RearrangeFormationsAccordingToFilters(Team team, List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> MassTransferData)
		{
			team.RearrangeFormationsAccordingToFilters(MassTransferData);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00032984 File Offset: 0x00030B84
		public void SetOrderWithFormationAndNumber(OrderType orderType, Formation orderFormation, int number)
		{
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithFormationAndNumber(orderType, orderFormation.Index, number));
				GameNetwork.EndModuleEventAsClient();
			}
			List<int> list = null;
			if (orderType == OrderType.Transfer)
			{
				int num = this.SelectedFormations.Sum((Formation f) => f.CountOfUnits);
				int num2 = number;
				int num3 = 0;
				if (this.SelectedFormations.Count > 1)
				{
					list = new List<int>();
				}
				foreach (Formation formation in this.SelectedFormations)
				{
					int countOfUnits = formation.CountOfUnits;
					int num4 = num2 * countOfUnits / num;
					if (!GameNetwork.IsClientOrReplay)
					{
						formation.OnMassUnitTransferStart();
						orderFormation.OnMassUnitTransferStart();
						formation.TransferUnitsAux(orderFormation, num4, true, num4 < countOfUnits && orderFormation.CountOfUnits > 0 && orderFormation.OrderPositionIsValid);
						formation.OnMassUnitTransferEnd();
						orderFormation.OnMassUnitTransferEnd();
					}
					if (list != null)
					{
						list.Add(num4);
					}
					num2 -= num4;
					num -= countOfUnits;
					num3 += num4;
				}
				if (!GameNetwork.IsClientOrReplay)
				{
					orderFormation.QuerySystem.Expire();
				}
			}
			else
			{
				Debug.FailedAssert("[DEBUG]Invalid order type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrderWithFormationAndNumber", 1330);
			}
			this.AfterSetOrder(orderType);
			if (this.OnOrderIssued != null)
			{
				if (list != null)
				{
					object[] array = new object[list.Count + 1];
					array[0] = number;
					for (int i = 0; i < list.Count; i++)
					{
						array[i + 1] = list[i];
					}
					this.OnOrderIssued(orderType, this.SelectedFormations, this, new object[]
					{
						orderFormation,
						array
					});
					return;
				}
				this.OnOrderIssued(orderType, this.SelectedFormations, this, new object[]
				{
					orderFormation,
					number
				});
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00032B88 File Offset: 0x00030D88
		public void SetOrderWithTwoPositions(OrderType orderType, WorldPosition position1, WorldPosition position2)
		{
			MBDebug.Print(string.Concat(new object[]
			{
				"SetOrderWithTwoPositions ",
				orderType,
				" ",
				position1,
				" ",
				position2,
				"on team"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			this.BeforeSetOrder(orderType);
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithTwoPositions(orderType, position1.GetGroundVec3(), position2.GetGroundVec3()));
				GameNetwork.EndModuleEventAsClient();
			}
			if (orderType - OrderType.MoveToLineSegment <= 1)
			{
				bool isFormationLayoutVertical = orderType == OrderType.MoveToLineSegment;
				IEnumerable<Formation> enumerable = from f in this.SelectedFormations
				where f.CountOfUnitsWithoutDetachedOnes > 0
				select f;
				if (enumerable.Any<Formation>())
				{
					this.MoveToLineSegment(enumerable, position1, position2, isFormationLayoutVertical);
				}
			}
			else
			{
				Debug.FailedAssert("Invalid order type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "SetOrderWithTwoPositions", 1384);
			}
			this.AfterSetOrder(orderType);
			if (this.OnOrderIssued != null)
			{
				this.OnOrderIssued(orderType, this.SelectedFormations, this, new object[]
				{
					position1,
					position2
				});
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00032CBC File Offset: 0x00030EBC
		public void SetOrderWithOrderableObject(IOrderable target)
		{
			BattleSideEnum side = this.SelectedFormations[0].Team.Side;
			OrderType order = target.GetOrder(side);
			this.BeforeSetOrder(order);
			MissionObject missionObject = target as MissionObject;
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplyOrderWithMissionObject(missionObject.Id));
				GameNetwork.EndModuleEventAsClient();
			}
			switch (order)
			{
			case OrderType.Move:
				break;
			case OrderType.MoveToLineSegment:
				goto IL_19F;
			case OrderType.MoveToLineSegmentWithHorizontalLayout:
			{
				IPointDefendable pointDefendable = target as IPointDefendable;
				Vec3 globalPosition = pointDefendable.DefencePoints.Last<DefencePoint>().GameEntity.GlobalPosition;
				Vec3 globalPosition2 = pointDefendable.DefencePoints.First<DefencePoint>().GameEntity.GlobalPosition;
				IEnumerable<Formation> enumerable = from f in this.SelectedFormations
				where f.CountOfUnitsWithoutDetachedOnes > 0
				select f;
				if (enumerable.Any<Formation>())
				{
					WorldPosition targetLineSegmentBegin = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalPosition, false);
					WorldPosition targetLineSegmentEnd = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalPosition2, false);
					this.MoveToLineSegment(enumerable, targetLineSegmentBegin, targetLineSegmentEnd, false);
					goto IL_378;
				}
				goto IL_378;
			}
			default:
			{
				if (order == OrderType.FollowEntity)
				{
					GameEntity waitEntity = (target as UsableMachine).WaitEntity;
					Vec2 direction = waitEntity.GetGlobalFrame().rotation.f.AsVec2.Normalized();
					foreach (Formation formation in this.SelectedFormations)
					{
						formation.FacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
						formation.SetMovementOrder(MovementOrder.MovementOrderFollowEntity(waitEntity));
					}
					goto IL_378;
				}
				switch (order)
				{
				case OrderType.Use:
				{
					UsableMachine usable = target as UsableMachine;
					this.ToggleSideOrderUse(this.SelectedFormations, usable);
					goto IL_378;
				}
				case OrderType.AttackEntity:
				{
					GameEntity gameEntity = missionObject.GameEntity;
					using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Formation formation2 = enumerator.Current;
							formation2.SetMovementOrder(MovementOrder.MovementOrderAttackEntity(gameEntity, !(missionObject is CastleGate)));
						}
						goto IL_378;
					}
					break;
				}
				case OrderType.PointDefence:
					break;
				default:
					goto IL_378;
				}
				IPointDefendable pointDefendable2 = target as IPointDefendable;
				using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation3 = enumerator.Current;
						formation3.SetMovementOrder(MovementOrder.MovementOrderMove(pointDefendable2.MiddleFrame.Origin));
					}
					goto IL_378;
				}
				break;
			}
			}
			WorldPosition position = new WorldPosition(this._mission.Scene, UIntPtr.Zero, missionObject.GameEntity.GlobalPosition, false);
			using (List<Formation>.Enumerator enumerator = this.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation4 = enumerator.Current;
					formation4.SetMovementOrder(MovementOrder.MovementOrderMove(position));
				}
				goto IL_378;
			}
			IL_19F:
			IPointDefendable pointDefendable3 = target as IPointDefendable;
			Vec3 globalPosition3 = pointDefendable3.DefencePoints.Last<DefencePoint>().GameEntity.GlobalPosition;
			Vec3 globalPosition4 = pointDefendable3.DefencePoints.First<DefencePoint>().GameEntity.GlobalPosition;
			IEnumerable<Formation> enumerable2 = from f in this.SelectedFormations
			where f.CountOfUnitsWithoutDetachedOnes > 0
			select f;
			if (enumerable2.Any<Formation>())
			{
				WorldPosition targetLineSegmentBegin2 = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalPosition3, false);
				WorldPosition targetLineSegmentEnd2 = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalPosition4, false);
				this.MoveToLineSegment(enumerable2, targetLineSegmentBegin2, targetLineSegmentEnd2, true);
			}
			IL_378:
			this.AfterSetOrder(order);
			OnOrderIssuedDelegate onOrderIssued = this.OnOrderIssued;
			if (onOrderIssued == null)
			{
				return;
			}
			onOrderIssued(order, this.SelectedFormations, this, new object[]
			{
				target
			});
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000330A0 File Offset: 0x000312A0
		public unsafe static OrderType GetActiveMovementOrderOf(Formation formation)
		{
			MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
			switch (movementOrder.MovementState)
			{
			case MovementOrder.MovementStateEnum.Charge:
				movementOrder = *formation.GetReadonlyMovementOrderReference();
				if (movementOrder.OrderType == OrderType.GuardMe)
				{
					return OrderType.GuardMe;
				}
				return OrderType.Charge;
			case MovementOrder.MovementStateEnum.Hold:
			{
				movementOrder = *formation.GetReadonlyMovementOrderReference();
				OrderType orderType = movementOrder.OrderType;
				if (orderType <= OrderType.FollowMe)
				{
					if (orderType == OrderType.ChargeWithTarget)
					{
						return OrderType.Charge;
					}
					if (orderType == OrderType.FollowMe)
					{
						return OrderType.FollowMe;
					}
				}
				else
				{
					if (orderType == OrderType.Advance)
					{
						return OrderType.Advance;
					}
					if (orderType == OrderType.FallBack)
					{
						return OrderType.FallBack;
					}
				}
				return OrderType.Move;
			}
			case MovementOrder.MovementStateEnum.Retreat:
				return OrderType.Retreat;
			case MovementOrder.MovementStateEnum.StandGround:
				return OrderType.StandYourGround;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "GetActiveMovementOrderOf", 1543);
				return OrderType.Move;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00033150 File Offset: 0x00031350
		public static OrderType GetActiveFacingOrderOf(Formation formation)
		{
			if (formation.FacingOrder.OrderType == OrderType.LookAtDirection)
			{
				return OrderType.LookAtDirection;
			}
			return OrderType.LookAtEnemy;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00033174 File Offset: 0x00031374
		public static OrderType GetActiveRidingOrderOf(Formation formation)
		{
			OrderType orderType = formation.RidingOrder.OrderType;
			if (orderType == OrderType.RideFree)
			{
				return OrderType.Mount;
			}
			return orderType;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003319C File Offset: 0x0003139C
		public static OrderType GetActiveArrangementOrderOf(Formation formation)
		{
			return formation.ArrangementOrder.OrderType;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000331B8 File Offset: 0x000313B8
		public static OrderType GetActiveFormOrderOf(Formation formation)
		{
			return formation.FormOrder.OrderType;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000331D4 File Offset: 0x000313D4
		public static OrderType GetActiveFiringOrderOf(Formation formation)
		{
			return formation.FiringOrder.OrderType;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000331EF File Offset: 0x000313EF
		public static OrderType GetActiveAIControlOrderOf(Formation formation)
		{
			if (formation.IsAIControlled)
			{
				return OrderType.AIControlOn;
			}
			return OrderType.AIControlOff;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00033200 File Offset: 0x00031400
		public void SimulateNewOrderWithPositionAndDirection(WorldPosition formationLineBegin, WorldPosition formationLineEnd, out List<WorldPosition> simulationAgentFrames, bool isFormationLayoutVertical)
		{
			IEnumerable<Formation> enumerable = from f in this.SelectedFormations
			where f.CountOfUnitsWithoutDetachedOnes > 0
			select f;
			if (enumerable.Any<Formation>())
			{
				OrderController.SimulateNewOrderWithPositionAndDirection(enumerable, this.simulationFormations, formationLineBegin, formationLineEnd, out simulationAgentFrames, isFormationLayoutVertical);
				return;
			}
			simulationAgentFrames = new List<WorldPosition>();
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003325C File Offset: 0x0003145C
		public void SimulateNewFacingOrder(Vec2 direction, out List<WorldPosition> simulationAgentFrames)
		{
			IEnumerable<Formation> enumerable = from f in this.SelectedFormations
			where f.CountOfUnitsWithoutDetachedOnes > 0
			select f;
			if (enumerable.Any<Formation>())
			{
				OrderController.SimulateNewFacingOrder(enumerable, this.simulationFormations, direction, out simulationAgentFrames);
				return;
			}
			simulationAgentFrames = new List<WorldPosition>();
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000332B4 File Offset: 0x000314B4
		public void SimulateNewCustomWidthOrder(float width, out List<WorldPosition> simulationAgentFrames)
		{
			IEnumerable<Formation> enumerable = from f in this.SelectedFormations
			where f.CountOfUnitsWithoutDetachedOnes > 0
			select f;
			if (enumerable.Any<Formation>())
			{
				OrderController.SimulateNewCustomWidthOrder(enumerable, this.simulationFormations, width, out simulationAgentFrames);
				return;
			}
			simulationAgentFrames = new List<WorldPosition>();
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003330C File Offset: 0x0003150C
		private static void SimulateNewOrderWithPositionAndDirectionAux(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, WorldPosition formationLineBegin, WorldPosition formationLineEnd, bool isSimulatingAgentFrames, out List<WorldPosition> simulationAgentFrames, bool isSimulatingFormationChanges, out List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> simulationFormationChanges, out bool isLineShort, bool isFormationLayoutVertical = true)
		{
			float length = (formationLineEnd.AsVec2 - formationLineBegin.AsVec2).Length;
			isLineShort = false;
			if (length < ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRadius))
			{
				isLineShort = true;
			}
			else
			{
				float num;
				if (isFormationLayoutVertical)
				{
					num = formations.Sum((Formation f) => f.MinimumWidth) + (float)(formations.Count<Formation>() - 1) * 1.5f;
				}
				else
				{
					num = formations.Max((Formation f) => f.Width);
				}
				if (length < num)
				{
					isLineShort = true;
				}
			}
			if (isLineShort)
			{
				float num2;
				if (isFormationLayoutVertical)
				{
					num2 = formations.Sum((Formation f) => f.Width);
					num2 += (float)(formations.Count<Formation>() - 1) * 1.5f;
				}
				else
				{
					num2 = formations.Max((Formation f) => f.Width);
				}
				Vec2 direction = formations.MaxBy((Formation f) => f.CountOfUnitsWithoutDetachedOnes).Direction;
				direction.RotateCCW(-1.5707964f);
				direction.Normalize();
				formationLineEnd = Mission.Current.GetStraightPathToTarget(formationLineBegin.AsVec2 + num2 / 2f * direction, formationLineBegin, 1f, true);
				formationLineBegin = Mission.Current.GetStraightPathToTarget(formationLineBegin.AsVec2 - num2 / 2f * direction, formationLineBegin, 1f, true);
			}
			else
			{
				formationLineEnd = Mission.Current.GetStraightPathToTarget(formationLineEnd.AsVec2, formationLineBegin, 1f, true);
			}
			if (isFormationLayoutVertical)
			{
				OrderController.SimulateNewOrderWithVerticalLayout(formations, simulationFormations, formationLineBegin, formationLineEnd, isSimulatingAgentFrames, out simulationAgentFrames, isSimulatingFormationChanges, out simulationFormationChanges);
				return;
			}
			OrderController.SimulateNewOrderWithHorizontalLayout(formations, simulationFormations, formationLineBegin, formationLineEnd, isSimulatingAgentFrames, out simulationAgentFrames, isSimulatingFormationChanges, out simulationFormationChanges);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00033500 File Offset: 0x00031700
		private static Formation GetSimulationFormation(Formation formation, Dictionary<Formation, Formation> simulationFormations)
		{
			if (simulationFormations == null)
			{
				return null;
			}
			return simulationFormations[formation];
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00033510 File Offset: 0x00031710
		private static void SimulateNewFacingOrder(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, Vec2 direction, out List<WorldPosition> simulationAgentFrames)
		{
			simulationAgentFrames = new List<WorldPosition>();
			foreach (Formation formation in formations)
			{
				float width = formation.Width;
				WorldPosition worldPosition = formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
				int unitSpacingReduction = 0;
				OrderController.DecreaseUnitSpacingAndWidthIfNotAllUnitsFit(formation, OrderController.GetSimulationFormation(formation, simulationFormations), worldPosition, direction, ref width, ref unitSpacingReduction);
				float num;
				OrderController.SimulateNewOrderWithFrameAndWidth(formation, OrderController.GetSimulationFormation(formation, simulationFormations), simulationAgentFrames, null, worldPosition, direction, width, unitSpacingReduction, false, out num);
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00033598 File Offset: 0x00031798
		private static void SimulateNewCustomWidthOrder(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, float width, out List<WorldPosition> simulationAgentFrames)
		{
			simulationAgentFrames = new List<WorldPosition>();
			foreach (Formation formation in formations)
			{
				float num = width;
				num = MathF.Min(num, formation.MaximumWidth);
				Mat3 identity = Mat3.Identity;
				Vec2 direction = formation.Direction;
				identity.f = direction.ToVec3(0f);
				identity.Orthonormalize();
				WorldPosition worldPosition = formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
				int unitSpacingReduction = 0;
				Formation formation2 = formation;
				Formation simulationFormation = OrderController.GetSimulationFormation(formation, simulationFormations);
				direction = formation.Direction;
				OrderController.DecreaseUnitSpacingAndWidthIfNotAllUnitsFit(formation2, simulationFormation, worldPosition, direction, ref num, ref unitSpacingReduction);
				int count = simulationAgentFrames.Count;
				Formation formation3 = formation;
				Formation simulationFormation2 = OrderController.GetSimulationFormation(formation, simulationFormations);
				List<WorldPosition> simulationAgentFrames2 = simulationAgentFrames;
				List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> simulationFormationChanges = null;
				direction = formation.Direction;
				float num2;
				OrderController.SimulateNewOrderWithFrameAndWidth(formation3, simulationFormation2, simulationAgentFrames2, simulationFormationChanges, worldPosition, direction, num, unitSpacingReduction, false, out num2);
				float lastSimulatedFormationsOccupationWidthIfLesserThanActualWidth = Formation.GetLastSimulatedFormationsOccupationWidthIfLesserThanActualWidth(OrderController.GetSimulationFormation(formation, simulationFormations));
				if (lastSimulatedFormationsOccupationWidthIfLesserThanActualWidth > 0f)
				{
					simulationAgentFrames.RemoveRange(count, simulationAgentFrames.Count - count);
					Formation formation4 = formation;
					Formation simulationFormation3 = OrderController.GetSimulationFormation(formation, simulationFormations);
					List<WorldPosition> simulationAgentFrames3 = simulationAgentFrames;
					List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> simulationFormationChanges2 = null;
					direction = formation.Direction;
					OrderController.SimulateNewOrderWithFrameAndWidth(formation4, simulationFormation3, simulationAgentFrames3, simulationFormationChanges2, worldPosition, direction, lastSimulatedFormationsOccupationWidthIfLesserThanActualWidth, unitSpacingReduction, false, out num2);
				}
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000336C0 File Offset: 0x000318C0
		public static void SimulateNewOrderWithPositionAndDirection(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, WorldPosition formationLineBegin, WorldPosition formationLineEnd, out List<WorldPosition> simulationAgentFrames, bool isFormationLayoutVertical = true)
		{
			List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> list;
			bool flag;
			OrderController.SimulateNewOrderWithPositionAndDirectionAux(formations, simulationFormations, formationLineBegin, formationLineEnd, true, out simulationAgentFrames, false, out list, out flag, isFormationLayoutVertical);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000336E0 File Offset: 0x000318E0
		public static void SimulateNewOrderWithPositionAndDirection(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, WorldPosition formationLineBegin, WorldPosition formationLineEnd, out List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> formationChanges, out bool isLineShort, bool isFormationLayoutVertical = true)
		{
			List<WorldPosition> list;
			OrderController.SimulateNewOrderWithPositionAndDirectionAux(formations, simulationFormations, formationLineBegin, formationLineEnd, false, out list, true, out formationChanges, out isLineShort, isFormationLayoutVertical);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00033700 File Offset: 0x00031900
		private static void SimulateNewOrderWithVerticalLayout(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, WorldPosition formationLineBegin, WorldPosition formationLineEnd, bool isSimulatingAgentFrames, out List<WorldPosition> simulationAgentFrames, bool isSimulatingFormationChanges, out List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> simulationFormationChanges)
		{
			simulationAgentFrames = ((!isSimulatingAgentFrames) ? null : new List<WorldPosition>());
			simulationFormationChanges = ((!isSimulatingFormationChanges) ? null : new List<ValueTuple<Formation, int, float, WorldPosition, Vec2>>());
			Vec2 vec = formationLineEnd.AsVec2 - formationLineBegin.AsVec2;
			float length = vec.Length;
			vec.Normalize();
			float num = MathF.Max(0f, length - (float)(formations.Count<Formation>() - 1) * 1.5f);
			float num2 = formations.Sum((Formation f) => f.Width);
			bool flag = num.ApproximatelyEqualsTo(num2, 0.1f);
			float num3 = formations.Sum((Formation f) => f.MinimumWidth);
			formations.Count<Formation>();
			Vec2 vec2 = new Vec2(-vec.y, vec.x).Normalized();
			float num4 = 0f;
			foreach (Formation formation in formations)
			{
				float minimumWidth = formation.MinimumWidth;
				float num5 = flag ? formation.Width : MathF.Min((num < num2) ? formation.Width : float.MaxValue, num * (minimumWidth / num3));
				num5 = MathF.Min(num5, formation.MaximumWidth);
				WorldPosition worldPosition = formationLineBegin;
				worldPosition.SetVec2(worldPosition.AsVec2 + vec * (num5 * 0.5f + num4));
				int unitSpacingReduction = 0;
				OrderController.DecreaseUnitSpacingAndWidthIfNotAllUnitsFit(formation, OrderController.GetSimulationFormation(formation, simulationFormations), worldPosition, vec2, ref num5, ref unitSpacingReduction);
				float num6;
				OrderController.SimulateNewOrderWithFrameAndWidth(formation, OrderController.GetSimulationFormation(formation, simulationFormations), simulationAgentFrames, simulationFormationChanges, worldPosition, vec2, num5, unitSpacingReduction, false, out num6);
				num4 += num5 + 1.5f;
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000338E8 File Offset: 0x00031AE8
		private static void DecreaseUnitSpacingAndWidthIfNotAllUnitsFit(Formation formation, Formation simulationFormation, in WorldPosition formationPosition, in Vec2 formationDirection, ref float formationWidth, ref int unitSpacingReduction)
		{
			if (simulationFormation.UnitSpacing != formation.UnitSpacing)
			{
				simulationFormation = new Formation(null, -1);
			}
			int unitIndex = formation.CountOfUnitsWithoutDetachedOnes - 1;
			float num = formationWidth;
			do
			{
				WorldPosition? worldPosition;
				Vec2? vec;
				formation.GetUnitPositionWithIndexAccordingToNewOrder(simulationFormation, unitIndex, formationPosition, formationDirection, formationWidth, formation.UnitSpacing - unitSpacingReduction, out worldPosition, out vec, out num);
				if (worldPosition != null)
				{
					break;
				}
				unitSpacingReduction++;
			}
			while (formation.UnitSpacing - unitSpacingReduction >= 0);
			unitSpacingReduction = MathF.Min(unitSpacingReduction, formation.UnitSpacing);
			if (unitSpacingReduction > 0)
			{
				formationWidth = num;
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00033970 File Offset: 0x00031B70
		private static float GetGapBetweenLinesOfFormation(Formation f, float unitSpacing)
		{
			float num = 0f;
			float num2 = 0.2f;
			if (f.HasAnyMountedUnit && !(f.RidingOrder == RidingOrder.RidingOrderDismount))
			{
				num = 2f;
				num2 = 0.6f;
			}
			return num + unitSpacing * num2;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000339B4 File Offset: 0x00031BB4
		private static void SimulateNewOrderWithHorizontalLayout(IEnumerable<Formation> formations, Dictionary<Formation, Formation> simulationFormations, WorldPosition formationLineBegin, WorldPosition formationLineEnd, bool isSimulatingAgentFrames, out List<WorldPosition> simulationAgentFrames, bool isSimulatingFormationChanges, out List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> simulationFormationChanges)
		{
			simulationAgentFrames = ((!isSimulatingAgentFrames) ? null : new List<WorldPosition>());
			simulationFormationChanges = ((!isSimulatingFormationChanges) ? null : new List<ValueTuple<Formation, int, float, WorldPosition, Vec2>>());
			Vec2 vec = formationLineEnd.AsVec2 - formationLineBegin.AsVec2;
			float num = vec.Normalize();
			float num2 = formations.Max((Formation f) => f.MinimumWidth);
			if (num < num2)
			{
				num = num2;
			}
			Vec2 v = new Vec2(-vec.y, vec.x).Normalized();
			float num3 = 0f;
			foreach (Formation formation in formations)
			{
				float num4 = num;
				num4 = MathF.Min(num4, formation.MaximumWidth);
				WorldPosition worldPosition = formationLineBegin;
				worldPosition.SetVec2((formationLineEnd.AsVec2 + formationLineBegin.AsVec2) * 0.5f - v * num3);
				int num5 = 0;
				OrderController.DecreaseUnitSpacingAndWidthIfNotAllUnitsFit(formation, OrderController.GetSimulationFormation(formation, simulationFormations), worldPosition, v, ref num4, ref num5);
				float num6;
				OrderController.SimulateNewOrderWithFrameAndWidth(formation, OrderController.GetSimulationFormation(formation, simulationFormations), simulationAgentFrames, simulationFormationChanges, worldPosition, v, num4, num5, true, out num6);
				num3 += num6 + OrderController.GetGapBetweenLinesOfFormation(formation, (float)(formation.UnitSpacing - num5));
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00033B28 File Offset: 0x00031D28
		private static void SimulateNewOrderWithFrameAndWidth(Formation formation, Formation simulationFormation, List<WorldPosition> simulationAgentFrames, List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> simulationFormationChanges, in WorldPosition formationPosition, in Vec2 formationDirection, float formationWidth, int unitSpacingReduction, bool simulateFormationDepth, out float simulatedFormationDepth)
		{
			int num = 0;
			float num2 = simulateFormationDepth ? 0f : float.NaN;
			bool flag = Mission.Current.Mode != MissionMode.Deployment || Mission.Current.IsOrderPositionAvailable(formationPosition, formation.Team);
			foreach (Agent agent in from u in formation.GetUnitsWithoutDetachedOnes()
			orderby MBCommon.Hash(u.Index, u)
			select u)
			{
				WorldPosition? worldPosition = null;
				Vec2? vec = null;
				if (flag)
				{
					formation.GetUnitPositionWithIndexAccordingToNewOrder(simulationFormation, num, formationPosition, formationDirection, formationWidth, formation.UnitSpacing - unitSpacingReduction, out worldPosition, out vec);
				}
				else
				{
					worldPosition = new WorldPosition?(agent.GetWorldPosition());
					vec = new Vec2?(agent.GetMovementDirection());
				}
				if (worldPosition != null)
				{
					if (simulationAgentFrames != null)
					{
						simulationAgentFrames.Add(worldPosition.Value);
					}
					if (simulateFormationDepth)
					{
						WorldPosition worldPosition2 = formationPosition;
						Vec2 asVec = worldPosition2.AsVec2;
						worldPosition2 = formationPosition;
						Vec2 asVec2 = worldPosition2.AsVec2;
						Vec2 vec2 = formationDirection;
						float num3 = Vec2.DistanceToLine(asVec, asVec2 + vec2.RightVec(), worldPosition.Value.AsVec2);
						if (num3 > num2)
						{
							num2 = num3;
						}
					}
				}
				num++;
			}
			if (flag)
			{
				if (simulationFormationChanges != null)
				{
					simulationFormationChanges.Add(ValueTuple.Create<Formation, int, float, WorldPosition, Vec2>(formation, unitSpacingReduction, formationWidth, formationPosition, formationDirection));
				}
			}
			else
			{
				WorldPosition item = formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
				if (simulationFormationChanges != null)
				{
					simulationFormationChanges.Add(ValueTuple.Create<Formation, int, float, WorldPosition, Vec2>(formation, unitSpacingReduction, formationWidth, item, formation.Direction));
				}
			}
			simulatedFormationDepth = num2 + formation.UnitDiameter;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00033CE8 File Offset: 0x00031EE8
		public void SimulateDestinationFrames(out List<WorldPosition> simulationAgentFrames, float minDistance = 3f)
		{
			List<Formation> selectedFormations = this.SelectedFormations;
			simulationAgentFrames = new List<WorldPosition>(100);
			float minDistanceSq = minDistance * minDistance;
			Action<Agent, List<WorldPosition>> <>9__0;
			foreach (Formation formation in selectedFormations)
			{
				Action<Agent, List<WorldPosition>> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(Agent agent, List<WorldPosition> localSimulationAgentFrames)
					{
						WorldPosition item;
						if (this._mission.IsTeleportingAgents && !agent.CanTeleport())
						{
							item = agent.GetWorldPosition();
						}
						else
						{
							item = agent.Formation.GetOrderPositionOfUnit(agent);
						}
						bool flag = item.IsValid;
						if (!GameNetwork.IsMultiplayer && this._mission.Mode == MissionMode.Deployment)
						{
							MBSceneUtilities.ProjectPositionToDeploymentBoundaries(agent.Formation.Team.Side, ref item);
							flag = this._mission.IsFormationUnitPositionAvailable(ref item, agent.Formation.Team);
						}
						if (flag && agent.Position.AsVec2.DistanceSquared(item.AsVec2) >= minDistanceSq)
						{
							localSimulationAgentFrames.Add(item);
						}
					});
				}
				formation.ApplyActionOnEachUnit(action, simulationAgentFrames);
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00033D78 File Offset: 0x00031F78
		private void ToggleSideOrderUse(IEnumerable<Formation> formations, UsableMachine usable)
		{
			IEnumerable<Formation> enumerable = formations.Where(new Func<Formation, bool>(usable.IsUsedByFormation));
			if (enumerable.IsEmpty<Formation>())
			{
				foreach (Formation formation in formations)
				{
					formation.StartUsingMachine(usable, true);
				}
				if (!usable.HasWaitFrame)
				{
					return;
				}
				using (IEnumerator<Formation> enumerator = formations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Formation formation2 = enumerator.Current;
						formation2.SetMovementOrder(MovementOrder.MovementOrderFollowEntity(usable.WaitEntity));
					}
					return;
				}
			}
			foreach (Formation formation3 in enumerable)
			{
				formation3.StopUsingMachine(usable, true);
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00033E58 File Offset: 0x00032058
		private static int GetLineOrderByClass(FormationClass formationClass)
		{
			FormationClass[] array = new FormationClass[8];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.6DE253DA153DA622C93844B719DADEA29978BADD).FieldHandle);
			return Array.IndexOf<FormationClass>(array, formationClass);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00033E71 File Offset: 0x00032071
		public static IEnumerable<Formation> SortFormationsForHorizontalLayout(IEnumerable<Formation> formations)
		{
			return from f in formations
			orderby OrderController.GetLineOrderByClass(f.FormationIndex)
			select f;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00033E98 File Offset: 0x00032098
		private static IEnumerable<Formation> GetSortedFormations(IEnumerable<Formation> formations, bool isFormationLayoutVertical)
		{
			if (isFormationLayoutVertical)
			{
				return formations;
			}
			return OrderController.SortFormationsForHorizontalLayout(formations);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00033EA8 File Offset: 0x000320A8
		private void MoveToLineSegment(IEnumerable<Formation> formations, WorldPosition TargetLineSegmentBegin, WorldPosition TargetLineSegmentEnd, bool isFormationLayoutVertical = true)
		{
			foreach (Formation formation in formations)
			{
				int value;
				if (this.actualUnitSpacings.TryGetValue(formation, out value))
				{
					formation.SetPositioning(null, null, new int?(value));
				}
				float customWidth;
				if (this.actualWidths.TryGetValue(formation, out customWidth))
				{
					formation.FormOrder = FormOrder.FormOrderCustom(customWidth);
				}
			}
			formations = OrderController.GetSortedFormations(formations, isFormationLayoutVertical);
			List<ValueTuple<Formation, int, float, WorldPosition, Vec2>> list;
			bool flag;
			OrderController.SimulateNewOrderWithPositionAndDirection(formations, this.simulationFormations, TargetLineSegmentBegin, TargetLineSegmentEnd, out list, out flag, isFormationLayoutVertical);
			if (!formations.Any<Formation>())
			{
				return;
			}
			foreach (ValueTuple<Formation, int, float, WorldPosition, Vec2> valueTuple in list)
			{
				Formation item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				float item3 = valueTuple.Item3;
				WorldPosition item4 = valueTuple.Item4;
				Vec2 item5 = valueTuple.Item5;
				int unitSpacing = item.UnitSpacing;
				float width = item.Width;
				if (item2 > 0)
				{
					int value2 = MathF.Max(item.UnitSpacing - item2, 0);
					item.SetPositioning(null, null, new int?(value2));
					if (item.UnitSpacing != unitSpacing)
					{
						this.actualUnitSpacings[item] = unitSpacing;
					}
				}
				if (item.Width != item3 && item.ArrangementOrder.OrderEnum != ArrangementOrder.ArrangementOrderEnum.Column)
				{
					item.FormOrder = FormOrder.FormOrderCustom(item3);
					if (flag)
					{
						this.actualWidths[item] = width;
					}
				}
				if (!flag)
				{
					item.SetMovementOrder(MovementOrder.MovementOrderMove(item4));
					item.FacingOrder = FacingOrder.FacingOrderLookAtDirection(item5);
					item.FormOrder = FormOrder.FormOrderCustom(item3);
					if (this.OnOrderIssued != null)
					{
						MBList<Formation> appliedFormations = new MBList<Formation>
						{
							item
						};
						this.OnOrderIssued(OrderType.Move, appliedFormations, this, new object[]
						{
							item4
						});
						this.OnOrderIssued(OrderType.LookAtDirection, appliedFormations, this, new object[]
						{
							item5
						});
						this.OnOrderIssued(OrderType.FormCustom, appliedFormations, this, new object[]
						{
							item3
						});
					}
				}
				else
				{
					Formation formation2 = formations.MaxBy((Formation f) => f.CountOfUnitsWithoutDetachedOnes);
					OrderType activeFacingOrderOf = OrderController.GetActiveFacingOrderOf(formation2);
					if (activeFacingOrderOf == OrderType.LookAtEnemy)
					{
						item.SetMovementOrder(MovementOrder.MovementOrderMove(item4));
						if (this.OnOrderIssued != null)
						{
							MBList<Formation> appliedFormations2 = new MBList<Formation>
							{
								item
							};
							this.OnOrderIssued(OrderType.Move, appliedFormations2, this, new object[]
							{
								item4
							});
							this.OnOrderIssued(OrderType.LookAtEnemy, appliedFormations2, this, Array.Empty<object>());
						}
					}
					else if (activeFacingOrderOf == OrderType.LookAtDirection)
					{
						item.SetMovementOrder(MovementOrder.MovementOrderMove(item4));
						item.FacingOrder = FacingOrder.FacingOrderLookAtDirection(formation2.Direction);
						if (this.OnOrderIssued != null)
						{
							MBList<Formation> appliedFormations3 = new MBList<Formation>
							{
								item
							};
							this.OnOrderIssued(OrderType.Move, appliedFormations3, this, new object[]
							{
								item4
							});
							this.OnOrderIssued(OrderType.LookAtDirection, appliedFormations3, this, new object[]
							{
								formation2.Direction
							});
						}
					}
					else
					{
						Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "MoveToLineSegment", 2361);
					}
				}
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003425C File Offset: 0x0003245C
		public static Vec2 GetOrderLookAtDirection(IEnumerable<Formation> formations, Vec2 target)
		{
			if (!formations.Any<Formation>())
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\OrderController.cs", "GetOrderLookAtDirection", 2381);
				return Vec2.One;
			}
			Formation formation = formations.MaxBy((Formation f) => f.CountOfUnitsWithoutDetachedOnes);
			return (target - formation.OrderPosition).Normalized();
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000342CC File Offset: 0x000324CC
		public static float GetOrderFormCustomWidth(IEnumerable<Formation> formations, Vec3 orderPosition)
		{
			return (Agent.Main.Position - orderPosition).Length;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000342F4 File Offset: 0x000324F4
		public void TransferUnits(Formation source, Formation target, int count)
		{
			source.TransferUnitsAux(target, count, false, count < source.CountOfUnits && target.CountOfUnits > 0);
			OnOrderIssuedDelegate onOrderIssued = this.OnOrderIssued;
			if (onOrderIssued == null)
			{
				return;
			}
			onOrderIssued(OrderType.Transfer, new MBList<Formation>
			{
				source
			}, this, new object[]
			{
				target,
				count
			});
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00034354 File Offset: 0x00032554
		public IEnumerable<Formation> SplitFormation(Formation formation, int count = 2)
		{
			if (!formation.IsSplittableByAI || formation.CountOfUnitsWithoutDetachedOnes < count)
			{
				return new List<Formation>
				{
					formation
				};
			}
			MBDebug.Print(string.Concat(new object[]
			{
				(formation.Team.Side == BattleSideEnum.Attacker) ? "Attacker team" : "Defender team",
				" formation ",
				(int)formation.FormationIndex,
				" split"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			List<Formation> list = new List<Formation>
			{
				formation
			};
			while (count > 1)
			{
				int num = formation.CountOfUnits / count;
				int i = 0;
				while (i < 8)
				{
					Formation formation2 = formation.Team.GetFormation((FormationClass)i);
					if (formation2.CountOfUnits == 0)
					{
						formation.TransferUnitsAux(formation2, num, false, false);
						list.Add(formation2);
						OnOrderIssuedDelegate onOrderIssued = this.OnOrderIssued;
						if (onOrderIssued == null)
						{
							break;
						}
						onOrderIssued(OrderType.Transfer, new MBList<Formation>
						{
							formation
						}, this, new object[]
						{
							formation2,
							num
						});
						break;
					}
					else
					{
						i++;
					}
				}
				count--;
			}
			return list;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003445F File Offset: 0x0003265F
		[Conditional("DEBUG")]
		public void TickDebug()
		{
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00034461 File Offset: 0x00032661
		public void AddOrderOverride(Func<Formation, MovementOrder, MovementOrder> orderOverride)
		{
			if (this.orderOverrides == null)
			{
				this.orderOverrides = new List<Func<Formation, MovementOrder, MovementOrder>>();
				this.overridenOrders = new List<ValueTuple<Formation, OrderType>>();
			}
			this.orderOverrides.Add(orderOverride);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00034490 File Offset: 0x00032690
		public OrderType GetOverridenOrderType(Formation formation)
		{
			if (this.overridenOrders == null)
			{
				return OrderType.None;
			}
			ValueTuple<Formation, OrderType> valueTuple = this.overridenOrders.FirstOrDefault((ValueTuple<Formation, OrderType> oo) => oo.Item1 == formation);
			if (valueTuple.Item1 != null)
			{
				return valueTuple.Item2;
			}
			return OrderType.None;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000344DC File Offset: 0x000326DC
		private void CreateDefaultOrderOverrides()
		{
			this.AddOrderOverride(delegate(Formation formation, MovementOrder order)
			{
				if (formation.ArrangementOrder.OrderType == OrderType.ArrangementCloseOrder && order.OrderType == OrderType.StandYourGround)
				{
					Vec2 averagePosition = formation.QuerySystem.AveragePosition;
					float movementSpeed = formation.QuerySystem.MovementSpeed;
					WorldPosition medianPosition = formation.QuerySystem.MedianPosition;
					medianPosition.SetVec2(averagePosition + formation.Direction * formation.Depth * (0.5f + movementSpeed));
					return MovementOrder.MovementOrderMove(medianPosition);
				}
				return MovementOrder.MovementOrderStop;
			});
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00034503 File Offset: 0x00032703
		internal void SetFormationUpdateEnabledAfterSetOrder(bool value)
		{
			this._formationUpdateEnabledAfterSetOrder = value;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003450C File Offset: 0x0003270C
		private static void TryCancelStopOrder(Formation formation)
		{
			if (!GameNetwork.IsClientOrReplay && formation.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.Stop)
			{
				WorldPosition position = formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
				if (position.IsValid)
				{
					formation.SetMovementOrder(MovementOrder.MovementOrderMove(position));
				}
			}
		}

		// Token: 0x04000409 RID: 1033
		private static readonly ActionIndexCache act_command = ActionIndexCache.Create("act_command");

		// Token: 0x0400040A RID: 1034
		private static readonly ActionIndexCache act_command_leftstance = ActionIndexCache.Create("act_command_leftstance");

		// Token: 0x0400040B RID: 1035
		private static readonly ActionIndexCache act_command_unarmed = ActionIndexCache.Create("act_command_unarmed");

		// Token: 0x0400040C RID: 1036
		private static readonly ActionIndexCache act_command_unarmed_leftstance = ActionIndexCache.Create("act_command_unarmed_leftstance");

		// Token: 0x0400040D RID: 1037
		private static readonly ActionIndexCache act_command_2h = ActionIndexCache.Create("act_command_2h");

		// Token: 0x0400040E RID: 1038
		private static readonly ActionIndexCache act_command_2h_leftstance = ActionIndexCache.Create("act_command_2h_leftstance");

		// Token: 0x0400040F RID: 1039
		private static readonly ActionIndexCache act_command_bow = ActionIndexCache.Create("act_command_bow");

		// Token: 0x04000410 RID: 1040
		private static readonly ActionIndexCache act_command_follow = ActionIndexCache.Create("act_command_follow");

		// Token: 0x04000411 RID: 1041
		private static readonly ActionIndexCache act_command_follow_leftstance = ActionIndexCache.Create("act_command_follow_leftstance");

		// Token: 0x04000412 RID: 1042
		private static readonly ActionIndexCache act_command_follow_unarmed = ActionIndexCache.Create("act_command_follow_unarmed");

		// Token: 0x04000413 RID: 1043
		private static readonly ActionIndexCache act_command_follow_unarmed_leftstance = ActionIndexCache.Create("act_command_follow_unarmed_leftstance");

		// Token: 0x04000414 RID: 1044
		private static readonly ActionIndexCache act_command_follow_2h = ActionIndexCache.Create("act_command_follow_2h");

		// Token: 0x04000415 RID: 1045
		private static readonly ActionIndexCache act_command_follow_2h_leftstance = ActionIndexCache.Create("act_command_follow_2h_leftstance");

		// Token: 0x04000416 RID: 1046
		private static readonly ActionIndexCache act_command_follow_bow = ActionIndexCache.Create("act_command_follow_bow");

		// Token: 0x04000417 RID: 1047
		private static readonly ActionIndexCache act_horse_command = ActionIndexCache.Create("act_horse_command");

		// Token: 0x04000418 RID: 1048
		private static readonly ActionIndexCache act_horse_command_unarmed = ActionIndexCache.Create("act_horse_command_unarmed");

		// Token: 0x04000419 RID: 1049
		private static readonly ActionIndexCache act_horse_command_2h = ActionIndexCache.Create("act_horse_command_2h");

		// Token: 0x0400041A RID: 1050
		private static readonly ActionIndexCache act_horse_command_bow = ActionIndexCache.Create("act_horse_command_bow");

		// Token: 0x0400041B RID: 1051
		private static readonly ActionIndexCache act_horse_command_follow = ActionIndexCache.Create("act_horse_command_follow");

		// Token: 0x0400041C RID: 1052
		private static readonly ActionIndexCache act_horse_command_follow_unarmed = ActionIndexCache.Create("act_horse_command_follow_unarmed");

		// Token: 0x0400041D RID: 1053
		private static readonly ActionIndexCache act_horse_command_follow_2h = ActionIndexCache.Create("act_horse_command_follow_2h");

		// Token: 0x0400041E RID: 1054
		private static readonly ActionIndexCache act_horse_command_follow_bow = ActionIndexCache.Create("act_horse_command_follow_bow");

		// Token: 0x0400041F RID: 1055
		public const float FormationGapInLine = 1.5f;

		// Token: 0x04000420 RID: 1056
		private readonly Mission _mission;

		// Token: 0x04000421 RID: 1057
		private readonly Team _team;

		// Token: 0x04000422 RID: 1058
		public Agent Owner;

		// Token: 0x04000424 RID: 1060
		private readonly MBList<Formation> _selectedFormations;

		// Token: 0x04000428 RID: 1064
		private Dictionary<Formation, float> actualWidths;

		// Token: 0x04000429 RID: 1065
		private Dictionary<Formation, int> actualUnitSpacings;

		// Token: 0x0400042A RID: 1066
		private List<Func<Formation, MovementOrder, MovementOrder>> orderOverrides;

		// Token: 0x0400042B RID: 1067
		private List<ValueTuple<Formation, OrderType>> overridenOrders;

		// Token: 0x0400042C RID: 1068
		private bool _gesturesEnabled;

		// Token: 0x0400042D RID: 1069
		private bool _formationUpdateEnabledAfterSetOrder = true;
	}
}
