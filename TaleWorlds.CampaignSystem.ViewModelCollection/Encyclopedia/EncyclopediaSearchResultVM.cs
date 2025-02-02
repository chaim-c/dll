using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000B4 RID: 180
	public class EncyclopediaSearchResultVM : ViewModel
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00046126 File Offset: 0x00044326
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0004612E File Offset: 0x0004432E
		public string OrgNameText { get; private set; }

		// Token: 0x060011BA RID: 4538 RVA: 0x00046138 File Offset: 0x00044338
		public EncyclopediaSearchResultVM(EncyclopediaListItem source, string searchedText, int matchStartIndex)
		{
			this.MatchStartIndex = matchStartIndex;
			this.LinkId = source.Id;
			this.PageType = source.TypeName;
			this.OrgNameText = source.Name;
			this._nameText = source.Name;
			this.UpdateSearchedText(searchedText);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00046194 File Offset: 0x00044394
		public void UpdateSearchedText(string searchedText)
		{
			this._searchedText = searchedText;
			string text = this.OrgNameText.Substring(this.OrgNameText.IndexOf(this._searchedText, StringComparison.OrdinalIgnoreCase), this._searchedText.Length);
			if (!string.IsNullOrEmpty(text))
			{
				this.NameText = this.OrgNameText.Replace(text, "<a>" + text + "</a>");
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000461FB File Offset: 0x000443FB
		public void Execute()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.PageType, this.LinkId);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00046218 File Offset: 0x00044418
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00046220 File Offset: 0x00044420
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (this._nameText != value)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x04000839 RID: 2105
		private string _searchedText;

		// Token: 0x0400083B RID: 2107
		public readonly int MatchStartIndex;

		// Token: 0x0400083C RID: 2108
		public string LinkId = "";

		// Token: 0x0400083D RID: 2109
		public string PageType;

		// Token: 0x0400083E RID: 2110
		public string _nameText;
	}
}
