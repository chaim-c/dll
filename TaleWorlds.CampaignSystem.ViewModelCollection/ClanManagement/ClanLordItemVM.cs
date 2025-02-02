using System;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000108 RID: 264
	public class ClanLordItemVM : ViewModel
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x0005A0C4 File Offset: 0x000582C4
		public ClanLordItemVM(Hero hero, ITeleportationCampaignBehavior teleportationBehavior, Action<Hero> showHeroOnMap, Action<ClanLordItemVM> onCharacterSelect, Action onRecall, Action onTalk)
		{
			this._hero = hero;
			this._onCharacterSelect = onCharacterSelect;
			this._onRecall = onRecall;
			this._onTalk = onTalk;
			this._showHeroOnMap = showHeroOnMap;
			this._teleportationBehavior = teleportationBehavior;
			CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false);
			this.Visual = new ImageIdentifierVM(characterCode);
			this.Skills = new MBBindingList<EncyclopediaSkillVM>();
			this.Traits = new MBBindingList<EncyclopediaTraitItemVM>();
			this.IsFamilyMember = Hero.MainHero.Clan.Lords.Contains(this._hero);
			this.Banner_9 = new ImageIdentifierVM(BannerCode.CreateFrom(hero.ClanBanner), true);
			this.RefreshValues();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0005A1B4 File Offset: 0x000583B4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._hero.Name.ToString();
			StringHelpers.SetCharacterProperties("NPC", this._hero.CharacterObject, null, false);
			this.CurrentActionText = ((this._hero != Hero.MainHero) ? CampaignUIHelper.GetHeroBehaviorText(this._hero, this._teleportationBehavior) : "");
			this.LocationText = this.CurrentActionText;
			this.PregnantHint = new HintViewModel(GameTexts.FindText("str_pregnant", null), null);
			this.UpdateProperties();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0005A249 File Offset: 0x00058449
		public void ExecuteLocationLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0005A25C File Offset: 0x0005845C
		public void UpdateProperties()
		{
			this.RelationToMainHeroText = "";
			this.GovernorOfText = "";
			this.Skills.Clear();
			this.Traits.Clear();
			this.IsMainHero = (this._hero == Hero.MainHero);
			this.IsPregnant = this._hero.IsPregnant;
			foreach (SkillObject skill in (from s in TaleWorlds.CampaignSystem.Extensions.Skills.All
			group s by s.CharacterAttribute.Id).SelectMany((IGrouping<MBGUID, SkillObject> s) => s).ToList<SkillObject>())
			{
				this.Skills.Add(new EncyclopediaSkillVM(skill, this._hero.GetSkillValue(skill)));
			}
			foreach (TraitObject traitObject in CampaignUIHelper.GetHeroTraits())
			{
				if (this._hero.GetTraitLevel(traitObject) != 0)
				{
					this.Traits.Add(new EncyclopediaTraitItemVM(traitObject, this._hero));
				}
			}
			this.IsChild = (FaceGen.GetMaturityTypeWithAge(this._hero.Age) <= BodyMeshMaturityType.Child);
			if (this._hero != Hero.MainHero)
			{
				this.RelationToMainHeroText = CampaignUIHelper.GetHeroRelationToHeroText(this._hero, Hero.MainHero, true).ToString();
			}
			if (this._hero.GovernorOf != null)
			{
				GameTexts.SetVariable("SETTLEMENT_NAME", this._hero.GovernorOf.Owner.Settlement.EncyclopediaLinkWithName);
				this.GovernorOfText = GameTexts.FindText("str_governor_of_label", null).ToString();
			}
			this.HeroModel = new HeroViewModel(CharacterViewModel.StanceTypes.None);
			this.HeroModel.FillFrom(this._hero, -1, false, false);
			this.Banner_9 = new ImageIdentifierVM(BannerCode.CreateFrom(this._hero.ClanBanner), true);
			this.CanShowLocationOfHero = ((this._hero.IsActive || (this._hero.IsPrisoner && this._hero.CurrentSettlement != null)) && this._hero.PartyBelongedTo != MobileParty.MainParty);
			this.ShowOnMapHint = new HintViewModel(this.CanShowLocationOfHero ? this._showLocationOfHeroOnMap : TextObject.Empty, null);
			TextObject empty = TextObject.Empty;
			bool flag = this._hero.PartyBelongedTo == MobileParty.MainParty;
			this.IsTalkVisible = (flag && !this.IsMainHero);
			this.IsTalkEnabled = (this.IsTalkVisible && CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out empty));
			bool flag2;
			bool flag3;
			IMapPoint mapPoint;
			this.IsTeleporting = this._teleportationBehavior.GetTargetOfTeleportingHero(this._hero, out flag2, out flag3, out mapPoint);
			TextObject empty2 = TextObject.Empty;
			this.IsRecallVisible = (!this.IsMainHero && !flag && !this.IsTeleporting);
			this.IsRecallEnabled = (this.IsRecallVisible && CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out empty2) && FactionHelper.IsMainClanMemberAvailableForRecall(this._hero, MobileParty.MainParty, out empty2));
			this.RecallHint = new HintViewModel(this.IsRecallEnabled ? this._recallHeroToMainPartyHintText : empty2, null);
			this.TalkHint = new HintViewModel(this.IsTalkEnabled ? this._talkToHeroHintText : empty, null);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0005A5E4 File Offset: 0x000587E4
		public void ExecuteLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._hero.EncyclopediaLink);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0005A600 File Offset: 0x00058800
		public void OnCharacterSelect()
		{
			this._onCharacterSelect(this);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0005A60E File Offset: 0x0005880E
		public virtual void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Hero), new object[]
			{
				this._hero,
				true
			});
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0005A637 File Offset: 0x00058837
		public virtual void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0005A63E File Offset: 0x0005883E
		public Hero GetHero()
		{
			return this._hero;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0005A648 File Offset: 0x00058848
		public void ExecuteRename()
		{
			InformationManager.ShowTextInquiry(new TextInquiryData(new TextObject("{=2lFwF07j}Change Name", null).ToString(), string.Empty, true, true, GameTexts.FindText("str_done", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action<string>(this.OnNamingHeroOver), null, false, new Func<string, Tuple<bool, string>>(CampaignUIHelper.IsStringApplicableForHeroName), "", ""), false, false);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0005A6BC File Offset: 0x000588BC
		private void OnNamingHeroOver(string suggestedName)
		{
			if (CampaignUIHelper.IsStringApplicableForHeroName(suggestedName).Item1)
			{
				TextObject textObject = GameTexts.FindText("str_generic_character_firstname", null);
				textObject.SetTextVariable("CHARACTER_FIRSTNAME", new TextObject(suggestedName, null));
				TextObject textObject2 = GameTexts.FindText("str_generic_character_name", null);
				textObject2.SetTextVariable("CHARACTER_NAME", new TextObject(suggestedName, null));
				textObject2.SetTextVariable("CHARACTER_GENDER", this._hero.IsFemale ? 1 : 0);
				textObject.SetTextVariable("CHARACTER_GENDER", this._hero.IsFemale ? 1 : 0);
				this._hero.SetName(textObject2, textObject);
				this.Name = suggestedName;
				MobileParty partyBelongedTo = this._hero.PartyBelongedTo;
				if (((partyBelongedTo != null) ? partyBelongedTo.Army : null) != null && this._hero.PartyBelongedTo.Army.LeaderParty.Owner == this._hero)
				{
					this._hero.PartyBelongedTo.Army.UpdateName();
					return;
				}
			}
			else
			{
				Debug.FailedAssert("Suggested name is not acceptable. This shouldn't happen", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanLordItemVM.cs", "OnNamingHeroOver", 190);
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0005A7CF File Offset: 0x000589CF
		public void ExecuteShowOnMap()
		{
			if (this._hero != null && this.CanShowLocationOfHero)
			{
				this._showHeroOnMap(this._hero);
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0005A7F2 File Offset: 0x000589F2
		public void ExecuteRecall()
		{
			Action onRecall = this._onRecall;
			if (onRecall == null)
			{
				return;
			}
			onRecall();
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005A804 File Offset: 0x00058A04
		public void ExecuteTalk()
		{
			Action onTalk = this._onTalk;
			if (onTalk == null)
			{
				return;
			}
			onTalk();
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0005A816 File Offset: 0x00058A16
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.HeroModel.OnFinalize();
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0005A829 File Offset: 0x00058A29
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x0005A831 File Offset: 0x00058A31
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSkillVM> Skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				if (value != this._skills)
				{
					this._skills = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSkillVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005A84F File Offset: 0x00058A4F
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x0005A857 File Offset: 0x00058A57
		[DataSourceProperty]
		public MBBindingList<EncyclopediaTraitItemVM> Traits
		{
			get
			{
				return this._traits;
			}
			set
			{
				if (value != this._traits)
				{
					this._traits = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaTraitItemVM>>(value, "Traits");
				}
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0005A875 File Offset: 0x00058A75
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x0005A87D File Offset: 0x00058A7D
		[DataSourceProperty]
		public HeroViewModel HeroModel
		{
			get
			{
				return this._heroModel;
			}
			set
			{
				if (value != this._heroModel)
				{
					this._heroModel = value;
					base.OnPropertyChangedWithValue<HeroViewModel>(value, "HeroModel");
				}
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0005A89B File Offset: 0x00058A9B
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0005A8A3 File Offset: 0x00058AA3
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

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005A8C1 File Offset: 0x00058AC1
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0005A8C9 File Offset: 0x00058AC9
		[DataSourceProperty]
		public bool IsChild
		{
			get
			{
				return this._isChild;
			}
			set
			{
				if (value != this._isChild)
				{
					this._isChild = value;
					base.OnPropertyChangedWithValue(value, "IsChild");
				}
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005A8E7 File Offset: 0x00058AE7
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x0005A8EF File Offset: 0x00058AEF
		[DataSourceProperty]
		public bool IsTeleporting
		{
			get
			{
				return this._isTeleporting;
			}
			set
			{
				if (value != this._isTeleporting)
				{
					this._isTeleporting = value;
					base.OnPropertyChangedWithValue(value, "IsTeleporting");
				}
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005A90D File Offset: 0x00058B0D
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x0005A915 File Offset: 0x00058B15
		[DataSourceProperty]
		public bool IsRecallVisible
		{
			get
			{
				return this._isRecallVisible;
			}
			set
			{
				if (value != this._isRecallVisible)
				{
					this._isRecallVisible = value;
					base.OnPropertyChangedWithValue(value, "IsRecallVisible");
				}
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0005A933 File Offset: 0x00058B33
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0005A93B File Offset: 0x00058B3B
		[DataSourceProperty]
		public bool IsRecallEnabled
		{
			get
			{
				return this._isRecallEnabled;
			}
			set
			{
				if (value != this._isRecallEnabled)
				{
					this._isRecallEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsRecallEnabled");
				}
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005A959 File Offset: 0x00058B59
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0005A961 File Offset: 0x00058B61
		[DataSourceProperty]
		public bool IsTalkVisible
		{
			get
			{
				return this._isTalkVisible;
			}
			set
			{
				if (value != this._isTalkVisible)
				{
					this._isTalkVisible = value;
					base.OnPropertyChangedWithValue(value, "IsTalkVisible");
				}
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0005A97F File Offset: 0x00058B7F
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x0005A987 File Offset: 0x00058B87
		[DataSourceProperty]
		public bool IsTalkEnabled
		{
			get
			{
				return this._isTalkEnabled;
			}
			set
			{
				if (value != this._isTalkEnabled)
				{
					this._isTalkEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTalkEnabled");
				}
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0005A9A5 File Offset: 0x00058BA5
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0005A9AD File Offset: 0x00058BAD
		[DataSourceProperty]
		public bool CanShowLocationOfHero
		{
			get
			{
				return this._canShowLocationOfHero;
			}
			set
			{
				if (value != this._canShowLocationOfHero)
				{
					this._canShowLocationOfHero = value;
					base.OnPropertyChangedWithValue(value, "CanShowLocationOfHero");
				}
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005A9CB File Offset: 0x00058BCB
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0005A9D3 File Offset: 0x00058BD3
		[DataSourceProperty]
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChangedWithValue(value, "IsMainHero");
				}
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0005A9F1 File Offset: 0x00058BF1
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0005A9F9 File Offset: 0x00058BF9
		[DataSourceProperty]
		public bool IsFamilyMember
		{
			get
			{
				return this._isFamilyMember;
			}
			set
			{
				if (value != this._isFamilyMember)
				{
					this._isFamilyMember = value;
					base.OnPropertyChangedWithValue(value, "IsFamilyMember");
				}
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0005AA17 File Offset: 0x00058C17
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0005AA1F File Offset: 0x00058C1F
		[DataSourceProperty]
		public bool IsPregnant
		{
			get
			{
				return this._isPregnant;
			}
			set
			{
				if (value != this._isPregnant)
				{
					this._isPregnant = value;
					base.OnPropertyChangedWithValue(value, "IsPregnant");
				}
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0005AA3D File Offset: 0x00058C3D
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0005AA45 File Offset: 0x00058C45
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0005AA63 File Offset: 0x00058C63
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0005AA6B File Offset: 0x00058C6B
		[DataSourceProperty]
		public ImageIdentifierVM Banner_9
		{
			get
			{
				return this._banner_9;
			}
			set
			{
				if (value != this._banner_9)
				{
					this._banner_9 = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner_9");
				}
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0005AA89 File Offset: 0x00058C89
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x0005AA91 File Offset: 0x00058C91
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005AAB4 File Offset: 0x00058CB4
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x0005AABC File Offset: 0x00058CBC
		[DataSourceProperty]
		public string CurrentActionText
		{
			get
			{
				return this._currentActionText;
			}
			set
			{
				if (value != this._currentActionText)
				{
					this._currentActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentActionText");
				}
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005AADF File Offset: 0x00058CDF
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0005AAE7 File Offset: 0x00058CE7
		[DataSourceProperty]
		public string RelationToMainHeroText
		{
			get
			{
				return this._relationToMainHeroText;
			}
			set
			{
				if (value != this._relationToMainHeroText)
				{
					this._relationToMainHeroText = value;
					base.OnPropertyChangedWithValue<string>(value, "RelationToMainHeroText");
				}
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005AB0A File Offset: 0x00058D0A
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0005AB12 File Offset: 0x00058D12
		[DataSourceProperty]
		public string GovernorOfText
		{
			get
			{
				return this._governorOfText;
			}
			set
			{
				if (value != this._governorOfText)
				{
					this._governorOfText = value;
					base.OnPropertyChangedWithValue<string>(value, "GovernorOfText");
				}
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0005AB35 File Offset: 0x00058D35
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x0005AB3D File Offset: 0x00058D3D
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

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0005AB60 File Offset: 0x00058D60
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0005AB68 File Offset: 0x00058D68
		[DataSourceProperty]
		public HintViewModel PregnantHint
		{
			get
			{
				return this._pregnantHint;
			}
			set
			{
				if (value != this._pregnantHint)
				{
					this._pregnantHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PregnantHint");
				}
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0005AB86 File Offset: 0x00058D86
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0005AB8E File Offset: 0x00058D8E
		[DataSourceProperty]
		public HintViewModel ShowOnMapHint
		{
			get
			{
				return this._showOnMapHint;
			}
			set
			{
				if (value != this._showOnMapHint)
				{
					this._showOnMapHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowOnMapHint");
				}
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005ABAC File Offset: 0x00058DAC
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0005ABB4 File Offset: 0x00058DB4
		[DataSourceProperty]
		public HintViewModel RecallHint
		{
			get
			{
				return this._recallHint;
			}
			set
			{
				if (value != this._recallHint)
				{
					this._recallHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RecallHint");
				}
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0005ABD2 File Offset: 0x00058DD2
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0005ABDA File Offset: 0x00058DDA
		[DataSourceProperty]
		public HintViewModel TalkHint
		{
			get
			{
				return this._talkHint;
			}
			set
			{
				if (value != this._talkHint)
				{
					this._talkHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TalkHint");
				}
			}
		}

		// Token: 0x04000B9C RID: 2972
		private readonly Action<ClanLordItemVM> _onCharacterSelect;

		// Token: 0x04000B9D RID: 2973
		private readonly Action _onRecall;

		// Token: 0x04000B9E RID: 2974
		private readonly Action _onTalk;

		// Token: 0x04000B9F RID: 2975
		private readonly Hero _hero;

		// Token: 0x04000BA0 RID: 2976
		private readonly Action<Hero> _showHeroOnMap;

		// Token: 0x04000BA1 RID: 2977
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000BA2 RID: 2978
		private readonly TextObject _prisonerOfText = new TextObject("{=a8nRxITn}Prisoner of {PARTY_NAME}", null);

		// Token: 0x04000BA3 RID: 2979
		private readonly TextObject _showLocationOfHeroOnMap = new TextObject("{=aGJYQOef}Show hero's location on map.", null);

		// Token: 0x04000BA4 RID: 2980
		private readonly TextObject _recallHeroToMainPartyHintText = new TextObject("{=ANV8UV5f}Recall this member to your party.", null);

		// Token: 0x04000BA5 RID: 2981
		private readonly TextObject _talkToHeroHintText = new TextObject("{=j4BdjLYp}Start a conversation with this clan member.", null);

		// Token: 0x04000BA6 RID: 2982
		private ImageIdentifierVM _visual;

		// Token: 0x04000BA7 RID: 2983
		private ImageIdentifierVM _banner_9;

		// Token: 0x04000BA8 RID: 2984
		private bool _isSelected;

		// Token: 0x04000BA9 RID: 2985
		private bool _isChild;

		// Token: 0x04000BAA RID: 2986
		private bool _isMainHero;

		// Token: 0x04000BAB RID: 2987
		private bool _isFamilyMember;

		// Token: 0x04000BAC RID: 2988
		private bool _isPregnant;

		// Token: 0x04000BAD RID: 2989
		private bool _isTeleporting;

		// Token: 0x04000BAE RID: 2990
		private bool _isRecallVisible;

		// Token: 0x04000BAF RID: 2991
		private bool _isRecallEnabled;

		// Token: 0x04000BB0 RID: 2992
		private bool _isTalkVisible;

		// Token: 0x04000BB1 RID: 2993
		private bool _isTalkEnabled;

		// Token: 0x04000BB2 RID: 2994
		private bool _canShowLocationOfHero;

		// Token: 0x04000BB3 RID: 2995
		private string _name;

		// Token: 0x04000BB4 RID: 2996
		private string _locationText;

		// Token: 0x04000BB5 RID: 2997
		private string _relationToMainHeroText;

		// Token: 0x04000BB6 RID: 2998
		private string _governorOfText;

		// Token: 0x04000BB7 RID: 2999
		private string _currentActionText;

		// Token: 0x04000BB8 RID: 3000
		private HeroViewModel _heroModel;

		// Token: 0x04000BB9 RID: 3001
		private MBBindingList<EncyclopediaSkillVM> _skills;

		// Token: 0x04000BBA RID: 3002
		private MBBindingList<EncyclopediaTraitItemVM> _traits;

		// Token: 0x04000BBB RID: 3003
		private HintViewModel _pregnantHint;

		// Token: 0x04000BBC RID: 3004
		private HintViewModel _showOnMapHint;

		// Token: 0x04000BBD RID: 3005
		private HintViewModel _recallHint;

		// Token: 0x04000BBE RID: 3006
		private HintViewModel _talkHint;
	}
}
