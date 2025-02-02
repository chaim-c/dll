using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000004 RID: 4
	public static class Extensions
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public static bool IsOperationComplete(this Result result)
		{
			return Common.IsOperationComplete(result);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003CEC File Offset: 0x00001EEC
		public static string ToHexString(this byte[] byteArray)
		{
			ArraySegment<byte> byteArray2 = new ArraySegment<byte>(byteArray);
			return Common.ToString(byteArray2);
		}
	}
}
