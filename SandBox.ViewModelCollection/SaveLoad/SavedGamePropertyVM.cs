using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.SaveLoad
{
	// Token: 0x02000011 RID: 17
	public class SavedGamePropertyVM : ViewModel
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00007A4E File Offset: 0x00005C4E
		public SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty type, TextObject value, TextObject hint)
		{
			this.PropertyType = type.ToString();
			this._valueText = value;
			this.Hint = new HintViewModel(hint, null);
			this.RefreshValues();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007A8E File Offset: 0x00005C8E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Value = this._valueText.ToString();
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007AA7 File Offset: 0x00005CA7
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00007AAF File Offset: 0x00005CAF
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007ACD File Offset: 0x00005CCD
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00007AD5 File Offset: 0x00005CD5
		[DataSourceProperty]
		public string PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (value != this._propertyType)
				{
					this._propertyType = value;
					base.OnPropertyChangedWithValue<string>(value, "PropertyType");
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007AF8 File Offset: 0x00005CF8
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00007B00 File Offset: 0x00005D00
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

		// Token: 0x04000084 RID: 132
		private TextObject _valueText = TextObject.Empty;

		// Token: 0x04000085 RID: 133
		private HintViewModel _hint;

		// Token: 0x04000086 RID: 134
		private string _propertyType;

		// Token: 0x04000087 RID: 135
		private string _value;

		// Token: 0x02000059 RID: 89
		public enum SavedGameProperty
		{
			// Token: 0x040002BD RID: 701
			None = -1,
			// Token: 0x040002BE RID: 702
			Health,
			// Token: 0x040002BF RID: 703
			Gold,
			// Token: 0x040002C0 RID: 704
			Influence,
			// Token: 0x040002C1 RID: 705
			PartySize,
			// Token: 0x040002C2 RID: 706
			Food,
			// Token: 0x040002C3 RID: 707
			Fiefs
		}
	}
}
