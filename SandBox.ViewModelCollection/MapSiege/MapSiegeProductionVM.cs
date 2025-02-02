using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000033 RID: 51
	public class MapSiegeProductionVM : ViewModel
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00011E28 File Offset: 0x00010028
		private SiegeEvent Siege
		{
			get
			{
				return PlayerSiege.PlayerSiegeEvent;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00011E2F File Offset: 0x0001002F
		private BattleSideEnum PlayerSide
		{
			get
			{
				return PlayerSiege.PlayerSide;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00011E36 File Offset: 0x00010036
		private Settlement Settlement
		{
			get
			{
				return this.Siege.BesiegedSettlement;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00011E43 File Offset: 0x00010043
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00011E4B File Offset: 0x0001004B
		public MapSiegePOIVM LatestSelectedPOI { get; private set; }

		// Token: 0x060003DD RID: 989 RVA: 0x00011E54 File Offset: 0x00010054
		public MapSiegeProductionVM()
		{
			this.PossibleProductionMachines = new MBBindingList<MapSiegeProductionMachineVM>();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00011E67 File Offset: 0x00010067
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PossibleProductionMachines.ApplyActionOnAllItems(delegate(MapSiegeProductionMachineVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00011E9C File Offset: 0x0001009C
		public void Update()
		{
			if (this.IsEnabled && this.LatestSelectedPOI.Machine == null)
			{
				if (this.PossibleProductionMachines.Any((MapSiegeProductionMachineVM m) => m.IsReserveOption))
				{
					this.ExecuteDisable();
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00011EF0 File Offset: 0x000100F0
		public void OnMachineSelection(MapSiegePOIVM poi)
		{
			this.PossibleProductionMachines.Clear();
			this.LatestSelectedPOI = poi;
			MapSiegePOIVM latestSelectedPOI = this.LatestSelectedPOI;
			if (((latestSelectedPOI != null) ? latestSelectedPOI.Machine : null) != null)
			{
				this.PossibleProductionMachines.Add(new MapSiegeProductionMachineVM(new Action<MapSiegeProductionMachineVM>(this.OnPossibleMachineSelection), !this.LatestSelectedPOI.Machine.IsActive && !this.LatestSelectedPOI.Machine.IsBeingRedeployed));
			}
			else
			{
				IEnumerable<SiegeEngineType> enumerable;
				switch (poi.Type)
				{
				case MapSiegePOIVM.POIType.DefenderSiegeMachine:
					enumerable = this.GetAllDefenderMachines();
					goto IL_BE;
				case MapSiegePOIVM.POIType.AttackerRamSiegeMachine:
					enumerable = this.GetAllAttackerRamMachines();
					goto IL_BE;
				case MapSiegePOIVM.POIType.AttackerTowerSiegeMachine:
					enumerable = this.GetAllAttackerTowerMachines();
					goto IL_BE;
				case MapSiegePOIVM.POIType.AttackerRangedSiegeMachine:
					enumerable = this.GetAllAttackerRangedMachines();
					goto IL_BE;
				}
				this.IsEnabled = false;
				return;
				IL_BE:
				using (IEnumerator<SiegeEngineType> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SiegeEngineType desMachine = enumerator.Current;
						int number = this.Siege.GetSiegeEventSide(this.PlayerSide).SiegeEngines.ReservedSiegeEngines.Count((SiegeEvent.SiegeEngineConstructionProgress m) => m.SiegeEngine == desMachine);
						this.PossibleProductionMachines.Add(new MapSiegeProductionMachineVM(desMachine, number, new Action<MapSiegeProductionMachineVM>(this.OnPossibleMachineSelection)));
					}
				}
			}
			this.IsEnabled = true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012054 File Offset: 0x00010254
		private void OnPossibleMachineSelection(MapSiegeProductionMachineVM machine)
		{
			if (this.LatestSelectedPOI.Machine == null || this.LatestSelectedPOI.Machine.SiegeEngine != machine.Engine)
			{
				ISiegeEventSide siegeEventSide = this.Siege.GetSiegeEventSide(this.PlayerSide);
				if (machine.IsReserveOption && this.LatestSelectedPOI.Machine != null)
				{
					bool moveToReserve = this.LatestSelectedPOI.Machine.IsActive || this.LatestSelectedPOI.Machine.IsBeingRedeployed;
					siegeEventSide.SiegeEngines.RemoveDeployedSiegeEngine(this.LatestSelectedPOI.MachineIndex, this.LatestSelectedPOI.Machine.SiegeEngine.IsRanged, moveToReserve);
				}
				else
				{
					SiegeEvent.SiegeEngineConstructionProgress siegeEngineConstructionProgress = siegeEventSide.SiegeEngines.ReservedSiegeEngines.FirstOrDefault((SiegeEvent.SiegeEngineConstructionProgress e) => e.SiegeEngine == machine.Engine);
					if (siegeEngineConstructionProgress == null)
					{
						float siegeEngineHitPoints = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineHitPoints(PlayerSiege.PlayerSiegeEvent, machine.Engine, this.PlayerSide);
						siegeEngineConstructionProgress = new SiegeEvent.SiegeEngineConstructionProgress(machine.Engine, 0f, siegeEngineHitPoints);
					}
					if (siegeEventSide.SiegeStrategy != DefaultSiegeStrategies.Custom && Campaign.Current.Models.EncounterModel.GetLeaderOfSiegeEvent(this.Siege, siegeEventSide.BattleSide) == Hero.MainHero)
					{
						siegeEventSide.SetSiegeStrategy(DefaultSiegeStrategies.Custom);
					}
					siegeEventSide.SiegeEngines.DeploySiegeEngineAtIndex(siegeEngineConstructionProgress, this.LatestSelectedPOI.MachineIndex);
				}
				this.Siege.BesiegedSettlement.Party.SetVisualAsDirty();
				Game.Current.EventManager.TriggerEvent<PlayerStartEngineConstructionEvent>(new PlayerStartEngineConstructionEvent(machine.Engine));
			}
			this.IsEnabled = false;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012216 File Offset: 0x00010416
		public void ExecuteDisable()
		{
			this.IsEnabled = false;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001221F File Offset: 0x0001041F
		private IEnumerable<SiegeEngineType> GetAllDefenderMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableDefenderSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001223A File Offset: 0x0001043A
		private IEnumerable<SiegeEngineType> GetAllAttackerRangedMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableAttackerRangedSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012255 File Offset: 0x00010455
		private IEnumerable<SiegeEngineType> GetAllAttackerRamMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableAttackerRamSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012270 File Offset: 0x00010470
		private IEnumerable<SiegeEngineType> GetAllAttackerTowerMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableAttackerTowerSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001228B File Offset: 0x0001048B
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00012293 File Offset: 0x00010493
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000122B1 File Offset: 0x000104B1
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000122B9 File Offset: 0x000104B9
		[DataSourceProperty]
		public MBBindingList<MapSiegeProductionMachineVM> PossibleProductionMachines
		{
			get
			{
				return this._possibleProductionMachines;
			}
			set
			{
				if (value != this._possibleProductionMachines)
				{
					this._possibleProductionMachines = value;
					base.OnPropertyChangedWithValue<MBBindingList<MapSiegeProductionMachineVM>>(value, "PossibleProductionMachines");
				}
			}
		}

		// Token: 0x0400020E RID: 526
		private MBBindingList<MapSiegeProductionMachineVM> _possibleProductionMachines;

		// Token: 0x0400020F RID: 527
		private bool _isEnabled;
	}
}
