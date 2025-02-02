using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tournament
{
	// Token: 0x0200004B RID: 75
	public class TournamentMatchWidget : Widget
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000D408 File Offset: 0x0000B608
		public TournamentMatchWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000D411 File Offset: 0x0000B611
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000D41C File Offset: 0x0000B61C
		[Editor(false)]
		public int State
		{
			get
			{
				return this._state;
			}
			set
			{
				if (this._state != value)
				{
					this._state = value;
					foreach (Widget widget in base.AllChildren)
					{
						TournamentParticipantBrushWidget tournamentParticipantBrushWidget = widget as TournamentParticipantBrushWidget;
						if (tournamentParticipantBrushWidget != null)
						{
							tournamentParticipantBrushWidget.MatchState = this.State;
						}
					}
				}
			}
		}

		// Token: 0x040001BD RID: 445
		private int _state;
	}
}
