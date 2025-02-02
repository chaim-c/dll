using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Friend
{
	// Token: 0x020000A7 RID: 167
	public class MultiplayerLobbyFriendGroupToggleWidget : ToggleButtonWidget
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x00019775 File Offset: 0x00017975
		public MultiplayerLobbyFriendGroupToggleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001977E File Offset: 0x0001797E
		protected override void OnClick(Widget widget)
		{
			base.OnClick(widget);
			this.UpdateCollapseIndicator();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001978D File Offset: 0x0001798D
		protected override void RefreshState()
		{
			base.RefreshState();
			Widget titleContainer = this.TitleContainer;
			if (titleContainer == null)
			{
				return;
			}
			titleContainer.SetState(base.CurrentState);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000197AB File Offset: 0x000179AB
		private void CollapseIndicatorUpdated()
		{
			this.CollapseIndicator.AddState("Collapsed");
			this.CollapseIndicator.AddState("Expanded");
			this.UpdateCollapseIndicator();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000197D3 File Offset: 0x000179D3
		private void UpdateCollapseIndicator()
		{
			if (base.WidgetToClose != null && this.CollapseIndicator != null)
			{
				if (base.WidgetToClose.IsVisible)
				{
					this.CollapseIndicator.SetState("Expanded");
					return;
				}
				this.CollapseIndicator.SetState("Collapsed");
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00019813 File Offset: 0x00017A13
		private void PlayerCountUpdated()
		{
			if (this.PlayerCountText == null)
			{
				return;
			}
			this.PlayerCountText.Text = "(" + this.PlayerCount + ")";
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00019843 File Offset: 0x00017A43
		private void InitialClosedStateUpdated()
		{
			base.IsSelected = !this.InitialClosedState;
			this.CollapseIndicatorUpdated();
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001985A File Offset: 0x00017A5A
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00019862 File Offset: 0x00017A62
		[Editor(false)]
		public Widget CollapseIndicator
		{
			get
			{
				return this._collapseIndicator;
			}
			set
			{
				if (this._collapseIndicator != value)
				{
					this._collapseIndicator = value;
					base.OnPropertyChanged<Widget>(value, "CollapseIndicator");
					this.CollapseIndicatorUpdated();
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00019886 File Offset: 0x00017A86
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0001988E File Offset: 0x00017A8E
		[Editor(false)]
		public Widget TitleContainer
		{
			get
			{
				return this._titleContainer;
			}
			set
			{
				if (this._titleContainer != value)
				{
					this._titleContainer = value;
					base.OnPropertyChanged<Widget>(value, "TitleContainer");
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000198AC File Offset: 0x00017AAC
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x000198B4 File Offset: 0x00017AB4
		[Editor(false)]
		public TextWidget PlayerCountText
		{
			get
			{
				return this._playerCountText;
			}
			set
			{
				if (this._playerCountText != value)
				{
					this._playerCountText = value;
					base.OnPropertyChanged<TextWidget>(value, "PlayerCountText");
				}
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000198D2 File Offset: 0x00017AD2
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x000198DA File Offset: 0x00017ADA
		[Editor(false)]
		public int PlayerCount
		{
			get
			{
				return this._playerCount;
			}
			set
			{
				if (this._playerCount != value)
				{
					this._playerCount = value;
					base.OnPropertyChanged(value, "PlayerCount");
					this.PlayerCountUpdated();
				}
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000198FE File Offset: 0x00017AFE
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00019906 File Offset: 0x00017B06
		[Editor(false)]
		public bool InitialClosedState
		{
			get
			{
				return this._initialClosedState;
			}
			set
			{
				if (this._initialClosedState != value)
				{
					this._initialClosedState = value;
					base.OnPropertyChanged(value, "InitialClosedState");
					this.InitialClosedStateUpdated();
				}
			}
		}

		// Token: 0x0400040E RID: 1038
		private Widget _collapseIndicator;

		// Token: 0x0400040F RID: 1039
		private Widget _titleContainer;

		// Token: 0x04000410 RID: 1040
		private TextWidget _playerCountText;

		// Token: 0x04000411 RID: 1041
		private int _playerCount;

		// Token: 0x04000412 RID: 1042
		private bool _initialClosedState;
	}
}
