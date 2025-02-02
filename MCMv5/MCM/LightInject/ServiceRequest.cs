using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000FC RID: 252
	[ExcludeFromCodeCoverage]
	internal class ServiceRequest
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00013600 File Offset: 0x00011800
		public ServiceRequest(Type serviceType, string serviceName, IServiceFactory serviceFactory)
		{
			this.ServiceType = serviceType;
			this.ServiceName = serviceName;
			this.ServiceFactory = serviceFactory;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00013622 File Offset: 0x00011822
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001362A File Offset: 0x0001182A
		public Type ServiceType { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00013633 File Offset: 0x00011833
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001363B File Offset: 0x0001183B
		public string ServiceName { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00013644 File Offset: 0x00011844
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001364C File Offset: 0x0001184C
		public IServiceFactory ServiceFactory { get; private set; }
	}
}
