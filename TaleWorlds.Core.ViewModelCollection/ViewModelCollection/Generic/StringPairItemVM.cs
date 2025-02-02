using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x02000024 RID: 36
	public class StringPairItemVM : ViewModel
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00005898 File Offset: 0x00003A98
		public StringPairItemVM(string definition, string value, BasicTooltipViewModel hint = null)
		{
			this.Definition = definition;
			this.Value = value;
			this.Hint = hint;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000058B5 File Offset: 0x00003AB5
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000058BD File Offset: 0x00003ABD
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000058E0 File Offset: 0x00003AE0
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000058E8 File Offset: 0x00003AE8
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000590B File Offset: 0x00003B0B
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00005913 File Offset: 0x00003B13
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
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
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x040000AD RID: 173
		private string _definition;

		// Token: 0x040000AE RID: 174
		private string _value;

		// Token: 0x040000AF RID: 175
		private BasicTooltipViewModel _hint;
	}
}
