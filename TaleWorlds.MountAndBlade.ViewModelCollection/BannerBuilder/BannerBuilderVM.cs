using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.BannerEditor;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000079 RID: 121
	public class BannerBuilderVM : ViewModel
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002650D File Offset: 0x0002470D
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x00026515 File Offset: 0x00024715
		public Banner CurrentBanner { get; private set; }

		// Token: 0x060009F2 RID: 2546 RVA: 0x00026520 File Offset: 0x00024720
		public BannerBuilderVM(BasicCharacterObject character, string initialKey, Action<bool> onExit, Action refresh, Action copyBannerCode)
		{
			this._character = character;
			this._onExit = onExit;
			this._refresh = refresh;
			this._copyBannerCode = copyBannerCode;
			this.Categories = new MBBindingList<BannerBuilderCategoryVM>();
			this.Layers = new MBBindingList<BannerBuilderLayerVM>();
			this.ColorSelection = new BannerBuilderColorSelectionVM();
			BannerBuilderLayerVM.SetLayerActions(new Action(this.OnRefreshFromLayer), new Action<BannerBuilderLayerVM>(this.OnLayerSelection), new Action<BannerBuilderLayerVM>(this.OnLayerDeletion), new Action<int, Action<BannerBuilderColorItemVM>>(this.OnColorSelection));
			ItemObject itemObject = BannerBuilderVM.FindShield(this._character, "highland_riders_shield");
			if (itemObject != null)
			{
				this.ShieldRosterElement = new ItemRosterElement(itemObject, 1, null);
			}
			this.CurrentBanner = new Banner(initialKey);
			this.PopulateCategories();
			this.PopulateLayers();
			this.OnLayerSelection(this.Layers[0]);
			this.BannerCodeAsString = initialKey;
			this.BannerImageIdentifier = new ImageIdentifierVM(BannerCode.CreateFrom(initialKey), true);
			this.RefreshValues();
			this.IsEditorPreviewActive = true;
			this.IsLayerPreviewActive = true;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00026628 File Offset: 0x00024828
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Title = new TextObject("{=!}Banner Builder", null).ToString();
			this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset_icon", null), null);
			this.RandomizeHint = new HintViewModel(GameTexts.FindText("str_randomize", null), null);
			this.UndoHint = new HintViewModel(GameTexts.FindText("str_undo", null), null);
			this.RedoHint = new HintViewModel(GameTexts.FindText("str_redo", null), null);
			this.DrawStrokeHint = new HintViewModel(new TextObject("{=!}Draw Stroke", null), null);
			this.CenterHint = new HintViewModel(new TextObject("{=!}Align Center", null), null);
			this.ResetSizeHint = new HintViewModel(new TextObject("{=!}Reset Size", null), null);
			this.MirrorHint = new HintViewModel(new TextObject("{=!}Mirror", null), null);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00026738 File Offset: 0x00024938
		private void PopulateCategories()
		{
			this.Categories.Clear();
			for (int i = 0; i < BannerManager.Instance.BannerIconGroups.Count; i++)
			{
				BannerIconGroup category = BannerManager.Instance.BannerIconGroups[i];
				this.Categories.Add(new BannerBuilderCategoryVM(category, new Action<BannerBuilderItemVM>(this.OnItemSelection)));
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00026798 File Offset: 0x00024998
		private void PopulateLayers()
		{
			this.Layers.Clear();
			for (int i = 0; i < this.CurrentBanner.BannerDataList.Count; i++)
			{
				this.Layers.Add(new BannerBuilderLayerVM(this.CurrentBanner.BannerDataList[i], i));
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000267ED File Offset: 0x000249ED
		private void OnColorSelection(int selectedColorId, Action<BannerBuilderColorItemVM> onSelection)
		{
			this.ColorSelection.EnableWith(selectedColorId, onSelection);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000267FC File Offset: 0x000249FC
		private void OnLayerSelection(BannerBuilderLayerVM selectedLayer)
		{
			if (this.CurrentSelectedLayer != null)
			{
				this.CurrentSelectedLayer.IsSelected = false;
			}
			if (this.CurrentSelectedItem != null)
			{
				this.CurrentSelectedItem.IsSelected = false;
			}
			this.CurrentSelectedLayer = selectedLayer;
			if (this.CurrentSelectedLayer != null)
			{
				BannerBuilderItemVM itemFromID = this.GetItemFromID(this.CurrentSelectedLayer.IconID);
				if (itemFromID != null)
				{
					this.CurrentSelectedItem = itemFromID;
					this.CurrentSelectedItem.IsSelected = true;
				}
				this.CurrentSelectedLayer.IsSelected = true;
				bool isPatternLayerSelected = this.CurrentSelectedLayer.LayerIndex == 0;
				this.Categories.ApplyActionOnAllItems(delegate(BannerBuilderCategoryVM c)
				{
					c.IsEnabled = (c.IsPattern ? isPatternLayerSelected : (!isPatternLayerSelected));
				});
				this.UpdateSelectedItem();
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000268AC File Offset: 0x00024AAC
		private void OnLayerDeletion(BannerBuilderLayerVM layerToDelete)
		{
			if (layerToDelete == null || layerToDelete.LayerIndex != 0)
			{
				this.CurrentBanner.RemoveIconDataAtIndex(layerToDelete.LayerIndex);
				if (this.CurrentSelectedLayer == layerToDelete)
				{
					this.OnLayerSelection(this.Layers[layerToDelete.LayerIndex - 1]);
				}
				this.Layers.RemoveAt(layerToDelete.LayerIndex);
				this.RefreshLayerIndicies();
				this.Refresh();
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002691C File Offset: 0x00024B1C
		private void OnItemSelection(BannerBuilderItemVM selectedItem)
		{
			if (this.CurrentSelectedLayer != null)
			{
				this.CurrentBanner.BannerDataList[this.CurrentSelectedLayer.LayerIndex].MeshId = selectedItem.MeshID;
				this.UpdateSelectedItem();
				this.CurrentSelectedLayer.Refresh();
				this.Refresh();
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00026970 File Offset: 0x00024B70
		private void UpdateSelectedItem()
		{
			if (this.CurrentSelectedLayer != null)
			{
				int meshId = this.CurrentBanner.BannerDataList[this.CurrentSelectedLayer.LayerIndex].MeshId;
				for (int i = 0; i < this.Categories.Count; i++)
				{
					BannerBuilderCategoryVM bannerBuilderCategoryVM = this.Categories[i];
					for (int j = 0; j < bannerBuilderCategoryVM.ItemsList.Count; j++)
					{
						BannerBuilderItemVM bannerBuilderItemVM = bannerBuilderCategoryVM.ItemsList[j];
						bannerBuilderItemVM.IsSelected = (bannerBuilderItemVM.MeshID == meshId);
					}
				}
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000269F9 File Offset: 0x00024BF9
		public void ExecuteCancel()
		{
			Action<bool> onExit = this._onExit;
			if (onExit == null)
			{
				return;
			}
			onExit(true);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00026A0C File Offset: 0x00024C0C
		public void ExecuteDone()
		{
			Action<bool> onExit = this._onExit;
			if (onExit == null)
			{
				return;
			}
			onExit(false);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00026A20 File Offset: 0x00024C20
		public void ExecuteAddDefaultLayer()
		{
			BannerData defaultBannerData = BannerBuilderVM.GetDefaultBannerData();
			this.CurrentBanner.AddIconData(defaultBannerData);
			BannerBuilderLayerVM bannerBuilderLayerVM = new BannerBuilderLayerVM(defaultBannerData, this.Layers.Count);
			this.Layers.Add(bannerBuilderLayerVM);
			this.OnLayerSelection(bannerBuilderLayerVM);
			this.Refresh();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00026A6C File Offset: 0x00024C6C
		public void ExecuteDuplicateCurrentLayer()
		{
			BannerBuilderLayerVM currentSelectedLayer = this.CurrentSelectedLayer;
			if (currentSelectedLayer != null && !currentSelectedLayer.IsLayerPattern)
			{
				BannerData bannerData = new BannerData(this.CurrentSelectedLayer.Data);
				this.CurrentBanner.AddIconData(bannerData);
				BannerBuilderLayerVM bannerBuilderLayerVM = new BannerBuilderLayerVM(bannerData, this.Layers.Count);
				this.Layers.Add(bannerBuilderLayerVM);
				this.OnLayerSelection(bannerBuilderLayerVM);
				this.Refresh();
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00026AD8 File Offset: 0x00024CD8
		public void ExecuteCopyBannerCode()
		{
			Action copyBannerCode = this._copyBannerCode;
			if (copyBannerCode == null)
			{
				return;
			}
			copyBannerCode();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00026AEC File Offset: 0x00024CEC
		public void ExecuteReorderWithParameters(BannerBuilderLayerVM layer, int index, string targetTag)
		{
			if (layer.IsLayerPattern || index == 0)
			{
				return;
			}
			int index2 = (layer.LayerIndex >= index) ? index : (index - 1);
			this.Layers.Remove(layer);
			this.Layers.Insert(index2, layer);
			this.CurrentBanner.RemoveIconDataAtIndex(layer.LayerIndex);
			this.CurrentBanner.AddIconData(layer.Data, index2);
			this.RefreshLayerIndicies();
			this.OnRefreshFromLayer();
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00026B60 File Offset: 0x00024D60
		public void ExecuteReorderToEndWithParameters(BannerBuilderLayerVM layer, int index, string targetTag)
		{
			if (layer.IsLayerPattern)
			{
				return;
			}
			this.ExecuteReorderWithParameters(layer, this.Layers.Count, string.Empty);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00026B82 File Offset: 0x00024D82
		private void OnRefreshFromLayer()
		{
			this.Refresh();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00026B8A File Offset: 0x00024D8A
		public string GetBannerCode()
		{
			return this.BannerCodeAsString;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00026B94 File Offset: 0x00024D94
		public void SetBannerCode(string v)
		{
			string bannerCodeAsString = this.BannerCodeAsString;
			try
			{
				this.CurrentBanner.Deserialize(v);
				this.PopulateLayers();
				this.OnLayerSelection(this.Layers[0]);
				this.Refresh();
			}
			catch (Exception)
			{
				InformationManager.DisplayMessage(new InformationMessage("Couldn't parse the clipboard text."));
				this.CurrentBanner.Deserialize(bannerCodeAsString);
				this.PopulateLayers();
				this.OnLayerSelection(this.Layers[0]);
				this.Refresh();
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00026C20 File Offset: 0x00024E20
		private void Refresh()
		{
			Action refresh = this._refresh;
			if (refresh != null)
			{
				refresh();
			}
			this.BannerImageIdentifier = new ImageIdentifierVM(BannerCode.CreateFrom(this.BannerCodeAsString), true);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00026C4C File Offset: 0x00024E4C
		private BannerBuilderItemVM GetItemFromID(int id)
		{
			for (int i = 0; i < this.Categories.Count; i++)
			{
				BannerBuilderCategoryVM bannerBuilderCategoryVM = this.Categories[i];
				for (int j = 0; j < bannerBuilderCategoryVM.ItemsList.Count; j++)
				{
					BannerBuilderItemVM bannerBuilderItemVM = bannerBuilderCategoryVM.ItemsList[j];
					if (bannerBuilderItemVM.MeshID == id)
					{
						return bannerBuilderItemVM;
					}
				}
			}
			return null;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00026CAC File Offset: 0x00024EAC
		private void RefreshLayerIndicies()
		{
			for (int i = 0; i < this.Layers.Count; i++)
			{
				this.Layers[i].SetLayerIndex(i);
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00026CE4 File Offset: 0x00024EE4
		public void TranslateCurrentLayerWith(Vec2 moveDirection)
		{
			this.CurrentSelectedLayer.PositionValueX += moveDirection.x;
			this.CurrentSelectedLayer.PositionValueY += moveDirection.y;
			this.CurrentSelectedLayer.PositionValueX = MathF.Clamp(this.CurrentSelectedLayer.PositionValueX, 0f, 1528f);
			this.CurrentSelectedLayer.PositionValueY = MathF.Clamp(this.CurrentSelectedLayer.PositionValueY, 0f, 1528f);
			this.Refresh();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00026D71 File Offset: 0x00024F71
		public void DeleteCurrentLayer()
		{
			BannerBuilderLayerVM currentSelectedLayer = this.CurrentSelectedLayer;
			if (currentSelectedLayer != null && !currentSelectedLayer.IsLayerPattern)
			{
				this.OnLayerDeletion(this.Layers[this.CurrentSelectedLayer.LayerIndex]);
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00026DA6 File Offset: 0x00024FA6
		public override void OnFinalize()
		{
			base.OnFinalize();
			BannerBuilderLayerVM.ResetLayerActions();
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00026DB3 File Offset: 0x00024FB3
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00026DBB File Offset: 0x00024FBB
		[DataSourceProperty]
		public ImageIdentifierVM BannerImageIdentifier
		{
			get
			{
				return this._bannerImageIdentifier;
			}
			set
			{
				if (value != this._bannerImageIdentifier)
				{
					this._bannerImageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "BannerImageIdentifier");
				}
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00026DD9 File Offset: 0x00024FD9
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00026DE1 File Offset: 0x00024FE1
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00026E04 File Offset: 0x00025004
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00026E0C File Offset: 0x0002500C
		[DataSourceProperty]
		public MBBindingList<BannerBuilderCategoryVM> Categories
		{
			get
			{
				return this._categories;
			}
			set
			{
				if (value != this._categories)
				{
					this._categories = value;
					base.OnPropertyChangedWithValue<MBBindingList<BannerBuilderCategoryVM>>(value, "Categories");
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00026E2A File Offset: 0x0002502A
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00026E32 File Offset: 0x00025032
		[DataSourceProperty]
		public BannerBuilderColorSelectionVM ColorSelection
		{
			get
			{
				return this._colorSelection;
			}
			set
			{
				if (value != this._colorSelection)
				{
					this._colorSelection = value;
					base.OnPropertyChangedWithValue<BannerBuilderColorSelectionVM>(value, "ColorSelection");
				}
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00026E50 File Offset: 0x00025050
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00026E58 File Offset: 0x00025058
		[DataSourceProperty]
		public MBBindingList<BannerBuilderLayerVM> Layers
		{
			get
			{
				return this._layers;
			}
			set
			{
				if (value != this._layers)
				{
					this._layers = value;
					base.OnPropertyChangedWithValue<MBBindingList<BannerBuilderLayerVM>>(value, "Layers");
				}
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00026E76 File Offset: 0x00025076
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00026E7E File Offset: 0x0002507E
		[DataSourceProperty]
		public BannerBuilderLayerVM CurrentSelectedLayer
		{
			get
			{
				return this._currentSelectedLayer;
			}
			set
			{
				if (value != this._currentSelectedLayer)
				{
					this._currentSelectedLayer = value;
					base.OnPropertyChangedWithValue<BannerBuilderLayerVM>(value, "CurrentSelectedLayer");
				}
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00026E9C File Offset: 0x0002509C
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x00026EA4 File Offset: 0x000250A4
		[DataSourceProperty]
		public BannerBuilderItemVM CurrentSelectedItem
		{
			get
			{
				return this._currentSelectedItem;
			}
			set
			{
				if (value != this._currentSelectedItem)
				{
					this._currentSelectedItem = value;
					base.OnPropertyChangedWithValue<BannerBuilderItemVM>(value, "CurrentSelectedItem");
				}
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00026EC2 File Offset: 0x000250C2
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x00026ECA File Offset: 0x000250CA
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

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00026EE8 File Offset: 0x000250E8
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00026EF0 File Offset: 0x000250F0
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

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00026F0E File Offset: 0x0002510E
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x00026F16 File Offset: 0x00025116
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00026F34 File Offset: 0x00025134
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x00026F3C File Offset: 0x0002513C
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00026F5A File Offset: 0x0002515A
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00026F62 File Offset: 0x00025162
		[DataSourceProperty]
		public HintViewModel DrawStrokeHint
		{
			get
			{
				return this._drawStrokeHint;
			}
			set
			{
				if (value != this._drawStrokeHint)
				{
					this._drawStrokeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DrawStrokeHint");
				}
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00026F80 File Offset: 0x00025180
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00026F88 File Offset: 0x00025188
		[DataSourceProperty]
		public HintViewModel CenterHint
		{
			get
			{
				return this._centerHint;
			}
			set
			{
				if (value != this._centerHint)
				{
					this._centerHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CenterHint");
				}
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00026FA6 File Offset: 0x000251A6
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x00026FAE File Offset: 0x000251AE
		[DataSourceProperty]
		public HintViewModel ResetSizeHint
		{
			get
			{
				return this._resetSizeHint;
			}
			set
			{
				if (value != this._resetSizeHint)
				{
					this._resetSizeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetSizeHint");
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00026FCC File Offset: 0x000251CC
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00026FD4 File Offset: 0x000251D4
		[DataSourceProperty]
		public HintViewModel MirrorHint
		{
			get
			{
				return this._mirrorHint;
			}
			set
			{
				if (value != this._mirrorHint)
				{
					this._mirrorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "MirrorHint");
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00026FF2 File Offset: 0x000251F2
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00026FFA File Offset: 0x000251FA
		[DataSourceProperty]
		public string CurrentShieldName
		{
			get
			{
				return this._currentShieldName;
			}
			set
			{
				if (value != this._currentShieldName)
				{
					this._currentShieldName = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentShieldName");
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002701D File Offset: 0x0002521D
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00027025 File Offset: 0x00025225
		[DataSourceProperty]
		public int MinIconSize
		{
			get
			{
				return this._minIconSize;
			}
			set
			{
				if (value != this._minIconSize)
				{
					this._minIconSize = value;
					base.OnPropertyChangedWithValue(value, "MinIconSize");
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00027043 File Offset: 0x00025243
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x0002704B File Offset: 0x0002524B
		[DataSourceProperty]
		public int MaxIconSize
		{
			get
			{
				return this._maxIconSize;
			}
			set
			{
				if (value != this._maxIconSize)
				{
					this._maxIconSize = value;
					base.OnPropertyChangedWithValue(value, "MaxIconSize");
				}
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00027069 File Offset: 0x00025269
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x00027071 File Offset: 0x00025271
		[DataSourceProperty]
		public string BannerCodeAsString
		{
			get
			{
				return this._bannerCodeAsString;
			}
			set
			{
				if (value != this._bannerCodeAsString)
				{
					this._bannerCodeAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerCodeAsString");
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00027094 File Offset: 0x00025294
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0002709C File Offset: 0x0002529C
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x000270BF File Offset: 0x000252BF
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x000270C7 File Offset: 0x000252C7
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

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x000270EA File Offset: 0x000252EA
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x000270F2 File Offset: 0x000252F2
		[DataSourceProperty]
		public BannerViewModel BannerVM
		{
			get
			{
				return this._bannerVM;
			}
			set
			{
				if (value != this._bannerVM)
				{
					this._bannerVM = value;
					base.OnPropertyChangedWithValue<BannerViewModel>(value, "BannerVM");
				}
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00027110 File Offset: 0x00025310
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00027118 File Offset: 0x00025318
		[DataSourceProperty]
		public string IconCodes
		{
			get
			{
				return this._iconCodes;
			}
			set
			{
				if (value != this._iconCodes)
				{
					this._iconCodes = value;
					base.OnPropertyChangedWithValue<string>(value, "IconCodes");
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002713B File Offset: 0x0002533B
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00027143 File Offset: 0x00025343
		[DataSourceProperty]
		public string ColorCodes
		{
			get
			{
				return this._colorCodes;
			}
			set
			{
				if (value != this._colorCodes)
				{
					this._colorCodes = value;
					base.OnPropertyChangedWithValue<string>(value, "ColorCodes");
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00027166 File Offset: 0x00025366
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0002716E File Offset: 0x0002536E
		[DataSourceProperty]
		public bool CanChangeBackgroundColor
		{
			get
			{
				return this._canChangeBackgroundColor;
			}
			set
			{
				if (value != this._canChangeBackgroundColor)
				{
					this._canChangeBackgroundColor = value;
					base.OnPropertyChangedWithValue(value, "CanChangeBackgroundColor");
				}
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002718C File Offset: 0x0002538C
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00027194 File Offset: 0x00025394
		[DataSourceProperty]
		public bool IsBannerPreviewsActive
		{
			get
			{
				return this._isBannerPreviewsActive;
			}
			set
			{
				if (value != this._isBannerPreviewsActive)
				{
					this._isBannerPreviewsActive = value;
					base.OnPropertyChangedWithValue(value, "IsBannerPreviewsActive");
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000271B2 File Offset: 0x000253B2
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x000271BA File Offset: 0x000253BA
		[DataSourceProperty]
		public bool IsEditorPreviewActive
		{
			get
			{
				return this._isEditorPreviewActive;
			}
			set
			{
				if (value != this._isEditorPreviewActive)
				{
					this._isEditorPreviewActive = value;
					base.OnPropertyChangedWithValue(value, "IsEditorPreviewActive");
				}
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000271D8 File Offset: 0x000253D8
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x000271E0 File Offset: 0x000253E0
		[DataSourceProperty]
		public bool IsLayerPreviewActive
		{
			get
			{
				return this._isLayerPreviewActive;
			}
			set
			{
				if (value != this._isLayerPreviewActive)
				{
					this._isLayerPreviewActive = value;
					base.OnPropertyChangedWithValue(value, "IsLayerPreviewActive");
				}
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000271FE File Offset: 0x000253FE
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002720D File Offset: 0x0002540D
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002721C File Offset: 0x0002541C
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00027224 File Offset: 0x00025424
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

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00027242 File Offset: 0x00025442
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0002724A File Offset: 0x0002544A
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

		// Token: 0x06000A49 RID: 2633 RVA: 0x00027268 File Offset: 0x00025468
		private static ItemObject FindShield(BasicCharacterObject character, string desiredShieldID = "")
		{
			for (int i = 0; i < 4; i++)
			{
				EquipmentElement equipmentFromSlot = character.Equipment.GetEquipmentFromSlot((EquipmentIndex)i);
				ItemObject item = equipmentFromSlot.Item;
				if (((item != null) ? item.PrimaryWeapon : null) != null && equipmentFromSlot.Item.PrimaryWeapon.IsShield && equipmentFromSlot.Item.IsUsingTableau)
				{
					return equipmentFromSlot.Item;
				}
			}
			if (!string.IsNullOrEmpty(desiredShieldID))
			{
				ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(desiredShieldID);
				if (@object != null)
				{
					WeaponComponentData primaryWeapon = @object.PrimaryWeapon;
					if (primaryWeapon != null && primaryWeapon.IsShield)
					{
						return @object;
					}
				}
			}
			MBReadOnlyList<ItemObject> objectTypeList = Game.Current.ObjectManager.GetObjectTypeList<ItemObject>();
			for (int j = 0; j < objectTypeList.Count; j++)
			{
				ItemObject itemObject = objectTypeList[j];
				WeaponComponentData primaryWeapon2 = itemObject.PrimaryWeapon;
				if (primaryWeapon2 != null && primaryWeapon2.IsShield && itemObject.IsUsingTableau)
				{
					return itemObject;
				}
			}
			return null;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002734F File Offset: 0x0002554F
		private static BannerData GetDefaultBannerData()
		{
			return new BannerData(133, 171, 171, new Vec2(483f, 483f), new Vec2(764f, 764f), false, false, 0f);
		}

		// Token: 0x040004B7 RID: 1207
		private const int PatternLayerIndex = 0;

		// Token: 0x040004B8 RID: 1208
		public int ShieldSlotIndex = 3;

		// Token: 0x040004B9 RID: 1209
		public int CurrentShieldIndex;

		// Token: 0x040004BA RID: 1210
		public ItemRosterElement ShieldRosterElement;

		// Token: 0x040004BB RID: 1211
		private readonly BasicCharacterObject _character;

		// Token: 0x040004BC RID: 1212
		private readonly Action<bool> _onExit;

		// Token: 0x040004BD RID: 1213
		private readonly Action _refresh;

		// Token: 0x040004BE RID: 1214
		private readonly Action _copyBannerCode;

		// Token: 0x040004BF RID: 1215
		private ImageIdentifierVM _bannerImageIdentifier;

		// Token: 0x040004C0 RID: 1216
		private string _iconCodes;

		// Token: 0x040004C1 RID: 1217
		private string _colorCodes;

		// Token: 0x040004C2 RID: 1218
		private string _bannerCodeAsString;

		// Token: 0x040004C3 RID: 1219
		private BannerViewModel _bannerVM;

		// Token: 0x040004C4 RID: 1220
		private MBBindingList<BannerBuilderCategoryVM> _categories;

		// Token: 0x040004C5 RID: 1221
		private MBBindingList<BannerBuilderLayerVM> _layers;

		// Token: 0x040004C6 RID: 1222
		private BannerBuilderLayerVM _currentSelectedLayer;

		// Token: 0x040004C7 RID: 1223
		private BannerBuilderItemVM _currentSelectedItem;

		// Token: 0x040004C8 RID: 1224
		private BannerBuilderColorSelectionVM _colorSelection;

		// Token: 0x040004C9 RID: 1225
		private string _title;

		// Token: 0x040004CA RID: 1226
		private string _cancelText;

		// Token: 0x040004CB RID: 1227
		private string _doneText;

		// Token: 0x040004CC RID: 1228
		private string _currentShieldName;

		// Token: 0x040004CD RID: 1229
		private bool _canChangeBackgroundColor;

		// Token: 0x040004CE RID: 1230
		private bool _isBannerPreviewsActive;

		// Token: 0x040004CF RID: 1231
		private bool _isEditorPreviewActive;

		// Token: 0x040004D0 RID: 1232
		private bool _isLayerPreviewActive;

		// Token: 0x040004D1 RID: 1233
		private int _minIconSize;

		// Token: 0x040004D2 RID: 1234
		private int _maxIconSize;

		// Token: 0x040004D3 RID: 1235
		private HintViewModel _resetHint;

		// Token: 0x040004D4 RID: 1236
		private HintViewModel _randomizeHint;

		// Token: 0x040004D5 RID: 1237
		private HintViewModel _undoHint;

		// Token: 0x040004D6 RID: 1238
		private HintViewModel _redoHint;

		// Token: 0x040004D7 RID: 1239
		private HintViewModel _drawStrokeHint;

		// Token: 0x040004D8 RID: 1240
		private HintViewModel _centerHint;

		// Token: 0x040004D9 RID: 1241
		private HintViewModel _resetSizeHint;

		// Token: 0x040004DA RID: 1242
		private HintViewModel _mirrorHint;

		// Token: 0x040004DB RID: 1243
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040004DC RID: 1244
		private InputKeyItemVM _doneInputKey;
	}
}
