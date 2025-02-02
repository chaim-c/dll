using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x0200009F RID: 159
	public class RecruitmentVM : ViewModel
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003CC0A File Offset: 0x0003AE0A
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0003CC12 File Offset: 0x0003AE12
		public bool IsQuitting { get; private set; }

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003CC1C File Offset: 0x0003AE1C
		public RecruitmentVM()
		{
			this.VolunteerList = new MBBindingList<RecruitVolunteerVM>();
			this.TroopsInCart = new MBBindingList<RecruitVolunteerTroopVM>();
			this.RefreshValues();
			if (Settlement.CurrentSettlement != null)
			{
				this.RefreshScreen();
			}
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			RecruitVolunteerTroopVM.OnFocused = (Action<RecruitVolunteerTroopVM>)Delegate.Combine(RecruitVolunteerTroopVM.OnFocused, new Action<RecruitVolunteerTroopVM>(this.OnVolunteerTroopFocusChanged));
			RecruitVolunteerOwnerVM.OnFocused = (Action<RecruitVolunteerOwnerVM>)Delegate.Combine(RecruitVolunteerOwnerVM.OnFocused, new Action<RecruitVolunteerOwnerVM>(this.OnVolunteerOwnerFocusChanged));
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003CCEC File Offset: 0x0003AEEC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PartyWageHint = new HintViewModel(GameTexts.FindText("str_weekly_wage", null), null);
			this.TotalWealthHint = new HintViewModel(GameTexts.FindText("str_wealth", null), null);
			this.TotalCostHint = new HintViewModel(GameTexts.FindText("str_total_cost", null), null);
			this.PartyCapacityHint = new HintViewModel();
			this.PartySpeedHint = new BasicTooltipViewModel();
			this.RemainingFoodHint = new HintViewModel();
			this.DoneHint = new HintViewModel();
			this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset", null), null);
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.TitleText = GameTexts.FindText("str_recruitment", null).ToString();
			this._recruitAllTextObject = GameTexts.FindText("str_recruit_all", null);
			this.ResetAllText = GameTexts.FindText("str_reset_all", null).ToString();
			this.CancelText = GameTexts.FindText("str_party_cancel", null).ToString();
			this._playerDoesntHaveEnoughMoneyStr = GameTexts.FindText("str_warning_you_dont_have_enough_money", null).ToString();
			this._playerIsOverPartyLimitStr = GameTexts.FindText("str_party_size_limit_exceeded", null).ToString();
			this.VolunteerList.ApplyActionOnAllItems(delegate(RecruitVolunteerVM x)
			{
				x.RefreshValues();
			});
			this.TroopsInCart.ApplyActionOnAllItems(delegate(RecruitVolunteerTroopVM x)
			{
				x.RefreshValues();
			});
			this.SetRecruitAllHint();
			this.UpdateRecruitAllProperties();
			if (Settlement.CurrentSettlement != null)
			{
				this.RefreshScreen();
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003CE8C File Offset: 0x0003B08C
		public void RefreshScreen()
		{
			this.VolunteerList.Clear();
			this.TroopsInCart.Clear();
			int num = 0;
			this.InitialPartySize = PartyBase.MainParty.NumberOfAllMembers;
			this.RefreshPartyProperties();
			foreach (Hero hero in Settlement.CurrentSettlement.Notables)
			{
				if (hero.CanHaveRecruits)
				{
					MBTextManager.SetTextVariable("INDIVIDUAL_NAME", hero.Name, false);
					List<CharacterObject> volunteerTroopsOfHeroForRecruitment = HeroHelper.GetVolunteerTroopsOfHeroForRecruitment(hero);
					RecruitVolunteerVM item = new RecruitVolunteerVM(hero, volunteerTroopsOfHeroForRecruitment, new Action<RecruitVolunteerVM, RecruitVolunteerTroopVM>(this.OnRecruit), new Action<RecruitVolunteerVM, RecruitVolunteerTroopVM>(this.OnRemoveFromCart));
					this.VolunteerList.Add(item);
					num++;
				}
			}
			this.TotalWealth = Hero.MainHero.Gold;
			this.UpdateRecruitAllProperties();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003CF74 File Offset: 0x0003B174
		private void OnRecruit(RecruitVolunteerVM recruitNotable, RecruitVolunteerTroopVM recruitTroop)
		{
			if (!recruitTroop.CanBeRecruited)
			{
				return;
			}
			recruitNotable.OnRecruitMoveToCart(recruitTroop);
			recruitTroop.CanBeRecruited = false;
			this.TroopsInCart.Add(recruitTroop);
			recruitTroop.IsInCart = true;
			CampaignEventDispatcher.Instance.OnPlayerStartRecruitment(recruitTroop.Character);
			this.RefreshPartyProperties();
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003CFC4 File Offset: 0x0003B1C4
		private void RefreshPartyProperties()
		{
			int num = this.TroopsInCart.Sum((RecruitVolunteerTroopVM t) => t.Wage);
			this.PartyWage = MobileParty.MainParty.TotalWage;
			if (num > 0)
			{
				this.PartyWageText = CampaignUIHelper.GetValueChangeText((float)this.PartyWage, (float)num, "F0");
			}
			else
			{
				this.PartyWageText = this.PartyWage.ToString();
			}
			double num2 = 0.0;
			if (this.TroopsInCart.Count > 0)
			{
				int num3 = 0;
				int num4 = 0;
				using (IEnumerator<RecruitVolunteerTroopVM> enumerator = this.TroopsInCart.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Character.IsMounted)
						{
							num4++;
						}
						else
						{
							num3++;
						}
					}
				}
				ExplainedNumber finalSpeed = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateBaseSpeed(MobileParty.MainParty, false, num3, num4);
				ExplainedNumber explainedNumber = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(MobileParty.MainParty, finalSpeed);
				ExplainedNumber finalSpeed2 = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateBaseSpeed(MobileParty.MainParty, false, 0, 0);
				ExplainedNumber explainedNumber2 = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(MobileParty.MainParty, finalSpeed2);
				num2 = (double)(MathF.Round(explainedNumber.ResultNumber, 1) - MathF.Round(explainedNumber2.ResultNumber, 1));
			}
			this.PartySpeedText = MobileParty.MainParty.Speed.ToString("0.0");
			this.PartySpeedHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartySpeedTooltip());
			if (num2 != 0.0)
			{
				this.PartySpeedText = CampaignUIHelper.GetValueChangeText(MobileParty.MainParty.Speed, (float)num2, "0.0");
			}
			int partySizeLimit = PartyBase.MainParty.PartySizeLimit;
			this.CurrentPartySize = PartyBase.MainParty.NumberOfAllMembers + this.TroopsInCart.Count;
			this.PartyCapacity = partySizeLimit;
			this.IsPartyCapacityWarningEnabled = (this.CurrentPartySize > this.PartyCapacity);
			GameTexts.SetVariable("LEFT", this.CurrentPartySize.ToString());
			GameTexts.SetVariable("RIGHT", partySizeLimit.ToString());
			this.PartyCapacityText = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
			this.PartyCapacityHint.HintText = new TextObject("{=!}" + PartyBase.MainParty.PartySizeLimitExplainer.ToString(), null);
			float food = MobileParty.MainParty.Food;
			this.RemainingFoodText = MathF.Round(food, 1).ToString();
			float foodChange = MobileParty.MainParty.FoodChange;
			int totalFoodAtInventory = MobileParty.MainParty.TotalFoodAtInventory;
			int numDaysForFoodToLast = MobileParty.MainParty.GetNumDaysForFoodToLast();
			MBTextManager.SetTextVariable("DAY_NUM", numDaysForFoodToLast);
			this.RemainingFoodHint.HintText = GameTexts.FindText("str_food_consumption_tooltip", null);
			this.RemainingFoodHint.HintText.SetTextVariable("DAILY_FOOD_CONSUMPTION", foodChange);
			this.RemainingFoodHint.HintText.SetTextVariable("REMAINING_DAYS", GameTexts.FindText("str_party_food_left", null));
			this.RemainingFoodHint.HintText.SetTextVariable("TOTAL_FOOD_AMOUNT", ((double)totalFoodAtInventory + 0.01 * (double)PartyBase.MainParty.RemainingFoodPercentage).ToString("0.00"));
			this.RemainingFoodHint.HintText.SetTextVariable("TOTAL_FOOD", totalFoodAtInventory);
			int num5 = this.TroopsInCart.Sum((RecruitVolunteerTroopVM t) => t.Cost);
			this.TotalCostText = num5.ToString();
			bool flag = num5 <= Hero.MainHero.Gold;
			this.IsDoneEnabled = flag;
			this.DoneHint.HintText = new TextObject("{=!}" + this.GetDoneHint(flag), null);
			this.UpdateRecruitAllProperties();
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003D3E8 File Offset: 0x0003B5E8
		public void ExecuteDone()
		{
			if (this.CurrentPartySize <= this.PartyCapacity)
			{
				this.OnDone();
				return;
			}
			GameTexts.SetVariable("newline", "\n");
			string text = GameTexts.FindText("str_party_over_limit_troops", null).ToString();
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=uJro3Bua}Over Limit", null).ToString(), text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
			{
				this.OnDone();
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003D484 File Offset: 0x0003B684
		private void OnDone()
		{
			this.RefreshPartyProperties();
			int num = this.TroopsInCart.Sum((RecruitVolunteerTroopVM t) => t.Cost);
			if (num > Hero.MainHero.Gold)
			{
				Debug.FailedAssert("Execution shouldn't come here. The checks should happen before", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Recruitment\\RecruitmentVM.cs", "OnDone", 229);
				return;
			}
			foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in this.TroopsInCart)
			{
				recruitVolunteerTroopVM.Owner.OwnerHero.VolunteerTypes[recruitVolunteerTroopVM.Index] = null;
				MobileParty.MainParty.MemberRoster.AddToCounts(recruitVolunteerTroopVM.Character, 1, false, 0, 0, true, -1);
				CampaignEventDispatcher.Instance.OnUnitRecruited(recruitVolunteerTroopVM.Character, 1);
			}
			GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, num, true);
			if (num > 0)
			{
				MBTextManager.SetTextVariable("GOLD_AMOUNT", MathF.Abs(num));
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_gold_removed_with_icon", null).ToString(), "event:/ui/notification/coins_negative"));
			}
			this.Deactivate();
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003D5B0 File Offset: 0x0003B7B0
		public void ExecuteForceQuit()
		{
			if (!this.IsQuitting)
			{
				this.IsQuitting = true;
				if (this.TroopsInCart.Count > 0)
				{
					InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_quit", null).ToString(), GameTexts.FindText("str_quit_question", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
					{
						this.ExecuteReset();
						this.ExecuteDone();
						this.IsQuitting = false;
					}, delegate()
					{
						this.IsQuitting = false;
					}, "", 0f, null, null, null), true, false);
					return;
				}
				this.Deactivate();
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003D658 File Offset: 0x0003B858
		public void ExecuteReset()
		{
			for (int i = this.TroopsInCart.Count - 1; i >= 0; i--)
			{
				this.TroopsInCart[i].ExecuteRemoveFromCart();
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003D690 File Offset: 0x0003B890
		public void ExecuteRecruitAll()
		{
			foreach (RecruitVolunteerVM recruitVolunteerVM in this.VolunteerList.ToList<RecruitVolunteerVM>())
			{
				foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in recruitVolunteerVM.Troops.ToList<RecruitVolunteerTroopVM>())
				{
					recruitVolunteerTroopVM.ExecuteRecruit();
				}
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003D724 File Offset: 0x0003B924
		public void Deactivate()
		{
			this.ExecuteReset();
			this.Enabled = false;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0003D734 File Offset: 0x0003B934
		public override void OnFinalize()
		{
			base.OnFinalize();
			RecruitVolunteerTroopVM.OnFocused = (Action<RecruitVolunteerTroopVM>)Delegate.Remove(RecruitVolunteerTroopVM.OnFocused, new Action<RecruitVolunteerTroopVM>(this.OnVolunteerTroopFocusChanged));
			RecruitVolunteerOwnerVM.OnFocused = (Action<RecruitVolunteerOwnerVM>)Delegate.Remove(RecruitVolunteerOwnerVM.OnFocused, new Action<RecruitVolunteerOwnerVM>(this.OnVolunteerOwnerFocusChanged));
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.CancelInputKey.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.ResetInputKey.OnFinalize();
			this.RecruitAllInputKey.OnFinalize();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003D7D0 File Offset: 0x0003B9D0
		private void OnRemoveFromCart(RecruitVolunteerVM recruitNotable, RecruitVolunteerTroopVM recruitTroop)
		{
			if (this.TroopsInCart.Any((RecruitVolunteerTroopVM r) => r == recruitTroop))
			{
				recruitNotable.OnRecruitRemovedFromCart(recruitTroop);
				recruitTroop.CanBeRecruited = true;
				recruitTroop.IsInCart = false;
				recruitTroop.IsHiglightEnabled = false;
				this.TroopsInCart.Remove(recruitTroop);
				this.RefreshPartyProperties();
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003D84B File Offset: 0x0003BA4B
		private static bool IsBitSet(int num, int bit)
		{
			return 1 == (num >> bit & 1);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003D858 File Offset: 0x0003BA58
		private string GetDoneHint(bool doesPlayerHasEnoughMoney)
		{
			if (!doesPlayerHasEnoughMoney)
			{
				return this._playerDoesntHaveEnoughMoneyStr;
			}
			return null;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003D865 File Offset: 0x0003BA65
		private void SetRecruitAllHint()
		{
			this.RecruitAllHint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("HOTKEY", this.GetRecruitAllKey());
				GameTexts.SetVariable("TEXT", GameTexts.FindText("str_recruit_all", null));
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003D880 File Offset: 0x0003BA80
		private void UpdateRecruitAllProperties()
		{
			int numberOfAvailableRecruits = this.GetNumberOfAvailableRecruits();
			GameTexts.SetVariable("STR", numberOfAvailableRecruits);
			GameTexts.SetVariable("STR1", this._recruitAllTextObject);
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_STR_in_parentheses", null));
			this.RecruitAllText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			this.CanRecruitAll = (numberOfAvailableRecruits > 0);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
		private int GetNumberOfAvailableRecruits()
		{
			int num = 0;
			foreach (RecruitVolunteerVM recruitVolunteerVM in this.VolunteerList)
			{
				foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in recruitVolunteerVM.Troops)
				{
					if (!recruitVolunteerTroopVM.IsInCart && recruitVolunteerTroopVM.CanBeRecruited)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003D974 File Offset: 0x0003BB74
		private void OnVolunteerTroopFocusChanged(RecruitVolunteerTroopVM volunteer)
		{
			this.FocusedVolunteerTroop = volunteer;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003D97D File Offset: 0x0003BB7D
		private void OnVolunteerOwnerFocusChanged(RecruitVolunteerOwnerVM owner)
		{
			this.FocusedVolunteerOwner = owner;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003D988 File Offset: 0x0003BB88
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null && this._isAvailableTroopsHighlightApplied)
				{
					this.SetAvailableTroopsHighlightState(false);
					this._isAvailableTroopsHighlightApplied = false;
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null && !this._isAvailableTroopsHighlightApplied && this._latestTutorialElementID == "AvailableTroops")
				{
					this.SetAvailableTroopsHighlightState(true);
					this._isAvailableTroopsHighlightApplied = true;
				}
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003DA04 File Offset: 0x0003BC04
		private void SetAvailableTroopsHighlightState(bool state)
		{
			foreach (RecruitVolunteerVM recruitVolunteerVM in this.VolunteerList)
			{
				foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in recruitVolunteerVM.Troops)
				{
					if (recruitVolunteerTroopVM.Wage < Hero.MainHero.Gold && recruitVolunteerTroopVM.PlayerHasEnoughRelation && !recruitVolunteerTroopVM.IsTroopEmpty)
					{
						recruitVolunteerTroopVM.IsHiglightEnabled = state;
					}
				}
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003DAA8 File Offset: 0x0003BCA8
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0003DAB0 File Offset: 0x0003BCB0
		[DataSourceProperty]
		public HintViewModel ResetHint
		{
			get
			{
				return this._resetHint;
			}
			set
			{
				if (value != this._resetHint)
				{
					this._resetHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetHint");
				}
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003DACE File Offset: 0x0003BCCE
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0003DAD6 File Offset: 0x0003BCD6
		[DataSourceProperty]
		public RecruitVolunteerTroopVM FocusedVolunteerTroop
		{
			get
			{
				return this._focusedVolunteerTroop;
			}
			set
			{
				if (value != this._focusedVolunteerTroop)
				{
					this._focusedVolunteerTroop = value;
					base.OnPropertyChangedWithValue<RecruitVolunteerTroopVM>(value, "FocusedVolunteerTroop");
				}
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003DAF4 File Offset: 0x0003BCF4
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0003DAFC File Offset: 0x0003BCFC
		[DataSourceProperty]
		public RecruitVolunteerOwnerVM FocusedVolunteerOwner
		{
			get
			{
				return this._focusedVolunteerOwner;
			}
			set
			{
				if (value != this._focusedVolunteerOwner)
				{
					this._focusedVolunteerOwner = value;
					base.OnPropertyChangedWithValue<RecruitVolunteerOwnerVM>(value, "FocusedVolunteerOwner");
				}
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003DB1A File Offset: 0x0003BD1A
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0003DB22 File Offset: 0x0003BD22
		[DataSourceProperty]
		public HintViewModel PartyWageHint
		{
			get
			{
				return this._partyWageHint;
			}
			set
			{
				if (value != this._partyWageHint)
				{
					this._partyWageHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PartyWageHint");
				}
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0003DB40 File Offset: 0x0003BD40
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0003DB48 File Offset: 0x0003BD48
		[DataSourceProperty]
		public HintViewModel PartyCapacityHint
		{
			get
			{
				return this._partyCapacityHint;
			}
			set
			{
				if (value != this._partyCapacityHint)
				{
					this._partyCapacityHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PartyCapacityHint");
				}
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0003DB66 File Offset: 0x0003BD66
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x0003DB6E File Offset: 0x0003BD6E
		[DataSourceProperty]
		public BasicTooltipViewModel PartySpeedHint
		{
			get
			{
				return this._partySpeedHint;
			}
			set
			{
				if (value != this._partySpeedHint)
				{
					this._partySpeedHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PartySpeedHint");
				}
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0003DB8C File Offset: 0x0003BD8C
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0003DB94 File Offset: 0x0003BD94
		[DataSourceProperty]
		public HintViewModel RemainingFoodHint
		{
			get
			{
				return this._remainingFoodHint;
			}
			set
			{
				if (value != this._remainingFoodHint)
				{
					this._remainingFoodHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RemainingFoodHint");
				}
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0003DBB2 File Offset: 0x0003BDB2
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x0003DBBA File Offset: 0x0003BDBA
		[DataSourceProperty]
		public HintViewModel TotalWealthHint
		{
			get
			{
				return this._totalWealthHint;
			}
			set
			{
				if (value != this._totalWealthHint)
				{
					this._totalWealthHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TotalWealthHint");
				}
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0003DBE0 File Offset: 0x0003BDE0
		[DataSourceProperty]
		public HintViewModel TotalCostHint
		{
			get
			{
				return this._totalCostHint;
			}
			set
			{
				if (value != this._totalCostHint)
				{
					this._totalCostHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TotalCostHint");
				}
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003DBFE File Offset: 0x0003BDFE
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0003DC06 File Offset: 0x0003BE06
		[DataSourceProperty]
		public HintViewModel DoneHint
		{
			get
			{
				return this._doneHint;
			}
			set
			{
				if (value != this._doneHint)
				{
					this._doneHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DoneHint");
				}
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003DC24 File Offset: 0x0003BE24
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		[DataSourceProperty]
		public BasicTooltipViewModel RecruitAllHint
		{
			get
			{
				return this._recruitAllHint;
			}
			set
			{
				if (value != this._recruitAllHint)
				{
					this._recruitAllHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "RecruitAllHint");
				}
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0003DC4A File Offset: 0x0003BE4A
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0003DC52 File Offset: 0x0003BE52
		[DataSourceProperty]
		public int PartyWage
		{
			get
			{
				return this._partyWage;
			}
			set
			{
				if (value != this._partyWage)
				{
					this._partyWage = value;
					base.OnPropertyChangedWithValue(value, "PartyWage");
				}
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003DC70 File Offset: 0x0003BE70
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0003DC78 File Offset: 0x0003BE78
		[DataSourceProperty]
		public string PartyCapacityText
		{
			get
			{
				return this._partyCapacityText;
			}
			set
			{
				if (value != this._partyCapacityText)
				{
					this._partyCapacityText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyCapacityText");
				}
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0003DC9B File Offset: 0x0003BE9B
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0003DCA3 File Offset: 0x0003BEA3
		[DataSourceProperty]
		public string PartyWageText
		{
			get
			{
				return this._partyWageText;
			}
			set
			{
				if (value != this._partyWageText)
				{
					this._partyWageText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyWageText");
				}
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0003DCC6 File Offset: 0x0003BEC6
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0003DCCE File Offset: 0x0003BECE
		[DataSourceProperty]
		public string RecruitAllText
		{
			get
			{
				return this._recruitAllText;
			}
			set
			{
				if (value != this._recruitAllText)
				{
					this._recruitAllText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitAllText");
				}
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0003DCF1 File Offset: 0x0003BEF1
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0003DCF9 File Offset: 0x0003BEF9
		[DataSourceProperty]
		public string PartySpeedText
		{
			get
			{
				return this._partySpeedText;
			}
			set
			{
				if (value != this._partySpeedText)
				{
					this._partySpeedText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartySpeedText");
				}
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0003DD1C File Offset: 0x0003BF1C
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x0003DD24 File Offset: 0x0003BF24
		[DataSourceProperty]
		public string ResetAllText
		{
			get
			{
				return this._resetAllText;
			}
			set
			{
				if (value != this._resetAllText)
				{
					this._resetAllText = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetAllText");
				}
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0003DD47 File Offset: 0x0003BF47
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x0003DD4F File Offset: 0x0003BF4F
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0003DD72 File Offset: 0x0003BF72
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x0003DD7A File Offset: 0x0003BF7A
		[DataSourceProperty]
		public string RemainingFoodText
		{
			get
			{
				return this._remainingFoodText;
			}
			set
			{
				if (value != this._remainingFoodText)
				{
					this._remainingFoodText = value;
					base.OnPropertyChangedWithValue<string>(value, "RemainingFoodText");
				}
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0003DD9D File Offset: 0x0003BF9D
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0003DDA5 File Offset: 0x0003BFA5
		[DataSourceProperty]
		public string TotalCostText
		{
			get
			{
				return this._totalCostText;
			}
			set
			{
				if (value != this._totalCostText)
				{
					this._totalCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalCostText");
				}
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003DDC8 File Offset: 0x0003BFC8
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0003DDD0 File Offset: 0x0003BFD0
		[DataSourceProperty]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (value != this._enabled)
				{
					this._enabled = value;
					base.OnPropertyChangedWithValue(value, "Enabled");
				}
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0003DDF8 File Offset: 0x0003BFF8
		[DataSourceProperty]
		public bool IsDoneEnabled
		{
			get
			{
				return this._isDoneEnabled;
			}
			set
			{
				if (value != this._isDoneEnabled)
				{
					this._isDoneEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsDoneEnabled");
				}
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0003DE16 File Offset: 0x0003C016
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0003DE1E File Offset: 0x0003C01E
		[DataSourceProperty]
		public bool IsPartyCapacityWarningEnabled
		{
			get
			{
				return this._isPartyCapacityWarningEnabled;
			}
			set
			{
				if (value != this._isPartyCapacityWarningEnabled)
				{
					this._isPartyCapacityWarningEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPartyCapacityWarningEnabled");
				}
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003DE3C File Offset: 0x0003C03C
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0003DE44 File Offset: 0x0003C044
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0003DE67 File Offset: 0x0003C067
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x0003DE6F File Offset: 0x0003C06F
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0003DE92 File Offset: 0x0003C092
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0003DE9A File Offset: 0x0003C09A
		[DataSourceProperty]
		public bool CanRecruitAll
		{
			get
			{
				return this._canRecruitAll;
			}
			set
			{
				if (value != this._canRecruitAll)
				{
					this._canRecruitAll = value;
					base.OnPropertyChangedWithValue(value, "CanRecruitAll");
				}
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003DEB8 File Offset: 0x0003C0B8
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		[DataSourceProperty]
		public int TotalWealth
		{
			get
			{
				return this._totalWealth;
			}
			set
			{
				if (value != this._totalWealth)
				{
					this._totalWealth = value;
					base.OnPropertyChangedWithValue(value, "TotalWealth");
				}
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003DEDE File Offset: 0x0003C0DE
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0003DEE6 File Offset: 0x0003C0E6
		[DataSourceProperty]
		public int PartyCapacity
		{
			get
			{
				return this._partyCapacity;
			}
			set
			{
				if (value != this._partyCapacity)
				{
					this._partyCapacity = value;
					base.OnPropertyChangedWithValue(value, "PartyCapacity");
				}
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003DF04 File Offset: 0x0003C104
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0003DF0C File Offset: 0x0003C10C
		[DataSourceProperty]
		public int InitialPartySize
		{
			get
			{
				return this._initialPartySize;
			}
			set
			{
				if (value != this._initialPartySize)
				{
					this._initialPartySize = value;
					base.OnPropertyChangedWithValue(value, "InitialPartySize");
				}
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0003DF2A File Offset: 0x0003C12A
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0003DF32 File Offset: 0x0003C132
		[DataSourceProperty]
		public int CurrentPartySize
		{
			get
			{
				return this._currentPartySize;
			}
			set
			{
				if (value != this._currentPartySize)
				{
					this._currentPartySize = value;
					base.OnPropertyChangedWithValue(value, "CurrentPartySize");
				}
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0003DF50 File Offset: 0x0003C150
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0003DF58 File Offset: 0x0003C158
		[DataSourceProperty]
		public MBBindingList<RecruitVolunteerVM> VolunteerList
		{
			get
			{
				return this._volunteerList;
			}
			set
			{
				if (value != this._volunteerList)
				{
					this._volunteerList = value;
					base.OnPropertyChangedWithValue<MBBindingList<RecruitVolunteerVM>>(value, "VolunteerList");
				}
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0003DF76 File Offset: 0x0003C176
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0003DF7E File Offset: 0x0003C17E
		[DataSourceProperty]
		public MBBindingList<RecruitVolunteerTroopVM> TroopsInCart
		{
			get
			{
				return this._troopsInCart;
			}
			set
			{
				if (value != this._troopsInCart)
				{
					this._troopsInCart = value;
					base.OnPropertyChangedWithValue<MBBindingList<RecruitVolunteerTroopVM>>(value, "TroopsInCart");
				}
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003DF9C File Offset: 0x0003C19C
		public void SetGetKeyTextFromKeyIDFunc(Func<string, TextObject> getKeyTextFromKeyId)
		{
			this._getKeyTextFromKeyId = getKeyTextFromKeyId;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003DFA5 File Offset: 0x0003C1A5
		private string GetRecruitAllKey()
		{
			if (this.RecruitAllInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return string.Empty;
			}
			return this._getKeyTextFromKeyId(this.RecruitAllInputKey.KeyID).ToString();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003DFD8 File Offset: 0x0003C1D8
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003DFE7 File Offset: 0x0003C1E7
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003DFF6 File Offset: 0x0003C1F6
		public void SetRecruitAllInputKey(HotKey hotKey)
		{
			this.RecruitAllInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetRecruitAllHint();
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003E00B File Offset: 0x0003C20B
		public void SetResetInputKey(HotKey hotKey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0003E01A File Offset: 0x0003C21A
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0003E022 File Offset: 0x0003C222
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0003E040 File Offset: 0x0003C240
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x0003E048 File Offset: 0x0003C248
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0003E066 File Offset: 0x0003C266
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0003E06E File Offset: 0x0003C26E
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0003E08C File Offset: 0x0003C28C
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0003E094 File Offset: 0x0003C294
		[DataSourceProperty]
		public InputKeyItemVM RecruitAllInputKey
		{
			get
			{
				return this._recruitAllInputKey;
			}
			set
			{
				if (value != this._recruitAllInputKey)
				{
					this._recruitAllInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RecruitAllInputKey");
				}
			}
		}

		// Token: 0x0400072F RID: 1839
		private TextObject _recruitAllTextObject;

		// Token: 0x04000730 RID: 1840
		private string _playerDoesntHaveEnoughMoneyStr;

		// Token: 0x04000731 RID: 1841
		private string _playerIsOverPartyLimitStr;

		// Token: 0x04000732 RID: 1842
		private Func<string, TextObject> _getKeyTextFromKeyId;

		// Token: 0x04000733 RID: 1843
		private bool _isAvailableTroopsHighlightApplied;

		// Token: 0x04000734 RID: 1844
		private string _latestTutorialElementID;

		// Token: 0x04000735 RID: 1845
		private bool _enabled;

		// Token: 0x04000736 RID: 1846
		private bool _isDoneEnabled;

		// Token: 0x04000737 RID: 1847
		private bool _isPartyCapacityWarningEnabled;

		// Token: 0x04000738 RID: 1848
		private bool _canRecruitAll;

		// Token: 0x04000739 RID: 1849
		private string _titleText;

		// Token: 0x0400073A RID: 1850
		private string _doneText;

		// Token: 0x0400073B RID: 1851
		private string _recruitAllText;

		// Token: 0x0400073C RID: 1852
		private string _resetAllText;

		// Token: 0x0400073D RID: 1853
		private string _cancelText;

		// Token: 0x0400073E RID: 1854
		private int _totalWealth;

		// Token: 0x0400073F RID: 1855
		private int _partyCapacity;

		// Token: 0x04000740 RID: 1856
		private int _initialPartySize;

		// Token: 0x04000741 RID: 1857
		private int _currentPartySize;

		// Token: 0x04000742 RID: 1858
		private MBBindingList<RecruitVolunteerVM> _volunteerList;

		// Token: 0x04000743 RID: 1859
		private MBBindingList<RecruitVolunteerTroopVM> _troopsInCart;

		// Token: 0x04000744 RID: 1860
		private int _partyWage;

		// Token: 0x04000745 RID: 1861
		private string _partyCapacityText = "";

		// Token: 0x04000746 RID: 1862
		private string _partyWageText = "";

		// Token: 0x04000747 RID: 1863
		private string _partySpeedText = "";

		// Token: 0x04000748 RID: 1864
		private string _remainingFoodText = "";

		// Token: 0x04000749 RID: 1865
		private string _totalCostText = "";

		// Token: 0x0400074A RID: 1866
		private RecruitVolunteerTroopVM _focusedVolunteerTroop;

		// Token: 0x0400074B RID: 1867
		private RecruitVolunteerOwnerVM _focusedVolunteerOwner;

		// Token: 0x0400074C RID: 1868
		private HintViewModel _partyWageHint;

		// Token: 0x0400074D RID: 1869
		private HintViewModel _partyCapacityHint;

		// Token: 0x0400074E RID: 1870
		private BasicTooltipViewModel _partySpeedHint;

		// Token: 0x0400074F RID: 1871
		private HintViewModel _remainingFoodHint;

		// Token: 0x04000750 RID: 1872
		private HintViewModel _totalWealthHint;

		// Token: 0x04000751 RID: 1873
		private HintViewModel _totalCostHint;

		// Token: 0x04000752 RID: 1874
		private HintViewModel _resetHint;

		// Token: 0x04000753 RID: 1875
		private HintViewModel _doneHint;

		// Token: 0x04000754 RID: 1876
		private BasicTooltipViewModel _recruitAllHint;

		// Token: 0x04000755 RID: 1877
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000756 RID: 1878
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000757 RID: 1879
		private InputKeyItemVM _resetInputKey;

		// Token: 0x04000758 RID: 1880
		private InputKeyItemVM _recruitAllInputKey;
	}
}
