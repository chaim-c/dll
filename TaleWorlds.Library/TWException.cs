using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Library
{
	// Token: 0x02000090 RID: 144
	public class TWException : ApplicationException
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00010DDE File Offset: 0x0000EFDE
		public TWException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00010DE8 File Offset: 0x0000EFE8
		public TWException(string message) : base(message)
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00010DF1 File Offset: 0x0000EFF1
		public TWException()
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00010DF9 File Offset: 0x0000EFF9
		public TWException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
