using System;
using Helpers;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x02000096 RID: 150
	public abstract class SettlementProjectVM : ViewModel
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0003A874 File Offset: 0x00038A74
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x0003A87C File Offset: 0x00038A7C
		public bool IsDaily { get; protected set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003A885 File Offset: 0x00038A85
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x0003A890 File Offset: 0x00038A90
		public Building Building
		{
			get
			{
				return this._building;
			}
			set
			{
				this._building = value;
				this.Name = ((value != null) ? value.Name.ToString() : "");
				this.Explanation = ((value != null) ? value.Explanation.ToString() : "");
				this.VisualCode = ((value != null) ? value.BuildingType.StringId.ToLower() : "");
				int constructionCost = this.Building.GetConstructionCost();
				TextObject textObject;
				if (constructionCost > 0)
				{
					textObject = new TextObject("{=tAwRIPiy}Construction Cost: {COST}", null);
					textObject.SetTextVariable("COST", constructionCost);
				}
				else
				{
					textObject = TextObject.Empty;
				}
				this.ProductionCostText = ((value != null) ? textObject.ToString() : "");
				this.CurrentPositiveEffectText = ((value != null) ? value.GetBonusExplanation().ToString() : "");
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003A95C File Offset: 0x00038B5C
		protected SettlementProjectVM(Action<SettlementProjectVM, bool> onSelection, Action<SettlementProjectVM> onSetAsCurrent, Action onResetCurrent, Building building, Settlement settlement)
		{
			this._onSelection = onSelection;
			this._onSetAsCurrent = onSetAsCurrent;
			this._onResetCurrent = onResetCurrent;
			this.Building = building;
			this._settlement = settlement;
			this.Progress = (int)(BuildingHelper.GetProgressOfBuilding(building, this._settlement.Town) * 100f);
			this.RefreshValues();
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003A9EC File Offset: 0x00038BEC
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Building.BuildingType.IsDefaultProject)
			{
				this.CurrentPositiveEffectText = this.Building.BuildingType.GetExplanationAtLevel(this.Building.CurrentLevel).ToString();
				this.NextPositiveEffectText = "";
				return;
			}
			this.CurrentPositiveEffectText = this.GetBonusText(this.Building, this.Building.CurrentLevel);
			this.NextPositiveEffectText = this.GetBonusText(this.Building, this.Building.CurrentLevel + 1);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003AA80 File Offset: 0x00038C80
		private string GetBonusText(Building building, int level)
		{
			if (level == 0 || level == 4)
			{
				return "";
			}
			object obj = (level == 1) ? this.L1BonusText : ((level == 2) ? this.L2BonusText : this.L3BonusText);
			TextObject bonusExplanationOfLevel = this.GetBonusExplanationOfLevel(level);
			object obj2 = obj;
			obj2.SetTextVariable("BONUS", bonusExplanationOfLevel);
			return obj2.ToString();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003AAD2 File Offset: 0x00038CD2
		private void ExecuteShowTooltip()
		{
			InformationManager.ShowTooltip(typeof(Building), new object[]
			{
				this._building
			});
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003AAF2 File Offset: 0x00038CF2
		private void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003AAF9 File Offset: 0x00038CF9
		private TextObject GetBonusExplanationOfLevel(int level)
		{
			if (level >= 0 && level <= 3)
			{
				return this.Building.BuildingType.GetExplanationAtLevel(level);
			}
			return TextObject.Empty;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003AB1A File Offset: 0x00038D1A
		public virtual void RefreshProductionText()
		{
		}

		// Token: 0x06000EA0 RID: 3744
		public abstract void ExecuteAddToQueue();

		// Token: 0x06000EA1 RID: 3745
		public abstract void ExecuteSetAsActiveDevelopment();

		// Token: 0x06000EA2 RID: 3746
		public abstract void ExecuteSetAsCurrent();

		// Token: 0x06000EA3 RID: 3747
		public abstract void ExecuteResetCurrent();

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003AB1C File Offset: 0x00038D1C
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x0003AB24 File Offset: 0x00038D24
		[DataSourceProperty]
		public string VisualCode
		{
			get
			{
				return this._visualCode;
			}
			set
			{
				if (value != this._visualCode)
				{
					this._visualCode = value;
					base.OnPropertyChangedWithValue<string>(value, "VisualCode");
				}
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0003AB47 File Offset: 0x00038D47
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x0003AB4F File Offset: 0x00038D4F
		[DataSourceProperty]
		public string ProductionText
		{
			get
			{
				return this._productionText;
			}
			set
			{
				if (value != this._productionText)
				{
					this._productionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProductionText");
				}
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003AB72 File Offset: 0x00038D72
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x0003AB7A File Offset: 0x00038D7A
		[DataSourceProperty]
		public string CurrentPositiveEffectText
		{
			get
			{
				return this._currentPositiveEffectText;
			}
			set
			{
				if (value != this._currentPositiveEffectText)
				{
					this._currentPositiveEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentPositiveEffectText");
				}
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003AB9D File Offset: 0x00038D9D
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x0003ABA5 File Offset: 0x00038DA5
		[DataSourceProperty]
		public string NextPositiveEffectText
		{
			get
			{
				return this._nextPositiveEffectText;
			}
			set
			{
				if (value != this._nextPositiveEffectText)
				{
					this._nextPositiveEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextPositiveEffectText");
				}
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003ABC8 File Offset: 0x00038DC8
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0003ABD0 File Offset: 0x00038DD0
		[DataSourceProperty]
		public string ProductionCostText
		{
			get
			{
				return this._productionCostText;
			}
			set
			{
				if (value != this._productionCostText)
				{
					this._productionCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProductionCostText");
				}
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003ABF3 File Offset: 0x00038DF3
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0003ABFB File Offset: 0x00038DFB
		[DataSourceProperty]
		public bool IsCurrentActiveProject
		{
			get
			{
				return this._isCurrentActiveProject;
			}
			set
			{
				if (value != this._isCurrentActiveProject)
				{
					this._isCurrentActiveProject = value;
					base.OnPropertyChangedWithValue(value, "IsCurrentActiveProject");
				}
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003AC19 File Offset: 0x00038E19
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0003AC21 File Offset: 0x00038E21
		[DataSourceProperty]
		public int Progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				if (value != this._progress)
				{
					this._progress = value;
					base.OnPropertyChangedWithValue(value, "Progress");
				}
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0003AC3F File Offset: 0x00038E3F
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x0003AC47 File Offset: 0x00038E47
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0003AC6A File Offset: 0x00038E6A
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x0003AC72 File Offset: 0x00038E72
		[DataSourceProperty]
		public string Explanation
		{
			get
			{
				return this._explanation;
			}
			set
			{
				if (value != this._explanation)
				{
					this._explanation = value;
					base.OnPropertyChangedWithValue<string>(value, "Explanation");
				}
			}
		}

		// Token: 0x040006C2 RID: 1730
		public int Index;

		// Token: 0x040006C4 RID: 1732
		private Building _building;

		// Token: 0x040006C5 RID: 1733
		protected Action<SettlementProjectVM, bool> _onSelection;

		// Token: 0x040006C6 RID: 1734
		protected Action<SettlementProjectVM> _onSetAsCurrent;

		// Token: 0x040006C7 RID: 1735
		protected Action _onResetCurrent;

		// Token: 0x040006C8 RID: 1736
		protected Settlement _settlement;

		// Token: 0x040006C9 RID: 1737
		private readonly TextObject L1BonusText = new TextObject("{=PJZ8QYgA}L-I : {BONUS}", null);

		// Token: 0x040006CA RID: 1738
		private readonly TextObject L2BonusText = new TextObject("{=9i0wnjJK}L-II : {BONUS}", null);

		// Token: 0x040006CB RID: 1739
		private readonly TextObject L3BonusText = new TextObject("{=pRP2sOWP}L-III : {BONUS}", null);

		// Token: 0x040006CC RID: 1740
		private string _name;

		// Token: 0x040006CD RID: 1741
		private string _visualCode;

		// Token: 0x040006CE RID: 1742
		private string _explanation;

		// Token: 0x040006CF RID: 1743
		private string _currentPositiveEffectText;

		// Token: 0x040006D0 RID: 1744
		private string _nextPositiveEffectText;

		// Token: 0x040006D1 RID: 1745
		private string _productionCostText;

		// Token: 0x040006D2 RID: 1746
		private int _progress;

		// Token: 0x040006D3 RID: 1747
		private bool _isCurrentActiveProject;

		// Token: 0x040006D4 RID: 1748
		private string _productionText;
	}
}
