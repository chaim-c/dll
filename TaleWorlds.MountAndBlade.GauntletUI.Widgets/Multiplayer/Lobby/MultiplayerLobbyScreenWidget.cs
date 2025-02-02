using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Friend;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Matchmaking;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x020000A3 RID: 163
	public class MultiplayerLobbyScreenWidget : Widget
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00018EE0 File Offset: 0x000170E0
		public MultiplayerLobbyScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00018EE9 File Offset: 0x000170E9
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.OnLobbyStateChanged();
				this._initialized = true;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00018F08 File Offset: 0x00017108
		private void OnLoggedInChanged()
		{
			if (!this._isLoggedIn)
			{
				this._stateChangeLocked = true;
				this.IsSearchGameRequested = false;
				this.IsSearchingGame = false;
				this.IsCustomBattleEnabled = false;
				this.IsMatchmakingEnabled = false;
				this.IsPartyLeader = false;
				this.IsInParty = false;
				this._stateChangeLocked = false;
				this.OnLobbyStateChanged();
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00018F5C File Offset: 0x0001715C
		private void OnLobbyStateChanged()
		{
			if (!this._stateChangeLocked)
			{
				this.MenuWidget.LobbyStateChanged(this.IsSearchGameRequested, this.IsSearchingGame, this.IsMatchmakingEnabled, this.IsCustomBattleEnabled, this.IsPartyLeader, this.IsInParty);
				this.HomeScreenWidget.LobbyStateChanged(this.IsSearchGameRequested, this.IsSearchingGame, this.IsMatchmakingEnabled, this.IsCustomBattleEnabled, this.IsPartyLeader, this.IsInParty);
				this.MatchmakingScreenWidget.LobbyStateChanged(this.IsSearchGameRequested, this.IsSearchingGame, this.IsMatchmakingEnabled, this.IsCustomBattleEnabled, this.IsPartyLeader, this.IsInParty);
				this.ProfileScreenWidget.LobbyStateChanged(this.IsSearchGameRequested, this.IsSearchingGame, this.IsMatchmakingEnabled, this.IsCustomBattleEnabled, this.IsPartyLeader, this.IsInParty);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00019030 File Offset: 0x00017230
		private void HomeScreenWidgetPropertyChanged(PropertyOwnerObject owner, string property, bool value)
		{
			this.ToggleFriendListOnTabToggled(property, value);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001903A File Offset: 0x0001723A
		private void SocialScreenWidgetPropertyChanged(PropertyOwnerObject owner, string property, bool value)
		{
			this.ToggleFriendListOnTabToggled(property, value);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00019044 File Offset: 0x00017244
		private void ToggleFriendListOnTabToggled(string property, bool value)
		{
			if (this.FriendsPanelWidget != null && property == "IsVisible")
			{
				bool flag = value;
				if (!flag)
				{
					MultiplayerLobbyHomeScreenWidget homeScreenWidget = this.HomeScreenWidget;
					if (homeScreenWidget == null || !homeScreenWidget.IsVisible)
					{
						MultiplayerLobbyProfileScreenWidget profileScreenWidget = this.ProfileScreenWidget;
						if (profileScreenWidget == null || !profileScreenWidget.IsVisible)
						{
							goto IL_44;
						}
					}
					flag = true;
				}
				IL_44:
				this.FriendsPanelWidget.IsForcedOpen = flag;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000190A1 File Offset: 0x000172A1
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x000190A9 File Offset: 0x000172A9
		[Editor(false)]
		public bool IsLoggedIn
		{
			get
			{
				return this._isLoggedIn;
			}
			set
			{
				if (value != this._isLoggedIn)
				{
					this._isLoggedIn = value;
					base.OnPropertyChanged(value, "IsLoggedIn");
					this.OnLoggedInChanged();
				}
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000190CD File Offset: 0x000172CD
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x000190D5 File Offset: 0x000172D5
		[Editor(false)]
		public bool IsSearchGameRequested
		{
			get
			{
				return this._isSearchGameRequested;
			}
			set
			{
				if (this._isSearchGameRequested != value)
				{
					this._isSearchGameRequested = value;
					base.OnPropertyChanged(value, "IsSearchGameRequested");
					this.OnLobbyStateChanged();
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000190F9 File Offset: 0x000172F9
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00019101 File Offset: 0x00017301
		[Editor(false)]
		public bool IsSearchingGame
		{
			get
			{
				return this._isSearchingGame;
			}
			set
			{
				if (this._isSearchingGame != value)
				{
					this._isSearchingGame = value;
					base.OnPropertyChanged(value, "IsSearchingGame");
					this.OnLobbyStateChanged();
				}
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00019125 File Offset: 0x00017325
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001912D File Offset: 0x0001732D
		[Editor(false)]
		public bool IsCustomBattleEnabled
		{
			get
			{
				return this._isCustomBattleEnabled;
			}
			set
			{
				if (this._isCustomBattleEnabled != value)
				{
					this._isCustomBattleEnabled = value;
					base.OnPropertyChanged(value, "IsCustomBattleEnabled");
					this.OnLobbyStateChanged();
				}
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00019151 File Offset: 0x00017351
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00019159 File Offset: 0x00017359
		[Editor(false)]
		public bool IsMatchmakingEnabled
		{
			get
			{
				return this._isMatchmakingEnabled;
			}
			set
			{
				if (this._isMatchmakingEnabled != value)
				{
					this._isMatchmakingEnabled = value;
					base.OnPropertyChanged(value, "IsMatchmakingEnabled");
					this.OnLobbyStateChanged();
				}
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001917D File Offset: 0x0001737D
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00019185 File Offset: 0x00017385
		[Editor(false)]
		public bool IsPartyLeader
		{
			get
			{
				return this._isPartyLeader;
			}
			set
			{
				if (this._isPartyLeader != value)
				{
					this._isPartyLeader = value;
					base.OnPropertyChanged(value, "IsPartyLeader");
					this.OnLobbyStateChanged();
				}
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x000191A9 File Offset: 0x000173A9
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x000191B1 File Offset: 0x000173B1
		[Editor(false)]
		public bool IsInParty
		{
			get
			{
				return this._isInParty;
			}
			set
			{
				if (this._isInParty != value)
				{
					this._isInParty = value;
					base.OnPropertyChanged(value, "IsInParty");
					this.OnLobbyStateChanged();
				}
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000191D5 File Offset: 0x000173D5
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x000191DD File Offset: 0x000173DD
		[Editor(false)]
		public MultiplayerLobbyMenuWidget MenuWidget
		{
			get
			{
				return this._menuWidget;
			}
			set
			{
				if (this._menuWidget != value)
				{
					this._menuWidget = value;
					base.OnPropertyChanged<MultiplayerLobbyMenuWidget>(value, "MenuWidget");
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000191FB File Offset: 0x000173FB
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00019204 File Offset: 0x00017404
		[Editor(false)]
		public MultiplayerLobbyHomeScreenWidget HomeScreenWidget
		{
			get
			{
				return this._homeScreenWidget;
			}
			set
			{
				if (this._homeScreenWidget != value)
				{
					if (this._homeScreenWidget != null)
					{
						this._homeScreenWidget.boolPropertyChanged -= this.HomeScreenWidgetPropertyChanged;
					}
					this._homeScreenWidget = value;
					if (this._homeScreenWidget != null)
					{
						this._homeScreenWidget.boolPropertyChanged += this.HomeScreenWidgetPropertyChanged;
					}
					base.OnPropertyChanged<MultiplayerLobbyHomeScreenWidget>(value, "HomeScreenWidget");
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001926B File Offset: 0x0001746B
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00019273 File Offset: 0x00017473
		[Editor(false)]
		public MultiplayerLobbyMatchmakingScreenWidget MatchmakingScreenWidget
		{
			get
			{
				return this._matchmakingScreenWidget;
			}
			set
			{
				if (this._matchmakingScreenWidget != value)
				{
					this._matchmakingScreenWidget = value;
					base.OnPropertyChanged<MultiplayerLobbyMatchmakingScreenWidget>(value, "MatchmakingScreenWidget");
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00019291 File Offset: 0x00017491
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001929C File Offset: 0x0001749C
		[Editor(false)]
		public MultiplayerLobbyProfileScreenWidget ProfileScreenWidget
		{
			get
			{
				return this._profileScreenWidget;
			}
			set
			{
				if (this._profileScreenWidget != value)
				{
					if (this._profileScreenWidget != null)
					{
						this._profileScreenWidget.boolPropertyChanged -= this.SocialScreenWidgetPropertyChanged;
					}
					this._profileScreenWidget = value;
					if (this._profileScreenWidget != null)
					{
						this._profileScreenWidget.boolPropertyChanged += this.SocialScreenWidgetPropertyChanged;
					}
					base.OnPropertyChanged<MultiplayerLobbyProfileScreenWidget>(value, "ProfileScreenWidget");
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00019303 File Offset: 0x00017503
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0001930B File Offset: 0x0001750B
		[Editor(false)]
		public MultiplayerLobbyFriendsPanelWidget FriendsPanelWidget
		{
			get
			{
				return this._friendsPanelWidget;
			}
			set
			{
				if (this._friendsPanelWidget != value)
				{
					this._friendsPanelWidget = value;
					base.OnPropertyChanged<MultiplayerLobbyFriendsPanelWidget>(value, "FriendsPanelWidget");
				}
			}
		}

		// Token: 0x040003F0 RID: 1008
		private bool _initialized;

		// Token: 0x040003F1 RID: 1009
		private bool _stateChangeLocked;

		// Token: 0x040003F2 RID: 1010
		private bool _isLoggedIn;

		// Token: 0x040003F3 RID: 1011
		private bool _isSearchGameRequested;

		// Token: 0x040003F4 RID: 1012
		private bool _isSearchingGame;

		// Token: 0x040003F5 RID: 1013
		private bool _isMatchmakingEnabled;

		// Token: 0x040003F6 RID: 1014
		private bool _isPartyLeader;

		// Token: 0x040003F7 RID: 1015
		private bool _isInParty;

		// Token: 0x040003F8 RID: 1016
		private bool _isCustomBattleEnabled;

		// Token: 0x040003F9 RID: 1017
		private MultiplayerLobbyMenuWidget _menuWidget;

		// Token: 0x040003FA RID: 1018
		private MultiplayerLobbyHomeScreenWidget _homeScreenWidget;

		// Token: 0x040003FB RID: 1019
		private MultiplayerLobbyMatchmakingScreenWidget _matchmakingScreenWidget;

		// Token: 0x040003FC RID: 1020
		private MultiplayerLobbyFriendsPanelWidget _friendsPanelWidget;

		// Token: 0x040003FD RID: 1021
		private MultiplayerLobbyProfileScreenWidget _profileScreenWidget;
	}
}
