using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000588 RID: 1416
	public enum LoginCredentialType
	{
		// Token: 0x04001006 RID: 4102
		Password,
		// Token: 0x04001007 RID: 4103
		ExchangeCode,
		// Token: 0x04001008 RID: 4104
		PersistentAuth,
		// Token: 0x04001009 RID: 4105
		DeviceCode,
		// Token: 0x0400100A RID: 4106
		Developer,
		// Token: 0x0400100B RID: 4107
		RefreshToken,
		// Token: 0x0400100C RID: 4108
		AccountPortal,
		// Token: 0x0400100D RID: 4109
		ExternalAuth
	}
}
