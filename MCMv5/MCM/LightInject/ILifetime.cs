using System;

namespace MCM.LightInject
{
	// Token: 0x020000CD RID: 205
	internal interface ILifetime
	{
		// Token: 0x06000485 RID: 1157
		object GetInstance(Func<object> createInstance, Scope scope);
	}
}
