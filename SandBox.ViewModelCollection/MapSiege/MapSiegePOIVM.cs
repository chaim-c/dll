using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000032 RID: 50
	public class MapSiegePOIVM : ViewModel
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00011350 File Offset: 0x0000F550
		private SiegeEvent Siege
		{
			get
			{
				return PlayerSiege.PlayerSiegeEvent;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00011357 File Offset: 0x0000F557
		private BattleSideEnum PlayerSide
		{
			get
			{
				return PlayerSiege.PlayerSide;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001135E File Offset: 0x0000F55E
		private Settlement Settlement
		{
			get
			{
				return this.Siege.BesiegedSettlement;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001136B File Offset: 0x0000F56B
		public MapSiegePOIVM.POIType Type { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00011373 File Offset: 0x0000F573
		public int MachineIndex { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001137B File Offset: 0x0000F57B
		public float LatestW
		{
			get
			{
				return this._latestW;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00011383 File Offset: 0x0000F583
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0001138B File Offset: 0x0000F58B
		public SiegeEvent.SiegeEngineConstructionProgress Machine { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00011394 File Offset: 0x0000F594
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0001139C File Offset: 0x0000F59C
		public MatrixFrame MapSceneLocationFrame { get; private set; }

		// Token: 0x060003AD RID: 941 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public MapSiegePOIVM(MapSiegePOIVM.POIType type, MatrixFrame mapSceneLocation, Camera mapCamera, int machineIndex, Action<MapSiegePOIVM> onSelection)
		{
			this.Type = type;
			this._onSelection = onSelection;
			this._thisSide = ((this.Type == MapSiegePOIVM.POIType.AttackerRamSiegeMachine || this.Type == MapSiegePOIVM.POIType.AttackerTowerSiegeMachine || this.Type == MapSiegePOIVM.POIType.AttackerRangedSiegeMachine) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
			this.MapSceneLocationFrame = mapSceneLocation;
			this._mapSceneLocation = this.MapSceneLocationFrame.origin;
			this._mapCamera = mapCamera;
			this.MachineIndex = machineIndex;
			Color sidePrimaryColor;
			if (this._thisSide != BattleSideEnum.Attacker)
			{
				IFaction mapFaction = this.Siege.BesiegedSettlement.MapFaction;
				sidePrimaryColor = Color.FromUint((mapFaction != null) ? mapFaction.Color : 0U);
			}
			else
			{
				IFaction mapFaction2 = this.Siege.BesiegerCamp.LeaderParty.MapFaction;
				sidePrimaryColor = Color.FromUint((mapFaction2 != null) ? mapFaction2.Color : 0U);
			}
			this.SidePrimaryColor = sidePrimaryColor;
			Color sideSecondaryColor;
			if (this._thisSide != BattleSideEnum.Attacker)
			{
				IFaction mapFaction3 = this.Siege.BesiegedSettlement.MapFaction;
				sideSecondaryColor = Color.FromUint((mapFaction3 != null) ? mapFaction3.Color2 : 0U);
			}
			else
			{
				IFaction mapFaction4 = this.Siege.BesiegerCamp.LeaderParty.MapFaction;
				sideSecondaryColor = Color.FromUint((mapFaction4 != null) ? mapFaction4.Color2 : 0U);
			}
			this.SideSecondaryColor = sideSecondaryColor;
			this.IsPlayerSidePOI = this.DetermineIfPOIIsPlayerSide();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public void ExecuteSelection()
		{
			this._onSelection(this);
			this.IsSelected = true;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000114F8 File Offset: 0x0000F6F8
		public void UpdateProperties()
		{
			this.Machine = this.GetDesiredMachine();
			this._bindHasItem = (this.Type == MapSiegePOIVM.POIType.WallSection || this.Machine != null);
			SiegeEvent.SiegeEngineConstructionProgress machine = this.Machine;
			this._bindIsConstructing = (machine != null && !machine.IsActive);
			this.RefreshMachineType();
			this.RefreshHitpoints();
			this.RefreshQueueIndex();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011558 File Offset: 0x0000F758
		public void RefreshDistanceValue(float newDistance)
		{
			this._bindIsInVisibleRange = (newDistance <= 20f);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001156C File Offset: 0x0000F76C
		public void RefreshPosition()
		{
			this._latestX = 0f;
			this._latestY = 0f;
			this._latestW = 0f;
			MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, this._mapSceneLocation, ref this._latestX, ref this._latestY, ref this._latestW);
			this._bindWPos = this._latestW;
			this._bindWSign = (int)this._bindWPos;
			this._bindIsInside = this.IsInsideWindow();
			if (!this._bindIsInside)
			{
				this._bindPosition = new Vec2(-1000f, -1000f);
				return;
			}
			this._bindPosition = new Vec2(this._latestX, this._latestY);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011618 File Offset: 0x0000F818
		public void RefreshBinding()
		{
			this.Position = this._bindPosition;
			this.IsInside = this._bindIsInside;
			this.CurrentHitpoints = this._bindCurrentHitpoints;
			this.MaxHitpoints = this._bindMaxHitpoints;
			this.HasItem = this._bindHasItem;
			this.IsConstructing = this._bindIsConstructing;
			this.MachineType = this._bindMachineType;
			this.QueueIndex = this._bindQueueIndex;
			this.IsInVisibleRange = this._bindIsInVisibleRange;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011694 File Offset: 0x0000F894
		private void RefreshHitpoints()
		{
			if (this.Siege == null)
			{
				this._bindCurrentHitpoints = 0f;
				this._bindMaxHitpoints = 0f;
				return;
			}
			MapSiegePOIVM.POIType type = this.Type;
			if (type == MapSiegePOIVM.POIType.WallSection)
			{
				MBReadOnlyList<float> settlementWallSectionHitPointsRatioList = this.Settlement.SettlementWallSectionHitPointsRatioList;
				this._bindMaxHitpoints = this.Settlement.MaxWallHitPoints / (float)this.Settlement.WallSectionCount;
				this._bindCurrentHitpoints = settlementWallSectionHitPointsRatioList[this.MachineIndex] * this._bindMaxHitpoints;
				this._bindMachineType = ((this._bindCurrentHitpoints <= 0f) ? 1 : 0);
				return;
			}
			if (type - MapSiegePOIVM.POIType.DefenderSiegeMachine > 3)
			{
				return;
			}
			if (this.Machine == null)
			{
				this._bindCurrentHitpoints = 0f;
				this._bindMaxHitpoints = 0f;
				return;
			}
			if (this.Machine.IsActive)
			{
				this._bindCurrentHitpoints = this.Machine.Hitpoints;
				this._bindMaxHitpoints = this.Machine.MaxHitPoints;
				return;
			}
			if (this.Machine.IsBeingRedeployed)
			{
				this._bindCurrentHitpoints = this.Machine.RedeploymentProgress;
				this._bindMaxHitpoints = 1f;
				return;
			}
			this._bindCurrentHitpoints = this.Machine.Progress;
			this._bindMaxHitpoints = 1f;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000117C4 File Offset: 0x0000F9C4
		private void RefreshMachineType()
		{
			if (this.Siege == null)
			{
				this._bindMachineType = -1;
				return;
			}
			MapSiegePOIVM.POIType type = this.Type;
			if (type == MapSiegePOIVM.POIType.WallSection)
			{
				this._bindMachineType = 0;
				return;
			}
			if (type - MapSiegePOIVM.POIType.DefenderSiegeMachine > 3)
			{
				return;
			}
			this._bindMachineType = (int)((this.Machine != null) ? this.GetMachineTypeFromId(this.Machine.SiegeEngine.StringId) : MapSiegePOIVM.MachineTypes.None);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011824 File Offset: 0x0000FA24
		private void RefreshQueueIndex()
		{
			int bindQueueIndex;
			if (this.Machine == null)
			{
				bindQueueIndex = -1;
			}
			else
			{
				bindQueueIndex = (from e in this.Siege.GetSiegeEventSide(this.PlayerSide).SiegeEngines.DeployedSiegeEngines
				where !e.IsActive
				select e).ToList<SiegeEvent.SiegeEngineConstructionProgress>().IndexOf(this.Machine);
			}
			this._bindQueueIndex = bindQueueIndex;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00011894 File Offset: 0x0000FA94
		private bool DetermineIfPOIIsPlayerSide()
		{
			MapSiegePOIVM.POIType type = this.Type;
			if (type > MapSiegePOIVM.POIType.DefenderSiegeMachine)
			{
				return type - MapSiegePOIVM.POIType.AttackerRamSiegeMachine <= 2 && this.PlayerSide == BattleSideEnum.Attacker;
			}
			return this.PlayerSide == BattleSideEnum.Defender;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000118CC File Offset: 0x0000FACC
		private bool IsInsideWindow()
		{
			return this._latestX <= Screen.RealScreenResolutionWidth && this._latestY <= Screen.RealScreenResolutionHeight && this._latestX + 200f >= 0f && this._latestY + 100f >= 0f;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001191E File Offset: 0x0000FB1E
		public void ExecuteShowTooltip()
		{
			InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
			{
				SandBoxUIHelper.GetSiegeEngineInProgressTooltip(this.Machine)
			});
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011943 File Offset: 0x0000FB43
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001194C File Offset: 0x0000FB4C
		private MapSiegePOIVM.MachineTypes GetMachineTypeFromId(string id)
		{
			string text = id.ToLower();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num > 746114623U)
			{
				if (num <= 1820818168U)
				{
					if (num <= 1241455715U)
					{
						if (num != 808481256U)
						{
							if (num != 1241455715U)
							{
								return MapSiegePOIVM.MachineTypes.None;
							}
							if (!(text == "ram"))
							{
								return MapSiegePOIVM.MachineTypes.None;
							}
							return MapSiegePOIVM.MachineTypes.Ram;
						}
						else if (!(text == "fire_ballista"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
					}
					else if (num != 1748194790U)
					{
						if (num != 1820818168U)
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						if (!(text == "fire_onager"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Mangonel;
					}
					else
					{
						if (!(text == "fire_catapult"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Mangonel;
					}
				}
				else if (num <= 1898442385U)
				{
					if (num != 1839032341U)
					{
						if (num != 1898442385U)
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						if (!(text == "catapult"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Mangonel;
					}
					else
					{
						if (!(text == "trebuchet"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Trebuchet;
					}
				}
				else if (num != 2806198843U)
				{
					if (num != 4036530155U)
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					if (!(text == "ballista"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
				}
				else
				{
					if (!(text == "onager"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					return MapSiegePOIVM.MachineTypes.Mangonel;
				}
				return MapSiegePOIVM.MachineTypes.Ballista;
			}
			if (num > 473034592U)
			{
				if (num <= 712590611U)
				{
					if (num != 695812992U)
					{
						if (num != 712590611U)
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						if (!(text == "siege_tower_level2"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
					}
					else if (!(text == "siege_tower_level3"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
				}
				else if (num != 729368230U)
				{
					if (num != 746114623U)
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					if (!(text == "fire_mangonel"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					return MapSiegePOIVM.MachineTypes.Mangonel;
				}
				else if (!(text == "siege_tower_level1"))
				{
					return MapSiegePOIVM.MachineTypes.None;
				}
				return MapSiegePOIVM.MachineTypes.SiegeTower;
			}
			if (num != 6339497U)
			{
				if (num != 390431385U)
				{
					if (num != 473034592U)
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					if (!(text == "mangonel"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
				}
				else
				{
					if (!(text == "bricole"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					return MapSiegePOIVM.MachineTypes.Trebuchet;
				}
			}
			else
			{
				if (!(text == "ladder"))
				{
					return MapSiegePOIVM.MachineTypes.None;
				}
				return MapSiegePOIVM.MachineTypes.Ladder;
			}
			return MapSiegePOIVM.MachineTypes.Mangonel;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00011B70 File Offset: 0x0000FD70
		private SiegeEvent.SiegeEngineConstructionProgress GetDesiredMachine()
		{
			if (this.Siege != null)
			{
				switch (this.Type)
				{
				case MapSiegePOIVM.POIType.DefenderSiegeMachine:
					return this.Siege.GetSiegeEventSide(BattleSideEnum.Defender).SiegeEngines.DeployedRangedSiegeEngines[this.MachineIndex];
				case MapSiegePOIVM.POIType.AttackerRamSiegeMachine:
				case MapSiegePOIVM.POIType.AttackerTowerSiegeMachine:
					return this.Siege.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines[this.MachineIndex];
				case MapSiegePOIVM.POIType.AttackerRangedSiegeMachine:
					return this.Siege.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedRangedSiegeEngines[this.MachineIndex];
				}
				return null;
			}
			return null;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00011C05 File Offset: 0x0000FE05
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00011C0D File Offset: 0x0000FE0D
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00011C30 File Offset: 0x0000FE30
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00011C38 File Offset: 0x0000FE38
		public Color SidePrimaryColor
		{
			get
			{
				return this._sidePrimaryColor;
			}
			set
			{
				if (this._sidePrimaryColor != value)
				{
					this._sidePrimaryColor = value;
					base.OnPropertyChangedWithValue(value, "SidePrimaryColor");
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011C5B File Offset: 0x0000FE5B
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00011C63 File Offset: 0x0000FE63
		public Color SideSecondaryColor
		{
			get
			{
				return this._sideSecondaryColor;
			}
			set
			{
				if (this._sideSecondaryColor != value)
				{
					this._sideSecondaryColor = value;
					base.OnPropertyChangedWithValue(value, "SideSecondaryColor");
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00011C86 File Offset: 0x0000FE86
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00011C8E File Offset: 0x0000FE8E
		public int QueueIndex
		{
			get
			{
				return this._queueIndex;
			}
			set
			{
				if (this._queueIndex != value)
				{
					this._queueIndex = value;
					base.OnPropertyChangedWithValue(value, "QueueIndex");
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00011CAC File Offset: 0x0000FEAC
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public int MachineType
		{
			get
			{
				return this._machineType;
			}
			set
			{
				if (this._machineType != value)
				{
					this._machineType = value;
					base.OnPropertyChangedWithValue(value, "MachineType");
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00011CD2 File Offset: 0x0000FED2
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00011CDA File Offset: 0x0000FEDA
		public float CurrentHitpoints
		{
			get
			{
				return this._currentHitpoints;
			}
			set
			{
				if (this._currentHitpoints != value)
				{
					this._currentHitpoints = value;
					base.OnPropertyChangedWithValue(value, "CurrentHitpoints");
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00011D00 File Offset: 0x0000FF00
		public float MaxHitpoints
		{
			get
			{
				return this._maxHitpoints;
			}
			set
			{
				if (this._maxHitpoints != value)
				{
					this._maxHitpoints = value;
					base.OnPropertyChangedWithValue(value, "MaxHitpoints");
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00011D1E File Offset: 0x0000FF1E
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00011D26 File Offset: 0x0000FF26
		public bool IsPlayerSidePOI
		{
			get
			{
				return this._isPlayerSidePOI;
			}
			set
			{
				if (this._isPlayerSidePOI != value)
				{
					this._isPlayerSidePOI = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerSidePOI");
				}
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00011D44 File Offset: 0x0000FF44
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public bool IsFireVersion
		{
			get
			{
				return this._isFireVersion;
			}
			set
			{
				if (this._isFireVersion != value)
				{
					this._isFireVersion = value;
					base.OnPropertyChangedWithValue(value, "IsFireVersion");
				}
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00011D6A File Offset: 0x0000FF6A
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00011D72 File Offset: 0x0000FF72
		public bool IsInVisibleRange
		{
			get
			{
				return this._isInVisibleRange;
			}
			set
			{
				if (this._isInVisibleRange != value)
				{
					this._isInVisibleRange = value;
					base.OnPropertyChangedWithValue(value, "IsInVisibleRange");
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011D90 File Offset: 0x0000FF90
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00011D98 File Offset: 0x0000FF98
		public bool IsConstructing
		{
			get
			{
				return this._isConstructing;
			}
			set
			{
				if (this._isConstructing != value)
				{
					this._isConstructing = value;
					base.OnPropertyChangedWithValue(value, "IsConstructing");
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011DB6 File Offset: 0x0000FFB6
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011DBE File Offset: 0x0000FFBE
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (this._isSelected != value)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011DDC File Offset: 0x0000FFDC
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		public bool HasItem
		{
			get
			{
				return this._hasItem;
			}
			set
			{
				if (this._hasItem != value)
				{
					this._hasItem = value;
					base.OnPropertyChangedWithValue(value, "HasItem");
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011E02 File Offset: 0x00010002
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00011E0A File Offset: 0x0001000A
		public bool IsInside
		{
			get
			{
				return this._isInside;
			}
			set
			{
				if (this._isInside != value)
				{
					this._isInside = value;
					base.OnPropertyChangedWithValue(value, "IsInside");
				}
			}
		}

		// Token: 0x040001ED RID: 493
		private readonly Vec3 _mapSceneLocation;

		// Token: 0x040001EE RID: 494
		private readonly Camera _mapCamera;

		// Token: 0x040001EF RID: 495
		private readonly BattleSideEnum _thisSide;

		// Token: 0x040001F0 RID: 496
		private readonly Action<MapSiegePOIVM> _onSelection;

		// Token: 0x040001F1 RID: 497
		private float _latestX;

		// Token: 0x040001F2 RID: 498
		private float _latestY;

		// Token: 0x040001F3 RID: 499
		private float _latestW;

		// Token: 0x040001F4 RID: 500
		private float _bindCurrentHitpoints;

		// Token: 0x040001F5 RID: 501
		private float _bindMaxHitpoints;

		// Token: 0x040001F6 RID: 502
		private float _bindWPos;

		// Token: 0x040001F7 RID: 503
		private int _bindWSign;

		// Token: 0x040001F8 RID: 504
		private int _bindMachineType = -1;

		// Token: 0x040001F9 RID: 505
		private int _bindQueueIndex;

		// Token: 0x040001FA RID: 506
		private bool _bindIsInside;

		// Token: 0x040001FB RID: 507
		private bool _bindHasItem;

		// Token: 0x040001FC RID: 508
		private bool _bindIsConstructing;

		// Token: 0x040001FD RID: 509
		private Vec2 _bindPosition;

		// Token: 0x040001FE RID: 510
		private bool _bindIsInVisibleRange;

		// Token: 0x040001FF RID: 511
		private Color _sidePrimaryColor;

		// Token: 0x04000200 RID: 512
		private Color _sideSecondaryColor;

		// Token: 0x04000201 RID: 513
		private Vec2 _position;

		// Token: 0x04000202 RID: 514
		private float _currentHitpoints;

		// Token: 0x04000203 RID: 515
		private int _machineType = -1;

		// Token: 0x04000204 RID: 516
		private float _maxHitpoints;

		// Token: 0x04000205 RID: 517
		private int _queueIndex;

		// Token: 0x04000206 RID: 518
		private bool _isInside;

		// Token: 0x04000207 RID: 519
		private bool _hasItem;

		// Token: 0x04000208 RID: 520
		private bool _isConstructing;

		// Token: 0x04000209 RID: 521
		private bool _isPlayerSidePOI;

		// Token: 0x0400020A RID: 522
		private bool _isFireVersion;

		// Token: 0x0400020B RID: 523
		private bool _isInVisibleRange;

		// Token: 0x0400020C RID: 524
		private bool _isSelected;

		// Token: 0x02000095 RID: 149
		public enum POIType
		{
			// Token: 0x04000342 RID: 834
			WallSection,
			// Token: 0x04000343 RID: 835
			DefenderSiegeMachine,
			// Token: 0x04000344 RID: 836
			AttackerRamSiegeMachine,
			// Token: 0x04000345 RID: 837
			AttackerTowerSiegeMachine,
			// Token: 0x04000346 RID: 838
			AttackerRangedSiegeMachine
		}

		// Token: 0x02000096 RID: 150
		public enum MachineTypes
		{
			// Token: 0x04000348 RID: 840
			None = -1,
			// Token: 0x04000349 RID: 841
			Wall,
			// Token: 0x0400034A RID: 842
			BrokenWall,
			// Token: 0x0400034B RID: 843
			Ballista,
			// Token: 0x0400034C RID: 844
			Trebuchet,
			// Token: 0x0400034D RID: 845
			Ladder,
			// Token: 0x0400034E RID: 846
			Ram,
			// Token: 0x0400034F RID: 847
			SiegeTower,
			// Token: 0x04000350 RID: 848
			Mangonel
		}
	}
}
