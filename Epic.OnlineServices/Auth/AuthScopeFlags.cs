using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000573 RID: 1395
	[Flags]
	public enum AuthScopeFlags
	{
		// Token: 0x04000FBE RID: 4030
		NoFlags = 0,
		// Token: 0x04000FBF RID: 4031
		BasicProfile = 1,
		// Token: 0x04000FC0 RID: 4032
		FriendsList = 2,
		// Token: 0x04000FC1 RID: 4033
		Presence = 4,
		// Token: 0x04000FC2 RID: 4034
		FriendsManagement = 8,
		// Token: 0x04000FC3 RID: 4035
		Email = 16,
		// Token: 0x04000FC4 RID: 4036
		Country = 32
	}
}
