using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000247 RID: 583
	internal class OnPlatformRequestedMultiplayerJob : Job
	{
		// Token: 0x06001F65 RID: 8037 RVA: 0x0006F3AC File Offset: 0x0006D5AC
		public override void DoJob(float dt)
		{
			base.DoJob(dt);
			if (MBGameManager.Current != null)
			{
				MBGameManager.Current.OnPlatformRequestedMultiplayer();
			}
			else if (GameStateManager.Current != null && GameStateManager.Current.ActiveState != null)
			{
				GameStateManager.Current.CleanStates(0);
			}
			base.Finished = true;
		}
	}
}
