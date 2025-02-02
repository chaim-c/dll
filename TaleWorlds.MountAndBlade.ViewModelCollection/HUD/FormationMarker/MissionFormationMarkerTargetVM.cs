using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker
{
	// Token: 0x02000051 RID: 81
	public class MissionFormationMarkerTargetVM : ViewModel
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001A0DE File Offset: 0x000182DE
		public Formation Formation { get; private set; }

		// Token: 0x06000673 RID: 1651 RVA: 0x0001A0E8 File Offset: 0x000182E8
		public MissionFormationMarkerTargetVM(Formation formation)
		{
			this.Formation = formation;
			this.TeamType = (this.Formation.Team.IsPlayerTeam ? 0 : (this.Formation.Team.IsPlayerAlly ? 1 : 2));
			this.FormationType = MissionFormationMarkerTargetVM.GetFormationType(this.Formation.RepresentativeClass);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001A149 File Offset: 0x00018349
		public void Refresh()
		{
			this.Size = this.Formation.CountOfUnits;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001A15C File Offset: 0x0001835C
		public void SetTargetedState(bool isFocused, bool isTargetingAFormation)
		{
			this.IsCenterOfFocus = isFocused;
			this.IsTargetingAFormation = isTargetingAFormation;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001A16C File Offset: 0x0001836C
		public static string GetFormationType(FormationClass formationType)
		{
			switch (formationType)
			{
			case FormationClass.Infantry:
				return "Infantry_Light";
			case FormationClass.Ranged:
				return "Archer_Light";
			case FormationClass.Cavalry:
				return "Cavalry_Light";
			case FormationClass.HorseArcher:
				return "HorseArcher_Light";
			case FormationClass.NumberOfDefaultFormations:
			case FormationClass.HeavyInfantry:
			case FormationClass.NumberOfRegularFormations:
			case FormationClass.Bodyguard:
			case FormationClass.NumberOfAllFormations:
				return "Infantry_Heavy";
			case FormationClass.LightCavalry:
				return "Cavalry_Light";
			case FormationClass.HeavyCavalry:
				return "Cavalry_Heavy";
			default:
				return "None";
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001A1DC File Offset: 0x000183DC
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001A1E4 File Offset: 0x000183E4
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (this._isEnabled != value)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001A202 File Offset: 0x00018402
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0001A20A File Offset: 0x0001840A
		[DataSourceProperty]
		public bool IsCenterOfFocus
		{
			get
			{
				return this._isCenterOfFocus;
			}
			set
			{
				if (this._isCenterOfFocus != value)
				{
					this._isCenterOfFocus = value;
					base.OnPropertyChangedWithValue(value, "IsCenterOfFocus");
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001A228 File Offset: 0x00018428
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001A230 File Offset: 0x00018430
		[DataSourceProperty]
		public bool IsFormationTargetRelevant
		{
			get
			{
				return this._isFormationTargetRelevant;
			}
			set
			{
				if (this._isFormationTargetRelevant != value)
				{
					this._isFormationTargetRelevant = value;
					base.OnPropertyChangedWithValue(value, "IsFormationTargetRelevant");
				}
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001A24E File Offset: 0x0001844E
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001A256 File Offset: 0x00018456
		[DataSourceProperty]
		public bool IsTargetingAFormation
		{
			get
			{
				return this._isTargetingAFormation;
			}
			set
			{
				if (this._isTargetingAFormation != value)
				{
					this._isTargetingAFormation = value;
					base.OnPropertyChangedWithValue(value, "IsTargetingAFormation");
				}
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001A274 File Offset: 0x00018474
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001A27C File Offset: 0x0001847C
		[DataSourceProperty]
		public string FormationType
		{
			get
			{
				return this._formationType;
			}
			set
			{
				if (this._formationType != value)
				{
					this._formationType = value;
					base.OnPropertyChangedWithValue<string>(value, "FormationType");
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001A29F File Offset: 0x0001849F
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001A2A7 File Offset: 0x000184A7
		[DataSourceProperty]
		public int TeamType
		{
			get
			{
				return this._teamType;
			}
			set
			{
				if (this._teamType != value)
				{
					this._teamType = value;
					base.OnPropertyChangedWithValue(value, "TeamType");
				}
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001A2C5 File Offset: 0x000184C5
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0001A2CD File Offset: 0x000184CD
		[DataSourceProperty]
		public bool IsInsideScreenBoundaries
		{
			get
			{
				return this._isInsideScreenBoundaries;
			}
			set
			{
				if (this._isInsideScreenBoundaries != value)
				{
					this._isInsideScreenBoundaries = value;
					base.OnPropertyChangedWithValue(value, "IsInsideScreenBoundaries");
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001A2EB File Offset: 0x000184EB
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0001A2F3 File Offset: 0x000184F3
		[DataSourceProperty]
		public Vec2 ScreenPosition
		{
			get
			{
				return this._screenPosition;
			}
			set
			{
				if (value.x != this._screenPosition.x || value.y != this._screenPosition.y)
				{
					this._screenPosition = value;
					base.OnPropertyChangedWithValue(value, "ScreenPosition");
				}
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001A32E File Offset: 0x0001852E
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001A336 File Offset: 0x00018536
		[DataSourceProperty]
		public float Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (this._distance != value && !float.IsNaN(value))
				{
					this._distance = value;
					base.OnPropertyChangedWithValue(value, "Distance");
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001A35C File Offset: 0x0001855C
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001A364 File Offset: 0x00018564
		[DataSourceProperty]
		public int Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (this._size != value)
				{
					this._size = value;
					base.OnPropertyChangedWithValue(value, "Size");
				}
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001A382 File Offset: 0x00018582
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001A38A File Offset: 0x0001858A
		[DataSourceProperty]
		public int WSign
		{
			get
			{
				return this._wSign;
			}
			set
			{
				if (this._wSign != value)
				{
					this._wSign = value;
					base.OnPropertyChangedWithValue(value, "WSign");
				}
			}
		}

		// Token: 0x0400030E RID: 782
		private Vec2 _screenPosition;

		// Token: 0x0400030F RID: 783
		private float _distance;

		// Token: 0x04000310 RID: 784
		private bool _isEnabled;

		// Token: 0x04000311 RID: 785
		private bool _isInsideScreenBoundaries;

		// Token: 0x04000312 RID: 786
		private bool _isCenterOfFocus;

		// Token: 0x04000313 RID: 787
		private bool _isFormationTargetRelevant;

		// Token: 0x04000314 RID: 788
		private bool _isTargetingAFormation;

		// Token: 0x04000315 RID: 789
		private int _teamType;

		// Token: 0x04000316 RID: 790
		private int _size;

		// Token: 0x04000317 RID: 791
		private int _wSign;

		// Token: 0x04000318 RID: 792
		private string _formationType;

		// Token: 0x020000D2 RID: 210
		public enum TeamTypes
		{
			// Token: 0x0400060E RID: 1550
			PlayerTeam,
			// Token: 0x0400060F RID: 1551
			PlayerAllyTeam,
			// Token: 0x04000610 RID: 1552
			EnemyTeam
		}
	}
}
