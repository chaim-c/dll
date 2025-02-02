using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x02000120 RID: 288
	public class ClanPartiesVM : ViewModel
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00064432 File Offset: 0x00062632
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0006443A File Offset: 0x0006263A
		public int TotalExpense { get; private set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00064443 File Offset: 0x00062643
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0006444B File Offset: 0x0006264B
		public int TotalIncome { get; private set; }

		// Token: 0x06001BCC RID: 7116 RVA: 0x00064454 File Offset: 0x00062654
		public ClanPartiesVM(Action onExpenseChange, Action<Hero> openPartyAsManage, Action onRefresh, Action<ClanCardSelectionInfo> openCardSelectionPopup)
		{
			this._onExpenseChange = onExpenseChange;
			this._onRefresh = onRefresh;
			this._disbandBehavior = Campaign.Current.GetCampaignBehavior<IDisbandPartyCampaignBehavior>();
			this._teleportationBehavior = Campaign.Current.GetCampaignBehavior<ITeleportationCampaignBehavior>();
			this._openPartyAsManage = openPartyAsManage;
			this._openCardSelectionPopup = openCardSelectionPopup;
			this._faction = Hero.MainHero.Clan;
			this.Parties = new MBBindingList<ClanPartyItemVM>();
			this.Garrisons = new MBBindingList<ClanPartyItemVM>();
			this.Caravans = new MBBindingList<ClanPartyItemVM>();
			MBBindingList<MBBindingList<ClanPartyItemVM>> listsToControl = new MBBindingList<MBBindingList<ClanPartyItemVM>>
			{
				this.Parties,
				this.Garrisons,
				this.Caravans
			};
			this.SortController = new ClanPartiesSortControllerVM(listsToControl);
			this.CreateNewPartyActionHint = new HintViewModel();
			this.RefreshPartiesList();
			this.RefreshValues();
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0006455C File Offset: 0x0006275C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SizeText = GameTexts.FindText("str_clan_party_size", null).ToString();
			this.MoraleText = GameTexts.FindText("str_morale", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.CreateNewPartyText = GameTexts.FindText("str_clan_create_new_party", null).ToString();
			this.GarrisonsText = GameTexts.FindText("str_clan_garrisons", null).ToString();
			this.CaravansText = GameTexts.FindText("str_clan_caravans", null).ToString();
			this.RefreshPartiesList();
			this.Parties.ApplyActionOnAllItems(delegate(ClanPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.Garrisons.ApplyActionOnAllItems(delegate(ClanPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.Caravans.ApplyActionOnAllItems(delegate(ClanPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.SortController.RefreshValues();
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00064698 File Offset: 0x00062898
		public void RefreshTotalExpense()
		{
			this.TotalExpense = (from p in this.Parties.Union(this.Garrisons).Union(this.Caravans)
			where p.ShouldPartyHaveExpense
			select p).Sum((ClanPartyItemVM p) => p.Expense);
			this.TotalIncome = this.Caravans.Sum((ClanPartyItemVM p) => p.Income);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00064740 File Offset: 0x00062940
		public void RefreshPartiesList()
		{
			this.Parties.Clear();
			this.Garrisons.Clear();
			this.Caravans.Clear();
			this.SortController.ResetAllStates();
			foreach (WarPartyComponent warPartyComponent in this._faction.WarPartyComponents)
			{
				if (warPartyComponent.MobileParty == MobileParty.MainParty)
				{
					this.Parties.Insert(0, new ClanPartyItemVM(warPartyComponent.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Main, this._disbandBehavior, this._teleportationBehavior));
				}
				else
				{
					this.Parties.Add(new ClanPartyItemVM(warPartyComponent.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Member, this._disbandBehavior, this._teleportationBehavior));
				}
			}
			using (IEnumerator<CaravanPartyComponent> enumerator2 = this._faction.Heroes.SelectMany((Hero h) => h.OwnedCaravans).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					CaravanPartyComponent party = enumerator2.Current;
					if (!this.Caravans.Any((ClanPartyItemVM c) => c.Party.MobileParty == party.MobileParty))
					{
						this.Caravans.Add(new ClanPartyItemVM(party.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Caravan, this._disbandBehavior, this._teleportationBehavior));
					}
				}
			}
			using (IEnumerator<MobileParty> enumerator3 = (from a in this._faction.Settlements
			where a.Town != null
			select a into s
			select s.Town.GarrisonParty).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					MobileParty garrison = enumerator3.Current;
					if (garrison != null && !this.Garrisons.Any((ClanPartyItemVM c) => c.Party == garrison.Party))
					{
						this.Garrisons.Add(new ClanPartyItemVM(garrison.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Garrison, this._disbandBehavior, this._teleportationBehavior));
					}
				}
			}
			int count = this._faction.WarPartyComponents.Count;
			(from h in this._faction.Heroes
			where !h.IsDisabled
			select h).Union(this._faction.Companions).Any((Hero h) => h.IsActive && h.PartyBelongedToAsPrisoner == null && !h.IsChild && h.CanLeadParty() && (h.PartyBelongedTo == null || h.PartyBelongedTo.LeaderHero != h));
			TextObject hintText;
			this.CanCreateNewParty = this.GetCanCreateNewParty(out hintText);
			this.CreateNewPartyActionHint.HintText = hintText;
			GameTexts.SetVariable("CURRENT", count);
			GameTexts.SetVariable("LIMIT", this._faction.CommanderLimit);
			this.PartiesText = GameTexts.FindText("str_clan_parties", null).ToString();
			GameTexts.SetVariable("CURRENT", this.Caravans.Count);
			this.CaravansText = GameTexts.FindText("str_clan_caravans", null).ToString();
			GameTexts.SetVariable("CURRENT", this.Garrisons.Count);
			this.GarrisonsText = GameTexts.FindText("str_clan_garrisons", null).ToString();
			this.OnPartySelection(this.GetDefaultMember());
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00064B6C File Offset: 0x00062D6C
		private bool GetCanCreateNewParty(out TextObject disabledReason)
		{
			bool flag = (from h in this._faction.Heroes
			where !h.IsDisabled
			select h).Union(this._faction.Companions).Any((Hero h) => h.IsActive && h.PartyBelongedToAsPrisoner == null && !h.IsChild && h.CanLeadParty() && (h.PartyBelongedTo == null || h.PartyBelongedTo.LeaderHero != h));
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (this._faction.CommanderLimit - this._faction.WarPartyComponents.Count <= 0)
			{
				disabledReason = GameTexts.FindText("str_clan_doesnt_have_empty_party_slots", null);
				return false;
			}
			if (!flag)
			{
				disabledReason = GameTexts.FindText("str_clan_doesnt_have_available_heroes", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00064C33 File Offset: 0x00062E33
		private void OnAnyExpenseChange()
		{
			this.RefreshTotalExpense();
			this._onExpenseChange();
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00064C46 File Offset: 0x00062E46
		private ClanPartyItemVM GetDefaultMember()
		{
			return this.Parties.FirstOrDefault<ClanPartyItemVM>();
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00064C54 File Offset: 0x00062E54
		public void ExecuteCreateNewParty()
		{
			if (this.CanCreateNewParty)
			{
				List<InquiryElement> list = new List<InquiryElement>();
				foreach (Hero hero in (from h in this._faction.Heroes
				where !h.IsDisabled
				select h).Union(this._faction.Companions))
				{
					if ((hero.IsActive || hero.IsReleased || hero.IsFugitive) && !hero.IsChild && hero != Hero.MainHero && hero.CanLeadParty())
					{
						bool isEnabled = false;
						string hint = this.GetPartyLeaderAssignmentSkillsHint(hero);
						if (hero.PartyBelongedToAsPrisoner != null)
						{
							hint = new TextObject("{=vOojEcIf}You cannot assign a prisoner member as a new party leader", null).ToString();
						}
						else if (hero.IsReleased)
						{
							hint = new TextObject("{=OhNYkblK}This hero has just escaped from captors and will be available after some time.", null).ToString();
						}
						else if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero == hero)
						{
							hint = new TextObject("{=aFYwbosi}This hero is already leading a party.", null).ToString();
						}
						else if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero != Hero.MainHero)
						{
							hint = new TextObject("{=FjJi1DJb}This hero is already a part of an another party.", null).ToString();
						}
						else if (hero.GovernorOf != null)
						{
							hint = new TextObject("{=Hz8XO8wk}Governors cannot lead a mobile party and be a governor at the same time.", null).ToString();
						}
						else if (hero.HeroState == Hero.CharacterStates.Disabled)
						{
							hint = new TextObject("{=slzfQzl3}This hero is lost", null).ToString();
						}
						else if (hero.HeroState == Hero.CharacterStates.Fugitive)
						{
							hint = new TextObject("{=dD3kRDHi}This hero is a fugitive and running from their captors. They will be available after some time.", null).ToString();
						}
						else
						{
							isEnabled = true;
						}
						list.Add(new InquiryElement(hero, hero.Name.ToString(), new ImageIdentifier(CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false)), isEnabled, hint));
					}
				}
				if (list.Count > 0)
				{
					MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("{=0Q4Xo2BQ}Select the Leader of the New Party", null).ToString(), string.Empty, list, true, 1, 1, GameTexts.FindText("str_done", null).ToString(), "", new Action<List<InquiryElement>>(this.OnNewPartySelectionOver), new Action<List<InquiryElement>>(this.OnNewPartySelectionOver), "", false), false, false);
					return;
				}
				MBInformationManager.AddQuickInformation(new TextObject("{=qZvNIVGV}There is no one available in your clan who can lead a party right now.", null), 0, null, "");
			}
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00064ED4 File Offset: 0x000630D4
		private void OnNewPartySelectionOver(List<InquiryElement> element)
		{
			if (element.Count == 0)
			{
				return;
			}
			Hero hero = (Hero)element[0].Identifier;
			bool flag = hero.PartyBelongedTo == MobileParty.MainParty;
			if (flag)
			{
				this._openPartyAsManage(hero);
				return;
			}
			MobilePartyHelper.CreateNewClanMobileParty(hero, this._faction, out flag);
			this._onRefresh();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00064F34 File Offset: 0x00063134
		public void SelectParty(PartyBase party)
		{
			foreach (ClanPartyItemVM clanPartyItemVM in this.Parties)
			{
				if (clanPartyItemVM.Party == party)
				{
					this.OnPartySelection(clanPartyItemVM);
					break;
				}
			}
			foreach (ClanPartyItemVM clanPartyItemVM2 in this.Caravans)
			{
				if (clanPartyItemVM2.Party == party)
				{
					this.OnPartySelection(clanPartyItemVM2);
					break;
				}
			}
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00064FD4 File Offset: 0x000631D4
		private void OnPartySelection(ClanPartyItemVM party)
		{
			if (this.CurrentSelectedParty != null)
			{
				this.CurrentSelectedParty.IsSelected = false;
			}
			this.CurrentSelectedParty = party;
			if (party != null)
			{
				party.IsSelected = true;
			}
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00064FFC File Offset: 0x000631FC
		private string GetPartyLeaderAssignmentSkillsHint(Hero hero)
		{
			string text = "";
			int num = 0;
			foreach (SkillObject skillObject in this._leaderAssignmentRelevantSkills)
			{
				int skillValue = hero.GetSkillValue(skillObject);
				GameTexts.SetVariable("LEFT", skillObject.Name.ToString());
				GameTexts.SetVariable("RIGHT", skillValue);
				string text2 = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				if (num == 0)
				{
					text = text2;
				}
				else
				{
					GameTexts.SetVariable("STR1", text);
					GameTexts.SetVariable("STR2", text2);
					text = GameTexts.FindText("str_string_newline_string", null).ToString();
				}
				num++;
			}
			return text;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000650BC File Offset: 0x000632BC
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Parties.ApplyActionOnAllItems(delegate(ClanPartyItemVM p)
			{
				p.OnFinalize();
			});
			this.Garrisons.ApplyActionOnAllItems(delegate(ClanPartyItemVM p)
			{
				p.OnFinalize();
			});
			this.Caravans.ApplyActionOnAllItems(delegate(ClanPartyItemVM p)
			{
				p.OnFinalize();
			});
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00065150 File Offset: 0x00063350
		public void OnShowChangeLeaderPopup()
		{
			ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
			bool flag;
			if (currentSelectedParty == null)
			{
				flag = (null != null);
			}
			else
			{
				PartyBase party = currentSelectedParty.Party;
				flag = (((party != null) ? party.MobileParty : null) != null);
			}
			if (flag)
			{
				ClanCardSelectionInfo obj = new ClanCardSelectionInfo(GameTexts.FindText("str_change_party_leader", null), this.GetChangeLeaderCandidates(), new Action<List<object>, Action>(this.OnChangeLeaderOver), false);
				Action<ClanCardSelectionInfo> openCardSelectionPopup = this._openCardSelectionPopup;
				if (openCardSelectionPopup == null)
				{
					return;
				}
				openCardSelectionPopup(obj);
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000651B3 File Offset: 0x000633B3
		private IEnumerable<ClanCardSelectionItemInfo> GetChangeLeaderCandidates()
		{
			TextObject disabledReason;
			bool canDisbandParty = this.GetCanDisbandParty(out disabledReason);
			yield return new ClanCardSelectionItemInfo(GameTexts.FindText("str_disband_party", null), !canDisbandParty, disabledReason, null);
			foreach (Hero hero in (from h in this._faction.Heroes
			where !h.IsDisabled
			select h).Union(this._faction.Companions))
			{
				if ((hero.IsActive || hero.IsReleased || hero.IsFugitive || hero.IsTraveling) && !hero.IsChild && hero != Hero.MainHero && hero.CanLeadParty())
				{
					Hero hero2 = hero;
					ClanPartyMemberItemVM leaderMember = this.CurrentSelectedParty.LeaderMember;
					if (hero2 != ((leaderMember != null) ? leaderMember.HeroObject : null))
					{
						TextObject disabledReason2;
						bool flag = FactionHelper.IsMainClanMemberAvailableForPartyLeaderChange(hero, true, this.CurrentSelectedParty.Party.MobileParty, out disabledReason2);
						ImageIdentifier image = new ImageIdentifier(CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false));
						yield return new ClanCardSelectionItemInfo(hero, hero.Name, image, CardSelectionItemSpriteType.None, null, null, this.GetChangeLeaderCandidateProperties(hero), !flag, disabledReason2, null);
					}
				}
			}
			IEnumerator<Hero> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000651C3 File Offset: 0x000633C3
		private IEnumerable<ClanCardSelectionItemPropertyInfo> GetChangeLeaderCandidateProperties(Hero hero)
		{
			TextObject teleportationDelayText = CampaignUIHelper.GetTeleportationDelayText(hero, this.CurrentSelectedParty.Party);
			yield return new ClanCardSelectionItemPropertyInfo(teleportationDelayText);
			TextObject textObject = new TextObject("{=hwrQqWir}No Skills", null);
			int num = 0;
			foreach (SkillObject skillObject in this._leaderAssignmentRelevantSkills)
			{
				TextObject textObject2 = new TextObject("{=!}{SKILL_VALUE}", null);
				textObject2.SetTextVariable("SKILL_VALUE", hero.GetSkillValue(skillObject));
				TextObject textObject3 = ClanCardSelectionItemPropertyInfo.CreateLabeledValueText(skillObject.Name, textObject2);
				if (num == 0)
				{
					textObject = textObject3;
				}
				else
				{
					TextObject textObject4 = GameTexts.FindText("str_string_newline_newline_string", null);
					textObject4.SetTextVariable("STR1", textObject);
					textObject4.SetTextVariable("STR2", textObject3);
					textObject = textObject4;
				}
				num++;
			}
			yield return new ClanCardSelectionItemPropertyInfo(GameTexts.FindText("str_skills", null), textObject);
			yield break;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000651DC File Offset: 0x000633DC
		private void OnChangeLeaderOver(List<object> selectedItems, Action closePopup)
		{
			if (selectedItems.Count == 1)
			{
				Hero newLeader = selectedItems.FirstOrDefault<object>() as Hero;
				bool isDisband = newLeader == null;
				ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
				MobileParty mobileParty;
				if (currentSelectedParty == null)
				{
					mobileParty = null;
				}
				else
				{
					PartyBase party = currentSelectedParty.Party;
					mobileParty = ((party != null) ? party.MobileParty : null);
				}
				MobileParty mobileParty2 = mobileParty;
				DelayedTeleportationModel delayedTeleportationModel = Campaign.Current.Models.DelayedTeleportationModel;
				int num = (!isDisband && mobileParty2 != null) ? ((int)Math.Ceiling((double)delayedTeleportationModel.GetTeleportationDelayAsHours(newLeader, mobileParty2.Party).ResultNumber)) : 0;
				MBTextManager.SetTextVariable("TRAVEL_DURATION", CampaignUIHelper.GetHoursAndDaysTextFromHourValue(num).ToString(), false);
				Hero newLeader2 = newLeader;
				if (((newLeader2 != null) ? newLeader2.CharacterObject : null) != null)
				{
					StringHelpers.SetCharacterProperties("LEADER", newLeader.CharacterObject, null, false);
				}
				object obj = GameTexts.FindText(isDisband ? "str_disband_party" : "str_change_clan_party_leader", null);
				TextObject textObject = GameTexts.FindText(isDisband ? "str_disband_party_inquiry" : ((num == 0) ? "str_change_clan_party_leader_instantly_inquiry" : "str_change_clan_party_leader_inquiry"), null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					Action closePopup3 = closePopup;
					if (closePopup3 != null)
					{
						closePopup3();
					}
					if (isDisband)
					{
						this.OnDisbandCurrentParty();
					}
					else
					{
						this.OnPartyLeaderChanged(newLeader);
					}
					Action onRefresh = this._onRefresh;
					if (onRefresh == null)
					{
						return;
					}
					onRefresh();
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			Action closePopup2 = closePopup;
			if (closePopup2 == null)
			{
				return;
			}
			closePopup2();
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00065380 File Offset: 0x00063580
		private void OnPartyLeaderChanged(Hero newLeader)
		{
			ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
			bool flag;
			if (currentSelectedParty == null)
			{
				flag = (null != null);
			}
			else
			{
				PartyBase party = currentSelectedParty.Party;
				flag = (((party != null) ? party.LeaderHero : null) != null);
			}
			if (flag)
			{
				TeleportHeroAction.ApplyDelayedTeleportToParty(this.CurrentSelectedParty.Party.LeaderHero, MobileParty.MainParty);
			}
			TeleportHeroAction.ApplyDelayedTeleportToPartyAsPartyLeader(newLeader, this.CurrentSelectedParty.Party.MobileParty);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000653DD File Offset: 0x000635DD
		private void OnDisbandCurrentParty()
		{
			DisbandPartyAction.StartDisband(this.CurrentSelectedParty.Party.MobileParty);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000653F4 File Offset: 0x000635F4
		private bool GetCanDisbandParty(out TextObject cannotDisbandReason)
		{
			bool result = false;
			cannotDisbandReason = TextObject.Empty;
			ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
			MobileParty mobileParty;
			if (currentSelectedParty == null)
			{
				mobileParty = null;
			}
			else
			{
				PartyBase party = currentSelectedParty.Party;
				mobileParty = ((party != null) ? party.MobileParty : null);
			}
			MobileParty mobileParty2 = mobileParty;
			if (mobileParty2 != null)
			{
				TextObject textObject;
				if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
				{
					cannotDisbandReason = textObject;
				}
				else if (mobileParty2.IsMilitia)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_milita_party", null);
				}
				else if (mobileParty2.IsGarrison)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_garrison_party", null);
				}
				else if (mobileParty2.IsMainParty)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_main_party", null);
				}
				else if (this.CurrentSelectedParty.IsDisbanding)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_already_disbanding_party", null);
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0006549E File Offset: 0x0006369E
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x000654A6 File Offset: 0x000636A6
		[DataSourceProperty]
		public HintViewModel CreateNewPartyActionHint
		{
			get
			{
				return this._createNewPartyActionHint;
			}
			set
			{
				if (value != this._createNewPartyActionHint)
				{
					this._createNewPartyActionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CreateNewPartyActionHint");
				}
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000654C4 File Offset: 0x000636C4
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x000654CC File Offset: 0x000636CC
		[DataSourceProperty]
		public bool IsAnyValidPartySelected
		{
			get
			{
				return this._isAnyValidPartySelected;
			}
			set
			{
				if (value != this._isAnyValidPartySelected)
				{
					this._isAnyValidPartySelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidPartySelected");
				}
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000654EA File Offset: 0x000636EA
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x000654F2 File Offset: 0x000636F2
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

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00065515 File Offset: 0x00063715
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x0006551D File Offset: 0x0006371D
		[DataSourceProperty]
		public string CaravansText
		{
			get
			{
				return this._caravansText;
			}
			set
			{
				if (value != this._caravansText)
				{
					this._caravansText = value;
					base.OnPropertyChangedWithValue<string>(value, "CaravansText");
				}
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00065540 File Offset: 0x00063740
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00065548 File Offset: 0x00063748
		[DataSourceProperty]
		public string GarrisonsText
		{
			get
			{
				return this._garrisonsText;
			}
			set
			{
				if (value != this._garrisonsText)
				{
					this._garrisonsText = value;
					base.OnPropertyChangedWithValue<string>(value, "GarrisonsText");
				}
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0006556B File Offset: 0x0006376B
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x00065573 File Offset: 0x00063773
		[DataSourceProperty]
		public string PartiesText
		{
			get
			{
				return this._partiesText;
			}
			set
			{
				if (value != this._partiesText)
				{
					this._partiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartiesText");
				}
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00065596 File Offset: 0x00063796
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0006559E File Offset: 0x0006379E
		[DataSourceProperty]
		public string MoraleText
		{
			get
			{
				return this._moraleText;
			}
			set
			{
				if (value != this._moraleText)
				{
					this._moraleText = value;
					base.OnPropertyChangedWithValue<string>(value, "MoraleText");
				}
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000655C1 File Offset: 0x000637C1
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x000655C9 File Offset: 0x000637C9
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

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000655EC File Offset: 0x000637EC
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x000655F4 File Offset: 0x000637F4
		[DataSourceProperty]
		public string CreateNewPartyText
		{
			get
			{
				return this._createNewPartyText;
			}
			set
			{
				if (value != this._createNewPartyText)
				{
					this._createNewPartyText = value;
					base.OnPropertyChangedWithValue<string>(value, "CreateNewPartyText");
				}
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00065617 File Offset: 0x00063817
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0006561F File Offset: 0x0006381F
		[DataSourceProperty]
		public string SizeText
		{
			get
			{
				return this._sizeText;
			}
			set
			{
				if (value != this._sizeText)
				{
					this._sizeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SizeText");
				}
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00065642 File Offset: 0x00063842
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0006564A File Offset: 0x0006384A
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

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00065668 File Offset: 0x00063868
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x00065670 File Offset: 0x00063870
		[DataSourceProperty]
		public bool CanCreateNewParty
		{
			get
			{
				return this._canCreateNewParty;
			}
			set
			{
				if (value != this._canCreateNewParty)
				{
					this._canCreateNewParty = value;
					base.OnPropertyChangedWithValue(value, "CanCreateNewParty");
				}
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0006568E File Offset: 0x0006388E
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x00065696 File Offset: 0x00063896
		[DataSourceProperty]
		public MBBindingList<ClanPartyItemVM> Parties
		{
			get
			{
				return this._parties;
			}
			set
			{
				if (value != this._parties)
				{
					this._parties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyItemVM>>(value, "Parties");
				}
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000656B4 File Offset: 0x000638B4
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x000656BC File Offset: 0x000638BC
		[DataSourceProperty]
		public MBBindingList<ClanPartyItemVM> Caravans
		{
			get
			{
				return this._caravans;
			}
			set
			{
				if (value != this._caravans)
				{
					this._caravans = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyItemVM>>(value, "Caravans");
				}
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x000656DA File Offset: 0x000638DA
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x000656E2 File Offset: 0x000638E2
		[DataSourceProperty]
		public MBBindingList<ClanPartyItemVM> Garrisons
		{
			get
			{
				return this._garrisons;
			}
			set
			{
				if (value != this._garrisons)
				{
					this._garrisons = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyItemVM>>(value, "Garrisons");
				}
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00065700 File Offset: 0x00063900
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00065708 File Offset: 0x00063908
		[DataSourceProperty]
		public ClanPartyItemVM CurrentSelectedParty
		{
			get
			{
				return this._currentSelectedParty;
			}
			set
			{
				if (value != this._currentSelectedParty)
				{
					this._currentSelectedParty = value;
					base.OnPropertyChangedWithValue<ClanPartyItemVM>(value, "CurrentSelectedParty");
					this.IsAnyValidPartySelected = (value != null);
				}
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00065730 File Offset: 0x00063930
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x00065738 File Offset: 0x00063938
		[DataSourceProperty]
		public ClanPartiesSortControllerVM SortController
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
					base.OnPropertyChangedWithValue<ClanPartiesSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x04000D27 RID: 3367
		private Action _onExpenseChange;

		// Token: 0x04000D28 RID: 3368
		private Action<Hero> _openPartyAsManage;

		// Token: 0x04000D29 RID: 3369
		private Action<ClanCardSelectionInfo> _openCardSelectionPopup;

		// Token: 0x04000D2A RID: 3370
		private readonly IDisbandPartyCampaignBehavior _disbandBehavior;

		// Token: 0x04000D2B RID: 3371
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000D2C RID: 3372
		private readonly Action _onRefresh;

		// Token: 0x04000D2D RID: 3373
		private readonly Clan _faction;

		// Token: 0x04000D2E RID: 3374
		private readonly IEnumerable<SkillObject> _leaderAssignmentRelevantSkills = new List<SkillObject>
		{
			DefaultSkills.Engineering,
			DefaultSkills.Steward,
			DefaultSkills.Scouting,
			DefaultSkills.Medicine
		};

		// Token: 0x04000D2F RID: 3375
		private MBBindingList<ClanPartyItemVM> _parties;

		// Token: 0x04000D30 RID: 3376
		private MBBindingList<ClanPartyItemVM> _garrisons;

		// Token: 0x04000D31 RID: 3377
		private MBBindingList<ClanPartyItemVM> _caravans;

		// Token: 0x04000D32 RID: 3378
		private ClanPartyItemVM _currentSelectedParty;

		// Token: 0x04000D33 RID: 3379
		private HintViewModel _createNewPartyActionHint;

		// Token: 0x04000D34 RID: 3380
		private bool _canCreateNewParty;

		// Token: 0x04000D35 RID: 3381
		private bool _isSelected;

		// Token: 0x04000D36 RID: 3382
		private string _nameText;

		// Token: 0x04000D37 RID: 3383
		private string _moraleText;

		// Token: 0x04000D38 RID: 3384
		private string _locationText;

		// Token: 0x04000D39 RID: 3385
		private string _sizeText;

		// Token: 0x04000D3A RID: 3386
		private string _createNewPartyText;

		// Token: 0x04000D3B RID: 3387
		private string _partiesText;

		// Token: 0x04000D3C RID: 3388
		private string _caravansText;

		// Token: 0x04000D3D RID: 3389
		private string _garrisonsText;

		// Token: 0x04000D3E RID: 3390
		private bool _isAnyValidPartySelected;

		// Token: 0x04000D3F RID: 3391
		private ClanPartiesSortControllerVM _sortController;
	}
}
