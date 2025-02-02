using System;

namespace MCM.LightInject
{
	// Token: 0x020000DA RID: 218
	internal interface IScopeManager
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000493 RID: 1171
		// (set) Token: 0x06000494 RID: 1172
		Scope CurrentScope { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000495 RID: 1173
		IServiceFactory ServiceFactory { get; }

		// Token: 0x06000496 RID: 1174
		Scope BeginScope();

		// Token: 0x06000497 RID: 1175
		void EndScope(Scope scope);
	}
}
