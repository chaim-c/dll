using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200011E RID: 286
	public class ClanMembersVM : ViewModel
	{
		// Token: 0x06001B8C RID: 7052 RVA: 0x000636E4 File Offset: 0x000618E4
		public ClanMembersVM(Action onRefresh, Action<Hero> showHeroOnMap)
		{
			this._onRefresh = onRefresh;
			this._faction = Hero.MainHero.Clan;
			this._showHeroOnMap = showHeroOnMap;
			this._teleportationBehavior = Campaign.Current.GetCampaignBehavior<ITeleportationCampaignBehavior>();
			this.Family = new MBBindingList<ClanLordItemVM>();
			this.Companions = new MBBindingList<ClanLordItemVM>();
			MBBindingList<MBBindingList<ClanLordItemVM>> listsToControl = new MBBindingList<MBBindingList<ClanLordItemVM>>
			{
				this.Family,
				this.Companions
			};
			this.SortController = new ClanMembersSortControllerVM(listsToControl);
			this.RefreshMembersList();
			this.RefreshValues();
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00063774 File Offset: 0x00061974
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TraitsText = GameTexts.FindText("str_traits_group", null).ToString();
			this.SkillsText = GameTexts.FindText("str_skills", null).ToString();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.Family.ApplyActionOnAllItems(delegate(ClanLordItemVM x)
			{
				x.RefreshValues();
			});
			this.Companions.ApplyActionOnAllItems(delegate(ClanLordItemVM x)
			{
				x.RefreshValues();
			});
			this.SortController.RefreshValues();
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00063840 File Offset: 0x00061A40
		public void RefreshMembersList()
		{
			this.Family.Clear();
			this.Companions.Clear();
			this.SortController.ResetAllStates();
			List<Hero> list = new List<Hero>();
			foreach (Hero hero in this._faction.Lords)
			{
				if (hero.IsAlive && !hero.IsDisabled)
				{
					if (hero == Hero.MainHero)
					{
						list.Insert(0, hero);
					}
					else
					{
						list.Add(hero);
					}
				}
			}
			IEnumerable<Hero> enumerable = from m in this._faction.Companions
			where m.IsPlayerCompanion
			select m;
			foreach (Hero hero2 in list)
			{
				this.Family.Add(new ClanLordItemVM(hero2, this._teleportationBehavior, this._showHeroOnMap, new Action<ClanLordItemVM>(this.OnMemberSelection), new Action(this.OnRequestRecall), new Action(this.OnTalkWithMember)));
			}
			foreach (Hero hero3 in enumerable)
			{
				this.Companions.Add(new ClanLordItemVM(hero3, this._teleportationBehavior, this._showHeroOnMap, new Action<ClanLordItemVM>(this.OnMemberSelection), new Action(this.OnRequestRecall), new Action(this.OnTalkWithMember)));
			}
			GameTexts.SetVariable("RANK", GameTexts.FindText("str_family_group", null));
			GameTexts.SetVariable("NUMBER", this.Family.Count);
			this.FamilyText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			GameTexts.SetVariable("STR1", GameTexts.FindText("str_companions_group", null));
			GameTexts.SetVariable("LEFT", this._faction.Companions.Count);
			GameTexts.SetVariable("RIGHT", this._faction.CompanionLimit);
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null));
			this.CompanionsText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			this.OnMemberSelection(this.GetDefaultMember());
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00063AC0 File Offset: 0x00061CC0
		private ClanLordItemVM GetDefaultMember()
		{
			if (this.Family.Count > 0)
			{
				return this.Family[0];
			}
			if (this.Companions.Count <= 0)
			{
				return null;
			}
			return this.Companions[0];
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00063AFC File Offset: 0x00061CFC
		public void SelectMember(Hero hero)
		{
			bool flag = false;
			foreach (ClanLordItemVM clanLordItemVM in this.Family)
			{
				if (clanLordItemVM.GetHero() == hero)
				{
					this.OnMemberSelection(clanLordItemVM);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				foreach (ClanLordItemVM clanLordItemVM2 in this.Companions)
				{
					if (clanLordItemVM2.GetHero() == hero)
					{
						this.OnMemberSelection(clanLordItemVM2);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				foreach (ClanLordItemVM clanLordItemVM3 in this.Family)
				{
					if (clanLordItemVM3.GetHero() == Hero.MainHero)
					{
						this.OnMemberSelection(clanLordItemVM3);
						flag = true;
						break;
					}
				}
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00063BF8 File Offset: 0x00061DF8
		private void OnMemberSelection(ClanLordItemVM member)
		{
			if (this.CurrentSelectedMember != null)
			{
				this.CurrentSelectedMember.IsSelected = false;
			}
			this.CurrentSelectedMember = member;
			if (member != null)
			{
				member.IsSelected = true;
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00063C20 File Offset: 0x00061E20
		private void OnRequestRecall()
		{
			ClanLordItemVM currentSelectedMember = this.CurrentSelectedMember;
			Hero hero = (currentSelectedMember != null) ? currentSelectedMember.GetHero() : null;
			if (hero != null)
			{
				int hours = (int)Math.Ceiling((double)Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(hero, PartyBase.MainParty).ResultNumber);
				MBTextManager.SetTextVariable("TRAVEL_DURATION", CampaignUIHelper.GetHoursAndDaysTextFromHourValue(hours).ToString(), false);
				MBTextManager.SetTextVariable("HERO_NAME", hero.Name.ToString(), false);
				object obj = GameTexts.FindText("str_recall_member", null);
				TextObject textObject = GameTexts.FindText("str_recall_clan_member_inquiry", null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.OnConfirmRecall), null, "", 0f, null, null, null), false, false);
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00063D06 File Offset: 0x00061F06
		private void OnConfirmRecall()
		{
			TeleportHeroAction.ApplyDelayedTeleportToParty(this.CurrentSelectedMember.GetHero(), MobileParty.MainParty);
			Action onRefresh = this._onRefresh;
			if (onRefresh == null)
			{
				return;
			}
			onRefresh();
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00063D2D File Offset: 0x00061F2D
		private void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00063D40 File Offset: 0x00061F40
		private void OnTalkWithMember()
		{
			ClanLordItemVM currentSelectedMember = this.CurrentSelectedMember;
			bool flag;
			if (currentSelectedMember == null)
			{
				flag = (null != null);
			}
			else
			{
				Hero hero = currentSelectedMember.GetHero();
				flag = (((hero != null) ? hero.CharacterObject : null) != null);
			}
			if (flag)
			{
				CharacterObject characterObject = this.CurrentSelectedMember.GetHero().CharacterObject;
				LocationComplex locationComplex = LocationComplex.Current;
				Location location = (locationComplex != null) ? locationComplex.GetLocationOfCharacter(LocationComplex.Current.GetFirstLocationCharacterOfCharacter(characterObject)) : null;
				if (location == null)
				{
					CampaignMission.OpenConversationMission(new ConversationCharacterData(CharacterObject.PlayerCharacter, PartyBase.MainParty, false, false, false, false, false, false), new ConversationCharacterData(characterObject, PartyBase.MainParty, false, false, false, false, false, false), "", "");
					return;
				}
				PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(location, null, characterObject, null);
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00063DE4 File Offset: 0x00061FE4
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Family.ApplyActionOnAllItems(delegate(ClanLordItemVM f)
			{
				f.OnFinalize();
			});
			this.Companions.ApplyActionOnAllItems(delegate(ClanLordItemVM f)
			{
				f.OnFinalize();
			});
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00063E4B File Offset: 0x0006204B
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x00063E53 File Offset: 0x00062053
		[DataSourceProperty]
		public bool IsAnyValidMemberSelected
		{
			get
			{
				return this._isAnyValidMemberSelected;
			}
			set
			{
				if (value != this._isAnyValidMemberSelected)
				{
					this._isAnyValidMemberSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidMemberSelected");
				}
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00063E71 File Offset: 0x00062071
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x00063E79 File Offset: 0x00062079
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

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00063E97 File Offset: 0x00062097
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00063E9F File Offset: 0x0006209F
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

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00063EC2 File Offset: 0x000620C2
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x00063ECA File Offset: 0x000620CA
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

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00063EED File Offset: 0x000620ED
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x00063EF5 File Offset: 0x000620F5
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

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00063F18 File Offset: 0x00062118
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x00063F20 File Offset: 0x00062120
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

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00063F43 File Offset: 0x00062143
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x00063F4B File Offset: 0x0006214B
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

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00063F6E File Offset: 0x0006216E
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x00063F76 File Offset: 0x00062176
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

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00063F99 File Offset: 0x00062199
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x00063FA1 File Offset: 0x000621A1
		[DataSourceProperty]
		public MBBindingList<ClanLordItemVM> Companions
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
					base.OnPropertyChangedWithValue<MBBindingList<ClanLordItemVM>>(value, "Companions");
				}
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00063FBF File Offset: 0x000621BF
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x00063FC7 File Offset: 0x000621C7
		[DataSourceProperty]
		public MBBindingList<ClanLordItemVM> Family
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
					base.OnPropertyChangedWithValue<MBBindingList<ClanLordItemVM>>(value, "Family");
				}
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00063FE5 File Offset: 0x000621E5
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x00063FED File Offset: 0x000621ED
		[DataSourceProperty]
		public ClanLordItemVM CurrentSelectedMember
		{
			get
			{
				return this._currentSelectedMember;
			}
			set
			{
				if (value != this._currentSelectedMember)
				{
					this._currentSelectedMember = value;
					base.OnPropertyChangedWithValue<ClanLordItemVM>(value, "CurrentSelectedMember");
					this.IsAnyValidMemberSelected = (value != null);
				}
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00064015 File Offset: 0x00062215
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x0006401D File Offset: 0x0006221D
		[DataSourceProperty]
		public ClanMembersSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<ClanMembersSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x04000D08 RID: 3336
		private readonly Clan _faction;

		// Token: 0x04000D09 RID: 3337
		private readonly Action _onRefresh;

		// Token: 0x04000D0A RID: 3338
		private readonly Action<Hero> _showHeroOnMap;

		// Token: 0x04000D0B RID: 3339
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000D0C RID: 3340
		private bool _isSelected;

		// Token: 0x04000D0D RID: 3341
		private MBBindingList<ClanLordItemVM> _companions;

		// Token: 0x04000D0E RID: 3342
		private MBBindingList<ClanLordItemVM> _family;

		// Token: 0x04000D0F RID: 3343
		private ClanLordItemVM _currentSelectedMember;

		// Token: 0x04000D10 RID: 3344
		private string _familyText;

		// Token: 0x04000D11 RID: 3345
		private string _traitsText;

		// Token: 0x04000D12 RID: 3346
		private string _companionsText;

		// Token: 0x04000D13 RID: 3347
		private string _skillsText;

		// Token: 0x04000D14 RID: 3348
		private string _nameText;

		// Token: 0x04000D15 RID: 3349
		private string _locationText;

		// Token: 0x04000D16 RID: 3350
		private bool _isAnyValidMemberSelected;

		// Token: 0x04000D17 RID: 3351
		private ClanMembersSortControllerVM _sortController;
	}
}
