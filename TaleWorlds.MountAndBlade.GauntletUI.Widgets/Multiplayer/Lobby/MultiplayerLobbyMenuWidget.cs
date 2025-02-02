using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x020000A0 RID: 160
	public class MultiplayerLobbyMenuWidget : Widget
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x00018C6A File Offset: 0x00016E6A
		public MultiplayerLobbyMenuWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00018C73 File Offset: 0x00016E73
		public void LobbyStateChanged(bool isSearchRequested, bool isSearching, bool isMatchmakingEnabled, bool isCustomBattleEnabled, bool isPartyLeader, bool isInParty)
		{
			this.MatchmakingButtonWidget.IsEnabled = (isMatchmakingEnabled || isCustomBattleEnabled);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00018C84 File Offset: 0x00016E84
		private void SelectedItemIndexChanged()
		{
			if (this.MenuItemListPanel == null)
			{
				return;
			}
			this.MenuItemListPanel.IntValue = this.SelectedItemIndex - 3;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00018CA2 File Offset: 0x00016EA2
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00018CAA File Offset: 0x00016EAA
		[Editor(false)]
		public int SelectedItemIndex
		{
			get
			{
				return this._selectedItemIndex;
			}
			set
			{
				if (this._selectedItemIndex != value)
				{
					this._selectedItemIndex = value;
					base.OnPropertyChanged(value, "SelectedItemIndex");
					this.SelectedItemIndexChanged();
				}
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00018CCE File Offset: 0x00016ECE
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00018CD6 File Offset: 0x00016ED6
		[Editor(false)]
		public ListPanel MenuItemListPanel
		{
			get
			{
				return this._menuItemListPanel;
			}
			set
			{
				if (this._menuItemListPanel != value)
				{
					this._menuItemListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "MenuItemListPanel");
					this.SelectedItemIndexChanged();
				}
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00018CFA File Offset: 0x00016EFA
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00018D02 File Offset: 0x00016F02
		[Editor(false)]
		public ButtonWidget MatchmakingButtonWidget
		{
			get
			{
				return this._matchmakingButtonWidget;
			}
			set
			{
				if (this._matchmakingButtonWidget != value)
				{
					this._matchmakingButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "MatchmakingButtonWidget");
				}
			}
		}

		// Token: 0x040003E7 RID: 999
		private int _selectedItemIndex;

		// Token: 0x040003E8 RID: 1000
		private ListPanel _menuItemListPanel;

		// Token: 0x040003E9 RID: 1001
		private ButtonWidget _matchmakingButtonWidget;
	}
}
