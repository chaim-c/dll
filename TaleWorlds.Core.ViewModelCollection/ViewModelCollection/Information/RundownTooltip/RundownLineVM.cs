using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information.RundownTooltip
{
	// Token: 0x0200001C RID: 28
	public class RundownLineVM : ViewModel
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000051DF File Offset: 0x000033DF
		public RundownLineVM(string name, float value)
		{
			this.Name = name;
			this.ValueAsString = string.Format("{0:0.##}", value);
			this.Value = value;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000520B File Offset: 0x0000340B
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00005213 File Offset: 0x00003413
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005236 File Offset: 0x00003436
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000523E File Offset: 0x0000343E
		[DataSourceProperty]
		public string ValueAsString
		{
			get
			{
				return this._valueAsString;
			}
			set
			{
				if (value != this._valueAsString)
				{
					this._valueAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "ValueAsString");
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005261 File Offset: 0x00003461
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005269 File Offset: 0x00003469
		[DataSourceProperty]
		public float Value
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
					base.OnPropertyChangedWithValue(value, "Value");
				}
			}
		}

		// Token: 0x04000091 RID: 145
		private string _name;

		// Token: 0x04000092 RID: 146
		private string _valueAsString;

		// Token: 0x04000093 RID: 147
		private float _value;
	}
}
