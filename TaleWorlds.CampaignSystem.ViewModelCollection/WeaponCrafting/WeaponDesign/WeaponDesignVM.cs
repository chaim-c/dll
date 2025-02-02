using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign.Order;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000F0 RID: 240
	public class WeaponDesignVM : ViewModel
	{
		// Token: 0x06001607 RID: 5639 RVA: 0x00051F38 File Offset: 0x00050138
		public WeaponDesignVM(Crafting crafting, ICraftingCampaignBehavior craftingBehavior, Action onRefresh, Action onWeaponCrafted, Func<CraftingAvailableHeroItemVM> getCurrentCraftingHero, Action<CraftingOrder> refreshHeroAvailabilities, Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> getItemUsageSetFlags)
		{
			this._crafting = crafting;
			this._craftingBehavior = craftingBehavior;
			this._onRefresh = onRefresh;
			this._onWeaponCrafted = onWeaponCrafted;
			this._getCurrentCraftingHero = getCurrentCraftingHero;
			this._getItemUsageSetFlags = getItemUsageSetFlags;
			this._refreshHeroAvailabilities = refreshHeroAvailabilities;
			this.MaxDifficulty = 300;
			this._currentCraftingSkillText = new TextObject("{=LEiZWuZm}{SKILL_NAME}: {SKILL_VALUE}", null);
			this.PrimaryPropertyList = new MBBindingList<CraftingListPropertyItem>();
			this.DesignResultPropertyList = new MBBindingList<WeaponDesignResultPropertyItemVM>();
			this._newlyUnlockedPieces = new List<CraftingPiece>();
			this._pieceTierComparer = new WeaponDesignVM.PieceTierComparer();
			this.BladePieceList = new CraftingPieceListVM(new MBBindingList<CraftingPieceVM>(), CraftingPiece.PieceTypes.Blade, new Action<CraftingPiece.PieceTypes>(this.OnSelectPieceType));
			this.GuardPieceList = new CraftingPieceListVM(new MBBindingList<CraftingPieceVM>(), CraftingPiece.PieceTypes.Guard, new Action<CraftingPiece.PieceTypes>(this.OnSelectPieceType));
			this.HandlePieceList = new CraftingPieceListVM(new MBBindingList<CraftingPieceVM>(), CraftingPiece.PieceTypes.Handle, new Action<CraftingPiece.PieceTypes>(this.OnSelectPieceType));
			this.PommelPieceList = new CraftingPieceListVM(new MBBindingList<CraftingPieceVM>(), CraftingPiece.PieceTypes.Pommel, new Action<CraftingPiece.PieceTypes>(this.OnSelectPieceType));
			this.PieceLists = new MBBindingList<CraftingPieceListVM>
			{
				this.BladePieceList,
				this.GuardPieceList,
				this.HandlePieceList,
				this.PommelPieceList
			};
			this._pieceListsDictionary = new Dictionary<CraftingPiece.PieceTypes, CraftingPieceListVM>
			{
				{
					CraftingPiece.PieceTypes.Blade,
					this.BladePieceList
				},
				{
					CraftingPiece.PieceTypes.Guard,
					this.GuardPieceList
				},
				{
					CraftingPiece.PieceTypes.Handle,
					this.HandlePieceList
				},
				{
					CraftingPiece.PieceTypes.Pommel,
					this.PommelPieceList
				}
			};
			this._pieceVMs = new Dictionary<CraftingPiece, CraftingPieceVM>();
			this.TierFilters = new MBBindingList<TierFilterTypeVM>
			{
				new TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter.All, new Action<WeaponDesignVM.CraftingPieceTierFilter>(this.OnSelectPieceTierFilter), GameTexts.FindText("str_crafting_tier_filter_all", null).ToString()),
				new TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter.Tier1, new Action<WeaponDesignVM.CraftingPieceTierFilter>(this.OnSelectPieceTierFilter), GameTexts.FindText("str_tier_one", null).ToString()),
				new TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter.Tier2, new Action<WeaponDesignVM.CraftingPieceTierFilter>(this.OnSelectPieceTierFilter), GameTexts.FindText("str_tier_two", null).ToString()),
				new TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter.Tier3, new Action<WeaponDesignVM.CraftingPieceTierFilter>(this.OnSelectPieceTierFilter), GameTexts.FindText("str_tier_three", null).ToString()),
				new TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter.Tier4, new Action<WeaponDesignVM.CraftingPieceTierFilter>(this.OnSelectPieceTierFilter), GameTexts.FindText("str_tier_four", null).ToString()),
				new TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter.Tier5, new Action<WeaponDesignVM.CraftingPieceTierFilter>(this.OnSelectPieceTierFilter), GameTexts.FindText("str_tier_five", null).ToString())
			};
			this._templateComparer = new WeaponDesignVM.TemplateComparer();
			this._primaryUsages = CraftingTemplate.All.ToList<CraftingTemplate>();
			this._primaryUsages.Sort(this._templateComparer);
			this.SecondaryUsageSelector = new SelectorVM<CraftingSecondaryUsageItemVM>(new List<string>(), 0, null);
			this.CraftingOrderPopup = new CraftingOrderPopupVM(new Action<CraftingOrderItemVM>(this.OnCraftingOrderSelected), this._getCurrentCraftingHero, new Func<CraftingOrder, IEnumerable<CraftingStatData>>(this.GetOrderStatDatas));
			this.WeaponClassSelectionPopup = new WeaponClassSelectionPopupVM(this._craftingBehavior, this._primaryUsages, delegate(int x)
			{
				this.RefreshWeaponDesignMode(null, x, false);
			}, new Func<CraftingTemplate, int>(this.GetUnlockedPartsCount));
			this.WeaponFlagIconsList = new MBBindingList<ItemFlagVM>();
			this.CraftedItemVisual = new ItemCollectionElementViewModel();
			CampaignEvents.CraftingPartUnlockedEvent.AddNonSerializedListener(this, new Action<CraftingPiece>(this.OnNewPieceUnlocked));
			this.CraftingHistory = new CraftingHistoryVM(this._crafting, this._craftingBehavior, delegate()
			{
				CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
				if (activeCraftingOrder == null)
				{
					return null;
				}
				return activeCraftingOrder.CraftingOrder;
			}, new Action<WeaponDesignSelectorVM>(this.OnSelectItemFromHistory));
			this.RefreshWeaponDesignMode(null, -1, false);
			this._selectedWeaponClassIndex = this._primaryUsages.IndexOf(this._crafting.CurrentCraftingTemplate);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00052308 File Offset: 0x00050508
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ShowOnlyUnlockedPiecesHint = new HintViewModel(new TextObject("{=dOa7frHR}Show only unlocked pieces", null), null);
			this.ComponentSizeLbl = new TextObject("{=OkWLI5C8}Size:", null).ToString();
			this.AlternativeUsageText = new TextObject("{=13wo3QQB}Secondary", null).ToString();
			this.DefaultUsageText = new TextObject("{=ta4R2RR7}Primary", null).ToString();
			this.DifficultyText = GameTexts.FindText("str_difficulty", null).ToString();
			this.ScabbardHint = new HintViewModel(GameTexts.FindText("str_toggle_scabbard", null), null);
			this.RandomizeHint = new HintViewModel(GameTexts.FindText("str_randomize", null), null);
			this.UndoHint = new HintViewModel(GameTexts.FindText("str_undo", null), null);
			this.RedoHint = new HintViewModel(GameTexts.FindText("str_redo", null), null);
			this.OrderDisabledReasonHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetOrdersDisabledReasonTooltip(this.CraftingOrderPopup.CraftingOrders, this._getCurrentCraftingHero().Hero));
			this._primaryPropertyList.ApplyActionOnAllItems(delegate(CraftingListPropertyItem x)
			{
				x.RefreshValues();
			});
			CraftingPieceVM selectedBladePiece = this._selectedBladePiece;
			if (selectedBladePiece != null)
			{
				selectedBladePiece.RefreshValues();
			}
			CraftingPieceVM selectedGuardPiece = this._selectedGuardPiece;
			if (selectedGuardPiece != null)
			{
				selectedGuardPiece.RefreshValues();
			}
			CraftingPieceVM selectedHandlePiece = this._selectedHandlePiece;
			if (selectedHandlePiece != null)
			{
				selectedHandlePiece.RefreshValues();
			}
			CraftingPieceVM selectedPommelPiece = this._selectedPommelPiece;
			if (selectedPommelPiece != null)
			{
				selectedPommelPiece.RefreshValues();
			}
			this._secondaryUsageSelector.RefreshValues();
			this._craftingOrderPopup.RefreshValues();
			this.ChooseOrderText = this.CraftingOrderPopup.OrderCountText;
			this.ChooseWeaponTypeText = new TextObject("{=Gd6zuUwh}Free Build", null).ToString();
			this.CurrentCraftedWeaponTypeText = this._crafting.CurrentCraftingTemplate.TemplateName.ToString();
			this.CurrentCraftedWeaponTemplateId = this._crafting.CurrentCraftingTemplate.StringId;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000524DC File Offset: 0x000506DC
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.CraftingPartUnlockedEvent.ClearListeners(this);
			CraftingHistoryVM craftingHistory = this.CraftingHistory;
			if (craftingHistory != null)
			{
				craftingHistory.OnFinalize();
			}
			ItemCollectionElementViewModel craftedItemVisual = this.CraftedItemVisual;
			if (craftedItemVisual != null)
			{
				craftedItemVisual.OnFinalize();
			}
			WeaponDesignResultPopupVM craftingResultPopup = this.CraftingResultPopup;
			if (craftingResultPopup != null)
			{
				craftingResultPopup.OnFinalize();
			}
			this.CraftedItemVisual = null;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00052534 File Offset: 0x00050734
		internal void OnCraftingLogicRefreshed(Crafting newCraftingLogic)
		{
			this._crafting = newCraftingLogic;
			this.InitializeDefaultFromLogic();
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00052544 File Offset: 0x00050744
		private void FilterPieces(WeaponDesignVM.CraftingPieceTierFilter filter)
		{
			List<int> list = new List<int>();
			switch (filter)
			{
			case WeaponDesignVM.CraftingPieceTierFilter.None:
				goto IL_9B;
			case WeaponDesignVM.CraftingPieceTierFilter.Tier1:
				list.Add(1);
				goto IL_9B;
			case WeaponDesignVM.CraftingPieceTierFilter.Tier2:
				list.Add(2);
				goto IL_9B;
			case WeaponDesignVM.CraftingPieceTierFilter.Tier1 | WeaponDesignVM.CraftingPieceTierFilter.Tier2:
			case WeaponDesignVM.CraftingPieceTierFilter.Tier1 | WeaponDesignVM.CraftingPieceTierFilter.Tier3:
			case WeaponDesignVM.CraftingPieceTierFilter.Tier2 | WeaponDesignVM.CraftingPieceTierFilter.Tier3:
			case WeaponDesignVM.CraftingPieceTierFilter.Tier1 | WeaponDesignVM.CraftingPieceTierFilter.Tier2 | WeaponDesignVM.CraftingPieceTierFilter.Tier3:
				break;
			case WeaponDesignVM.CraftingPieceTierFilter.Tier3:
				list.Add(3);
				goto IL_9B;
			case WeaponDesignVM.CraftingPieceTierFilter.Tier4:
				list.Add(4);
				goto IL_9B;
			default:
				if (filter == WeaponDesignVM.CraftingPieceTierFilter.Tier5)
				{
					list.Add(5);
					goto IL_9B;
				}
				if (filter == WeaponDesignVM.CraftingPieceTierFilter.All)
				{
					list.AddRange(new int[]
					{
						1,
						2,
						3,
						4,
						5
					});
					goto IL_9B;
				}
				break;
			}
			Debug.FailedAssert("Invalid tier filter", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Crafting\\WeaponDesign\\WeaponDesignVM.cs", "FilterPieces", 217);
			IL_9B:
			foreach (TierFilterTypeVM tierFilterTypeVM in this.TierFilters)
			{
				tierFilterTypeVM.IsSelected = filter.HasAllFlags(tierFilterTypeVM.FilterType);
			}
			foreach (CraftingPieceListVM craftingPieceListVM in this.PieceLists)
			{
				foreach (CraftingPieceVM craftingPieceVM in craftingPieceListVM.Pieces)
				{
					bool flag = list.Contains(craftingPieceVM.CraftingPiece.CraftingPiece.PieceTier);
					bool flag2 = this.ShowOnlyUnlockedPieces && !craftingPieceVM.PlayerHasPiece;
					craftingPieceVM.IsFilteredOut = (!flag || flag2);
				}
			}
			this._currentTierFilter = filter;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000526EC File Offset: 0x000508EC
		private void OnNewPieceUnlocked(CraftingPiece piece)
		{
			if (piece.IsValid && !piece.IsHiddenOnDesigner)
			{
				this.SetPieceNewlyUnlocked(piece);
				CraftingPieceVM craftingPieceVM;
				if (this._pieceVMs.TryGetValue(piece, out craftingPieceVM))
				{
					craftingPieceVM.PlayerHasPiece = true;
					craftingPieceVM.IsNewlyUnlocked = true;
				}
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00052730 File Offset: 0x00050930
		private int GetUnlockedPartsCount(CraftingTemplate template)
		{
			return template.Pieces.Count((CraftingPiece piece) => this._craftingBehavior.IsOpened(piece, template) && !string.IsNullOrEmpty(piece.MeshName));
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0005276D File Offset: 0x0005096D
		private WeaponClassVM GetCurrentWeaponClass()
		{
			if (this._selectedWeaponClassIndex >= 0 && this._selectedWeaponClassIndex < this.WeaponClassSelectionPopup.WeaponClasses.Count)
			{
				return this.WeaponClassSelectionPopup.WeaponClasses[this._selectedWeaponClassIndex];
			}
			return null;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000527A8 File Offset: 0x000509A8
		private void OnSelectItemFromHistory(WeaponDesignSelectorVM selector)
		{
			WeaponDesign design = selector.Design;
			if (design == null)
			{
				Debug.FailedAssert("History design returned null", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Crafting\\WeaponDesign\\WeaponDesignVM.cs", "OnSelectItemFromHistory", 280);
				return;
			}
			ValueTuple<CraftingPiece, int>[] array = new ValueTuple<CraftingPiece, int>[design.UsedPieces.Length];
			for (int i = 0; i < design.UsedPieces.Length; i++)
			{
				array[i] = new ValueTuple<CraftingPiece, int>(design.UsedPieces[i].CraftingPiece, design.UsedPieces[i].ScalePercentage);
			}
			this.SetDesignManually(design.Template, array, true);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00052834 File Offset: 0x00050A34
		public void SetPieceNewlyUnlocked(CraftingPiece piece)
		{
			if (!this._newlyUnlockedPieces.Contains(piece))
			{
				this._newlyUnlockedPieces.Add(piece);
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00052850 File Offset: 0x00050A50
		private void UnsetPieceNewlyUnlocked(CraftingPieceVM pieceVM)
		{
			CraftingPiece craftingPiece = pieceVM.CraftingPiece.CraftingPiece;
			if (this._newlyUnlockedPieces.Contains(craftingPiece))
			{
				this._newlyUnlockedPieces.Remove(craftingPiece);
				pieceVM.IsNewlyUnlocked = false;
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0005288B File Offset: 0x00050A8B
		private void OnSelectPieceTierFilter(WeaponDesignVM.CraftingPieceTierFilter filter)
		{
			if (this._currentTierFilter != filter)
			{
				this.FilterPieces(filter);
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000528A0 File Offset: 0x00050AA0
		private void OnSelectPieceType(CraftingPiece.PieceTypes pieceType)
		{
			CraftingPieceListVM craftingPieceListVM = this.PieceLists.ElementAt(this.SelectedPieceTypeIndex);
			if (craftingPieceListVM != null)
			{
				foreach (CraftingPieceVM craftingPieceVM in craftingPieceListVM.Pieces)
				{
					if (craftingPieceVM.IsNewlyUnlocked)
					{
						this.UnsetPieceNewlyUnlocked(craftingPieceVM);
					}
				}
			}
			CraftingPieceListVM craftingPieceListVM2 = this.PieceLists.FirstOrDefault((CraftingPieceListVM x) => x.PieceType == pieceType);
			foreach (CraftingPieceListVM craftingPieceListVM3 in this.PieceLists)
			{
				craftingPieceListVM3.Refresh();
				craftingPieceListVM3.IsSelected = (craftingPieceListVM3 == craftingPieceListVM2);
			}
			this.SelectedPieceTypeIndex = (int)pieceType;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00052988 File Offset: 0x00050B88
		private void SelectDefaultPiecesForCurrentTemplate()
		{
			CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
			string text = (activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder.GetStatWeapon().WeaponDescriptionId : null;
			WeaponDescription statWeaponUsage = (text != null) ? MBObjectManager.Instance.GetObject<WeaponDescription>(text) : null;
			WeaponClassVM currentWeaponClass = this.GetCurrentWeaponClass();
			this._shouldRecordHistory = false;
			this._isAutoSelectingPieces = true;
			Func<CraftingPieceVM, bool> <>9__3;
			foreach (CraftingPieceListVM craftingPieceListVM in this.PieceLists)
			{
				if (this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(craftingPieceListVM.PieceType))
				{
					CraftingPieceVM craftingPieceVM = null;
					if (this.IsInFreeMode && currentWeaponClass != null)
					{
						string selectedPieceID = currentWeaponClass.GetSelectedPieceData(craftingPieceListVM.PieceType);
						craftingPieceVM = craftingPieceListVM.Pieces.FirstOrDefault((CraftingPieceVM p) => p.CraftingPiece.CraftingPiece.StringId == selectedPieceID);
					}
					if (craftingPieceVM == null)
					{
						IOrderedEnumerable<CraftingPieceVM> source = from p in craftingPieceListVM.Pieces
						orderby p.PlayerHasPiece descending, !p.IsNewlyUnlocked descending
						select p;
						Func<CraftingPieceVM, bool> keySelector;
						if ((keySelector = <>9__3) == null)
						{
							keySelector = (<>9__3 = ((CraftingPieceVM p) => statWeaponUsage == null || statWeaponUsage.AvailablePieces.Any((CraftingPiece x) => x.StringId == p.CraftingPiece.CraftingPiece.StringId)));
						}
						craftingPieceVM = source.ThenByDescending(keySelector).FirstOrDefault<CraftingPieceVM>();
					}
					if (craftingPieceVM != null)
					{
						craftingPieceVM.ExecuteSelect();
					}
				}
			}
			this._shouldRecordHistory = true;
			this._isAutoSelectingPieces = false;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00052B2C File Offset: 0x00050D2C
		private void InitializeDefaultFromLogic()
		{
			this.PrimaryPropertyList.Clear();
			this.BladePieceList.Pieces.Clear();
			this.GuardPieceList.Pieces.Clear();
			this.HandlePieceList.Pieces.Clear();
			this.PommelPieceList.Pieces.Clear();
			this.SelectedBladePiece = new CraftingPieceVM();
			this.SelectedGuardPiece = new CraftingPieceVM();
			this.SelectedHandlePiece = new CraftingPieceVM();
			this.SelectedPommelPiece = new CraftingPieceVM();
			this._pieceVMs.Clear();
			bool flag = Campaign.Current.GameMode == CampaignGameMode.Tutorial;
			foreach (CraftingPieceListVM craftingPieceListVM in this.PieceLists)
			{
				if (this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(craftingPieceListVM.PieceType))
				{
					int pieceType = (int)craftingPieceListVM.PieceType;
					for (int i = 0; i < this._crafting.UsablePiecesList[pieceType].Count; i++)
					{
						WeaponDesignElement weaponDesignElement = this._crafting.UsablePiecesList[pieceType][i];
						if (flag || !weaponDesignElement.CraftingPiece.IsHiddenOnDesigner)
						{
							bool flag2 = this._craftingBehavior.IsOpened(weaponDesignElement.CraftingPiece, this._crafting.CurrentCraftingTemplate);
							CraftingPieceVM craftingPieceVM = new CraftingPieceVM(new Action<CraftingPieceVM>(this.OnSetItemPieceManually), this._crafting.CurrentCraftingTemplate.StringId, this._crafting.UsablePiecesList[pieceType][i], pieceType, i, flag2);
							craftingPieceListVM.Pieces.Add(craftingPieceVM);
							craftingPieceVM.IsNewlyUnlocked = (flag2 && this._newlyUnlockedPieces.Contains(weaponDesignElement.CraftingPiece));
							if (this._crafting.SelectedPieces[pieceType].CraftingPiece == craftingPieceVM.CraftingPiece.CraftingPiece)
							{
								craftingPieceListVM.SelectedPiece = craftingPieceVM;
								craftingPieceVM.IsSelected = true;
							}
							this._pieceVMs.Add(this._crafting.UsablePiecesList[pieceType][i].CraftingPiece, craftingPieceVM);
						}
					}
					craftingPieceListVM.Pieces.Sort(this._pieceTierComparer);
				}
			}
			CraftingPieceListVM craftingPieceListVM2 = this.PieceLists.FirstOrDefault((CraftingPieceListVM x) => x.PieceType == CraftingPiece.PieceTypes.Blade);
			this.SelectedBladePiece = ((craftingPieceListVM2 != null) ? craftingPieceListVM2.SelectedPiece : null);
			CraftingPieceListVM craftingPieceListVM3 = this.PieceLists.FirstOrDefault((CraftingPieceListVM x) => x.PieceType == CraftingPiece.PieceTypes.Guard);
			this.SelectedGuardPiece = ((craftingPieceListVM3 != null) ? craftingPieceListVM3.SelectedPiece : null);
			CraftingPieceListVM craftingPieceListVM4 = this.PieceLists.FirstOrDefault((CraftingPieceListVM x) => x.PieceType == CraftingPiece.PieceTypes.Handle);
			this.SelectedHandlePiece = ((craftingPieceListVM4 != null) ? craftingPieceListVM4.SelectedPiece : null);
			CraftingPieceListVM craftingPieceListVM5 = this.PieceLists.FirstOrDefault((CraftingPieceListVM x) => x.PieceType == CraftingPiece.PieceTypes.Pommel);
			this.SelectedPommelPiece = ((craftingPieceListVM5 != null) ? craftingPieceListVM5.SelectedPiece : null);
			this.ItemName = this._crafting.CraftedWeaponName.ToString();
			this.PommelSize = 0;
			this.GuardSize = 0;
			this.HandleSize = 0;
			this.BladeSize = 0;
			this.RefreshPieceFlags();
			this.RefreshItem();
			this.RefreshAlternativeUsageList();
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00052EAC File Offset: 0x000510AC
		private void RefreshPieceFlags()
		{
			foreach (CraftingPieceListVM craftingPieceListVM in this.PieceLists)
			{
				craftingPieceListVM.IsEnabled = this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(craftingPieceListVM.PieceType);
				foreach (CraftingPieceVM craftingPieceVM in craftingPieceListVM.Pieces)
				{
					craftingPieceVM.RefreshFlagIcons();
					if (craftingPieceListVM.PieceType == CraftingPiece.PieceTypes.Blade)
					{
						this.AddClassFlagsToPiece(craftingPieceVM);
					}
				}
			}
			this.RefreshWeaponFlags();
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00052F60 File Offset: 0x00051160
		private void AddClassFlagsToPiece(CraftingPieceVM piece)
		{
			WeaponComponentData weaponWithUsageIndex = this._crafting.GetCurrentCraftedItemObject(false).GetWeaponWithUsageIndex(this.SecondaryUsageSelector.SelectedIndex);
			int indexOfUsageDataWithId = this._crafting.CurrentCraftingTemplate.GetIndexOfUsageDataWithId(weaponWithUsageIndex.WeaponDescriptionId);
			WeaponDescription weaponDescription = this._crafting.CurrentCraftingTemplate.WeaponDescriptions.ElementAtOrDefault(indexOfUsageDataWithId);
			if (weaponDescription != null)
			{
				using (List<ValueTuple<string, TextObject>>.Enumerator enumerator = CampaignUIHelper.GetWeaponFlagDetails(weaponDescription.WeaponFlags, null).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ValueTuple<string, TextObject> flagPath = enumerator.Current;
						if (!piece.ItemAttributeIcons.Any((CraftingItemFlagVM x) => x.Icon.Contains(flagPath.Item1)))
						{
							piece.ItemAttributeIcons.Add(new CraftingItemFlagVM(flagPath.Item1, flagPath.Item2, false));
						}
					}
				}
			}
			using (List<ValueTuple<string, TextObject>>.Enumerator enumerator = CampaignUIHelper.GetFlagDetailsForWeapon(weaponWithUsageIndex, this._getItemUsageSetFlags(weaponWithUsageIndex), null).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ValueTuple<string, TextObject> usageFlag = enumerator.Current;
					if (!piece.ItemAttributeIcons.Any((CraftingItemFlagVM x) => x.Icon.Contains(usageFlag.Item1)))
					{
						piece.ItemAttributeIcons.Add(new CraftingItemFlagVM(usageFlag.Item1, usageFlag.Item2, false));
					}
				}
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000530EC File Offset: 0x000512EC
		private void UpdateSecondaryUsageIndex(SelectorVM<CraftingSecondaryUsageItemVM> selector)
		{
			if (selector.SelectedIndex != -1)
			{
				this.RefreshStats();
				this.RefreshPieceFlags();
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00053104 File Offset: 0x00051304
		private MBBindingList<WeaponDesignResultPropertyItemVM> GetResultPropertyList(CraftingSecondaryUsageItemVM usageItem)
		{
			MBBindingList<WeaponDesignResultPropertyItemVM> mbbindingList = new MBBindingList<WeaponDesignResultPropertyItemVM>();
			if (usageItem == null)
			{
				return mbbindingList;
			}
			int usageIndex = usageItem.UsageIndex;
			this.TrySetSecondaryUsageIndex(usageIndex);
			this.RefreshStats();
			ItemModifier currentItemModifier = this._craftingBehavior.GetCurrentItemModifier();
			foreach (CraftingListPropertyItem craftingListPropertyItem in this.PrimaryPropertyList)
			{
				float changeAmount = 0f;
				bool showFloatingPoint = craftingListPropertyItem.Type == CraftingTemplate.CraftingStatTypes.Weight;
				if (currentItemModifier != null)
				{
					float num = craftingListPropertyItem.PropertyValue;
					if (craftingListPropertyItem.Type == CraftingTemplate.CraftingStatTypes.SwingDamage)
					{
						num = (float)currentItemModifier.ModifyDamage((int)craftingListPropertyItem.PropertyValue);
					}
					else if (craftingListPropertyItem.Type == CraftingTemplate.CraftingStatTypes.SwingSpeed)
					{
						num = (float)currentItemModifier.ModifySpeed((int)craftingListPropertyItem.PropertyValue);
					}
					else if (craftingListPropertyItem.Type == CraftingTemplate.CraftingStatTypes.ThrustDamage)
					{
						num = (float)currentItemModifier.ModifyDamage((int)craftingListPropertyItem.PropertyValue);
					}
					else if (craftingListPropertyItem.Type == CraftingTemplate.CraftingStatTypes.ThrustSpeed)
					{
						num = (float)currentItemModifier.ModifySpeed((int)craftingListPropertyItem.PropertyValue);
					}
					else if (craftingListPropertyItem.Type == CraftingTemplate.CraftingStatTypes.Handling)
					{
						num = (float)currentItemModifier.ModifySpeed((int)craftingListPropertyItem.PropertyValue);
					}
					if (num != craftingListPropertyItem.PropertyValue)
					{
						changeAmount = num - craftingListPropertyItem.PropertyValue;
					}
				}
				if (this.IsInOrderMode)
				{
					mbbindingList.Add(new WeaponDesignResultPropertyItemVM(craftingListPropertyItem.Description, craftingListPropertyItem.PropertyValue, craftingListPropertyItem.TargetValue, changeAmount, showFloatingPoint, craftingListPropertyItem.IsExceedingBeneficial, true));
				}
				else
				{
					mbbindingList.Add(new WeaponDesignResultPropertyItemVM(craftingListPropertyItem.Description, craftingListPropertyItem.PropertyValue, changeAmount, showFloatingPoint));
				}
			}
			return mbbindingList;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000532A4 File Offset: 0x000514A4
		public void SelectPrimaryWeaponClass(CraftingTemplate template)
		{
			int selectedWeaponClassIndex = this._primaryUsages.IndexOf(template);
			this._selectedWeaponClassIndex = selectedWeaponClassIndex;
			if (this._crafting.CurrentCraftingTemplate != template)
			{
				CraftingHelper.ChangeCurrentCraftingTemplate(template);
				return;
			}
			this.AddHistoryKey();
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000532E0 File Offset: 0x000514E0
		private void RefreshWeaponDesignMode(CraftingOrderItemVM orderToSelect, int classIndex = -1, bool doNotAutoSelectPieces = false)
		{
			bool flag = false;
			CraftingTemplate selectedCraftingTemplate = null;
			this.SecondaryUsageSelector.SelectedIndex = 0;
			if (orderToSelect != null)
			{
				this.IsInOrderMode = true;
				this.ActiveCraftingOrder = orderToSelect;
				selectedCraftingTemplate = orderToSelect.CraftingOrder.PreCraftedWeaponDesignItem.WeaponDesign.Template;
				this.SelectPrimaryWeaponClass(selectedCraftingTemplate);
				flag = true;
			}
			else
			{
				this.IsInOrderMode = false;
				this.ActiveCraftingOrder = null;
				if (classIndex >= 0)
				{
					selectedCraftingTemplate = this._primaryUsages[classIndex];
					this.SelectPrimaryWeaponClass(selectedCraftingTemplate);
					flag = true;
				}
			}
			WeaponClassVM weaponClassVM = this.WeaponClassSelectionPopup.WeaponClasses.FirstOrDefault((WeaponClassVM x) => x.Template == selectedCraftingTemplate);
			if (weaponClassVM != null)
			{
				weaponClassVM.NewlyUnlockedPieceCount = 0;
			}
			this.CraftingOrderPopup.RefreshOrders();
			this.CraftingHistory.RefreshAvailability();
			this.IsOrderButtonActive = this.CraftingOrderPopup.HasEnabledOrders;
			Action onRefresh = this._onRefresh;
			if (onRefresh != null)
			{
				onRefresh();
			}
			Action<CraftingOrder> refreshHeroAvailabilities = this._refreshHeroAvailabilities;
			if (refreshHeroAvailabilities != null)
			{
				CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
				refreshHeroAvailabilities((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null);
			}
			if (!flag)
			{
				this.InitializeDefaultFromLogic();
			}
			this.RefreshValues();
			this.RefreshItem();
			this.OnSelectPieceType(CraftingPiece.PieceTypes.Blade);
			this.FilterPieces(this._currentTierFilter);
			this.RefreshCurrentHeroSkillLevel();
			if (!doNotAutoSelectPieces)
			{
				this.SelectDefaultPiecesForCurrentTemplate();
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0005342F File Offset: 0x0005162F
		private void OnCraftingOrderSelected(CraftingOrderItemVM selectedOrder)
		{
			this.RefreshWeaponDesignMode(selectedOrder, -1, false);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0005343C File Offset: 0x0005163C
		public void ExecuteOpenOrderPopup()
		{
			this.CraftingOrderPopup.ExecuteOpenPopup();
			MBBindingList<CraftingOrderItemVM> craftingOrders = this.CraftingOrderPopup.CraftingOrders;
			CraftingOrderItemVM craftingOrderItemVM = (craftingOrders != null) ? craftingOrders.FirstOrDefault(delegate(CraftingOrderItemVM x)
			{
				CraftingOrder craftingOrder = x.CraftingOrder;
				CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
				return craftingOrder == ((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null);
			}) : null;
			if (craftingOrderItemVM != null)
			{
				craftingOrderItemVM.IsSelected = true;
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00053482 File Offset: 0x00051682
		public void ExecuteCloseOrderPopup()
		{
			this.CraftingOrderPopup.IsVisible = false;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00053490 File Offset: 0x00051690
		public void ExecuteOpenOrdersTab()
		{
			if (this.IsInFreeMode)
			{
				MBBindingList<CraftingOrderItemVM> craftingOrders = this.CraftingOrderPopup.CraftingOrders;
				CraftingOrderItemVM craftingOrderItemVM;
				if (craftingOrders == null)
				{
					craftingOrderItemVM = null;
				}
				else
				{
					craftingOrderItemVM = craftingOrders.FirstOrDefault((CraftingOrderItemVM x) => x.IsEnabled);
				}
				CraftingOrderItemVM craftingOrderItemVM2 = craftingOrderItemVM;
				if (craftingOrderItemVM2 != null)
				{
					this.CraftingOrderPopup.SelectOrder(craftingOrderItemVM2);
					return;
				}
				this.CraftingOrderPopup.ExecuteOpenPopup();
			}
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x000534F7 File Offset: 0x000516F7
		public void ExecuteOpenWeaponClassSelectionPopup()
		{
			this.WeaponClassSelectionPopup.UpdateNewlyUnlockedPiecesCount(this._newlyUnlockedPieces);
			this.WeaponClassSelectionPopup.WeaponClasses.ApplyActionOnAllItems(delegate(WeaponClassVM x)
			{
				x.IsSelected = (x.SelectionIndex == this._selectedWeaponClassIndex);
			});
			this.WeaponClassSelectionPopup.ExecuteOpenPopup();
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00053534 File Offset: 0x00051734
		public void ExecuteOpenFreeBuildTab()
		{
			if (this.IsInOrderMode)
			{
				this.WeaponClassSelectionPopup.UpdateNewlyUnlockedPiecesCount(this._newlyUnlockedPieces);
				this.WeaponClassSelectionPopup.WeaponClasses.ApplyActionOnAllItems(delegate(WeaponClassVM x)
				{
					x.IsSelected = false;
				});
				this.WeaponClassSelectionPopup.ExecuteSelectWeaponClass(0);
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00053598 File Offset: 0x00051798
		public void CreateCraftingResultPopup()
		{
			this.CraftedItemVisual.StringId = this.CraftedItemObject.StringId;
			this.IsWeaponCivilian = this.CraftedItemObject.IsCivilian;
			WeaponDesignResultPopupVM craftingResultPopup = this.CraftingResultPopup;
			if (craftingResultPopup != null)
			{
				craftingResultPopup.OnFinalize();
			}
			ItemObject craftedItemObject = this.CraftedItemObject;
			string itemName = this._itemName;
			Action onFinalize = new Action(this.ExecuteFinalizeCrafting);
			Crafting crafting = this._crafting;
			CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
			this.CraftingResultPopup = new WeaponDesignResultPopupVM(craftedItemObject, itemName, onFinalize, crafting, (activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null, this._craftedItemVisual, this.WeaponFlagIconsList, new Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>>(this.GetResultPropertyList), new Action<CraftingSecondaryUsageItemVM>(this.OnSecondaryUsageChangedFromPopup));
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0005363C File Offset: 0x0005183C
		private void OnSecondaryUsageChangedFromPopup(CraftingSecondaryUsageItemVM usage)
		{
			for (int i = 0; i < this.SecondaryUsageSelector.ItemList.Count; i++)
			{
				if (this.SecondaryUsageSelector.ItemList[i].UsageIndex == usage.UsageIndex)
				{
					this.SecondaryUsageSelector.SelectedIndex = i;
					return;
				}
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0005368F File Offset: 0x0005188F
		public void ExecuteToggleShowOnlyUnlockedPieces()
		{
			this.ShowOnlyUnlockedPieces = !this.ShowOnlyUnlockedPieces;
			this.FilterPieces(this._currentTierFilter);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000536AC File Offset: 0x000518AC
		public void ExecuteUndo()
		{
			if (this._crafting.Undo())
			{
				Action onRefresh = this._onRefresh;
				if (onRefresh != null)
				{
					onRefresh();
				}
				this._updatePiece = false;
				int i2;
				int i;
				for (i = 0; i < 4; i = i2 + 1)
				{
					CraftingPiece.PieceTypes j = (CraftingPiece.PieceTypes)i;
					if (this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(j))
					{
						CraftingPieceVM piece2 = this._pieceListsDictionary[j].Pieces.First((CraftingPieceVM piece) => piece.CraftingPiece.CraftingPiece == this._crafting.SelectedPieces[i].CraftingPiece);
						this.OnSetItemPiece(piece2, 0, true, false);
					}
					i2 = i;
				}
				this.RefreshItem();
				this._updatePiece = true;
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00053764 File Offset: 0x00051964
		public void ExecuteRedo()
		{
			if (this._crafting.Redo())
			{
				Action onRefresh = this._onRefresh;
				if (onRefresh != null)
				{
					onRefresh();
				}
				this._updatePiece = false;
				int i2;
				int i;
				for (i = 0; i < 4; i = i2 + 1)
				{
					CraftingPiece.PieceTypes j = (CraftingPiece.PieceTypes)i;
					if (this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(j))
					{
						CraftingPieceVM piece2 = this._pieceListsDictionary[j].Pieces.First((CraftingPieceVM piece) => piece.CraftingPiece.CraftingPiece == this._crafting.SelectedPieces[i].CraftingPiece);
						this.OnSetItemPiece(piece2, 0, true, false);
					}
					i2 = i;
				}
				this.RefreshItem();
				this._updatePiece = true;
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0005381C File Offset: 0x00051A1C
		internal void OnCraftingHeroChanged(CraftingAvailableHeroItemVM newHero)
		{
			this.RefreshCurrentHeroSkillLevel();
			this.RefreshDifficulty();
			this.CraftingOrderPopup.RefreshOrders();
			this.IsOrderButtonActive = this.CraftingOrderPopup.HasEnabledOrders;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00053848 File Offset: 0x00051A48
		public void ChangeModeIfHeroIsUnavailable()
		{
			CraftingAvailableHeroItemVM craftingAvailableHeroItemVM = this._getCurrentCraftingHero();
			if (this.IsInOrderMode && craftingAvailableHeroItemVM.IsDisabled)
			{
				this.RefreshWeaponDesignMode(null, -1, false);
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0005387C File Offset: 0x00051A7C
		public void ExecuteBeginHeroHint()
		{
			CraftingOrderItemVM activeCraftingOrder = this._activeCraftingOrder;
			if (((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder.OrderOwner : null) != null)
			{
				InformationManager.ShowTooltip(typeof(Hero), new object[]
				{
					this._activeCraftingOrder.CraftingOrder.OrderOwner,
					false
				});
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000538D3 File Offset: 0x00051AD3
		public void ExecuteEndHeroHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000538DC File Offset: 0x00051ADC
		public void ExecuteRandomize()
		{
			for (int i = 0; i < 4; i++)
			{
				CraftingPiece.PieceTypes pieceTypes = (CraftingPiece.PieceTypes)i;
				if (this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(pieceTypes))
				{
					CraftingPieceVM randomElementWithPredicate = this._pieceListsDictionary[pieceTypes].Pieces.GetRandomElementWithPredicate((CraftingPieceVM p) => p.PlayerHasPiece);
					if (randomElementWithPredicate != null)
					{
						this.OnSetItemPiece(randomElementWithPredicate, (int)(90f + MBRandom.RandomFloat * 20f), false, true);
					}
				}
			}
			this._updatePiece = false;
			this.RefreshItem();
			this.AddHistoryKey();
			this._updatePiece = true;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00053978 File Offset: 0x00051B78
		public void ExecuteChangeScabbardVisibility()
		{
			if (!this._crafting.CurrentCraftingTemplate.UseWeaponAsHolsterMesh)
			{
				this.IsScabbardVisible = !this.IsScabbardVisible;
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0005399C File Offset: 0x00051B9C
		public void SelectWeapon(ItemObject itemObject)
		{
			this._crafting.SwitchToCraftedItem(itemObject);
			Action onRefresh = this._onRefresh;
			if (onRefresh != null)
			{
				onRefresh();
			}
			this._updatePiece = false;
			int i;
			int i2;
			for (i = 0; i < 4; i = i2 + 1)
			{
				CraftingPiece.PieceTypes j = (CraftingPiece.PieceTypes)i;
				if (this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(j))
				{
					CraftingPieceVM piece2 = this._pieceListsDictionary[j].Pieces.First((CraftingPieceVM piece) => piece.CraftingPiece.CraftingPiece == this._crafting.CurrentWeaponDesign.UsedPieces[i].CraftingPiece);
					this.OnSetItemPiece(piece2, this._crafting.CurrentWeaponDesign.UsedPieces[i].ScalePercentage, true, false);
				}
				i2 = i;
			}
			this.RefreshItem();
			this.AddHistoryKey();
			this._updatePiece = true;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00053A74 File Offset: 0x00051C74
		public bool CanCompleteOrder()
		{
			bool result = true;
			if (this.IsInOrderMode)
			{
				ItemObject currentCraftedItemObject = this._crafting.GetCurrentCraftedItemObject(false);
				result = this.ActiveCraftingOrder.CraftingOrder.CanHeroCompleteOrder(this._getCurrentCraftingHero().Hero, currentCraftedItemObject);
			}
			return result;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00053ABC File Offset: 0x00051CBC
		public void ExecuteFinalizeCrafting()
		{
			if (this._craftingBehavior != null && Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				if (GameStateManager.Current.ActiveState is CraftingState)
				{
					if (this.IsInOrderMode)
					{
						this._craftingBehavior.CompleteOrder(Settlement.CurrentSettlement.Town, this.ActiveCraftingOrder.CraftingOrder, this.CraftedItemObject, this._getCurrentCraftingHero().Hero);
						this.CraftedItemObject = null;
						this.CraftingOrderPopup.RefreshOrders();
						CraftingOrderItemVM craftingOrderItemVM = this.CraftingOrderPopup.CraftingOrders.FirstOrDefault((CraftingOrderItemVM x) => x.IsEnabled);
						if (craftingOrderItemVM != null)
						{
							this.CraftingOrderPopup.SelectOrder(craftingOrderItemVM);
						}
						else
						{
							this.ExecuteOpenFreeBuildTab();
						}
					}
					else
					{
						int bladeSize = this.BladeSize;
						int guardSize = this.GuardSize;
						int handleSize = this.HandleSize;
						int pommelSize = this.PommelSize;
						this.RefreshWeaponDesignMode(null, this._selectedWeaponClassIndex, false);
						this.BladeSize = bladeSize;
						this.GuardSize = guardSize;
						this.HandleSize = handleSize;
						this.PommelSize = pommelSize;
					}
				}
				this.IsInFinalCraftingStage = false;
			}
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00053BE3 File Offset: 0x00051DE3
		private bool DoesCurrentItemHaveSecondaryUsage(int usageIndex)
		{
			return usageIndex >= 0 && usageIndex < this._crafting.GetCurrentCraftedItemObject(false).Weapons.Count;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00053C04 File Offset: 0x00051E04
		private void TrySetSecondaryUsageIndex(int usageIndex)
		{
			int num = 0;
			if (this.DoesCurrentItemHaveSecondaryUsage(usageIndex))
			{
				CraftingSecondaryUsageItemVM craftingSecondaryUsageItemVM = this.SecondaryUsageSelector.ItemList.FirstOrDefault((CraftingSecondaryUsageItemVM x) => x.UsageIndex == usageIndex);
				if (craftingSecondaryUsageItemVM != null)
				{
					num = craftingSecondaryUsageItemVM.SelectorIndex;
				}
			}
			if (num >= 0 && num < this.SecondaryUsageSelector.ItemList.Count)
			{
				this.SecondaryUsageSelector.SelectedIndex = num;
				this.SecondaryUsageSelector.ItemList[num].IsSelected = true;
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00053C90 File Offset: 0x00051E90
		private void RefreshAlternativeUsageList()
		{
			int usageIndex = this.SecondaryUsageSelector.SelectedIndex;
			this.SecondaryUsageSelector.Refresh(new List<string>(), 0, new Action<SelectorVM<CraftingSecondaryUsageItemVM>>(this.UpdateSecondaryUsageIndex));
			MBReadOnlyList<WeaponComponentData> weapons = this._crafting.GetCurrentCraftedItemObject(false).Weapons;
			int num = 0;
			for (int i = 0; i < weapons.Count; i++)
			{
				if (CampaignUIHelper.IsItemUsageApplicable(weapons[i]))
				{
					TextObject name = GameTexts.FindText("str_weapon_usage", weapons[i].WeaponDescriptionId);
					this.SecondaryUsageSelector.AddItem(new CraftingSecondaryUsageItemVM(name, num, i, this.SecondaryUsageSelector));
					CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
					if (((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder.GetStatWeapon().WeaponDescriptionId : null) == weapons[i].WeaponDescriptionId)
					{
						usageIndex = num;
					}
					num++;
				}
			}
			this.TrySetSecondaryUsageIndex(usageIndex);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00053D6C File Offset: 0x00051F6C
		private void RefreshStats()
		{
			if (!this.DoesCurrentItemHaveSecondaryUsage(this.SecondaryUsageSelector.SelectedIndex))
			{
				this.TrySetSecondaryUsageIndex(0);
			}
			List<CraftingStatData> list = this._crafting.GetStatDatas(this.SecondaryUsageSelector.SelectedIndex).ToList<CraftingStatData>();
			WeaponComponentData weaponComponentData = this.IsInOrderMode ? this.ActiveCraftingOrder.CraftingOrder.GetStatWeapon() : null;
			IEnumerable<CraftingStatData> enumerable = this.IsInOrderMode ? this.GetOrderStatDatas(this.ActiveCraftingOrder.CraftingOrder) : null;
			ItemObject currentCraftedItemObject = this._crafting.GetCurrentCraftedItemObject(false);
			WeaponComponentData weaponWithUsageIndex = currentCraftedItemObject.GetWeaponWithUsageIndex(this.SecondaryUsageSelector.SelectedIndex);
			bool flag = weaponComponentData == null || weaponComponentData.WeaponDescriptionId == weaponWithUsageIndex.WeaponDescriptionId;
			if (enumerable != null)
			{
				using (IEnumerator<CraftingStatData> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CraftingStatData orderStatData = enumerator.Current;
						if (!list.Any((CraftingStatData x) => x.Type == orderStatData.Type && x.DamageType == orderStatData.DamageType))
						{
							if ((orderStatData.Type == CraftingTemplate.CraftingStatTypes.SwingDamage && orderStatData.DamageType != weaponWithUsageIndex.SwingDamageType) || (orderStatData.Type == CraftingTemplate.CraftingStatTypes.ThrustDamage && orderStatData.DamageType != weaponWithUsageIndex.ThrustDamageType))
							{
								list.Add(new CraftingStatData(orderStatData.DescriptionText, 0f, orderStatData.MaxValue, orderStatData.Type, orderStatData.DamageType));
							}
							else
							{
								list.Add(orderStatData);
							}
						}
					}
				}
			}
			this.PrimaryPropertyList.Clear();
			using (List<CraftingStatData>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					CraftingStatData statData = enumerator2.Current;
					if (statData.IsValid)
					{
						float num = 0f;
						if (this.IsInOrderMode && flag)
						{
							WeaponAttributeVM weaponAttributeVM = this.ActiveCraftingOrder.WeaponAttributes.FirstOrDefault((WeaponAttributeVM x) => x.AttributeType == statData.Type && x.DamageType == statData.DamageType);
							num = ((weaponAttributeVM != null) ? weaponAttributeVM.AttributeValue : 0f);
						}
						float maxValue = MathF.Max(statData.MaxValue, num);
						CraftingListPropertyItem craftingListPropertyItem = new CraftingListPropertyItem(statData.DescriptionText, maxValue, statData.CurValue, num, statData.Type, false);
						this.PrimaryPropertyList.Add(craftingListPropertyItem);
						craftingListPropertyItem.IsValidForUsage = true;
					}
				}
			}
			this.PrimaryPropertyList.Sort(new WeaponDesignVM.WeaponPropertyComparer());
			CraftingOrderItemVM activeCraftingOrder = this.ActiveCraftingOrder;
			this.MissingPropertyWarningText = CampaignUIHelper.GetCraftingOrderMissingPropertyWarningText((activeCraftingOrder != null) ? activeCraftingOrder.CraftingOrder : null, currentCraftedItemObject);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00054054 File Offset: 0x00052254
		private IEnumerable<CraftingStatData> GetOrderStatDatas(CraftingOrder order)
		{
			if (order == null)
			{
				return null;
			}
			WeaponComponentData weaponComponentData;
			return order.GetStatDataForItem(order.PreCraftedWeaponDesignItem, out weaponComponentData);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00054074 File Offset: 0x00052274
		private void RefreshWeaponFlags()
		{
			this.WeaponFlagIconsList.Clear();
			foreach (CraftingPieceListVM craftingPieceListVM in this.PieceLists)
			{
				if (craftingPieceListVM.SelectedPiece != null)
				{
					using (IEnumerator<CraftingItemFlagVM> enumerator2 = craftingPieceListVM.SelectedPiece.ItemAttributeIcons.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							CraftingItemFlagVM iconData = enumerator2.Current;
							if (!this.WeaponFlagIconsList.Any((ItemFlagVM x) => x.Icon == iconData.Icon))
							{
								this.WeaponFlagIconsList.Add(iconData);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0005413C File Offset: 0x0005233C
		private void OnSetItemPieceManually(CraftingPieceVM piece)
		{
			this.OnSetItemPiece(piece, 0, true, false);
			this.RefreshItem();
			this.AddHistoryKey();
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00054154 File Offset: 0x00052354
		private void OnSetItemPiece(CraftingPieceVM piece, int scalePercentage = 0, bool shouldUpdateWholeWeapon = true, bool forceUpdatePiece = false)
		{
			CraftingPiece.PieceTypes pieceType = (CraftingPiece.PieceTypes)piece.PieceType;
			this._pieceListsDictionary[pieceType].SelectedPiece.IsSelected = false;
			bool updatePiece = this._updatePiece;
			if (!this._isAutoSelectingPieces)
			{
				this.UnsetPieceNewlyUnlocked(piece);
			}
			if (updatePiece)
			{
				this._crafting.SwitchToPiece(piece.CraftingPiece);
				if (!forceUpdatePiece)
				{
					this._updatePiece = false;
				}
			}
			piece.IsSelected = true;
			this._pieceListsDictionary[pieceType].SelectedPiece = piece;
			int num = ((scalePercentage != 0) ? scalePercentage : this._crafting.SelectedPieces[(int)pieceType].ScalePercentage) - 100;
			switch (pieceType)
			{
			case CraftingPiece.PieceTypes.Blade:
				this.BladeSize = num;
				this.SelectedBladePiece = piece;
				break;
			case CraftingPiece.PieceTypes.Guard:
				this.GuardSize = num;
				this.SelectedGuardPiece = piece;
				break;
			case CraftingPiece.PieceTypes.Handle:
				this.HandleSize = num;
				this.SelectedHandlePiece = piece;
				break;
			case CraftingPiece.PieceTypes.Pommel:
				this.PommelSize = num;
				this.SelectedPommelPiece = piece;
				break;
			}
			if (this.IsInFreeMode)
			{
				WeaponClassVM currentWeaponClass = this.GetCurrentWeaponClass();
				if (currentWeaponClass != null)
				{
					currentWeaponClass.RegisterSelectedPiece(pieceType, piece.CraftingPiece.CraftingPiece.StringId);
				}
			}
			this._updatePiece = updatePiece;
			this.RefreshAlternativeUsageList();
			if (shouldUpdateWholeWeapon)
			{
				Action onRefresh = this._onRefresh;
				if (onRefresh != null)
				{
					onRefresh();
				}
			}
			this.PieceLists.ApplyActionOnAllItems(delegate(CraftingPieceListVM x)
			{
				x.Refresh();
			});
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000542B3 File Offset: 0x000524B3
		public void RefreshItem()
		{
			this.RefreshStats();
			this.RefreshWeaponFlags();
			this.RefreshDifficulty();
			Action onRefresh = this._onRefresh;
			if (onRefresh == null)
			{
				return;
			}
			onRefresh();
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000542D8 File Offset: 0x000524D8
		private void RefreshDifficulty()
		{
			this.CurrentDifficulty = Campaign.Current.Models.SmithingModel.CalculateWeaponDesignDifficulty(this._crafting.CurrentWeaponDesign);
			if (this.IsInOrderMode)
			{
				this.CurrentOrderDifficulty = MathF.Round(this.ActiveCraftingOrder.CraftingOrder.OrderDifficulty);
			}
			this._currentCraftingSkillText.SetTextVariable("SKILL_VALUE", this.CurrentHeroCraftingSkill);
			this._currentCraftingSkillText.SetTextVariable("SKILL_NAME", DefaultSkills.Crafting.Name);
			this.CurrentCraftingSkillValueText = this._currentCraftingSkillText.ToString();
			this.CurrentDifficultyText = this.GetCurrentDifficultyText(this.CurrentHeroCraftingSkill, this.CurrentDifficulty);
			this.CurrentOrderDifficultyText = this.GetCurrentOrderDifficultyText(this.CurrentOrderDifficulty);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0005439A File Offset: 0x0005259A
		private string GetCurrentDifficultyText(int skillValue, int difficultyValue)
		{
			this._difficultyTextobj.SetTextVariable("DIFFICULTY", difficultyValue);
			return this._difficultyTextobj.ToString();
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000543B9 File Offset: 0x000525B9
		private string GetCurrentOrderDifficultyText(int orderDifficulty)
		{
			this._orderDifficultyTextObj.SetTextVariable("DIFFICULTY", orderDifficulty.ToString());
			return this._orderDifficultyTextObj.ToString();
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000543E0 File Offset: 0x000525E0
		private void RefreshCurrentHeroSkillLevel()
		{
			Func<CraftingAvailableHeroItemVM> getCurrentCraftingHero = this._getCurrentCraftingHero;
			int? num;
			if (getCurrentCraftingHero == null)
			{
				num = null;
			}
			else
			{
				CraftingAvailableHeroItemVM craftingAvailableHeroItemVM = getCurrentCraftingHero();
				num = ((craftingAvailableHeroItemVM != null) ? new int?(craftingAvailableHeroItemVM.Hero.CharacterObject.GetSkillValue(DefaultSkills.Crafting)) : null);
			}
			this.CurrentHeroCraftingSkill = (num ?? 0);
			this.IsCurrentHeroAtMaxCraftingSkill = (this.CurrentHeroCraftingSkill >= 300);
			this._currentCraftingSkillText.SetTextVariable("SKILL_VALUE", this.CurrentHeroCraftingSkill);
			this.CurrentCraftingSkillValueText = this._currentCraftingSkillText.ToString();
			this.CurrentDifficultyText = this.GetCurrentDifficultyText(this.CurrentHeroCraftingSkill, this.CurrentDifficulty);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000544A0 File Offset: 0x000526A0
		public bool HaveUnlockedAllSelectedPieces()
		{
			foreach (CraftingPieceListVM craftingPieceListVM in this.PieceLists)
			{
				if (craftingPieceListVM.IsEnabled)
				{
					CraftingPieceVM selectedPiece = craftingPieceListVM.SelectedPiece;
					if (((selectedPiece != null) ? selectedPiece.CraftingPiece : null) != null && !craftingPieceListVM.SelectedPiece.PlayerHasPiece)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00054518 File Offset: 0x00052718
		private void AddHistoryKey()
		{
			if (this._shouldRecordHistory)
			{
				this._crafting.UpdateHistory();
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00054530 File Offset: 0x00052730
		public void SwitchToPiece(WeaponDesignElement usedPiece)
		{
			CraftingPieceVM piece = this._pieceListsDictionary[usedPiece.CraftingPiece.PieceType].Pieces.FirstOrDefault((CraftingPieceVM p) => p.CraftingPiece.CraftingPiece == usedPiece.CraftingPiece);
			this.OnSetItemPiece(piece, usedPiece.ScalePercentage, true, false);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00054590 File Offset: 0x00052790
		internal void SetDesignManually(CraftingTemplate craftingTemplate, ValueTuple<CraftingPiece, int>[] pieces, bool forceChangeTemplate = false)
		{
			int num = this._primaryUsages.IndexOf(craftingTemplate);
			if ((this.IsInFreeMode && forceChangeTemplate) || num == this._selectedWeaponClassIndex)
			{
				this.RefreshWeaponDesignMode(this.ActiveCraftingOrder, this._primaryUsages.IndexOf(craftingTemplate), true);
				for (int i = 0; i < pieces.Length; i++)
				{
					ValueTuple<CraftingPiece, int> currentPiece = pieces[i];
					if (currentPiece.Item1 != null)
					{
						CraftingPieceVM craftingPieceVM = this._pieceListsDictionary[currentPiece.Item1.PieceType].Pieces.FirstOrDefault((CraftingPieceVM piece) => piece.CraftingPiece.CraftingPiece == currentPiece.Item1);
						if (craftingPieceVM != null)
						{
							this.OnSetItemPiece(craftingPieceVM, currentPiece.Item2, true, false);
							this._crafting.ScaleThePiece(currentPiece.Item1.PieceType, currentPiece.Item2);
						}
					}
				}
				this.RefreshDifficulty();
				Action onRefresh = this._onRefresh;
				if (onRefresh == null)
				{
					return;
				}
				onRefresh();
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00054692 File Offset: 0x00052892
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0005469A File Offset: 0x0005289A
		[DataSourceProperty]
		public MBBindingList<TierFilterTypeVM> TierFilters
		{
			get
			{
				return this._tierFilters;
			}
			set
			{
				if (value != this._tierFilters)
				{
					this._tierFilters = value;
					base.OnPropertyChangedWithValue<MBBindingList<TierFilterTypeVM>>(value, "TierFilters");
				}
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x000546B8 File Offset: 0x000528B8
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x000546C0 File Offset: 0x000528C0
		[DataSourceProperty]
		public string CurrentCraftedWeaponTemplateId
		{
			get
			{
				return this._currentCraftedWeaponTemplateId;
			}
			set
			{
				if (value != this._currentCraftedWeaponTemplateId)
				{
					this._currentCraftedWeaponTemplateId = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCraftedWeaponTemplateId");
				}
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000546E3 File Offset: 0x000528E3
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x000546EB File Offset: 0x000528EB
		[DataSourceProperty]
		public string ChooseOrderText
		{
			get
			{
				return this._chooseOrderText;
			}
			set
			{
				if (value != this._chooseOrderText)
				{
					this._chooseOrderText = value;
					base.OnPropertyChangedWithValue<string>(value, "ChooseOrderText");
				}
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0005470E File Offset: 0x0005290E
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x00054716 File Offset: 0x00052916
		[DataSourceProperty]
		public string ChooseWeaponTypeText
		{
			get
			{
				return this._chooseWeaponTypeText;
			}
			set
			{
				if (value != this._chooseWeaponTypeText)
				{
					this._chooseWeaponTypeText = value;
					base.OnPropertyChangedWithValue<string>(value, "ChooseWeaponTypeText");
				}
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00054739 File Offset: 0x00052939
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x00054741 File Offset: 0x00052941
		[DataSourceProperty]
		public string CurrentCraftedWeaponTypeText
		{
			get
			{
				return this._currentCraftedWeaponTypeText;
			}
			set
			{
				if (value != this._currentCraftedWeaponTypeText)
				{
					this._currentCraftedWeaponTypeText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCraftedWeaponTypeText");
				}
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00054764 File Offset: 0x00052964
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x0005476C File Offset: 0x0005296C
		[DataSourceProperty]
		public MBBindingList<CraftingPieceListVM> PieceLists
		{
			get
			{
				return this._pieceLists;
			}
			set
			{
				if (value != this._pieceLists)
				{
					this._pieceLists = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingPieceListVM>>(value, "PieceLists");
				}
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0005478A File Offset: 0x0005298A
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x00054792 File Offset: 0x00052992
		[DataSourceProperty]
		public int SelectedPieceTypeIndex
		{
			get
			{
				return this._selectedPieceTypeIndex;
			}
			set
			{
				if (value != this._selectedPieceTypeIndex)
				{
					this._selectedPieceTypeIndex = value;
					base.OnPropertyChangedWithValue(value, "SelectedPieceTypeIndex");
				}
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000547B0 File Offset: 0x000529B0
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x000547B8 File Offset: 0x000529B8
		[DataSourceProperty]
		public bool ShowOnlyUnlockedPieces
		{
			get
			{
				return this._showOnlyUnlockedPieces;
			}
			set
			{
				if (value != this._showOnlyUnlockedPieces)
				{
					this._showOnlyUnlockedPieces = value;
					base.OnPropertyChangedWithValue(value, "ShowOnlyUnlockedPieces");
				}
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000547D6 File Offset: 0x000529D6
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x000547DE File Offset: 0x000529DE
		[DataSourceProperty]
		public string MissingPropertyWarningText
		{
			get
			{
				return this._missingPropertyWarningText;
			}
			set
			{
				if (value != this._missingPropertyWarningText)
				{
					this._missingPropertyWarningText = value;
					base.OnPropertyChangedWithValue<string>(value, "MissingPropertyWarningText");
				}
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00054801 File Offset: 0x00052A01
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x00054809 File Offset: 0x00052A09
		[DataSourceProperty]
		public WeaponDesignResultPopupVM CraftingResultPopup
		{
			get
			{
				return this._craftingResultPopup;
			}
			set
			{
				if (value != this._craftingResultPopup)
				{
					this._craftingResultPopup = value;
					base.OnPropertyChangedWithValue<WeaponDesignResultPopupVM>(value, "CraftingResultPopup");
				}
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00054827 File Offset: 0x00052A27
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x0005482F File Offset: 0x00052A2F
		[DataSourceProperty]
		public bool IsOrderButtonActive
		{
			get
			{
				return this._isOrderButtonActive;
			}
			set
			{
				if (value != this._isOrderButtonActive)
				{
					this._isOrderButtonActive = value;
					base.OnPropertyChangedWithValue(value, "IsOrderButtonActive");
				}
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0005484D File Offset: 0x00052A4D
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x00054855 File Offset: 0x00052A55
		[DataSourceProperty]
		public bool IsInOrderMode
		{
			get
			{
				return this._isInOrderMode;
			}
			set
			{
				if (value != this._isInOrderMode)
				{
					this._isInOrderMode = value;
					base.OnPropertyChangedWithValue(value, "IsInOrderMode");
					base.OnPropertyChanged("IsInFreeMode");
				}
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005487E File Offset: 0x00052A7E
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x00054889 File Offset: 0x00052A89
		[DataSourceProperty]
		public bool IsInFreeMode
		{
			get
			{
				return !this._isInOrderMode;
			}
			set
			{
				if (value != this.IsInFreeMode)
				{
					this._isInOrderMode = !value;
					base.OnPropertyChangedWithValue(value, "IsInFreeMode");
					base.OnPropertyChanged("IsInOrderMode");
				}
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x000548B5 File Offset: 0x00052AB5
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x000548BD File Offset: 0x00052ABD
		[DataSourceProperty]
		public bool WeaponControlsEnabled
		{
			get
			{
				return this._weaponControlsEnabled;
			}
			set
			{
				if (value != this._weaponControlsEnabled)
				{
					this._weaponControlsEnabled = value;
					base.OnPropertyChangedWithValue(value, "WeaponControlsEnabled");
				}
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x000548DB File Offset: 0x00052ADB
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x000548E3 File Offset: 0x00052AE3
		[DataSourceProperty]
		public string FreeModeButtonText
		{
			get
			{
				return this._freeModeButtonText;
			}
			set
			{
				if (value != this._freeModeButtonText)
				{
					this._freeModeButtonText = value;
					base.OnPropertyChangedWithValue<string>(value, "FreeModeButtonText");
				}
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00054906 File Offset: 0x00052B06
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0005490E File Offset: 0x00052B0E
		[DataSourceProperty]
		public CraftingOrderItemVM ActiveCraftingOrder
		{
			get
			{
				return this._activeCraftingOrder;
			}
			set
			{
				if (value != this._activeCraftingOrder)
				{
					this._activeCraftingOrder = value;
					base.OnPropertyChangedWithValue<CraftingOrderItemVM>(value, "ActiveCraftingOrder");
				}
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0005492C File Offset: 0x00052B2C
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x00054934 File Offset: 0x00052B34
		[DataSourceProperty]
		public CraftingOrderPopupVM CraftingOrderPopup
		{
			get
			{
				return this._craftingOrderPopup;
			}
			set
			{
				if (value != this._craftingOrderPopup)
				{
					this._craftingOrderPopup = value;
					base.OnPropertyChangedWithValue<CraftingOrderPopupVM>(value, "CraftingOrderPopup");
				}
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00054952 File Offset: 0x00052B52
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0005495A File Offset: 0x00052B5A
		[DataSourceProperty]
		public WeaponClassSelectionPopupVM WeaponClassSelectionPopup
		{
			get
			{
				return this._weaponClassSelectionPopup;
			}
			set
			{
				if (value != this._weaponClassSelectionPopup)
				{
					this._weaponClassSelectionPopup = value;
					base.OnPropertyChangedWithValue<WeaponClassSelectionPopupVM>(value, "WeaponClassSelectionPopup");
				}
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00054978 File Offset: 0x00052B78
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x00054980 File Offset: 0x00052B80
		[DataSourceProperty]
		public MBBindingList<CraftingListPropertyItem> PrimaryPropertyList
		{
			get
			{
				return this._primaryPropertyList;
			}
			set
			{
				if (value != this._primaryPropertyList)
				{
					this._primaryPropertyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingListPropertyItem>>(value, "PrimaryPropertyList");
				}
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005499E File Offset: 0x00052B9E
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x000549A6 File Offset: 0x00052BA6
		[DataSourceProperty]
		public MBBindingList<WeaponDesignResultPropertyItemVM> DesignResultPropertyList
		{
			get
			{
				return this._designResultPropertyList;
			}
			set
			{
				if (value != this._designResultPropertyList)
				{
					this._designResultPropertyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<WeaponDesignResultPropertyItemVM>>(value, "DesignResultPropertyList");
				}
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x000549C4 File Offset: 0x00052BC4
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x000549CC File Offset: 0x00052BCC
		[DataSourceProperty]
		public SelectorVM<CraftingSecondaryUsageItemVM> SecondaryUsageSelector
		{
			get
			{
				return this._secondaryUsageSelector;
			}
			set
			{
				if (value != this._secondaryUsageSelector)
				{
					this._secondaryUsageSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<CraftingSecondaryUsageItemVM>>(value, "SecondaryUsageSelector");
				}
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x000549EA File Offset: 0x00052BEA
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x000549F2 File Offset: 0x00052BF2
		[DataSourceProperty]
		public ItemCollectionElementViewModel CraftedItemVisual
		{
			get
			{
				return this._craftedItemVisual;
			}
			set
			{
				if (value != this._craftedItemVisual)
				{
					this._craftedItemVisual = value;
					base.OnPropertyChangedWithValue<ItemCollectionElementViewModel>(value, "CraftedItemVisual");
				}
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00054A10 File Offset: 0x00052C10
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x00054A18 File Offset: 0x00052C18
		[DataSourceProperty]
		public bool IsInFinalCraftingStage
		{
			get
			{
				return this._isInFinalCraftingStage;
			}
			set
			{
				if (value != this._isInFinalCraftingStage)
				{
					this._isInFinalCraftingStage = value;
					base.OnPropertyChangedWithValue(value, "IsInFinalCraftingStage");
				}
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00054A36 File Offset: 0x00052C36
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x00054A3E File Offset: 0x00052C3E
		[DataSourceProperty]
		public string ItemName
		{
			get
			{
				return this._itemName;
			}
			set
			{
				if (value != this._itemName)
				{
					this._itemName = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemName");
				}
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00054A61 File Offset: 0x00052C61
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x00054A69 File Offset: 0x00052C69
		[DataSourceProperty]
		public bool IsScabbardVisible
		{
			get
			{
				return this._isScabbardVisible;
			}
			set
			{
				if (value != this._isScabbardVisible)
				{
					this._isScabbardVisible = value;
					base.OnPropertyChangedWithValue(value, "IsScabbardVisible");
					this._crafting.ReIndex(false);
					Action onRefresh = this._onRefresh;
					if (onRefresh == null)
					{
						return;
					}
					onRefresh();
				}
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00054AA3 File Offset: 0x00052CA3
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x00054AAB File Offset: 0x00052CAB
		[DataSourceProperty]
		public bool CurrentWeaponHasScabbard
		{
			get
			{
				return this._currentWeaponHasScabbard;
			}
			set
			{
				if (value != this._currentWeaponHasScabbard)
				{
					this._currentWeaponHasScabbard = value;
					base.OnPropertyChangedWithValue(value, "CurrentWeaponHasScabbard");
				}
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00054AC9 File Offset: 0x00052CC9
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x00054AD1 File Offset: 0x00052CD1
		[DataSourceProperty]
		public int CurrentDifficulty
		{
			get
			{
				return this._currentDifficulty;
			}
			set
			{
				if (value != this._currentDifficulty)
				{
					this._currentDifficulty = value;
					base.OnPropertyChangedWithValue(value, "CurrentDifficulty");
				}
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00054AEF File Offset: 0x00052CEF
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x00054AF7 File Offset: 0x00052CF7
		[DataSourceProperty]
		public int CurrentOrderDifficulty
		{
			get
			{
				return this._currentOrderDifficulty;
			}
			set
			{
				if (value != this._currentOrderDifficulty)
				{
					this._currentOrderDifficulty = value;
					base.OnPropertyChangedWithValue(value, "CurrentOrderDifficulty");
				}
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00054B15 File Offset: 0x00052D15
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x00054B1D File Offset: 0x00052D1D
		[DataSourceProperty]
		public int MaxDifficulty
		{
			get
			{
				return this._maxDifficulty;
			}
			set
			{
				if (value != this._maxDifficulty)
				{
					this._maxDifficulty = value;
					base.OnPropertyChangedWithValue(value, "MaxDifficulty");
				}
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00054B3B File Offset: 0x00052D3B
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x00054B43 File Offset: 0x00052D43
		[DataSourceProperty]
		public bool IsCurrentHeroAtMaxCraftingSkill
		{
			get
			{
				return this._isCurrentHeroAtMaxCraftingSkill;
			}
			set
			{
				if (value != this._isCurrentHeroAtMaxCraftingSkill)
				{
					this._isCurrentHeroAtMaxCraftingSkill = value;
					base.OnPropertyChangedWithValue(value, "IsCurrentHeroAtMaxCraftingSkill");
				}
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00054B61 File Offset: 0x00052D61
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x00054B69 File Offset: 0x00052D69
		[DataSourceProperty]
		public int CurrentHeroCraftingSkill
		{
			get
			{
				return this._currentHeroCraftingSkill;
			}
			set
			{
				if (value != this._currentHeroCraftingSkill)
				{
					this._currentHeroCraftingSkill = value;
					base.OnPropertyChangedWithValue(value, "CurrentHeroCraftingSkill");
				}
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00054B87 File Offset: 0x00052D87
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x00054B8F File Offset: 0x00052D8F
		[DataSourceProperty]
		public string CurrentDifficultyText
		{
			get
			{
				return this._currentDifficultyText;
			}
			set
			{
				if (value != this._currentDifficultyText)
				{
					this._currentDifficultyText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentDifficultyText");
				}
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00054BB2 File Offset: 0x00052DB2
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x00054BBA File Offset: 0x00052DBA
		[DataSourceProperty]
		public string CurrentOrderDifficultyText
		{
			get
			{
				return this._currentOrderDifficultyText;
			}
			set
			{
				if (value != this._currentOrderDifficultyText)
				{
					this._currentOrderDifficultyText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentOrderDifficultyText");
				}
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00054BDD File Offset: 0x00052DDD
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x00054BE5 File Offset: 0x00052DE5
		[DataSourceProperty]
		public string CurrentCraftingSkillValueText
		{
			get
			{
				return this._currentCraftingSkillValueText;
			}
			set
			{
				if (value != this._currentCraftingSkillValueText)
				{
					this._currentCraftingSkillValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCraftingSkillValueText");
				}
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00054C08 File Offset: 0x00052E08
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x00054C10 File Offset: 0x00052E10
		[DataSourceProperty]
		public string DifficultyText
		{
			get
			{
				return this._difficultyText;
			}
			set
			{
				if (value != this._difficultyText)
				{
					this._difficultyText = value;
					base.OnPropertyChangedWithValue<string>(value, "DifficultyText");
				}
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00054C33 File Offset: 0x00052E33
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x00054C3B File Offset: 0x00052E3B
		[DataSourceProperty]
		public string DefaultUsageText
		{
			get
			{
				return this._defaultUsageText;
			}
			set
			{
				if (value != this._defaultUsageText)
				{
					this._defaultUsageText = value;
					base.OnPropertyChangedWithValue<string>(value, "DefaultUsageText");
				}
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00054C5E File Offset: 0x00052E5E
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x00054C66 File Offset: 0x00052E66
		[DataSourceProperty]
		public string AlternativeUsageText
		{
			get
			{
				return this._alternativeUsageText;
			}
			set
			{
				if (value != this._alternativeUsageText)
				{
					this._alternativeUsageText = value;
					base.OnPropertyChangedWithValue<string>(value, "AlternativeUsageText");
				}
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00054C89 File Offset: 0x00052E89
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x00054C91 File Offset: 0x00052E91
		[DataSourceProperty]
		public BasicTooltipViewModel OrderDisabledReasonHint
		{
			get
			{
				return this._orderDisabledReasonHint;
			}
			set
			{
				if (value != this._orderDisabledReasonHint)
				{
					this._orderDisabledReasonHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "OrderDisabledReasonHint");
				}
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00054CAF File Offset: 0x00052EAF
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x00054CB7 File Offset: 0x00052EB7
		[DataSourceProperty]
		public HintViewModel ShowOnlyUnlockedPiecesHint
		{
			get
			{
				return this._showOnlyUnlockedPiecesHint;
			}
			set
			{
				if (value != this._showOnlyUnlockedPiecesHint)
				{
					this._showOnlyUnlockedPiecesHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowOnlyUnlockedPiecesHint");
				}
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00054CD5 File Offset: 0x00052ED5
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x00054CDD File Offset: 0x00052EDD
		[DataSourceProperty]
		public CraftingPieceListVM BladePieceList
		{
			get
			{
				return this._bladePieceList;
			}
			set
			{
				if (value != this._bladePieceList)
				{
					this._bladePieceList = value;
					base.OnPropertyChangedWithValue<CraftingPieceListVM>(value, "BladePieceList");
				}
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00054CFB File Offset: 0x00052EFB
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x00054D03 File Offset: 0x00052F03
		[DataSourceProperty]
		public CraftingPieceListVM GuardPieceList
		{
			get
			{
				return this._guardPieceList;
			}
			set
			{
				if (value != this._guardPieceList)
				{
					this._guardPieceList = value;
					base.OnPropertyChangedWithValue<CraftingPieceListVM>(value, "GuardPieceList");
				}
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00054D21 File Offset: 0x00052F21
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x00054D29 File Offset: 0x00052F29
		[DataSourceProperty]
		public CraftingPieceListVM HandlePieceList
		{
			get
			{
				return this._handlePieceList;
			}
			set
			{
				if (value != this._handlePieceList)
				{
					this._handlePieceList = value;
					base.OnPropertyChangedWithValue<CraftingPieceListVM>(value, "HandlePieceList");
				}
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x00054D47 File Offset: 0x00052F47
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x00054D4F File Offset: 0x00052F4F
		[DataSourceProperty]
		public CraftingPieceListVM PommelPieceList
		{
			get
			{
				return this._pommelPieceList;
			}
			set
			{
				if (value != this._pommelPieceList)
				{
					this._pommelPieceList = value;
					base.OnPropertyChangedWithValue<CraftingPieceListVM>(value, "PommelPieceList");
				}
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00054D6D File Offset: 0x00052F6D
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x00054D75 File Offset: 0x00052F75
		[DataSourceProperty]
		public CraftingPieceVM SelectedBladePiece
		{
			get
			{
				return this._selectedBladePiece;
			}
			set
			{
				if (value != this._selectedBladePiece)
				{
					this._selectedBladePiece = value;
					base.OnPropertyChangedWithValue<CraftingPieceVM>(value, "SelectedBladePiece");
				}
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00054D93 File Offset: 0x00052F93
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x00054D9B File Offset: 0x00052F9B
		[DataSourceProperty]
		public CraftingPieceVM SelectedGuardPiece
		{
			get
			{
				return this._selectedGuardPiece;
			}
			set
			{
				if (value != this._selectedGuardPiece)
				{
					this._selectedGuardPiece = value;
					base.OnPropertyChangedWithValue<CraftingPieceVM>(value, "SelectedGuardPiece");
				}
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00054DB9 File Offset: 0x00052FB9
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x00054DC1 File Offset: 0x00052FC1
		[DataSourceProperty]
		public CraftingPieceVM SelectedHandlePiece
		{
			get
			{
				return this._selectedHandlePiece;
			}
			set
			{
				if (value != this._selectedHandlePiece)
				{
					this._selectedHandlePiece = value;
					base.OnPropertyChangedWithValue<CraftingPieceVM>(value, "SelectedHandlePiece");
				}
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00054DDF File Offset: 0x00052FDF
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x00054DE7 File Offset: 0x00052FE7
		[DataSourceProperty]
		public CraftingPieceVM SelectedPommelPiece
		{
			get
			{
				return this._selectedPommelPiece;
			}
			set
			{
				if (value != this._selectedPommelPiece)
				{
					this._selectedPommelPiece = value;
					base.OnPropertyChangedWithValue<CraftingPieceVM>(value, "SelectedPommelPiece");
				}
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00054E05 File Offset: 0x00053005
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x00054E10 File Offset: 0x00053010
		[DataSourceProperty]
		public int BladeSize
		{
			get
			{
				return this._bladeSize;
			}
			set
			{
				if (value != this._bladeSize)
				{
					this._bladeSize = value;
					base.OnPropertyChangedWithValue(value, "BladeSize");
					if (this._crafting != null && this._updatePiece && this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(CraftingPiece.PieceTypes.Blade))
					{
						int percentage = 100 + value;
						this._crafting.ScaleThePiece(CraftingPiece.PieceTypes.Blade, percentage);
						this.RefreshItem();
					}
				}
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00054E74 File Offset: 0x00053074
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00054E7C File Offset: 0x0005307C
		[DataSourceProperty]
		public int GuardSize
		{
			get
			{
				return this._guardSize;
			}
			set
			{
				if (value != this._guardSize)
				{
					this._guardSize = value;
					base.OnPropertyChangedWithValue(value, "GuardSize");
					if (this._crafting != null && this._updatePiece && this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(CraftingPiece.PieceTypes.Guard))
					{
						int percentage = 100 + value;
						this._crafting.ScaleThePiece(CraftingPiece.PieceTypes.Guard, percentage);
						this.RefreshItem();
					}
				}
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00054EE0 File Offset: 0x000530E0
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x00054EE8 File Offset: 0x000530E8
		[DataSourceProperty]
		public int HandleSize
		{
			get
			{
				return this._handleSize;
			}
			set
			{
				if (value != this._handleSize)
				{
					this._handleSize = value;
					base.OnPropertyChangedWithValue(value, "HandleSize");
					if (this._crafting != null && this._updatePiece && this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(CraftingPiece.PieceTypes.Handle))
					{
						int percentage = 100 + value;
						this._crafting.ScaleThePiece(CraftingPiece.PieceTypes.Handle, percentage);
						this.RefreshItem();
					}
				}
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00054F4C File Offset: 0x0005314C
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00054F54 File Offset: 0x00053154
		[DataSourceProperty]
		public int PommelSize
		{
			get
			{
				return this._pommelSize;
			}
			set
			{
				if (value != this._pommelSize)
				{
					this._pommelSize = value;
					base.OnPropertyChangedWithValue(value, "PommelSize");
					if (this._crafting != null && this._updatePiece && this._crafting.CurrentCraftingTemplate.IsPieceTypeUsable(CraftingPiece.PieceTypes.Pommel))
					{
						int percentage = 100 + value;
						this._crafting.ScaleThePiece(CraftingPiece.PieceTypes.Pommel, percentage);
						this.RefreshItem();
					}
				}
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00054FB8 File Offset: 0x000531B8
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00054FC0 File Offset: 0x000531C0
		[DataSourceProperty]
		public string ComponentSizeLbl
		{
			get
			{
				return this._componentSizeLbl;
			}
			set
			{
				if (value != this._componentSizeLbl)
				{
					this._componentSizeLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ComponentSizeLbl");
				}
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00054FE3 File Offset: 0x000531E3
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x00054FEB File Offset: 0x000531EB
		[DataSourceProperty]
		public bool IsWeaponCivilian
		{
			get
			{
				return this._isWeaponCivilian;
			}
			set
			{
				if (value != this._isWeaponCivilian)
				{
					this._isWeaponCivilian = value;
					base.OnPropertyChangedWithValue(value, "IsWeaponCivilian");
				}
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00055009 File Offset: 0x00053209
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x00055011 File Offset: 0x00053211
		[DataSourceProperty]
		public HintViewModel ScabbardHint
		{
			get
			{
				return this._scabbardHint;
			}
			set
			{
				if (value != this._scabbardHint)
				{
					this._scabbardHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ScabbardHint");
				}
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0005502F File Offset: 0x0005322F
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x00055037 File Offset: 0x00053237
		[DataSourceProperty]
		public HintViewModel RandomizeHint
		{
			get
			{
				return this._randomizeHint;
			}
			set
			{
				if (value != this._randomizeHint)
				{
					this._randomizeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RandomizeHint");
				}
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00055055 File Offset: 0x00053255
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0005505D File Offset: 0x0005325D
		[DataSourceProperty]
		public HintViewModel UndoHint
		{
			get
			{
				return this._undoHint;
			}
			set
			{
				if (value != this._undoHint)
				{
					this._undoHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UndoHint");
				}
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005507B File Offset: 0x0005327B
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x00055083 File Offset: 0x00053283
		[DataSourceProperty]
		public HintViewModel RedoHint
		{
			get
			{
				return this._redoHint;
			}
			set
			{
				if (value != this._redoHint)
				{
					this._redoHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RedoHint");
				}
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000550A1 File Offset: 0x000532A1
		// (set) Token: 0x060016B4 RID: 5812 RVA: 0x000550A9 File Offset: 0x000532A9
		[DataSourceProperty]
		public MBBindingList<ItemFlagVM> WeaponFlagIconsList
		{
			get
			{
				return this._weaponFlagIconsList;
			}
			set
			{
				if (value != this._weaponFlagIconsList)
				{
					this._weaponFlagIconsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemFlagVM>>(value, "WeaponFlagIconsList");
				}
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000550C7 File Offset: 0x000532C7
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x000550CF File Offset: 0x000532CF
		[DataSourceProperty]
		public CraftingHistoryVM CraftingHistory
		{
			get
			{
				return this._craftingHistory;
			}
			set
			{
				if (value != this._craftingHistory)
				{
					this._craftingHistory = value;
					base.OnPropertyChangedWithValue<CraftingHistoryVM>(value, "CraftingHistory");
				}
			}
		}

		// Token: 0x04000A46 RID: 2630
		private WeaponDesignVM.CraftingPieceTierFilter _currentTierFilter = WeaponDesignVM.CraftingPieceTierFilter.All;

		// Token: 0x04000A47 RID: 2631
		public const int MAX_SKILL_LEVEL = 300;

		// Token: 0x04000A48 RID: 2632
		public ItemObject CraftedItemObject;

		// Token: 0x04000A49 RID: 2633
		private int _selectedWeaponClassIndex;

		// Token: 0x04000A4A RID: 2634
		private readonly List<CraftingPiece> _newlyUnlockedPieces;

		// Token: 0x04000A4B RID: 2635
		private readonly List<CraftingTemplate> _primaryUsages;

		// Token: 0x04000A4C RID: 2636
		private readonly WeaponDesignVM.PieceTierComparer _pieceTierComparer;

		// Token: 0x04000A4D RID: 2637
		private readonly WeaponDesignVM.TemplateComparer _templateComparer;

		// Token: 0x04000A4E RID: 2638
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x04000A4F RID: 2639
		private readonly Action _onRefresh;

		// Token: 0x04000A50 RID: 2640
		private readonly Action _onWeaponCrafted;

		// Token: 0x04000A51 RID: 2641
		private readonly Func<CraftingAvailableHeroItemVM> _getCurrentCraftingHero;

		// Token: 0x04000A52 RID: 2642
		private readonly Action<CraftingOrder> _refreshHeroAvailabilities;

		// Token: 0x04000A53 RID: 2643
		private Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> _getItemUsageSetFlags;

		// Token: 0x04000A54 RID: 2644
		private Crafting _crafting;

		// Token: 0x04000A55 RID: 2645
		private bool _updatePiece = true;

		// Token: 0x04000A56 RID: 2646
		private Dictionary<CraftingPiece.PieceTypes, CraftingPieceListVM> _pieceListsDictionary;

		// Token: 0x04000A57 RID: 2647
		private Dictionary<CraftingPiece, CraftingPieceVM> _pieceVMs;

		// Token: 0x04000A58 RID: 2648
		private TextObject _difficultyTextobj = new TextObject("{=cbbUzYX3}Difficulty: {DIFFICULTY}", null);

		// Token: 0x04000A59 RID: 2649
		private TextObject _orderDifficultyTextObj = new TextObject("{=8szijlHj}Order Difficulty: {DIFFICULTY}", null);

		// Token: 0x04000A5A RID: 2650
		private bool _isAutoSelectingPieces;

		// Token: 0x04000A5B RID: 2651
		private bool _shouldRecordHistory;

		// Token: 0x04000A5C RID: 2652
		private MBBindingList<TierFilterTypeVM> _tierFilters;

		// Token: 0x04000A5D RID: 2653
		private string _currentCraftedWeaponTemplateId;

		// Token: 0x04000A5E RID: 2654
		private string _chooseOrderText;

		// Token: 0x04000A5F RID: 2655
		private string _chooseWeaponTypeText;

		// Token: 0x04000A60 RID: 2656
		private string _currentCraftedWeaponTypeText;

		// Token: 0x04000A61 RID: 2657
		private MBBindingList<CraftingPieceListVM> _pieceLists;

		// Token: 0x04000A62 RID: 2658
		private int _selectedPieceTypeIndex;

		// Token: 0x04000A63 RID: 2659
		private bool _showOnlyUnlockedPieces;

		// Token: 0x04000A64 RID: 2660
		private string _missingPropertyWarningText;

		// Token: 0x04000A65 RID: 2661
		private bool _isInFinalCraftingStage;

		// Token: 0x04000A66 RID: 2662
		private string _componentSizeLbl;

		// Token: 0x04000A67 RID: 2663
		private string _itemName;

		// Token: 0x04000A68 RID: 2664
		private string _difficultyText;

		// Token: 0x04000A69 RID: 2665
		private int _bladeSize;

		// Token: 0x04000A6A RID: 2666
		private int _pommelSize;

		// Token: 0x04000A6B RID: 2667
		private int _handleSize;

		// Token: 0x04000A6C RID: 2668
		private int _guardSize;

		// Token: 0x04000A6D RID: 2669
		private CraftingPieceVM _selectedBladePiece;

		// Token: 0x04000A6E RID: 2670
		private CraftingPieceVM _selectedGuardPiece;

		// Token: 0x04000A6F RID: 2671
		private CraftingPieceVM _selectedHandlePiece;

		// Token: 0x04000A70 RID: 2672
		private CraftingPieceVM _selectedPommelPiece;

		// Token: 0x04000A71 RID: 2673
		private CraftingPieceListVM _bladePieceList;

		// Token: 0x04000A72 RID: 2674
		private CraftingPieceListVM _guardPieceList;

		// Token: 0x04000A73 RID: 2675
		private CraftingPieceListVM _handlePieceList;

		// Token: 0x04000A74 RID: 2676
		private CraftingPieceListVM _pommelPieceList;

		// Token: 0x04000A75 RID: 2677
		private string _alternativeUsageText;

		// Token: 0x04000A76 RID: 2678
		private string _defaultUsageText;

		// Token: 0x04000A77 RID: 2679
		private bool _isScabbardVisible;

		// Token: 0x04000A78 RID: 2680
		private bool _currentWeaponHasScabbard;

		// Token: 0x04000A79 RID: 2681
		public SelectorVM<CraftingSecondaryUsageItemVM> _secondaryUsageSelector;

		// Token: 0x04000A7A RID: 2682
		private ItemCollectionElementViewModel _craftedItemVisual;

		// Token: 0x04000A7B RID: 2683
		private MBBindingList<CraftingListPropertyItem> _primaryPropertyList;

		// Token: 0x04000A7C RID: 2684
		private MBBindingList<WeaponDesignResultPropertyItemVM> _designResultPropertyList;

		// Token: 0x04000A7D RID: 2685
		private int _currentDifficulty;

		// Token: 0x04000A7E RID: 2686
		private int _currentOrderDifficulty;

		// Token: 0x04000A7F RID: 2687
		private int _maxDifficulty;

		// Token: 0x04000A80 RID: 2688
		private string _currentDifficultyText;

		// Token: 0x04000A81 RID: 2689
		private string _currentOrderDifficultyText;

		// Token: 0x04000A82 RID: 2690
		private string _currentCraftingSkillValueText;

		// Token: 0x04000A83 RID: 2691
		private bool _isCurrentHeroAtMaxCraftingSkill;

		// Token: 0x04000A84 RID: 2692
		private int _currentHeroCraftingSkill;

		// Token: 0x04000A85 RID: 2693
		private bool _isWeaponCivilian;

		// Token: 0x04000A86 RID: 2694
		private HintViewModel _scabbardHint;

		// Token: 0x04000A87 RID: 2695
		private HintViewModel _randomizeHint;

		// Token: 0x04000A88 RID: 2696
		private HintViewModel _undoHint;

		// Token: 0x04000A89 RID: 2697
		private HintViewModel _redoHint;

		// Token: 0x04000A8A RID: 2698
		private HintViewModel _showOnlyUnlockedPiecesHint;

		// Token: 0x04000A8B RID: 2699
		private BasicTooltipViewModel _orderDisabledReasonHint;

		// Token: 0x04000A8C RID: 2700
		private CraftingOrderItemVM _activeCraftingOrder;

		// Token: 0x04000A8D RID: 2701
		private CraftingOrderPopupVM _craftingOrderPopup;

		// Token: 0x04000A8E RID: 2702
		private WeaponClassSelectionPopupVM _weaponClassSelectionPopup;

		// Token: 0x04000A8F RID: 2703
		private string _freeModeButtonText;

		// Token: 0x04000A90 RID: 2704
		private bool _isOrderButtonActive;

		// Token: 0x04000A91 RID: 2705
		private bool _isInOrderMode;

		// Token: 0x04000A92 RID: 2706
		private bool _weaponControlsEnabled;

		// Token: 0x04000A93 RID: 2707
		private WeaponDesignResultPopupVM _craftingResultPopup;

		// Token: 0x04000A94 RID: 2708
		private MBBindingList<ItemFlagVM> _weaponFlagIconsList;

		// Token: 0x04000A95 RID: 2709
		private CraftingHistoryVM _craftingHistory;

		// Token: 0x04000A96 RID: 2710
		private TextObject _currentCraftingSkillText;

		// Token: 0x0200021A RID: 538
		[Flags]
		public enum CraftingPieceTierFilter
		{
			// Token: 0x040010FE RID: 4350
			None = 0,
			// Token: 0x040010FF RID: 4351
			Tier1 = 1,
			// Token: 0x04001100 RID: 4352
			Tier2 = 2,
			// Token: 0x04001101 RID: 4353
			Tier3 = 4,
			// Token: 0x04001102 RID: 4354
			Tier4 = 8,
			// Token: 0x04001103 RID: 4355
			Tier5 = 16,
			// Token: 0x04001104 RID: 4356
			All = 31
		}

		// Token: 0x0200021B RID: 539
		public class PieceTierComparer : IComparer<CraftingPieceVM>
		{
			// Token: 0x06002249 RID: 8777 RVA: 0x00074DA4 File Offset: 0x00072FA4
			public int Compare(CraftingPieceVM x, CraftingPieceVM y)
			{
				if (x.Tier != y.Tier)
				{
					return x.Tier.CompareTo(y.Tier);
				}
				return x.CraftingPiece.CraftingPiece.StringId.CompareTo(y.CraftingPiece.CraftingPiece.StringId);
			}
		}

		// Token: 0x0200021C RID: 540
		public class TemplateComparer : IComparer<CraftingTemplate>
		{
			// Token: 0x0600224B RID: 8779 RVA: 0x00074E01 File Offset: 0x00073001
			public int Compare(CraftingTemplate x, CraftingTemplate y)
			{
				return string.Compare(x.StringId, y.StringId, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0200021D RID: 541
		public class WeaponPropertyComparer : IComparer<CraftingListPropertyItem>
		{
			// Token: 0x0600224D RID: 8781 RVA: 0x00074E20 File Offset: 0x00073020
			public int Compare(CraftingListPropertyItem x, CraftingListPropertyItem y)
			{
				return ((int)x.Type).CompareTo((int)y.Type);
			}
		}
	}
}
