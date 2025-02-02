using System;

namespace MCM.LightInject
{
	// Token: 0x020000DE RID: 222
	internal interface ICompositionRootExecutor
	{
		// Token: 0x0600049C RID: 1180
		void Execute(Type compositionRootType);

		// Token: 0x0600049D RID: 1181
		void Execute<TCompositionRoot>(TCompositionRoot compositionRoot) where TCompositionRoot : ICompositionRoot;
	}
}
