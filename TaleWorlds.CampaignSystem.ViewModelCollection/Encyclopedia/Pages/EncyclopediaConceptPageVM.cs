using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000B6 RID: 182
	[EncyclopediaViewModel(typeof(Concept))]
	public class EncyclopediaConceptPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x060011FB RID: 4603 RVA: 0x00046F80 File Offset: 0x00045180
		public EncyclopediaConceptPageVM(EncyclopediaPageArgs args) : base(args)
		{
			this._concept = (base.Obj as Concept);
			Concept.SetConceptTextLinks();
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._concept);
			this.RefreshValues();
			this.Refresh();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00046FD6 File Offset: 0x000451D6
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = this._concept.Title.ToString();
			this.DescriptionText = this._concept.Description.ToString();
			base.UpdateBookmarkHintText();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00047010 File Offset: 0x00045210
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			base.IsLoadingOver = true;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00047020 File Offset: 0x00045220
		public override string GetName()
		{
			return this._concept.Title.ToString();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00047032 File Offset: 0x00045232
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00047044 File Offset: 0x00045244
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Concept", GameTexts.FindText("str_encyclopedia_concepts", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000470AC File Offset: 0x000452AC
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._concept);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._concept);
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000470FC File Offset: 0x000452FC
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x00047104 File Offset: 0x00045304
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00047127 File Offset: 0x00045327
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0004712F File Offset: 0x0004532F
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x0400085C RID: 2140
		private Concept _concept;

		// Token: 0x0400085D RID: 2141
		private string _titleText;

		// Token: 0x0400085E RID: 2142
		private string _descriptionText;
	}
}
