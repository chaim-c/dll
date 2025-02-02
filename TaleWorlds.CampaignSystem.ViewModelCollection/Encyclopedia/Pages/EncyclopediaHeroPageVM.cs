using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000B9 RID: 185
	[EncyclopediaViewModel(typeof(Hero))]
	public class EncyclopediaHeroPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x00047DE0 File Offset: 0x00045FE0
		public EncyclopediaHeroPageVM(EncyclopediaPageArgs args) : base(args)
		{
			this._hero = (base.Obj as Hero);
			this._relationAscendingComparer = new HeroRelationComparer(this._hero, true);
			this._relationDescendingComparer = new HeroRelationComparer(this._hero, false);
			TextObject infoHiddenReasonText;
			this.IsInformationHidden = CampaignUIHelper.IsHeroInformationHidden(this._hero, out infoHiddenReasonText);
			this._infoHiddenReasonText = infoHiddenReasonText;
			this._allRelatedHeroes = new List<Hero>
			{
				this._hero.Father,
				this._hero.Mother,
				this._hero.Spouse
			};
			this._allRelatedHeroes.AddRange(this._hero.Children);
			this._allRelatedHeroes.AddRange(this._hero.Siblings);
			this._allRelatedHeroes.AddRange(this._hero.ExSpouses);
			StringHelpers.SetCharacterProperties("NPC", this._hero.CharacterObject, null, false);
			this.Settlements = new MBBindingList<EncyclopediaSettlementVM>();
			this.Dwellings = new MBBindingList<EncyclopediaDwellingVM>();
			this.Allies = new MBBindingList<HeroVM>();
			this.Enemies = new MBBindingList<HeroVM>();
			this.Family = new MBBindingList<EncyclopediaFamilyMemberVM>();
			this.Companions = new MBBindingList<HeroVM>();
			this.History = new MBBindingList<EncyclopediaHistoryEventVM>();
			this.Skills = new MBBindingList<EncyclopediaSkillVM>();
			this.Stats = new MBBindingList<StringPairItemVM>();
			this.Traits = new MBBindingList<EncyclopediaTraitItemVM>();
			this.HeroCharacter = new HeroViewModel(CharacterViewModel.StanceTypes.EmphasizeFace);
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._hero);
			this.Faction = new EncyclopediaFactionVM(this._hero.Clan);
			this.RefreshValues();
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00047F94 File Offset: 0x00046194
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ClanText = GameTexts.FindText("str_clan", null).ToString();
			this.AlliesText = GameTexts.FindText("str_friends", null).ToString();
			this.EnemiesText = GameTexts.FindText("str_enemies", null).ToString();
			this.FamilyText = GameTexts.FindText("str_family_group", null).ToString();
			this.CompanionsText = GameTexts.FindText("str_companions", null).ToString();
			this.DwellingsText = GameTexts.FindText("str_dwellings", null).ToString();
			this.SettlementsText = GameTexts.FindText("str_settlements", null).ToString();
			this.DeceasedText = GameTexts.FindText("str_encyclopedia_deceased", null).ToString();
			this.TraitsText = GameTexts.FindText("str_traits_group", null).ToString();
			this.SkillsText = GameTexts.FindText("str_skills", null).ToString();
			this.InfoText = GameTexts.FindText("str_info", null).ToString();
			this.PregnantHint = new HintViewModel(GameTexts.FindText("str_pregnant", null), null);
			base.UpdateBookmarkHintText();
			this.UpdateInformationText();
			this.Refresh();
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000480C4 File Offset: 0x000462C4
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			this.Settlements.Clear();
			this.Dwellings.Clear();
			this.Allies.Clear();
			this.Enemies.Clear();
			this.Companions.Clear();
			this.Family.Clear();
			this.History.Clear();
			this.Skills.Clear();
			this.Stats.Clear();
			this.Traits.Clear();
			this.NameText = this._hero.Name.ToString();
			string text = GameTexts.FindText("str_missing_info_indicator", null).ToString();
			EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
			this.HasNeutralClan = (this._hero.Clan == null);
			if (!this.IsInformationHidden)
			{
				for (int i = 0; i < TaleWorlds.CampaignSystem.Extensions.Skills.All.Count; i++)
				{
					SkillObject skill = TaleWorlds.CampaignSystem.Extensions.Skills.All[i];
					if (this._hero.GetSkillValue(skill) > 0 && this._hero.GetSkillValue(skill) > 50)
					{
						this.Skills.Add(new EncyclopediaSkillVM(skill, this._hero.GetSkillValue(skill)));
					}
				}
				foreach (TraitObject traitObject in CampaignUIHelper.GetHeroTraits())
				{
					if (this._hero.GetTraitLevel(traitObject) != 0)
					{
						this.Traits.Add(new EncyclopediaTraitItemVM(traitObject, this._hero));
					}
				}
				if (this._hero.Age >= (float)Campaign.Current.Models.AgeModel.HeroComesOfAge)
				{
					for (int j = 0; j < Hero.AllAliveHeroes.Count; j++)
					{
						this.AddHeroToRelatedVMList(Hero.AllAliveHeroes[j]);
					}
					for (int k = 0; k < Hero.DeadOrDisabledHeroes.Count; k++)
					{
						this.AddHeroToRelatedVMList(Hero.DeadOrDisabledHeroes[k]);
					}
					this.Allies.Sort(this._relationDescendingComparer);
					this.Enemies.Sort(this._relationAscendingComparer);
				}
				if (this._hero.Clan != null && this._hero == this._hero.Clan.Leader)
				{
					for (int l = 0; l < this._hero.Clan.Companions.Count; l++)
					{
						Hero hero = this._hero.Clan.Companions[l];
						this.Companions.Add(new HeroVM(hero, false));
					}
				}
				for (int m = 0; m < this._allRelatedHeroes.Count; m++)
				{
					Hero hero2 = this._allRelatedHeroes[m];
					if (hero2 != null && pageOf.IsValidEncyclopediaItem(hero2))
					{
						this.Family.Add(new EncyclopediaFamilyMemberVM(hero2, this._hero));
					}
				}
				for (int n = 0; n < this._hero.OwnedWorkshops.Count; n++)
				{
					this.Dwellings.Add(new EncyclopediaDwellingVM(this._hero.OwnedWorkshops[n].WorkshopType));
				}
				EncyclopediaPage pageOf2 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Settlement));
				for (int num = 0; num < Settlement.All.Count; num++)
				{
					Settlement settlement = Settlement.All[num];
					if (settlement.OwnerClan != null && settlement.OwnerClan.Leader == this._hero && pageOf2.IsValidEncyclopediaItem(settlement))
					{
						this.Settlements.Add(new EncyclopediaSettlementVM(settlement));
					}
				}
			}
			if (this._hero.Culture != null)
			{
				string definition = GameTexts.FindText("str_enc_sf_culture", null).ToString();
				this.Stats.Add(new StringPairItemVM(definition, this._hero.Culture.Name.ToString(), null));
			}
			string definition2 = GameTexts.FindText("str_enc_sf_age", null).ToString();
			this.Stats.Add(new StringPairItemVM(definition2, this.IsInformationHidden ? text : ((int)this._hero.Age).ToString(), null));
			for (int num2 = Campaign.Current.LogEntryHistory.GameActionLogs.Count - 1; num2 >= 0; num2--)
			{
				IEncyclopediaLog encyclopediaLog;
				if ((encyclopediaLog = (Campaign.Current.LogEntryHistory.GameActionLogs[num2] as IEncyclopediaLog)) != null && encyclopediaLog.IsVisibleInEncyclopediaPageOf<Hero>(this._hero))
				{
					this.History.Add(new EncyclopediaHistoryEventVM(encyclopediaLog));
				}
			}
			if (!this._hero.IsNotable && !this._hero.IsWanderer)
			{
				Clan clan = this._hero.Clan;
				if (((clan != null) ? clan.Kingdom : null) != null)
				{
					this.KingdomRankText = CampaignUIHelper.GetHeroKingdomRank(this._hero);
				}
			}
			string heroOccupationName = CampaignUIHelper.GetHeroOccupationName(this._hero);
			if (!string.IsNullOrEmpty(heroOccupationName))
			{
				string definition3 = GameTexts.FindText("str_enc_sf_occupation", null).ToString();
				this.Stats.Add(new StringPairItemVM(definition3, this.IsInformationHidden ? text : heroOccupationName, null));
			}
			if (this._hero != Hero.MainHero)
			{
				string definition4 = GameTexts.FindText("str_enc_sf_relation", null).ToString();
				this.Stats.Add(new StringPairItemVM(definition4, this.IsInformationHidden ? text : this._hero.GetRelationWithPlayer().ToString(), null));
			}
			this.LastSeenText = ((this._hero == Hero.MainHero) ? "" : HeroHelper.GetLastSeenText(this._hero).ToString());
			this.HeroCharacter.FillFrom(this._hero, -1, this._hero.IsNotable, true);
			this.HeroCharacter.SetEquipment(EquipmentIndex.ArmorItemEndSlot, default(EquipmentElement));
			this.HeroCharacter.SetEquipment(EquipmentIndex.HorseHarness, default(EquipmentElement));
			this.HeroCharacter.SetEquipment(EquipmentIndex.NumAllWeaponSlots, default(EquipmentElement));
			this.IsCompanion = (this._hero.CompanionOf != null);
			if (this.IsCompanion)
			{
				this.MasterText = GameTexts.FindText("str_companion_of", null).ToString();
				Clan companionOf = this._hero.CompanionOf;
				this.Master = new HeroVM((companionOf != null) ? companionOf.Leader : null, false);
			}
			this.IsPregnant = this._hero.IsPregnant;
			this.IsDead = !this._hero.IsAlive;
			base.IsLoadingOver = true;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004876C File Offset: 0x0004696C
		private void AddHeroToRelatedVMList(Hero hero)
		{
			if (!Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero)).IsValidEncyclopediaItem(hero) || hero.IsNotable)
			{
				return;
			}
			if (hero != this._hero && hero.IsAlive && hero.Age >= (float)Campaign.Current.Models.AgeModel.HeroComesOfAge && !this._allRelatedHeroes.Contains(hero))
			{
				if (EncyclopediaHeroPageVM.IsFriend(this._hero, hero))
				{
					this.Allies.Add(new HeroVM(hero, false));
					return;
				}
				if (EncyclopediaHeroPageVM.IsEnemy(this._hero, hero))
				{
					this.Enemies.Add(new HeroVM(hero, false));
				}
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00048820 File Offset: 0x00046A20
		private static bool IsFriend(Hero h1, Hero h2)
		{
			return CharacterRelationManager.GetHeroRelation(h1, h2) >= 40;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00048830 File Offset: 0x00046A30
		public static bool IsEnemy(Hero h1, Hero h2)
		{
			return CharacterRelationManager.GetHeroRelation(h1, h2) <= -30;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00048840 File Offset: 0x00046A40
		public override string GetName()
		{
			return this._hero.Name.ToString();
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00048854 File Offset: 0x00046A54
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Heroes", GameTexts.FindText("str_encyclopedia_heroes", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000488B9 File Offset: 0x00046AB9
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000488CC File Offset: 0x00046ACC
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._hero);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._hero);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004891C File Offset: 0x00046B1C
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.HeroCharacter.OnFinalize();
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00048930 File Offset: 0x00046B30
		private void UpdateInformationText()
		{
			this.InformationText = "";
			if (!TextObject.IsNullOrEmpty(this._hero.EncyclopediaText))
			{
				this.InformationText = this._hero.EncyclopediaText.ToString();
				return;
			}
			if (this._hero.CharacterObject.Occupation == Occupation.Lord)
			{
				this.InformationText = Hero.SetHeroEncyclopediaTextAndLinks(this._hero).ToString();
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004899A File Offset: 0x00046B9A
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x000489A2 File Offset: 0x00046BA2
		[DataSourceProperty]
		public EncyclopediaFactionVM Faction
		{
			get
			{
				return this._faction;
			}
			set
			{
				if (value != this._faction)
				{
					this._faction = value;
					base.OnPropertyChangedWithValue<EncyclopediaFactionVM>(value, "Faction");
				}
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x000489C0 File Offset: 0x00046BC0
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x000489C8 File Offset: 0x00046BC8
		[DataSourceProperty]
		public bool IsCompanion
		{
			get
			{
				return this._isCompanion;
			}
			set
			{
				if (value != this._isCompanion)
				{
					this._isCompanion = value;
					base.OnPropertyChangedWithValue(value, "IsCompanion");
				}
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000489E6 File Offset: 0x00046BE6
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x000489EE File Offset: 0x00046BEE
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

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00048A0C File Offset: 0x00046C0C
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00048A14 File Offset: 0x00046C14
		[DataSourceProperty]
		public HeroVM Master
		{
			get
			{
				return this._master;
			}
			set
			{
				if (value != this._master)
				{
					this._master = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Master");
				}
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00048A32 File Offset: 0x00046C32
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00048A3A File Offset: 0x00046C3A
		[DataSourceProperty]
		public string ClanText
		{
			get
			{
				return this._clanText;
			}
			set
			{
				if (value != this._clanText)
				{
					this._clanText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClanText");
				}
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00048A5D File Offset: 0x00046C5D
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00048A65 File Offset: 0x00046C65
		[DataSourceProperty]
		public string InfoText
		{
			get
			{
				return this._infoText;
			}
			set
			{
				if (value != this._infoText)
				{
					this._infoText = value;
					base.OnPropertyChangedWithValue<string>(value, "InfoText");
				}
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00048A88 File Offset: 0x00046C88
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00048A90 File Offset: 0x00046C90
		[DataSourceProperty]
		public string TraitsText
		{
			get
			{
				return this._traitsText;
			}
			set
			{
				if (value != this._traitsText)
				{
					this._traitsText = value;
					base.OnPropertyChangedWithValue<string>(value, "TraitsText");
				}
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00048AB3 File Offset: 0x00046CB3
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x00048ABB File Offset: 0x00046CBB
		[DataSourceProperty]
		public string MasterText
		{
			get
			{
				return this._masterText;
			}
			set
			{
				if (value != this._masterText)
				{
					this._masterText = value;
					base.OnPropertyChangedWithValue<string>(value, "MasterText");
				}
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00048ADE File Offset: 0x00046CDE
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x00048AE6 File Offset: 0x00046CE6
		[DataSourceProperty]
		public string KingdomRankText
		{
			get
			{
				return this._kingdomRankText;
			}
			set
			{
				if (value != this._kingdomRankText)
				{
					this._kingdomRankText = value;
					base.OnPropertyChangedWithValue<string>(value, "KingdomRankText");
				}
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00048B09 File Offset: 0x00046D09
		[DataSourceProperty]
		public string InfoHiddenReasonText
		{
			get
			{
				return this._infoHiddenReasonText.ToString();
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00048B16 File Offset: 0x00046D16
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x00048B1E File Offset: 0x00046D1E
		[DataSourceProperty]
		public string SkillsText
		{
			get
			{
				return this._skillsText;
			}
			set
			{
				if (value != this._skillsText)
				{
					this._skillsText = value;
					base.OnPropertyChangedWithValue<string>(value, "SkillsText");
				}
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00048B41 File Offset: 0x00046D41
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00048B49 File Offset: 0x00046D49
		[DataSourceProperty]
		public HeroViewModel HeroCharacter
		{
			get
			{
				return this._heroCharacter;
			}
			set
			{
				if (value != this._heroCharacter)
				{
					this._heroCharacter = value;
					base.OnPropertyChangedWithValue<HeroViewModel>(value, "HeroCharacter");
				}
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00048B67 File Offset: 0x00046D67
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x00048B6F File Offset: 0x00046D6F
		[DataSourceProperty]
		public string LastSeenText
		{
			get
			{
				return this._lastSeenText;
			}
			set
			{
				if (value != this._lastSeenText)
				{
					this._lastSeenText = value;
					base.OnPropertyChangedWithValue<string>(value, "LastSeenText");
				}
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00048B92 File Offset: 0x00046D92
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00048B9A File Offset: 0x00046D9A
		[DataSourceProperty]
		public string DeceasedText
		{
			get
			{
				return this._deceasedText;
			}
			set
			{
				if (value != this._deceasedText)
				{
					this._deceasedText = value;
					base.OnPropertyChangedWithValue<string>(value, "DeceasedText");
				}
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00048BBD File Offset: 0x00046DBD
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x00048BC5 File Offset: 0x00046DC5
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

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00048BE8 File Offset: 0x00046DE8
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00048BF0 File Offset: 0x00046DF0
		[DataSourceProperty]
		public string SettlementsText
		{
			get
			{
				return this._settlementsText;
			}
			set
			{
				if (value != this._settlementsText)
				{
					this._settlementsText = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementsText");
				}
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00048C13 File Offset: 0x00046E13
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00048C1B File Offset: 0x00046E1B
		[DataSourceProperty]
		public string DwellingsText
		{
			get
			{
				return this._dwellingsText;
			}
			set
			{
				if (value != this._dwellingsText)
				{
					this._dwellingsText = value;
					base.OnPropertyChangedWithValue<string>(value, "DwellingsText");
				}
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00048C3E File Offset: 0x00046E3E
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00048C46 File Offset: 0x00046E46
		[DataSourceProperty]
		public string CompanionsText
		{
			get
			{
				return this._companionsText;
			}
			set
			{
				if (value != this._companionsText)
				{
					this._companionsText = value;
					base.OnPropertyChangedWithValue<string>(value, "CompanionsText");
				}
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00048C69 File Offset: 0x00046E69
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00048C71 File Offset: 0x00046E71
		[DataSourceProperty]
		public string AlliesText
		{
			get
			{
				return this._alliesText;
			}
			set
			{
				if (value != this._alliesText)
				{
					this._alliesText = value;
					base.OnPropertyChangedWithValue<string>(value, "AlliesText");
				}
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00048C94 File Offset: 0x00046E94
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00048C9C File Offset: 0x00046E9C
		[DataSourceProperty]
		public string EnemiesText
		{
			get
			{
				return this._enemiesText;
			}
			set
			{
				if (value != this._enemiesText)
				{
					this._enemiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "EnemiesText");
				}
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00048CBF File Offset: 0x00046EBF
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00048CC7 File Offset: 0x00046EC7
		[DataSourceProperty]
		public string FamilyText
		{
			get
			{
				return this._familyText;
			}
			set
			{
				if (value != this._familyText)
				{
					this._familyText = value;
					base.OnPropertyChangedWithValue<string>(value, "FamilyText");
				}
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00048CEA File Offset: 0x00046EEA
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00048CF2 File Offset: 0x00046EF2
		[DataSourceProperty]
		public MBBindingList<StringPairItemVM> Stats
		{
			get
			{
				return this._stats;
			}
			set
			{
				if (value != this._stats)
				{
					this._stats = value;
					base.OnPropertyChangedWithValue<MBBindingList<StringPairItemVM>>(value, "Stats");
				}
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00048D10 File Offset: 0x00046F10
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x00048D18 File Offset: 0x00046F18
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

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00048D36 File Offset: 0x00046F36
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x00048D3E File Offset: 0x00046F3E
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

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00048D5C File Offset: 0x00046F5C
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x00048D64 File Offset: 0x00046F64
		[DataSourceProperty]
		public MBBindingList<EncyclopediaDwellingVM> Dwellings
		{
			get
			{
				return this._dwellings;
			}
			set
			{
				if (value != this._dwellings)
				{
					this._dwellings = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaDwellingVM>>(value, "Dwellings");
				}
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00048D82 File Offset: 0x00046F82
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00048D8A File Offset: 0x00046F8A
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSettlementVM> Settlements
		{
			get
			{
				return this._settlements;
			}
			set
			{
				if (value != this._settlements)
				{
					this._settlements = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSettlementVM>>(value, "Settlements");
				}
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00048DA8 File Offset: 0x00046FA8
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00048DB0 File Offset: 0x00046FB0
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFamilyMemberVM> Family
		{
			get
			{
				return this._family;
			}
			set
			{
				if (value != this._family)
				{
					this._family = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFamilyMemberVM>>(value, "Family");
				}
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00048DCE File Offset: 0x00046FCE
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00048DD6 File Offset: 0x00046FD6
		[DataSourceProperty]
		public MBBindingList<HeroVM> Companions
		{
			get
			{
				return this._companions;
			}
			set
			{
				if (value != this._companions)
				{
					this._companions = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Companions");
				}
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00048DF4 File Offset: 0x00046FF4
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x00048DFC File Offset: 0x00046FFC
		[DataSourceProperty]
		public MBBindingList<HeroVM> Enemies
		{
			get
			{
				return this._enemies;
			}
			set
			{
				if (value != this._enemies)
				{
					this._enemies = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Enemies");
				}
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00048E1A File Offset: 0x0004701A
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x00048E22 File Offset: 0x00047022
		[DataSourceProperty]
		public MBBindingList<HeroVM> Allies
		{
			get
			{
				return this._allies;
			}
			set
			{
				if (value != this._allies)
				{
					this._allies = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Allies");
				}
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00048E40 File Offset: 0x00047040
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x00048E48 File Offset: 0x00047048
		[DataSourceProperty]
		public MBBindingList<EncyclopediaHistoryEventVM> History
		{
			get
			{
				return this._history;
			}
			set
			{
				if (value != this._history)
				{
					this._history = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaHistoryEventVM>>(value, "History");
				}
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00048E66 File Offset: 0x00047066
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x00048E6E File Offset: 0x0004706E
		[DataSourceProperty]
		public bool HasNeutralClan
		{
			get
			{
				return this._hasNeutralClan;
			}
			set
			{
				if (value != this._hasNeutralClan)
				{
					this._hasNeutralClan = value;
					base.OnPropertyChangedWithValue(value, "HasNeutralClan");
				}
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00048E8C File Offset: 0x0004708C
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x00048E94 File Offset: 0x00047094
		[DataSourceProperty]
		public bool IsDead
		{
			get
			{
				return this._isDead;
			}
			set
			{
				if (value != this._isDead)
				{
					this._isDead = value;
					base.OnPropertyChanged("IsAlive");
					base.OnPropertyChangedWithValue(value, "IsDead");
				}
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00048EBD File Offset: 0x000470BD
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00048EC5 File Offset: 0x000470C5
		[DataSourceProperty]
		public bool IsInformationHidden
		{
			get
			{
				return this._isInformationHidden;
			}
			set
			{
				if (value != this._isInformationHidden)
				{
					this._isInformationHidden = value;
					base.OnPropertyChangedWithValue(value, "IsInformationHidden");
				}
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00048EE3 File Offset: 0x000470E3
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00048EEB File Offset: 0x000470EB
		[DataSourceProperty]
		public string InformationText
		{
			get
			{
				return this._informationText;
			}
			set
			{
				if (value != this._informationText)
				{
					this._informationText = value;
					base.OnPropertyChangedWithValue<string>(value, "InformationText");
				}
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00048F0E File Offset: 0x0004710E
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00048F16 File Offset: 0x00047116
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

		// Token: 0x0400087D RID: 2173
		private readonly Hero _hero;

		// Token: 0x0400087E RID: 2174
		private readonly TextObject _infoHiddenReasonText;

		// Token: 0x0400087F RID: 2175
		private List<Hero> _allRelatedHeroes;

		// Token: 0x04000880 RID: 2176
		private readonly HeroRelationComparer _relationAscendingComparer;

		// Token: 0x04000881 RID: 2177
		private readonly HeroRelationComparer _relationDescendingComparer;

		// Token: 0x04000882 RID: 2178
		private const int _friendLimit = 40;

		// Token: 0x04000883 RID: 2179
		private const int _enemyLimit = -30;

		// Token: 0x04000884 RID: 2180
		private MBBindingList<HeroVM> _enemies;

		// Token: 0x04000885 RID: 2181
		private MBBindingList<HeroVM> _allies;

		// Token: 0x04000886 RID: 2182
		private MBBindingList<EncyclopediaFamilyMemberVM> _family;

		// Token: 0x04000887 RID: 2183
		private MBBindingList<HeroVM> _companions;

		// Token: 0x04000888 RID: 2184
		private MBBindingList<EncyclopediaSettlementVM> _settlements;

		// Token: 0x04000889 RID: 2185
		private MBBindingList<EncyclopediaDwellingVM> _dwellings;

		// Token: 0x0400088A RID: 2186
		private MBBindingList<EncyclopediaHistoryEventVM> _history;

		// Token: 0x0400088B RID: 2187
		private MBBindingList<EncyclopediaSkillVM> _skills;

		// Token: 0x0400088C RID: 2188
		private MBBindingList<StringPairItemVM> _stats;

		// Token: 0x0400088D RID: 2189
		private MBBindingList<EncyclopediaTraitItemVM> _traits;

		// Token: 0x0400088E RID: 2190
		private string _clanText;

		// Token: 0x0400088F RID: 2191
		private string _settlementsText;

		// Token: 0x04000890 RID: 2192
		private string _dwellingsText;

		// Token: 0x04000891 RID: 2193
		private string _alliesText;

		// Token: 0x04000892 RID: 2194
		private string _enemiesText;

		// Token: 0x04000893 RID: 2195
		private string _companionsText;

		// Token: 0x04000894 RID: 2196
		private string _lastSeenText;

		// Token: 0x04000895 RID: 2197
		private string _nameText;

		// Token: 0x04000896 RID: 2198
		private string _informationText;

		// Token: 0x04000897 RID: 2199
		private string _deceasedText;

		// Token: 0x04000898 RID: 2200
		private string _traitsText;

		// Token: 0x04000899 RID: 2201
		private string _skillsText;

		// Token: 0x0400089A RID: 2202
		private string _infoText;

		// Token: 0x0400089B RID: 2203
		private string _kingdomRankText;

		// Token: 0x0400089C RID: 2204
		private string _familyText;

		// Token: 0x0400089D RID: 2205
		private HeroViewModel _heroCharacter;

		// Token: 0x0400089E RID: 2206
		private bool _isCompanion;

		// Token: 0x0400089F RID: 2207
		private bool _isPregnant;

		// Token: 0x040008A0 RID: 2208
		private bool _hasNeutralClan;

		// Token: 0x040008A1 RID: 2209
		private bool _isDead;

		// Token: 0x040008A2 RID: 2210
		private bool _isInformationHidden;

		// Token: 0x040008A3 RID: 2211
		private HeroVM _master;

		// Token: 0x040008A4 RID: 2212
		private EncyclopediaFactionVM _faction;

		// Token: 0x040008A5 RID: 2213
		private string _masterText;

		// Token: 0x040008A6 RID: 2214
		private HintViewModel _pregnantHint;
	}
}
