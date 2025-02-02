using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000094 RID: 148
	public interface IMethodSignature : IMetadataTokenProvider
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000502 RID: 1282
		// (set) Token: 0x06000503 RID: 1283
		bool HasThis { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000504 RID: 1284
		// (set) Token: 0x06000505 RID: 1285
		bool ExplicitThis { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000506 RID: 1286
		// (set) Token: 0x06000507 RID: 1287
		MethodCallingConvention CallingConvention { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000508 RID: 1288
		bool HasParameters { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000509 RID: 1289
		Collection<ParameterDefinition> Parameters { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600050A RID: 1290
		// (set) Token: 0x0600050B RID: 1291
		TypeReference ReturnType { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600050C RID: 1292
		MethodReturnType MethodReturnType { get; }
	}
}
