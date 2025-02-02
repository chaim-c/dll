using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x0200009A RID: 154
	public class MultiplayerLobbyCustomServerScreenWidget : Widget
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001812C File Offset: 0x0001632C
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x00018134 File Offset: 0x00016334
		public NavigationScopeTargeter FilterSearchBarScope { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001813D File Offset: 0x0001633D
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x00018145 File Offset: 0x00016345
		public NavigationScopeTargeter FilterButtonsScope { get; set; }

		// Token: 0x0600083E RID: 2110 RVA: 0x0001814E File Offset: 0x0001634E
		public MultiplayerLobbyCustomServerScreenWidget(UIContext context) : base(context)
		{
			this._createGameClickHandler = new Action<Widget>(this.OnCreateGameClick);
			this._closeCreatePanelClickHandler = new Action<Widget>(this.OnCloseCreatePanelClick);
			this._isCreateGamePanelActive = false;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00018182 File Offset: 0x00016382
		private void OnCreateGameClick(Widget widget)
		{
			this._isCreateGamePanelActive = true;
			this.UpdatePanels();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00018191 File Offset: 0x00016391
		private void OnCloseCreatePanelClick(Widget widget)
		{
			this._isCreateGamePanelActive = false;
			this.UpdatePanels();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000181A0 File Offset: 0x000163A0
		private void UpdatePanels()
		{
			this.JoinGamePanel.IsVisible = !this._isCreateGamePanelActive;
			this.CreateGameButton.IsVisible = !this._isCreateGamePanelActive;
			this.CreateGamePanel.IsVisible = this._isCreateGamePanelActive;
			this.CloseCreatePanelButton.IsVisible = this._isCreateGamePanelActive;
			this.RefreshButton.IsVisible = !this._isCreateGamePanelActive;
			this.ServerCountText.IsVisible = !this._isCreateGamePanelActive;
			this.InfoText.IsVisible = !this._isCreateGamePanelActive;
			this.JoinServerButton.IsVisible = !this._isCreateGamePanelActive;
			this.HostServerButton.IsVisible = this._isCreateGamePanelActive;
			this.FiltersPanel.SetState(this._isCreateGamePanelActive ? "Disabled" : "Default");
			if (this.FilterSearchBarScope != null)
			{
				this.FilterSearchBarScope.IsScopeDisabled = this._isCreateGamePanelActive;
			}
			if (this.FilterButtonsScope != null)
			{
				this.FilterButtonsScope.IsScopeDisabled = this._isCreateGamePanelActive;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000182A9 File Offset: 0x000164A9
		private void FiltersPanelUpdated()
		{
			this.FiltersPanel.AddState("Disabled");
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000182BB File Offset: 0x000164BB
		private void ServerListItemsChanged(Widget widget)
		{
			this.ServerCountText.IntText = this.ServerListPanel.ChildCount;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000182D3 File Offset: 0x000164D3
		private void ServerListItemsChanged(Widget parentWidget, Widget addedWidget)
		{
			this.ServerCountText.IntText = this.ServerListPanel.ChildCount;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000182EB File Offset: 0x000164EB
		private void ServerSelectionChanged(Widget child)
		{
			this.OnUpdateJoinServerEnabled();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000182F3 File Offset: 0x000164F3
		private void OnUpdateJoinServerEnabled()
		{
			if (this.JoinServerButton != null && this.ServerListPanel != null)
			{
				this.JoinServerButton.IsEnabled = (this.IsAnyGameSelected && (this.IsPartyLeader || !this.IsInParty));
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00018330 File Offset: 0x00016530
		private void OnUpdateCreateServerEnabled()
		{
			bool flag = (this.IsPlayerBasedCustomBattleEnabled || this.IsPremadeGameEnabled) && (this.IsPartyLeader || !this.IsInParty);
			if (this.CreateGameButton != null && this.ServerListPanel != null)
			{
				this.CreateGameButton.IsVisible = flag;
			}
			if (this.CreateGamePanel.IsVisible && !flag)
			{
				this._isCreateGamePanelActive = false;
				this.UpdatePanels();
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001839E File Offset: 0x0001659E
		private void RefreshClicked(Widget widget)
		{
			this.JoinServerButton.IsEnabled = false;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000183AC File Offset: 0x000165AC
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x000183B4 File Offset: 0x000165B4
		[Editor(false)]
		public ListPanel ServerListPanel
		{
			get
			{
				return this._serverListPanel;
			}
			set
			{
				if (this._serverListPanel != value)
				{
					ListPanel serverListPanel = this._serverListPanel;
					if (serverListPanel != null)
					{
						serverListPanel.ItemAddEventHandlers.Remove(new Action<Widget, Widget>(this.ServerListItemsChanged));
					}
					ListPanel serverListPanel2 = this._serverListPanel;
					if (serverListPanel2 != null)
					{
						serverListPanel2.ItemAfterRemoveEventHandlers.Remove(new Action<Widget>(this.ServerListItemsChanged));
					}
					ListPanel serverListPanel3 = this._serverListPanel;
					if (serverListPanel3 != null)
					{
						serverListPanel3.SelectEventHandlers.Remove(new Action<Widget>(this.ServerSelectionChanged));
					}
					this._serverListPanel = value;
					ListPanel serverListPanel4 = this._serverListPanel;
					if (serverListPanel4 != null)
					{
						serverListPanel4.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.ServerListItemsChanged));
					}
					ListPanel serverListPanel5 = this._serverListPanel;
					if (serverListPanel5 != null)
					{
						serverListPanel5.ItemAfterRemoveEventHandlers.Add(new Action<Widget>(this.ServerListItemsChanged));
					}
					ListPanel serverListPanel6 = this._serverListPanel;
					if (serverListPanel6 != null)
					{
						serverListPanel6.SelectEventHandlers.Add(new Action<Widget>(this.ServerSelectionChanged));
					}
					base.OnPropertyChanged<ListPanel>(value, "ServerListPanel");
				}
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x000184AF File Offset: 0x000166AF
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x000184B7 File Offset: 0x000166B7
		[Editor(false)]
		public ButtonWidget JoinServerButton
		{
			get
			{
				return this._joinServerButton;
			}
			set
			{
				if (this._joinServerButton != value)
				{
					this._joinServerButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "JoinServerButton");
				}
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000184D5 File Offset: 0x000166D5
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x000184DD File Offset: 0x000166DD
		[Editor(false)]
		public ButtonWidget HostServerButton
		{
			get
			{
				return this._hostServerButton;
			}
			set
			{
				if (this._hostServerButton != value)
				{
					this._hostServerButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "HostServerButton");
				}
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000184FB File Offset: 0x000166FB
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00018504 File Offset: 0x00016704
		[Editor(false)]
		public ButtonWidget CreateGameButton
		{
			get
			{
				return this._createGameButton;
			}
			set
			{
				if (this._createGameButton != value)
				{
					ButtonWidget createGameButton = this._createGameButton;
					if (createGameButton != null)
					{
						createGameButton.ClickEventHandlers.Remove(this._createGameClickHandler);
					}
					this._createGameButton = value;
					ButtonWidget createGameButton2 = this._createGameButton;
					if (createGameButton2 != null)
					{
						createGameButton2.ClickEventHandlers.Add(this._createGameClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "CreateGameButton");
				}
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00018566 File Offset: 0x00016766
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00018570 File Offset: 0x00016770
		[Editor(false)]
		public ButtonWidget CloseCreatePanelButton
		{
			get
			{
				return this._closeCreatePanelButton;
			}
			set
			{
				if (this._closeCreatePanelButton != value)
				{
					ButtonWidget closeCreatePanelButton = this._closeCreatePanelButton;
					if (closeCreatePanelButton != null)
					{
						closeCreatePanelButton.ClickEventHandlers.Remove(this._closeCreatePanelClickHandler);
					}
					this._closeCreatePanelButton = value;
					ButtonWidget closeCreatePanelButton2 = this._closeCreatePanelButton;
					if (closeCreatePanelButton2 != null)
					{
						closeCreatePanelButton2.ClickEventHandlers.Add(this._closeCreatePanelClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "CloseCreatePanelButton");
				}
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000185D2 File Offset: 0x000167D2
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x000185DA File Offset: 0x000167DA
		[Editor(false)]
		public Widget JoinGamePanel
		{
			get
			{
				return this._joinGamePanel;
			}
			set
			{
				if (this._joinGamePanel != value)
				{
					this._joinGamePanel = value;
					base.OnPropertyChanged<Widget>(value, "JoinGamePanel");
				}
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000185F8 File Offset: 0x000167F8
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00018600 File Offset: 0x00016800
		[Editor(false)]
		public Widget CreateGamePanel
		{
			get
			{
				return this._createGamePanel;
			}
			set
			{
				if (this._createGamePanel != value)
				{
					this._createGamePanel = value;
					base.OnPropertyChanged<Widget>(value, "CreateGamePanel");
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001861E File Offset: 0x0001681E
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00018628 File Offset: 0x00016828
		[Editor(false)]
		public ButtonWidget RefreshButton
		{
			get
			{
				return this._refreshButton;
			}
			set
			{
				if (this._refreshButton != value)
				{
					ButtonWidget refreshButton = this._refreshButton;
					if (refreshButton != null)
					{
						refreshButton.ClickEventHandlers.Remove(new Action<Widget>(this.RefreshClicked));
					}
					this._refreshButton = value;
					ButtonWidget refreshButton2 = this._refreshButton;
					if (refreshButton2 != null)
					{
						refreshButton2.ClickEventHandlers.Add(new Action<Widget>(this.RefreshClicked));
					}
					base.OnPropertyChanged<ButtonWidget>(value, "RefreshButton");
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00018696 File Offset: 0x00016896
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0001869E File Offset: 0x0001689E
		[Editor(false)]
		public Widget FiltersPanel
		{
			get
			{
				return this._filtersPanel;
			}
			set
			{
				if (this._filtersPanel != value)
				{
					this._filtersPanel = value;
					base.OnPropertyChanged<Widget>(value, "FiltersPanel");
					this.FiltersPanelUpdated();
				}
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000186C2 File Offset: 0x000168C2
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x000186CA File Offset: 0x000168CA
		[Editor(false)]
		public TextWidget ServerCountText
		{
			get
			{
				return this._serverCountText;
			}
			set
			{
				if (this._serverCountText != value)
				{
					this._serverCountText = value;
					base.OnPropertyChanged<TextWidget>(value, "ServerCountText");
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000186E8 File Offset: 0x000168E8
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000186F0 File Offset: 0x000168F0
		[Editor(false)]
		public TextWidget InfoText
		{
			get
			{
				return this._infoText;
			}
			set
			{
				if (this._infoText != value)
				{
					this._infoText = value;
					base.OnPropertyChanged<TextWidget>(value, "InfoText");
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001870E File Offset: 0x0001690E
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00018716 File Offset: 0x00016916
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
					this.OnUpdateJoinServerEnabled();
					this.OnUpdateCreateServerEnabled();
				}
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00018740 File Offset: 0x00016940
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00018748 File Offset: 0x00016948
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
					this.OnUpdateJoinServerEnabled();
					this.OnUpdateCreateServerEnabled();
				}
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00018772 File Offset: 0x00016972
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0001877A File Offset: 0x0001697A
		[Editor(false)]
		public bool IsAnyGameSelected
		{
			get
			{
				return this._isAnyGameSelected;
			}
			set
			{
				if (this._isAnyGameSelected != value)
				{
					this._isAnyGameSelected = value;
					base.OnPropertyChanged(value, "IsAnyGameSelected");
					this.OnUpdateJoinServerEnabled();
				}
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001879E File Offset: 0x0001699E
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x000187A6 File Offset: 0x000169A6
		[Editor(false)]
		public bool IsPlayerBasedCustomBattleEnabled
		{
			get
			{
				return this._isPlayerBasedCustomBattleEnabled;
			}
			set
			{
				if (!value && this._isCreateGamePanelActive)
				{
					this._isCreateGamePanelActive = false;
					this.UpdatePanels();
				}
				if (this._isPlayerBasedCustomBattleEnabled != value)
				{
					this._isPlayerBasedCustomBattleEnabled = value;
					base.OnPropertyChanged(value, "IsPlayerBasedCustomBattleEnabled");
				}
				this.OnUpdateCreateServerEnabled();
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000187E2 File Offset: 0x000169E2
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x000187EA File Offset: 0x000169EA
		[Editor(false)]
		public bool IsPremadeGameEnabled
		{
			get
			{
				return this._isPremadeGameEnabled;
			}
			set
			{
				if (!value && this._isCreateGamePanelActive)
				{
					this._isCreateGamePanelActive = false;
					this.UpdatePanels();
				}
				if (this._isPremadeGameEnabled != value)
				{
					this._isPremadeGameEnabled = value;
					base.OnPropertyChanged(value, "IsPremadeGameEnabled");
				}
				this.OnUpdateCreateServerEnabled();
			}
		}

		// Token: 0x040003C6 RID: 966
		private readonly Action<Widget> _createGameClickHandler;

		// Token: 0x040003C7 RID: 967
		private readonly Action<Widget> _closeCreatePanelClickHandler;

		// Token: 0x040003C8 RID: 968
		private bool _isCreateGamePanelActive;

		// Token: 0x040003CB RID: 971
		private ListPanel _serverListPanel;

		// Token: 0x040003CC RID: 972
		private ButtonWidget _joinServerButton;

		// Token: 0x040003CD RID: 973
		private ButtonWidget _hostServerButton;

		// Token: 0x040003CE RID: 974
		private ButtonWidget _createGameButton;

		// Token: 0x040003CF RID: 975
		private ButtonWidget _closeCreatePanelButton;

		// Token: 0x040003D0 RID: 976
		private Widget _joinGamePanel;

		// Token: 0x040003D1 RID: 977
		private Widget _createGamePanel;

		// Token: 0x040003D2 RID: 978
		private ButtonWidget _refreshButton;

		// Token: 0x040003D3 RID: 979
		private Widget _filtersPanel;

		// Token: 0x040003D4 RID: 980
		private TextWidget _serverCountText;

		// Token: 0x040003D5 RID: 981
		private TextWidget _infoText;

		// Token: 0x040003D6 RID: 982
		private bool _isPlayerBasedCustomBattleEnabled;

		// Token: 0x040003D7 RID: 983
		private bool _isPremadeGameEnabled;

		// Token: 0x040003D8 RID: 984
		private bool _isPartyLeader;

		// Token: 0x040003D9 RID: 985
		private bool _isInParty;

		// Token: 0x040003DA RID: 986
		private bool _isAnyGameSelected;
	}
}
