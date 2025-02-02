using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200011A RID: 282
	public class ClanFiefsVM : ViewModel
	{
		// Token: 0x06001AFA RID: 6906 RVA: 0x0006183C File Offset: 0x0005FA3C
		public ClanFiefsVM(Action onRefresh, Action<ClanCardSelectionInfo> openCardSelectionPopup)
		{
			this._onRefresh = onRefresh;
			this._clan = Hero.MainHero.Clan;
			this._openCardSelectionPopup = openCardSelectionPopup;
			this._teleportationBehavior = Campaign.Current.GetCampaignBehavior<ITeleportationCampaignBehavior>();
			this.Settlements = new MBBindingList<ClanSettlementItemVM>();
			this.Castles = new MBBindingList<ClanSettlementItemVM>();
			List<MBBindingList<ClanSettlementItemVM>> listsToControl = new List<MBBindingList<ClanSettlementItemVM>>
			{
				this.Settlements,
				this.Castles
			};
			this.SortController = new ClanFiefsSortControllerVM(listsToControl);
			this.RefreshAllLists();
			this.RefreshValues();
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000618DC File Offset: 0x0005FADC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TaxText = GameTexts.FindText("str_tax", null).ToString();
			this.GovernorText = GameTexts.FindText("str_notable_governor", null).ToString();
			this.ProfitText = GameTexts.FindText("str_profit", null).ToString();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.NoFiefsText = GameTexts.FindText("str_clan_no_fiefs", null).ToString();
			this.NoGovernorText = this._noGovernorTextSource.ToString();
			this.Settlements.ApplyActionOnAllItems(delegate(ClanSettlementItemVM x)
			{
				x.RefreshValues();
			});
			this.Castles.ApplyActionOnAllItems(delegate(ClanSettlementItemVM x)
			{
				x.RefreshValues();
			});
			ClanSettlementItemVM currentSelectedFief = this.CurrentSelectedFief;
			if (currentSelectedFief != null)
			{
				currentSelectedFief.RefreshValues();
			}
			this.SortController.RefreshValues();
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000619DE File Offset: 0x0005FBDE
		public override void OnFinalize()
		{
			base.OnFinalize();
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000619E8 File Offset: 0x0005FBE8
		public void RefreshAllLists()
		{
			this.Settlements.Clear();
			this.Castles.Clear();
			this.SortController.ResetAllStates();
			foreach (Settlement settlement in this._clan.Settlements)
			{
				if (settlement.IsTown)
				{
					this.Settlements.Add(new ClanSettlementItemVM(settlement, new Action<ClanSettlementItemVM>(this.OnFiefSelection), new Action(this.OnShowSendMembers), this._teleportationBehavior));
				}
				else if (settlement.IsCastle)
				{
					this.Castles.Add(new ClanSettlementItemVM(settlement, new Action<ClanSettlementItemVM>(this.OnFiefSelection), new Action(this.OnShowSendMembers), this._teleportationBehavior));
				}
			}
			GameTexts.SetVariable("RANK", GameTexts.FindText("str_towns", null));
			GameTexts.SetVariable("NUMBER", this.Settlements.Count);
			this.TownsText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			GameTexts.SetVariable("RANK", GameTexts.FindText("str_castles", null));
			GameTexts.SetVariable("NUMBER", this.Castles.Count);
			this.CastlesText = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
			this.OnFiefSelection(this.GetDefaultMember());
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00061B5C File Offset: 0x0005FD5C
		private ClanSettlementItemVM GetDefaultMember()
		{
			if (!this.Settlements.IsEmpty<ClanSettlementItemVM>())
			{
				return this.Settlements.FirstOrDefault<ClanSettlementItemVM>();
			}
			return this.Castles.FirstOrDefault<ClanSettlementItemVM>();
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00061B84 File Offset: 0x0005FD84
		public void SelectFief(Settlement settlement)
		{
			foreach (ClanSettlementItemVM clanSettlementItemVM in this.Settlements)
			{
				if (clanSettlementItemVM.Settlement == settlement)
				{
					this.OnFiefSelection(clanSettlementItemVM);
					break;
				}
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00061BDC File Offset: 0x0005FDDC
		private void OnFiefSelection(ClanSettlementItemVM fief)
		{
			if (this.CurrentSelectedFief != null)
			{
				this.CurrentSelectedFief.IsSelected = false;
			}
			this.CurrentSelectedFief = fief;
			TextObject hintText;
			this.CanChangeGovernorOfCurrentFief = this.GetCanChangeGovernor(out hintText);
			this.GovernorActionHint = new HintViewModel(hintText, null);
			if (fief != null)
			{
				fief.IsSelected = true;
				this.GovernorActionText = (fief.HasGovernor ? GameTexts.FindText("str_clan_change_governor", null).ToString() : GameTexts.FindText("str_clan_assign_governor", null).ToString());
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00061C5C File Offset: 0x0005FE5C
		private bool GetCanChangeGovernor(out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			ClanSettlementItemVM currentSelectedFief = this.CurrentSelectedFief;
			bool flag;
			if (currentSelectedFief == null)
			{
				flag = false;
			}
			else
			{
				HeroVM governor = currentSelectedFief.Governor;
				bool? flag2;
				if (governor == null)
				{
					flag2 = null;
				}
				else
				{
					Hero hero = governor.Hero;
					flag2 = ((hero != null) ? new bool?(hero.IsTraveling) : null);
				}
				bool? flag3 = flag2;
				bool flag4 = true;
				flag = (flag3.GetValueOrDefault() == flag4 & flag3 != null);
			}
			if (flag)
			{
				disabledReason = new TextObject("{=qbqimqMb}{GOVERNOR.NAME} is on the way to be the new governor of {SETTLEMENT_NAME}", null);
				if (this.CurrentSelectedFief.Governor.Hero.CharacterObject != null)
				{
					StringHelpers.SetCharacterProperties("GOVERNOR", this.CurrentSelectedFief.Governor.Hero.CharacterObject, disabledReason, false);
				}
				TextObject textObject2 = disabledReason;
				string tag = "SETTLEMENT_NAME";
				Settlement settlement = this.CurrentSelectedFief.Settlement;
				string text;
				if (settlement == null)
				{
					text = null;
				}
				else
				{
					TextObject name = settlement.Name;
					text = ((name != null) ? name.ToString() : null);
				}
				textObject2.SetTextVariable(tag, text ?? string.Empty);
				return false;
			}
			ClanSettlementItemVM currentSelectedFief2 = this.CurrentSelectedFief;
			if (((currentSelectedFief2 != null) ? currentSelectedFief2.Settlement.Town : null) == null)
			{
				disabledReason = TextObject.Empty;
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00061D7C File Offset: 0x0005FF7C
		public void ExecuteAssignGovernor()
		{
			ClanSettlementItemVM currentSelectedFief = this.CurrentSelectedFief;
			bool flag;
			if (currentSelectedFief == null)
			{
				flag = (null != null);
			}
			else
			{
				Settlement settlement = currentSelectedFief.Settlement;
				flag = (((settlement != null) ? settlement.Town : null) != null);
			}
			if (flag)
			{
				ClanCardSelectionInfo obj = new ClanCardSelectionInfo(GameTexts.FindText("str_clan_assign_governor", null).CopyTextObject(), this.GetGovernorCandidates(), new Action<List<object>, Action>(this.OnGovernorSelectionOver), false);
				Action<ClanCardSelectionInfo> openCardSelectionPopup = this._openCardSelectionPopup;
				if (openCardSelectionPopup == null)
				{
					return;
				}
				openCardSelectionPopup(obj);
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00061DE4 File Offset: 0x0005FFE4
		private IEnumerable<ClanCardSelectionItemInfo> GetGovernorCandidates()
		{
			yield return new ClanCardSelectionItemInfo(this._noGovernorTextSource.CopyTextObject(), false, null, null);
			foreach (Hero hero in (from h in this._clan.Heroes
			where !h.IsDisabled
			select h).Union(this._clan.Companions))
			{
				if ((hero.IsActive || hero.IsTraveling) && !hero.IsChild && hero != Hero.MainHero)
				{
					Hero hero2 = hero;
					HeroVM governor = this.CurrentSelectedFief.Governor;
					if (hero2 != ((governor != null) ? governor.Hero : null) && hero.CanBeGovernorOrHavePartyRole())
					{
						TextObject disabledReason;
						bool flag = FactionHelper.IsMainClanMemberAvailableForSendingSettlementAsGovernor(hero, this.GetSettlementOfGovernor(hero), out disabledReason);
						SkillObject charm = DefaultSkills.Charm;
						int skillValue = hero.GetSkillValue(charm);
						ImageIdentifier image = new ImageIdentifier(CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false));
						yield return new ClanCardSelectionItemInfo(hero, hero.Name, image, CardSelectionItemSpriteType.Skill, charm.StringId.ToLower(), skillValue.ToString(), this.GetGovernorCandidateProperties(hero), !flag, disabledReason, null);
					}
				}
			}
			IEnumerator<Hero> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00061DF4 File Offset: 0x0005FFF4
		private IEnumerable<ClanCardSelectionItemPropertyInfo> GetGovernorCandidateProperties(Hero hero)
		{
			GameTexts.SetVariable("newline", "\n");
			TextObject teleportationDelayText = CampaignUIHelper.GetTeleportationDelayText(hero, this.CurrentSelectedFief.Settlement.Party);
			yield return new ClanCardSelectionItemPropertyInfo(teleportationDelayText);
			ValueTuple<TextObject, TextObject> governorEngineeringSkillEffectForHero = PerkHelper.GetGovernorEngineeringSkillEffectForHero(hero);
			yield return new ClanCardSelectionItemPropertyInfo(new TextObject("{=J8ddrAOf}Governor Effects", null), governorEngineeringSkillEffectForHero.Item2);
			List<PerkObject> governorPerksForHero = PerkHelper.GetGovernorPerksForHero(hero);
			TextObject value = new TextObject("{=oSfsqBwJ}No perks", null);
			int num = 0;
			foreach (PerkObject perkObject in governorPerksForHero)
			{
				bool flag = perkObject.PrimaryRole == SkillEffect.PerkRole.Governor;
				bool flag2 = perkObject.SecondaryRole == SkillEffect.PerkRole.Governor;
				if (flag)
				{
					TextObject perkText = ClanCardSelectionItemPropertyInfo.CreateLabeledValueText(perkObject.Name, perkObject.PrimaryDescription);
					this.SetPerksPropertyText(perkText, ref value, ref num);
				}
				if (flag2)
				{
					TextObject perkText2 = ClanCardSelectionItemPropertyInfo.CreateLabeledValueText(perkObject.Name, perkObject.SecondaryDescription);
					this.SetPerksPropertyText(perkText2, ref value, ref num);
				}
			}
			yield return new ClanCardSelectionItemPropertyInfo(GameTexts.FindText("str_clan_governor_perks", null), value);
			yield break;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00061E0C File Offset: 0x0006000C
		private void SetPerksPropertyText(TextObject perkText, ref TextObject perksPropertyText, ref int addedPerkCount)
		{
			if (addedPerkCount == 0)
			{
				perksPropertyText = perkText;
			}
			else
			{
				TextObject textObject = GameTexts.FindText("str_string_newline_newline_string", null);
				textObject.SetTextVariable("STR1", perksPropertyText);
				textObject.SetTextVariable("STR2", perkText);
				perksPropertyText = textObject;
			}
			addedPerkCount++;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00061E54 File Offset: 0x00060054
		private void OnGovernorSelectionOver(List<object> selectedItems, Action closePopup)
		{
			if (selectedItems.Count == 1)
			{
				ClanSettlementItemVM currentSelectedFief = this.CurrentSelectedFief;
				Hero hero;
				if (currentSelectedFief == null)
				{
					hero = null;
				}
				else
				{
					HeroVM governor = currentSelectedFief.Governor;
					hero = ((governor != null) ? governor.Hero : null);
				}
				Hero hero2 = hero;
				Hero newGovernor = selectedItems.FirstOrDefault<object>() as Hero;
				bool isRemoveGovernor = newGovernor == null;
				if (!isRemoveGovernor || hero2 != null)
				{
					ValueTuple<TextObject, TextObject> governorSelectionConfirmationPopupTexts = CampaignUIHelper.GetGovernorSelectionConfirmationPopupTexts(hero2, newGovernor, this.CurrentSelectedFief.Settlement);
					InformationManager.ShowInquiry(new InquiryData(governorSelectionConfirmationPopupTexts.Item1.ToString(), governorSelectionConfirmationPopupTexts.Item2.ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
					{
						Action closePopup4 = closePopup;
						if (closePopup4 != null)
						{
							closePopup4();
						}
						if (isRemoveGovernor)
						{
							ChangeGovernorAction.RemoveGovernorOfIfExists(this.CurrentSelectedFief.Settlement.Town);
						}
						else
						{
							ChangeGovernorAction.Apply(this.CurrentSelectedFief.Settlement.Town, newGovernor);
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
				return;
			}
			else
			{
				Action closePopup3 = closePopup;
				if (closePopup3 == null)
				{
					return;
				}
				closePopup3();
				return;
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00061F78 File Offset: 0x00060178
		private Settlement GetSettlementOfGovernor(Hero hero)
		{
			foreach (ClanSettlementItemVM clanSettlementItemVM in this.Settlements)
			{
				Hero hero2;
				if (clanSettlementItemVM == null)
				{
					hero2 = null;
				}
				else
				{
					HeroVM governor = clanSettlementItemVM.Governor;
					hero2 = ((governor != null) ? governor.Hero : null);
				}
				if (hero2 == hero)
				{
					return clanSettlementItemVM.Settlement;
				}
			}
			foreach (ClanSettlementItemVM clanSettlementItemVM2 in this.Castles)
			{
				Hero hero3;
				if (clanSettlementItemVM2 == null)
				{
					hero3 = null;
				}
				else
				{
					HeroVM governor2 = clanSettlementItemVM2.Governor;
					hero3 = ((governor2 != null) ? governor2.Hero : null);
				}
				if (hero3 == hero)
				{
					return clanSettlementItemVM2.Settlement;
				}
			}
			return null;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00062040 File Offset: 0x00060240
		private void OnShowSendMembers()
		{
			ClanSettlementItemVM currentSelectedFief = this.CurrentSelectedFief;
			Settlement settlement = (currentSelectedFief != null) ? currentSelectedFief.Settlement : null;
			if (settlement != null)
			{
				TextObject textObject = GameTexts.FindText("str_send_members", null);
				textObject.SetTextVariable("SETTLEMENT_NAME", settlement.Name);
				ClanCardSelectionInfo obj = new ClanCardSelectionInfo(textObject, this.GetSendMembersCandidates(), new Action<List<object>, Action>(this.OnSendMembersSelectionOver), true);
				Action<ClanCardSelectionInfo> openCardSelectionPopup = this._openCardSelectionPopup;
				if (openCardSelectionPopup == null)
				{
					return;
				}
				openCardSelectionPopup(obj);
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000620AD File Offset: 0x000602AD
		private IEnumerable<ClanCardSelectionItemInfo> GetSendMembersCandidates()
		{
			foreach (Hero hero in (from h in this._clan.Heroes
			where !h.IsDisabled
			select h).Union(this._clan.Companions))
			{
				if ((hero.IsActive || hero.IsTraveling) && (hero.CurrentSettlement != this.CurrentSelectedFief.Settlement || hero.PartyBelongedTo != null) && !hero.IsChild && hero != Hero.MainHero)
				{
					TextObject disabledReason;
					bool flag = FactionHelper.IsMainClanMemberAvailableForSendingSettlement(hero, this.CurrentSelectedFief.Settlement, out disabledReason);
					SkillObject charm = DefaultSkills.Charm;
					int skillValue = hero.GetSkillValue(charm);
					ImageIdentifier image = new ImageIdentifier(CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false));
					yield return new ClanCardSelectionItemInfo(hero, hero.Name, image, CardSelectionItemSpriteType.Skill, charm.StringId.ToLower(), skillValue.ToString(), this.GetSendMembersCandidateProperties(hero), !flag, disabledReason, null);
				}
			}
			IEnumerator<Hero> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000620BD File Offset: 0x000602BD
		private IEnumerable<ClanCardSelectionItemPropertyInfo> GetSendMembersCandidateProperties(Hero hero)
		{
			TextObject teleportationDelayText = CampaignUIHelper.GetTeleportationDelayText(hero, this.CurrentSelectedFief.Settlement.Party);
			yield return new ClanCardSelectionItemPropertyInfo(teleportationDelayText);
			TextObject textObject = new TextObject("{=otaUtXMX}+{AMOUNT} relation chance with notables per day.", null);
			int emissaryRelationBonusForMainClan = Campaign.Current.Models.EmissaryModel.EmissaryRelationBonusForMainClan;
			textObject.SetTextVariable("AMOUNT", emissaryRelationBonusForMainClan);
			yield return new ClanCardSelectionItemPropertyInfo(textObject);
			yield break;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000620D4 File Offset: 0x000602D4
		private void OnSendMembersSelectionOver(List<object> selectedItems, Action closePopup)
		{
			if (selectedItems.Count > 0)
			{
				string variableName = "SETTLEMENT_NAME";
				ClanSettlementItemVM currentSelectedFief = this.CurrentSelectedFief;
				string text;
				if (currentSelectedFief == null)
				{
					text = null;
				}
				else
				{
					Settlement settlement = currentSelectedFief.Settlement;
					if (settlement == null)
					{
						text = null;
					}
					else
					{
						TextObject name = settlement.Name;
						text = ((name != null) ? name.ToString() : null);
					}
				}
				MBTextManager.SetTextVariable(variableName, text ?? string.Empty, false);
				InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_send_members", null).ToString(), GameTexts.FindText("str_send_members_inquiry", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					Action closePopup3 = closePopup;
					if (closePopup3 != null)
					{
						closePopup3();
					}
					using (List<object>.Enumerator enumerator = selectedItems.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Hero heroToBeMoved;
							if ((heroToBeMoved = (enumerator.Current as Hero)) != null)
							{
								TeleportHeroAction.ApplyDelayedTeleportToSettlement(heroToBeMoved, this.CurrentSelectedFief.Settlement);
							}
						}
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

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000621C4 File Offset: 0x000603C4
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x000621CC File Offset: 0x000603CC
		[DataSourceProperty]
		public string GovernorActionText
		{
			get
			{
				return this._governorActionText;
			}
			set
			{
				if (value != this._governorActionText)
				{
					this._governorActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "GovernorActionText");
				}
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000621EF File Offset: 0x000603EF
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x000621F7 File Offset: 0x000603F7
		[DataSourceProperty]
		public bool CanChangeGovernorOfCurrentFief
		{
			get
			{
				return this._canChangeGovernorOfCurrentFief;
			}
			set
			{
				if (value != this._canChangeGovernorOfCurrentFief)
				{
					this._canChangeGovernorOfCurrentFief = value;
					base.OnPropertyChangedWithValue(value, "CanChangeGovernorOfCurrentFief");
				}
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00062215 File Offset: 0x00060415
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x0006221D File Offset: 0x0006041D
		[DataSourceProperty]
		public HintViewModel GovernorActionHint
		{
			get
			{
				return this._governorActionHint;
			}
			set
			{
				if (value != this._governorActionHint)
				{
					this._governorActionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "GovernorActionHint");
				}
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0006223B File Offset: 0x0006043B
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x00062243 File Offset: 0x00060443
		[DataSourceProperty]
		public bool IsAnyValidFiefSelected
		{
			get
			{
				return this._isAnyValidFiefSelected;
			}
			set
			{
				if (value != this._isAnyValidFiefSelected)
				{
					this._isAnyValidFiefSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidFiefSelected");
				}
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00062261 File Offset: 0x00060461
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x00062269 File Offset: 0x00060469
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

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0006228C File Offset: 0x0006048C
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x00062294 File Offset: 0x00060494
		[DataSourceProperty]
		public string TaxText
		{
			get
			{
				return this._taxText;
			}
			set
			{
				if (value != this._taxText)
				{
					this._taxText = value;
					base.OnPropertyChangedWithValue<string>(value, "TaxText");
				}
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000622B7 File Offset: 0x000604B7
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x000622BF File Offset: 0x000604BF
		[DataSourceProperty]
		public string GovernorText
		{
			get
			{
				return this._governorText;
			}
			set
			{
				if (value != this._governorText)
				{
					this._governorText = value;
					base.OnPropertyChangedWithValue<string>(value, "GovernorText");
				}
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000622E2 File Offset: 0x000604E2
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x000622EA File Offset: 0x000604EA
		[DataSourceProperty]
		public string ProfitText
		{
			get
			{
				return this._profitText;
			}
			set
			{
				if (value != this._profitText)
				{
					this._profitText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProfitText");
				}
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0006230D File Offset: 0x0006050D
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x00062315 File Offset: 0x00060515
		[DataSourceProperty]
		public string TownsText
		{
			get
			{
				return this._townsText;
			}
			set
			{
				if (value != this._townsText)
				{
					this._townsText = value;
					base.OnPropertyChangedWithValue<string>(value, "TownsText");
				}
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x00062338 File Offset: 0x00060538
		// (set) Token: 0x06001B1F RID: 6943 RVA: 0x00062340 File Offset: 0x00060540
		[DataSourceProperty]
		public string CastlesText
		{
			get
			{
				return this._castlesText;
			}
			set
			{
				if (value != this._castlesText)
				{
					this._castlesText = value;
					base.OnPropertyChangedWithValue<string>(value, "CastlesText");
				}
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x00062363 File Offset: 0x00060563
		// (set) Token: 0x06001B21 RID: 6945 RVA: 0x0006236B File Offset: 0x0006056B
		[DataSourceProperty]
		public string NoFiefsText
		{
			get
			{
				return this._noFiefsText;
			}
			set
			{
				if (value != this._noFiefsText)
				{
					this._noFiefsText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoFiefsText");
				}
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0006238E File Offset: 0x0006058E
		// (set) Token: 0x06001B23 RID: 6947 RVA: 0x00062396 File Offset: 0x00060596
		[DataSourceProperty]
		public string NoGovernorText
		{
			get
			{
				return this._noGovernorText;
			}
			set
			{
				if (value != this._noGovernorText)
				{
					this._noGovernorText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoGovernorText");
				}
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x000623B9 File Offset: 0x000605B9
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x000623C1 File Offset: 0x000605C1
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

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x000623DF File Offset: 0x000605DF
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x000623E7 File Offset: 0x000605E7
		[DataSourceProperty]
		public MBBindingList<ClanSettlementItemVM> Settlements
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
					base.OnPropertyChangedWithValue<MBBindingList<ClanSettlementItemVM>>(value, "Settlements");
				}
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x00062405 File Offset: 0x00060605
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x0006240D File Offset: 0x0006060D
		[DataSourceProperty]
		public MBBindingList<ClanSettlementItemVM> Castles
		{
			get
			{
				return this._castles;
			}
			set
			{
				if (value != this._castles)
				{
					this._castles = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanSettlementItemVM>>(value, "Castles");
				}
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0006242B File Offset: 0x0006062B
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x00062433 File Offset: 0x00060633
		[DataSourceProperty]
		public ClanSettlementItemVM CurrentSelectedFief
		{
			get
			{
				return this._currentSelectedFief;
			}
			set
			{
				if (value != this._currentSelectedFief)
				{
					this._currentSelectedFief = value;
					base.OnPropertyChangedWithValue<ClanSettlementItemVM>(value, "CurrentSelectedFief");
					this.IsAnyValidFiefSelected = (value != null);
				}
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0006245B File Offset: 0x0006065B
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x00062463 File Offset: 0x00060663
		[DataSourceProperty]
		public ClanFiefsSortControllerVM SortController
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
					base.OnPropertyChangedWithValue<ClanFiefsSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x04000CC0 RID: 3264
		private readonly Clan _clan;

		// Token: 0x04000CC1 RID: 3265
		private readonly Action _onRefresh;

		// Token: 0x04000CC2 RID: 3266
		private readonly Action<ClanCardSelectionInfo> _openCardSelectionPopup;

		// Token: 0x04000CC3 RID: 3267
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000CC4 RID: 3268
		private readonly TextObject _noGovernorTextSource = new TextObject("{=zLFsnaqR}No Governor", null);

		// Token: 0x04000CC5 RID: 3269
		private MBBindingList<ClanSettlementItemVM> _settlements;

		// Token: 0x04000CC6 RID: 3270
		private MBBindingList<ClanSettlementItemVM> _castles;

		// Token: 0x04000CC7 RID: 3271
		private ClanSettlementItemVM _currentSelectedFief;

		// Token: 0x04000CC8 RID: 3272
		private bool _isSelected;

		// Token: 0x04000CC9 RID: 3273
		private string _nameText;

		// Token: 0x04000CCA RID: 3274
		private string _taxText;

		// Token: 0x04000CCB RID: 3275
		private string _governorText;

		// Token: 0x04000CCC RID: 3276
		private string _profitText;

		// Token: 0x04000CCD RID: 3277
		private string _townsText;

		// Token: 0x04000CCE RID: 3278
		private string _castlesText;

		// Token: 0x04000CCF RID: 3279
		private string _noFiefsText;

		// Token: 0x04000CD0 RID: 3280
		private string _noGovernorText;

		// Token: 0x04000CD1 RID: 3281
		private bool _isAnyValidFiefSelected;

		// Token: 0x04000CD2 RID: 3282
		private bool _canChangeGovernorOfCurrentFief;

		// Token: 0x04000CD3 RID: 3283
		private HintViewModel _governorActionHint;

		// Token: 0x04000CD4 RID: 3284
		private string _governorActionText;

		// Token: 0x04000CD5 RID: 3285
		private ClanFiefsSortControllerVM _sortController;
	}
}
