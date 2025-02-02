using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;

namespace SandBox.GauntletUI
{
	// Token: 0x02000012 RID: 18
	public class SandboxSceneNotificationContextProvider : ISceneNotificationContextProvider
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00007511 File Offset: 0x00005711
		public bool IsContextAllowed(SceneNotificationData.RelevantContextType relevantType)
		{
			return relevantType != SceneNotificationData.RelevantContextType.Map || GameStateManager.Current.ActiveState is MapState;
		}
	}
}
