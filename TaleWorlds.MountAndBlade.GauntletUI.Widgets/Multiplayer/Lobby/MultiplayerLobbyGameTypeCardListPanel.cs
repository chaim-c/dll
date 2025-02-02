using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x0200009C RID: 156
	public class MultiplayerLobbyGameTypeCardListPanel : ListPanel
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x00018A13 File Offset: 0x00016C13
		public MultiplayerLobbyGameTypeCardListPanel(UIContext context) : base(context)
		{
			this._cardButtons = new List<MultiplayerLobbyGameTypeCardButtonWidget>();
		}

		// Token: 0x040003DE RID: 990
		private List<MultiplayerLobbyGameTypeCardButtonWidget> _cardButtons;
	}
}
