using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Extensions;
using TaleWorlds.MountAndBlade;

namespace MCM.Internal.Extensions
{
	// Token: 0x02000012 RID: 18
	public static class ServiceCollectionExtensions
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003534 File Offset: 0x00001734
		[NullableContext(1)]
		public static IGenericServiceContainer GetServiceContainer(this MBSubModuleBase _)
		{
			return ServiceCollectionExtensions.ServiceContainer;
		}
	}
}
