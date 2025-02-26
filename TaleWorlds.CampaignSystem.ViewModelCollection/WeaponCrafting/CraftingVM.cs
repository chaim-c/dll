﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Refinement;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign.Order;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000E1 RID: 225
	public class CraftingVM : ViewModel
	{
		// Token: 0x060014BB RID: 5307 RVA: 0x0004E718 File Offset: 0x0004C918
		public CraftingVM(Crafting crafting, Action onClose, Action resetCamera, Action onWeaponCrafted, Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> getItemUsageSetFlags)
		{
			this._crafting = crafting;
			this._onClose = onClose;
			this._resetCamera = resetCamera;
			this._onWeaponCrafted = onWeaponCrafted;
			this._craftingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			this._getItemUsageSetFlags = getItemUsageSetFlags;
			this.AvailableCharactersForSmithing = new MBBindingList<CraftingAvailableHeroItemVM>();
			this.MainActionHint = new BasicTooltipViewModel();
			this.TutorialNotification = new ElementNotificationVM();
			this.CameraControlKeys = new MBBindingList<InputKeyItemVM>();
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				foreach (Hero hero in CraftingHelper.GetAvailableHeroesForCrafting())
				{
					this.AvailableCharactersForSmithing.Add(new CraftingAvailableHeroItemVM(hero, new Action<CraftingAvailableHeroItemVM>(this.UpdateCraftingHero)));
				}
				this.CurrentCraftingHero = this.AvailableCharactersForSmithing.FirstOrDefault<CraftingAvailableHeroItemVM>();
			}
			else
			{
				this.CurrentCraftingHero = new CraftingAvailableHeroItemVM(Hero.MainHero, new Action<CraftingAvailableHeroItemVM>(this.UpdateCraftingHero));
			}
			this.UpdateCurrentMaterialsAvailable();
			this.Smelting = new SmeltingVM(new Action(this.OnSmeltItemSelection), new Action(this.UpdateAll));
			this.Refinement = new RefinementVM(new Action(this.OnRefinementSelectionChange), new Func<CraftingAvailableHeroItemVM>(this.GetCurrentCraftingHero));
			this.WeaponDesign = new WeaponDesignVM(this._crafting, this._craftingBehavior, new Action(this.OnRequireUpdateFromWeaponDesign), this._onWeaponCrafted, new Func<CraftingAvailableHeroItemVM>(this.GetCurrentCraftingHero), new Action<CraftingOrder>(this.RefreshHeroAvailabilities), this._getItemUsageSetFlags);
			this.CraftingHeroPopup = new CraftingHeroPopupVM(new Func<MBBindingList<CraftingAvailableHeroItemVM>>(this.GetCraftingHeroes));
			this.UpdateCraftingPerks();
			this.ExecuteSwitchToCrafting();
			this.RefreshValues();
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0004E8FC File Offset: 0x0004CAFC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			this.CancelLbl = GameTexts.FindText("str_exit", null).ToString();
			this.ResetCameraHint = new HintViewModel(GameTexts.FindText("str_reset_camera", null), null);
			this.CraftingHint = new HintViewModel(GameTexts.FindText("str_crafting", null), null);
			this.RefiningHint = new HintViewModel(GameTexts.FindText("str_refining", null), null);
			this.SmeltingHint = new HintViewModel(GameTexts.FindText("str_smelting", null), null);
			this.RefinementText = GameTexts.FindText("str_crafting_category_refinement", null).ToString();
			this.CraftingText = GameTexts.FindText("str_crafting_category_crafting", null).ToString();
			this.SmeltingText = GameTexts.FindText("str_crafting_category_smelting", null).ToString();
			this.SelectItemToSmeltText = new TextObject("{=rUeWBOOi}Select an item to smelt", null).ToString();
			this.SelectItemToRefineText = new TextObject("{=BqLsZhhr}Select an item to refine", null).ToString();
			this.TutorialNotification.RefreshValues();
			this._availableCharactersForSmithing.ApplyActionOnAllItems(delegate(CraftingAvailableHeroItemVM x)
			{
				x.RefreshValues();
			});
			this._playerCurrentMaterials.ApplyActionOnAllItems(delegate(CraftingResourceItemVM x)
			{
				x.RefreshValues();
			});
			CraftingAvailableHeroItemVM currentCraftingHero = this._currentCraftingHero;
			if (currentCraftingHero != null)
			{
				currentCraftingHero.RefreshValues();
			}
			this.CraftingHeroPopup.RefreshValues();
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0004EA80 File Offset: 0x0004CC80
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.WeaponDesign.OnFinalize();
			InputKeyItemVM confirmInputKey = this.ConfirmInputKey;
			if (confirmInputKey != null)
			{
				confirmInputKey.OnFinalize();
			}
			InputKeyItemVM exitInputKey = this.ExitInputKey;
			if (exitInputKey != null)
			{
				exitInputKey.OnFinalize();
			}
			InputKeyItemVM previousTabInputKey = this.PreviousTabInputKey;
			if (previousTabInputKey != null)
			{
				previousTabInputKey.OnFinalize();
			}
			InputKeyItemVM nextTabInputKey = this.NextTabInputKey;
			if (nextTabInputKey != null)
			{
				nextTabInputKey.OnFinalize();
			}
			foreach (InputKeyItemVM inputKeyItemVM in this.CameraControlKeys)
			{
				if (inputKeyItemVM != null)
				{
					inputKeyItemVM.OnFinalize();
				}
			}
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			EventManager eventManager = game.EventManager;
			if (eventManager == null)
			{
				return;
			}
			eventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0004EB4C File Offset: 0x0004CD4C
		private void OnRequireUpdateFromWeaponDesign()
		{
			CraftingVM.OnItemRefreshedDelegate onItemRefreshed = this.OnItemRefreshed;
			if (onItemRefreshed != null)
			{
				onItemRefreshed(true);
			}
			this.UpdateAll();
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004EB66 File Offset: 0x0004CD66
		public void OnCraftingLogicRefreshed(Crafting newCraftingLogic)
		{
			this._crafting = newCraftingLogic;
			this.WeaponDesign.OnCraftingLogicRefreshed(newCraftingLogic);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004EB7C File Offset: 0x0004CD7C
		private void UpdateCurrentMaterialCosts()
		{
			for (int i = 0; i < 9; i++)
			{
				this.PlayerCurrentMaterials[i].ResourceAmount = MobileParty.MainParty.ItemRoster.GetItemNumber(this.PlayerCurrentMaterials[i].ResourceItem);
				this.PlayerCurrentMaterials[i].ResourceChangeAmount = 0;
			}
			if (this.IsInSmeltingMode)
			{
				if (this.Smelting.CurrentSelectedItem != null)
				{
					int[] smeltingOutputForItem = Campaign.Current.Models.SmithingModel.GetSmeltingOutputForItem(this.Smelting.CurrentSelectedItem.EquipmentElement.Item);
					for (int j = 0; j < 9; j++)
					{
						this.PlayerCurrentMaterials[j].ResourceChangeAmount = smeltingOutputForItem[j];
					}
					return;
				}
			}
			else
			{
				if (this.IsInRefinementMode)
				{
					RefinementActionItemVM currentSelectedAction = this.Refinement.CurrentSelectedAction;
					if (currentSelectedAction == null)
					{
						return;
					}
					Crafting.RefiningFormula refineFormula = currentSelectedAction.RefineFormula;
					SmithingModel smithingModel = Campaign.Current.Models.SmithingModel;
					for (int k = 0; k < 9; k++)
					{
						this.PlayerCurrentMaterials[k].ResourceChangeAmount = 0;
						if (smithingModel.GetCraftingMaterialItem(refineFormula.Input1) == this.PlayerCurrentMaterials[k].ResourceItem)
						{
							this.PlayerCurrentMaterials[k].ResourceChangeAmount -= refineFormula.Input1Count;
						}
						else if (smithingModel.GetCraftingMaterialItem(refineFormula.Input2) == this.PlayerCurrentMaterials[k].ResourceItem)
						{
							this.PlayerCurrentMaterials[k].ResourceChangeAmount -= refineFormula.Input2Count;
						}
						else if (smithingModel.GetCraftingMaterialItem(refineFormula.Output) == this.PlayerCurrentMaterials[k].ResourceItem)
						{
							this.PlayerCurrentMaterials[k].ResourceChangeAmount += refineFormula.OutputCount;
						}
						else if (smithingModel.GetCraftingMaterialItem(refineFormula.Output2) == this.PlayerCurrentMaterials[k].ResourceItem)
						{
							this.PlayerCurrentMaterials[k].ResourceChangeAmount += refineFormula.Output2Count;
						}
					}
					int[] array = new int[9];
					foreach (CraftingResourceItemVM craftingResourceItemVM in currentSelectedAction.InputMaterials)
					{
						array[(int)craftingResourceItemVM.ResourceMaterial] -= craftingResourceItemVM.ResourceAmount;
					}
					using (IEnumerator<CraftingResourceItemVM> enumerator = currentSelectedAction.OutputMaterials.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CraftingResourceItemVM craftingResourceItemVM2 = enumerator.Current;
							array[(int)craftingResourceItemVM2.ResourceMaterial] += craftingResourceItemVM2.ResourceAmount;
						}
						return;
					}
				}
				int[] smithingCostsForWeaponDesign = Campaign.Current.Models.SmithingModel.GetSmithingCostsForWeaponDesign(this._crafting.CurrentWeaponDesign);
				for (int l = 0; l < 9; l++)
				{
					this.PlayerCurrentMaterials[l].ResourceChangeAmount = smithingCostsForWeaponDesign[l];
				}
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004EEB8 File Offset: 0x0004D0B8
		private void UpdateCurrentMaterialsAvailable()
		{
			if (this.PlayerCurrentMaterials == null)
			{
				this.PlayerCurrentMaterials = new MBBindingList<CraftingResourceItemVM>();
				for (int i = 0; i < 9; i++)
				{
					this.PlayerCurrentMaterials.Add(new CraftingResourceItemVM((CraftingMaterials)i, 0, 0));
				}
			}
			for (int j = 0; j < 9; j++)
			{
				ItemObject craftingMaterialItem = Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem((CraftingMaterials)j);
				this.PlayerCurrentMaterials[j].ResourceAmount = MobileParty.MainParty.ItemRoster.GetItemNumber(craftingMaterialItem);
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004EF3C File Offset: 0x0004D13C
		private void UpdateAll()
		{
			this.UpdateCurrentMaterialCosts();
			this.UpdateCurrentMaterialsAvailable();
			this.RefreshEnableMainAction();
			this.UpdateCraftingStamina();
			this.UpdateCraftingSkills();
			CraftingOrder order;
			if (!this.IsInCraftingMode)
			{
				order = null;
			}
			else
			{
				CraftingOrderItemVM activeCraftingOrder = this.WeaponDesign.ActiveCraftingOrder;
				order = ((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null);
			}
			this.RefreshHeroAvailabilities(order);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0004EF90 File Offset: 0x0004D190
		private void UpdateCraftingSkills()
		{
			foreach (CraftingAvailableHeroItemVM craftingAvailableHeroItemVM in this.AvailableCharactersForSmithing)
			{
				craftingAvailableHeroItemVM.RefreshSkills();
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0004EFDC File Offset: 0x0004D1DC
		private void UpdateCraftingStamina()
		{
			foreach (CraftingAvailableHeroItemVM craftingAvailableHeroItemVM in this.AvailableCharactersForSmithing)
			{
				craftingAvailableHeroItemVM.RefreshStamina();
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0004F028 File Offset: 0x0004D228
		private void UpdateCraftingPerks()
		{
			foreach (CraftingAvailableHeroItemVM craftingAvailableHeroItemVM in this.AvailableCharactersForSmithing)
			{
				craftingAvailableHeroItemVM.RefreshPerks();
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0004F074 File Offset: 0x0004D274
		private void RefreshHeroAvailabilities(CraftingOrder order)
		{
			foreach (CraftingAvailableHeroItemVM craftingAvailableHeroItemVM in this.AvailableCharactersForSmithing)
			{
				craftingAvailableHeroItemVM.RefreshOrderAvailability(order);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0004F0C0 File Offset: 0x0004D2C0
		private void RefreshEnableMainAction()
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Tutorial)
			{
				this.IsMainActionEnabled = true;
				return;
			}
			this.IsMainActionEnabled = true;
			if (!this.HaveEnergy())
			{
				this.IsMainActionEnabled = false;
				if (this.MainActionHint != null)
				{
					this.MainActionHint = new BasicTooltipViewModel(() => new TextObject("{=PRE5RKpp}You must rest and spend time before you can do this action.", null).ToString());
				}
			}
			else if (!this.HaveMaterialsNeeded())
			{
				this.IsMainActionEnabled = false;
				if (this.MainActionHint != null)
				{
					this.MainActionHint = new BasicTooltipViewModel(() => new TextObject("{=gduqxfck}You don't have all required materials!", null).ToString());
				}
			}
			if (this.IsInSmeltingMode)
			{
				this.IsMainActionEnabled = (this.IsMainActionEnabled && this.Smelting.IsAnyItemSelected);
				this.IsSmeltingItemSelected = this.Smelting.IsAnyItemSelected;
				if (!this.IsSmeltingItemSelected && this.MainActionHint != null)
				{
					this.MainActionHint = new BasicTooltipViewModel(() => new TextObject("{=SzuCFlNq}No item selected.", null).ToString());
					return;
				}
			}
			else if (this.IsInRefinementMode)
			{
				this.IsMainActionEnabled = (this.IsMainActionEnabled && this.Refinement.IsValidRefinementActionSelected);
				this.IsRefinementItemSelected = this.Refinement.IsValidRefinementActionSelected;
				if (!this.IsRefinementItemSelected && this.MainActionHint != null)
				{
					this.MainActionHint = new BasicTooltipViewModel(() => new TextObject("{=SzuCFlNq}No item selected.", null).ToString());
					return;
				}
			}
			else
			{
				if (this.WeaponDesign != null)
				{
					if (!this.WeaponDesign.HaveUnlockedAllSelectedPieces())
					{
						this.IsMainActionEnabled = false;
						if (this.MainActionHint != null)
						{
							this.MainActionHint = new BasicTooltipViewModel(() => new TextObject("{=Wir2xZIg}You haven't unlocked some of the selected pieces.", null).ToString());
						}
					}
					else if (!this.WeaponDesign.CanCompleteOrder())
					{
						this.IsMainActionEnabled = false;
						if (this.MainActionHint != null)
						{
							CraftingVM.<>c__DisplayClass21_0 CS$<>8__locals1 = new CraftingVM.<>c__DisplayClass21_0();
							CraftingVM.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
							CraftingOrderItemVM activeCraftingOrder = this.WeaponDesign.ActiveCraftingOrder;
							CS$<>8__locals2.order = ((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null);
							CS$<>8__locals1.item = this._crafting.GetCurrentCraftedItemObject(false);
							this.MainActionHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetOrderCannotBeCompletedReasonTooltip(CS$<>8__locals1.order, CS$<>8__locals1.item));
						}
					}
				}
				if (this.IsMainActionEnabled && this.MainActionHint != null)
				{
					this.MainActionHint = new BasicTooltipViewModel();
				}
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0004F332 File Offset: 0x0004D532
		private bool HaveEnergy()
		{
			CraftingAvailableHeroItemVM currentCraftingHero = this.CurrentCraftingHero;
			return ((currentCraftingHero != null) ? currentCraftingHero.Hero : null) == null || this._craftingBehavior.GetHeroCraftingStamina(this.CurrentCraftingHero.Hero) > 10;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0004F364 File Offset: 0x0004D564
		private bool HaveMaterialsNeeded()
		{
			return !this.PlayerCurrentMaterials.Any((CraftingResourceItemVM m) => m.ResourceChangeAmount + m.ResourceAmount < 0);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0004F394 File Offset: 0x0004D594
		public void UpdateCraftingHero(CraftingAvailableHeroItemVM newHero)
		{
			this.CurrentCraftingHero = newHero;
			CraftingHeroPopupVM craftingHeroPopup = this.CraftingHeroPopup;
			if (craftingHeroPopup != null && craftingHeroPopup.IsVisible)
			{
				this.CraftingHeroPopup.ExecuteClosePopup();
			}
			this.WeaponDesign.OnCraftingHeroChanged(newHero);
			this.Refinement.OnCraftingHeroChanged(newHero);
			this.Smelting.OnCraftingHeroChanged(newHero);
			this.RefreshEnableMainAction();
			this.UpdateCraftingSkills();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0004F3F8 File Offset: 0x0004D5F8
		[return: TupleElementNames(new string[]
		{
			"isConfirmSuccessful",
			"isMainActionExecuted"
		})]
		public ValueTuple<bool, bool> ExecuteConfirm()
		{
			CraftingHistoryVM craftingHistory = this.WeaponDesign.CraftingHistory;
			if (craftingHistory != null && craftingHistory.IsVisible)
			{
				if (this.WeaponDesign.CraftingHistory.SelectedDesign != null)
				{
					this.WeaponDesign.CraftingHistory.ExecuteDone();
					return new ValueTuple<bool, bool>(true, false);
				}
			}
			else
			{
				CraftingOrderPopupVM craftingOrderPopup = this.WeaponDesign.CraftingOrderPopup;
				if (craftingOrderPopup != null && !craftingOrderPopup.IsVisible)
				{
					WeaponClassSelectionPopupVM weaponClassSelectionPopup = this.WeaponDesign.WeaponClassSelectionPopup;
					if (weaponClassSelectionPopup != null && !weaponClassSelectionPopup.IsVisible)
					{
						CraftingHeroPopupVM craftingHeroPopup = this.CraftingHeroPopup;
						if (craftingHeroPopup != null && !craftingHeroPopup.IsVisible)
						{
							if (this.WeaponDesign.IsInFinalCraftingStage)
							{
								if (this.WeaponDesign.CraftingResultPopup.CanConfirm)
								{
									this.WeaponDesign.CraftingResultPopup.ExecuteFinalizeCrafting();
									return new ValueTuple<bool, bool>(true, false);
								}
							}
							else if (this.IsMainActionEnabled)
							{
								this.ExecuteMainAction();
								return new ValueTuple<bool, bool>(true, true);
							}
						}
					}
				}
			}
			return new ValueTuple<bool, bool>(false, false);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
		public void ExecuteCancel()
		{
			CraftingHistoryVM craftingHistory = this.WeaponDesign.CraftingHistory;
			if (craftingHistory != null && craftingHistory.IsVisible)
			{
				this.WeaponDesign.CraftingHistory.ExecuteCancel();
				return;
			}
			CraftingHeroPopupVM craftingHeroPopup = this.CraftingHeroPopup;
			if (craftingHeroPopup != null && craftingHeroPopup.IsVisible)
			{
				this.CraftingHeroPopup.ExecuteClosePopup();
				return;
			}
			CraftingOrderPopupVM craftingOrderPopup = this.WeaponDesign.CraftingOrderPopup;
			if (craftingOrderPopup != null && craftingOrderPopup.IsVisible)
			{
				this.WeaponDesign.CraftingOrderPopup.ExecuteCloseWithoutSelection();
				return;
			}
			WeaponClassSelectionPopupVM weaponClassSelectionPopup = this.WeaponDesign.WeaponClassSelectionPopup;
			if (weaponClassSelectionPopup != null && weaponClassSelectionPopup.IsVisible)
			{
				this.WeaponDesign.WeaponClassSelectionPopup.ExecuteClosePopup();
				return;
			}
			if (this.WeaponDesign.IsInFinalCraftingStage)
			{
				if (this.WeaponDesign.CraftingResultPopup.CanConfirm)
				{
					this.WeaponDesign.CraftingResultPopup.ExecuteFinalizeCrafting();
					return;
				}
			}
			else
			{
				this.Smelting.SaveItemLockStates();
				Game.Current.GameStateManager.PopState(0);
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0004F5EC File Offset: 0x0004D7EC
		public void ExecuteMainAction()
		{
			if (this.IsInSmeltingMode)
			{
				this.Smelting.TrySmeltingSelectedItems(this.CurrentCraftingHero.Hero);
			}
			else if (this.IsInRefinementMode)
			{
				this.Refinement.ExecuteSelectedRefinement(this.CurrentCraftingHero.Hero);
			}
			else if (Campaign.Current.GameMode == CampaignGameMode.Tutorial)
			{
				CraftingState craftingState;
				if ((craftingState = (GameStateManager.Current.ActiveState as CraftingState)) != null)
				{
					ItemObject currentCraftedItemObject = craftingState.CraftingLogic.GetCurrentCraftedItemObject(true);
					ItemObject item = MBObjectManager.Instance.GetObject<ItemObject>(currentCraftedItemObject.WeaponDesign.HashedCode) ?? MBObjectManager.Instance.RegisterObject<ItemObject>(currentCraftedItemObject);
					PartyBase.MainParty.ItemRoster.AddToCounts(item, 1);
					this.WeaponDesign.IsInFinalCraftingStage = false;
				}
			}
			else
			{
				if (!this.HaveMaterialsNeeded() || !this.HaveEnergy())
				{
					return;
				}
				CraftingAvailableHeroItemVM currentCraftingHero = this.GetCurrentCraftingHero();
				Hero hero = (currentCraftingHero != null) ? currentCraftingHero.Hero : null;
				ItemModifier craftedWeaponModifier = Campaign.Current.Models.SmithingModel.GetCraftedWeaponModifier(this._crafting.CurrentWeaponDesign, hero);
				this._craftingBehavior.SetCurrentItemModifier(craftedWeaponModifier);
				if (this.WeaponDesign.IsInOrderMode)
				{
					WeaponDesignVM weaponDesign = this.WeaponDesign;
					ICraftingCampaignBehavior craftingBehavior = this._craftingBehavior;
					Hero crafterHero = hero;
					CraftingOrderItemVM activeCraftingOrder = this.WeaponDesign.ActiveCraftingOrder;
					weaponDesign.CraftedItemObject = craftingBehavior.CreateCraftedWeaponInCraftingOrderMode(crafterHero, (activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null, this._crafting.CurrentWeaponDesign);
				}
				else
				{
					this.WeaponDesign.CraftedItemObject = this._craftingBehavior.CreateCraftedWeaponInFreeBuildMode(hero, this._crafting.CurrentWeaponDesign, this._craftingBehavior.GetCurrentItemModifier());
				}
				this.WeaponDesign.IsInFinalCraftingStage = true;
				this.WeaponDesign.CreateCraftingResultPopup();
				Action onWeaponCrafted = this._onWeaponCrafted;
				if (onWeaponCrafted != null)
				{
					onWeaponCrafted();
				}
			}
			if (!this.IsInSmeltingMode)
			{
				this.UpdateAll();
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0004F7BD File Offset: 0x0004D9BD
		public void ExecuteResetCamera()
		{
			this._resetCamera();
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0004F7CA File Offset: 0x0004D9CA
		private CraftingAvailableHeroItemVM GetCurrentCraftingHero()
		{
			return this.CurrentCraftingHero;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0004F7D2 File Offset: 0x0004D9D2
		private MBBindingList<CraftingAvailableHeroItemVM> GetCraftingHeroes()
		{
			return this.AvailableCharactersForSmithing;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0004F7DA File Offset: 0x0004D9DA
		public void SetConfirmInputKey(HotKey hotKey)
		{
			this.ConfirmInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0004F7E9 File Offset: 0x0004D9E9
		public void SetExitInputKey(HotKey hotKey)
		{
			this.ExitInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0004F7F8 File Offset: 0x0004D9F8
		public void SetPreviousTabInputKey(HotKey hotKey)
		{
			this.PreviousTabInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004F807 File Offset: 0x0004DA07
		public void SetNextTabInputKey(HotKey hotKey)
		{
			this.NextTabInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004F818 File Offset: 0x0004DA18
		public void AddCameraControlInputKey(HotKey hotKey)
		{
			InputKeyItemVM item = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.CameraControlKeys.Add(item);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004F83C File Offset: 0x0004DA3C
		public void AddCameraControlInputKey(GameKey gameKey)
		{
			InputKeyItemVM item = InputKeyItemVM.CreateFromGameKey(gameKey, true);
			this.CameraControlKeys.Add(item);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0004F860 File Offset: 0x0004DA60
		public void AddCameraControlInputKey(GameAxisKey gameAxisKey)
		{
			TextObject forcedName = GameTexts.FindText("str_key_name", "CraftingHotkeyCategory_" + gameAxisKey.Id);
			InputKeyItemVM item = InputKeyItemVM.CreateFromForcedID(gameAxisKey.AxisKey.ToString(), forcedName, true);
			this.CameraControlKeys.Add(item);
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0004F8A7 File Offset: 0x0004DAA7
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x0004F8AF File Offset: 0x0004DAAF
		public InputKeyItemVM ConfirmInputKey
		{
			get
			{
				return this._confirmInputKey;
			}
			set
			{
				if (value != this._confirmInputKey)
				{
					this._confirmInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ConfirmInputKey");
				}
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0004F8CD File Offset: 0x0004DACD
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x0004F8D5 File Offset: 0x0004DAD5
		public InputKeyItemVM ExitInputKey
		{
			get
			{
				return this._exitInputKey;
			}
			set
			{
				if (value != this._exitInputKey)
				{
					this._exitInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ExitInputKey");
				}
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0004F8F3 File Offset: 0x0004DAF3
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0004F8FB File Offset: 0x0004DAFB
		public InputKeyItemVM PreviousTabInputKey
		{
			get
			{
				return this._previousTabInputKey;
			}
			set
			{
				if (value != this._previousTabInputKey)
				{
					this._previousTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousTabInputKey");
				}
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0004F919 File Offset: 0x0004DB19
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x0004F921 File Offset: 0x0004DB21
		public InputKeyItemVM NextTabInputKey
		{
			get
			{
				return this._nextTabInputKey;
			}
			set
			{
				if (value != this._nextTabInputKey)
				{
					this._nextTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextTabInputKey");
				}
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0004F93F File Offset: 0x0004DB3F
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0004F947 File Offset: 0x0004DB47
		[DataSourceProperty]
		public MBBindingList<InputKeyItemVM> CameraControlKeys
		{
			get
			{
				return this._cameraControlKeys;
			}
			set
			{
				if (value != this._cameraControlKeys)
				{
					this._cameraControlKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<InputKeyItemVM>>(value, "CameraControlKeys");
				}
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0004F965 File Offset: 0x0004DB65
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x0004F96D File Offset: 0x0004DB6D
		public bool CanSwitchTabs
		{
			get
			{
				return this._canSwitchTabs;
			}
			set
			{
				if (value != this._canSwitchTabs)
				{
					this._canSwitchTabs = value;
					base.OnPropertyChangedWithValue(value, "CanSwitchTabs");
				}
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0004F98B File Offset: 0x0004DB8B
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0004F993 File Offset: 0x0004DB93
		public bool AreGamepadControlHintsEnabled
		{
			get
			{
				return this._areGamepadControlHintsEnabled;
			}
			set
			{
				if (value != this._areGamepadControlHintsEnabled)
				{
					this._areGamepadControlHintsEnabled = value;
					base.OnPropertyChangedWithValue(value, "AreGamepadControlHintsEnabled");
				}
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0004F9B1 File Offset: 0x0004DBB1
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0004F9B9 File Offset: 0x0004DBB9
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> PlayerCurrentMaterials
		{
			get
			{
				return this._playerCurrentMaterials;
			}
			set
			{
				if (value != this._playerCurrentMaterials)
				{
					this._playerCurrentMaterials = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "PlayerCurrentMaterials");
				}
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0004F9D7 File Offset: 0x0004DBD7
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0004F9DF File Offset: 0x0004DBDF
		[DataSourceProperty]
		public MBBindingList<CraftingAvailableHeroItemVM> AvailableCharactersForSmithing
		{
			get
			{
				return this._availableCharactersForSmithing;
			}
			set
			{
				if (value != this._availableCharactersForSmithing)
				{
					this._availableCharactersForSmithing = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingAvailableHeroItemVM>>(value, "AvailableCharactersForSmithing");
				}
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0004F9FD File Offset: 0x0004DBFD
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0004FA08 File Offset: 0x0004DC08
		[DataSourceProperty]
		public CraftingAvailableHeroItemVM CurrentCraftingHero
		{
			get
			{
				return this._currentCraftingHero;
			}
			set
			{
				if (value != this._currentCraftingHero)
				{
					if (this._currentCraftingHero != null)
					{
						this._currentCraftingHero.IsSelected = false;
					}
					this._currentCraftingHero = value;
					if (this._currentCraftingHero != null)
					{
						this._currentCraftingHero.IsSelected = true;
					}
					base.OnPropertyChangedWithValue<CraftingAvailableHeroItemVM>(value, "CurrentCraftingHero");
				}
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0004FA59 File Offset: 0x0004DC59
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x0004FA61 File Offset: 0x0004DC61
		[DataSourceProperty]
		public CraftingHeroPopupVM CraftingHeroPopup
		{
			get
			{
				return this._craftingHeroPopup;
			}
			set
			{
				if (value != this._craftingHeroPopup)
				{
					this._craftingHeroPopup = value;
					base.OnPropertyChangedWithValue<CraftingHeroPopupVM>(value, "CraftingHeroPopup");
				}
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0004FA7F File Offset: 0x0004DC7F
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x0004FA87 File Offset: 0x0004DC87
		[DataSourceProperty]
		public string CurrentCategoryText
		{
			get
			{
				return this._currentCategoryText;
			}
			set
			{
				if (value != this._currentCategoryText)
				{
					this._currentCategoryText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCategoryText");
				}
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0004FAAA File Offset: 0x0004DCAA
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0004FAB2 File Offset: 0x0004DCB2
		[DataSourceProperty]
		public string CraftingText
		{
			get
			{
				return this._craftingText;
			}
			set
			{
				if (value != this._craftingText)
				{
					this._craftingText = value;
					base.OnPropertyChangedWithValue<string>(value, "CraftingText");
				}
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0004FAD5 File Offset: 0x0004DCD5
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0004FADD File Offset: 0x0004DCDD
		[DataSourceProperty]
		public string SmeltingText
		{
			get
			{
				return this._smeltingText;
			}
			set
			{
				if (value != this._smeltingText)
				{
					this._smeltingText = value;
					base.OnPropertyChangedWithValue<string>(value, "SmeltingText");
				}
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0004FB00 File Offset: 0x0004DD00
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0004FB08 File Offset: 0x0004DD08
		[DataSourceProperty]
		public string RefinementText
		{
			get
			{
				return this._refinementText;
			}
			set
			{
				if (value != this._refinementText)
				{
					this._refinementText = value;
					base.OnPropertyChangedWithValue<string>(value, "RefinementText");
				}
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0004FB2B File Offset: 0x0004DD2B
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0004FB33 File Offset: 0x0004DD33
		[DataSourceProperty]
		public string MainActionText
		{
			get
			{
				return this._mainActionText;
			}
			set
			{
				if (value != this._mainActionText)
				{
					this._mainActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "MainActionText");
				}
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0004FB56 File Offset: 0x0004DD56
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0004FB5E File Offset: 0x0004DD5E
		[DataSourceProperty]
		public bool IsMainActionEnabled
		{
			get
			{
				return this._isMainActionEnabled;
			}
			set
			{
				if (value != this._isMainActionEnabled)
				{
					this._isMainActionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsMainActionEnabled");
				}
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0004FB7C File Offset: 0x0004DD7C
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0004FB84 File Offset: 0x0004DD84
		[DataSourceProperty]
		public int ItemValue
		{
			get
			{
				return this._itemValue;
			}
			set
			{
				if (value != this._itemValue)
				{
					this._itemValue = value;
					base.OnPropertyChangedWithValue(value, "ItemValue");
				}
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0004FBA2 File Offset: 0x0004DDA2
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0004FBAA File Offset: 0x0004DDAA
		[DataSourceProperty]
		public HintViewModel CraftingHint
		{
			get
			{
				return this._craftingHint;
			}
			set
			{
				if (value != this._craftingHint)
				{
					this._craftingHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CraftingHint");
				}
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x0004FBD0 File Offset: 0x0004DDD0
		[DataSourceProperty]
		public HintViewModel RefiningHint
		{
			get
			{
				return this._refiningHint;
			}
			set
			{
				if (value != this._refiningHint)
				{
					this._refiningHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RefiningHint");
				}
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0004FBEE File Offset: 0x0004DDEE
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0004FBF6 File Offset: 0x0004DDF6
		[DataSourceProperty]
		public HintViewModel SmeltingHint
		{
			get
			{
				return this._smeltingHint;
			}
			set
			{
				if (value != this._smeltingHint)
				{
					this._smeltingHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SmeltingHint");
				}
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0004FC14 File Offset: 0x0004DE14
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0004FC1C File Offset: 0x0004DE1C
		[DataSourceProperty]
		public HintViewModel ResetCameraHint
		{
			get
			{
				return this._resetCameraHint;
			}
			set
			{
				if (value != this._resetCameraHint)
				{
					this._resetCameraHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetCameraHint");
				}
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0004FC3A File Offset: 0x0004DE3A
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0004FC42 File Offset: 0x0004DE42
		[DataSourceProperty]
		public BasicTooltipViewModel MainActionHint
		{
			get
			{
				return this._mainActionHint;
			}
			set
			{
				if (value != this._mainActionHint)
				{
					this._mainActionHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "MainActionHint");
				}
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0004FC60 File Offset: 0x0004DE60
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0004FC68 File Offset: 0x0004DE68
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0004FC8B File Offset: 0x0004DE8B
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x0004FC93 File Offset: 0x0004DE93
		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelLbl");
				}
			}
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public void ExecuteSwitchToCrafting()
		{
			this.IsInSmeltingMode = false;
			this.IsInCraftingMode = true;
			this.IsInRefinementMode = false;
			this.CurrentCategoryText = new TextObject("{=POjDNVW3}Forging", null).ToString();
			this.MainActionText = GameTexts.FindText("str_crafting_category_crafting", null).ToString();
			CraftingVM.OnItemRefreshedDelegate onItemRefreshed = this.OnItemRefreshed;
			if (onItemRefreshed != null)
			{
				onItemRefreshed(true);
			}
			this.UpdateCurrentMaterialCosts();
			this.UpdateAll();
			WeaponDesignVM weaponDesign = this.WeaponDesign;
			if (weaponDesign == null)
			{
				return;
			}
			weaponDesign.ChangeModeIfHeroIsUnavailable();
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0004FD34 File Offset: 0x0004DF34
		public void ExecuteSwitchToSmelting()
		{
			this.IsInSmeltingMode = true;
			this.IsInCraftingMode = false;
			this.IsInRefinementMode = false;
			this.CurrentCategoryText = new TextObject("{=4cU98rkg}Smelting", null).ToString();
			this.MainActionText = GameTexts.FindText("str_crafting_category_smelting", null).ToString();
			CraftingVM.OnItemRefreshedDelegate onItemRefreshed = this.OnItemRefreshed;
			if (onItemRefreshed != null)
			{
				onItemRefreshed(false);
			}
			this.UpdateCurrentMaterialCosts();
			this.Smelting.RefreshList();
			this.UpdateAll();
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0004FDAC File Offset: 0x0004DFAC
		public void ExecuteSwitchToRefinement()
		{
			this.IsInSmeltingMode = false;
			this.IsInCraftingMode = false;
			this.IsInRefinementMode = true;
			this.CurrentCategoryText = new TextObject("{=p7raHA9x}Refinement", null).ToString();
			this.MainActionText = GameTexts.FindText("str_crafting_category_refinement", null).ToString();
			CraftingVM.OnItemRefreshedDelegate onItemRefreshed = this.OnItemRefreshed;
			if (onItemRefreshed != null)
			{
				onItemRefreshed(false);
			}
			this.UpdateCurrentMaterialCosts();
			this.Refinement.RefreshRefinementActionsList(this.CurrentCraftingHero.Hero);
			this.UpdateAll();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004FE2E File Offset: 0x0004E02E
		private void OnRefinementSelectionChange()
		{
			this.UpdateCurrentMaterialCosts();
			this.RefreshEnableMainAction();
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0004FE3C File Offset: 0x0004E03C
		private void OnSmeltItemSelection()
		{
			this.UpdateCurrentMaterialCosts();
			this.RefreshEnableMainAction();
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004FE4A File Offset: 0x0004E04A
		public void SetCurrentDesignManually(CraftingTemplate craftingTemplate, ValueTuple<CraftingPiece, int>[] pieces)
		{
			if (!this.IsInCraftingMode)
			{
				this.ExecuteSwitchToCrafting();
			}
			this.WeaponDesign.SetDesignManually(craftingTemplate, pieces, true);
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0004FE68 File Offset: 0x0004E068
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x0004FE70 File Offset: 0x0004E070
		[DataSourceProperty]
		public SmeltingVM Smelting
		{
			get
			{
				return this._smelting;
			}
			set
			{
				if (value != this._smelting)
				{
					this._smelting = value;
					base.OnPropertyChangedWithValue<SmeltingVM>(value, "Smelting");
				}
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0004FE8E File Offset: 0x0004E08E
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x0004FE96 File Offset: 0x0004E096
		[DataSourceProperty]
		public WeaponDesignVM WeaponDesign
		{
			get
			{
				return this._weaponDesign;
			}
			set
			{
				if (value != this._weaponDesign)
				{
					this._weaponDesign = value;
					base.OnPropertyChangedWithValue<WeaponDesignVM>(value, "WeaponDesign");
				}
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0004FEB4 File Offset: 0x0004E0B4
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x0004FEBC File Offset: 0x0004E0BC
		[DataSourceProperty]
		public RefinementVM Refinement
		{
			get
			{
				return this._refinement;
			}
			set
			{
				if (value != this._refinement)
				{
					this._refinement = value;
					base.OnPropertyChangedWithValue<RefinementVM>(value, "Refinement");
				}
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0004FEDA File Offset: 0x0004E0DA
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x0004FEE2 File Offset: 0x0004E0E2
		[DataSourceProperty]
		public bool IsInCraftingMode
		{
			get
			{
				return this._isInCraftingMode;
			}
			set
			{
				if (value != this._isInCraftingMode)
				{
					this._isInCraftingMode = value;
					base.OnPropertyChangedWithValue(value, "IsInCraftingMode");
				}
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0004FF00 File Offset: 0x0004E100
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x0004FF08 File Offset: 0x0004E108
		[DataSourceProperty]
		public bool IsInSmeltingMode
		{
			get
			{
				return this._isInSmeltingMode;
			}
			set
			{
				if (value != this._isInSmeltingMode)
				{
					this._isInSmeltingMode = value;
					base.OnPropertyChangedWithValue(value, "IsInSmeltingMode");
				}
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004FF26 File Offset: 0x0004E126
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0004FF2E File Offset: 0x0004E12E
		[DataSourceProperty]
		public bool IsInRefinementMode
		{
			get
			{
				return this._isInRefinementMode;
			}
			set
			{
				if (value != this._isInRefinementMode)
				{
					this._isInRefinementMode = value;
					base.OnPropertyChangedWithValue(value, "IsInRefinementMode");
				}
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0004FF4C File Offset: 0x0004E14C
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0004FF54 File Offset: 0x0004E154
		[DataSourceProperty]
		public bool IsSmeltingItemSelected
		{
			get
			{
				return this._isSmeltingItemSelected;
			}
			set
			{
				if (value != this._isSmeltingItemSelected)
				{
					this._isSmeltingItemSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSmeltingItemSelected");
				}
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0004FF72 File Offset: 0x0004E172
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0004FF7A File Offset: 0x0004E17A
		[DataSourceProperty]
		public bool IsRefinementItemSelected
		{
			get
			{
				return this._isRefinementItemSelected;
			}
			set
			{
				if (value != this._isRefinementItemSelected)
				{
					this._isRefinementItemSelected = value;
					base.OnPropertyChangedWithValue(value, "IsRefinementItemSelected");
				}
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0004FF98 File Offset: 0x0004E198
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
		[DataSourceProperty]
		public string SelectItemToSmeltText
		{
			get
			{
				return this._selectItemToSmeltText;
			}
			set
			{
				if (value != this._selectItemToSmeltText)
				{
					this._selectItemToSmeltText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectItemToSmeltText");
				}
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0004FFC3 File Offset: 0x0004E1C3
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x0004FFCB File Offset: 0x0004E1CB
		[DataSourceProperty]
		public string SelectItemToRefineText
		{
			get
			{
				return this._selectItemToRefineText;
			}
			set
			{
				if (value != this._selectItemToRefineText)
				{
					this._selectItemToRefineText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectItemToRefineText");
				}
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0004FFEE File Offset: 0x0004E1EE
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x0004FFF6 File Offset: 0x0004E1F6
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

		// Token: 0x06001526 RID: 5414 RVA: 0x00050014 File Offset: 0x0004E214
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

		// Token: 0x040009A5 RID: 2469
		private const int _minimumRequiredStamina = 10;

		// Token: 0x040009A6 RID: 2470
		public CraftingVM.OnItemRefreshedDelegate OnItemRefreshed;

		// Token: 0x040009A7 RID: 2471
		private readonly Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> _getItemUsageSetFlags;

		// Token: 0x040009A8 RID: 2472
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x040009A9 RID: 2473
		private readonly Action _onClose;

		// Token: 0x040009AA RID: 2474
		private readonly Action _resetCamera;

		// Token: 0x040009AB RID: 2475
		private readonly Action _onWeaponCrafted;

		// Token: 0x040009AC RID: 2476
		private Crafting _crafting;

		// Token: 0x040009AD RID: 2477
		private InputKeyItemVM _confirmInputKey;

		// Token: 0x040009AE RID: 2478
		private InputKeyItemVM _exitInputKey;

		// Token: 0x040009AF RID: 2479
		private InputKeyItemVM _previousTabInputKey;

		// Token: 0x040009B0 RID: 2480
		private InputKeyItemVM _nextTabInputKey;

		// Token: 0x040009B1 RID: 2481
		private MBBindingList<InputKeyItemVM> _cameraControlKeys;

		// Token: 0x040009B2 RID: 2482
		private bool _canSwitchTabs;

		// Token: 0x040009B3 RID: 2483
		private bool _areGamepadControlHintsEnabled;

		// Token: 0x040009B4 RID: 2484
		private string _doneLbl;

		// Token: 0x040009B5 RID: 2485
		private string _cancelLbl;

		// Token: 0x040009B6 RID: 2486
		private HintViewModel _resetCameraHint;

		// Token: 0x040009B7 RID: 2487
		private HintViewModel _smeltingHint;

		// Token: 0x040009B8 RID: 2488
		private HintViewModel _craftingHint;

		// Token: 0x040009B9 RID: 2489
		private HintViewModel _refiningHint;

		// Token: 0x040009BA RID: 2490
		private BasicTooltipViewModel _mainActionHint;

		// Token: 0x040009BB RID: 2491
		private int _itemValue = -1;

		// Token: 0x040009BC RID: 2492
		private string _currentCategoryText;

		// Token: 0x040009BD RID: 2493
		private string _mainActionText;

		// Token: 0x040009BE RID: 2494
		private string _craftingText;

		// Token: 0x040009BF RID: 2495
		private string _smeltingText;

		// Token: 0x040009C0 RID: 2496
		private string _refinementText;

		// Token: 0x040009C1 RID: 2497
		private bool _isMainActionEnabled;

		// Token: 0x040009C2 RID: 2498
		private MBBindingList<CraftingAvailableHeroItemVM> _availableCharactersForSmithing;

		// Token: 0x040009C3 RID: 2499
		private CraftingAvailableHeroItemVM _currentCraftingHero;

		// Token: 0x040009C4 RID: 2500
		private MBBindingList<CraftingResourceItemVM> _playerCurrentMaterials;

		// Token: 0x040009C5 RID: 2501
		private CraftingHeroPopupVM _craftingHeroPopup;

		// Token: 0x040009C6 RID: 2502
		private bool _isInSmeltingMode;

		// Token: 0x040009C7 RID: 2503
		private bool _isInCraftingMode;

		// Token: 0x040009C8 RID: 2504
		private bool _isInRefinementMode;

		// Token: 0x040009C9 RID: 2505
		private SmeltingVM _smelting;

		// Token: 0x040009CA RID: 2506
		private RefinementVM _refinement;

		// Token: 0x040009CB RID: 2507
		private WeaponDesignVM _weaponDesign;

		// Token: 0x040009CC RID: 2508
		private bool _isSmeltingItemSelected;

		// Token: 0x040009CD RID: 2509
		private bool _isRefinementItemSelected;

		// Token: 0x040009CE RID: 2510
		private string _selectItemToSmeltText;

		// Token: 0x040009CF RID: 2511
		private string _selectItemToRefineText;

		// Token: 0x040009D0 RID: 2512
		public ElementNotificationVM _tutorialNotification;

		// Token: 0x040009D1 RID: 2513
		private string _latestTutorialElementID;

		// Token: 0x02000214 RID: 532
		// (Invoke) Token: 0x06002233 RID: 8755
		public delegate void OnItemRefreshedDelegate(bool isItemVisible);
	}
}
