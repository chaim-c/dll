using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x020000FE RID: 254
	public class ClanCardSelectionPopupItemPropertyVM : ViewModel
	{
		// Token: 0x0600181C RID: 6172 RVA: 0x00059087 File Offset: 0x00057287
		public ClanCardSelectionPopupItemPropertyVM(in ClanCardSelectionItemPropertyInfo info)
		{
			this._titleText = info.Title;
			this._valueText = info.Value;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000590A8 File Offset: 0x000572A8
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject titleText = this._titleText;
			this.Title = (((titleText != null) ? titleText.ToString() : null) ?? string.Empty);
			TextObject valueText = this._valueText;
			this.Value = (((valueText != null) ? valueText.ToString() : null) ?? string.Empty);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000590FD File Offset: 0x000572FD
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x00059105 File Offset: 0x00057305
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x00059128 File Offset: 0x00057328
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x00059130 File Offset: 0x00057330
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

		// Token: 0x04000B41 RID: 2881
		private readonly TextObject _titleText;

		// Token: 0x04000B42 RID: 2882
		private readonly TextObject _valueText;

		// Token: 0x04000B43 RID: 2883
		private string _title;

		// Token: 0x04000B44 RID: 2884
		private string _value;
	}
}
