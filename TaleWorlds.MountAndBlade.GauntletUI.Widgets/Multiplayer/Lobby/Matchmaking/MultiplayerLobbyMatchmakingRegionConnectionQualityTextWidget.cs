using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Matchmaking
{
	// Token: 0x020000A4 RID: 164
	public class MultiplayerLobbyMatchmakingRegionConnectionQualityTextWidget : TextWidget
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x00019329 File Offset: 0x00017529
		public MultiplayerLobbyMatchmakingRegionConnectionQualityTextWidget(UIContext context) : base(context)
		{
			base.AddState("PoorQuality");
			base.AddState("AverageQuality");
			base.AddState("GoodQuality");
			this.ConnectionQualityLevelUpdated();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001935C File Offset: 0x0001755C
		private void ConnectionQualityLevelUpdated()
		{
			switch (this.ConnectionQualityLevel)
			{
			case 0:
				this.SetState("PoorQuality");
				return;
			case 1:
				this.SetState("AverageQuality");
				return;
			case 2:
				this.SetState("GoodQuality");
				return;
			default:
				this.SetState("Default");
				return;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000193B3 File Offset: 0x000175B3
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x000193BB File Offset: 0x000175BB
		[Editor(false)]
		public int ConnectionQualityLevel
		{
			get
			{
				return this._connectionQualityLevel;
			}
			set
			{
				if (this._connectionQualityLevel != value)
				{
					this._connectionQualityLevel = value;
					base.OnPropertyChanged(value, "ConnectionQualityLevel");
					this.ConnectionQualityLevelUpdated();
				}
			}
		}

		// Token: 0x040003FE RID: 1022
		private int _connectionQualityLevel;
	}
}
