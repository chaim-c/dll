using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x0200008E RID: 142
	public class MultiplayerScoreboardStripedBackgroundWidget : MultiplayerScoreboardStatsListPanel
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0001677C File Offset: 0x0001497C
		public MultiplayerScoreboardStripedBackgroundWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00016788 File Offset: 0x00014988
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			if (base.ChildCount % 2 == 1)
			{
				child.Sprite = base.Context.SpriteData.GetSprite("BlankWhiteSquare_9");
				child.Color = Color.ConvertStringToColor("#000000FF");
				child.AlphaFactor = 0.2f;
			}
		}
	}
}
