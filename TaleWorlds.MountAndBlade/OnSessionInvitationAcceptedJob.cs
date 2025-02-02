using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.PlatformService;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000248 RID: 584
	internal class OnSessionInvitationAcceptedJob : Job
	{
		// Token: 0x06001F67 RID: 8039 RVA: 0x0006F400 File Offset: 0x0006D600
		public OnSessionInvitationAcceptedJob(SessionInvitationType sessionInvitationType)
		{
			this._sessionInvitationType = sessionInvitationType;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0006F410 File Offset: 0x0006D610
		public override void DoJob(float dt)
		{
			base.DoJob(dt);
			if (MBGameManager.Current != null)
			{
				MBGameManager.Current.OnSessionInvitationAccepted(this._sessionInvitationType);
			}
			else if (GameStateManager.Current != null && GameStateManager.Current.ActiveState != null)
			{
				GameStateManager.Current.CleanStates(0);
			}
			base.Finished = true;
		}

		// Token: 0x04000B8F RID: 2959
		private readonly SessionInvitationType _sessionInvitationType;
	}
}
