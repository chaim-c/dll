using System;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu
{
	// Token: 0x0200008B RID: 139
	public class GameMenuItemVM : BindingListStringItem
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000379C2 File Offset: 0x00035BC2
		public string OptionID { get; }

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000379CC File Offset: 0x00035BCC
		public GameMenuItemVM(MenuContext menuContext, int index, TextObject text, TextObject text2, TextObject tooltip, GameMenu.MenuAndOptionType type, GameMenuOption gameMenuOption, GameKey shortcutKey) : base(text.ToString())
		{
			this._gameMenuOption = gameMenuOption;
			this.ItemHint = new HintViewModel();
			this.Index = index;
			this._menuContext = menuContext;
			this._itemType = (int)type;
			this._tooltip = tooltip;
			this._nonWaitText = text;
			this._waitText = text2;
			base.Item = this._nonWaitText.ToString();
			this.ItemHint.HintText = this._tooltip;
			this.OptionLeaveType = (int)gameMenuOption.OptionLeaveType;
			this.OptionID = gameMenuOption.IdString;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			foreach (GameMenuOption.IssueQuestFlags issueQuestFlags in GameMenuOption.IssueQuestFlagsValues)
			{
				if (issueQuestFlags != GameMenuOption.IssueQuestFlags.None && (gameMenuOption.OptionQuestData & issueQuestFlags) != GameMenuOption.IssueQuestFlags.None)
				{
					CampaignUIHelper.IssueQuestFlags issueQuestFlag = (CampaignUIHelper.IssueQuestFlags)issueQuestFlags;
					this.Quests.Add(new QuestMarkerVM(issueQuestFlag, null, null));
				}
			}
			this.ShortcutKey = ((shortcutKey != null) ? InputKeyItemVM.CreateFromGameKey(shortcutKey, true) : null);
			this.RefreshValues();
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00037AD7 File Offset: 0x00035CD7
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Refresh();
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00037AE5 File Offset: 0x00035CE5
		public void UpdateMenuContext(MenuContext newMenuContext)
		{
			this._menuContext = newMenuContext;
			this.Refresh();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00037AF4 File Offset: 0x00035CF4
		public void ExecuteAction()
		{
			MenuContext menuContext = this._menuContext;
			if (menuContext == null)
			{
				return;
			}
			menuContext.InvokeConsequence(this.Index);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00037B0C File Offset: 0x00035D0C
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (this.ShortcutKey != null)
			{
				this.ShortcutKey.OnFinalize();
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00037B28 File Offset: 0x00035D28
		public void Refresh()
		{
			int itemType = this._itemType;
			if (itemType != 0)
			{
				int num = itemType - 1;
			}
			this.IsWaitActive = Campaign.Current.GameMenuManager.GetVirtualMenuIsWaitActive(this._menuContext);
			this.IsEnabled = Campaign.Current.GameMenuManager.GetVirtualMenuOptionIsEnabled(this._menuContext, this.Index);
			this.ItemHint.HintText = Campaign.Current.GameMenuManager.GetVirtualMenuOptionTooltip(this._menuContext, this.Index);
			this.GameMenuStringId = this._menuContext.GameMenu.StringId;
			if (PlayerEncounter.Battle != null)
			{
				this.BattleSize = PlayerEncounter.Battle.AttackerSide.TroopCount + PlayerEncounter.Battle.DefenderSide.TroopCount;
				return;
			}
			this.BattleSize = -1;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00037BF1 File Offset: 0x00035DF1
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x00037BF9 File Offset: 0x00035DF9
		[DataSourceProperty]
		public MBBindingList<QuestMarkerVM> Quests
		{
			get
			{
				return this._quests;
			}
			set
			{
				if (value != this._quests)
				{
					this._quests = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestMarkerVM>>(value, "Quests");
				}
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00037C17 File Offset: 0x00035E17
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x00037C1F File Offset: 0x00035E1F
		[DataSourceProperty]
		public int OptionLeaveType
		{
			get
			{
				return this._optionLeaveType;
			}
			set
			{
				if (value != this._optionLeaveType)
				{
					this._optionLeaveType = value;
					base.OnPropertyChangedWithValue(value, "OptionLeaveType");
				}
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00037C3D File Offset: 0x00035E3D
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x00037C45 File Offset: 0x00035E45
		[DataSourceProperty]
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (value != this._itemType)
				{
					this._itemType = value;
					base.OnPropertyChangedWithValue(value, "ItemType");
				}
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00037C63 File Offset: 0x00035E63
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x00037C6B File Offset: 0x00035E6B
		[DataSourceProperty]
		public bool IsWaitActive
		{
			get
			{
				return this._isWaitActive;
			}
			set
			{
				if (value != this._isWaitActive)
				{
					this._isWaitActive = value;
					base.OnPropertyChangedWithValue(value, "IsWaitActive");
					base.Item = (value ? this._waitText.ToString() : this._nonWaitText.ToString());
				}
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00037CAA File Offset: 0x00035EAA
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x00037CB2 File Offset: 0x00035EB2
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00037CD0 File Offset: 0x00035ED0
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x00037CD8 File Offset: 0x00035ED8
		[DataSourceProperty]
		public bool IsHighlightEnabled
		{
			get
			{
				return this._isHighlightEnabled;
			}
			set
			{
				if (value != this._isHighlightEnabled)
				{
					this._isHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHighlightEnabled");
				}
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00037CF6 File Offset: 0x00035EF6
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00037CFE File Offset: 0x00035EFE
		[DataSourceProperty]
		public HintViewModel ItemHint
		{
			get
			{
				return this._itemHint;
			}
			set
			{
				if (value != this._itemHint)
				{
					this._itemHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ItemHint");
				}
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00037D1C File Offset: 0x00035F1C
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x00037D24 File Offset: 0x00035F24
		[DataSourceProperty]
		public HintViewModel QuestHint
		{
			get
			{
				return this._questHint;
			}
			set
			{
				if (value != this._questHint)
				{
					this._questHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "QuestHint");
				}
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00037D42 File Offset: 0x00035F42
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x00037D4A File Offset: 0x00035F4A
		[DataSourceProperty]
		public HintViewModel IssueHint
		{
			get
			{
				return this._issueHint;
			}
			set
			{
				if (value != this._issueHint)
				{
					this._issueHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "IssueHint");
				}
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00037D68 File Offset: 0x00035F68
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x00037D70 File Offset: 0x00035F70
		[DataSourceProperty]
		public string GameMenuStringId
		{
			get
			{
				return this._gameMenuStringId;
			}
			set
			{
				if (value != this._gameMenuStringId)
				{
					this._gameMenuStringId = value;
					base.OnPropertyChangedWithValue<string>(value, "GameMenuStringId");
				}
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00037D93 File Offset: 0x00035F93
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00037D9B File Offset: 0x00035F9B
		[DataSourceProperty]
		public int BattleSize
		{
			get
			{
				return this._battleSize;
			}
			set
			{
				if (value != this._battleSize)
				{
					this._battleSize = value;
					base.OnPropertyChangedWithValue(value, "BattleSize");
				}
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00037DB9 File Offset: 0x00035FB9
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x00037DC1 File Offset: 0x00035FC1
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

		// Token: 0x0400064D RID: 1613
		private MenuContext _menuContext;

		// Token: 0x0400064E RID: 1614
		public int Index;

		// Token: 0x0400064F RID: 1615
		private TextObject _nonWaitText;

		// Token: 0x04000650 RID: 1616
		private TextObject _waitText;

		// Token: 0x04000651 RID: 1617
		private TextObject _tooltip;

		// Token: 0x04000653 RID: 1619
		private readonly GameMenuOption _gameMenuOption;

		// Token: 0x04000654 RID: 1620
		private MBBindingList<QuestMarkerVM> _quests;

		// Token: 0x04000655 RID: 1621
		private int _itemType = -1;

		// Token: 0x04000656 RID: 1622
		private bool _isWaitActive;

		// Token: 0x04000657 RID: 1623
		private bool _isEnabled;

		// Token: 0x04000658 RID: 1624
		private HintViewModel _itemHint;

		// Token: 0x04000659 RID: 1625
		private HintViewModel _questHint;

		// Token: 0x0400065A RID: 1626
		private HintViewModel _issueHint;

		// Token: 0x0400065B RID: 1627
		private bool _isHighlightEnabled;

		// Token: 0x0400065C RID: 1628
		private int _optionLeaveType = -1;

		// Token: 0x0400065D RID: 1629
		private string _gameMenuStringId;

		// Token: 0x0400065E RID: 1630
		private int _battleSize = -1;

		// Token: 0x0400065F RID: 1631
		private InputKeyItemVM _shortcutKey;
	}
}
