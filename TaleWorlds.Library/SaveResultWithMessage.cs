using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000084 RID: 132
	public struct SaveResultWithMessage
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000FB62 File Offset: 0x0000DD62
		public static SaveResultWithMessage Default
		{
			get
			{
				return new SaveResultWithMessage(SaveResult.Success, string.Empty);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000FB6F File Offset: 0x0000DD6F
		public SaveResultWithMessage(SaveResult result, string message)
		{
			this.SaveResult = result;
			this.Message = message;
		}

		// Token: 0x0400015E RID: 350
		public readonly SaveResult SaveResult;

		// Token: 0x0400015F RID: 351
		public readonly string Message;
	}
}
