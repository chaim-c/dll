using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu
{
	// Token: 0x02000071 RID: 113
	public class GameTipsVM : ViewModel
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x00024F93 File Offset: 0x00023193
		public GameTipsVM(bool isAutoChangeEnabled, bool navigationButtonsEnabled)
		{
			this._navigationButtonsEnabled = navigationButtonsEnabled;
			this._isAutoChangeEnabled = isAutoChangeEnabled;
			this.RefreshValues();
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00024FBC File Offset: 0x000231BC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._allTips = new MBList<string>();
			this.GameTipTitle = GameTexts.FindText("str_game_tip_title", null).ToString();
			string keyHyperlinkText = HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 4));
			GameTexts.SetVariable("LEAVE_AREA_KEY", keyHyperlinkText);
			string keyHyperlinkText2 = HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 5));
			GameTexts.SetVariable("MISSION_INDICATORS_KEY", keyHyperlinkText2);
			GameTexts.SetVariable("EXTEND_KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("MapHotKeyCategory", "MapFollowModifier")));
			GameTexts.SetVariable("ENCYCLOPEDIA_SHORTCUT", HyperlinkTexts.GetKeyHyperlinkText("RightMouseButton"));
			if (Input.IsMouseActive)
			{
				foreach (TextObject textObject in GameTexts.FindAllTextVariations("str_game_tip_pc"))
				{
					this._allTips.Add(textObject.ToString());
				}
			}
			foreach (TextObject textObject2 in GameTexts.FindAllTextVariations("str_game_tip"))
			{
				this._allTips.Add(textObject2.ToString());
			}
			this.NavigationButtonsEnabled = (this._allTips.Count > 1);
			this.CurrentTip = ((this._allTips.Count == 0) ? string.Empty : this._allTips.GetRandomElement<string>());
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00025134 File Offset: 0x00023334
		public void ExecutePreviousTip()
		{
			this._currentTipIndex--;
			if (this._currentTipIndex < 0)
			{
				this._currentTipIndex = this._allTips.Count - 1;
			}
			this.CurrentTip = this._allTips[this._currentTipIndex];
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00025182 File Offset: 0x00023382
		public void ExecuteNextTip()
		{
			this._currentTipIndex = (this._currentTipIndex + 1) % this._allTips.Count;
			this.CurrentTip = this._allTips[this._currentTipIndex];
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000251B5 File Offset: 0x000233B5
		public void OnTick(float dt)
		{
			if (this._isAutoChangeEnabled)
			{
				this._totalDt += dt;
				if (this._totalDt > this._tipTimeInterval)
				{
					this.ExecuteNextTip();
					this._totalDt = 0f;
				}
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000251EC File Offset: 0x000233EC
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x000251F4 File Offset: 0x000233F4
		[DataSourceProperty]
		public string CurrentTip
		{
			get
			{
				return this._currentTip;
			}
			set
			{
				if (value != this._currentTip)
				{
					this._currentTip = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentTip");
				}
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00025217 File Offset: 0x00023417
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0002521F File Offset: 0x0002341F
		[DataSourceProperty]
		public string GameTipTitle
		{
			get
			{
				return this._gameTipTitle;
			}
			set
			{
				if (value != this._gameTipTitle)
				{
					this._gameTipTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "GameTipTitle");
				}
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00025242 File Offset: 0x00023442
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0002524A File Offset: 0x0002344A
		[DataSourceProperty]
		public bool NavigationButtonsEnabled
		{
			get
			{
				return this._navigationButtonsEnabled;
			}
			set
			{
				if (value != this._navigationButtonsEnabled)
				{
					this._navigationButtonsEnabled = value;
					base.OnPropertyChangedWithValue(value, "NavigationButtonsEnabled");
				}
			}
		}

		// Token: 0x0400047E RID: 1150
		private MBList<string> _allTips;

		// Token: 0x0400047F RID: 1151
		private readonly float _tipTimeInterval = 5f;

		// Token: 0x04000480 RID: 1152
		private readonly bool _isAutoChangeEnabled;

		// Token: 0x04000481 RID: 1153
		private int _currentTipIndex;

		// Token: 0x04000482 RID: 1154
		private float _totalDt;

		// Token: 0x04000483 RID: 1155
		private string _currentTip;

		// Token: 0x04000484 RID: 1156
		private string _gameTipTitle;

		// Token: 0x04000485 RID: 1157
		private bool _navigationButtonsEnabled;
	}
}
