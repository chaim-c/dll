using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar
{
	// Token: 0x02000055 RID: 85
	public class MapTimeControlVM : ViewModel
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001ECBF File Offset: 0x0001CEBF
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001ECC7 File Offset: 0x0001CEC7
		public bool IsInBattleSimulation { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public bool IsInRecruitment { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001ECE1 File Offset: 0x0001CEE1
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0001ECE9 File Offset: 0x0001CEE9
		public bool IsEncyclopediaOpen { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001ECF2 File Offset: 0x0001CEF2
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001ECFA File Offset: 0x0001CEFA
		public bool IsInArmyManagement { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001ED03 File Offset: 0x0001CF03
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001ED0B File Offset: 0x0001CF0B
		public bool IsInTownManagement { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001ED14 File Offset: 0x0001CF14
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		public bool IsInHideoutTroopManage { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001ED25 File Offset: 0x0001CF25
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001ED2D File Offset: 0x0001CF2D
		public bool IsInMap { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001ED36 File Offset: 0x0001CF36
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001ED3E File Offset: 0x0001CF3E
		public bool IsInCampaignOptions { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001ED47 File Offset: 0x0001CF47
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001ED4F File Offset: 0x0001CF4F
		public bool IsEscapeMenuOpened { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001ED58 File Offset: 0x0001CF58
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001ED60 File Offset: 0x0001CF60
		public bool IsMarriageOfferPopupActive { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001ED69 File Offset: 0x0001CF69
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001ED71 File Offset: 0x0001CF71
		public bool IsMapCheatsActive { get; set; }

		// Token: 0x060006BD RID: 1725 RVA: 0x0001ED7C File Offset: 0x0001CF7C
		public MapTimeControlVM(Func<MapBarShortcuts> getMapBarShortcuts, Action onTimeFlowStateChange, Action onCameraResetted)
		{
			this._onTimeFlowStateChange = onTimeFlowStateChange;
			this._getMapBarShortcuts = getMapBarShortcuts;
			this._onCameraReset = onCameraResetted;
			this.IsCenterPanelEnabled = false;
			this._lastSetDate = CampaignTime.Zero;
			this.PlayHint = new BasicTooltipViewModel();
			this.FastForwardHint = new BasicTooltipViewModel();
			this.PauseHint = new BasicTooltipViewModel();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			CampaignEvents.OnSaveStartedEvent.AddNonSerializedListener(this, new Action(this.OnSaveStarted));
			CampaignEvents.OnSaveOverEvent.AddNonSerializedListener(this, new Action<bool, string>(this.OnSaveOver));
			this.RefreshValues();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001EE34 File Offset: 0x0001D034
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._shortcuts = this._getMapBarShortcuts();
			if (Input.IsGamepadActive)
			{
				this.PlayHint.SetHintCallback(() => GameTexts.FindText("str_play", null).ToString());
				this.FastForwardHint.SetHintCallback(() => GameTexts.FindText("str_fast_forward", null).ToString());
				this.PauseHint.SetHintCallback(() => GameTexts.FindText("str_pause", null).ToString());
			}
			else
			{
				this.PlayHint.SetHintCallback(delegate
				{
					GameTexts.SetVariable("TEXT", GameTexts.FindText("str_play", null).ToString());
					GameTexts.SetVariable("HOTKEY", this._shortcuts.PlayHotkey);
					return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
				});
				this.FastForwardHint.SetHintCallback(delegate
				{
					GameTexts.SetVariable("TEXT", GameTexts.FindText("str_fast_forward", null).ToString());
					GameTexts.SetVariable("HOTKEY", this._shortcuts.FastForwardHotkey);
					return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
				});
				this.PauseHint.SetHintCallback(delegate
				{
					GameTexts.SetVariable("TEXT", GameTexts.FindText("str_pause", null).ToString());
					GameTexts.SetVariable("HOTKEY", this._shortcuts.PauseHotkey);
					return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
				});
			}
			this.PausedText = GameTexts.FindText("str_paused_capital", null).ToString();
			this.Date = CampaignTime.Now.ToString();
			this._lastSetDate = CampaignTime.Now;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001EF64 File Offset: 0x0001D164
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			this._onTimeFlowStateChange = null;
			this._getMapBarShortcuts = null;
			this._onCameraReset = null;
			CampaignEvents.OnSaveStartedEvent.ClearListeners(this);
			CampaignEvents.OnSaveOverEvent.ClearListeners(this);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001EFC2 File Offset: 0x0001D1C2
		private void OnGamepadActiveStateChanged()
		{
			this.RefreshValues();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001EFCA File Offset: 0x0001D1CA
		private void OnSaveStarted()
		{
			this._isSaving = true;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001EFD3 File Offset: 0x0001D1D3
		private void OnSaveOver(bool wasSuccessful, string saveName)
		{
			this._isSaving = false;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001EFDC File Offset: 0x0001D1DC
		public void Tick()
		{
			this.TimeFlowState = (int)Campaign.Current.GetSimplifiedTimeControlMode();
			this.IsCurrentlyPausedOnMap = ((this.TimeFlowState == 0 || this.TimeFlowState == 6) && this.IsCenterPanelEnabled && !this.IsEscapeMenuOpened && !this._isSaving);
			this.IsCenterPanelEnabled = (!this.IsInBattleSimulation && !this.IsInRecruitment && !this.IsEncyclopediaOpen && !this.IsInTownManagement && !this.IsInArmyManagement && this.IsInMap && !this.IsInCampaignOptions && !this.IsInHideoutTroopManage && !this.IsMarriageOfferPopupActive && !this.IsMapCheatsActive);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001F088 File Offset: 0x0001D288
		public void Refresh()
		{
			if (!this._lastSetDate.StringSameAs(CampaignTime.Now))
			{
				this.Date = CampaignTime.Now.ToString();
				this._lastSetDate = CampaignTime.Now;
			}
			this.Time = CampaignTime.Now.ToHours % 24.0;
			this.TimeOfDayHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTimeOfDayAndResetCameraTooltip());
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001F112 File Offset: 0x0001D312
		private void SetTimeSpeed(int speed)
		{
			Campaign.Current.SetTimeSpeed(speed);
			this._onTimeFlowStateChange();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001F12C File Offset: 0x0001D32C
		public void ExecuteTimeControlChange(int selectedTimeSpeed)
		{
			if (Campaign.Current.CurrentMenuContext == null || (Campaign.Current.CurrentMenuContext.GameMenu.IsWaitActive && !Campaign.Current.TimeControlModeLock))
			{
				int num = selectedTimeSpeed;
				if (this._timeFlowState == 3 && num == 2)
				{
					num = 4;
				}
				else if (this._timeFlowState == 4 && num == 1)
				{
					num = 3;
				}
				else if (this._timeFlowState == 2 && num == 0)
				{
					num = 6;
				}
				if (num != this._timeFlowState)
				{
					this.TimeFlowState = num;
					this.SetTimeSpeed(selectedTimeSpeed);
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001F1B0 File Offset: 0x0001D3B0
		public void ExecuteResetCamera()
		{
			this._onCameraReset();
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001F1BD File Offset: 0x0001D3BD
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001F1C5 File Offset: 0x0001D3C5
		[DataSourceProperty]
		public BasicTooltipViewModel TimeOfDayHint
		{
			get
			{
				return this._timeOfDayHint;
			}
			set
			{
				if (value != this._timeOfDayHint)
				{
					this._timeOfDayHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "TimeOfDayHint");
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001F1E3 File Offset: 0x0001D3E3
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001F1EB File Offset: 0x0001D3EB
		[DataSourceProperty]
		public bool IsCurrentlyPausedOnMap
		{
			get
			{
				return this._isCurrentlyPausedOnMap;
			}
			set
			{
				if (value != this._isCurrentlyPausedOnMap)
				{
					this._isCurrentlyPausedOnMap = value;
					base.OnPropertyChangedWithValue(value, "IsCurrentlyPausedOnMap");
				}
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001F209 File Offset: 0x0001D409
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001F211 File Offset: 0x0001D411
		[DataSourceProperty]
		public bool IsCenterPanelEnabled
		{
			get
			{
				return this._isCenterPanelEnabled;
			}
			set
			{
				if (value != this._isCenterPanelEnabled)
				{
					this._isCenterPanelEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsCenterPanelEnabled");
				}
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001F22F File Offset: 0x0001D42F
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001F237 File Offset: 0x0001D437
		[DataSourceProperty]
		public double Time
		{
			get
			{
				return this._time;
			}
			set
			{
				if (this._time != value)
				{
					this._time = value;
					base.OnPropertyChangedWithValue(value, "Time");
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001F255 File Offset: 0x0001D455
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001F25D File Offset: 0x0001D45D
		[DataSourceProperty]
		public string PausedText
		{
			get
			{
				return this._pausedText;
			}
			set
			{
				if (this._pausedText != value)
				{
					this._pausedText = value;
					base.OnPropertyChangedWithValue<string>(value, "PausedText");
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001F280 File Offset: 0x0001D480
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001F288 File Offset: 0x0001D488
		[DataSourceProperty]
		public string Date
		{
			get
			{
				return this._date;
			}
			set
			{
				if (value != this._date)
				{
					this._date = value;
					base.OnPropertyChangedWithValue<string>(value, "Date");
				}
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001F2AB File Offset: 0x0001D4AB
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0001F2B3 File Offset: 0x0001D4B3
		[DataSourceProperty]
		public int TimeFlowState
		{
			get
			{
				return this._timeFlowState;
			}
			set
			{
				if (value != this._timeFlowState)
				{
					this._timeFlowState = value;
					base.OnPropertyChangedWithValue(value, "TimeFlowState");
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001F2D1 File Offset: 0x0001D4D1
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001F2D9 File Offset: 0x0001D4D9
		[DataSourceProperty]
		public BasicTooltipViewModel PauseHint
		{
			get
			{
				return this._pauseHint;
			}
			set
			{
				if (value != this._pauseHint)
				{
					this._pauseHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PauseHint");
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001F2F7 File Offset: 0x0001D4F7
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0001F2FF File Offset: 0x0001D4FF
		[DataSourceProperty]
		public BasicTooltipViewModel PlayHint
		{
			get
			{
				return this._playHint;
			}
			set
			{
				if (value != this._playHint)
				{
					this._playHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PlayHint");
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001F31D File Offset: 0x0001D51D
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0001F325 File Offset: 0x0001D525
		[DataSourceProperty]
		public BasicTooltipViewModel FastForwardHint
		{
			get
			{
				return this._fastForwardHint;
			}
			set
			{
				if (value != this._fastForwardHint)
				{
					this._fastForwardHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "FastForwardHint");
				}
			}
		}

		// Token: 0x040002F9 RID: 761
		private Action _onTimeFlowStateChange;

		// Token: 0x040002FA RID: 762
		private Func<MapBarShortcuts> _getMapBarShortcuts;

		// Token: 0x040002FB RID: 763
		private MapBarShortcuts _shortcuts;

		// Token: 0x040002FC RID: 764
		private Action _onCameraReset;

		// Token: 0x040002FD RID: 765
		private CampaignTime _lastSetDate;

		// Token: 0x040002FE RID: 766
		private bool _isSaving;

		// Token: 0x040002FF RID: 767
		private int _timeFlowState = -1;

		// Token: 0x04000300 RID: 768
		private double _time;

		// Token: 0x04000301 RID: 769
		private string _date;

		// Token: 0x04000302 RID: 770
		private string _pausedText;

		// Token: 0x04000303 RID: 771
		private bool _isCurrentlyPausedOnMap;

		// Token: 0x04000304 RID: 772
		private bool _isCenterPanelEnabled;

		// Token: 0x04000305 RID: 773
		private BasicTooltipViewModel _pauseHint;

		// Token: 0x04000306 RID: 774
		private BasicTooltipViewModel _playHint;

		// Token: 0x04000307 RID: 775
		private BasicTooltipViewModel _fastForwardHint;

		// Token: 0x04000308 RID: 776
		private BasicTooltipViewModel _timeOfDayHint;
	}
}
