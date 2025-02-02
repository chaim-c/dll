using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000036 RID: 54
	public class MapSiegeVM : ViewModel
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x000124AD File Offset: 0x000106AD
		private bool IsPlayerLeaderOfSiegeEvent
		{
			get
			{
				SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
				return playerSiegeEvent != null && playerSiegeEvent.IsPlayerSiegeEvent && Campaign.Current.Models.EncounterModel.GetLeaderOfSiegeEvent(PlayerSiege.PlayerSiegeEvent, PlayerSiege.PlayerSide) == Hero.MainHero;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000124EC File Offset: 0x000106EC
		public MapSiegeVM(Camera mapCamera, MatrixFrame[] batteringRamFrames, MatrixFrame[] rangedSiegeEngineFrames, MatrixFrame[] towerSiegeEngineFrames, MatrixFrame[] defenderSiegeEngineFrames, MatrixFrame[] breachableWallFrames)
		{
			this._mapCamera = mapCamera;
			this.PointsOfInterest = new MBBindingList<MapSiegePOIVM>();
			this._poiDistanceComparer = new MapSiegeVM.SiegePOIDistanceComparer();
			for (int i = 0; i < batteringRamFrames.Length; i++)
			{
				this.PointsOfInterest.Add(new MapSiegePOIVM(MapSiegePOIVM.POIType.AttackerRamSiegeMachine, batteringRamFrames[i], this._mapCamera, i, new Action<MapSiegePOIVM>(this.OnPOISelection)));
			}
			for (int j = 0; j < rangedSiegeEngineFrames.Length; j++)
			{
				this.PointsOfInterest.Add(new MapSiegePOIVM(MapSiegePOIVM.POIType.AttackerRangedSiegeMachine, rangedSiegeEngineFrames[j], this._mapCamera, j, new Action<MapSiegePOIVM>(this.OnPOISelection)));
			}
			for (int k = 0; k < towerSiegeEngineFrames.Length; k++)
			{
				this.PointsOfInterest.Add(new MapSiegePOIVM(MapSiegePOIVM.POIType.AttackerTowerSiegeMachine, towerSiegeEngineFrames[k], this._mapCamera, batteringRamFrames.Length + k, new Action<MapSiegePOIVM>(this.OnPOISelection)));
			}
			for (int l = 0; l < defenderSiegeEngineFrames.Length; l++)
			{
				this.PointsOfInterest.Add(new MapSiegePOIVM(MapSiegePOIVM.POIType.DefenderSiegeMachine, defenderSiegeEngineFrames[l], this._mapCamera, l, new Action<MapSiegePOIVM>(this.OnPOISelection)));
			}
			for (int m = 0; m < breachableWallFrames.Length; m++)
			{
				this.PointsOfInterest.Add(new MapSiegePOIVM(MapSiegePOIVM.POIType.WallSection, breachableWallFrames[m], this._mapCamera, m, new Action<MapSiegePOIVM>(this.OnPOISelection)));
			}
			this.ProductionController = new MapSiegeProductionVM();
			this.RefreshValues();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001265C File Offset: 0x0001085C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PreparationTitleText = GameTexts.FindText("str_building_siege_camp", null).ToString();
			this.ProductionController.RefreshValues();
			this.PointsOfInterest.ApplyActionOnAllItems(delegate(MapSiegePOIVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000126BA File Offset: 0x000108BA
		private void OnPOISelection(MapSiegePOIVM poi)
		{
			if (this.ProductionController.LatestSelectedPOI != null)
			{
				this.ProductionController.LatestSelectedPOI.IsSelected = false;
			}
			if (this.IsPlayerLeaderOfSiegeEvent)
			{
				this.ProductionController.OnMachineSelection(poi);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000126F0 File Offset: 0x000108F0
		public void OnSelectionFromScene(MatrixFrame frameOfEngine)
		{
			if (PlayerSiege.PlayerSiegeEvent != null)
			{
				Settlement besiegedSettlement = PlayerSiege.BesiegedSettlement;
				if ((besiegedSettlement == null || besiegedSettlement.CurrentSiegeState != Settlement.SiegeState.InTheLordsHall) && this.IsPlayerLeaderOfSiegeEvent)
				{
					IEnumerable<MapSiegePOIVM> enumerable = from poi in this.PointsOfInterest
					where frameOfEngine.NearlyEquals(poi.MapSceneLocationFrame, 1E-05f)
					select poi;
					if (enumerable == null)
					{
						return;
					}
					MapSiegePOIVM mapSiegePOIVM = enumerable.FirstOrDefault<MapSiegePOIVM>();
					if (mapSiegePOIVM == null)
					{
						return;
					}
					mapSiegePOIVM.ExecuteSelection();
				}
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012760 File Offset: 0x00010960
		public void Update(float mapCameraDistanceValue)
		{
			SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
			this.IsPreparationsCompleted = ((playerSiegeEvent != null && playerSiegeEvent.BesiegerCamp.IsPreparationComplete) || PlayerSiege.PlayerSide == BattleSideEnum.Defender);
			SiegeEvent playerSiegeEvent2 = PlayerSiege.PlayerSiegeEvent;
			float? num;
			if (playerSiegeEvent2 == null)
			{
				num = null;
			}
			else
			{
				SiegeEvent.SiegeEnginesContainer siegeEngines = playerSiegeEvent2.BesiegerCamp.SiegeEngines;
				if (siegeEngines == null)
				{
					num = null;
				}
				else
				{
					SiegeEvent.SiegeEngineConstructionProgress siegePreparations = siegeEngines.SiegePreparations;
					num = ((siegePreparations != null) ? new float?(siegePreparations.Progress) : null);
				}
			}
			this.PreparationProgress = (num ?? 0f);
			TWParallel.For(0, this.PointsOfInterest.Count, delegate(int startInclusive, int endExclusive)
			{
				for (int i = startInclusive; i < endExclusive; i++)
				{
					this.PointsOfInterest[i].RefreshDistanceValue(mapCameraDistanceValue);
					this.PointsOfInterest[i].RefreshPosition();
					this.PointsOfInterest[i].UpdateProperties();
				}
			}, 16);
			foreach (MapSiegePOIVM mapSiegePOIVM in this.PointsOfInterest)
			{
				mapSiegePOIVM.RefreshBinding();
			}
			this.ProductionController.Update();
			this.PointsOfInterest.Sort(this._poiDistanceComparer);
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001288C File Offset: 0x00010A8C
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00012894 File Offset: 0x00010A94
		[DataSourceProperty]
		public float PreparationProgress
		{
			get
			{
				return this._preparationProgress;
			}
			set
			{
				if (value != this._preparationProgress)
				{
					this._preparationProgress = value;
					base.OnPropertyChangedWithValue(value, "PreparationProgress");
				}
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000128B2 File Offset: 0x00010AB2
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000128BA File Offset: 0x00010ABA
		[DataSourceProperty]
		public bool IsPreparationsCompleted
		{
			get
			{
				return this._isPreparationsCompleted;
			}
			set
			{
				if (value != this._isPreparationsCompleted)
				{
					this._isPreparationsCompleted = value;
					base.OnPropertyChangedWithValue(value, "IsPreparationsCompleted");
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000128D8 File Offset: 0x00010AD8
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x000128E0 File Offset: 0x00010AE0
		[DataSourceProperty]
		public string PreparationTitleText
		{
			get
			{
				return this._preparationTitleText;
			}
			set
			{
				if (value != this._preparationTitleText)
				{
					this._preparationTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "PreparationTitleText");
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00012903 File Offset: 0x00010B03
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0001290B File Offset: 0x00010B0B
		[DataSourceProperty]
		public MapSiegeProductionVM ProductionController
		{
			get
			{
				return this._productionController;
			}
			set
			{
				if (value != this._productionController)
				{
					this._productionController = value;
					base.OnPropertyChangedWithValue<MapSiegeProductionVM>(value, "ProductionController");
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00012929 File Offset: 0x00010B29
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00012931 File Offset: 0x00010B31
		[DataSourceProperty]
		public MBBindingList<MapSiegePOIVM> PointsOfInterest
		{
			get
			{
				return this._pointsOfInterest;
			}
			set
			{
				if (value != this._pointsOfInterest)
				{
					this._pointsOfInterest = value;
					base.OnPropertyChangedWithValue<MBBindingList<MapSiegePOIVM>>(value, "PointsOfInterest");
				}
			}
		}

		// Token: 0x04000219 RID: 537
		private readonly Camera _mapCamera;

		// Token: 0x0400021A RID: 538
		private readonly MapSiegeVM.SiegePOIDistanceComparer _poiDistanceComparer;

		// Token: 0x0400021B RID: 539
		private MBBindingList<MapSiegePOIVM> _pointsOfInterest;

		// Token: 0x0400021C RID: 540
		private MapSiegeProductionVM _productionController;

		// Token: 0x0400021D RID: 541
		private float _preparationProgress;

		// Token: 0x0400021E RID: 542
		private string _preparationTitleText;

		// Token: 0x0400021F RID: 543
		private bool _isPreparationsCompleted;

		// Token: 0x0200009B RID: 155
		public class SiegePOIDistanceComparer : IComparer<MapSiegePOIVM>
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x00014F68 File Offset: 0x00013168
			public int Compare(MapSiegePOIVM x, MapSiegePOIVM y)
			{
				return y.LatestW.CompareTo(x.LatestW);
			}
		}
	}
}
