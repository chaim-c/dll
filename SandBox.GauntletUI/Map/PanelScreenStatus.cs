using System;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000023 RID: 35
	public readonly struct PanelScreenStatus
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public PanelScreenStatus(ScreenBase screen)
		{
			this.IsCharacterScreenOpen = false;
			this.IsPartyScreenOpen = false;
			this.IsQuestsScreenOpen = false;
			this.IsInventoryScreenOpen = false;
			this.IsClanScreenOpen = false;
			this.IsKingdomScreenOpen = false;
			this.IsAnyPanelScreenOpen = true;
			this.IsCurrentScreenLocksNavigation = false;
			if (screen is GauntletCharacterDeveloperScreen)
			{
				this.IsCharacterScreenOpen = true;
				return;
			}
			if (screen is GauntletPartyScreen)
			{
				this.IsPartyScreenOpen = true;
				return;
			}
			if (screen is GauntletQuestsScreen)
			{
				this.IsQuestsScreenOpen = true;
				return;
			}
			if (screen is GauntletInventoryScreen)
			{
				this.IsInventoryScreenOpen = true;
				return;
			}
			if (screen is GauntletClanScreen)
			{
				this.IsClanScreenOpen = true;
				return;
			}
			GauntletKingdomScreen gauntletKingdomScreen;
			if ((gauntletKingdomScreen = (screen as GauntletKingdomScreen)) != null)
			{
				this.IsKingdomScreenOpen = true;
				this.IsCurrentScreenLocksNavigation = (gauntletKingdomScreen != null && gauntletKingdomScreen.IsMakingDecision);
				return;
			}
			this.IsAnyPanelScreenOpen = false;
		}

		// Token: 0x0400009D RID: 157
		public readonly bool IsCharacterScreenOpen;

		// Token: 0x0400009E RID: 158
		public readonly bool IsPartyScreenOpen;

		// Token: 0x0400009F RID: 159
		public readonly bool IsQuestsScreenOpen;

		// Token: 0x040000A0 RID: 160
		public readonly bool IsInventoryScreenOpen;

		// Token: 0x040000A1 RID: 161
		public readonly bool IsClanScreenOpen;

		// Token: 0x040000A2 RID: 162
		public readonly bool IsKingdomScreenOpen;

		// Token: 0x040000A3 RID: 163
		public readonly bool IsAnyPanelScreenOpen;

		// Token: 0x040000A4 RID: 164
		public readonly bool IsCurrentScreenLocksNavigation;
	}
}
