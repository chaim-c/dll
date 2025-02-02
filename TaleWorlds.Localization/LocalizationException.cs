using System;

namespace TaleWorlds.Localization
{
	// Token: 0x02000005 RID: 5
	public class LocalizationException : Exception
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002BE5 File Offset: 0x00000DE5
		public LocalizationException()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BED File Offset: 0x00000DED
		public LocalizationException(string message) : base(message)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public LocalizationException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
