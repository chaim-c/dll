using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000124 RID: 292
	[ExcludeFromCodeCoverage]
	internal static class ServiceRequestHelper
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public static ServiceRequest CreateServiceRequest<TService>(string serviceName, IServiceFactory serviceFactory)
		{
			return new ServiceRequest(typeof(TService), serviceName, serviceFactory);
		}

		// Token: 0x0400021C RID: 540
		public static readonly MethodInfo CreateServiceRequestMethod = typeof(ServiceRequestHelper).GetTypeInfo().GetDeclaredMethod("CreateServiceRequest");
	}
}
