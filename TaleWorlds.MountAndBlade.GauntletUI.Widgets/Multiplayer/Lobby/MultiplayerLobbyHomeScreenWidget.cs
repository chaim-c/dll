using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x0200009E RID: 158
	public class MultiplayerLobbyHomeScreenWidget : Widget
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00018A93 File Offset: 0x00016C93
		public MultiplayerLobbyHomeScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00018A9C File Offset: 0x00016C9C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				if (base.IsVisible)
				{
					base.OnPropertyChanged(true, "IsVisible");
				}
				this._initialized = true;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public void LobbyStateChanged(bool isSearchRequested, bool isSearching, bool isMatchmakingEnabled, bool isCustomBattleEnabled, bool isPartyLeader, bool isInParty)
		{
			this.FindGameButton.IsEnabled = (!this.HasUnofficialModulesLoaded && isMatchmakingEnabled && !isSearchRequested && (isPartyLeader || !isInParty));
			this.FindGameButton.IsVisible = (!this.HasUnofficialModulesLoaded && !isSearching);
			this.SelectionInfo.IsEnabled = (!this.HasUnofficialModulesLoaded && isMatchmakingEnabled);
			this.SelectionInfo.IsVisible = (!this.HasUnofficialModulesLoaded && !isSearching);
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00018B49 File Offset: 0x00016D49
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x00018B51 File Offset: 0x00016D51
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00018B6F File Offset: 0x00016D6F
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00018B77 File Offset: 0x00016D77
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00018B95 File Offset: 0x00016D95
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x00018B9D File Offset: 0x00016D9D
		[Editor(false)]
		public bool HasUnofficialModulesLoaded
		{
			get
			{
				return this._hasUnofficialModulesLoaded;
			}
			set
			{
				if (value != this._hasUnofficialModulesLoaded)
				{
					this._hasUnofficialModulesLoaded = value;
					base.OnPropertyChanged(value, "HasUnofficialModulesLoaded");
				}
			}
		}

		// Token: 0x040003E0 RID: 992
		private bool _initialized;

		// Token: 0x040003E1 RID: 993
		private ButtonWidget _findGameButton;

		// Token: 0x040003E2 RID: 994
		private Widget _selectionInfo;

		// Token: 0x040003E3 RID: 995
		private bool _hasUnofficialModulesLoaded;
	}
}
