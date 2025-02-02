using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000B2 RID: 178
	public class EncyclopediaLinkVM : ViewModel
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x000455F0 File Offset: 0x000437F0
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x000455F8 File Offset: 0x000437F8
		[DataSourceProperty]
		public string ActiveLink
		{
			get
			{
				return this._activeLink;
			}
			set
			{
				if (this._activeLink != value)
				{
					this._activeLink = value;
					base.OnPropertyChangedWithValue<string>(value, "ActiveLink");
				}
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0004561B File Offset: 0x0004381B
		public void ExecuteActiveLink()
		{
			if (!string.IsNullOrEmpty(this.ActiveLink))
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.ActiveLink);
			}
		}

		// Token: 0x04000824 RID: 2084
		private string _activeLink;
	}
}
