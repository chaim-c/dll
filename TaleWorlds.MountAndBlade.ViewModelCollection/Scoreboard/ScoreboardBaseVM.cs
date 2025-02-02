using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard
{
	// Token: 0x0200000E RID: 14
	public abstract class ScoreboardBaseVM : ViewModel
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.KillHint = new HintViewModel(GameTexts.FindText("str_battle_result_score_sort_button", "0"), null);
			this.DeadHint = new HintViewModel(GameTexts.FindText("str_battle_result_score_sort_button", "1"), null);
			this.WoundedHint = new HintViewModel(GameTexts.FindText("str_battle_result_score_sort_button", "2"), null);
			this.RoutedHint = new HintViewModel(GameTexts.FindText("str_battle_result_score_sort_button", "3"), null);
			this.RemainingHint = new HintViewModel(GameTexts.FindText("str_battle_result_score_sort_button", "4"), null);
			this.UpgradeHint = new HintViewModel(GameTexts.FindText("str_battle_result_score_sort_button", "5"), null);
			this.QuitText = GameTexts.FindText("str_retreat", null).ToString();
			GameTexts.SetVariable("KEY", Game.Current.GameTextManager.GetHotKeyGameText("Generic", 4));
			this._retreatInquiryData = new InquiryData("", GameTexts.FindText("str_can_not_retreat", null).ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", null, null, "", 0f, null, null, null);
			SPScoreboardSideVM attackers = this.Attackers;
			if (attackers != null)
			{
				attackers.RefreshValues();
			}
			SPScoreboardSideVM defenders = this.Defenders;
			if (defenders != null)
			{
				defenders.RefreshValues();
			}
			this.ShowScoreboardText = new TextObject("{=5Ixsvn3s}Toggle scoreboard", null).ToString();
			this.FastForwardText = new TextObject("{=HH7LDwlK}Toggle Fast Forward", null).ToString();
			InputKeyItemVM showMouseKey = this.ShowMouseKey;
			if (showMouseKey != null)
			{
				showMouseKey.RefreshValues();
			}
			InputKeyItemVM showScoreboardKey = this.ShowScoreboardKey;
			if (showScoreboardKey != null)
			{
				showScoreboardKey.RefreshValues();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.RefreshValues();
			}
			InputKeyItemVM fastForwardKey = this.FastForwardKey;
			if (fastForwardKey == null)
			{
				return;
			}
			fastForwardKey.RefreshValues();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004FA5 File Offset: 0x000031A5
		public void OnMainHeroDeath()
		{
			this.IsMainCharacterDead = true;
			IMissionScreen missionScreen = this._missionScreen;
			if (missionScreen == null)
			{
				return;
			}
			missionScreen.SetOrderFlagVisibility(false);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004FC0 File Offset: 0x000031C0
		public virtual void Initialize(IMissionScreen missionScreen, Mission mission, Action releaseSimulationSources, Action<bool> onToggle)
		{
			this.OnToggle = onToggle;
			this._missionScreen = missionScreen;
			this._mission = mission;
			this._releaseSimulationSources = releaseSimulationSources;
			this.BattleResult = "";
			this.BattleResultIndex = -1;
			this.IsOver = false;
			this.ShowScoreboard = false;
			Action<bool> onToggle2 = this.OnToggle;
			if (onToggle2 != null)
			{
				onToggle2(false);
			}
			if (mission != null)
			{
				this._battleEndLogic = this._mission.GetMissionBehavior<BattleEndLogic>();
			}
			this.PowerComparer = new PowerLevelComparer(1.0, 1.0);
			this.RefreshValues();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005054 File Offset: 0x00003254
		private void UpdateQuitText()
		{
			if (this.IsOver)
			{
				this.QuitText = GameTexts.FindText("str_done", null).ToString();
				return;
			}
			if (this.IsMainCharacterDead && !this.IsSimulation)
			{
				this.QuitText = GameTexts.FindText("str_end_battle", null).ToString();
			}
		}

		// Token: 0x060000DF RID: 223
		public abstract void Tick(float dt);

		// Token: 0x060000E0 RID: 224 RVA: 0x000050A6 File Offset: 0x000032A6
		protected SPScoreboardSideVM GetSide(BattleSideEnum side)
		{
			if (side != BattleSideEnum.Defender)
			{
				return this.Attackers;
			}
			return this.Defenders;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000050B8 File Offset: 0x000032B8
		public void SetMouseState(bool visible)
		{
			this._mouseState = (visible ? ScoreboardBaseVM.MouseState.Visible : ScoreboardBaseVM.MouseState.NotVisible);
			this.IsMouseEnabled = visible;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000050D0 File Offset: 0x000032D0
		public static string GetFormattedTimeTextFromSeconds(int seconds)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)seconds);
			string str = "";
			if (timeSpan.Hours > 0)
			{
				str += string.Format("{0:D2}{1}:", timeSpan.Hours, ScoreboardBaseVM._hourAbbrString);
			}
			str += string.Format("{0:D2}{1}:", timeSpan.Minutes, ScoreboardBaseVM._minuteAbbrString);
			return str + string.Format("{0:D2}{1}", timeSpan.Seconds, ScoreboardBaseVM._secondAbbrString);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000515C File Offset: 0x0000335C
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM showMouseKey = this.ShowMouseKey;
			if (showMouseKey != null)
			{
				showMouseKey.OnFinalize();
			}
			InputKeyItemVM showScoreboardKey = this.ShowScoreboardKey;
			if (showScoreboardKey != null)
			{
				showScoreboardKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM fastForwardKey = this.FastForwardKey;
			if (fastForwardKey == null)
			{
				return;
			}
			fastForwardKey.OnFinalize();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000051B2 File Offset: 0x000033B2
		public virtual void ExecuteShowScoreboardAction()
		{
			this.ShowScoreboard = !this.ShowScoreboard;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000051C3 File Offset: 0x000033C3
		public virtual void ExecutePlayAction()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000051C5 File Offset: 0x000033C5
		public virtual void ExecuteFastForwardAction()
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000051C7 File Offset: 0x000033C7
		public virtual void ExecuteEndSimulationAction()
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000051C9 File Offset: 0x000033C9
		public virtual void ExecuteQuitAction()
		{
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000051CB File Offset: 0x000033CB
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000051D3 File Offset: 0x000033D3
		protected int MissionTimeInSeconds
		{
			get
			{
				return this._missionTimeInSeconds;
			}
			set
			{
				if (value != this._missionTimeInSeconds)
				{
					this._missionTimeInSeconds = value;
					this.MissionTimeStr = ScoreboardBaseVM.GetFormattedTimeTextFromSeconds(this._missionTimeInSeconds);
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000051F6 File Offset: 0x000033F6
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000051FE File Offset: 0x000033FE
		[DataSourceProperty]
		public string MissionTimeStr
		{
			get
			{
				return this._missionTimeStr;
			}
			set
			{
				if (value != this._missionTimeStr)
				{
					this._missionTimeStr = value;
					base.OnPropertyChangedWithValue<string>(value, "MissionTimeStr");
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005221 File Offset: 0x00003421
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005229 File Offset: 0x00003429
		[DataSourceProperty]
		public bool IsPowerComparerEnabled
		{
			get
			{
				return this._isPowerComparerEnabled;
			}
			set
			{
				if (value != this._isPowerComparerEnabled)
				{
					this._isPowerComparerEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPowerComparerEnabled");
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005247 File Offset: 0x00003447
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000524F File Offset: 0x0000344F
		[DataSourceProperty]
		public string QuitText
		{
			get
			{
				return this._quitText;
			}
			set
			{
				if (value != this._quitText)
				{
					this._quitText = value;
					base.OnPropertyChangedWithValue<string>(value, "QuitText");
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005272 File Offset: 0x00003472
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000527A File Offset: 0x0000347A
		[DataSourceProperty]
		public string ShowScoreboardText
		{
			get
			{
				return this._showScoreboardText;
			}
			set
			{
				if (value != this._showScoreboardText)
				{
					this._showScoreboardText = value;
					base.OnPropertyChangedWithValue<string>(value, "ShowScoreboardText");
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000529D File Offset: 0x0000349D
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000052A5 File Offset: 0x000034A5
		[DataSourceProperty]
		public string FastForwardText
		{
			get
			{
				return this._fastForwardText;
			}
			set
			{
				if (value != this._fastForwardText)
				{
					this._fastForwardText = value;
					base.OnPropertyChangedWithValue<string>(value, "FastForwardText");
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000052C8 File Offset: 0x000034C8
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000052D0 File Offset: 0x000034D0
		[DataSourceProperty]
		public SPScoreboardSideVM Attackers
		{
			get
			{
				return this._attackers;
			}
			set
			{
				if (value != this._attackers)
				{
					this._attackers = value;
					base.OnPropertyChangedWithValue<SPScoreboardSideVM>(value, "Attackers");
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000052EE File Offset: 0x000034EE
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000052F6 File Offset: 0x000034F6
		[DataSourceProperty]
		public SPScoreboardSideVM Defenders
		{
			get
			{
				return this._defenders;
			}
			set
			{
				if (value != this._defenders)
				{
					this._defenders = value;
					base.OnPropertyChangedWithValue<SPScoreboardSideVM>(value, "Defenders");
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005314 File Offset: 0x00003514
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000531C File Offset: 0x0000351C
		[DataSourceProperty]
		public HintViewModel KillHint
		{
			get
			{
				return this._killHint;
			}
			set
			{
				if (value != this._killHint)
				{
					this._killHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "KillHint");
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000533A File Offset: 0x0000353A
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005342 File Offset: 0x00003542
		[DataSourceProperty]
		public HintViewModel DeadHint
		{
			get
			{
				return this._deadHint;
			}
			set
			{
				if (value != this._deadHint)
				{
					this._deadHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DeadHint");
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005360 File Offset: 0x00003560
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005368 File Offset: 0x00003568
		[DataSourceProperty]
		public HintViewModel UpgradeHint
		{
			get
			{
				return this._upgradeHint;
			}
			set
			{
				if (value != this._upgradeHint)
				{
					this._upgradeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UpgradeHint");
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005386 File Offset: 0x00003586
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000538E File Offset: 0x0000358E
		[DataSourceProperty]
		public HintViewModel WoundedHint
		{
			get
			{
				return this._woundedHint;
			}
			set
			{
				if (value != this._woundedHint)
				{
					this._woundedHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "WoundedHint");
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000053AC File Offset: 0x000035AC
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000053B4 File Offset: 0x000035B4
		[DataSourceProperty]
		public HintViewModel RoutedHint
		{
			get
			{
				return this._routedHint;
			}
			set
			{
				if (value != this._routedHint)
				{
					this._routedHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RoutedHint");
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000053D2 File Offset: 0x000035D2
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000053DA File Offset: 0x000035DA
		[DataSourceProperty]
		public HintViewModel RemainingHint
		{
			get
			{
				return this._remainingHint;
			}
			set
			{
				if (value != this._remainingHint)
				{
					this._remainingHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RemainingHint");
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000053F8 File Offset: 0x000035F8
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005400 File Offset: 0x00003600
		[DataSourceProperty]
		public int BattleResultIndex
		{
			get
			{
				return this._battleResultIndex;
			}
			set
			{
				if (value != this._battleResultIndex)
				{
					this._battleResultIndex = value;
					base.OnPropertyChangedWithValue(value, "BattleResultIndex");
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000541E File Offset: 0x0000361E
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005426 File Offset: 0x00003626
		[DataSourceProperty]
		public string BattleResult
		{
			get
			{
				return this._battleResult;
			}
			set
			{
				if (value != this._battleResult)
				{
					this._battleResult = value;
					base.OnPropertyChangedWithValue<string>(value, "BattleResult");
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005449 File Offset: 0x00003649
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00005451 File Offset: 0x00003651
		[DataSourceProperty]
		public bool IsMouseEnabled
		{
			get
			{
				return this._isMouseEnabled;
			}
			set
			{
				if (value != this._isMouseEnabled)
				{
					this._isMouseEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsMouseEnabled");
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000546F File Offset: 0x0000366F
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005477 File Offset: 0x00003677
		[DataSourceProperty]
		public bool IsOver
		{
			get
			{
				return this._isOver;
			}
			set
			{
				if (value != this._isOver)
				{
					this._isOver = value;
					base.OnPropertyChangedWithValue(value, "IsOver");
					this.UpdateQuitText();
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000549B File Offset: 0x0000369B
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000054A3 File Offset: 0x000036A3
		[DataSourceProperty]
		public bool IsFastForwarding
		{
			get
			{
				return this._isFastForwarding;
			}
			set
			{
				if (value != this._isFastForwarding)
				{
					this._isFastForwarding = value;
					base.OnPropertyChangedWithValue(value, "IsFastForwarding");
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000054C4 File Offset: 0x000036C4
		public virtual void SetShortcuts(ScoreboardHotkeys shortcuts)
		{
			this.ShowMouseKey = InputKeyItemVM.CreateFromGameKey(shortcuts.ShowMouseHotkey, false);
			this.ShowScoreboardKey = InputKeyItemVM.CreateFromGameKey(shortcuts.ShowScoreboardHotkey, false);
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(shortcuts.DoneInputKey, true);
			this.FastForwardKey = InputKeyItemVM.CreateFromHotKey(shortcuts.FastForwardKey, true);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005519 File Offset: 0x00003719
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005521 File Offset: 0x00003721
		[DataSourceProperty]
		public bool IsMainCharacterDead
		{
			get
			{
				return this._isMainCharacterDead;
			}
			set
			{
				if (value != this._isMainCharacterDead)
				{
					this._isMainCharacterDead = value;
					base.OnPropertyChangedWithValue(value, "IsMainCharacterDead");
					this.UpdateQuitText();
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005545 File Offset: 0x00003745
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000554D File Offset: 0x0000374D
		[DataSourceProperty]
		public bool IsSimulation
		{
			get
			{
				return this._isSimulation;
			}
			set
			{
				if (value != this._isSimulation)
				{
					this._isSimulation = value;
					base.OnPropertyChangedWithValue(value, "IsSimulation");
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000556B File Offset: 0x0000376B
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005573 File Offset: 0x00003773
		[DataSourceProperty]
		public PowerLevelComparer PowerComparer
		{
			get
			{
				return this._powerComparer;
			}
			set
			{
				if (value != this._powerComparer)
				{
					this._powerComparer = value;
					base.OnPropertyChangedWithValue<PowerLevelComparer>(value, "PowerComparer");
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005591 File Offset: 0x00003791
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005599 File Offset: 0x00003799
		[DataSourceProperty]
		public InputKeyItemVM ShowMouseKey
		{
			get
			{
				return this._showMouseKey;
			}
			set
			{
				if (value != this._showMouseKey)
				{
					this._showMouseKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ShowMouseKey");
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000055B7 File Offset: 0x000037B7
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000055BF File Offset: 0x000037BF
		[DataSourceProperty]
		public bool ShowScoreboard
		{
			get
			{
				return this._showScoreboard;
			}
			set
			{
				if (value != this._showScoreboard)
				{
					this._showScoreboard = value;
					base.OnPropertyChangedWithValue(value, "ShowScoreboard");
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000055DD File Offset: 0x000037DD
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000055E5 File Offset: 0x000037E5
		[DataSourceProperty]
		public InputKeyItemVM ShowScoreboardKey
		{
			get
			{
				return this._showScoreboardKey;
			}
			set
			{
				if (value != this._showScoreboardKey)
				{
					this._showScoreboardKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ShowScoreboardKey");
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005603 File Offset: 0x00003803
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000560B File Offset: 0x0000380B
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005629 File Offset: 0x00003829
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005631 File Offset: 0x00003831
		[DataSourceProperty]
		public InputKeyItemVM FastForwardKey
		{
			get
			{
				return this._fastForwardKey;
			}
			set
			{
				if (value != this._fastForwardKey)
				{
					this._fastForwardKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "FastForwardKey");
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000564F File Offset: 0x0000384F
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005652 File Offset: 0x00003852
		[DataSourceProperty]
		public virtual MBBindingList<BattleResultVM> BattleResults
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0400005C RID: 92
		protected Action OnFastForwardIncreaseSpeed;

		// Token: 0x0400005D RID: 93
		protected Action OnFastForwardDecreaseSpeed;

		// Token: 0x0400005E RID: 94
		protected Action OnFastForwardResetSpeed;

		// Token: 0x0400005F RID: 95
		private static readonly TextObject _hourAbbrString = GameTexts.FindText("str_hour_abbr", null);

		// Token: 0x04000060 RID: 96
		private static readonly TextObject _minuteAbbrString = GameTexts.FindText("str_minute_abbr", null);

		// Token: 0x04000061 RID: 97
		private static readonly TextObject _secondAbbrString = GameTexts.FindText("str_second_abbr", null);

		// Token: 0x04000062 RID: 98
		protected BattleSideEnum PlayerSide;

		// Token: 0x04000063 RID: 99
		protected IMissionScreen _missionScreen;

		// Token: 0x04000064 RID: 100
		protected Mission _mission;

		// Token: 0x04000065 RID: 101
		protected BattleEndLogic _battleEndLogic;

		// Token: 0x04000066 RID: 102
		protected InquiryData _retreatInquiryData;

		// Token: 0x04000067 RID: 103
		protected Action _releaseSimulationSources;

		// Token: 0x04000068 RID: 104
		protected Action<bool> OnToggle;

		// Token: 0x04000069 RID: 105
		private ScoreboardBaseVM.MouseState _mouseState;

		// Token: 0x0400006A RID: 106
		protected const float MissionEndScoreboardDelayTime = 1.5f;

		// Token: 0x0400006B RID: 107
		private string _quitText;

		// Token: 0x0400006C RID: 108
		private string _showScoreboardText;

		// Token: 0x0400006D RID: 109
		private string _fastForwardText;

		// Token: 0x0400006E RID: 110
		private bool _isFastForwarding;

		// Token: 0x0400006F RID: 111
		private bool _isMainCharacterDead;

		// Token: 0x04000070 RID: 112
		private bool _showScoreboard;

		// Token: 0x04000071 RID: 113
		private bool _isSimulation = true;

		// Token: 0x04000072 RID: 114
		private bool _isMouseEnabled;

		// Token: 0x04000073 RID: 115
		private PowerLevelComparer _powerComparer;

		// Token: 0x04000074 RID: 116
		private bool _isOver;

		// Token: 0x04000075 RID: 117
		private string _battleResult;

		// Token: 0x04000076 RID: 118
		private int _battleResultIndex = -1;

		// Token: 0x04000077 RID: 119
		private HintViewModel _killHint;

		// Token: 0x04000078 RID: 120
		private HintViewModel _upgradeHint;

		// Token: 0x04000079 RID: 121
		private HintViewModel _deadHint;

		// Token: 0x0400007A RID: 122
		private HintViewModel _woundedHint;

		// Token: 0x0400007B RID: 123
		private HintViewModel _routedHint;

		// Token: 0x0400007C RID: 124
		private HintViewModel _remainingHint;

		// Token: 0x0400007D RID: 125
		private SPScoreboardSideVM _attackers;

		// Token: 0x0400007E RID: 126
		private SPScoreboardSideVM _defenders;

		// Token: 0x0400007F RID: 127
		private bool _isPowerComparerEnabled;

		// Token: 0x04000080 RID: 128
		private int _missionTimeInSeconds;

		// Token: 0x04000081 RID: 129
		private string _missionTimeStr;

		// Token: 0x04000082 RID: 130
		private InputKeyItemVM _showMouseKey;

		// Token: 0x04000083 RID: 131
		private InputKeyItemVM _showScoreboardKey;

		// Token: 0x04000084 RID: 132
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000085 RID: 133
		private InputKeyItemVM _fastForwardKey;

		// Token: 0x0200007D RID: 125
		internal enum MouseState
		{
			// Token: 0x040004E8 RID: 1256
			NotVisible,
			// Token: 0x040004E9 RID: 1257
			Visible
		}

		// Token: 0x0200007E RID: 126
		public enum Categories
		{
			// Token: 0x040004EB RID: 1259
			Party,
			// Token: 0x040004EC RID: 1260
			Tactical,
			// Token: 0x040004ED RID: 1261
			NumOfCategories
		}

		// Token: 0x0200007F RID: 127
		protected enum BattleResultType
		{
			// Token: 0x040004EF RID: 1263
			NotOver = -1,
			// Token: 0x040004F0 RID: 1264
			Defeat,
			// Token: 0x040004F1 RID: 1265
			Victory,
			// Token: 0x040004F2 RID: 1266
			Retreat
		}
	}
}
