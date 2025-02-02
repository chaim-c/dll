using System;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x0200012F RID: 303
	public class CharacterCreationCultureVM : ViewModel
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00069DF2 File Offset: 0x00067FF2
		public CultureObject Culture { get; }

		// Token: 0x06001D8B RID: 7563 RVA: 0x00069DFC File Offset: 0x00067FFC
		public CharacterCreationCultureVM(CultureObject culture, Action<CharacterCreationCultureVM> onSelection)
		{
			this._onSelection = onSelection;
			this.Culture = culture;
			MBTextManager.SetTextVariable("FOCUS_VALUE", CharacterCreationContentBase.Instance.FocusToAddByCulture);
			MBTextManager.SetTextVariable("EXP_VALUE", CharacterCreationContentBase.Instance.SkillLevelToAddByCulture);
			this.DescriptionText = GameTexts.FindText("str_culture_description", this.Culture.StringId).ToString();
			this.ShortenedNameText = GameTexts.FindText("str_culture_rich_name", this.Culture.StringId).ToString();
			this.NameText = GameTexts.FindText("str_culture_rich_name", this.Culture.StringId).ToString();
			this.CultureID = (((culture != null) ? culture.StringId : null) ?? "");
			this.CultureColor1 = Color.FromUint((culture != null) ? culture.Color : Color.White.ToUnsignedInteger());
			this.Feats = new MBBindingList<CharacterCreationCultureFeatVM>();
			foreach (FeatObject featObject in this.Culture.GetCulturalFeats((FeatObject x) => x.IsPositive))
			{
				this.Feats.Add(new CharacterCreationCultureFeatVM(true, featObject.Description.ToString()));
			}
			foreach (FeatObject featObject2 in this.Culture.GetCulturalFeats((FeatObject x) => !x.IsPositive))
			{
				this.Feats.Add(new CharacterCreationCultureFeatVM(false, featObject2.Description.ToString()));
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00069FE8 File Offset: 0x000681E8
		public void ExecuteSelectCulture()
		{
			this._onSelection(this);
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00069FF6 File Offset: 0x000681F6
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x00069FFE File Offset: 0x000681FE
		[DataSourceProperty]
		public string CultureID
		{
			get
			{
				return this._cultureID;
			}
			set
			{
				if (value != this._cultureID)
				{
					this._cultureID = value;
					base.OnPropertyChangedWithValue<string>(value, "CultureID");
				}
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x0006A021 File Offset: 0x00068221
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x0006A029 File Offset: 0x00068229
		[DataSourceProperty]
		public Color CultureColor1
		{
			get
			{
				return this._cultureColor1;
			}
			set
			{
				if (value != this._cultureColor1)
				{
					this._cultureColor1 = value;
					base.OnPropertyChangedWithValue(value, "CultureColor1");
				}
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0006A04C File Offset: 0x0006824C
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0006A054 File Offset: 0x00068254
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0006A077 File Offset: 0x00068277
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0006A07F File Offset: 0x0006827F
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0006A0A2 File Offset: 0x000682A2
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0006A0AA File Offset: 0x000682AA
		[DataSourceProperty]
		public string ShortenedNameText
		{
			get
			{
				return this._shortenedNameText;
			}
			set
			{
				if (value != this._shortenedNameText)
				{
					this._shortenedNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "ShortenedNameText");
				}
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0006A0CD File Offset: 0x000682CD
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0006A0D5 File Offset: 0x000682D5
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0006A0F3 File Offset: 0x000682F3
		// (set) Token: 0x06001D9A RID: 7578 RVA: 0x0006A0FB File Offset: 0x000682FB
		[DataSourceProperty]
		public MBBindingList<CharacterCreationCultureFeatVM> Feats
		{
			get
			{
				return this._feats;
			}
			set
			{
				if (value != this._feats)
				{
					this._feats = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationCultureFeatVM>>(value, "Feats");
				}
			}
		}

		// Token: 0x04000DED RID: 3565
		private readonly Action<CharacterCreationCultureVM> _onSelection;

		// Token: 0x04000DEE RID: 3566
		private string _descriptionText = "";

		// Token: 0x04000DEF RID: 3567
		private string _nameText;

		// Token: 0x04000DF0 RID: 3568
		private string _shortenedNameText;

		// Token: 0x04000DF1 RID: 3569
		private bool _isSelected;

		// Token: 0x04000DF2 RID: 3570
		private string _cultureID;

		// Token: 0x04000DF3 RID: 3571
		private Color _cultureColor1;

		// Token: 0x04000DF4 RID: 3572
		private MBBindingList<CharacterCreationCultureFeatVM> _feats;
	}
}
