using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B1 RID: 177
	public class MultiplayerLobbyArmoryCosmeticTierVisualBrushWidget : BrushWidget
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x0001ACDF File Offset: 0x00018EDF
		public MultiplayerLobbyArmoryCosmeticTierVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		private void UpdateVisual()
		{
			switch (this._rarity)
			{
			case 0:
			case 1:
				this.SetState("Common");
				return;
			case 2:
				this.SetState("Rare");
				return;
			case 3:
				this.SetState("Unique");
				return;
			default:
				return;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001AD3E File Offset: 0x00018F3E
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0001AD46 File Offset: 0x00018F46
		[Editor(false)]
		public int Rarity
		{
			get
			{
				return this._rarity;
			}
			set
			{
				if (this._rarity != value)
				{
					this._rarity = value;
					base.OnPropertyChanged(value, "Rarity");
					this.UpdateVisual();
				}
			}
		}

		// Token: 0x0400044D RID: 1101
		private int _rarity = -1;
	}
}
