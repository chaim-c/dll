using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200010B RID: 267
	public class ClanPartyItemVM : ViewModel
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0005C102 File Offset: 0x0005A302
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x0005C10A File Offset: 0x0005A30A
		public int Expense { get; private set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0005C113 File Offset: 0x0005A313
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x0005C11B File Offset: 0x0005A31B
		public int Income { get; private set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0005C124 File Offset: 0x0005A324
		public PartyBase Party { get; }

		// Token: 0x06001969 RID: 6505 RVA: 0x0005C12C File Offset: 0x0005A32C
		public ClanPartyItemVM(PartyBase party, Action<ClanPartyItemVM> onAssignment, Action onExpenseChange, Action onShowChangeLeaderPopup, ClanPartyItemVM.ClanPartyType type, IDisbandPartyCampaignBehavior disbandBehavior, ITeleportationCampaignBehavior teleportationBehavior)
		{
			this.Party = party;
			this._type = type;
			this._disbandBehavior = disbandBehavior;
			this._leader = CampaignUIHelper.GetVisualPartyLeader(this.Party);
			this.HasHeroMembers = party.IsMobile;
			if (this._leader == null)
			{
				TroopRosterElement troopRosterElement = this.Party.MemberRoster.GetTroopRoster().FirstOrDefault<TroopRosterElement>();
				if (!troopRosterElement.Equals(default(TroopRosterElement)))
				{
					this._leader = troopRosterElement.Character;
				}
				else
				{
					IFaction mapFaction = this.Party.MapFaction;
					this._leader = ((mapFaction != null) ? mapFaction.BasicTroop : null);
				}
			}
			CharacterObject leader = this._leader;
			if ((leader == null || !leader.IsHero) && party.IsMobile && (this._type == ClanPartyItemVM.ClanPartyType.Member || this._type == ClanPartyItemVM.ClanPartyType.Caravan))
			{
				Hero teleportingLeaderHero = CampaignUIHelper.GetTeleportingLeaderHero(party.MobileParty, teleportationBehavior);
				this._leader = ((teleportingLeaderHero != null) ? teleportingLeaderHero.CharacterObject : null);
				this._isLeaderTeleporting = (this._leader != null);
			}
			if (this._leader != null)
			{
				CharacterCode characterCode = ClanPartyItemVM.GetCharacterCode(this._leader);
				this.LeaderVisual = new ImageIdentifierVM(characterCode);
				this.CharacterModel = new CharacterViewModel(CharacterViewModel.StanceTypes.None);
				this.CharacterModel.FillFrom(this._leader, -1);
				CharacterViewModel characterModel = this.CharacterModel;
				IFaction mapFaction2 = this.Party.MapFaction;
				characterModel.ArmorColor1 = ((mapFaction2 != null) ? mapFaction2.Color : 0U);
				CharacterViewModel characterModel2 = this.CharacterModel;
				IFaction mapFaction3 = this.Party.MapFaction;
				characterModel2.ArmorColor2 = ((mapFaction3 != null) ? mapFaction3.Color2 : 0U);
			}
			else
			{
				this.LeaderVisual = new ImageIdentifierVM(ImageIdentifierType.Null);
				this.CharacterModel = new CharacterViewModel();
			}
			this._onAssignment = onAssignment;
			this._onExpenseChange = onExpenseChange;
			this._onShowChangeLeaderPopup = onShowChangeLeaderPopup;
			bool isDisbanding;
			if (!this.Party.MobileParty.IsDisbanding)
			{
				IDisbandPartyCampaignBehavior disbandBehavior2 = this._disbandBehavior;
				isDisbanding = (disbandBehavior2 != null && disbandBehavior2.IsPartyWaitingForDisband(party.MobileParty));
			}
			else
			{
				isDisbanding = true;
			}
			this.IsDisbanding = isDisbanding;
			bool flag = !party.MobileParty.IsMilitia && !party.MobileParty.IsVillager && party.MobileParty.IsActive && !this.IsDisbanding;
			this.ShouldPartyHaveExpense = (flag && (type == ClanPartyItemVM.ClanPartyType.Garrison || type == ClanPartyItemVM.ClanPartyType.Member));
			this.IsCaravan = (type == ClanPartyItemVM.ClanPartyType.Caravan);
			TextObject empty = TextObject.Empty;
			this.IsChangeLeaderVisible = (type == ClanPartyItemVM.ClanPartyType.Caravan || type == ClanPartyItemVM.ClanPartyType.Member);
			this.IsChangeLeaderEnabled = (this.IsChangeLeaderVisible && CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out empty));
			this.ChangeLeaderHint = new HintViewModel(this.IsChangeLeaderEnabled ? this._changeLeaderHintText : empty, null);
			if (this.ShouldPartyHaveExpense)
			{
				if (party.MobileParty != null)
				{
					this.ExpenseItem = new ClanFinanceExpenseItemVM(party.MobileParty);
					this.OnExpenseChange();
				}
				else
				{
					Debug.FailedAssert("This party should have expense info but it doesn't", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanPartyItemVM.cs", ".ctor", 115);
				}
			}
			if (this.IsCaravan)
			{
				this.Income = Campaign.Current.Models.ClanFinanceModel.CalculateOwnerIncomeFromCaravan(party.MobileParty);
			}
			this.AutoRecruitmentHint = new HintViewModel(GameTexts.FindText("str_clan_auto_recruitment_hint", null), null);
			this.IsAutoRecruitmentVisible = party.MobileParty.IsGarrison;
			this.AutoRecruitmentValue = (party.MobileParty.IsGarrison && this.Party.MobileParty.CurrentSettlement.Town.GarrisonAutoRecruitmentIsEnabled);
			this.HeroMembers = new MBBindingList<ClanPartyMemberItemVM>();
			this.Roles = new MBBindingList<ClanRoleItemVM>();
			this.InfantryHint = new BasicTooltipViewModel(() => this.GetPartyTroopInfo(this.Party, FormationClass.Infantry));
			this.CavalryHint = new BasicTooltipViewModel(() => this.GetPartyTroopInfo(this.Party, FormationClass.Cavalry));
			this.RangedHint = new BasicTooltipViewModel(() => this.GetPartyTroopInfo(this.Party, FormationClass.Ranged));
			this.HorseArcherHint = new BasicTooltipViewModel(() => this.GetPartyTroopInfo(this.Party, FormationClass.HorseArcher));
			this.ActionsDisabledHint = new HintViewModel();
			this.InArmyHint = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0005C517 File Offset: 0x0005A717
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.UpdateProperties();
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0005C528 File Offset: 0x0005A728
		public void UpdateProperties()
		{
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.AssigneesText = GameTexts.FindText("str_clan_assignee_title", null).ToString();
			this.RolesText = GameTexts.FindText("str_clan_role_title", null).ToString();
			this.PartyLeaderRoleEffectsText = GameTexts.FindText("str_clan_party_leader_roles_and_effects", null).ToString();
			this.AutoRecruitmentText = GameTexts.FindText("str_clan_auto_recruitment", null).ToString();
			PartyBase party = this.Party;
			this.IsPartyBehaviorEnabled = (((party != null) ? party.LeaderHero : null) != null && this.Party.LeaderHero.Clan.Leader != this.Party.LeaderHero && !this.Party.MobileParty.IsCaravan && !this.IsDisbanding);
			if (this.Party == PartyBase.MainParty && Hero.MainHero.IsPrisoner)
			{
				TextObject textObject = new TextObject("{=shL0WElC}{TROOP.NAME}{.o} Party", null);
				textObject.SetCharacterProperties("TROOP", Hero.MainHero.CharacterObject, false);
				this.Name = textObject.ToString();
			}
			else if (this._isLeaderTeleporting)
			{
				TextObject textObject2 = new TextObject("{=P5YtNXHR}{LEADER.NAME}{.o} Party", null);
				StringHelpers.SetCharacterProperties("LEADER", this._leader, textObject2, false);
				this.Name = textObject2.ToString();
			}
			else
			{
				this.Name = this.Party.Name.ToString();
			}
			this.IsMainHeroParty = (this._type == ClanPartyItemVM.ClanPartyType.Main);
			if (this.Party.MobileParty.CurrentSettlement != null)
			{
				this.PartyLocationText = this.Party.MobileParty.CurrentSettlement.Name.ToString();
			}
			else
			{
				Settlement settlement = SettlementHelper.FindNearestSettlement(null, this.Party.MobileParty);
				GameTexts.SetVariable("SETTLEMENT_NAME", settlement.Name);
				string partyLocationText = GameTexts.FindText("str_near_settlement", null).ToString();
				this.PartyLocationText = partyLocationText;
			}
			GameTexts.SetVariable("LEFT", this.Party.MobileParty.MemberRoster.TotalManCount);
			GameTexts.SetVariable("RIGHT", this.Party.MobileParty.Party.PartySizeLimit);
			string text = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
			string content = GameTexts.FindText("str_party_morale_party_size", null).ToString();
			this.PartySizeText = text;
			GameTexts.SetVariable("LEFT", content);
			GameTexts.SetVariable("RIGHT", text);
			this.PartySizeSubTitleText = GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString();
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_party_wage", null));
			GameTexts.SetVariable("RIGHT", this.Party.MobileParty.TotalWage);
			this.PartyWageSubTitleText = GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString();
			this.InArmyText = "";
			if (this.Party.MobileParty.Army != null)
			{
				this.IsInArmy = true;
				TextObject textObject3 = GameTexts.FindText("str_clan_in_army_hint", null);
				TextObject textObject4 = textObject3;
				string tag = "ARMY_LEADER";
				MobileParty leaderParty = this.Party.MobileParty.Army.LeaderParty;
				string text2;
				if (leaderParty == null)
				{
					text2 = null;
				}
				else
				{
					Hero leaderHero = leaderParty.LeaderHero;
					text2 = ((leaderHero != null) ? leaderHero.Name.ToString() : null);
				}
				textObject4.SetTextVariable(tag, text2 ?? string.Empty);
				this.InArmyHint = new HintViewModel(textObject3, null);
				this.InArmyText = GameTexts.FindText("str_in_army", null).ToString();
			}
			this.DisbandingText = "";
			this.IsMembersAndRolesVisible = (!this.IsDisbanding && this._type != ClanPartyItemVM.ClanPartyType.Garrison);
			if (this.IsDisbanding)
			{
				this.DisbandingText = GameTexts.FindText("str_disbanding", null).ToString();
			}
			this.PartyBehaviorText = "";
			if (this.IsPartyBehaviorEnabled)
			{
				this.PartyBehaviorSelector = new SelectorVM<SelectorItemVM>(0, new Action<SelectorVM<SelectorItemVM>>(this.UpdatePartyBehaviorSelectionUpdate));
				for (int i = 0; i < 3; i++)
				{
					string s = GameTexts.FindText("str_clan_party_objective", i.ToString()).ToString();
					TextObject hint = GameTexts.FindText("str_clan_party_objective_hint", i.ToString());
					this.PartyBehaviorSelector.AddItem(new SelectorItemVM(s, hint));
				}
				this.PartyBehaviorSelector.SelectedIndex = (int)this.Party.MobileParty.Objective;
				this.PartyBehaviorText = GameTexts.FindText("str_clan_party_behavior", null).ToString();
			}
			if (this._leader != null)
			{
				this.CharacterModel.FillFrom(this._leader, -1);
				CharacterViewModel characterModel = this.CharacterModel;
				IFaction mapFaction = this.Party.MapFaction;
				characterModel.ArmorColor1 = ((mapFaction != null) ? mapFaction.Color : 0U);
				CharacterViewModel characterModel2 = this.CharacterModel;
				IFaction mapFaction2 = this.Party.MapFaction;
				characterModel2.ArmorColor2 = ((mapFaction2 != null) ? mapFaction2.Color2 : 0U);
			}
			this.HeroMembers.Clear();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (TroopRosterElement troopRosterElement in this.Party.MemberRoster.GetTroopRoster())
			{
				Hero heroObject = troopRosterElement.Character.HeroObject;
				if (heroObject != null && heroObject.Clan == Clan.PlayerClan && heroObject.GovernorOf == null)
				{
					ClanPartyMemberItemVM clanPartyMemberItemVM = new ClanPartyMemberItemVM(troopRosterElement.Character.HeroObject, this.Party.MobileParty);
					this.HeroMembers.Add(clanPartyMemberItemVM);
					if (clanPartyMemberItemVM.IsLeader)
					{
						this.LeaderMember = clanPartyMemberItemVM;
					}
				}
				else if (troopRosterElement.Character.DefaultFormationClass.Equals(FormationClass.Infantry))
				{
					num += troopRosterElement.Number;
				}
				else if (troopRosterElement.Character.DefaultFormationClass.Equals(FormationClass.Ranged))
				{
					num2 += troopRosterElement.Number;
				}
				else if (troopRosterElement.Character.DefaultFormationClass.Equals(FormationClass.Cavalry))
				{
					num3 += troopRosterElement.Number;
				}
				else if (troopRosterElement.Character.DefaultFormationClass.Equals(FormationClass.HorseArcher))
				{
					num4 += troopRosterElement.Number;
				}
			}
			if (this._isLeaderTeleporting)
			{
				ClanPartyMemberItemVM clanPartyMemberItemVM2 = new ClanPartyMemberItemVM(this._leader.HeroObject, this.Party.MobileParty);
				this.LeaderMember = clanPartyMemberItemVM2;
				this.HeroMembers.Insert(0, clanPartyMemberItemVM2);
			}
			this.HasCompanion = (this.HeroMembers.Count > 1);
			if (this.IsMembersAndRolesVisible)
			{
				this.Roles.ApplyActionOnAllItems(delegate(ClanRoleItemVM x)
				{
					x.OnFinalize();
				});
				this.Roles.Clear();
				foreach (SkillEffect.PerkRole role in this.GetAssignablePartyRoles())
				{
					this.Roles.Add(new ClanRoleItemVM(this.Party.MobileParty, role, this.HeroMembers, new Action<ClanRoleItemVM>(this.OnRoleSelectionToggled), new Action(this.OnRoleAssigned)));
				}
			}
			this.InfantryCount = num;
			this.RangedCount = num2;
			this.CavalryCount = num3;
			this.HorseArcherCount = num4;
			ValueTuple<bool, TextObject> canUseActions = this.GetCanUseActions();
			this.CanUseActions = canUseActions.Item1;
			this.ActionsDisabledHint.HintText = (this.CanUseActions ? TextObject.Empty : canUseActions.Item2);
			if (!this.CanUseActions)
			{
				this.AutoRecruitmentHint.HintText = canUseActions.Item2;
				if (this.ExpenseItem != null)
				{
					this.ExpenseItem.IsEnabled = this.CanUseActions;
					this.ExpenseItem.WageLimitHint.HintText = canUseActions.Item2;
				}
				foreach (ClanRoleItemVM clanRoleItemVM in this.Roles)
				{
					clanRoleItemVM.SetEnabled(false, canUseActions.Item2);
				}
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005CD84 File Offset: 0x0005AF84
		private ValueTuple<bool, TextObject> GetCanUseActions()
		{
			if (Hero.MainHero.IsPrisoner)
			{
				return new ValueTuple<bool, TextObject>(false, GameTexts.FindText("str_action_disabled_reason_prisoner", null));
			}
			return new ValueTuple<bool, TextObject>(true, TextObject.Empty);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0005CDAF File Offset: 0x0005AFAF
		private void OnExpenseChange()
		{
			this._onExpenseChange();
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0005CDBC File Offset: 0x0005AFBC
		public void OnPartySelection()
		{
			int selectedIndex = this.IsPartyBehaviorEnabled ? this.PartyBehaviorSelector.SelectedIndex : -1;
			this._onAssignment(this);
			if (this.IsPartyBehaviorEnabled)
			{
				this.PartyBehaviorSelector.SelectedIndex = selectedIndex;
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0005CE00 File Offset: 0x0005B000
		public void ExecuteChangeLeader()
		{
			Action onShowChangeLeaderPopup = this._onShowChangeLeaderPopup;
			if (onShowChangeLeaderPopup == null)
			{
				return;
			}
			onShowChangeLeaderPopup();
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0005CE12 File Offset: 0x0005B012
		private void OnRoleAssigned()
		{
			this.Roles.ApplyActionOnAllItems(delegate(ClanRoleItemVM x)
			{
				x.Refresh();
			});
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005CE3E File Offset: 0x0005B03E
		private void ExecuteLocationLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0005CE50 File Offset: 0x0005B050
		private void UpdatePartyBehaviorSelectionUpdate(SelectorVM<SelectorItemVM> s)
		{
			if (s.SelectedIndex != (int)this.Party.MobileParty.Objective)
			{
				this.Party.MobileParty.SetPartyObjective((MobileParty.PartyObjective)s.SelectedIndex);
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0005CE80 File Offset: 0x0005B080
		private void OnAutoRecruitChanged(bool value)
		{
			if (this.Party.IsMobile && this.Party.MobileParty.IsGarrison)
			{
				Settlement homeSettlement = this.Party.MobileParty.HomeSettlement;
				if (((homeSettlement != null) ? homeSettlement.Town : null) != null)
				{
					this.Party.MobileParty.HomeSettlement.Town.GarrisonAutoRecruitmentIsEnabled = value;
				}
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0005CEE5 File Offset: 0x0005B0E5
		private IEnumerable<SkillEffect.PerkRole> GetAssignablePartyRoles()
		{
			yield return SkillEffect.PerkRole.Quartermaster;
			yield return SkillEffect.PerkRole.Scout;
			yield return SkillEffect.PerkRole.Surgeon;
			yield return SkillEffect.PerkRole.Engineer;
			yield break;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0005CEEE File Offset: 0x0005B0EE
		private void OnRoleSelectionToggled(ClanRoleItemVM role)
		{
			this.LastOpenedRoleSelection = role;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
		private static CharacterCode GetCharacterCode(CharacterObject character)
		{
			if (character.IsHero)
			{
				return CampaignUIHelper.GetCharacterCode(character, false);
			}
			uint color = Hero.MainHero.MapFaction.Color;
			uint color2 = Hero.MainHero.MapFaction.Color2;
			Equipment equipment = character.Equipment;
			string equipmentCode = (equipment != null) ? equipment.CalculateEquipmentCode() : null;
			BodyProperties bodyProperties = character.GetBodyProperties(character.Equipment, -1);
			return CharacterCode.CreateFrom(equipmentCode, bodyProperties, character.IsFemale, character.IsHero, color, color2, character.DefaultFormationClass, character.Race);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0005CF78 File Offset: 0x0005B178
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.HeroMembers.ApplyActionOnAllItems(delegate(ClanPartyMemberItemVM h)
			{
				h.OnFinalize();
			});
			this.Roles.ApplyActionOnAllItems(delegate(ClanRoleItemVM x)
			{
				x.OnFinalize();
			});
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x0005CFDF File Offset: 0x0005B1DF
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0005CFE7 File Offset: 0x0005B1E7
		[DataSourceProperty]
		public CharacterViewModel CharacterModel
		{
			get
			{
				return this._characterModel;
			}
			set
			{
				if (value != this._characterModel)
				{
					this._characterModel = value;
					base.OnPropertyChangedWithValue<CharacterViewModel>(value, "CharacterModel");
				}
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x0005D005 File Offset: 0x0005B205
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x0005D00D File Offset: 0x0005B20D
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> PartyBehaviorSelector
		{
			get
			{
				return this._partyBehaviorSelector;
			}
			set
			{
				if (value != this._partyBehaviorSelector)
				{
					this._partyBehaviorSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "PartyBehaviorSelector");
				}
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0005D02B File Offset: 0x0005B22B
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x0005D033 File Offset: 0x0005B233
		[DataSourceProperty]
		public ImageIdentifierVM LeaderVisual
		{
			get
			{
				return this._leaderVisual;
			}
			set
			{
				if (value != this._leaderVisual)
				{
					this._leaderVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "LeaderVisual");
				}
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0005D051 File Offset: 0x0005B251
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x0005D059 File Offset: 0x0005B259
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005D077 File Offset: 0x0005B277
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x0005D07F File Offset: 0x0005B27F
		[DataSourceProperty]
		public bool HasHeroMembers
		{
			get
			{
				return this._hasHeroMembers;
			}
			set
			{
				if (value != this._hasHeroMembers)
				{
					this._hasHeroMembers = value;
					base.OnPropertyChangedWithValue(value, "HasHeroMembers");
				}
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0005D09D File Offset: 0x0005B29D
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x0005D0A5 File Offset: 0x0005B2A5
		[DataSourceProperty]
		public bool IsClanRoleSelectionHighlightEnabled
		{
			get
			{
				return this._isClanRoleSelectionHighlightEnabled;
			}
			set
			{
				if (value != this._isClanRoleSelectionHighlightEnabled)
				{
					this._isClanRoleSelectionHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsClanRoleSelectionHighlightEnabled");
				}
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0005D0C3 File Offset: 0x0005B2C3
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x0005D0CB File Offset: 0x0005B2CB
		[DataSourceProperty]
		public bool IsDisbanding
		{
			get
			{
				return this._isDisbanding;
			}
			set
			{
				if (value != this._isDisbanding)
				{
					this._isDisbanding = value;
					base.OnPropertyChangedWithValue(value, "IsDisbanding");
				}
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0005D0E9 File Offset: 0x0005B2E9
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x0005D0F1 File Offset: 0x0005B2F1
		[DataSourceProperty]
		public bool IsInArmy
		{
			get
			{
				return this._isInArmy;
			}
			set
			{
				if (value != this._isInArmy)
				{
					this._isInArmy = value;
					base.OnPropertyChangedWithValue(value, "IsInArmy");
				}
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0005D10F File Offset: 0x0005B30F
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x0005D117 File Offset: 0x0005B317
		[DataSourceProperty]
		public bool CanUseActions
		{
			get
			{
				return this._canUseActions;
			}
			set
			{
				if (value != this._canUseActions)
				{
					this._canUseActions = value;
					base.OnPropertyChangedWithValue(value, "CanUseActions");
				}
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0005D135 File Offset: 0x0005B335
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x0005D13D File Offset: 0x0005B33D
		[DataSourceProperty]
		public bool IsChangeLeaderVisible
		{
			get
			{
				return this._isChangeLeaderVisible;
			}
			set
			{
				if (value != this._isChangeLeaderVisible)
				{
					this._isChangeLeaderVisible = value;
					base.OnPropertyChangedWithValue(value, "IsChangeLeaderVisible");
				}
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0005D15B File Offset: 0x0005B35B
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x0005D163 File Offset: 0x0005B363
		[DataSourceProperty]
		public bool IsChangeLeaderEnabled
		{
			get
			{
				return this._isChangeLeaderEnabled;
			}
			set
			{
				if (value != this._isChangeLeaderEnabled)
				{
					this._isChangeLeaderEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsChangeLeaderEnabled");
				}
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0005D181 File Offset: 0x0005B381
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x0005D189 File Offset: 0x0005B389
		[DataSourceProperty]
		public HintViewModel ActionsDisabledHint
		{
			get
			{
				return this._actionsDisabledHint;
			}
			set
			{
				if (value != this._actionsDisabledHint)
				{
					this._actionsDisabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ActionsDisabledHint");
				}
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0005D1A7 File Offset: 0x0005B3A7
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x0005D1AF File Offset: 0x0005B3AF
		[DataSourceProperty]
		public bool IsCaravan
		{
			get
			{
				return this._isCaravan;
			}
			set
			{
				if (value != this._isCaravan)
				{
					this._isCaravan = value;
					base.OnPropertyChangedWithValue(value, "IsCaravan");
				}
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0005D1CD File Offset: 0x0005B3CD
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x0005D1D5 File Offset: 0x0005B3D5
		[DataSourceProperty]
		public bool ShouldPartyHaveExpense
		{
			get
			{
				return this._shouldPartyHaveExpense;
			}
			set
			{
				if (value != this._shouldPartyHaveExpense)
				{
					this._shouldPartyHaveExpense = value;
					base.OnPropertyChangedWithValue(value, "ShouldPartyHaveExpense");
				}
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0005D1F3 File Offset: 0x0005B3F3
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x0005D1FB File Offset: 0x0005B3FB
		[DataSourceProperty]
		public bool HasCompanion
		{
			get
			{
				return this._hasCompanion;
			}
			set
			{
				if (value != this._hasCompanion)
				{
					this._hasCompanion = value;
					base.OnPropertyChangedWithValue(value, "HasCompanion");
				}
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0005D219 File Offset: 0x0005B419
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x0005D221 File Offset: 0x0005B421
		[DataSourceProperty]
		public bool IsAutoRecruitmentVisible
		{
			get
			{
				return this._isAutoRecruitmentVisible;
			}
			set
			{
				if (value != this._isAutoRecruitmentVisible)
				{
					this._isAutoRecruitmentVisible = value;
					base.OnPropertyChangedWithValue(value, "IsAutoRecruitmentVisible");
				}
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0005D23F File Offset: 0x0005B43F
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x0005D247 File Offset: 0x0005B447
		[DataSourceProperty]
		public bool AutoRecruitmentValue
		{
			get
			{
				return this._autoRecruitmentValue;
			}
			set
			{
				if (value != this._autoRecruitmentValue)
				{
					this._autoRecruitmentValue = value;
					base.OnPropertyChangedWithValue(value, "AutoRecruitmentValue");
					this.OnAutoRecruitChanged(value);
				}
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0005D26C File Offset: 0x0005B46C
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x0005D274 File Offset: 0x0005B474
		[DataSourceProperty]
		public bool IsPartyBehaviorEnabled
		{
			get
			{
				return this._isPartyBehaviorEnabled;
			}
			set
			{
				if (value != this._isPartyBehaviorEnabled)
				{
					this._isPartyBehaviorEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPartyBehaviorEnabled");
				}
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0005D292 File Offset: 0x0005B492
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x0005D29A File Offset: 0x0005B49A
		[DataSourceProperty]
		public bool IsMembersAndRolesVisible
		{
			get
			{
				return this._isMembersAndRolesVisible;
			}
			set
			{
				if (value != this._isMembersAndRolesVisible)
				{
					this._isMembersAndRolesVisible = value;
					base.OnPropertyChangedWithValue(value, "IsMembersAndRolesVisible");
				}
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x0005D2C0 File Offset: 0x0005B4C0
		[DataSourceProperty]
		public bool IsMainHeroParty
		{
			get
			{
				return this._isMainHeroParty;
			}
			set
			{
				if (value != this._isMainHeroParty)
				{
					this._isMainHeroParty = value;
					base.OnPropertyChangedWithValue(value, "IsMainHeroParty");
				}
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0005D2DE File Offset: 0x0005B4DE
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x0005D2E6 File Offset: 0x0005B4E6
		[DataSourceProperty]
		public ClanFinanceExpenseItemVM ExpenseItem
		{
			get
			{
				return this._expenseItem;
			}
			set
			{
				if (value != this._expenseItem)
				{
					this._expenseItem = value;
					base.OnPropertyChangedWithValue<ClanFinanceExpenseItemVM>(value, "ExpenseItem");
				}
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0005D304 File Offset: 0x0005B504
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0005D30C File Offset: 0x0005B50C
		[DataSourceProperty]
		public ClanRoleItemVM LastOpenedRoleSelection
		{
			get
			{
				return this._lastOpenedRoleSelection;
			}
			set
			{
				if (value != this._lastOpenedRoleSelection)
				{
					this._lastOpenedRoleSelection = value;
					base.OnPropertyChangedWithValue<ClanRoleItemVM>(value, "LastOpenedRoleSelection");
				}
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0005D32A File Offset: 0x0005B52A
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x0005D332 File Offset: 0x0005B532
		[DataSourceProperty]
		public ClanPartyMemberItemVM LeaderMember
		{
			get
			{
				return this._leaderMember;
			}
			set
			{
				if (value != this._leaderMember)
				{
					this._leaderMember = value;
					base.OnPropertyChangedWithValue<ClanPartyMemberItemVM>(value, "LeaderMember");
				}
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0005D350 File Offset: 0x0005B550
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x0005D358 File Offset: 0x0005B558
		[DataSourceProperty]
		public string PartySizeText
		{
			get
			{
				return this._partySizeText;
			}
			set
			{
				if (value != this._partySizeText)
				{
					this._partySizeText = value;
					base.OnPropertyChanged("PartyStrengthText");
				}
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0005D37A File Offset: 0x0005B57A
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x0005D382 File Offset: 0x0005B582
		[DataSourceProperty]
		public string MembersText
		{
			get
			{
				return this._membersText;
			}
			set
			{
				if (value != null)
				{
					this._membersText = value;
					base.OnPropertyChangedWithValue<string>(value, "MembersText");
				}
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0005D39A File Offset: 0x0005B59A
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x0005D3A2 File Offset: 0x0005B5A2
		[DataSourceProperty]
		public string AssigneesText
		{
			get
			{
				return this._assigneesText;
			}
			set
			{
				if (value != this._assigneesText)
				{
					this._assigneesText = value;
					base.OnPropertyChangedWithValue<string>(value, "AssigneesText");
				}
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0005D3C5 File Offset: 0x0005B5C5
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0005D3CD File Offset: 0x0005B5CD
		[DataSourceProperty]
		public string RolesText
		{
			get
			{
				return this._rolesText;
			}
			set
			{
				if (value != this._rolesText)
				{
					this._rolesText = value;
					base.OnPropertyChangedWithValue<string>(value, "RolesText");
				}
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0005D3F0 File Offset: 0x0005B5F0
		// (set) Token: 0x060019AF RID: 6575 RVA: 0x0005D3F8 File Offset: 0x0005B5F8
		[DataSourceProperty]
		public string PartyLeaderRoleEffectsText
		{
			get
			{
				return this._partyLeaderRoleEffectsText;
			}
			set
			{
				if (value != this._partyLeaderRoleEffectsText)
				{
					this._partyLeaderRoleEffectsText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyLeaderRoleEffectsText");
				}
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0005D41B File Offset: 0x0005B61B
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x0005D423 File Offset: 0x0005B623
		[DataSourceProperty]
		public string PartyLocationText
		{
			get
			{
				return this._partyLocationText;
			}
			set
			{
				if (value != this._partyLocationText)
				{
					this._partyLocationText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyLocationText");
				}
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0005D446 File Offset: 0x0005B646
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0005D44E File Offset: 0x0005B64E
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

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0005D471 File Offset: 0x0005B671
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x0005D479 File Offset: 0x0005B679
		[DataSourceProperty]
		public string PartySizeSubTitleText
		{
			get
			{
				return this._partySizeSubTitleText;
			}
			set
			{
				if (value != this._partySizeSubTitleText)
				{
					this._partySizeSubTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartySizeSubTitleText");
				}
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0005D49C File Offset: 0x0005B69C
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x0005D4A4 File Offset: 0x0005B6A4
		[DataSourceProperty]
		public string PartyWageSubTitleText
		{
			get
			{
				return this._partyWageSubTitleText;
			}
			set
			{
				if (value != this._partyWageSubTitleText)
				{
					this._partyWageSubTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyWageSubTitleText");
				}
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0005D4C7 File Offset: 0x0005B6C7
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x0005D4CF File Offset: 0x0005B6CF
		[DataSourceProperty]
		public string PartyBehaviorText
		{
			get
			{
				return this._partyBehaviorText;
			}
			set
			{
				if (value != this._partyBehaviorText)
				{
					this._partyBehaviorText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyBehaviorText");
				}
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0005D4F2 File Offset: 0x0005B6F2
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x0005D4FA File Offset: 0x0005B6FA
		[DataSourceProperty]
		public int InfantryCount
		{
			get
			{
				return this._infantryCount;
			}
			set
			{
				if (value != this._infantryCount)
				{
					this._infantryCount = value;
					base.OnPropertyChangedWithValue(value, "InfantryCount");
				}
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0005D518 File Offset: 0x0005B718
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x0005D520 File Offset: 0x0005B720
		[DataSourceProperty]
		public int RangedCount
		{
			get
			{
				return this._rangedCount;
			}
			set
			{
				if (value != this._rangedCount)
				{
					this._rangedCount = value;
					base.OnPropertyChangedWithValue(value, "RangedCount");
				}
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x0005D53E File Offset: 0x0005B73E
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x0005D546 File Offset: 0x0005B746
		[DataSourceProperty]
		public int CavalryCount
		{
			get
			{
				return this._cavalryCount;
			}
			set
			{
				if (value != this._cavalryCount)
				{
					this._cavalryCount = value;
					base.OnPropertyChangedWithValue(value, "CavalryCount");
				}
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0005D564 File Offset: 0x0005B764
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x0005D56C File Offset: 0x0005B76C
		[DataSourceProperty]
		public int HorseArcherCount
		{
			get
			{
				return this._horseArcherCount;
			}
			set
			{
				if (value != this._horseArcherCount)
				{
					this._horseArcherCount = value;
					base.OnPropertyChangedWithValue(value, "HorseArcherCount");
				}
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0005D58A File Offset: 0x0005B78A
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x0005D592 File Offset: 0x0005B792
		[DataSourceProperty]
		public string InArmyText
		{
			get
			{
				return this._inArmyText;
			}
			set
			{
				if (value != this._inArmyText)
				{
					this._inArmyText = value;
					base.OnPropertyChangedWithValue<string>(value, "InArmyText");
				}
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0005D5B5 File Offset: 0x0005B7B5
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0005D5BD File Offset: 0x0005B7BD
		[DataSourceProperty]
		public string DisbandingText
		{
			get
			{
				return this._disbandingText;
			}
			set
			{
				if (value != this._disbandingText)
				{
					this._disbandingText = value;
					base.OnPropertyChangedWithValue<string>(value, "DisbandingText");
				}
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x0005D5E8 File Offset: 0x0005B7E8
		[DataSourceProperty]
		public string AutoRecruitmentText
		{
			get
			{
				return this._autoRecruitmentText;
			}
			set
			{
				if (value != this._autoRecruitmentText)
				{
					this._autoRecruitmentText = value;
					base.OnPropertyChangedWithValue<string>(value, "AutoRecruitmentText");
				}
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x0005D60B File Offset: 0x0005B80B
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x0005D613 File Offset: 0x0005B813
		[DataSourceProperty]
		public HintViewModel AutoRecruitmentHint
		{
			get
			{
				return this._autoRecruitmentHint;
			}
			set
			{
				if (value != this._autoRecruitmentHint)
				{
					this._autoRecruitmentHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AutoRecruitmentHint");
				}
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x0005D631 File Offset: 0x0005B831
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x0005D639 File Offset: 0x0005B839
		[DataSourceProperty]
		public HintViewModel InArmyHint
		{
			get
			{
				return this._inArmyHint;
			}
			set
			{
				if (value != this._inArmyHint)
				{
					this._inArmyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "InArmyHint");
				}
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x0005D657 File Offset: 0x0005B857
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x0005D65F File Offset: 0x0005B85F
		[DataSourceProperty]
		public HintViewModel ChangeLeaderHint
		{
			get
			{
				return this._changeLeaderHint;
			}
			set
			{
				if (value != this._changeLeaderHint)
				{
					this._changeLeaderHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ChangeLeaderHint");
				}
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x0005D67D File Offset: 0x0005B87D
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x0005D685 File Offset: 0x0005B885
		[DataSourceProperty]
		public BasicTooltipViewModel InfantryHint
		{
			get
			{
				return this._infantryHint;
			}
			set
			{
				if (value != this._infantryHint)
				{
					this._infantryHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "InfantryHint");
				}
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0005D6A3 File Offset: 0x0005B8A3
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0005D6AB File Offset: 0x0005B8AB
		[DataSourceProperty]
		public BasicTooltipViewModel RangedHint
		{
			get
			{
				return this._rangedHint;
			}
			set
			{
				if (value != this._rangedHint)
				{
					this._rangedHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "RangedHint");
				}
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0005D6C9 File Offset: 0x0005B8C9
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0005D6D1 File Offset: 0x0005B8D1
		[DataSourceProperty]
		public BasicTooltipViewModel CavalryHint
		{
			get
			{
				return this._cavalryHint;
			}
			set
			{
				if (value != this._cavalryHint)
				{
					this._cavalryHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CavalryHint");
				}
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0005D6EF File Offset: 0x0005B8EF
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x0005D6F7 File Offset: 0x0005B8F7
		[DataSourceProperty]
		public BasicTooltipViewModel HorseArcherHint
		{
			get
			{
				return this._horseArcherHint;
			}
			set
			{
				if (value != this._horseArcherHint)
				{
					this._horseArcherHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "HorseArcherHint");
				}
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0005D715 File Offset: 0x0005B915
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x0005D71D File Offset: 0x0005B91D
		[DataSourceProperty]
		public MBBindingList<ClanPartyMemberItemVM> HeroMembers
		{
			get
			{
				return this._heroMembers;
			}
			set
			{
				if (value != this._heroMembers)
				{
					this._heroMembers = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyMemberItemVM>>(value, "HeroMembers");
				}
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0005D73B File Offset: 0x0005B93B
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x0005D743 File Offset: 0x0005B943
		[DataSourceProperty]
		public MBBindingList<ClanRoleItemVM> Roles
		{
			get
			{
				return this._roles;
			}
			set
			{
				if (value != this._roles)
				{
					this._roles = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanRoleItemVM>>(value, "Roles");
				}
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0005D764 File Offset: 0x0005B964
		private List<TooltipProperty> GetPartyTroopInfo(PartyBase party, FormationClass formationClass)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty("", GameTexts.FindText("str_formation_class_string", formationClass.GetName()).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			foreach (TroopRosterElement troopRosterElement in this.Party.MemberRoster.GetTroopRoster())
			{
				if (!troopRosterElement.Character.IsHero && troopRosterElement.Character.DefaultFormationClass.Equals(formationClass))
				{
					list.Add(new TooltipProperty(troopRosterElement.Character.Name.ToString(), troopRosterElement.Number.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return list;
		}

		// Token: 0x04000C04 RID: 3076
		private readonly Action<ClanPartyItemVM> _onAssignment;

		// Token: 0x04000C05 RID: 3077
		private readonly Action _onExpenseChange;

		// Token: 0x04000C06 RID: 3078
		private readonly Action _onShowChangeLeaderPopup;

		// Token: 0x04000C07 RID: 3079
		private readonly ClanPartyItemVM.ClanPartyType _type;

		// Token: 0x04000C08 RID: 3080
		private readonly TextObject _changeLeaderHintText = GameTexts.FindText("str_change_party_leader", null);

		// Token: 0x04000C09 RID: 3081
		private readonly IDisbandPartyCampaignBehavior _disbandBehavior;

		// Token: 0x04000C0A RID: 3082
		private readonly bool _isLeaderTeleporting;

		// Token: 0x04000C0C RID: 3084
		private readonly CharacterObject _leader;

		// Token: 0x04000C0D RID: 3085
		private SelectorVM<SelectorItemVM> _partyBehaviorSelector;

		// Token: 0x04000C0E RID: 3086
		private ClanFinanceExpenseItemVM _expenseItem;

		// Token: 0x04000C0F RID: 3087
		private ClanRoleItemVM _lastOpenedRoleSelection;

		// Token: 0x04000C10 RID: 3088
		private ClanPartyMemberItemVM _leaderMember;

		// Token: 0x04000C11 RID: 3089
		private ImageIdentifierVM _leaderVisual;

		// Token: 0x04000C12 RID: 3090
		private bool _isMainHeroParty;

		// Token: 0x04000C13 RID: 3091
		private bool _isSelected;

		// Token: 0x04000C14 RID: 3092
		private bool _hasHeroMembers;

		// Token: 0x04000C15 RID: 3093
		private string _partyLocationText;

		// Token: 0x04000C16 RID: 3094
		private string _partySizeText;

		// Token: 0x04000C17 RID: 3095
		private string _membersText;

		// Token: 0x04000C18 RID: 3096
		private string _assigneesText;

		// Token: 0x04000C19 RID: 3097
		private string _rolesText;

		// Token: 0x04000C1A RID: 3098
		private string _partyLeaderRoleEffectsText;

		// Token: 0x04000C1B RID: 3099
		private string _name;

		// Token: 0x04000C1C RID: 3100
		private string _partySizeSubTitleText;

		// Token: 0x04000C1D RID: 3101
		private string _partyWageSubTitleText;

		// Token: 0x04000C1E RID: 3102
		private string _partyBehaviorText;

		// Token: 0x04000C1F RID: 3103
		private int _infantryCount;

		// Token: 0x04000C20 RID: 3104
		private int _rangedCount;

		// Token: 0x04000C21 RID: 3105
		private int _cavalryCount;

		// Token: 0x04000C22 RID: 3106
		private int _horseArcherCount;

		// Token: 0x04000C23 RID: 3107
		private string _inArmyText;

		// Token: 0x04000C24 RID: 3108
		private string _disbandingText;

		// Token: 0x04000C25 RID: 3109
		private string _autoRecruitmentText;

		// Token: 0x04000C26 RID: 3110
		private bool _autoRecruitmentValue;

		// Token: 0x04000C27 RID: 3111
		private bool _isAutoRecruitmentVisible;

		// Token: 0x04000C28 RID: 3112
		private bool _shouldPartyHaveExpense;

		// Token: 0x04000C29 RID: 3113
		private bool _hasCompanion;

		// Token: 0x04000C2A RID: 3114
		private bool _isPartyBehaviorEnabled;

		// Token: 0x04000C2B RID: 3115
		private bool _isMembersAndRolesVisible;

		// Token: 0x04000C2C RID: 3116
		private bool _isCaravan;

		// Token: 0x04000C2D RID: 3117
		private bool _isDisbanding;

		// Token: 0x04000C2E RID: 3118
		private bool _isInArmy;

		// Token: 0x04000C2F RID: 3119
		private bool _canUseActions;

		// Token: 0x04000C30 RID: 3120
		private bool _isChangeLeaderVisible;

		// Token: 0x04000C31 RID: 3121
		private bool _isChangeLeaderEnabled;

		// Token: 0x04000C32 RID: 3122
		private bool _isClanRoleSelectionHighlightEnabled;

		// Token: 0x04000C33 RID: 3123
		private HintViewModel _actionsDisabledHint;

		// Token: 0x04000C34 RID: 3124
		private CharacterViewModel _characterModel;

		// Token: 0x04000C35 RID: 3125
		private HintViewModel _autoRecruitmentHint;

		// Token: 0x04000C36 RID: 3126
		private HintViewModel _inArmyHint;

		// Token: 0x04000C37 RID: 3127
		private HintViewModel _changeLeaderHint;

		// Token: 0x04000C38 RID: 3128
		private BasicTooltipViewModel _infantryHint;

		// Token: 0x04000C39 RID: 3129
		private BasicTooltipViewModel _rangedHint;

		// Token: 0x04000C3A RID: 3130
		private BasicTooltipViewModel _cavalryHint;

		// Token: 0x04000C3B RID: 3131
		private BasicTooltipViewModel _horseArcherHint;

		// Token: 0x04000C3C RID: 3132
		private MBBindingList<ClanPartyMemberItemVM> _heroMembers;

		// Token: 0x04000C3D RID: 3133
		private MBBindingList<ClanRoleItemVM> _roles;

		// Token: 0x02000244 RID: 580
		public enum ClanPartyType
		{
			// Token: 0x04001153 RID: 4435
			Main,
			// Token: 0x04001154 RID: 4436
			Member,
			// Token: 0x04001155 RID: 4437
			Caravan,
			// Token: 0x04001156 RID: 4438
			Garrison
		}
	}
}
