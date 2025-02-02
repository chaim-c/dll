using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Matchmaking
{
	// Token: 0x020000A5 RID: 165
	public class MultiplayerLobbyMatchmakingScreenWidget : Widget
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000193DF File Offset: 0x000175DF
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x000193E7 File Offset: 0x000175E7
		public MultiplayerLobbyCustomServerScreenWidget CustomServerParentWidget { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000193F0 File Offset: 0x000175F0
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x000193F8 File Offset: 0x000175F8
		public MultiplayerLobbyCustomServerScreenWidget PremadeMatchesParentWidget { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00019401 File Offset: 0x00017601
		private MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages _selectedMode
		{
			get
			{
				return (MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages)this.SelectedModeIndex;
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00019409 File Offset: 0x00017609
		public MultiplayerLobbyMatchmakingScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00019414 File Offset: 0x00017614
		public void LobbyStateChanged(bool isSearchRequested, bool isSearching, bool isMatchmakingEnabled, bool isCustomBattleEnabled, bool isPartyLeader, bool isInParty)
		{
			this._latestIsSearchRequested = isSearchRequested;
			this._latestIsSearching = isSearching;
			this._latestIsMatchmakingEnabled = isMatchmakingEnabled;
			this._latestIsCustomBattleEnabled = isCustomBattleEnabled;
			this._latestIsPartyLeader = isPartyLeader;
			this._latestIsInParty = isInParty;
			if (this.CustomServerParentWidget != null)
			{
				this.CustomServerParentWidget.IsInParty = isInParty;
				this.CustomServerParentWidget.IsPartyLeader = isPartyLeader;
			}
			if (this.PremadeMatchesParentWidget != null)
			{
				this.PremadeMatchesParentWidget.IsInParty = isInParty;
				this.PremadeMatchesParentWidget.IsPartyLeader = isPartyLeader;
			}
			this.UpdateStates();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00019498 File Offset: 0x00017698
		private void UpdateStates()
		{
			this.FindGameButton.IsEnabled = (((this._selectedMode != MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages.CustomGame && this._latestIsMatchmakingEnabled && this.IsMatchFindPossible) || (this._selectedMode == MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages.CustomGame && this.IsCustomGameFindEnabled)) && (this._latestIsPartyLeader || !this._latestIsInParty) && !this._latestIsSearchRequested);
			this.FindGameButton.IsVisible = (!this._latestIsSearching && ((this._latestIsCustomBattleEnabled && this._selectedMode == MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages.CustomGame) || (this._latestIsMatchmakingEnabled && this._selectedMode == MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages.QuickPlay)));
			this.SelectionInfo.IsEnabled = this._latestIsMatchmakingEnabled;
			this.SelectionInfo.IsVisible = (!this._latestIsSearching && !this._latestIsCustomBattleEnabled);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00019564 File Offset: 0x00017764
		private void OnSubpageIndexChange()
		{
			this.FindGameButton.IsVisible = (!this._latestIsSearching && ((this._latestIsCustomBattleEnabled && this._selectedMode == MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages.CustomGame) || (this._latestIsMatchmakingEnabled && this._selectedMode == MultiplayerLobbyMatchmakingScreenWidget.MatchmakingSubPages.QuickPlay)));
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000195A4 File Offset: 0x000177A4
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x000195AC File Offset: 0x000177AC
		[Editor(false)]
		public bool IsMatchFindPossible
		{
			get
			{
				return this._isMatchFindPossible;
			}
			set
			{
				if (this._isMatchFindPossible != value)
				{
					this._isMatchFindPossible = value;
					base.OnPropertyChanged(value, "IsMatchFindPossible");
					this.UpdateStates();
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000195D0 File Offset: 0x000177D0
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x000195D8 File Offset: 0x000177D8
		[Editor(false)]
		public bool IsCustomGameFindEnabled
		{
			get
			{
				return this._isCustomGameFindEnabled;
			}
			set
			{
				if (this._isCustomGameFindEnabled != value)
				{
					this._isCustomGameFindEnabled = value;
					base.OnPropertyChanged(value, "IsCustomGameFindEnabled");
					this.UpdateStates();
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000195FC File Offset: 0x000177FC
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00019604 File Offset: 0x00017804
		[Editor(false)]
		public int SelectedModeIndex
		{
			get
			{
				return this._selectedModeIndex;
			}
			set
			{
				if (this._selectedModeIndex != value)
				{
					this._selectedModeIndex = value;
					base.OnPropertyChanged(value, "SelectedModeIndex");
					this.OnSubpageIndexChange();
				}
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00019628 File Offset: 0x00017828
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00019630 File Offset: 0x00017830
		[Editor(false)]
		public ButtonWidget FindGameButton
		{
			get
			{
				return this._findGameButton;
			}
			set
			{
				if (this._findGameButton != value)
				{
					this._findGameButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "FindGameButton");
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001964E File Offset: 0x0001784E
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00019656 File Offset: 0x00017856
		[Editor(false)]
		public Widget SelectionInfo
		{
			get
			{
				return this._selectionInfo;
			}
			set
			{
				if (this._selectionInfo != value)
				{
					this._selectionInfo = value;
					base.OnPropertyChanged<Widget>(value, "SelectionInfo");
				}
			}
		}

		// Token: 0x04000401 RID: 1025
		private bool _latestIsSearchRequested;

		// Token: 0x04000402 RID: 1026
		private bool _latestIsSearching;

		// Token: 0x04000403 RID: 1027
		private bool _latestIsMatchmakingEnabled;

		// Token: 0x04000404 RID: 1028
		private bool _latestIsCustomBattleEnabled;

		// Token: 0x04000405 RID: 1029
		private bool _latestIsPartyLeader;

		// Token: 0x04000406 RID: 1030
		private bool _latestIsInParty;

		// Token: 0x04000407 RID: 1031
		private ButtonWidget _findGameButton;

		// Token: 0x04000408 RID: 1032
		private Widget _selectionInfo;

		// Token: 0x04000409 RID: 1033
		private int _selectedModeIndex;

		// Token: 0x0400040A RID: 1034
		private bool _isMatchFindPossible;

		// Token: 0x0400040B RID: 1035
		private bool _isCustomGameFindEnabled;

		// Token: 0x020001A5 RID: 421
		private enum MatchmakingSubPages
		{
			// Token: 0x04000992 RID: 2450
			QuickPlay,
			// Token: 0x04000993 RID: 2451
			CustomGame,
			// Token: 0x04000994 RID: 2452
			CustomGameList,
			// Token: 0x04000995 RID: 2453
			PremadeMatchList,
			// Token: 0x04000996 RID: 2454
			Default
		}
	}
}
