using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x02000025 RID: 37
	public class StringPairItemWithActionVM : ViewModel
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00005931 File Offset: 0x00003B31
		public StringPairItemWithActionVM(Action<object> onExecute, string definition, string value, object identifier)
		{
			this._onExecute = onExecute;
			this.Identifier = identifier;
			this.Definition = definition;
			this.Value = value;
			this.Hint = new HintViewModel();
			this.IsEnabled = true;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005968 File Offset: 0x00003B68
		public void ExecuteAction()
		{
			this._onExecute(this.Identifier);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000597B File Offset: 0x00003B7B
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00005983 File Offset: 0x00003B83
		[DataSourceProperty]
		public string Definition
		{
			get
			{
				return this._definition;
			}
			set
			{
				if (value != this._definition)
				{
					this._definition = value;
					base.OnPropertyChangedWithValue<string>(value, "Definition");
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000059A6 File Offset: 0x00003BA6
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000059AE File Offset: 0x00003BAE
		[DataSourceProperty]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this._value)
				{
					this._value = value;
					base.OnPropertyChangedWithValue<string>(value, "Value");
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000059D1 File Offset: 0x00003BD1
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000059D9 File Offset: 0x00003BD9
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000059F7 File Offset: 0x00003BF7
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000059FF File Offset: 0x00003BFF
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

		// Token: 0x040000B0 RID: 176
		public object Identifier;

		// Token: 0x040000B1 RID: 177
		protected Action<object> _onExecute;

		// Token: 0x040000B2 RID: 178
		private string _definition;

		// Token: 0x040000B3 RID: 179
		private string _value;

		// Token: 0x040000B4 RID: 180
		private HintViewModel _hint;

		// Token: 0x040000B5 RID: 181
		private bool _isEnabled;
	}
}
