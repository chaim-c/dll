using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005D7 RID: 1495
	public enum AntiCheatCommonClientActionReason
	{
		// Token: 0x040010D3 RID: 4307
		Invalid,
		// Token: 0x040010D4 RID: 4308
		InternalError,
		// Token: 0x040010D5 RID: 4309
		InvalidMessage,
		// Token: 0x040010D6 RID: 4310
		AuthenticationFailed,
		// Token: 0x040010D7 RID: 4311
		NullClient,
		// Token: 0x040010D8 RID: 4312
		HeartbeatTimeout,
		// Token: 0x040010D9 RID: 4313
		ClientViolation,
		// Token: 0x040010DA RID: 4314
		BackendViolation,
		// Token: 0x040010DB RID: 4315
		TemporaryCooldown,
		// Token: 0x040010DC RID: 4316
		TemporaryBanned,
		// Token: 0x040010DD RID: 4317
		PermanentBanned
	}
}
