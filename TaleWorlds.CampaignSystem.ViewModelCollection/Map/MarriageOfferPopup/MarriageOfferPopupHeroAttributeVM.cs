using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MarriageOfferPopup
{
	// Token: 0x02000034 RID: 52
	public class MarriageOfferPopupHeroAttributeVM : ViewModel
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0001AA46 File Offset: 0x00018C46
		public MarriageOfferPopupHeroAttributeVM(Hero hero, CharacterAttribute attribute)
		{
			this._hero = hero;
			this._attribute = attribute;
			this.FillSkillsList();
			this.RefreshValues();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001AA68 File Offset: 0x00018C68
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject textObject = GameTexts.FindText("str_STR1_space_STR2", null);
			textObject.SetTextVariable("STR1", this._attribute.Name);
			TextObject textObject2 = GameTexts.FindText("str_STR_in_parentheses", null);
			textObject2.SetTextVariable("STR", this._hero.GetAttributeValue(this._attribute));
			textObject.SetTextVariable("STR2", textObject2);
			this._attributeText = textObject.ToString();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		private void FillSkillsList()
		{
			this._attributeSkills = new MBBindingList<EncyclopediaSkillVM>();
			foreach (SkillObject skill in this._attribute.Skills)
			{
				this._attributeSkills.Add(new EncyclopediaSkillVM(skill, this._hero.GetSkillValue(skill)));
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001AB5C File Offset: 0x00018D5C
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001AB64 File Offset: 0x00018D64
		[DataSourceProperty]
		public string AttributeText
		{
			get
			{
				return this._attributeText;
			}
			set
			{
				if (value != this._attributeText)
				{
					this._attributeText = value;
					base.OnPropertyChangedWithValue<string>(value, "AttributeText");
				}
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001AB87 File Offset: 0x00018D87
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001AB8F File Offset: 0x00018D8F
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSkillVM> AttributeSkills
		{
			get
			{
				return this._attributeSkills;
			}
			set
			{
				if (value != this._attributeSkills)
				{
					this._attributeSkills = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSkillVM>>(value, "AttributeSkills");
				}
			}
		}

		// Token: 0x04000227 RID: 551
		private readonly Hero _hero;

		// Token: 0x04000228 RID: 552
		private readonly CharacterAttribute _attribute;

		// Token: 0x04000229 RID: 553
		private string _attributeText;

		// Token: 0x0400022A RID: 554
		private MBBindingList<EncyclopediaSkillVM> _attributeSkills;
	}
}
