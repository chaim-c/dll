using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Friend
{
	// Token: 0x020000A9 RID: 169
	public class MultiplayerLobbyFriendsPanelWidget : Widget
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x00019A48 File Offset: 0x00017C48
		public MultiplayerLobbyFriendsPanelWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00019A51 File Offset: 0x00017C51
		private void OnShowListTogglePropertyChanged(PropertyOwnerObject owner, string propertyName, bool value)
		{
			if (propertyName == "IsSelected")
			{
				this.FriendsListPanel.IsVisible = this.ShowListToggle.IsSelected;
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00019A76 File Offset: 0x00017C76
		private void IsForcedOpenUpdated()
		{
			this.FriendsListPanel.IsVisible = this.IsForcedOpen;
			this.ShowListToggle.IsSelected = this.IsForcedOpen;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00019A9A File Offset: 0x00017C9A
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00019AA2 File Offset: 0x00017CA2
		[Editor(false)]
		public bool IsForcedOpen
		{
			get
			{
				return this._isForcedOpen;
			}
			set
			{
				if (this._isForcedOpen != value)
				{
					this._isForcedOpen = value;
					base.OnPropertyChanged(value, "IsForcedOpen");
					this.IsForcedOpenUpdated();
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00019AC6 File Offset: 0x00017CC6
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00019ACE File Offset: 0x00017CCE
		[Editor(false)]
		public Widget FriendsListPanel
		{
			get
			{
				return this._friendsListPanel;
			}
			set
			{
				if (this._friendsListPanel != value)
				{
					this._friendsListPanel = value;
					base.OnPropertyChanged<Widget>(value, "FriendsListPanel");
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00019AEC File Offset: 0x00017CEC
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00019AF4 File Offset: 0x00017CF4
		[Editor(false)]
		public ToggleStateButtonWidget ShowListToggle
		{
			get
			{
				return this._showListToggle;
			}
			set
			{
				if (this._showListToggle != value)
				{
					if (this._showListToggle != null)
					{
						this._showListToggle.boolPropertyChanged -= this.OnShowListTogglePropertyChanged;
					}
					this._showListToggle = value;
					if (this._showListToggle != null)
					{
						this._showListToggle.boolPropertyChanged += this.OnShowListTogglePropertyChanged;
					}
					base.OnPropertyChanged<ToggleStateButtonWidget>(value, "ShowListToggle");
				}
			}
		}

		// Token: 0x04000415 RID: 1045
		private bool _isForcedOpen;

		// Token: 0x04000416 RID: 1046
		private Widget _friendsListPanel;

		// Token: 0x04000417 RID: 1047
		private ToggleStateButtonWidget _showListToggle;
	}
}
