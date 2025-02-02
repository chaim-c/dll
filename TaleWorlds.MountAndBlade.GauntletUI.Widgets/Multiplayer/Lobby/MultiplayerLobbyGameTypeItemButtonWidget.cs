using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x0200009D RID: 157
	public class MultiplayerLobbyGameTypeItemButtonWidget : ButtonWidget
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x00018A27 File Offset: 0x00016C27
		public MultiplayerLobbyGameTypeItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00018A30 File Offset: 0x00016C30
		private void UpdateSprite()
		{
			base.Brush.DefaultLayer.Sprite = base.Context.SpriteData.GetSprite("MPLobby\\GameTypes\\" + this.GameTypeID);
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00018A62 File Offset: 0x00016C62
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00018A6A File Offset: 0x00016C6A
		[Editor(false)]
		public string GameTypeID
		{
			get
			{
				return this._gameTypeID;
			}
			set
			{
				if (value != this._gameTypeID)
				{
					this._gameTypeID = value;
					base.OnPropertyChanged<string>(value, "GameTypeID");
					this.UpdateSprite();
				}
			}
		}

		// Token: 0x040003DF RID: 991
		private string _gameTypeID;
	}
}
