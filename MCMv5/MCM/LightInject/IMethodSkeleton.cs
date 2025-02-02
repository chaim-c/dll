using System;

namespace MCM.LightInject
{
	// Token: 0x020000E0 RID: 224
	internal interface IMethodSkeleton
	{
		// Token: 0x060004AC RID: 1196
		IEmitter GetEmitter();

		// Token: 0x060004AD RID: 1197
		Delegate CreateDelegate(Type delegateType);
	}
}
