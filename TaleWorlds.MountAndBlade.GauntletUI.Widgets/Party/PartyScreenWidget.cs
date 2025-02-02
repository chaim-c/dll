using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.GauntletUI.GamepadNavigation;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000061 RID: 97
	public class PartyScreenWidget : Widget
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public PartyTroopTupleButtonWidget CurrentMainTuple
		{
			get
			{
				return this._currentMainTuple;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public PartyTroopTupleButtonWidget CurrentOtherTuple
		{
			get
			{
				return this._currentOtherTuple;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		public ScrollablePanel MainScrollPanel { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000F8C9 File Offset: 0x0000DAC9
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		public ScrollablePanel OtherScrollPanel { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000F8DA File Offset: 0x0000DADA
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000F8E2 File Offset: 0x0000DAE2
		public InputKeyVisualWidget TransferInputKeyVisual { get; set; }

		// Token: 0x06000517 RID: 1303 RVA: 0x0000F8EB File Offset: 0x0000DAEB
		public PartyScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		protected override void OnConnectedToRoot()
		{
			base.Context.EventManager.OnDragStarted += this.OnDragStarted;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000F912 File Offset: 0x0000DB12
		protected override void OnDisconnectedFromRoot()
		{
			base.Context.EventManager.OnDragStarted -= this.OnDragStarted;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F930 File Offset: 0x0000DB30
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			Widget latestMouseUpWidget = base.EventManager.LatestMouseUpWidget;
			if (this._currentMainTuple != null && latestMouseUpWidget != null && !(latestMouseUpWidget is PartyTroopTupleButtonWidget) && !this._currentMainTuple.CheckIsMyChildRecursive(latestMouseUpWidget))
			{
				PartyTroopTupleButtonWidget currentOtherTuple = this._currentOtherTuple;
				if (currentOtherTuple != null && !currentOtherTuple.CheckIsMyChildRecursive(latestMouseUpWidget) && this.IsWidgetChildOfType<PartyFormationDropdownWidget>(latestMouseUpWidget) == null)
				{
					this.SetCurrentTuple(null, false);
				}
			}
			this.UpdateInputKeyVisualsVisibility();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._newAddedTroop != null)
			{
				string characterID = this._newAddedTroop.CharacterID;
				PartyTroopTupleButtonWidget currentMainTuple = this._currentMainTuple;
				if (characterID == ((currentMainTuple != null) ? currentMainTuple.CharacterID : null))
				{
					bool isPrisoner = this._newAddedTroop.IsPrisoner;
					PartyTroopTupleButtonWidget currentMainTuple2 = this._currentMainTuple;
					bool? flag = (currentMainTuple2 != null) ? new bool?(currentMainTuple2.IsPrisoner) : null;
					if (isPrisoner == flag.GetValueOrDefault() & flag != null)
					{
						bool isTupleLeftSide = this._newAddedTroop.IsTupleLeftSide;
						PartyTroopTupleButtonWidget currentMainTuple3 = this._currentMainTuple;
						flag = ((currentMainTuple3 != null) ? new bool?(currentMainTuple3.IsTupleLeftSide) : null);
						if (!(isTupleLeftSide == flag.GetValueOrDefault() & flag != null))
						{
							this._currentOtherTuple = this._newAddedTroop;
							this._currentOtherTuple.IsSelected = true;
							this.UpdateScrollTarget();
						}
					}
				}
				this._newAddedTroop = null;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public void SetCurrentTuple(PartyTroopTupleButtonWidget tuple, bool isLeftSide)
		{
			if (this._currentMainTuple != null && this._currentMainTuple != tuple)
			{
				this._currentMainTuple.IsSelected = false;
				if (this._currentOtherTuple != null)
				{
					this._currentOtherTuple.IsSelected = false;
					this._currentOtherTuple = null;
				}
			}
			if (tuple == null)
			{
				this._currentMainTuple = null;
				this.RemoveZeroCountItems();
				if (this._currentOtherTuple != null)
				{
					this._currentOtherTuple.IsSelected = false;
					this._currentOtherTuple = null;
					return;
				}
			}
			else
			{
				if (tuple == this._currentMainTuple || tuple == this._currentOtherTuple)
				{
					this.SetCurrentTuple(null, false);
					return;
				}
				this._currentMainTuple = tuple;
				this._currentOtherTuple = this.FindTupleWithTroopIDInList(this._currentMainTuple.CharacterID, this._currentMainTuple.IsTupleLeftSide, this._currentMainTuple.IsPrisoner);
				if (this._currentOtherTuple != null)
				{
					this._currentOtherTuple.IsSelected = true;
					this.UpdateScrollTarget();
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000FB64 File Offset: 0x0000DD64
		private void UpdateInputKeyVisualsVisibility()
		{
			if (base.EventManager.IsControllerActive)
			{
				bool flag = false;
				PartyTroopTupleButtonWidget partyTroopTupleButtonWidget;
				if ((partyTroopTupleButtonWidget = (base.EventManager.HoveredView as PartyTroopTupleButtonWidget)) != null)
				{
					this.TransferInputKeyVisual.IsVisible = partyTroopTupleButtonWidget.IsTransferable;
					flag = true;
					if (partyTroopTupleButtonWidget.IsTupleLeftSide)
					{
						this.TransferInputKeyVisual.KeyID = this._takeAllPrisonersInputKeyVisual.KeyID;
						this.TransferInputKeyVisual.ScaledPositionXOffset = partyTroopTupleButtonWidget.GlobalPosition.X + partyTroopTupleButtonWidget.Size.X - 65f * base._scaleToUse - base.EventManager.LeftUsableAreaStart;
						this.TransferInputKeyVisual.ScaledPositionYOffset = partyTroopTupleButtonWidget.GlobalPosition.Y - 13f * base._scaleToUse - base.EventManager.TopUsableAreaStart;
					}
					else
					{
						this.TransferInputKeyVisual.KeyID = this._dismissAllPrisonersInputKeyVisual.KeyID;
						this.TransferInputKeyVisual.ScaledPositionXOffset = partyTroopTupleButtonWidget.GlobalPosition.X + 5f * base._scaleToUse - base.EventManager.LeftUsableAreaStart;
						this.TransferInputKeyVisual.ScaledPositionYOffset = partyTroopTupleButtonWidget.GlobalPosition.Y - 13f * base._scaleToUse - base.EventManager.TopUsableAreaStart;
					}
				}
				else
				{
					this.TransferInputKeyVisual.IsVisible = false;
					this.TransferInputKeyVisual.KeyID = "";
				}
				bool isVisible = !this.IsAnyPopupOpen() && !flag && !this.MainScrollPanel.InnerPanel.IsHovered && !this.OtherScrollPanel.InnerPanel.IsHovered && !GauntletGamepadNavigationManager.Instance.IsCursorMovingForNavigation;
				this.TakeAllPrisonersInputKeyVisualParent.IsVisible = isVisible;
				this.DismissAllPrisonersInputKeyVisualParent.IsVisible = isVisible;
				return;
			}
			this.TransferInputKeyVisual.IsVisible = false;
			this.TakeAllPrisonersInputKeyVisualParent.IsVisible = true;
			this.DismissAllPrisonersInputKeyVisualParent.IsVisible = true;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		private void RefreshWarningStatuses()
		{
			TextWidget prisonerLabel = this.PrisonerLabel;
			if (prisonerLabel != null)
			{
				prisonerLabel.SetState(this.IsPrisonerWarningEnabled ? "OverLimit" : "Default");
			}
			TextWidget troopLabel = this.TroopLabel;
			if (troopLabel != null)
			{
				troopLabel.SetState(this.IsTroopWarningEnabled ? "OverLimit" : "Default");
			}
			TextWidget otherTroopLabel = this.OtherTroopLabel;
			if (otherTroopLabel == null)
			{
				return;
			}
			otherTroopLabel.SetState(this.IsOtherTroopWarningEnabled ? "OverLimit" : "Default");
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		private PartyTroopTupleButtonWidget FindTupleWithTroopIDInList(string troopID, bool searchMainList, bool isPrisoner)
		{
			IEnumerable<PartyTroopTupleButtonWidget> source;
			if (searchMainList)
			{
				source = (isPrisoner ? this.MainPrisonerList.Children.Cast<PartyTroopTupleButtonWidget>() : this.MainMemberList.Children.Cast<PartyTroopTupleButtonWidget>());
			}
			else
			{
				source = (isPrisoner ? this.OtherPrisonerList.Children.Cast<PartyTroopTupleButtonWidget>() : this.OtherMemberList.Children.Cast<PartyTroopTupleButtonWidget>());
			}
			return source.SingleOrDefault((PartyTroopTupleButtonWidget i) => i.CharacterID == troopID);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000FE47 File Offset: 0x0000E047
		private void OnDragStarted()
		{
			this.RemoveZeroCountItems();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000FE4F File Offset: 0x0000E04F
		private void RemoveZeroCountItems()
		{
			base.EventFired("RemoveZeroCounts", Array.Empty<object>());
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000FE64 File Offset: 0x0000E064
		private void UpdateScrollTarget()
		{
			PartyTroopTupleButtonWidget currentOtherTuple = this._currentOtherTuple;
			if (((currentOtherTuple != null) ? currentOtherTuple.ParentWidget : null) != null)
			{
				(this._currentOtherTuple.IsTupleLeftSide ? this.OtherScrollPanel : this.MainScrollPanel).ScrollToChild(this._currentOtherTuple, -1f, 1f, 0, 400, 0.35f, 0f);
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000FEC5 File Offset: 0x0000E0C5
		private bool IsAnyPopupOpen()
		{
			Widget recruitPopupParent = this.RecruitPopupParent;
			if (recruitPopupParent == null || !recruitPopupParent.IsVisible)
			{
				Widget upgradePopupParent = this.UpgradePopupParent;
				return upgradePopupParent != null && upgradePopupParent.IsVisible;
			}
			return true;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		private void OnNewTroopAdded(Widget parent, Widget addedChild)
		{
			PartyTroopTupleButtonWidget newAddedTroop;
			if (this._currentMainTuple != null && (newAddedTroop = (addedChild as PartyTroopTupleButtonWidget)) != null)
			{
				this._newAddedTroop = newAddedTroop;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000FF16 File Offset: 0x0000E116
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000FF1E File Offset: 0x0000E11E
		public Widget UpgradePopupParent
		{
			get
			{
				return this._upgradePopupParent;
			}
			set
			{
				if (value != this._upgradePopupParent)
				{
					this._upgradePopupParent = value;
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000FF30 File Offset: 0x0000E130
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000FF38 File Offset: 0x0000E138
		public Widget RecruitPopupParent
		{
			get
			{
				return this._recruitPopupParent;
			}
			set
			{
				if (value != this._recruitPopupParent)
				{
					this._recruitPopupParent = value;
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000FF4A File Offset: 0x0000E14A
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0000FF54 File Offset: 0x0000E154
		public Widget TakeAllPrisonersInputKeyVisualParent
		{
			get
			{
				return this._takeAllPrisonersInputKeyVisualParent;
			}
			set
			{
				if (value != this._takeAllPrisonersInputKeyVisualParent)
				{
					this._takeAllPrisonersInputKeyVisualParent = value;
					if (this._takeAllPrisonersInputKeyVisualParent != null)
					{
						this._takeAllPrisonersInputKeyVisual = (this._takeAllPrisonersInputKeyVisualParent.Children.FirstOrDefault((Widget x) => x is InputKeyVisualWidget) as InputKeyVisualWidget);
					}
				}
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		public Widget DismissAllPrisonersInputKeyVisualParent
		{
			get
			{
				return this._dismissAllPrisonersInputKeyVisualParent;
			}
			set
			{
				if (value != this._dismissAllPrisonersInputKeyVisualParent)
				{
					this._dismissAllPrisonersInputKeyVisualParent = value;
					if (this._dismissAllPrisonersInputKeyVisualParent != null)
					{
						this._dismissAllPrisonersInputKeyVisual = (this._dismissAllPrisonersInputKeyVisualParent.Children.FirstOrDefault((Widget x) => x is InputKeyVisualWidget) as InputKeyVisualWidget);
					}
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001001B File Offset: 0x0000E21B
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00010023 File Offset: 0x0000E223
		[Editor(false)]
		public int MainPartyTroopSize
		{
			get
			{
				return this._mainPartyTroopSize;
			}
			set
			{
				if (this._mainPartyTroopSize != value)
				{
					this._mainPartyTroopSize = value;
					base.OnPropertyChanged(value, "MainPartyTroopSize");
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00010041 File Offset: 0x0000E241
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00010049 File Offset: 0x0000E249
		[Editor(false)]
		public bool IsPrisonerWarningEnabled
		{
			get
			{
				return this._isPrisonerWarningEnabled;
			}
			set
			{
				if (this._isPrisonerWarningEnabled != value)
				{
					this._isPrisonerWarningEnabled = value;
					base.OnPropertyChanged(value, "IsPrisonerWarningEnabled");
					this.RefreshWarningStatuses();
				}
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001006D File Offset: 0x0000E26D
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00010075 File Offset: 0x0000E275
		[Editor(false)]
		public bool IsOtherTroopWarningEnabled
		{
			get
			{
				return this._isOtherTroopWarningEnabled;
			}
			set
			{
				if (this._isOtherTroopWarningEnabled != value)
				{
					this._isOtherTroopWarningEnabled = value;
					base.OnPropertyChanged(value, "IsOtherTroopWarningEnabled");
					this.RefreshWarningStatuses();
				}
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00010099 File Offset: 0x0000E299
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x000100A1 File Offset: 0x0000E2A1
		[Editor(false)]
		public bool IsTroopWarningEnabled
		{
			get
			{
				return this._isTroopWarningEnabled;
			}
			set
			{
				if (this._isTroopWarningEnabled != value)
				{
					this._isTroopWarningEnabled = value;
					base.OnPropertyChanged(value, "IsTroopWarningEnabled");
					this.RefreshWarningStatuses();
				}
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000100C5 File Offset: 0x0000E2C5
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x000100CD File Offset: 0x0000E2CD
		[Editor(false)]
		public TextWidget TroopLabel
		{
			get
			{
				return this._troopLabel;
			}
			set
			{
				if (this._troopLabel != value)
				{
					this._troopLabel = value;
					base.OnPropertyChanged<TextWidget>(value, "TroopLabel");
					this.RefreshWarningStatuses();
				}
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000100F1 File Offset: 0x0000E2F1
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x000100F9 File Offset: 0x0000E2F9
		[Editor(false)]
		public TextWidget PrisonerLabel
		{
			get
			{
				return this._prisonerLabel;
			}
			set
			{
				if (this._prisonerLabel != value)
				{
					this._prisonerLabel = value;
					base.OnPropertyChanged<TextWidget>(value, "PrisonerLabel");
					this.RefreshWarningStatuses();
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001011D File Offset: 0x0000E31D
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00010125 File Offset: 0x0000E325
		[Editor(false)]
		public TextWidget OtherTroopLabel
		{
			get
			{
				return this._otherTroopLabel;
			}
			set
			{
				if (this._otherTroopLabel != value)
				{
					this._otherTroopLabel = value;
					base.OnPropertyChanged<TextWidget>(value, "OtherTroopLabel");
					this.RefreshWarningStatuses();
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00010149 File Offset: 0x0000E349
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00010151 File Offset: 0x0000E351
		[Editor(false)]
		public ListPanel OtherMemberList
		{
			get
			{
				return this._otherMemberList;
			}
			set
			{
				if (this._otherMemberList != value)
				{
					this._otherMemberList = value;
					value.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewTroopAdded));
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001017A File Offset: 0x0000E37A
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00010182 File Offset: 0x0000E382
		[Editor(false)]
		public ListPanel OtherPrisonerList
		{
			get
			{
				return this._otherPrisonerList;
			}
			set
			{
				if (this._otherPrisonerList != value)
				{
					this._otherPrisonerList = value;
					value.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewTroopAdded));
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000101AB File Offset: 0x0000E3AB
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x000101B3 File Offset: 0x0000E3B3
		[Editor(false)]
		public ListPanel MainMemberList
		{
			get
			{
				return this._mainMemberList;
			}
			set
			{
				if (this._mainMemberList != value)
				{
					this._mainMemberList = value;
					value.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewTroopAdded));
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000101DC File Offset: 0x0000E3DC
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x000101E4 File Offset: 0x0000E3E4
		[Editor(false)]
		public ListPanel MainPrisonerList
		{
			get
			{
				return this._mainPrisonerList;
			}
			set
			{
				if (this._mainPrisonerList != value)
				{
					this._mainPrisonerList = value;
					value.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewTroopAdded));
				}
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00010210 File Offset: 0x0000E410
		private T IsWidgetChildOfType<T>(Widget currentWidget) where T : Widget
		{
			while (currentWidget != null)
			{
				T result;
				if ((result = (currentWidget as T)) != null)
				{
					return result;
				}
				currentWidget = currentWidget.ParentWidget;
			}
			return default(T);
		}

		// Token: 0x04000232 RID: 562
		private PartyTroopTupleButtonWidget _currentMainTuple;

		// Token: 0x04000233 RID: 563
		private PartyTroopTupleButtonWidget _currentOtherTuple;

		// Token: 0x04000236 RID: 566
		private InputKeyVisualWidget _takeAllPrisonersInputKeyVisual;

		// Token: 0x04000237 RID: 567
		private InputKeyVisualWidget _dismissAllPrisonersInputKeyVisual;

		// Token: 0x04000239 RID: 569
		private PartyTroopTupleButtonWidget _newAddedTroop;

		// Token: 0x0400023A RID: 570
		private Widget _upgradePopupParent;

		// Token: 0x0400023B RID: 571
		private Widget _recruitPopupParent;

		// Token: 0x0400023C RID: 572
		private Widget _takeAllPrisonersInputKeyVisualParent;

		// Token: 0x0400023D RID: 573
		private Widget _dismissAllPrisonersInputKeyVisualParent;

		// Token: 0x0400023E RID: 574
		private int _mainPartyTroopSize;

		// Token: 0x0400023F RID: 575
		private bool _isPrisonerWarningEnabled;

		// Token: 0x04000240 RID: 576
		private bool _isTroopWarningEnabled;

		// Token: 0x04000241 RID: 577
		private bool _isOtherTroopWarningEnabled;

		// Token: 0x04000242 RID: 578
		private TextWidget _troopLabel;

		// Token: 0x04000243 RID: 579
		private TextWidget _prisonerLabel;

		// Token: 0x04000244 RID: 580
		private TextWidget _otherTroopLabel;

		// Token: 0x04000245 RID: 581
		private ListPanel _otherMemberList;

		// Token: 0x04000246 RID: 582
		private ListPanel _otherPrisonerList;

		// Token: 0x04000247 RID: 583
		private ListPanel _mainMemberList;

		// Token: 0x04000248 RID: 584
		private ListPanel _mainPrisonerList;
	}
}
