using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement
{
	// Token: 0x0200013F RID: 319
	public class ArmyManagementVM : ViewModel
	{
		// Token: 0x06001F15 RID: 7957 RVA: 0x0006E848 File Offset: 0x0006CA48
		public ArmyManagementVM(Action onClose)
		{
			this._onClose = onClose;
			this._itemComparer = new ArmyManagementVM.ManagementItemComparer();
			this.PartyList = new MBBindingList<ArmyManagementItemVM>();
			this.PartiesInCart = new MBBindingList<ArmyManagementItemVM>();
			this._partiesToRemove = new MBBindingList<ArmyManagementItemVM>();
			this._currentParties = new List<MobileParty>();
			this.CohesionHint = new BasicTooltipViewModel();
			this.FoodHint = new HintViewModel();
			this.MoraleHint = new HintViewModel();
			this.BoostCohesionHint = new HintViewModel();
			this.DisbandArmyHint = new HintViewModel();
			this.DoneHint = new HintViewModel();
			this.TutorialNotification = new ElementNotificationVM();
			this.CanAffordInfluenceCost = true;
			this.PlayerHasArmy = (MobileParty.MainParty.Army != null);
			foreach (MobileParty mobileParty in MobileParty.All)
			{
				if (mobileParty.LeaderHero != null && mobileParty.MapFaction == Hero.MainHero.MapFaction && mobileParty.LeaderHero != Hero.MainHero && !mobileParty.IsCaravan)
				{
					this.PartyList.Add(new ArmyManagementItemVM(new Action<ArmyManagementItemVM>(this.OnAddToCart), new Action<ArmyManagementItemVM>(this.OnRemove), new Action<ArmyManagementItemVM>(this.OnFocus), mobileParty));
				}
			}
			this._mainPartyItem = new ArmyManagementItemVM(null, null, null, Hero.MainHero.PartyBelongedTo)
			{
				IsAlreadyWithPlayer = true,
				IsMainHero = true,
				IsInCart = true
			};
			this.PartiesInCart.Add(this._mainPartyItem);
			foreach (ArmyManagementItemVM armyManagementItemVM in this.PartyList)
			{
				if (MobileParty.MainParty.Army != null && armyManagementItemVM.Party.Army == MobileParty.MainParty.Army && armyManagementItemVM.Party != MobileParty.MainParty)
				{
					armyManagementItemVM.Cost = 0;
					armyManagementItemVM.IsAlreadyWithPlayer = true;
					armyManagementItemVM.IsInCart = true;
					this.PartiesInCart.Add(armyManagementItemVM);
				}
			}
			this.CalculateCohesion();
			this.CanBoostCohesion = (this.PlayerHasArmy && this.NewCohesion < 100);
			if (MobileParty.MainParty.Army != null)
			{
				this.CohesionBoostCost = Campaign.Current.Models.ArmyManagementCalculationModel.GetCohesionBoostInfluenceCost(MobileParty.MainParty.Army, 10);
			}
			this._initialInfluence = Hero.MainHero.Clan.Influence;
			this.OnRefresh();
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.ArmyManagement));
			this.SortControllerVM = new ArmyManagementSortControllerVM(this._partyList);
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0006EB1C File Offset: 0x0006CD1C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = GameTexts.FindText("str_army_management", null).ToString();
			this.BoostTitleText = GameTexts.FindText("str_boost_cohesion", null).ToString();
			this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.DistanceText = GameTexts.FindText("str_distance", null).ToString();
			this.CostText = GameTexts.FindText("str_cost", null).ToString();
			this.StrengthText = GameTexts.FindText("str_men_numbersign", null).ToString();
			this.LordsText = GameTexts.FindText("str_leader", null).ToString();
			this.ClanText = GameTexts.FindText("str_clans", null).ToString();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.OwnerText = GameTexts.FindText("str_party", null).ToString();
			this.DisbandArmyText = GameTexts.FindText("str_disband_army", null).ToString();
			this._playerDoesntHaveEnoughInfluenceStr = GameTexts.FindText("str_warning_you_dont_have_enough_influence", null).ToString();
			GameTexts.SetVariable("TOTAL_INFLUENCE", MathF.Round(Hero.MainHero.Clan.Influence));
			this.TotalInfluence = GameTexts.FindText("str_total_influence", null).ToString();
			GameTexts.SetVariable("NUMBER", 10);
			this.CohesionBoostAmountText = GameTexts.FindText("str_plus_with_number", null).ToString();
			this.PartyList.ApplyActionOnAllItems(delegate(ArmyManagementItemVM x)
			{
				x.RefreshValues();
			});
			this.PartiesInCart.ApplyActionOnAllItems(delegate(ArmyManagementItemVM x)
			{
				x.RefreshValues();
			});
			this.TutorialNotification.RefreshValues();
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0006ED04 File Offset: 0x0006CF04
		private void CalculateCohesion()
		{
			if (MobileParty.MainParty.Army != null)
			{
				this.Cohesion = (int)MobileParty.MainParty.Army.Cohesion;
				this.NewCohesion = MathF.Min(this.Cohesion + this._boostedCohesion, 100);
				ArmyManagementCalculationModel armyManagementCalculationModel = Campaign.Current.Models.ArmyManagementCalculationModel;
				this._currentParties.Clear();
				foreach (ArmyManagementItemVM armyManagementItemVM in this.PartiesInCart)
				{
					if (!armyManagementItemVM.Party.IsMainParty)
					{
						this._currentParties.Add(armyManagementItemVM.Party);
						if (!armyManagementItemVM.IsAlreadyWithPlayer)
						{
							this.NewCohesion = armyManagementCalculationModel.CalculateNewCohesion(MobileParty.MainParty.Army, armyManagementItemVM.Party.Party, this.NewCohesion, 1);
						}
					}
				}
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0006EDF4 File Offset: 0x0006CFF4
		private void OnFocus(ArmyManagementItemVM focusedItem)
		{
			this.FocusedItem = focusedItem;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0006EE00 File Offset: 0x0006D000
		private void OnAddToCart(ArmyManagementItemVM armyItem)
		{
			if (!this.PartiesInCart.Contains(armyItem))
			{
				this.PartiesInCart.Add(armyItem);
				armyItem.IsInCart = true;
				Game.Current.EventManager.TriggerEvent<PartyAddedToArmyByPlayerEvent>(new PartyAddedToArmyByPlayerEvent(armyItem.Party));
				if (this._partiesToRemove.Contains(armyItem))
				{
					this._partiesToRemove.Remove(armyItem);
				}
				if (armyItem.IsAlreadyWithPlayer)
				{
					armyItem.CanJoinBackWithoutCost = false;
				}
				this.TotalCost += armyItem.Cost;
			}
			this.CalculateCohesion();
			this.OnRefresh();
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0006EE94 File Offset: 0x0006D094
		private void OnRemove(ArmyManagementItemVM armyItem)
		{
			if (this.PartiesInCart.Contains(armyItem))
			{
				this.PartiesInCart.Remove(armyItem);
				armyItem.IsInCart = false;
				this._partiesToRemove.Add(armyItem);
				if (armyItem.IsAlreadyWithPlayer)
				{
					armyItem.CanJoinBackWithoutCost = true;
				}
				this.TotalCost -= armyItem.Cost;
			}
			this.CalculateCohesion();
			this.OnRefresh();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0006EF00 File Offset: 0x0006D100
		private void ApplyCohesionChange()
		{
			if (MobileParty.MainParty.Army != null)
			{
				int num = this.NewCohesion - this.Cohesion;
				MobileParty.MainParty.Army.BoostCohesionWithInfluence((float)num, this._influenceSpentForCohesionBoosting);
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0006EF40 File Offset: 0x0006D140
		private void OnBoostCohesion()
		{
			Army army = MobileParty.MainParty.Army;
			if (army != null && army.Cohesion < (float)100)
			{
				if (Hero.MainHero.Clan.Influence >= (float)(this.CohesionBoostCost + this.TotalCost))
				{
					this.NewCohesion += 10;
					this.TotalCost += this.CohesionBoostCost;
					this._boostedCohesion += 10;
					this._influenceSpentForCohesionBoosting += this.CohesionBoostCost;
					this.OnRefresh();
					return;
				}
				MBInformationManager.AddQuickInformation(new TextObject("{=Xmw93W6a}Not Enough Influence", null), 0, null, "");
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0006EFF0 File Offset: 0x0006D1F0
		private void OnRefresh()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			float num4 = 0f;
			foreach (ArmyManagementItemVM armyManagementItemVM in this.PartiesInCart)
			{
				num2++;
				num += Campaign.Current.Models.ArmyManagementCalculationModel.GetPartyStrength(armyManagementItemVM.Party.Party);
				if (armyManagementItemVM.IsAlreadyWithPlayer)
				{
					num4 += armyManagementItemVM.Party.Food;
					num3 += (int)armyManagementItemVM.Party.Morale;
				}
			}
			this.TotalStrength = num;
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_total_cost", null).ToString());
			this.TotalCostText = GameTexts.FindText("str_LEFT_colon", null).ToString();
			GameTexts.SetVariable("LEFT", this.TotalCost.ToString());
			GameTexts.SetVariable("RIGHT", ((int)Hero.MainHero.Clan.Influence).ToString());
			this.TotalCostNumbersText = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
			GameTexts.SetVariable("NUM", num2);
			this.TotalLords = GameTexts.FindText("str_NUM_lords", null).ToString();
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_strength", null).ToString());
			this.TotalStrengthText = GameTexts.FindText("str_LEFT_colon", null).ToString();
			this.CanCreateArmy = ((float)this.TotalCost <= Hero.MainHero.Clan.Influence && num2 > 1);
			bool playerHasArmy;
			if (MobileParty.MainParty.Army != null)
			{
				if (this._partiesToRemove.Count > 0)
				{
					playerHasArmy = (this.PartiesInCart.Count((ArmyManagementItemVM p) => p.IsAlreadyWithPlayer) >= 1);
				}
				else
				{
					playerHasArmy = true;
				}
			}
			else
			{
				playerHasArmy = false;
			}
			this.PlayerHasArmy = playerHasArmy;
			this.CanBoostCohesion = (this.PlayerHasArmy && 100 - this.NewCohesion >= 10);
			if (this.CanBoostCohesion)
			{
				TextObject textObject = new TextObject("{=s5b77f0H}Add +{BOOSTAMOUNT} cohesion to your army", null);
				textObject.SetTextVariable("BOOSTAMOUNT", 10);
				this.BoostCohesionHint.HintText = new TextObject("{=!}" + textObject.ToString(), null);
			}
			else if (100 - this.NewCohesion >= 10)
			{
				TextObject textObject2 = new TextObject("{=rsHPaaYZ}Cohesion needs to be lower than {MINAMOUNT} to boost", null);
				textObject2.SetTextVariable("MINAMOUNT", 90);
				this.BoostCohesionHint.HintText = new TextObject("{=!}" + textObject2.ToString(), null);
			}
			else
			{
				this.BoostCohesionHint.HintText = new TextObject("{=Ioiqzz4E}You need to be in an army to boost cohesion", null);
			}
			if (MobileParty.MainParty.Army != null)
			{
				this.CohesionText = GameTexts.FindText("str_cohesion", null).ToString();
				num3 += (int)MobileParty.MainParty.Morale;
				num4 += MobileParty.MainParty.Food;
			}
			this.MoraleText = num3.ToString();
			this.FoodText = MathF.Round(num4, 1).ToString();
			this.UpdateTooltips();
			this.PartiesInCart.Sort(this._itemComparer);
			TextObject hintText;
			this.CanDisbandArmy = this.GetCanDisbandArmyWithReason(out hintText);
			this.DisbandArmyHint.HintText = hintText;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0006F34C File Offset: 0x0006D54C
		private bool GetCanDisbandArmyWithReason(out TextObject disabledReason)
		{
			if (MobileParty.MainParty.Army == null)
			{
				disabledReason = new TextObject("{=iSZTOeYH}No army to disband.", null);
				return false;
			}
			if (MobileParty.MainParty.MapEvent != null)
			{
				disabledReason = new TextObject("{=uipNpzVw}Cannot disband the army right now.", null);
				return false;
			}
			if (PlayerSiege.PlayerSiegeEvent != null)
			{
				disabledReason = GameTexts.FindText("str_action_disabled_reason_siege", null);
				return false;
			}
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0006F3BC File Offset: 0x0006D5BC
		private void UpdateTooltips()
		{
			if (this.PlayerHasArmy)
			{
				this.CohesionHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetArmyCohesionTooltip(PartyBase.MainParty.MobileParty.Army));
				PartyBase.MainParty.MobileParty.Army.RecalculateArmyMorale();
				MathF.Round(PartyBase.MainParty.MobileParty.Army.Morale, 1).ToString("0.0");
				MBTextManager.SetTextVariable("BASE_EFFECT", MathF.Round(MobileParty.MainParty.Morale, 1).ToString("0.0"), false);
				MBTextManager.SetTextVariable("STR1", "", false);
				MBTextManager.SetTextVariable("STR2", "", false);
				MBTextManager.SetTextVariable("ARMY_MORALE", MobileParty.MainParty.Army.Morale);
				foreach (MobileParty mobileParty in MobileParty.MainParty.Army.Parties)
				{
					MBTextManager.SetTextVariable("STR1", GameTexts.FindText("str_STR1_STR2", null).ToString(), false);
					MBTextManager.SetTextVariable("PARTY_NAME", mobileParty.Name, false);
					MBTextManager.SetTextVariable("PARTY_MORALE", (int)mobileParty.Morale);
					MBTextManager.SetTextVariable("STR2", GameTexts.FindText("str_new_morale_item_line", null), false);
				}
				MBTextManager.SetTextVariable("ARMY_MORALE_ITEMS", GameTexts.FindText("str_STR1_STR2", null).ToString(), false);
				this.MoraleHint.HintText = GameTexts.FindText("str_army_morale_tooltip", null);
			}
			else
			{
				GameTexts.SetVariable("reg1", (int)MobileParty.MainParty.Morale);
				this.MoraleHint.HintText = GameTexts.FindText("str_morale_reg1", null);
			}
			this.DoneHint.HintText = new TextObject("{=!}" + (this.CanAffordInfluenceCost ? null : this._playerDoesntHaveEnoughInfluenceStr), null);
			MBTextManager.SetTextVariable("newline", "\n", false);
			MBTextManager.SetTextVariable("DAILY_FOOD_CONSUMPTION", MobileParty.MainParty.FoodChange);
			this.FoodHint.HintText = GameTexts.FindText("str_food_consumption_tooltip", null);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0006F5FC File Offset: 0x0006D7FC
		public void ExecuteDone()
		{
			if (this.CanAffordInfluenceCost)
			{
				if (this.NewCohesion > this.Cohesion)
				{
					this.ApplyCohesionChange();
				}
				if (this.PartiesInCart.Count > 1 && MobileParty.MainParty.MapFaction.IsKingdomFaction)
				{
					if (MobileParty.MainParty.Army == null)
					{
						((Kingdom)MobileParty.MainParty.MapFaction).CreateArmy(Hero.MainHero, Hero.MainHero.HomeSettlement, Army.ArmyTypes.Patrolling);
					}
					foreach (ArmyManagementItemVM armyManagementItemVM in this.PartiesInCart)
					{
						if (armyManagementItemVM.Party != MobileParty.MainParty)
						{
							armyManagementItemVM.Party.Army = MobileParty.MainParty.Army;
							SetPartyAiAction.GetActionForEscortingParty(armyManagementItemVM.Party, MobileParty.MainParty);
						}
					}
					ChangeClanInfluenceAction.Apply(Clan.PlayerClan, (float)(-(float)(this.TotalCost - this._influenceSpentForCohesionBoosting)));
				}
				if (this._partiesToRemove.Count > 0)
				{
					bool flag = false;
					foreach (ArmyManagementItemVM armyManagementItemVM2 in this._partiesToRemove)
					{
						if (armyManagementItemVM2.Party == MobileParty.MainParty)
						{
							armyManagementItemVM2.Party.Army = null;
							flag = true;
						}
					}
					if (!flag)
					{
						foreach (ArmyManagementItemVM armyManagementItemVM3 in this._partiesToRemove)
						{
							Army army = MobileParty.MainParty.Army;
							if (army != null && army.Parties.Contains(armyManagementItemVM3.Party))
							{
								armyManagementItemVM3.Party.Army = null;
							}
						}
					}
					this._partiesToRemove.Clear();
				}
				this._onClose();
				CampaignEventDispatcher.Instance.OnArmyOverlaySetDirty();
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0006F7F0 File Offset: 0x0006D9F0
		public void ExecuteCancel()
		{
			ChangeClanInfluenceAction.Apply(Clan.PlayerClan, this._initialInfluence - Clan.PlayerClan.Influence);
			this._onClose();
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0006F818 File Offset: 0x0006DA18
		public void ExecuteReset()
		{
			foreach (ArmyManagementItemVM armyManagementItemVM in this.PartiesInCart.ToList<ArmyManagementItemVM>())
			{
				this.OnRemove(armyManagementItemVM);
				armyManagementItemVM.UpdateEligibility();
			}
			this.PartiesInCart.Add(this._mainPartyItem);
			foreach (ArmyManagementItemVM armyManagementItemVM2 in this.PartyList)
			{
				if (armyManagementItemVM2.IsAlreadyWithPlayer)
				{
					this.PartiesInCart.Add(armyManagementItemVM2);
					armyManagementItemVM2.IsInCart = true;
					armyManagementItemVM2.CanJoinBackWithoutCost = false;
				}
			}
			this.NewCohesion = this.Cohesion;
			ChangeClanInfluenceAction.Apply(Clan.PlayerClan, this._initialInfluence - Clan.PlayerClan.Influence);
			this.TotalCost = 0;
			this._boostedCohesion = 0;
			this._influenceSpentForCohesionBoosting = 0;
			this._partiesToRemove.Clear();
			this.OnRefresh();
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0006F92C File Offset: 0x0006DB2C
		public void ExecuteDisbandArmy()
		{
			if (this.CanDisbandArmy)
			{
				InformationManager.ShowInquiry(new InquiryData(new TextObject("{=ViYdZUbQ}Disband Army", null).ToString(), new TextObject("{=kqeA8rjL}Are you sure you want to disband your army?", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					this.DisbandArmy();
				}, null, "", 0f, null, null, null), false, false);
			}
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0006F9A9 File Offset: 0x0006DBA9
		public void ExecuteBoostCohesionManual()
		{
			this.OnBoostCohesion();
			Game.Current.EventManager.TriggerEvent<ArmyCohesionBoostedByPlayerEvent>(new ArmyCohesionBoostedByPlayerEvent());
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0006F9C8 File Offset: 0x0006DBC8
		private void DisbandArmy()
		{
			foreach (ArmyManagementItemVM armyItem in this.PartiesInCart.ToList<ArmyManagementItemVM>())
			{
				this.OnRemove(armyItem);
			}
			this.ExecuteDone();
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006FA28 File Offset: 0x0006DC28
		private void OnCloseBoost()
		{
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.ArmyManagement));
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0006FA40 File Offset: 0x0006DC40
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = string.Empty;
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = this._latestTutorialElementID;
				}
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0006FAA0 File Offset: 0x0006DCA0
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM resetInputKey = this.ResetInputKey;
			if (resetInputKey != null)
			{
				resetInputKey.OnFinalize();
			}
			InputKeyItemVM removeInputKey = this.RemoveInputKey;
			if (removeInputKey == null)
			{
				return;
			}
			removeInputKey.OnFinalize();
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0006FB11 File Offset: 0x0006DD11
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x0006FB19 File Offset: 0x0006DD19
		[DataSourceProperty]
		public ElementNotificationVM TutorialNotification
		{
			get
			{
				return this._tutorialNotification;
			}
			set
			{
				if (value != this._tutorialNotification)
				{
					this._tutorialNotification = value;
					base.OnPropertyChangedWithValue<ElementNotificationVM>(value, "TutorialNotification");
				}
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0006FB37 File Offset: 0x0006DD37
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0006FB3F File Offset: 0x0006DD3F
		[DataSourceProperty]
		public ArmyManagementSortControllerVM SortControllerVM
		{
			get
			{
				return this._sortControllerVM;
			}
			set
			{
				if (value != this._sortControllerVM)
				{
					this._sortControllerVM = value;
					base.OnPropertyChangedWithValue<ArmyManagementSortControllerVM>(value, "SortControllerVM");
				}
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0006FB5D File Offset: 0x0006DD5D
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0006FB65 File Offset: 0x0006DD65
		[DataSourceProperty]
		public string BoostTitleText
		{
			get
			{
				return this._boostTitleText;
			}
			set
			{
				if (value != this._boostTitleText)
				{
					this._boostTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "BoostTitleText");
				}
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0006FB88 File Offset: 0x0006DD88
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0006FB90 File Offset: 0x0006DD90
		[DataSourceProperty]
		public string DisbandArmyText
		{
			get
			{
				return this._disbandArmyText;
			}
			set
			{
				if (value != this._disbandArmyText)
				{
					this._disbandArmyText = value;
					base.OnPropertyChangedWithValue<string>(value, "DisbandArmyText");
				}
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0006FBB3 File Offset: 0x0006DDB3
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x0006FBBB File Offset: 0x0006DDBB
		[DataSourceProperty]
		public string CohesionBoostAmountText
		{
			get
			{
				return this._cohesionBoostAmountText;
			}
			set
			{
				if (value != this._cohesionBoostAmountText)
				{
					this._cohesionBoostAmountText = value;
					base.OnPropertyChangedWithValue<string>(value, "CohesionBoostAmountText");
				}
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0006FBDE File Offset: 0x0006DDDE
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x0006FBE6 File Offset: 0x0006DDE6
		[DataSourceProperty]
		public string DistanceText
		{
			get
			{
				return this._distanceText;
			}
			set
			{
				if (value != this._distanceText)
				{
					this._distanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "DistanceText");
				}
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0006FC09 File Offset: 0x0006DE09
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x0006FC11 File Offset: 0x0006DE11
		[DataSourceProperty]
		public string CostText
		{
			get
			{
				return this._costText;
			}
			set
			{
				if (value != this._costText)
				{
					this._costText = value;
					base.OnPropertyChangedWithValue<string>(value, "CostText");
				}
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0006FC34 File Offset: 0x0006DE34
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x0006FC3C File Offset: 0x0006DE3C
		[DataSourceProperty]
		public string OwnerText
		{
			get
			{
				return this._ownerText;
			}
			set
			{
				if (value != this._ownerText)
				{
					this._ownerText = value;
					base.OnPropertyChangedWithValue<string>(value, "OwnerText");
				}
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0006FC5F File Offset: 0x0006DE5F
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x0006FC67 File Offset: 0x0006DE67
		[DataSourceProperty]
		public string StrengthText
		{
			get
			{
				return this._strengthText;
			}
			set
			{
				if (value != this._strengthText)
				{
					this._strengthText = value;
					base.OnPropertyChangedWithValue<string>(value, "StrengthText");
				}
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0006FC8A File Offset: 0x0006DE8A
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x0006FC92 File Offset: 0x0006DE92
		[DataSourceProperty]
		public string LordsText
		{
			get
			{
				return this._lordsText;
			}
			set
			{
				if (value != this._lordsText)
				{
					this._lordsText = value;
					base.OnPropertyChangedWithValue<string>(value, "LordsText");
				}
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0006FCB5 File Offset: 0x0006DEB5
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x0006FCBD File Offset: 0x0006DEBD
		[DataSourceProperty]
		public string TotalInfluence
		{
			get
			{
				return this._totalInfluence;
			}
			set
			{
				if (value != this._totalInfluence)
				{
					this._totalInfluence = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalInfluence");
				}
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0006FCE0 File Offset: 0x0006DEE0
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x0006FCE8 File Offset: 0x0006DEE8
		[DataSourceProperty]
		public int TotalStrength
		{
			get
			{
				return this._totalStrength;
			}
			set
			{
				if (value != this._totalStrength)
				{
					this._totalStrength = value;
					base.OnPropertyChangedWithValue(value, "TotalStrength");
				}
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0006FD06 File Offset: 0x0006DF06
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0006FD10 File Offset: 0x0006DF10
		[DataSourceProperty]
		public int TotalCost
		{
			get
			{
				return this._totalCost;
			}
			set
			{
				if (value != this._totalCost)
				{
					this._totalCost = value;
					this.CanAffordInfluenceCost = (this.TotalCost <= 0 || (float)this.TotalCost <= Hero.MainHero.Clan.Influence);
					base.OnPropertyChangedWithValue(value, "TotalCost");
				}
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0006FD66 File Offset: 0x0006DF66
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x0006FD6E File Offset: 0x0006DF6E
		[DataSourceProperty]
		public string TotalLords
		{
			get
			{
				return this._totalLords;
			}
			set
			{
				if (value != this._totalLords)
				{
					this._totalLords = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalLords");
				}
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0006FD91 File Offset: 0x0006DF91
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x0006FD99 File Offset: 0x0006DF99
		[DataSourceProperty]
		public bool CanCreateArmy
		{
			get
			{
				return this._canCreateArmy;
			}
			set
			{
				if (value != this._canCreateArmy)
				{
					this._canCreateArmy = value;
					base.OnPropertyChangedWithValue(value, "CanCreateArmy");
				}
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0006FDB7 File Offset: 0x0006DFB7
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x0006FDBF File Offset: 0x0006DFBF
		[DataSourceProperty]
		public bool CanBoostCohesion
		{
			get
			{
				return this._canBoostCohesion;
			}
			set
			{
				if (value != this._canBoostCohesion)
				{
					this._canBoostCohesion = value;
					base.OnPropertyChangedWithValue(value, "CanBoostCohesion");
				}
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0006FDDD File Offset: 0x0006DFDD
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x0006FDE5 File Offset: 0x0006DFE5
		[DataSourceProperty]
		public bool CanDisbandArmy
		{
			get
			{
				return this._canDisbandArmy;
			}
			set
			{
				if (value != this._canDisbandArmy)
				{
					this._canDisbandArmy = value;
					base.OnPropertyChangedWithValue(value, "CanDisbandArmy");
				}
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0006FE03 File Offset: 0x0006E003
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0006FE0B File Offset: 0x0006E00B
		[DataSourceProperty]
		public bool CanAffordInfluenceCost
		{
			get
			{
				return this._canAffordInfluenceCost;
			}
			set
			{
				if (value != this._canAffordInfluenceCost)
				{
					this._canAffordInfluenceCost = value;
					base.OnPropertyChangedWithValue(value, "CanAffordInfluenceCost");
				}
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0006FE29 File Offset: 0x0006E029
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0006FE31 File Offset: 0x0006E031
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0006FE54 File Offset: 0x0006E054
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0006FE5C File Offset: 0x0006E05C
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

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0006FE7F File Offset: 0x0006E07F
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0006FE87 File Offset: 0x0006E087
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

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0006FEAA File Offset: 0x0006E0AA
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x0006FEB2 File Offset: 0x0006E0B2
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

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0006FED5 File Offset: 0x0006E0D5
		// (set) Token: 0x06001F56 RID: 8022 RVA: 0x0006FEDD File Offset: 0x0006E0DD
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

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0006FF00 File Offset: 0x0006E100
		// (set) Token: 0x06001F58 RID: 8024 RVA: 0x0006FF08 File Offset: 0x0006E108
		[DataSourceProperty]
		public ArmyManagementItemVM FocusedItem
		{
			get
			{
				return this._focusedItem;
			}
			set
			{
				if (value != this._focusedItem)
				{
					this._focusedItem = value;
					base.OnPropertyChangedWithValue<ArmyManagementItemVM>(value, "FocusedItem");
				}
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0006FF26 File Offset: 0x0006E126
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x0006FF2E File Offset: 0x0006E12E
		[DataSourceProperty]
		public MBBindingList<ArmyManagementItemVM> PartyList
		{
			get
			{
				return this._partyList;
			}
			set
			{
				if (value != this._partyList)
				{
					this._partyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<ArmyManagementItemVM>>(value, "PartyList");
				}
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0006FF4C File Offset: 0x0006E14C
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x0006FF54 File Offset: 0x0006E154
		[DataSourceProperty]
		public MBBindingList<ArmyManagementItemVM> PartiesInCart
		{
			get
			{
				return this._partiesInCart;
			}
			set
			{
				if (value != this._partiesInCart)
				{
					this._partiesInCart = value;
					base.OnPropertyChangedWithValue<MBBindingList<ArmyManagementItemVM>>(value, "PartiesInCart");
				}
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0006FF72 File Offset: 0x0006E172
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x0006FF7A File Offset: 0x0006E17A
		[DataSourceProperty]
		public string TotalStrengthText
		{
			get
			{
				return this._totalStrengthText;
			}
			set
			{
				if (value != this._totalStrengthText)
				{
					this._totalStrengthText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalStrengthText");
				}
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0006FF9D File Offset: 0x0006E19D
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0006FFA5 File Offset: 0x0006E1A5
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

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0006FFC8 File Offset: 0x0006E1C8
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x0006FFD0 File Offset: 0x0006E1D0
		[DataSourceProperty]
		public string TotalCostNumbersText
		{
			get
			{
				return this._totalCostNumbersText;
			}
			set
			{
				if (value != this._totalCostNumbersText)
				{
					this._totalCostNumbersText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalCostNumbersText");
				}
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0006FFF3 File Offset: 0x0006E1F3
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x0006FFFB File Offset: 0x0006E1FB
		[DataSourceProperty]
		public string CohesionText
		{
			get
			{
				return this._cohesionText;
			}
			set
			{
				if (value != this._cohesionText)
				{
					this._cohesionText = value;
					base.OnPropertyChangedWithValue<string>(value, "CohesionText");
				}
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0007001E File Offset: 0x0006E21E
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x00070026 File Offset: 0x0006E226
		[DataSourceProperty]
		public int Cohesion
		{
			get
			{
				return this._cohesion;
			}
			set
			{
				if (value != this._cohesion)
				{
					this._cohesion = value;
					base.OnPropertyChangedWithValue(value, "Cohesion");
				}
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00070044 File Offset: 0x0006E244
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x0007004C File Offset: 0x0006E24C
		[DataSourceProperty]
		public int CohesionBoostCost
		{
			get
			{
				return this._cohesionBoostCost;
			}
			set
			{
				if (value != this._cohesionBoostCost)
				{
					this._cohesionBoostCost = value;
					base.OnPropertyChangedWithValue(value, "CohesionBoostCost");
				}
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0007006A File Offset: 0x0006E26A
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x00070072 File Offset: 0x0006E272
		[DataSourceProperty]
		public bool PlayerHasArmy
		{
			get
			{
				return this._playerHasArmy;
			}
			set
			{
				if (value != this._playerHasArmy)
				{
					this._playerHasArmy = value;
					base.OnPropertyChangedWithValue(value, "PlayerHasArmy");
				}
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x00070090 File Offset: 0x0006E290
		// (set) Token: 0x06001F6C RID: 8044 RVA: 0x00070098 File Offset: 0x0006E298
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

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000700BB File Offset: 0x0006E2BB
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x000700C3 File Offset: 0x0006E2C3
		[DataSourceProperty]
		public string FoodText
		{
			get
			{
				return this._foodText;
			}
			set
			{
				if (value != this._foodText)
				{
					this._foodText = value;
					base.OnPropertyChangedWithValue<string>(value, "FoodText");
				}
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000700E6 File Offset: 0x0006E2E6
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x000700EE File Offset: 0x0006E2EE
		[DataSourceProperty]
		public int NewCohesion
		{
			get
			{
				return this._newCohesion;
			}
			set
			{
				if (value != this._newCohesion)
				{
					this._newCohesion = value;
					base.OnPropertyChangedWithValue(value, "NewCohesion");
				}
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0007010C File Offset: 0x0006E30C
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x00070114 File Offset: 0x0006E314
		[DataSourceProperty]
		public BasicTooltipViewModel CohesionHint
		{
			get
			{
				return this._cohesionHint;
			}
			set
			{
				if (value != this._cohesionHint)
				{
					this._cohesionHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CohesionHint");
				}
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00070132 File Offset: 0x0006E332
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x0007013A File Offset: 0x0006E33A
		[DataSourceProperty]
		public HintViewModel MoraleHint
		{
			get
			{
				return this._moraleHint;
			}
			set
			{
				if (value != this._moraleHint)
				{
					this._moraleHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "MoraleHint");
				}
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00070158 File Offset: 0x0006E358
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x00070160 File Offset: 0x0006E360
		[DataSourceProperty]
		public HintViewModel BoostCohesionHint
		{
			get
			{
				return this._boostCohesionHint;
			}
			set
			{
				if (value != this._boostCohesionHint)
				{
					this._boostCohesionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BoostCohesionHint");
				}
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x0007017E File Offset: 0x0006E37E
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x00070186 File Offset: 0x0006E386
		[DataSourceProperty]
		public HintViewModel DisbandArmyHint
		{
			get
			{
				return this._disbandArmyHint;
			}
			set
			{
				if (value != this._disbandArmyHint)
				{
					this._disbandArmyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisbandArmyHint");
				}
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000701A4 File Offset: 0x0006E3A4
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x000701AC File Offset: 0x0006E3AC
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

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x000701CA File Offset: 0x0006E3CA
		// (set) Token: 0x06001F7C RID: 8060 RVA: 0x000701D2 File Offset: 0x0006E3D2
		[DataSourceProperty]
		public HintViewModel FoodHint
		{
			get
			{
				return this._foodHint;
			}
			set
			{
				if (value != this._foodHint)
				{
					this._foodHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FoodHint");
				}
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000701F0 File Offset: 0x0006E3F0
		public void SetResetInputKey(HotKey hotKey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000701FF File Offset: 0x0006E3FF
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0007020E File Offset: 0x0006E40E
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0007021D File Offset: 0x0006E41D
		public void SetRemoveInputKey(HotKey hotKey)
		{
			this.RemoveInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0007022C File Offset: 0x0006E42C
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x00070234 File Offset: 0x0006E434
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

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x00070252 File Offset: 0x0006E452
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x0007025A File Offset: 0x0006E45A
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

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x00070278 File Offset: 0x0006E478
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x00070280 File Offset: 0x0006E480
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

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0007029E File Offset: 0x0006E49E
		// (set) Token: 0x06001F88 RID: 8072 RVA: 0x000702A8 File Offset: 0x0006E4A8
		[DataSourceProperty]
		public InputKeyItemVM RemoveInputKey
		{
			get
			{
				return this._removeInputKey;
			}
			set
			{
				if (value != this._removeInputKey)
				{
					this._removeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RemoveInputKey");
					foreach (ArmyManagementItemVM armyManagementItemVM in this.PartyList)
					{
						armyManagementItemVM.RemoveInputKey = value;
					}
				}
			}
		}

		// Token: 0x04000EA7 RID: 3751
		private readonly Action _onClose;

		// Token: 0x04000EA8 RID: 3752
		private readonly ArmyManagementItemVM _mainPartyItem;

		// Token: 0x04000EA9 RID: 3753
		private readonly ArmyManagementVM.ManagementItemComparer _itemComparer;

		// Token: 0x04000EAA RID: 3754
		private readonly float _initialInfluence;

		// Token: 0x04000EAB RID: 3755
		private string _latestTutorialElementID;

		// Token: 0x04000EAC RID: 3756
		private string _playerDoesntHaveEnoughInfluenceStr;

		// Token: 0x04000EAD RID: 3757
		private const int _cohesionBoostAmount = 10;

		// Token: 0x04000EAE RID: 3758
		private int _influenceSpentForCohesionBoosting;

		// Token: 0x04000EAF RID: 3759
		private int _boostedCohesion;

		// Token: 0x04000EB0 RID: 3760
		private string _titleText;

		// Token: 0x04000EB1 RID: 3761
		private string _boostTitleText;

		// Token: 0x04000EB2 RID: 3762
		private string _cancelText;

		// Token: 0x04000EB3 RID: 3763
		private string _doneText;

		// Token: 0x04000EB4 RID: 3764
		private bool _canCreateArmy;

		// Token: 0x04000EB5 RID: 3765
		private bool _canBoostCohesion;

		// Token: 0x04000EB6 RID: 3766
		private List<MobileParty> _currentParties;

		// Token: 0x04000EB7 RID: 3767
		private ArmyManagementItemVM _focusedItem;

		// Token: 0x04000EB8 RID: 3768
		private MBBindingList<ArmyManagementItemVM> _partyList;

		// Token: 0x04000EB9 RID: 3769
		private MBBindingList<ArmyManagementItemVM> _partiesInCart;

		// Token: 0x04000EBA RID: 3770
		private MBBindingList<ArmyManagementItemVM> _partiesToRemove;

		// Token: 0x04000EBB RID: 3771
		private ArmyManagementSortControllerVM _sortControllerVM;

		// Token: 0x04000EBC RID: 3772
		private int _totalStrength;

		// Token: 0x04000EBD RID: 3773
		private int _totalCost;

		// Token: 0x04000EBE RID: 3774
		private int _cohesion;

		// Token: 0x04000EBF RID: 3775
		private int _cohesionBoostCost;

		// Token: 0x04000EC0 RID: 3776
		private string _cohesionText;

		// Token: 0x04000EC1 RID: 3777
		private int _newCohesion;

		// Token: 0x04000EC2 RID: 3778
		private string _totalStrengthText;

		// Token: 0x04000EC3 RID: 3779
		private string _totalCostText;

		// Token: 0x04000EC4 RID: 3780
		private string _totalCostNumbersText;

		// Token: 0x04000EC5 RID: 3781
		private string _totalInfluence;

		// Token: 0x04000EC6 RID: 3782
		private string _totalLords;

		// Token: 0x04000EC7 RID: 3783
		private string _costText;

		// Token: 0x04000EC8 RID: 3784
		private string _strengthText;

		// Token: 0x04000EC9 RID: 3785
		private string _lordsText;

		// Token: 0x04000ECA RID: 3786
		private string _distanceText;

		// Token: 0x04000ECB RID: 3787
		private string _clanText;

		// Token: 0x04000ECC RID: 3788
		private string _ownerText;

		// Token: 0x04000ECD RID: 3789
		private string _nameText;

		// Token: 0x04000ECE RID: 3790
		private string _disbandArmyText;

		// Token: 0x04000ECF RID: 3791
		private string _cohesionBoostAmountText;

		// Token: 0x04000ED0 RID: 3792
		private bool _playerHasArmy;

		// Token: 0x04000ED1 RID: 3793
		private bool _canDisbandArmy;

		// Token: 0x04000ED2 RID: 3794
		private bool _canAffordInfluenceCost;

		// Token: 0x04000ED3 RID: 3795
		private string _moraleText;

		// Token: 0x04000ED4 RID: 3796
		private string _foodText;

		// Token: 0x04000ED5 RID: 3797
		private BasicTooltipViewModel _cohesionHint;

		// Token: 0x04000ED6 RID: 3798
		private HintViewModel _moraleHint;

		// Token: 0x04000ED7 RID: 3799
		private HintViewModel _foodHint;

		// Token: 0x04000ED8 RID: 3800
		private HintViewModel _boostCohesionHint;

		// Token: 0x04000ED9 RID: 3801
		private HintViewModel _disbandArmyHint;

		// Token: 0x04000EDA RID: 3802
		private HintViewModel _doneHint;

		// Token: 0x04000EDB RID: 3803
		public ElementNotificationVM _tutorialNotification;

		// Token: 0x04000EDC RID: 3804
		private InputKeyItemVM _resetInputKey;

		// Token: 0x04000EDD RID: 3805
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000EDE RID: 3806
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000EDF RID: 3807
		private InputKeyItemVM _removeInputKey;

		// Token: 0x020002AA RID: 682
		public class ManagementItemComparer : IComparer<ArmyManagementItemVM>
		{
			// Token: 0x0600242F RID: 9263 RVA: 0x000780D4 File Offset: 0x000762D4
			public int Compare(ArmyManagementItemVM x, ArmyManagementItemVM y)
			{
				if (x.IsMainHero)
				{
					return -1;
				}
				return y.IsAlreadyWithPlayer.CompareTo(x.IsAlreadyWithPlayer);
			}
		}
	}
}
