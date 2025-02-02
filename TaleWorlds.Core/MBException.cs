using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Core
{
	// Token: 0x02000098 RID: 152
	public class MBException : ApplicationException
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0001BBA3 File Offset: 0x00019DA3
		public MBException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001BBAD File Offset: 0x00019DAD
		public MBException(string message) : base(message)
		{
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001BBB6 File Offset: 0x00019DB6
		public MBException()
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001BBBE File Offset: 0x00019DBE
		public MBException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
