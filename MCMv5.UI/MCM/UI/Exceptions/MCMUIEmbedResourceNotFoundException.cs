using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using MCM.Common;

namespace MCM.UI.Exceptions
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class MCMUIEmbedResourceNotFoundException : MCMException
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00008238 File Offset: 0x00006438
		public MCMUIEmbedResourceNotFoundException()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008242 File Offset: 0x00006442
		public MCMUIEmbedResourceNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000824D File Offset: 0x0000644D
		public MCMUIEmbedResourceNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008259 File Offset: 0x00006459
		protected MCMUIEmbedResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
