using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200061E RID: 1566
	public enum AntiCheatClientViolationType
	{
		// Token: 0x0400120C RID: 4620
		Invalid,
		// Token: 0x0400120D RID: 4621
		IntegrityCatalogNotFound,
		// Token: 0x0400120E RID: 4622
		IntegrityCatalogError,
		// Token: 0x0400120F RID: 4623
		IntegrityCatalogCertificateRevoked,
		// Token: 0x04001210 RID: 4624
		IntegrityCatalogMissingMainExecutable,
		// Token: 0x04001211 RID: 4625
		GameFileMismatch,
		// Token: 0x04001212 RID: 4626
		RequiredGameFileNotFound,
		// Token: 0x04001213 RID: 4627
		UnknownGameFileForbidden,
		// Token: 0x04001214 RID: 4628
		SystemFileUntrusted,
		// Token: 0x04001215 RID: 4629
		ForbiddenModuleLoaded,
		// Token: 0x04001216 RID: 4630
		CorruptedMemory,
		// Token: 0x04001217 RID: 4631
		ForbiddenToolDetected,
		// Token: 0x04001218 RID: 4632
		InternalAntiCheatViolation,
		// Token: 0x04001219 RID: 4633
		CorruptedNetworkMessageFlow,
		// Token: 0x0400121A RID: 4634
		VirtualMachineNotAllowed,
		// Token: 0x0400121B RID: 4635
		ForbiddenSystemConfiguration
	}
}
