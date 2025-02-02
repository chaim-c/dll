using System;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x02000024 RID: 36
	public class PartyCharacterVM : ViewModel
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00011AD5 File Offset: 0x0000FCD5
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00011ADD File Offset: 0x0000FCDD
		public TroopRoster Troops { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00011AE6 File Offset: 0x0000FCE6
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00011AEE File Offset: 0x0000FCEE
		public string StringId { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00011AF7 File Offset: 0x0000FCF7
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00011B00 File Offset: 0x0000FD00
		public TroopRosterElement Troop
		{
			get
			{
				return this._troop;
			}
			set
			{
				this._troop = value;
				this.Character = value.Character;
				this.TroopID = this.Character.StringId;
				this.CheckTransferAmountDefaultValue();
				this.TroopXPTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetTroopXPTooltip(value));
				this.TroopConformityTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetTroopConformityTooltip(value));
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00011B7C File Offset: 0x0000FD7C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00011B84 File Offset: 0x0000FD84
		public CharacterObject Character
		{
			get
			{
				return this._character;
			}
			set
			{
				if (this._character != value)
				{
					this._character = value;
					CharacterCode characterCode = this.GetCharacterCode(value, this.Type, this.Side);
					this.Code = new ImageIdentifierVM(characterCode);
					CharacterObject[] upgradeTargets = this._character.UpgradeTargets;
					if (upgradeTargets != null && upgradeTargets.Length != 0)
					{
						this.Upgrades = new MBBindingList<UpgradeTargetVM>();
						for (int i = 0; i < this._character.UpgradeTargets.Length; i++)
						{
							CharacterCode characterCode2 = this.GetCharacterCode(this._character.UpgradeTargets[i], this.Type, this.Side);
							this.Upgrades.Add(new UpgradeTargetVM(i, value, characterCode2, new Action<int, int>(this.Upgrade), new Action<UpgradeTargetVM>(this.FocusUpgrade)));
						}
						this.HasMoreThanTwoUpgrades = (this.Upgrades.Count > 2);
					}
				}
				this.CheckTransferAmountDefaultValue();
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00011C68 File Offset: 0x0000FE68
		public PartyCharacterVM(PartyScreenLogic partyScreenLogic, PartyVM partyVm, TroopRoster troops, int index, PartyScreenLogic.TroopType type, PartyScreenLogic.PartyRosterSide side, bool isTroopTransferrable)
		{
			this.Upgrades = new MBBindingList<UpgradeTargetVM>();
			this._partyScreenLogic = partyScreenLogic;
			this._partyVm = partyVm;
			this.Troops = troops;
			this.Side = side;
			this.Type = type;
			this.Troop = troops.GetElementCopyAtIndex(index);
			this.Index = index;
			this.IsHero = this.Troop.Character.IsHero;
			this.IsMainHero = (Hero.MainHero.CharacterObject == this.Troop.Character);
			this.IsPrisoner = (this.Type == PartyScreenLogic.TroopType.Prisoner);
			this.TierIconData = CampaignUIHelper.GetCharacterTierData(this.Troop.Character, true);
			this.TypeIconData = CampaignUIHelper.GetCharacterTypeData(this.Troop.Character, false);
			this.StringId = CampaignUIHelper.GetTroopLockStringID(this.Troop);
			this._initIsTroopTransferable = isTroopTransferrable;
			this.IsTroopTransferrable = this._initIsTroopTransferable;
			this.TradeData = new PartyTradeVM(partyScreenLogic, this.Troop, this.Side, this.IsTroopTransferrable, this.IsPrisoner, new Action<int, bool>(this.OnTradeApplyTransaction));
			this.IsPrisonerOfPlayer = (this.IsPrisoner && this.Side == PartyScreenLogic.PartyRosterSide.Right);
			this.IsHeroPrisonerOfPlayer = (this.IsPrisonerOfPlayer && this.Character.IsHero);
			this.IsExecutable = this._partyScreenLogic.IsExecutable(this.Type, this.Character, this.Side);
			this.IsUpgradableTroop = (this.Side == PartyScreenLogic.PartyRosterSide.Right && !this.IsHero && !this.IsPrisoner && this.Character.UpgradeTargets.Length != 0 && !this._partyScreenLogic.IsTroopUpgradesDisabled);
			this.InitializeUpgrades();
			this.ThrowOnPropertyChanged();
			this.CheckTransferAmountDefaultValue();
			this.UpdateRecruitable();
			this.RefreshValues();
			this.UpdateTransferHint();
			this.SetMoraleCost();
			this.RecruitPrisonerHint = new BasicTooltipViewModel(() => this._partyScreenLogic.GetRecruitableReasonText(this.Troop.Character, this.IsTroopRecruitable, this.Troop.Number, PartyCharacterVM.FiveStackShortcutKeyText, PartyCharacterVM.EntireStackShortcutKeyText));
			this.ExecutePrisonerHint = new BasicTooltipViewModel(() => this._partyScreenLogic.GetExecutableReasonText(this.Troop.Character, this.IsExecutable));
			this.HeroHealthHint = (this.Troop.Character.IsHero ? new BasicTooltipViewModel(() => CampaignUIHelper.GetHeroHealthTooltip(this.Troop.Character.HeroObject)) : null);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00011EBC File Offset: 0x000100BC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.Troop.Character.Name.ToString();
			this.LockHint = new HintViewModel(GameTexts.FindText("str_inventory_lock", null), null);
			MBBindingList<UpgradeTargetVM> upgrades = this.Upgrades;
			if (upgrades != null)
			{
				upgrades.ApplyActionOnAllItems(delegate(UpgradeTargetVM x)
				{
					x.RefreshValues();
				});
			}
			PartyTradeVM tradeData = this.TradeData;
			if (tradeData == null)
			{
				return;
			}
			tradeData.RefreshValues();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00011F44 File Offset: 0x00010144
		private void UpdateTransferHint()
		{
			GameTexts.SetVariable("newline", "\n");
			GameTexts.SetVariable("STR1", "");
			GameTexts.SetVariable("STR2", "");
			if (!string.IsNullOrEmpty(PartyCharacterVM.EntireStackShortcutKeyText))
			{
				GameTexts.SetVariable("KEY_NAME", PartyCharacterVM.EntireStackShortcutKeyText);
				string content = GameTexts.FindText("str_entire_stack_shortcut_transfer_troops", null).ToString();
				GameTexts.SetVariable("STR1", content);
				GameTexts.SetVariable("STR2", "");
				if (this.Number >= 5 && !string.IsNullOrEmpty(PartyCharacterVM.FiveStackShortcutKeyText))
				{
					GameTexts.SetVariable("KEY_NAME", PartyCharacterVM.FiveStackShortcutKeyText);
					string content2 = GameTexts.FindText("str_five_stack_shortcut_transfer_troops", null).ToString();
					GameTexts.SetVariable("STR2", content2);
				}
			}
			string variable = GameTexts.FindText("str_string_newline_string", null).ToString();
			TextObject textObject = GameTexts.FindText("str_string_newline_string", null).CopyTextObject();
			textObject.SetTextVariable("STR2", variable);
			textObject.SetTextVariable("STR1", GameTexts.FindText("str_transfer", null).ToString());
			this.TransferHint = new HintViewModel(textObject, null);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00012068 File Offset: 0x00010268
		private void CheckTransferAmountDefaultValue()
		{
			if (this.TransferAmount == 0 && this.Troop.Character != null && this.Troop.Number > 0)
			{
				this.TransferAmount = 1;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000120A2 File Offset: 0x000102A2
		public void ExecuteSetSelected()
		{
			if (this.Character != null)
			{
				PartyCharacterVM.SetSelected(this);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000120B7 File Offset: 0x000102B7
		public void ExecuteTalk()
		{
			PartyVM partyVm = this._partyVm;
			if (partyVm == null)
			{
				return;
			}
			partyVm.ExecuteTalk();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000120C9 File Offset: 0x000102C9
		public void UpdateTradeData()
		{
			PartyTradeVM tradeData = this.TradeData;
			if (tradeData != null)
			{
				tradeData.UpdateTroopData(this.Troop, this.Side, true);
			}
			this.UpdateTransferHint();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000120F0 File Offset: 0x000102F0
		public void UpdateRecruitable()
		{
			this.MaxConformity = this.Troop.Character.ConformityNeededToRecruitPrisoner;
			int elementXp = PartyBase.MainParty.PrisonRoster.GetElementXp(this.Troop.Character);
			this.CurrentConformity = ((elementXp >= this.Troop.Number * this.MaxConformity) ? this.MaxConformity : (elementXp % this.MaxConformity));
			this.IsRecruitablePrisoner = (!this._character.IsHero && this.Type == PartyScreenLogic.TroopType.Prisoner);
			this.IsTroopRecruitable = this._partyScreenLogic.IsPrisonerRecruitable(this.Type, this.Character, this.Side);
			this.NumOfRecruitablePrisoners = this._partyScreenLogic.GetTroopRecruitableAmount(this.Character);
			GameTexts.SetVariable("LEFT", this.NumOfRecruitablePrisoners);
			GameTexts.SetVariable("RIGHT", this.Troop.Number);
			this.StrNumOfRecruitableTroop = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000121F4 File Offset: 0x000103F4
		private void OnTradeApplyTransaction(int amount, bool isIncreasing)
		{
			this.TransferAmount = amount;
			PartyScreenLogic.PartyRosterSide side = isIncreasing ? PartyScreenLogic.PartyRosterSide.Left : PartyScreenLogic.PartyRosterSide.Right;
			this.ApplyTransfer(this.TransferAmount, side);
			this.IsExecutable = (this._partyScreenLogic.IsExecutable(this.Type, this.Character, this.Side) && this.Troop.Number > 0);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00012258 File Offset: 0x00010458
		public void InitializeUpgrades()
		{
			if (this.IsUpgradableTroop)
			{
				for (int i = 0; i < this.Character.UpgradeTargets.Length; i++)
				{
					CharacterObject characterObject = this.Character.UpgradeTargets[i];
					int level = characterObject.Level;
					int upgradeGoldCost = this.Character.GetUpgradeGoldCost(PartyBase.MainParty, i);
					if (!this.Character.Culture.IsBandit)
					{
						int level2 = this.Character.Level;
					}
					else
					{
						int level3 = this.Character.Level;
					}
					PerkObject requiredPerk;
					bool flag = Campaign.Current.Models.PartyTroopUpgradeModel.DoesPartyHaveRequiredPerksForUpgrade(PartyBase.MainParty, this.Character, characterObject, out requiredPerk);
					int b = flag ? this.Troop.Number : 0;
					bool flag2 = true;
					int numOfCategoryItemPartyHas = this.GetNumOfCategoryItemPartyHas(this._partyScreenLogic.RightOwnerParty.ItemRoster, characterObject.UpgradeRequiresItemFromCategory);
					if (characterObject.UpgradeRequiresItemFromCategory != null)
					{
						flag2 = (numOfCategoryItemPartyHas > 0);
					}
					bool flag3 = Hero.MainHero.Gold + this._partyScreenLogic.CurrentData.PartyGoldChangeAmount >= upgradeGoldCost;
					bool flag4 = level >= this.Character.Level && this.Troop.Xp >= this.Character.GetUpgradeXpCost(PartyBase.MainParty, i) && !this._partyVm.PartyScreenLogic.IsTroopUpgradesDisabled;
					bool flag5 = !flag2 || !flag3;
					int a = this.Troop.Number;
					if (upgradeGoldCost > 0)
					{
						a = (int)MathF.Clamp((float)MathF.Floor((float)(Hero.MainHero.Gold + this._partyScreenLogic.CurrentData.PartyGoldChangeAmount) / (float)upgradeGoldCost), 0f, (float)this.Troop.Number);
					}
					int b2 = (characterObject.UpgradeRequiresItemFromCategory != null) ? numOfCategoryItemPartyHas : this.Troop.Number;
					int num = flag4 ? ((int)MathF.Clamp((float)MathF.Floor((float)this.Troop.Xp / (float)this.Character.GetUpgradeXpCost(PartyBase.MainParty, i)), 0f, (float)this.Troop.Number)) : 0;
					int num2 = MathF.Min(MathF.Min(a, b2), MathF.Min(num, b));
					if (this.Character.Culture.IsBandit)
					{
						flag5 = (flag5 || !Campaign.Current.Models.PartyTroopUpgradeModel.CanPartyUpgradeTroopToTarget(PartyBase.MainParty, this.Character, characterObject));
						num2 = ((!flag4) ? 0 : num2);
					}
					string upgradeHint = CampaignUIHelper.GetUpgradeHint(i, numOfCategoryItemPartyHas, num2, upgradeGoldCost, flag, requiredPerk, this.Character, this.Troop, this._partyScreenLogic.CurrentData.PartyGoldChangeAmount, PartyCharacterVM.EntireStackShortcutKeyText, PartyCharacterVM.FiveStackShortcutKeyText);
					this.Upgrades[i].Refresh(num2, upgradeHint, flag4, flag5, flag2, flag);
					if (i == 0)
					{
						this.UpgradeCostText = upgradeGoldCost.ToString();
						this.HasEnoughGold = flag3;
						this.NumOfReadyToUpgradeTroops = num;
						this.MaxXP = this.Character.GetUpgradeXpCost(PartyBase.MainParty, i);
						this.CurrentXP = ((this.Troop.Xp >= this.Troop.Number * this.MaxXP) ? this.MaxXP : (this.Troop.Xp % this.MaxXP));
					}
				}
				this.AnyUpgradeHasRequirement = this.Upgrades.Any((UpgradeTargetVM x) => x.Requirements.HasItemRequirement || x.Requirements.HasPerkRequirement);
			}
			int num3 = 0;
			foreach (UpgradeTargetVM upgradeTargetVM in this.Upgrades)
			{
				if (upgradeTargetVM.AvailableUpgrades > num3)
				{
					num3 = upgradeTargetVM.AvailableUpgrades;
				}
			}
			this.NumOfUpgradeableTroops = num3;
			this.IsTroopUpgradable = (this.NumOfUpgradeableTroops > 0 && !this._partyVm.PartyScreenLogic.IsTroopUpgradesDisabled);
			GameTexts.SetVariable("LEFT", this.NumOfReadyToUpgradeTroops);
			GameTexts.SetVariable("RIGHT", this.Troop.Number);
			this.StrNumOfUpgradableTroop = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
			base.OnPropertyChanged("AmountOfUpgrades");
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000126C0 File Offset: 0x000108C0
		public void OnTransferred()
		{
			if (this.Side != PartyScreenLogic.PartyRosterSide.Left || this.IsPrisoner)
			{
				this.InitializeUpgrades();
				return;
			}
			PartyCharacterVM partyCharacterVM = this._partyVm.MainPartyTroops.FirstOrDefault((PartyCharacterVM x) => x.Character == this.Character);
			if (partyCharacterVM == null)
			{
				return;
			}
			partyCharacterVM.InitializeUpgrades();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00012700 File Offset: 0x00010900
		public void ThrowOnPropertyChanged()
		{
			base.OnPropertyChanged("Name");
			base.OnPropertyChanged("Number");
			base.OnPropertyChanged("WoundedCount");
			base.OnPropertyChanged("IsTroopTransferrable");
			base.OnPropertyChanged("MaxCount");
			base.OnPropertyChanged("AmountOfUpgrades");
			base.OnPropertyChanged("Level");
			base.OnPropertyChanged("PartyIndex");
			base.OnPropertyChanged("Index");
			base.OnPropertyChanged("TroopNum");
			base.OnPropertyChanged("TransferString");
			base.OnPropertyChanged("CanTalk");
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00012794 File Offset: 0x00010994
		public override bool Equals(object obj)
		{
			PartyCharacterVM partyCharacterVM;
			return obj != null && (partyCharacterVM = (obj as PartyCharacterVM)) != null && ((partyCharacterVM.Character == null && this.Code == null) || partyCharacterVM.Character == this.Character);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000127D2 File Offset: 0x000109D2
		private void ApplyTransfer(int transferAmount, PartyScreenLogic.PartyRosterSide side)
		{
			PartyCharacterVM.OnTransfer(this, -1, transferAmount, side);
			this.ThrowOnPropertyChanged();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000127E8 File Offset: 0x000109E8
		private void ExecuteTransfer()
		{
			this.ApplyTransfer(this.TransferAmount, this.Side);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000127FC File Offset: 0x000109FC
		private void ExecuteTransferAll()
		{
			this.ApplyTransfer(this.Troop.Number, this.Side);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00012823 File Offset: 0x00010A23
		public void ExecuteSetFocused()
		{
			Action<PartyCharacterVM> onFocus = PartyCharacterVM.OnFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(this);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00012835 File Offset: 0x00010A35
		public void ExecuteSetUnfocused()
		{
			Action<PartyCharacterVM> onFocus = PartyCharacterVM.OnFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(null);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00012848 File Offset: 0x00010A48
		public void ExecuteTransferSingle()
		{
			int transferAmount = 1;
			if (this._partyVm.IsEntireStackModifierActive)
			{
				transferAmount = this.Troop.Number;
			}
			else if (this._partyVm.IsFiveStackModifierActive)
			{
				transferAmount = MathF.Min(5, this.Troop.Number);
			}
			this.ApplyTransfer(transferAmount, this.Side);
			this._partyVm.ExecuteRemoveZeroCounts();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000128AF File Offset: 0x00010AAF
		public void ExecuteResetTrade()
		{
			this.TradeData.ExecuteReset();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000128BC File Offset: 0x00010ABC
		public void Upgrade(int upgradeIndex, int maxUpgradeCount)
		{
			PartyVM partyVm = this._partyVm;
			if (partyVm == null)
			{
				return;
			}
			partyVm.ExecuteUpgrade(this, upgradeIndex, maxUpgradeCount);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000128D1 File Offset: 0x00010AD1
		public void FocusUpgrade(UpgradeTargetVM upgrade)
		{
			this._partyVm.CurrentFocusedUpgrade = upgrade;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000128DF File Offset: 0x00010ADF
		public void RecruitAll()
		{
			if (this.IsTroopRecruitable)
			{
				this._partyVm.ExecuteRecruit(this, true);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000128F6 File Offset: 0x00010AF6
		public void ExecuteRecruitTroop()
		{
			if (this.IsTroopRecruitable)
			{
				this._partyVm.ExecuteRecruit(this, false);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00012910 File Offset: 0x00010B10
		public void ExecuteExecuteTroop()
		{
			if (this.IsExecutable)
			{
				if (FaceGen.GetMaturityTypeWithAge(this.Character.HeroObject.BodyProperties.Age) <= BodyMeshMaturityType.Tween)
				{
					return;
				}
				MBInformationManager.ShowSceneNotification(HeroExecutionSceneNotificationData.CreateForPlayerExecutingHero(this.Character.HeroObject, delegate
				{
					this._partyVm.ExecuteExecution();
				}, SceneNotificationData.RelevantContextType.Any));
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00012968 File Offset: 0x00010B68
		public void ExecuteOpenTroopEncyclopedia()
		{
			if (!this.Troop.Character.IsHero)
			{
				if (Campaign.Current.EncyclopediaManager.GetPageOf(typeof(CharacterObject)).IsValidEncyclopediaItem(this.Troop.Character))
				{
					Campaign.Current.EncyclopediaManager.GoToLink(this.Troop.Character.EncyclopediaLink);
					return;
				}
			}
			else if (Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero)).IsValidEncyclopediaItem(this.Troop.Character.HeroObject))
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Troop.Character.HeroObject.EncyclopediaLink);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00012A28 File Offset: 0x00010C28
		private CharacterCode GetCharacterCode(CharacterObject character, PartyScreenLogic.TroopType type, PartyScreenLogic.PartyRosterSide side)
		{
			IFaction faction = null;
			if (type != PartyScreenLogic.TroopType.Prisoner)
			{
				if (side == PartyScreenLogic.PartyRosterSide.Left && this._partyScreenLogic.LeftOwnerParty != null)
				{
					faction = this._partyScreenLogic.LeftOwnerParty.MapFaction;
				}
				else if (this.Side == PartyScreenLogic.PartyRosterSide.Right && this._partyScreenLogic.RightOwnerParty != null)
				{
					faction = this._partyScreenLogic.RightOwnerParty.MapFaction;
				}
			}
			uint color = Color.White.ToUnsignedInteger();
			uint color2 = Color.White.ToUnsignedInteger();
			if (faction != null)
			{
				color = faction.Color;
				color2 = faction.Color2;
			}
			else if (character.Culture != null)
			{
				color = character.Culture.Color;
				color2 = character.Culture.Color2;
			}
			Equipment equipment = character.Equipment;
			string equipmentCode = (equipment != null) ? equipment.CalculateEquipmentCode() : null;
			BodyProperties bodyProperties = character.GetBodyProperties(character.Equipment, -1);
			return CharacterCode.CreateFrom(equipmentCode, bodyProperties, character.IsFemale, character.IsHero, color, color2, character.DefaultFormationClass, character.Race);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00012B18 File Offset: 0x00010D18
		private void SetMoraleCost()
		{
			if (this.IsTroopRecruitable)
			{
				this.RecruitMoraleCostText = Campaign.Current.Models.PrisonerRecruitmentCalculationModel.GetPrisonerRecruitmentMoraleEffect(this._partyScreenLogic.RightOwnerParty, this.Character, 1).ToString();
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00012B64 File Offset: 0x00010D64
		public void SetIsUpgradeButtonHighlighted(bool isHighlighted)
		{
			MBBindingList<UpgradeTargetVM> upgrades = this.Upgrades;
			if (upgrades == null)
			{
				return;
			}
			upgrades.ApplyActionOnAllItems(delegate(UpgradeTargetVM x)
			{
				x.IsHighlighted = isHighlighted;
			});
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00012B9C File Offset: 0x00010D9C
		public int GetNumOfCategoryItemPartyHas(ItemRoster items, ItemCategory itemCategory)
		{
			int num = 0;
			foreach (ItemRosterElement itemRosterElement in items)
			{
				if (itemRosterElement.EquipmentElement.Item.ItemCategory == itemCategory)
				{
					num += itemRosterElement.Amount;
				}
			}
			return num;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00012C04 File Offset: 0x00010E04
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00012C0C File Offset: 0x00010E0C
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00012C14 File Offset: 0x00010E14
		[DataSourceProperty]
		public bool IsFormationEnabled
		{
			get
			{
				return this._isFormationEnabled;
			}
			set
			{
				if (this._isFormationEnabled != value)
				{
					this._isFormationEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsFormationEnabled");
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00012C34 File Offset: 0x00010E34
		[DataSourceProperty]
		public string TransferString
		{
			get
			{
				return this.TransferAmount.ToString() + "/" + this.Number.ToString();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00012C67 File Offset: 0x00010E67
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00012C6F File Offset: 0x00010E6F
		[DataSourceProperty]
		public bool IsTroopUpgradable
		{
			get
			{
				return this._isTroopUpgradable;
			}
			set
			{
				if (value != this._isTroopUpgradable)
				{
					this._isTroopUpgradable = value;
					base.OnPropertyChangedWithValue(value, "IsTroopUpgradable");
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00012C8D File Offset: 0x00010E8D
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00012C95 File Offset: 0x00010E95
		[DataSourceProperty]
		public bool IsTroopRecruitable
		{
			get
			{
				return this._isTroopRecruitable;
			}
			set
			{
				if (value != this._isTroopRecruitable)
				{
					this._isTroopRecruitable = value;
					base.OnPropertyChangedWithValue(value, "IsTroopRecruitable");
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00012CB3 File Offset: 0x00010EB3
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00012CBB File Offset: 0x00010EBB
		[DataSourceProperty]
		public bool IsRecruitablePrisoner
		{
			get
			{
				return this._isRecruitablePrisoner;
			}
			set
			{
				if (value != this._isRecruitablePrisoner)
				{
					this._isRecruitablePrisoner = value;
					base.OnPropertyChangedWithValue(value, "IsRecruitablePrisoner");
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00012CD9 File Offset: 0x00010ED9
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00012CE1 File Offset: 0x00010EE1
		[DataSourceProperty]
		public bool IsUpgradableTroop
		{
			get
			{
				return this._isUpgradableTroop;
			}
			set
			{
				if (value != this._isUpgradableTroop)
				{
					this._isUpgradableTroop = value;
					base.OnPropertyChangedWithValue(value, "IsUpgradableTroop");
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00012CFF File Offset: 0x00010EFF
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00012D07 File Offset: 0x00010F07
		[DataSourceProperty]
		public bool IsExecutable
		{
			get
			{
				return this._isExecutable;
			}
			set
			{
				if (value != this._isExecutable)
				{
					this._isExecutable = value;
					base.OnPropertyChangedWithValue(value, "IsExecutable");
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00012D25 File Offset: 0x00010F25
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00012D2D File Offset: 0x00010F2D
		[DataSourceProperty]
		public int NumOfReadyToUpgradeTroops
		{
			get
			{
				return this._numOfReadyToUpgradeTroops;
			}
			set
			{
				if (value != this._numOfReadyToUpgradeTroops)
				{
					this._numOfReadyToUpgradeTroops = value;
					base.OnPropertyChangedWithValue(value, "NumOfReadyToUpgradeTroops");
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00012D4B File Offset: 0x00010F4B
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00012D53 File Offset: 0x00010F53
		[DataSourceProperty]
		public int NumOfUpgradeableTroops
		{
			get
			{
				return this._numOfUpgradeableTroops;
			}
			set
			{
				if (value != this._numOfUpgradeableTroops)
				{
					this._numOfUpgradeableTroops = value;
					base.OnPropertyChangedWithValue(value, "NumOfUpgradeableTroops");
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00012D71 File Offset: 0x00010F71
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00012D79 File Offset: 0x00010F79
		[DataSourceProperty]
		public int NumOfRecruitablePrisoners
		{
			get
			{
				return this._numOfRecruitablePrisoners;
			}
			set
			{
				if (value != this._numOfRecruitablePrisoners)
				{
					this._numOfRecruitablePrisoners = value;
					base.OnPropertyChangedWithValue(value, "NumOfRecruitablePrisoners");
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00012D97 File Offset: 0x00010F97
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00012D9F File Offset: 0x00010F9F
		[DataSourceProperty]
		public int MaxXP
		{
			get
			{
				return this._maxXP;
			}
			set
			{
				if (value != this._maxXP)
				{
					this._maxXP = value;
					base.OnPropertyChangedWithValue(value, "MaxXP");
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00012DBD File Offset: 0x00010FBD
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00012DC5 File Offset: 0x00010FC5
		[DataSourceProperty]
		public int CurrentXP
		{
			get
			{
				return this._currentXP;
			}
			set
			{
				if (value != this._currentXP)
				{
					this._currentXP = value;
					base.OnPropertyChangedWithValue(value, "CurrentXP");
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00012DE3 File Offset: 0x00010FE3
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00012DEB File Offset: 0x00010FEB
		[DataSourceProperty]
		public int CurrentConformity
		{
			get
			{
				return this._currentConformity;
			}
			set
			{
				if (value != this._currentConformity)
				{
					this._currentConformity = value;
					base.OnPropertyChangedWithValue(value, "CurrentConformity");
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00012E09 File Offset: 0x00011009
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00012E11 File Offset: 0x00011011
		[DataSourceProperty]
		public int MaxConformity
		{
			get
			{
				return this._maxConformity;
			}
			set
			{
				if (value != this._maxConformity)
				{
					this._maxConformity = value;
					base.OnPropertyChangedWithValue(value, "MaxConformity");
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00012E2F File Offset: 0x0001102F
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00012E37 File Offset: 0x00011037
		[DataSourceProperty]
		public BasicTooltipViewModel TroopXPTooltip
		{
			get
			{
				return this._troopXPTooltip;
			}
			set
			{
				if (value != this._troopXPTooltip)
				{
					this._troopXPTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "TroopXPTooltip");
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00012E55 File Offset: 0x00011055
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00012E5D File Offset: 0x0001105D
		[DataSourceProperty]
		public BasicTooltipViewModel TroopConformityTooltip
		{
			get
			{
				return this._troopConformityTooltip;
			}
			set
			{
				if (value != this._troopConformityTooltip)
				{
					this._troopConformityTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "TroopConformityTooltip");
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00012E7B File Offset: 0x0001107B
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00012E83 File Offset: 0x00011083
		[DataSourceProperty]
		public HintViewModel TransferHint
		{
			get
			{
				return this._transferHint;
			}
			set
			{
				if (value != this._transferHint)
				{
					this._transferHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TransferHint");
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00012EA1 File Offset: 0x000110A1
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00012EA9 File Offset: 0x000110A9
		[DataSourceProperty]
		public bool IsRecruitButtonsHiglighted
		{
			get
			{
				return this._isRecruitButtonsHiglighted;
			}
			set
			{
				if (value != this._isRecruitButtonsHiglighted)
				{
					this._isRecruitButtonsHiglighted = value;
					base.OnPropertyChangedWithValue(value, "IsRecruitButtonsHiglighted");
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00012EC7 File Offset: 0x000110C7
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00012ECF File Offset: 0x000110CF
		[DataSourceProperty]
		public bool IsTransferButtonHiglighted
		{
			get
			{
				return this._isTransferButtonHiglighted;
			}
			set
			{
				if (value != this._isTransferButtonHiglighted)
				{
					this._isTransferButtonHiglighted = value;
					base.OnPropertyChangedWithValue(value, "IsTransferButtonHiglighted");
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00012EED File Offset: 0x000110ED
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00012EF5 File Offset: 0x000110F5
		[DataSourceProperty]
		public string StrNumOfUpgradableTroop
		{
			get
			{
				return this._strNumOfUpgradableTroop;
			}
			set
			{
				if (value != this._strNumOfUpgradableTroop)
				{
					this._strNumOfUpgradableTroop = value;
					base.OnPropertyChangedWithValue<string>(value, "StrNumOfUpgradableTroop");
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00012F18 File Offset: 0x00011118
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00012F20 File Offset: 0x00011120
		[DataSourceProperty]
		public string StrNumOfRecruitableTroop
		{
			get
			{
				return this._strNumOfRecruitableTroop;
			}
			set
			{
				if (value != this._strNumOfRecruitableTroop)
				{
					this._strNumOfRecruitableTroop = value;
					base.OnPropertyChangedWithValue<string>(value, "StrNumOfRecruitableTroop");
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00012F43 File Offset: 0x00011143
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00012F4B File Offset: 0x0001114B
		[DataSourceProperty]
		public string TroopID
		{
			get
			{
				return this._troopID;
			}
			set
			{
				if (value != this._troopID)
				{
					this._troopID = value;
					base.OnPropertyChangedWithValue<string>(value, "TroopID");
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00012F6E File Offset: 0x0001116E
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00012F76 File Offset: 0x00011176
		[DataSourceProperty]
		public string UpgradeCostText
		{
			get
			{
				return this._upgradeCostText;
			}
			set
			{
				if (value != this._upgradeCostText)
				{
					this._upgradeCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "UpgradeCostText");
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00012F99 File Offset: 0x00011199
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00012FA1 File Offset: 0x000111A1
		[DataSourceProperty]
		public string RecruitMoraleCostText
		{
			get
			{
				return this._recruitMoraleCostText;
			}
			set
			{
				if (value != this._recruitMoraleCostText)
				{
					this._recruitMoraleCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitMoraleCostText");
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00012FC4 File Offset: 0x000111C4
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00012FCC File Offset: 0x000111CC
		[DataSourceProperty]
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				if (this._index != value)
				{
					this._index = value;
					base.OnPropertyChangedWithValue(value, "Index");
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00012FEA File Offset: 0x000111EA
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00012FF2 File Offset: 0x000111F2
		[DataSourceProperty]
		public int TransferAmount
		{
			get
			{
				return this._transferAmount;
			}
			set
			{
				if (value <= 0)
				{
					value = 1;
				}
				if (this._transferAmount != value)
				{
					this._transferAmount = value;
					base.OnPropertyChangedWithValue(value, "TransferAmount");
					base.OnPropertyChanged("TransferString");
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00013022 File Offset: 0x00011222
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001302A File Offset: 0x0001122A
		[DataSourceProperty]
		public bool IsTroopTransferrable
		{
			get
			{
				return this._isTroopTransferrable;
			}
			set
			{
				if (this.Character != CharacterObject.PlayerCharacter)
				{
					this._isTroopTransferrable = value;
					base.OnPropertyChangedWithValue(value, "IsTroopTransferrable");
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001304C File Offset: 0x0001124C
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00013054 File Offset: 0x00011254
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00013078 File Offset: 0x00011278
		[DataSourceProperty]
		public string TroopNum
		{
			get
			{
				if (this.Character != null && this.Character.IsHero)
				{
					return "1";
				}
				if (this.Troop.Character == null)
				{
					return "-1";
				}
				int num = this.Troop.Number - this.Troop.WoundedNumber;
				string text = GameTexts.FindText("str_party_nameplate_wounded_abbr", null).ToString();
				if (num != this.Troop.Number && this.Type != PartyScreenLogic.TroopType.Prisoner)
				{
					return string.Concat(new object[]
					{
						num,
						"+",
						this.Troop.WoundedNumber,
						text
					});
				}
				return this.Troop.Number.ToString();
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001314C File Offset: 0x0001134C
		[DataSourceProperty]
		public bool IsHeroWounded
		{
			get
			{
				CharacterObject character = this.Character;
				return character != null && character.IsHero && this.Character.HeroObject.IsWounded;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00013174 File Offset: 0x00011374
		[DataSourceProperty]
		public int HeroHealth
		{
			get
			{
				CharacterObject character = this.Character;
				if (character != null && character.IsHero)
				{
					return MathF.Ceiling((float)this.Character.HeroObject.HitPoints * 100f / (float)this.Character.MaxHitPoints());
				}
				return 0;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000131C0 File Offset: 0x000113C0
		[DataSourceProperty]
		public int Number
		{
			get
			{
				this.IsTroopTransferrable = (this._initIsTroopTransferable && this.Troop.Number > 0);
				return this.Troop.Number;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00013200 File Offset: 0x00011400
		[DataSourceProperty]
		public int WoundedCount
		{
			get
			{
				if (this.Troop.Character == null)
				{
					return 0;
				}
				return this.Troop.WoundedNumber;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0001322A File Offset: 0x0001142A
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00013232 File Offset: 0x00011432
		[DataSourceProperty]
		public BasicTooltipViewModel RecruitPrisonerHint
		{
			get
			{
				return this._recruitPrisonerHint;
			}
			set
			{
				if (value != this._recruitPrisonerHint)
				{
					this._recruitPrisonerHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "RecruitPrisonerHint");
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00013250 File Offset: 0x00011450
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00013258 File Offset: 0x00011458
		[DataSourceProperty]
		public ImageIdentifierVM Code
		{
			get
			{
				return this._code;
			}
			set
			{
				if (value != this._code)
				{
					this._code = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Code");
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00013276 File Offset: 0x00011476
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0001327E File Offset: 0x0001147E
		[DataSourceProperty]
		public BasicTooltipViewModel ExecutePrisonerHint
		{
			get
			{
				return this._executePrisonerHint;
			}
			set
			{
				if (value != this._executePrisonerHint)
				{
					this._executePrisonerHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ExecutePrisonerHint");
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001329C File Offset: 0x0001149C
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000132A4 File Offset: 0x000114A4
		[DataSourceProperty]
		public MBBindingList<UpgradeTargetVM> Upgrades
		{
			get
			{
				return this._upgrades;
			}
			set
			{
				if (value != this._upgrades)
				{
					this._upgrades = value;
					base.OnPropertyChangedWithValue<MBBindingList<UpgradeTargetVM>>(value, "Upgrades");
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000132C2 File Offset: 0x000114C2
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000132CA File Offset: 0x000114CA
		[DataSourceProperty]
		public BasicTooltipViewModel HeroHealthHint
		{
			get
			{
				return this._heroHealthHint;
			}
			set
			{
				if (value != this._heroHealthHint)
				{
					this._heroHealthHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "HeroHealthHint");
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000132E8 File Offset: 0x000114E8
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000132F0 File Offset: 0x000114F0
		[DataSourceProperty]
		public bool IsHero
		{
			get
			{
				return this._isHero;
			}
			set
			{
				if (value != this._isHero)
				{
					this._isHero = value;
					base.OnPropertyChangedWithValue(value, "IsHero");
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0001330E File Offset: 0x0001150E
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00013316 File Offset: 0x00011516
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00013334 File Offset: 0x00011534
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0001333C File Offset: 0x0001153C
		[DataSourceProperty]
		public bool HasMoreThanTwoUpgrades
		{
			get
			{
				return this._hasMoreThanTwoUpgrades;
			}
			set
			{
				if (value != this._hasMoreThanTwoUpgrades)
				{
					this._hasMoreThanTwoUpgrades = value;
					base.OnPropertyChangedWithValue(value, "HasMoreThanTwoUpgrades");
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0001335A File Offset: 0x0001155A
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00013362 File Offset: 0x00011562
		[DataSourceProperty]
		public bool IsPrisoner
		{
			get
			{
				return this._isPrisoner;
			}
			set
			{
				if (value != this._isPrisoner)
				{
					this._isPrisoner = value;
					base.OnPropertyChangedWithValue(value, "IsPrisoner");
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00013380 File Offset: 0x00011580
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00013388 File Offset: 0x00011588
		[DataSourceProperty]
		public bool IsPrisonerOfPlayer
		{
			get
			{
				return this._isPrisonerOfPlayer;
			}
			set
			{
				if (value != this._isPrisonerOfPlayer)
				{
					this._isPrisonerOfPlayer = value;
					base.OnPropertyChangedWithValue(value, "IsPrisonerOfPlayer");
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000133A6 File Offset: 0x000115A6
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x000133AE File Offset: 0x000115AE
		[DataSourceProperty]
		public bool IsHeroPrisonerOfPlayer
		{
			get
			{
				return this._isHeroPrisonerOfPlayer;
			}
			set
			{
				if (value != this._isHeroPrisonerOfPlayer)
				{
					this._isHeroPrisonerOfPlayer = value;
					base.OnPropertyChangedWithValue(value, "IsHeroPrisonerOfPlayer");
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000133CC File Offset: 0x000115CC
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x000133D4 File Offset: 0x000115D4
		[DataSourceProperty]
		public bool AnyUpgradeHasRequirement
		{
			get
			{
				return this._anyUpgradeHasRequirement;
			}
			set
			{
				if (value != this._anyUpgradeHasRequirement)
				{
					this._anyUpgradeHasRequirement = value;
					base.OnPropertyChangedWithValue(value, "AnyUpgradeHasRequirement");
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000133F2 File Offset: 0x000115F2
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x000133FA File Offset: 0x000115FA
		[DataSourceProperty]
		public StringItemWithHintVM TierIconData
		{
			get
			{
				return this._tierIconData;
			}
			set
			{
				if (value != this._tierIconData)
				{
					this._tierIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TierIconData");
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00013418 File Offset: 0x00011618
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00013420 File Offset: 0x00011620
		[DataSourceProperty]
		public StringItemWithHintVM TypeIconData
		{
			get
			{
				return this._typeIconData;
			}
			set
			{
				if (value != this._typeIconData)
				{
					this._typeIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TypeIconData");
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001343E File Offset: 0x0001163E
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00013446 File Offset: 0x00011646
		[DataSourceProperty]
		public bool HasEnoughGold
		{
			get
			{
				return this._hasEnoughGold;
			}
			set
			{
				if (value != this._hasEnoughGold)
				{
					this._hasEnoughGold = value;
					base.OnPropertyChangedWithValue(value, "HasEnoughGold");
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00013464 File Offset: 0x00011664
		[DataSourceProperty]
		public bool CanTalk
		{
			get
			{
				bool flag = this.Side == PartyScreenLogic.PartyRosterSide.Right;
				bool flag2 = this.Troop.Character != CharacterObject.PlayerCharacter;
				bool isHero = this.Troop.Character.IsHero;
				bool flag3 = CampaignMission.Current == null;
				bool flag4 = Settlement.CurrentSettlement == null;
				bool flag5 = MobileParty.MainParty.MapEvent == null;
				return flag2 && flag && isHero && flag3 && flag4 && flag5;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000134CD File Offset: 0x000116CD
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000134D5 File Offset: 0x000116D5
		[DataSourceProperty]
		public PartyTradeVM TradeData
		{
			get
			{
				return this._tradeData;
			}
			set
			{
				if (value != this._tradeData)
				{
					this._tradeData = value;
					base.OnPropertyChangedWithValue<PartyTradeVM>(value, "TradeData");
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000134F3 File Offset: 0x000116F3
		// (set) Token: 0x060002CF RID: 719 RVA: 0x000134FB File Offset: 0x000116FB
		[DataSourceProperty]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (value != this._isLocked)
				{
					this._isLocked = value;
					base.OnPropertyChangedWithValue(value, "IsLocked");
					Action<PartyCharacterVM, bool> processCharacterLock = PartyCharacterVM.ProcessCharacterLock;
					if (processCharacterLock == null)
					{
						return;
					}
					processCharacterLock(this, value);
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001352A File Offset: 0x0001172A
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00013532 File Offset: 0x00011732
		[DataSourceProperty]
		public HintViewModel LockHint
		{
			get
			{
				return this._lockHint;
			}
			set
			{
				if (value != this._lockHint)
				{
					this._lockHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LockHint");
				}
			}
		}

		// Token: 0x0400010F RID: 271
		public static bool IsShiftingDisabled;

		// Token: 0x04000110 RID: 272
		public static Action<PartyCharacterVM, bool> ProcessCharacterLock;

		// Token: 0x04000111 RID: 273
		public static Action<PartyCharacterVM> SetSelected;

		// Token: 0x04000112 RID: 274
		public static Action<PartyCharacterVM, int, int, PartyScreenLogic.PartyRosterSide> OnTransfer;

		// Token: 0x04000113 RID: 275
		public static Action<PartyCharacterVM> OnShift;

		// Token: 0x04000114 RID: 276
		public static Action<PartyCharacterVM> OnFocus;

		// Token: 0x04000115 RID: 277
		public static string FiveStackShortcutKeyText;

		// Token: 0x04000116 RID: 278
		public static string EntireStackShortcutKeyText;

		// Token: 0x04000117 RID: 279
		public readonly PartyScreenLogic.PartyRosterSide Side;

		// Token: 0x04000118 RID: 280
		public readonly PartyScreenLogic.TroopType Type;

		// Token: 0x04000119 RID: 281
		protected readonly PartyVM _partyVm;

		// Token: 0x0400011A RID: 282
		protected readonly PartyScreenLogic _partyScreenLogic;

		// Token: 0x0400011B RID: 283
		protected readonly bool _initIsTroopTransferable;

		// Token: 0x0400011E RID: 286
		private TroopRosterElement _troop;

		// Token: 0x0400011F RID: 287
		private CharacterObject _character;

		// Token: 0x04000120 RID: 288
		private string _name;

		// Token: 0x04000121 RID: 289
		private string _strNumOfUpgradableTroop;

		// Token: 0x04000122 RID: 290
		private string _strNumOfRecruitableTroop;

		// Token: 0x04000123 RID: 291
		private string _troopID;

		// Token: 0x04000124 RID: 292
		private string _upgradeCostText;

		// Token: 0x04000125 RID: 293
		private string _recruitMoraleCostText;

		// Token: 0x04000126 RID: 294
		private MBBindingList<UpgradeTargetVM> _upgrades;

		// Token: 0x04000127 RID: 295
		private ImageIdentifierVM _code = new ImageIdentifierVM(ImageIdentifierType.Null);

		// Token: 0x04000128 RID: 296
		public HintViewModel _transferHint;

		// Token: 0x04000129 RID: 297
		private BasicTooltipViewModel _recruitPrisonerHint;

		// Token: 0x0400012A RID: 298
		private BasicTooltipViewModel _executePrisonerHint;

		// Token: 0x0400012B RID: 299
		private BasicTooltipViewModel _heroHealthHint;

		// Token: 0x0400012C RID: 300
		private int _transferAmount = 1;

		// Token: 0x0400012D RID: 301
		private int _index = -2;

		// Token: 0x0400012E RID: 302
		private int _numOfReadyToUpgradeTroops;

		// Token: 0x0400012F RID: 303
		private int _numOfUpgradeableTroops;

		// Token: 0x04000130 RID: 304
		private int _numOfRecruitablePrisoners;

		// Token: 0x04000131 RID: 305
		private int _maxXP;

		// Token: 0x04000132 RID: 306
		private int _currentXP;

		// Token: 0x04000133 RID: 307
		private int _maxConformity;

		// Token: 0x04000134 RID: 308
		private int _currentConformity;

		// Token: 0x04000135 RID: 309
		public BasicTooltipViewModel _troopXPTooltip;

		// Token: 0x04000136 RID: 310
		public BasicTooltipViewModel _troopConformityTooltip;

		// Token: 0x04000137 RID: 311
		private bool _isHero;

		// Token: 0x04000138 RID: 312
		private bool _isMainHero;

		// Token: 0x04000139 RID: 313
		private bool _isPrisoner;

		// Token: 0x0400013A RID: 314
		private bool _isPrisonerOfPlayer;

		// Token: 0x0400013B RID: 315
		private bool _isRecruitablePrisoner;

		// Token: 0x0400013C RID: 316
		private bool _isUpgradableTroop;

		// Token: 0x0400013D RID: 317
		private bool _isTroopTransferrable;

		// Token: 0x0400013E RID: 318
		private bool _isHeroPrisonerOfPlayer;

		// Token: 0x0400013F RID: 319
		private bool _isTroopUpgradable;

		// Token: 0x04000140 RID: 320
		private StringItemWithHintVM _tierIconData;

		// Token: 0x04000141 RID: 321
		private bool _hasEnoughGold;

		// Token: 0x04000142 RID: 322
		private bool _anyUpgradeHasRequirement;

		// Token: 0x04000143 RID: 323
		private StringItemWithHintVM _typeIconData;

		// Token: 0x04000144 RID: 324
		private bool _hasMoreThanTwoUpgrades;

		// Token: 0x04000145 RID: 325
		private bool _isRecruitButtonsHiglighted;

		// Token: 0x04000146 RID: 326
		private bool _isTransferButtonHiglighted;

		// Token: 0x04000147 RID: 327
		private bool _isFormationEnabled;

		// Token: 0x04000148 RID: 328
		private PartyTradeVM _tradeData;

		// Token: 0x04000149 RID: 329
		private bool _isTroopRecruitable;

		// Token: 0x0400014A RID: 330
		private bool _isExecutable;

		// Token: 0x0400014B RID: 331
		private bool _isLocked;

		// Token: 0x0400014C RID: 332
		private HintViewModel _lockHint;
	}
}
