using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using MCM.Common;

namespace MCM.UI.Exceptions
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class MCMUIException : MCMException
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00008265 File Offset: 0x00006465
		public MCMUIException()
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000826F File Offset: 0x0000646F
		public MCMUIException(string message) : base(message)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000827A File Offset: 0x0000647A
		public MCMUIException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008286 File Offset: 0x00006486
		protected MCMUIException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
