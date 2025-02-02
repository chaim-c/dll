using System;
using System.Collections.Generic;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000064 RID: 100
	public class StringOptionDataVM : GenericOptionDataVM
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x0001E970 File Offset: 0x0001CB70
		public StringOptionDataVM(OptionsVM optionsVM, ISelectionOptionData option, TextObject name, TextObject description) : base(optionsVM, option, name, description, OptionsVM.OptionsDataType.MultipleSelectionOption)
		{
			this.Selector = new SelectorVM<SelectorItemVM>(0, null);
			this._selectionOptionData = option;
			this.UpdateData(true);
			this._initialValue = (int)this.Option.GetValue(false);
			this.Selector.SelectedIndex = this._initialValue;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001E9C8 File Offset: 0x0001CBC8
		public override void UpdateData(bool initialUpdate)
		{
			base.UpdateData(initialUpdate);
			IEnumerable<SelectionData> selectableOptionNames = this._selectionOptionData.GetSelectableOptionNames();
			this.Selector.SetOnChangeAction(null);
			bool flag = (int)this.Option.GetValue(true) != this.Selector.SelectedIndex;
			this.Selector.ItemList.Clear();
			foreach (SelectionData selectionData in selectableOptionNames)
			{
				if (selectionData.IsLocalizationId)
				{
					TextObject s = Module.CurrentModule.GlobalTextManager.FindText(selectionData.Data, null);
					this.Selector.AddItem(new SelectorItemVM(s));
				}
				else
				{
					this.Selector.AddItem(new SelectorItemVM(selectionData.Data));
				}
			}
			int num = (int)this.Option.GetValue(!initialUpdate);
			if (this.Selector.ItemList.Count > 0 && num == -1)
			{
				num = 0;
			}
			this.Selector.SelectedIndex = num;
			this.Selector.SetOnChangeAction(new Action<SelectorVM<SelectorItemVM>>(this.UpdateValue));
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001EAEC File Offset: 0x0001CCEC
		public override void RefreshValues()
		{
			base.RefreshValues();
			SelectorVM<SelectorItemVM> selector = this.Selector;
			if (selector == null)
			{
				return;
			}
			selector.RefreshValues();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001EB04 File Offset: 0x0001CD04
		public void UpdateValue(SelectorVM<SelectorItemVM> selector)
		{
			if (selector.SelectedIndex >= 0)
			{
				this.Option.SetValue((float)selector.SelectedIndex);
				this.Option.Commit();
				this._optionsVM.SetConfig(this.Option, (float)selector.SelectedIndex);
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001EB44 File Offset: 0x0001CD44
		public override void UpdateValue()
		{
			if (this.Selector.SelectedIndex >= 0 && (float)this.Selector.SelectedIndex != this.Option.GetValue(false))
			{
				this.Option.Commit();
				this._optionsVM.SetConfig(this.Option, (float)this.Selector.SelectedIndex);
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001EBA1 File Offset: 0x0001CDA1
		public override void Cancel()
		{
			this.Selector.SelectedIndex = this._initialValue;
			this.UpdateValue();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001EBBA File Offset: 0x0001CDBA
		public override void SetValue(float value)
		{
			this.Selector.SelectedIndex = (int)value;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001EBC9 File Offset: 0x0001CDC9
		public override void ResetData()
		{
			this.Selector.SelectedIndex = (int)this.Option.GetDefaultValue();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001EBE2 File Offset: 0x0001CDE2
		public override bool IsChanged()
		{
			return this._initialValue != this.Selector.SelectedIndex;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001EBFA File Offset: 0x0001CDFA
		public override void ApplyValue()
		{
			if (this._initialValue != this.Selector.SelectedIndex)
			{
				this._initialValue = this.Selector.SelectedIndex;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001EC20 File Offset: 0x0001CE20
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001EC2F File Offset: 0x0001CE2F
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> Selector
		{
			get
			{
				SelectorVM<SelectorItemVM> selector = this._selector;
				return this._selector;
			}
			set
			{
				if (value != this._selector)
				{
					this._selector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "Selector");
				}
			}
		}

		// Token: 0x040003C2 RID: 962
		private int _initialValue;

		// Token: 0x040003C3 RID: 963
		private ISelectionOptionData _selectionOptionData;

		// Token: 0x040003C4 RID: 964
		public SelectorVM<SelectorItemVM> _selector;
	}
}
