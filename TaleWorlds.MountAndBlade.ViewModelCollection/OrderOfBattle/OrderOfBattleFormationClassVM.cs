using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x0200002C RID: 44
	public class OrderOfBattleFormationClassVM : ViewModel
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000E478 File Offset: 0x0000C678
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000E480 File Offset: 0x0000C680
		public FormationClass Class
		{
			get
			{
				return this._class;
			}
			set
			{
				if (value != this._class)
				{
					if (!this._isFormationClassPreset)
					{
						Action<OrderOfBattleFormationClassVM, FormationClass> onClassChanged = OrderOfBattleFormationClassVM.OnClassChanged;
						if (onClassChanged != null)
						{
							onClassChanged(this, value);
						}
					}
					this._class = value;
					this.IsUnset = (this._class == FormationClass.NumberOfAllFormations);
					this.ShownFormationClass = (int)(this.IsUnset ? FormationClass.Infantry : (this._class + 1));
					this.UpdateWeightText();
					this._isFormationClassPreset = false;
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E4ED File Offset: 0x0000C6ED
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000E4F5 File Offset: 0x0000C6F5
		public int PreviousWeight { get; private set; }

		// Token: 0x06000331 RID: 817 RVA: 0x0000E500 File Offset: 0x0000C700
		public OrderOfBattleFormationClassVM(OrderOfBattleFormationItemVM formationItem, FormationClass formationClass = FormationClass.NumberOfAllFormations)
		{
			this.BelongedFormationItem = formationItem;
			this._isFormationClassPreset = (formationClass != FormationClass.NumberOfAllFormations);
			this.Class = formationClass;
			this.PreviousWeight = 0;
			this.OnWeightAdjusted();
			this.RefreshValues();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E553 File Offset: 0x0000C753
		public override void RefreshValues()
		{
			this.LockWeightHint = new HintViewModel(new TextObject("{=mPCrz4rs}Lock troop percentage from relative changes.", null), null);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000E56C File Offset: 0x0000C76C
		private void OnWeightAdjusted()
		{
			if (!this._isLockedOfWeightAdjustments)
			{
				Action<OrderOfBattleFormationClassVM> onWeightAdjustedCallback = OrderOfBattleFormationClassVM.OnWeightAdjustedCallback;
				if (onWeightAdjustedCallback != null)
				{
					onWeightAdjustedCallback(this);
				}
			}
			this.UpdateWeightText();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E590 File Offset: 0x0000C790
		public void UpdateWeightText()
		{
			if (this.Class != FormationClass.NumberOfAllFormations && OrderOfBattleFormationClassVM.GetTotalCountOfTroopType != null)
			{
				GameTexts.SetVariable("NUMBER", this.Weight);
				GameTexts.SetVariable("PERCENTAGE", GameTexts.FindText("str_NUMBER_percent", null));
				GameTexts.SetVariable("TROOP_COUNT", OrderOfBattleUIHelper.GetVisibleCountOfUnitsInClass(this));
				GameTexts.SetVariable("TOTAL_TROOP_COUNT", OrderOfBattleFormationClassVM.GetTotalCountOfTroopType(this.Class));
				this.WeightText = this._weightWithTroopCountText.ToString();
				return;
			}
			this.WeightText = string.Empty;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000E61A File Offset: 0x0000C81A
		public void SetWeightAdjustmentLock(bool isLocked)
		{
			this._isLockedOfWeightAdjustments = isLocked;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000E623 File Offset: 0x0000C823
		public void UpdateWeightAdjustable()
		{
			bool isAdjustable;
			if (this.Class != FormationClass.NumberOfAllFormations)
			{
				Func<OrderOfBattleFormationClassVM, bool> canAdjustWeight = OrderOfBattleFormationClassVM.CanAdjustWeight;
				isAdjustable = (canAdjustWeight != null && canAdjustWeight(this));
			}
			else
			{
				isAdjustable = false;
			}
			this.IsAdjustable = isAdjustable;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000E64A File Offset: 0x0000C84A
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000E652 File Offset: 0x0000C852
		[DataSourceProperty]
		public bool IsAdjustable
		{
			get
			{
				return this._isAdjustable;
			}
			set
			{
				if (value != this._isAdjustable)
				{
					this._isAdjustable = (value && Mission.Current.PlayerTeam.IsPlayerGeneral);
					base.OnPropertyChangedWithValue(this._isAdjustable, "IsAdjustable");
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E689 File Offset: 0x0000C889
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000E691 File Offset: 0x0000C891
		[DataSourceProperty]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (value != this._isLocked)
				{
					this._isLocked = value;
					base.OnPropertyChangedWithValue(value, "IsLocked");
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000E6AF File Offset: 0x0000C8AF
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000E6B7 File Offset: 0x0000C8B7
		[DataSourceProperty]
		public bool IsUnset
		{
			get
			{
				return this._isUnset;
			}
			set
			{
				if (value != this._isUnset)
				{
					this._isUnset = value;
					base.OnPropertyChangedWithValue(value, "IsUnset");
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E6D5 File Offset: 0x0000C8D5
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000E6DD File Offset: 0x0000C8DD
		[DataSourceProperty]
		public int Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				if (value != this._weight)
				{
					this.PreviousWeight = this._weight;
					this._weight = value;
					base.OnPropertyChangedWithValue(value, "Weight");
					this.OnWeightAdjusted();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E70D File Offset: 0x0000C90D
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000E715 File Offset: 0x0000C915
		[DataSourceProperty]
		public int ShownFormationClass
		{
			get
			{
				return this._shownFormationClass;
			}
			set
			{
				if (value != this._shownFormationClass)
				{
					this._shownFormationClass = value;
					base.OnPropertyChangedWithValue(value, "ShownFormationClass");
				}
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E733 File Offset: 0x0000C933
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000E73B File Offset: 0x0000C93B
		[DataSourceProperty]
		public string WeightText
		{
			get
			{
				return this._weightText;
			}
			set
			{
				if (value != this._weightText)
				{
					this._weightText = value;
					base.OnPropertyChangedWithValue<string>(value, "WeightText");
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E75E File Offset: 0x0000C95E
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000E766 File Offset: 0x0000C966
		[DataSourceProperty]
		public HintViewModel LockWeightHint
		{
			get
			{
				return this._lockWeightHint;
			}
			set
			{
				if (value != this._lockWeightHint)
				{
					this._lockWeightHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LockWeightHint");
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E784 File Offset: 0x0000C984
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000E78C File Offset: 0x0000C98C
		[DataSourceProperty]
		public bool IsWeightHighlightActive
		{
			get
			{
				return this._isWeightHighlightActive;
			}
			set
			{
				if (value != this._isWeightHighlightActive)
				{
					this._isWeightHighlightActive = value;
					base.OnPropertyChangedWithValue(value, "IsWeightHighlightActive");
				}
			}
		}

		// Token: 0x04000188 RID: 392
		private FormationClass _class;

		// Token: 0x04000189 RID: 393
		private bool _isLockedOfWeightAdjustments;

		// Token: 0x0400018B RID: 395
		public readonly OrderOfBattleFormationItemVM BelongedFormationItem;

		// Token: 0x0400018C RID: 396
		public static Action<OrderOfBattleFormationClassVM> OnWeightAdjustedCallback;

		// Token: 0x0400018D RID: 397
		public static Action<OrderOfBattleFormationClassVM, FormationClass> OnClassChanged;

		// Token: 0x0400018E RID: 398
		public static Func<OrderOfBattleFormationClassVM, bool> CanAdjustWeight;

		// Token: 0x0400018F RID: 399
		public static Func<FormationClass, int> GetTotalCountOfTroopType;

		// Token: 0x04000190 RID: 400
		private readonly TextObject _weightWithTroopCountText = new TextObject("{=s6qslcQY}{PERCENTAGE} ({TROOP_COUNT}/{TOTAL_TROOP_COUNT})", null);

		// Token: 0x04000191 RID: 401
		private bool _isFormationClassPreset;

		// Token: 0x04000192 RID: 402
		private bool _isAdjustable;

		// Token: 0x04000193 RID: 403
		private bool _isLocked;

		// Token: 0x04000194 RID: 404
		private bool _isUnset;

		// Token: 0x04000195 RID: 405
		private int _weight;

		// Token: 0x04000196 RID: 406
		private int _shownFormationClass;

		// Token: 0x04000197 RID: 407
		private string _weightText;

		// Token: 0x04000198 RID: 408
		private HintViewModel _lockWeightHint;

		// Token: 0x04000199 RID: 409
		private bool _isWeightHighlightActive;
	}
}
