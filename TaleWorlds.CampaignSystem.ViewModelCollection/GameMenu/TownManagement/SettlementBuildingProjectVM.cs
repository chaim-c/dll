using System;
using Helpers;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x02000091 RID: 145
	public class SettlementBuildingProjectVM : SettlementProjectVM
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x00039A04 File Offset: 0x00037C04
		public SettlementBuildingProjectVM(Action<SettlementProjectVM, bool> onSelection, Action<SettlementProjectVM> onSetAsCurrent, Action onResetCurrent, Building building, Settlement settlement) : base(onSelection, onSetAsCurrent, onResetCurrent, building, settlement)
		{
			this.Level = building.CurrentLevel;
			this.MaxLevel = 3;
			this.DevelopmentLevelText = building.CurrentLevel.ToString();
			this.CanBuild = (this.Level < 3);
			base.IsDaily = false;
			this.RefreshValues();
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00039A6A File Offset: 0x00037C6A
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.AlreadyAtMaxText = new TextObject("{=ybLA7ZXp}Already at Max", null).ToString();
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00039A88 File Offset: 0x00037C88
		public override void RefreshProductionText()
		{
			base.RefreshProductionText();
			if (this.DevelopmentQueueIndex == 0)
			{
				GameTexts.SetVariable("LEFT", GameTexts.FindText("str_completion", null));
				int daysToComplete = BuildingHelper.GetDaysToComplete(base.Building, this._settlement.Town);
				TextObject textObject;
				if (daysToComplete != -1)
				{
					textObject = new TextObject("{=c5eYzHaM}{DAYS} {?DAY_IS_PLURAL}Days{?}Day{\\?} ({PERCENTAGE}%)", null);
					textObject.SetTextVariable("DAYS", daysToComplete);
					GameTexts.SetVariable("DAY_IS_PLURAL", (daysToComplete > 1) ? 1 : 0);
				}
				else
				{
					textObject = new TextObject("{=0TauthlH}Never ({PERCENTAGE}%)", null);
				}
				textObject.SetTextVariable("PERCENTAGE", (int)(BuildingHelper.GetProgressOfBuilding(base.Building, this._settlement.Town) * 100f));
				GameTexts.SetVariable("RIGHT", textObject);
				base.ProductionText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				return;
			}
			if (this.DevelopmentQueueIndex > 0)
			{
				GameTexts.SetVariable("NUMBER", this.DevelopmentQueueIndex);
				base.ProductionText = GameTexts.FindText("str_in_queue_with_number", null).ToString();
				return;
			}
			base.ProductionText = " ";
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00039B95 File Offset: 0x00037D95
		public override void ExecuteAddToQueue()
		{
			if (this._onSelection != null && this.CanBuild)
			{
				this._onSelection(this, false);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00039BB4 File Offset: 0x00037DB4
		public override void ExecuteSetAsActiveDevelopment()
		{
			if (this._onSelection != null && this.CanBuild)
			{
				this._onSelection(this, true);
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00039BD3 File Offset: 0x00037DD3
		public override void ExecuteSetAsCurrent()
		{
			Action<SettlementProjectVM> onSetAsCurrent = this._onSetAsCurrent;
			if (onSetAsCurrent == null)
			{
				return;
			}
			onSetAsCurrent(this);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00039BE6 File Offset: 0x00037DE6
		public override void ExecuteResetCurrent()
		{
			Action onResetCurrent = this._onResetCurrent;
			if (onResetCurrent == null)
			{
				return;
			}
			onResetCurrent();
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00039BF8 File Offset: 0x00037DF8
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00039C00 File Offset: 0x00037E00
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00039C1E File Offset: 0x00037E1E
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x00039C26 File Offset: 0x00037E26
		[DataSourceProperty]
		public string DevelopmentLevelText
		{
			get
			{
				return this._developmentLevelText;
			}
			set
			{
				if (value != this._developmentLevelText)
				{
					this._developmentLevelText = value;
					base.OnPropertyChangedWithValue<string>(value, "DevelopmentLevelText");
				}
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00039C49 File Offset: 0x00037E49
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x00039C51 File Offset: 0x00037E51
		[DataSourceProperty]
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value != this._level)
				{
					this._level = value;
					base.OnPropertyChangedWithValue(value, "Level");
				}
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00039C6F File Offset: 0x00037E6F
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00039C77 File Offset: 0x00037E77
		[DataSourceProperty]
		public int MaxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				if (value != this._maxLevel)
				{
					this._maxLevel = value;
					base.OnPropertyChangedWithValue(value, "MaxLevel");
				}
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00039C95 File Offset: 0x00037E95
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00039C9D File Offset: 0x00037E9D
		[DataSourceProperty]
		public int DevelopmentQueueIndex
		{
			get
			{
				return this._developmentQueueIndex;
			}
			set
			{
				if (value != this._developmentQueueIndex)
				{
					this._developmentQueueIndex = value;
					base.OnPropertyChangedWithValue(value, "DevelopmentQueueIndex");
				}
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00039CBB File Offset: 0x00037EBB
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00039CC3 File Offset: 0x00037EC3
		[DataSourceProperty]
		public bool IsInQueue
		{
			get
			{
				return this._isInQueue;
			}
			set
			{
				if (value != this._isInQueue)
				{
					this._isInQueue = value;
					base.OnPropertyChangedWithValue(value, "IsInQueue");
				}
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00039CE1 File Offset: 0x00037EE1
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x00039CE9 File Offset: 0x00037EE9
		[DataSourceProperty]
		public string AlreadyAtMaxText
		{
			get
			{
				return this._alreadyAtMaxText;
			}
			set
			{
				if (value != this._alreadyAtMaxText)
				{
					this._alreadyAtMaxText = value;
					base.OnPropertyChangedWithValue<string>(value, "AlreadyAtMaxText");
				}
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00039D0C File Offset: 0x00037F0C
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x00039D14 File Offset: 0x00037F14
		[DataSourceProperty]
		public bool CanBuild
		{
			get
			{
				return this._canBuild;
			}
			set
			{
				if (value != this._canBuild)
				{
					this._canBuild = value;
					base.OnPropertyChangedWithValue(value, "CanBuild");
				}
			}
		}

		// Token: 0x040006A2 RID: 1698
		private bool _isSelected;

		// Token: 0x040006A3 RID: 1699
		private string _alreadyAtMaxText;

		// Token: 0x040006A4 RID: 1700
		private string _developmentLevelText;

		// Token: 0x040006A5 RID: 1701
		private int _level;

		// Token: 0x040006A6 RID: 1702
		private int _maxLevel;

		// Token: 0x040006A7 RID: 1703
		private int _developmentQueueIndex = -1;

		// Token: 0x040006A8 RID: 1704
		private bool _canBuild;

		// Token: 0x040006A9 RID: 1705
		private bool _isInQueue;
	}
}
