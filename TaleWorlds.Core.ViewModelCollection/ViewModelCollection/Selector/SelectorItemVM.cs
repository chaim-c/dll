using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Selector
{
	// Token: 0x02000011 RID: 17
	public class SelectorItemVM : ViewModel
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000352B File Offset: 0x0000172B
		public SelectorItemVM(TextObject s)
		{
			this._s = s;
			this.RefreshValues();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003547 File Offset: 0x00001747
		public SelectorItemVM(string s)
		{
			this._stringItem = s;
			this.RefreshValues();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003563 File Offset: 0x00001763
		public SelectorItemVM(TextObject s, TextObject hint)
		{
			this._s = s;
			this._hintObj = hint;
			this.RefreshValues();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003586 File Offset: 0x00001786
		public SelectorItemVM(string s, TextObject hint)
		{
			this._stringItem = s;
			this._hintObj = hint;
			this.RefreshValues();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000035AC File Offset: 0x000017AC
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._s != null)
			{
				this._stringItem = this._s.ToString();
			}
			if (this._hintObj != null)
			{
				if (this._hint == null)
				{
					this._hint = new HintViewModel(this._hintObj, null);
					return;
				}
				this._hint.HintText = this._hintObj;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000360C File Offset: 0x0000180C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003614 File Offset: 0x00001814
		[DataSourceProperty]
		public string StringItem
		{
			get
			{
				return this._stringItem;
			}
			set
			{
				if (value != this._stringItem)
				{
					this._stringItem = value;
					base.OnPropertyChangedWithValue<string>(value, "StringItem");
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003637 File Offset: 0x00001837
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000363F File Offset: 0x0000183F
		[DataSourceProperty]
		public bool CanBeSelected
		{
			get
			{
				return this._canBeSelected;
			}
			set
			{
				if (value != this._canBeSelected)
				{
					this._canBeSelected = value;
					base.OnPropertyChangedWithValue(value, "CanBeSelected");
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000365D File Offset: 0x0000185D
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003665 File Offset: 0x00001865
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003683 File Offset: 0x00001883
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000368B File Offset: 0x0000188B
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

		// Token: 0x04000052 RID: 82
		private TextObject _s;

		// Token: 0x04000053 RID: 83
		private TextObject _hintObj;

		// Token: 0x04000054 RID: 84
		private string _stringItem;

		// Token: 0x04000055 RID: 85
		private HintViewModel _hint;

		// Token: 0x04000056 RID: 86
		private bool _canBeSelected = true;

		// Token: 0x04000057 RID: 87
		private bool _isSelected;
	}
}
