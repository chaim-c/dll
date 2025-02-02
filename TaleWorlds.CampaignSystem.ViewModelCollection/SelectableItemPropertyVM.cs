using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200001B RID: 27
	public class SelectableItemPropertyVM : ViewModel
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000B4EE File Offset: 0x000096EE
		public SelectableItemPropertyVM(string name, string value, bool isWarning = false, BasicTooltipViewModel hint = null)
		{
			this.Name = name;
			this.Value = value;
			this.Hint = hint;
			this.Type = 0;
			this.IsWarning = isWarning;
			this.RefreshValues();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000B520 File Offset: 0x00009720
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ColonText = GameTexts.FindText("str_colon", null).ToString();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B53E File Offset: 0x0000973E
		private void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000B550 File Offset: 0x00009750
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000B558 File Offset: 0x00009758
		[DataSourceProperty]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000B576 File Offset: 0x00009776
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000B57E File Offset: 0x0000977E
		[DataSourceProperty]
		public bool IsWarning
		{
			get
			{
				return this._isWarning;
			}
			set
			{
				if (value != this._isWarning)
				{
					this._isWarning = value;
					base.OnPropertyChangedWithValue(value, "IsWarning");
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000B59C File Offset: 0x0000979C
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000B5A4 File Offset: 0x000097A4
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000B5C7 File Offset: 0x000097C7
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000B5CF File Offset: 0x000097CF
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000B5F2 File Offset: 0x000097F2
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000B5FA File Offset: 0x000097FA
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000B618 File Offset: 0x00009818
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000B620 File Offset: 0x00009820
		[DataSourceProperty]
		public string ColonText
		{
			get
			{
				return this._colonText;
			}
			set
			{
				if (value != this._colonText)
				{
					this._colonText = value;
					base.OnPropertyChangedWithValue<string>(value, "ColonText");
				}
			}
		}

		// Token: 0x040000B9 RID: 185
		private int _type;

		// Token: 0x040000BA RID: 186
		private bool _isWarning;

		// Token: 0x040000BB RID: 187
		private string _name;

		// Token: 0x040000BC RID: 188
		private string _value;

		// Token: 0x040000BD RID: 189
		private BasicTooltipViewModel _hint;

		// Token: 0x040000BE RID: 190
		private string _colonText;

		// Token: 0x02000159 RID: 345
		public enum PropertyType
		{
			// Token: 0x04000F3B RID: 3899
			None,
			// Token: 0x04000F3C RID: 3900
			Wall,
			// Token: 0x04000F3D RID: 3901
			Garrison,
			// Token: 0x04000F3E RID: 3902
			Militia,
			// Token: 0x04000F3F RID: 3903
			Prosperity,
			// Token: 0x04000F40 RID: 3904
			Food,
			// Token: 0x04000F41 RID: 3905
			Loyalty,
			// Token: 0x04000F42 RID: 3906
			Security
		}
	}
}
