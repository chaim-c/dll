﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance
{
	// Token: 0x02000113 RID: 275
	public class ClanFinanceAlleyItemVM : ClanFinanceIncomeItemBaseVM
	{
		// Token: 0x06001A78 RID: 6776 RVA: 0x0005F9E8 File Offset: 0x0005DBE8
		public ClanFinanceAlleyItemVM(Alley alley, Action<ClanCardSelectionInfo> openCardSelectionPopup, Action<ClanFinanceAlleyItemVM> onSelection, Action onRefresh) : base(null, onRefresh)
		{
			this.Alley = alley;
			this._alleyModel = Campaign.Current.Models.AlleyModel;
			this._alleyBehavior = Campaign.Current.GetCampaignBehavior<IAlleyCampaignBehavior>();
			this._onSelection = new Action<ClanFinanceIncomeItemBaseVM>(this.tempOnSelection);
			this._onSelectionT = onSelection;
			this._openCardSelectionPopup = openCardSelectionPopup;
			this.ManageAlleyHint = new HintViewModel();
			this._alleyOwner = this._alleyBehavior.GetAssignedClanMemberOfAlley(this.Alley);
			if (this._alleyOwner == null)
			{
				this._alleyOwner = this.Alley.Owner;
			}
			this.OwnerVisual = new ImageIdentifierVM(CharacterCode.CreateFrom(this._alleyOwner.CharacterObject));
			Settlement settlement = this.Alley.Settlement;
			base.ImageName = ((((settlement != null) ? settlement.SettlementComponent : null) != null) ? this.Alley.Settlement.SettlementComponent.WaitMeshName : "");
			this.RefreshValues();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0005FAE4 File Offset: 0x0005DCE4
		public override void RefreshValues()
		{
			base.RefreshValues();
			base.Name = this.Alley.Name.ToString();
			base.Location = this.Alley.Settlement.Name.ToString();
			base.Income = this._alleyModel.GetDailyIncomeOfAlley(this.Alley);
			this.IncomeText = GameTexts.FindText("str_plus_with_number", null).SetTextVariable("NUMBER", base.Income).ToString();
			this.ManageAlleyHint.HintText = new TextObject("{=dQBArrqh}Manage Alley", null);
			this.PopulateStatsList();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0005FB81 File Offset: 0x0005DD81
		private void tempOnSelection(ClanFinanceIncomeItemBaseVM item)
		{
			this._onSelectionT(this);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0005FB90 File Offset: 0x0005DD90
		protected override void PopulateStatsList()
		{
			base.PopulateStatsList();
			base.ItemProperties.Clear();
			string variable = GameTexts.FindText("str_plus_with_number", null).SetTextVariable("NUMBER", this._alleyModel.GetDailyCrimeRatingOfAlley).ToString();
			string value = new TextObject("{=LuC5ZZMu}{CRIMINAL_RATING} ({INCREASE}){CRIME_ICON}", null).SetTextVariable("CRIMINAL_RATING", this.Alley.Settlement.MapFaction.MainHeroCrimeRating).SetTextVariable("INCREASE", variable).SetTextVariable("CRIME_ICON", "{=!}<img src=\"SPGeneral\\MapOverlay\\Settlement\\icon_crime\" extend=\"16\">").ToString();
			this.IncomeTextWithVisual = new TextObject("{=ePmSvu1s}{AMOUNT}{GOLD_ICON}", null).SetTextVariable("AMOUNT", base.Income).ToString();
			base.ItemProperties.Add(new SelectableItemPropertyVM(new TextObject("{=FkhJz0po}Location", null).ToString(), this.Alley.Settlement.Name.ToString(), false, null));
			base.ItemProperties.Add(new SelectableItemPropertyVM(new TextObject("{=5k4dxUEJ}Troops", null).ToString(), this._alleyBehavior.GetPlayerOwnedAlleyTroopCount(this.Alley).ToString(), false, null));
			base.ItemProperties.Add(new SelectableItemPropertyVM(new TextObject("{=QPoA6vvx}Income", null).ToString(), this.IncomeTextWithVisual, false, null));
			base.ItemProperties.Add(new SelectableItemPropertyVM(new TextObject("{=r0WIRUHo}Criminal Rating", null).ToString(), value, false, null));
			string statusText = this.GetStatusText();
			if (!string.IsNullOrEmpty(statusText))
			{
				base.ItemProperties.Add(new SelectableItemPropertyVM(new TextObject("{=DXczLzml}Status", null).ToString(), statusText, false, null));
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0005FD34 File Offset: 0x0005DF34
		private string GetStatusText()
		{
			string result = string.Empty;
			List<ValueTuple<Hero, DefaultAlleyModel.AlleyMemberAvailabilityDetail>> clanMembersAndAvailabilityDetailsForLeadingAnAlley = this._alleyModel.GetClanMembersAndAvailabilityDetailsForLeadingAnAlley(this.Alley);
			Hero assignedClanMemberOfAlley = this._alleyBehavior.GetAssignedClanMemberOfAlley(this.Alley);
			if (this._alleyBehavior.GetIsAlleyUnderAttack(this.Alley))
			{
				TextObject textObject = new TextObject("{=q1DVNQS7}Under Attack! ({RESPONSE_TIME} {?RESPONSE_TIME>1}days{?}day{\\?} left.)", null);
				textObject.SetTextVariable("RESPONSE_TIME", this._alleyBehavior.GetResponseTimeLeftForAttackInDays(this.Alley));
				result = textObject.ToString();
			}
			else if (assignedClanMemberOfAlley.IsDead)
			{
				result = new TextObject("{=KjuxDQfn}Alley leader is dead.", null).ToString();
			}
			else if (assignedClanMemberOfAlley.IsTraveling)
			{
				TextObject textObject2 = new TextObject("{=SFB2uYHa}Alley leader is traveling to the alley. ({LEFT_TIME} {?LEFT_TIME>1}hours{?}hour{\\?} left.)", null);
				textObject2.SetTextVariable("LEFT_TIME", MathF.Ceiling(TeleportationHelper.GetHoursLeftForTeleportingHeroToReachItsDestination(assignedClanMemberOfAlley)));
				result = textObject2.ToString();
			}
			else
			{
				for (int i = 0; i < clanMembersAndAvailabilityDetailsForLeadingAnAlley.Count; i++)
				{
					if (clanMembersAndAvailabilityDetailsForLeadingAnAlley[i].Item1 == Hero.MainHero && clanMembersAndAvailabilityDetailsForLeadingAnAlley[i].Item2 != DefaultAlleyModel.AlleyMemberAvailabilityDetail.Available)
					{
						result = new TextObject("{=NHZ1jNIF}Below Requirements", null).ToString();
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0005FE44 File Offset: 0x0005E044
		private ClanCardSelectionItemPropertyInfo GetSkillProperty(Hero hero, SkillObject skill)
		{
			TextObject value = ClanCardSelectionItemPropertyInfo.CreateLabeledValueText(skill.Name, new TextObject("{=!}" + hero.GetSkillValue(skill), null));
			return new ClanCardSelectionItemPropertyInfo(TextObject.Empty, value);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0005FE84 File Offset: 0x0005E084
		private IEnumerable<ClanCardSelectionItemPropertyInfo> GetHeroProperties(Hero hero, Alley alley, DefaultAlleyModel.AlleyMemberAvailabilityDetail detail)
		{
			if (detail == DefaultAlleyModel.AlleyMemberAvailabilityDetail.AvailableWithDelay)
			{
				string partyDistanceByTimeText = CampaignUIHelper.GetPartyDistanceByTimeText(Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(hero, alley.Settlement.Party).ResultNumber, Campaign.Current.Models.DelayedTeleportationModel.DefaultTeleportationSpeed);
				yield return new ClanCardSelectionItemPropertyInfo(new TextObject("{=!}" + partyDistanceByTimeText, null));
			}
			yield return new ClanCardSelectionItemPropertyInfo(new TextObject("{=bz7Glmsm}Skills", null), TextObject.Empty);
			yield return this.GetSkillProperty(hero, DefaultSkills.Tactics);
			yield return this.GetSkillProperty(hero, DefaultSkills.Leadership);
			yield return this.GetSkillProperty(hero, DefaultSkills.Steward);
			yield return this.GetSkillProperty(hero, DefaultSkills.Roguery);
			yield break;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0005FEA9 File Offset: 0x0005E0A9
		private IEnumerable<ClanCardSelectionItemInfo> GetAvailableMembers()
		{
			yield return new ClanCardSelectionItemInfo(new TextObject("{=W3hmFcfv}Abandon Alley", null), false, TextObject.Empty, TextObject.Empty);
			List<ValueTuple<Hero, DefaultAlleyModel.AlleyMemberAvailabilityDetail>> availabilityDetails = this._alleyModel.GetClanMembersAndAvailabilityDetailsForLeadingAnAlley(this.Alley);
			using (List<Hero>.Enumerator enumerator = Clan.PlayerClan.Heroes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Hero member = enumerator.Current;
					ValueTuple<Hero, DefaultAlleyModel.AlleyMemberAvailabilityDetail> valueTuple = availabilityDetails.FirstOrDefault((ValueTuple<Hero, DefaultAlleyModel.AlleyMemberAvailabilityDetail> x) => x.Item1 == member);
					if (valueTuple.Item1 != null)
					{
						CharacterCode characterCode = CharacterCode.CreateFrom(member.CharacterObject);
						bool isDisabled = valueTuple.Item2 != DefaultAlleyModel.AlleyMemberAvailabilityDetail.Available && valueTuple.Item2 != DefaultAlleyModel.AlleyMemberAvailabilityDetail.AvailableWithDelay;
						yield return new ClanCardSelectionItemInfo(member, member.Name, new ImageIdentifier(characterCode), CardSelectionItemSpriteType.None, null, null, this.GetHeroProperties(member, this.Alley, valueTuple.Item2), isDisabled, this._alleyModel.GetDisabledReasonTextForHero(member, this.Alley, valueTuple.Item2), null);
					}
				}
			}
			List<Hero>.Enumerator enumerator = default(List<Hero>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0005FEBC File Offset: 0x0005E0BC
		private void OnMemberSelection(List<object> members, Action closePopup)
		{
			if (members.Count > 0)
			{
				Hero hero = members[0] as Hero;
				if (hero != null)
				{
					this._alleyBehavior.ChangeAlleyMember(this.Alley, hero);
					Action onRefresh = this._onRefresh;
					if (onRefresh != null)
					{
						onRefresh();
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
					InformationManager.ShowInquiry(new InquiryData(new TextObject("{=W3hmFcfv}Abandon Alley", null).ToString(), new TextObject("{=pBVbKYwo}You will lose the ownership of the alley and the troops in it. Are you sure?", null).ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), delegate()
					{
						this._alleyBehavior.AbandonAlleyFromClanMenu(this.Alley);
						Action onRefresh2 = this._onRefresh;
						if (onRefresh2 != null)
						{
							onRefresh2();
						}
						Action closePopup3 = closePopup;
						if (closePopup3 == null)
						{
							return;
						}
						closePopup3();
					}, null, "", 0f, null, null, null), false, false);
				}
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0005FF98 File Offset: 0x0005E198
		public void ExecuteManageAlley()
		{
			ClanCardSelectionInfo obj = new ClanCardSelectionInfo(new TextObject("{=dQBArrqh}Manage Alley", null), this.GetAvailableMembers(), new Action<List<object>, Action>(this.OnMemberSelection), false);
			this._openCardSelectionPopup(obj);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0005FFD6 File Offset: 0x0005E1D6
		public void ExecuteBeginHeroHint()
		{
			InformationManager.ShowTooltip(typeof(Hero), new object[]
			{
				this._alleyOwner,
				true
			});
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0005FFFF File Offset: 0x0005E1FF
		public void ExecuteEndHeroHint()
		{
			InformationManager.HideTooltip();
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00060006 File Offset: 0x0005E206
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x0006000E File Offset: 0x0005E20E
		[DataSourceProperty]
		public HintViewModel ManageAlleyHint
		{
			get
			{
				return this._manageAlleyHint;
			}
			set
			{
				if (value != this._manageAlleyHint)
				{
					this._manageAlleyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ManageAlleyHint");
				}
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0006002C File Offset: 0x0005E22C
		// (set) Token: 0x06001A87 RID: 6791 RVA: 0x00060034 File Offset: 0x0005E234
		[DataSourceProperty]
		public ImageIdentifierVM OwnerVisual
		{
			get
			{
				return this._ownerVisual;
			}
			set
			{
				if (value != this._ownerVisual)
				{
					this._ownerVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "OwnerVisual");
				}
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00060052 File Offset: 0x0005E252
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x0006005A File Offset: 0x0005E25A
		[DataSourceProperty]
		public string IncomeText
		{
			get
			{
				return this._incomeText;
			}
			set
			{
				if (value != this._incomeText)
				{
					this._incomeText = value;
					base.OnPropertyChangedWithValue<string>(value, "IncomeText");
				}
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0006007D File Offset: 0x0005E27D
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x00060085 File Offset: 0x0005E285
		[DataSourceProperty]
		public string IncomeTextWithVisual
		{
			get
			{
				return this._incomeTextWithVisual;
			}
			set
			{
				if (value != this._incomeTextWithVisual)
				{
					this._incomeTextWithVisual = value;
					base.OnPropertyChangedWithValue<string>(value, "IncomeTextWithVisual");
				}
			}
		}

		// Token: 0x04000C82 RID: 3202
		public readonly Alley Alley;

		// Token: 0x04000C83 RID: 3203
		private readonly Hero _alleyOwner;

		// Token: 0x04000C84 RID: 3204
		private readonly IAlleyCampaignBehavior _alleyBehavior;

		// Token: 0x04000C85 RID: 3205
		private readonly AlleyModel _alleyModel;

		// Token: 0x04000C86 RID: 3206
		private readonly Action<ClanFinanceAlleyItemVM> _onSelectionT;

		// Token: 0x04000C87 RID: 3207
		private readonly Action<ClanCardSelectionInfo> _openCardSelectionPopup;

		// Token: 0x04000C88 RID: 3208
		private HintViewModel _manageAlleyHint;

		// Token: 0x04000C89 RID: 3209
		private ImageIdentifierVM _ownerVisual;

		// Token: 0x04000C8A RID: 3210
		private string _incomeText;

		// Token: 0x04000C8B RID: 3211
		private string _incomeTextWithVisual;
	}
}
