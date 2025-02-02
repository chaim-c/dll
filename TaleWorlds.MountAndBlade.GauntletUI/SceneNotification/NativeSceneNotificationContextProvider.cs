using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.GauntletUI.SceneNotification
{
	// Token: 0x0200001F RID: 31
	public class NativeSceneNotificationContextProvider : ISceneNotificationContextProvider
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000855A File Offset: 0x0000675A
		public bool IsContextAllowed(SceneNotificationData.RelevantContextType relevantType)
		{
			return relevantType != SceneNotificationData.RelevantContextType.Mission || GameStateManager.Current.ActiveState is MissionState;
		}
	}
}
