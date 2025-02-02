using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x020000A2 RID: 162
	public class MultiplayerLobbyRankItemButtonWidget : ButtonWidget
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x00018E4D File Offset: 0x0001704D
		public MultiplayerLobbyRankItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00018E58 File Offset: 0x00017058
		private void UpdateSprite()
		{
			string str = "unranked";
			if (this.RankID != string.Empty)
			{
				str = this.RankID;
			}
			base.Brush.DefaultLayer.Sprite = base.Context.SpriteData.GetSprite("MPGeneral\\MPRanks\\" + str);
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00018EAF File Offset: 0x000170AF
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00018EB7 File Offset: 0x000170B7
		[Editor(false)]
		public string RankID
		{
			get
			{
				return this._rankID;
			}
			set
			{
				if (value != this._rankID)
				{
					this._rankID = value;
					base.OnPropertyChanged<string>(value, "RankID");
					this.UpdateSprite();
				}
			}
		}

		// Token: 0x040003EE RID: 1006
		private const string _defaultRankID = "unranked";

		// Token: 0x040003EF RID: 1007
		private string _rankID;
	}
}
