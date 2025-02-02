using System;
using Galaxy.Api;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x0200000B RID: 11
	public class UserInformationRetrieveListener : IUserInformationRetrieveListener
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000319C File Offset: 0x0000139C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000031A4 File Offset: 0x000013A4
		public bool GotResult { get; private set; }

		// Token: 0x06000079 RID: 121 RVA: 0x000031AD File Offset: 0x000013AD
		public override void OnUserInformationRetrieveFailure(GalaxyID userID, IUserInformationRetrieveListener.FailureReason failureReason)
		{
			this.GotResult = true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031B6 File Offset: 0x000013B6
		public override void OnUserInformationRetrieveSuccess(GalaxyID userID)
		{
			this.GotResult = true;
		}
	}
}
