using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200001E RID: 30
	public class GameMenuItemWidget : Widget
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005D88 File Offset: 0x00003F88
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005D90 File Offset: 0x00003F90
		public Brush DefaultTextBrush { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005D99 File Offset: 0x00003F99
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005DA1 File Offset: 0x00003FA1
		public Brush HoveredTextBrush { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005DAA File Offset: 0x00003FAA
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00005DB2 File Offset: 0x00003FB2
		public Brush PressedTextBrush { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005DBB File Offset: 0x00003FBB
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00005DC3 File Offset: 0x00003FC3
		public Brush DisabledTextBrush { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005DCC File Offset: 0x00003FCC
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public Brush NormalQuestBrush { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005DDD File Offset: 0x00003FDD
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005DE5 File Offset: 0x00003FE5
		public Brush MainStoryQuestBrush { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005DEE File Offset: 0x00003FEE
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005DF6 File Offset: 0x00003FF6
		public RichTextWidget ItemRichTextWidget { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x00005DFF File Offset: 0x00003FFF
		public GameMenuItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005E30 File Offset: 0x00004030
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._firstFrame)
			{
				base.GamepadNavigationIndex = base.GetSiblingIndex();
				this._firstFrame = false;
			}
			if (this._latestTextWidgetState != this.ItemRichTextWidget.CurrentState)
			{
				if (this.ItemRichTextWidget.CurrentState == "Default")
				{
					this.ItemRichTextWidget.Brush = this.DefaultTextBrush;
				}
				else if (this.ItemRichTextWidget.CurrentState == "Hovered")
				{
					this.ItemRichTextWidget.Brush = this.HoveredTextBrush;
				}
				else if (this.ItemRichTextWidget.CurrentState == "Pressed")
				{
					this.ItemRichTextWidget.Brush = this.PressedTextBrush;
				}
				else if (this.ItemRichTextWidget.CurrentState == "Disabled")
				{
					this.ItemRichTextWidget.Brush = this.DisabledTextBrush;
				}
				this._latestTextWidgetState = this.ItemRichTextWidget.CurrentState;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005F34 File Offset: 0x00004134
		private void SetLeaveTypeIcon(int type)
		{
			string text = string.Empty;
			switch (type)
			{
			case 0:
				text = "Default";
				break;
			case 1:
				text = "Mission";
				break;
			case 2:
				text = "SubMenu";
				break;
			case 3:
				text = "BribeAndEscape";
				break;
			case 4:
				text = "Escape";
				break;
			case 5:
				text = "Craft";
				break;
			case 6:
				text = "ForceToGiveGoods";
				break;
			case 7:
				text = "ForceToGiveTroops";
				break;
			case 8:
				text = "Bribe";
				break;
			case 9:
				text = "LeaveTroopsAndFlee";
				break;
			case 10:
				text = "OrderTroopsToAttack";
				break;
			case 11:
				text = "Raid";
				break;
			case 12:
				text = "HostileAction";
				break;
			case 13:
				text = "Recruit";
				break;
			case 14:
				text = "Trade";
				break;
			case 15:
				text = "Wait";
				break;
			case 16:
				text = "Leave";
				break;
			case 17:
				text = "Continue";
				break;
			case 18:
				text = "Manage";
				break;
			case 19:
				text = "ManageHideoutTroops";
				break;
			case 20:
				text = "WaitQuest";
				break;
			case 21:
				text = "Surrender";
				break;
			case 22:
				text = "Conversation";
				break;
			case 23:
				text = "DefendAction";
				break;
			case 24:
				text = "Devastate";
				break;
			case 25:
				text = "Pillage";
				break;
			case 26:
				text = "ShowMercy";
				break;
			case 27:
				text = "Leaderboard";
				break;
			case 28:
				text = "OpenStash";
				break;
			case 29:
				text = "ManageGarrison";
				break;
			case 30:
				text = "StagePrisonBreak";
				break;
			case 31:
				text = "ManagePrisoners";
				break;
			case 32:
				text = "Ransom";
				break;
			case 33:
				text = "PracticeFight";
				break;
			case 34:
				text = "BesiegeTown";
				break;
			case 35:
				text = "SneakIn";
				break;
			case 36:
				text = "LeadAssault";
				break;
			case 37:
				text = "DonateTroops";
				break;
			case 38:
				text = "DonatePrisoners";
				break;
			case 39:
				text = "SiegeAmbush";
				break;
			case 40:
				text = "Warehouse";
				break;
			}
			if (!string.IsNullOrEmpty(text) && type != 0)
			{
				this.LeaveTypeIcon.SetState(text);
				this.LeaveTypeIcon.IsVisible = true;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000061A8 File Offset: 0x000043A8
		private void SetLeaveTypeSound()
		{
			ButtonWidget parentButton = this.ParentButton;
			AudioProperty audioProperty = (parentButton != null) ? parentButton.Brush.SoundProperties.GetEventAudioProperty("Click") : null;
			if (audioProperty != null)
			{
				audioProperty.AudioName = "default";
				int leaveType = this.LeaveType;
				if (leaveType <= 12)
				{
					if (leaveType != 1)
					{
						if (leaveType != 9)
						{
							if (leaveType != 12)
							{
								return;
							}
							if (this.GameMenuStringId == "encounter")
							{
								if (this.BattleSize < 50)
								{
									audioProperty.AudioName = "panels/battle/attack_small";
									return;
								}
								if (this.BattleSize < 100)
								{
									audioProperty.AudioName = "panels/battle/attack_medium";
									return;
								}
								audioProperty.AudioName = "panels/battle/attack_large";
								return;
							}
						}
						else if (this.GameMenuStringId == "encounter" || this.GameMenuStringId == "encounter_interrupted_siege_preparations" || this.GameMenuStringId == "menu_siege_strategies")
						{
							audioProperty.AudioName = "panels/battle/retreat";
							return;
						}
					}
					else if (this.GameMenuStringId == "menu_siege_strategies")
					{
						audioProperty.AudioName = "panels/siege/sally_out";
						return;
					}
				}
				else if (leaveType <= 25)
				{
					if (leaveType != 21)
					{
						if (leaveType - 24 > 1)
						{
							return;
						}
						audioProperty.AudioName = "panels/siege/raid";
						return;
					}
					else if (this.GameMenuStringId == "encounter")
					{
						audioProperty.AudioName = "panels/battle/retreat";
						return;
					}
				}
				else
				{
					if (leaveType == 34)
					{
						audioProperty.AudioName = "panels/siege/besiege";
						return;
					}
					if (leaveType != 36)
					{
						return;
					}
					audioProperty.AudioName = "panels/siege/lead_assault";
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006320 File Offset: 0x00004520
		private void SetProgressIconType(int type, Widget progressWidget)
		{
			string text = string.Empty;
			switch (type)
			{
			case 0:
				text = "Default";
				break;
			case 1:
				text = "Available";
				break;
			case 2:
				text = "Active";
				break;
			case 3:
				text = "Completed";
				break;
			default:
				text = "";
				break;
			}
			if (progressWidget == this.QuestIconWidget)
			{
				this.QuestIconWidget.Brush = (this.IsMainStoryQuest ? this.MainStoryQuestBrush : this.NormalQuestBrush);
			}
			if (!string.IsNullOrEmpty(text) && type != 0)
			{
				progressWidget.SetState(text);
				progressWidget.IsVisible = true;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000063B4 File Offset: 0x000045B4
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000063BC File Offset: 0x000045BC
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (this._itemType != value)
				{
					this._itemType = value;
					base.OnPropertyChanged(value, "ItemType");
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000063DA File Offset: 0x000045DA
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000063E2 File Offset: 0x000045E2
		public BrushWidget QuestIconWidget
		{
			get
			{
				return this._questIconWidget;
			}
			set
			{
				if (this._questIconWidget != value)
				{
					this._questIconWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "QuestIconWidget");
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006400 File Offset: 0x00004600
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006408 File Offset: 0x00004608
		public BrushWidget IssueIconWidget
		{
			get
			{
				return this._issueIconWidget;
			}
			set
			{
				if (this._issueIconWidget != value)
				{
					this._issueIconWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "IssueIconWidget");
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006426 File Offset: 0x00004626
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000642E File Offset: 0x0000462E
		public int LeaveType
		{
			get
			{
				return this._leaveType;
			}
			set
			{
				if (this._leaveType != value)
				{
					this._leaveType = value;
					base.OnPropertyChanged(value, "LeaveType");
					this.SetLeaveTypeIcon(value);
					this.SetLeaveTypeSound();
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00006459 File Offset: 0x00004659
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00006461 File Offset: 0x00004661
		public bool IsMainStoryQuest
		{
			get
			{
				return this._isMainStoryQuest;
			}
			set
			{
				if (this._isMainStoryQuest != value)
				{
					this._isMainStoryQuest = value;
					base.OnPropertyChanged(value, "IsMainStoryQuest");
					this.SetProgressIconType(this.QuestType, this.QuestIconWidget);
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006491 File Offset: 0x00004691
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00006499 File Offset: 0x00004699
		public int QuestType
		{
			get
			{
				return this._questType;
			}
			set
			{
				if (this._questType != value)
				{
					this._questType = value;
					base.OnPropertyChanged(value, "QuestType");
					this.SetProgressIconType(value, this.QuestIconWidget);
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000064C4 File Offset: 0x000046C4
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000064CC File Offset: 0x000046CC
		public int IssueType
		{
			get
			{
				return this._issueType;
			}
			set
			{
				if (this._issueType != value)
				{
					this._issueType = value;
					base.OnPropertyChanged(value, "IssueType");
					this.SetProgressIconType(value, this.IssueIconWidget);
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000064F7 File Offset: 0x000046F7
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000064FF File Offset: 0x000046FF
		public bool IsWaitActive
		{
			get
			{
				return this._isWaitActive;
			}
			set
			{
				if (this._isWaitActive != value)
				{
					this._isWaitActive = value;
					base.OnPropertyChanged(value, "IsWaitActive");
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000651D File Offset: 0x0000471D
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00006525 File Offset: 0x00004725
		public BrushWidget LeaveTypeIcon
		{
			get
			{
				return this._leaveTypeIcon;
			}
			set
			{
				if (this._leaveTypeIcon != value)
				{
					this._leaveTypeIcon = value;
					base.OnPropertyChanged<BrushWidget>(value, "LeaveTypeIcon");
					if (value != null)
					{
						this.LeaveTypeIcon.IsVisible = false;
					}
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006552 File Offset: 0x00004752
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000655A File Offset: 0x0000475A
		public BrushWidget WaitStateWidget
		{
			get
			{
				return this._waitStateWidget;
			}
			set
			{
				if (this._waitStateWidget != value)
				{
					this._waitStateWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "WaitStateWidget");
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006578 File Offset: 0x00004778
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00006580 File Offset: 0x00004780
		public ButtonWidget ParentButton
		{
			get
			{
				return this._parentButton;
			}
			set
			{
				if (value != this._parentButton)
				{
					this._parentButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ParentButton");
					this._parentButton.boolPropertyChanged += this.ParentButton_PropertyChanged;
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000065B5 File Offset: 0x000047B5
		private void ParentButton_PropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsDisabled" || propertyName == "IsHighlightEnabled")
			{
				Action onOptionStateChanged = this.OnOptionStateChanged;
				if (onOptionStateChanged == null)
				{
					return;
				}
				onOptionStateChanged();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000065E1 File Offset: 0x000047E1
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000065E9 File Offset: 0x000047E9
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
					base.OnPropertyChanged<string>(value, "GameMenuStringId");
					this.SetLeaveTypeSound();
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006612 File Offset: 0x00004812
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000661A File Offset: 0x0000481A
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
					base.OnPropertyChanged(value, "BattleSize");
					this.SetLeaveTypeSound();
				}
			}
		}

		// Token: 0x040000A4 RID: 164
		public Action OnOptionStateChanged;

		// Token: 0x040000AC RID: 172
		private string _latestTextWidgetState = "";

		// Token: 0x040000AD RID: 173
		private bool _firstFrame = true;

		// Token: 0x040000AE RID: 174
		private int _itemType;

		// Token: 0x040000AF RID: 175
		private bool _isWaitActive;

		// Token: 0x040000B0 RID: 176
		private bool _isMainStoryQuest;

		// Token: 0x040000B1 RID: 177
		private BrushWidget _waitStateWidget;

		// Token: 0x040000B2 RID: 178
		private BrushWidget _leaveTypeIcon;

		// Token: 0x040000B3 RID: 179
		private int _leaveType = -1;

		// Token: 0x040000B4 RID: 180
		private int _questType = -1;

		// Token: 0x040000B5 RID: 181
		private int _issueType = -1;

		// Token: 0x040000B6 RID: 182
		private BrushWidget _questIconWidget;

		// Token: 0x040000B7 RID: 183
		private BrushWidget _issueIconWidget;

		// Token: 0x040000B8 RID: 184
		private ButtonWidget _parentButton;

		// Token: 0x040000B9 RID: 185
		private string _gameMenuStringId;

		// Token: 0x040000BA RID: 186
		private int _battleSize;
	}
}
