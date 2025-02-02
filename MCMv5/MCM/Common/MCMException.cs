using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MCM.Common
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class MCMException : Exception
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000376A File Offset: 0x0000196A
		public MCMException()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003774 File Offset: 0x00001974
		public MCMException(string message) : base(message)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000377F File Offset: 0x0000197F
		public MCMException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000378B File Offset: 0x0000198B
		protected MCMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
