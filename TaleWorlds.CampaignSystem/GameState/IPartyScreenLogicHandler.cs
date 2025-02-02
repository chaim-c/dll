using System;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200033F RID: 831
	public interface IPartyScreenLogicHandler
	{
		// Token: 0x06002F11 RID: 12049
		void RequestUserInput(string text, Action accept, Action cancel);
	}
}
