using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x02000022 RID: 34
	public class StringItemWithEnabledAndHintVM : ViewModel
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000575D File Offset: 0x0000395D
		public StringItemWithEnabledAndHintVM(Action<object> onExecute, string item, bool enabled, object identifier, TextObject hintText = null)
		{
			this._onExecute = onExecute;
			this.Identifier = identifier;
			this.ActionText = item;
			this.IsEnabled = enabled;
			this.Hint = new HintViewModel(hintText ?? TextObject.Empty, null);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005799 File Offset: 0x00003999
		public void ExecuteAction()
		{
			if (this.IsEnabled)
			{
				this._onExecute(this.Identifier);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000057B4 File Offset: 0x000039B4
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000057BC File Offset: 0x000039BC
		[DataSourceProperty]
		public string ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (value != this._actionText)
				{
					this._actionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionText");
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000057DF File Offset: 0x000039DF
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000057E7 File Offset: 0x000039E7
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005805 File Offset: 0x00003A05
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000580D File Offset: 0x00003A0D
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

		// Token: 0x040000A6 RID: 166
		public object Identifier;

		// Token: 0x040000A7 RID: 167
		protected Action<object> _onExecute;

		// Token: 0x040000A8 RID: 168
		private HintViewModel _hint;

		// Token: 0x040000A9 RID: 169
		private string _actionText;

		// Token: 0x040000AA RID: 170
		private bool _isEnabled;
	}
}
