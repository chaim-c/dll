using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000045 RID: 69
	public class CheerBarkNodeItemVM : ViewModel
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060005C6 RID: 1478 RVA: 0x00018254 File Offset: 0x00016454
		// (remove) Token: 0x060005C7 RID: 1479 RVA: 0x00018288 File Offset: 0x00016488
		internal static event Action<CheerBarkNodeItemVM> OnSelection;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060005C8 RID: 1480 RVA: 0x000182BC File Offset: 0x000164BC
		// (remove) Token: 0x060005C9 RID: 1481 RVA: 0x000182F0 File Offset: 0x000164F0
		internal static event Action<CheerBarkNodeItemVM> OnNodeFocused;

		// Token: 0x060005CA RID: 1482 RVA: 0x00018324 File Offset: 0x00016524
		public CheerBarkNodeItemVM(string tauntVisualName, TextObject nodeName, string nodeId, HotKey key, bool consoleOnlyShortcut = false, TauntUsageManager.TauntUsage.TauntUsageFlag disabledReason = TauntUsageManager.TauntUsage.TauntUsageFlag.None)
		{
			this._nodeName = nodeName;
			this.TauntVisualName = tauntVisualName;
			this.TypeAsString = nodeId;
			this.TauntUsageDisabledReason = disabledReason;
			this.IsDisabled = (disabledReason != TauntUsageManager.TauntUsage.TauntUsageFlag.None && disabledReason != TauntUsageManager.TauntUsage.TauntUsageFlag.IsLeftStance);
			this.SubNodes = new MBBindingList<CheerBarkNodeItemVM>();
			if (key != null)
			{
				this.ShortcutKey = InputKeyItemVM.CreateFromHotKey(key, consoleOnlyShortcut);
			}
			this.RefreshValues();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00018390 File Offset: 0x00016590
		public CheerBarkNodeItemVM(TextObject nodeName, string nodeId, HotKey key, bool consoleOnlyShortcut = false, TauntUsageManager.TauntUsage.TauntUsageFlag disabledReason = TauntUsageManager.TauntUsage.TauntUsageFlag.None)
		{
			this._nodeName = nodeName;
			this.TauntVisualName = string.Empty;
			this.TypeAsString = nodeId;
			this.TauntUsageDisabledReason = disabledReason;
			this.IsDisabled = (disabledReason > TauntUsageManager.TauntUsage.TauntUsageFlag.None);
			this.SubNodes = new MBBindingList<CheerBarkNodeItemVM>();
			if (key != null)
			{
				this.ShortcutKey = InputKeyItemVM.CreateFromHotKey(key, consoleOnlyShortcut);
			}
			this.RefreshValues();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000183F4 File Offset: 0x000165F4
		public void ClearSelectionRecursive()
		{
			this.IsSelected = false;
			for (int i = 0; i < this.SubNodes.Count; i++)
			{
				this.SubNodes[i].ClearSelectionRecursive();
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001842F File Offset: 0x0001662F
		public void ExecuteFocused()
		{
			Action<CheerBarkNodeItemVM> onNodeFocused = CheerBarkNodeItemVM.OnNodeFocused;
			if (onNodeFocused == null)
			{
				return;
			}
			onNodeFocused(this);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00018441 File Offset: 0x00016641
		public override void RefreshValues()
		{
			TextObject nodeName = this._nodeName;
			this.CheerNameText = ((nodeName != null) ? nodeName.ToString() : null);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001845B File Offset: 0x0001665B
		public void AddSubNode(CheerBarkNodeItemVM subNode)
		{
			this.SubNodes.Add(subNode);
			this.HasSubNodes = true;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018470 File Offset: 0x00016670
		public override void OnFinalize()
		{
			base.OnFinalize();
			MBBindingList<CheerBarkNodeItemVM> subNodes = this.SubNodes;
			if (subNodes != null)
			{
				subNodes.ApplyActionOnAllItems(delegate(CheerBarkNodeItemVM n)
				{
					n.OnFinalize();
				});
			}
			InputKeyItemVM shortcutKey = this.ShortcutKey;
			if (shortcutKey != null)
			{
				shortcutKey.OnFinalize();
			}
			this.ShortcutKey = null;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000184CB File Offset: 0x000166CB
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x000184D3 File Offset: 0x000166D3
		[DataSourceProperty]
		public InputKeyItemVM ShortcutKey
		{
			get
			{
				return this._shortcutKey;
			}
			set
			{
				if (value != this._shortcutKey)
				{
					this._shortcutKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ShortcutKey");
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000184F1 File Offset: 0x000166F1
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000184F9 File Offset: 0x000166F9
		[DataSourceProperty]
		public MBBindingList<CheerBarkNodeItemVM> SubNodes
		{
			get
			{
				return this._subNodes;
			}
			set
			{
				if (value != this._subNodes)
				{
					this._subNodes = value;
					base.OnPropertyChangedWithValue<MBBindingList<CheerBarkNodeItemVM>>(value, "SubNodes");
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00018517 File Offset: 0x00016717
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001851F File Offset: 0x0001671F
		[DataSourceProperty]
		public string CheerNameText
		{
			get
			{
				return this._cheerNameText;
			}
			set
			{
				if (value != this._cheerNameText)
				{
					this._cheerNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "CheerNameText");
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00018542 File Offset: 0x00016742
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001854A File Offset: 0x0001674A
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00018568 File Offset: 0x00016768
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00018570 File Offset: 0x00016770
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
					if (this._isSelected)
					{
						Action<CheerBarkNodeItemVM> onSelection = CheerBarkNodeItemVM.OnSelection;
						if (onSelection == null)
						{
							return;
						}
						onSelection(this);
					}
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000185A6 File Offset: 0x000167A6
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x000185AE File Offset: 0x000167AE
		[DataSourceProperty]
		public bool HasSubNodes
		{
			get
			{
				return this._hasSubNodes;
			}
			set
			{
				if (value != this._hasSubNodes)
				{
					this._hasSubNodes = value;
					base.OnPropertyChanged("HasSubNodes");
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000185CB File Offset: 0x000167CB
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x000185D3 File Offset: 0x000167D3
		[DataSourceProperty]
		public string TypeAsString
		{
			get
			{
				return this._typeAsString;
			}
			set
			{
				if (value != this._typeAsString)
				{
					this._typeAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeAsString");
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000185F6 File Offset: 0x000167F6
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x000185FE File Offset: 0x000167FE
		[DataSourceProperty]
		public string TauntVisualName
		{
			get
			{
				return this._tauntVisualName;
			}
			set
			{
				if (value != this._tauntVisualName)
				{
					this._tauntVisualName = value;
					base.OnPropertyChangedWithValue<string>(value, "TauntVisualName");
				}
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00018621 File Offset: 0x00016821
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00018629 File Offset: 0x00016829
		[DataSourceProperty]
		public string SelectedNodeText
		{
			get
			{
				return this._selectedNodeText;
			}
			set
			{
				if (value != this._selectedNodeText)
				{
					this._selectedNodeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectedNodeText");
				}
			}
		}

		// Token: 0x040002C8 RID: 712
		public readonly TauntUsageManager.TauntUsage.TauntUsageFlag TauntUsageDisabledReason;

		// Token: 0x040002C9 RID: 713
		private readonly TextObject _nodeName;

		// Token: 0x040002CA RID: 714
		private InputKeyItemVM _shortcutKey;

		// Token: 0x040002CB RID: 715
		private MBBindingList<CheerBarkNodeItemVM> _subNodes;

		// Token: 0x040002CC RID: 716
		private string _cheerNameText;

		// Token: 0x040002CD RID: 717
		private string _typeAsString;

		// Token: 0x040002CE RID: 718
		private string _tauntVisualName;

		// Token: 0x040002CF RID: 719
		private string _selectedNodeText;

		// Token: 0x040002D0 RID: 720
		private bool _isDisabled;

		// Token: 0x040002D1 RID: 721
		private bool _isSelected;

		// Token: 0x040002D2 RID: 722
		private bool _hasSubNodes;
	}
}
