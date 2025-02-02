using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x02000023 RID: 35
	public class StringItemWithHintVM : ViewModel
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000582B File Offset: 0x00003A2B
		public StringItemWithHintVM(string text, TextObject hint)
		{
			this.Text = text;
			this.Hint = new HintViewModel(hint, null);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005847 File Offset: 0x00003A47
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000584F File Offset: 0x00003A4F
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005872 File Offset: 0x00003A72
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000587A File Offset: 0x00003A7A
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

		// Token: 0x040000AB RID: 171
		private string _text;

		// Token: 0x040000AC RID: 172
		private HintViewModel _hint;
	}
}
