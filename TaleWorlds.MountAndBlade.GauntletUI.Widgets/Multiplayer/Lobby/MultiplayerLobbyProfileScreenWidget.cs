using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x020000A1 RID: 161
	public class MultiplayerLobbyProfileScreenWidget : Widget
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x00018D20 File Offset: 0x00016F20
		public MultiplayerLobbyProfileScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00018D2C File Offset: 0x00016F2C
		public void LobbyStateChanged(bool isSearchRequested, bool isSearching, bool isMatchmakingEnabled, bool isCustomBattleEnabled, bool isPartyLeader, bool isInParty)
		{
			this.FindGameButton.IsEnabled = (!this.HasUnofficialModulesLoaded && isMatchmakingEnabled && !isSearchRequested && (isPartyLeader || !isInParty));
			this.FindGameButton.IsVisible = (!this.HasUnofficialModulesLoaded && !isSearching);
			this.SelectionInfo.IsEnabled = (!this.HasUnofficialModulesLoaded && isMatchmakingEnabled);
			this.SelectionInfo.IsVisible = (!this.HasUnofficialModulesLoaded && !isSearching);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00018DAD File Offset: 0x00016FAD
		private void OnSubpageIndexChange()
		{
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00018DAF File Offset: 0x00016FAF
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x00018DB7 File Offset: 0x00016FB7
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

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00018DDB File Offset: 0x00016FDB
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00018DE3 File Offset: 0x00016FE3
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00018E01 File Offset: 0x00017001
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00018E09 File Offset: 0x00017009
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00018E27 File Offset: 0x00017027
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00018E2F File Offset: 0x0001702F
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

		// Token: 0x040003EA RID: 1002
		private ButtonWidget _findGameButton;

		// Token: 0x040003EB RID: 1003
		private Widget _selectionInfo;

		// Token: 0x040003EC RID: 1004
		private int _selectedModeIndex;

		// Token: 0x040003ED RID: 1005
		private bool _hasUnofficialModulesLoaded;
	}
}
