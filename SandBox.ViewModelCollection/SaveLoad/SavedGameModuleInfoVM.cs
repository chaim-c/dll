using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.SaveLoad
{
	// Token: 0x02000010 RID: 16
	public class SavedGameModuleInfoVM : ViewModel
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000079B0 File Offset: 0x00005BB0
		public SavedGameModuleInfoVM(string definition, string seperator, string value)
		{
			this.Definition = definition;
			this.Seperator = seperator;
			this.Value = value;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000079CD File Offset: 0x00005BCD
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000079D5 File Offset: 0x00005BD5
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000079F8 File Offset: 0x00005BF8
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007A00 File Offset: 0x00005C00
		[DataSourceProperty]
		public string Seperator
		{
			get
			{
				return this._seperator;
			}
			set
			{
				if (value != this._seperator)
				{
					this._seperator = value;
					base.OnPropertyChangedWithValue<string>(value, "Seperator");
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007A23 File Offset: 0x00005C23
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00007A2B File Offset: 0x00005C2B
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

		// Token: 0x04000081 RID: 129
		private string _definition;

		// Token: 0x04000082 RID: 130
		private string _seperator;

		// Token: 0x04000083 RID: 131
		private string _value;
	}
}
