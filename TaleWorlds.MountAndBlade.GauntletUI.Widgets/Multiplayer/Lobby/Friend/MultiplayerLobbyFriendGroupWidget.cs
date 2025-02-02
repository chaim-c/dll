using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Friend
{
	// Token: 0x020000A8 RID: 168
	public class MultiplayerLobbyFriendGroupWidget : Widget
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0001992A File Offset: 0x00017B2A
		public MultiplayerLobbyFriendGroupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00019933 File Offset: 0x00017B33
		private void FriendCountChanged(Widget widget)
		{
			this.Toggle.PlayerCount = this.List.ChildCount;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001994B File Offset: 0x00017B4B
		private void FriendCountChanged(Widget parentWidget, Widget addedWidget)
		{
			this.Toggle.PlayerCount = this.List.ChildCount;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00019963 File Offset: 0x00017B63
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0001996C File Offset: 0x00017B6C
		[Editor(false)]
		public ListPanel List
		{
			get
			{
				return this._list;
			}
			set
			{
				if (this._list != value)
				{
					ListPanel list = this._list;
					if (list != null)
					{
						list.ItemAddEventHandlers.Remove(new Action<Widget, Widget>(this.FriendCountChanged));
					}
					ListPanel list2 = this._list;
					if (list2 != null)
					{
						list2.ItemAfterRemoveEventHandlers.Remove(new Action<Widget>(this.FriendCountChanged));
					}
					this._list = value;
					ListPanel list3 = this._list;
					if (list3 != null)
					{
						list3.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.FriendCountChanged));
					}
					ListPanel list4 = this._list;
					if (list4 != null)
					{
						list4.ItemAfterRemoveEventHandlers.Add(new Action<Widget>(this.FriendCountChanged));
					}
					base.OnPropertyChanged<ListPanel>(value, "List");
				}
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00019A22 File Offset: 0x00017C22
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x00019A2A File Offset: 0x00017C2A
		[Editor(false)]
		public MultiplayerLobbyFriendGroupToggleWidget Toggle
		{
			get
			{
				return this._toggle;
			}
			set
			{
				if (this._toggle != value)
				{
					this._toggle = value;
					base.OnPropertyChanged<MultiplayerLobbyFriendGroupToggleWidget>(value, "Toggle");
				}
			}
		}

		// Token: 0x04000413 RID: 1043
		private ListPanel _list;

		// Token: 0x04000414 RID: 1044
		private MultiplayerLobbyFriendGroupToggleWidget _toggle;
	}
}
