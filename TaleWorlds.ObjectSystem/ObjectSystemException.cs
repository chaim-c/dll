using System;

namespace TaleWorlds.ObjectSystem
{
	// Token: 0x02000010 RID: 16
	public class ObjectSystemException : Exception
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000449F File Offset: 0x0000269F
		internal ObjectSystemException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000044A9 File Offset: 0x000026A9
		internal ObjectSystemException(string message) : base(message)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000044B2 File Offset: 0x000026B2
		internal ObjectSystemException()
		{
		}
	}
}
