using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x020001D7 RID: 471
	public abstract class CharacterCreationContentBase
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0007F622 File Offset: 0x0007D822
		public static CharacterCreationContentBase Instance
		{
			get
			{
				CharacterCreationState characterCreationState = GameStateManager.Current.ActiveState as CharacterCreationState;
				if (characterCreationState == null)
				{
					return null;
				}
				return characterCreationState.CurrentCharacterCreationContent;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0007F63E File Offset: 0x0007D83E
		protected virtual int ChildhoodAge
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0007F641 File Offset: 0x0007D841
		protected virtual int EducationAge
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0007F645 File Offset: 0x0007D845
		protected virtual int YouthAge
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0007F649 File Offset: 0x0007D849
		protected virtual int AccomplishmentAge
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0007F64D File Offset: 0x0007D84D
		protected virtual int FocusToAdd
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0007F650 File Offset: 0x0007D850
		protected virtual int SkillLevelToAdd
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0007F654 File Offset: 0x0007D854
		protected virtual int AttributeLevelToAdd
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0007F657 File Offset: 0x0007D857
		public virtual int FocusToAddByCulture
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0007F65A File Offset: 0x0007D85A
		public virtual int SkillLevelToAddByCulture
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0007F65E File Offset: 0x0007D85E
		// (set) Token: 0x06001C46 RID: 7238 RVA: 0x0007F666 File Offset: 0x0007D866
		protected int SelectedParentType { get; set; } = 1;

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0007F66F File Offset: 0x0007D86F
		// (set) Token: 0x06001C48 RID: 7240 RVA: 0x0007F677 File Offset: 0x0007D877
		protected int SelectedTitleType { get; set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001C49 RID: 7241
		public abstract TextObject ReviewPageDescription { get; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0007F680 File Offset: 0x0007D880
		// (set) Token: 0x06001C4B RID: 7243 RVA: 0x0007F688 File Offset: 0x0007D888
		protected FaceGenChar MotherFacegenCharacter { get; set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0007F691 File Offset: 0x0007D891
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x0007F699 File Offset: 0x0007D899
		protected FaceGenChar FatherFacegenCharacter { get; set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0007F6A2 File Offset: 0x0007D8A2
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x0007F6AA File Offset: 0x0007D8AA
		protected BodyProperties PlayerBodyProperties { get; set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0007F6B3 File Offset: 0x0007D8B3
		// (set) Token: 0x06001C51 RID: 7249 RVA: 0x0007F6BB File Offset: 0x0007D8BB
		protected Equipment PlayerStartEquipment { get; set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0007F6C4 File Offset: 0x0007D8C4
		// (set) Token: 0x06001C53 RID: 7251 RVA: 0x0007F6CC File Offset: 0x0007D8CC
		protected Equipment PlayerCivilianEquipment { get; set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001C54 RID: 7252
		public abstract IEnumerable<Type> CharacterCreationStages { get; }

		// Token: 0x06001C55 RID: 7253 RVA: 0x0007F6D5 File Offset: 0x0007D8D5
		public void Initialize(CharacterCreation characterCreation)
		{
			this.OnInitialized(characterCreation);
			this.SetMainHeroInitialStats();
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0007F6E4 File Offset: 0x0007D8E4
		protected virtual void OnInitialized(CharacterCreation characterCreation)
		{
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0007F6E8 File Offset: 0x0007D8E8
		public void ApplySkillAndAttributeEffects(List<SkillObject> skills, int focusToAdd, int skillLevelToAdd, CharacterAttribute attribute, int attributeLevelToAdd, List<TraitObject> traits = null, int traitLevelToAdd = 0, int renownToAdd = 0, int goldToAdd = 0, int unspentFocusPoints = 0, int unspentAttributePoints = 0)
		{
			foreach (SkillObject skill in skills)
			{
				Hero.MainHero.HeroDeveloper.AddFocus(skill, focusToAdd, false);
				if (Hero.MainHero.GetSkillValue(skill) == 1)
				{
					Hero.MainHero.HeroDeveloper.ChangeSkillLevel(skill, skillLevelToAdd - 1, false);
				}
				else
				{
					Hero.MainHero.HeroDeveloper.ChangeSkillLevel(skill, skillLevelToAdd, false);
				}
			}
			Hero.MainHero.HeroDeveloper.UnspentFocusPoints += unspentFocusPoints;
			Hero.MainHero.HeroDeveloper.UnspentAttributePoints += unspentAttributePoints;
			if (attribute != null)
			{
				Hero.MainHero.HeroDeveloper.AddAttribute(attribute, attributeLevelToAdd, false);
			}
			if (traits != null && traitLevelToAdd > 0 && traits.Count > 0)
			{
				foreach (TraitObject trait in traits)
				{
					Hero.MainHero.SetTraitLevel(trait, Hero.MainHero.GetTraitLevel(trait) + traitLevelToAdd);
				}
			}
			if (renownToAdd > 0)
			{
				GainRenownAction.Apply(Hero.MainHero, (float)renownToAdd, true);
			}
			if (goldToAdd > 0)
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, goldToAdd, true);
			}
			Hero.MainHero.HeroDeveloper.SetInitialLevel(1);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0007F858 File Offset: 0x0007DA58
		public void SetPlayerBanner(Banner banner)
		{
			this._banner = banner;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0007F861 File Offset: 0x0007DA61
		public Banner GetCurrentPlayerBanner()
		{
			return this._banner;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0007F869 File Offset: 0x0007DA69
		public void SetSelectedCulture(CultureObject culture, CharacterCreation characterCreation)
		{
			this._culture = culture;
			characterCreation.ResetMenuOptions();
			this.OnCultureSelected();
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0007F880 File Offset: 0x0007DA80
		public void ApplyCulture(CharacterCreation characterCreation)
		{
			CharacterObject.PlayerCharacter.Culture = CharacterCreationContentBase.Instance._culture;
			Clan.PlayerClan.Culture = CharacterCreationContentBase.Instance._culture;
			Clan.PlayerClan.UpdateHomeSettlement(null);
			Hero.MainHero.BornSettlement = Clan.PlayerClan.HomeSettlement;
			this.OnApplyCulture();
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0007F8DA File Offset: 0x0007DADA
		public IEnumerable<CultureObject> GetCultures()
		{
			foreach (CultureObject cultureObject in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
			{
				if (cultureObject.IsMainCulture)
				{
					yield return cultureObject;
				}
			}
			List<CultureObject>.Enumerator enumerator = default(List<CultureObject>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0007F8E3 File Offset: 0x0007DAE3
		public CultureObject GetSelectedCulture()
		{
			return this._culture;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0007F8EB File Offset: 0x0007DAEB
		public virtual int GetSelectedParentType()
		{
			return 0;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0007F8EE File Offset: 0x0007DAEE
		protected virtual void OnApplyCulture()
		{
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0007F8F0 File Offset: 0x0007DAF0
		protected virtual void OnCultureSelected()
		{
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0007F8F2 File Offset: 0x0007DAF2
		public virtual void OnCharacterCreationFinalized()
		{
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007F8F4 File Offset: 0x0007DAF4
		private void SetMainHeroInitialStats()
		{
			Hero.MainHero.HeroDeveloper.ClearHero();
			Hero.MainHero.HitPoints = 100;
			foreach (SkillObject skill in Skills.All)
			{
				Hero.MainHero.HeroDeveloper.InitializeSkillXp(skill);
			}
			foreach (CharacterAttribute attribute in Attributes.All)
			{
				Hero.MainHero.HeroDeveloper.AddAttribute(attribute, 2, false);
			}
		}

		// Token: 0x040008E4 RID: 2276
		private CultureObject _culture;

		// Token: 0x040008E5 RID: 2277
		private Banner _banner;
	}
}
