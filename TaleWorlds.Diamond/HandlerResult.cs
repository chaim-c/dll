using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200000C RID: 12
	public class HandlerResult
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028CE File Offset: 0x00000ACE
		public bool IsSuccessful { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000028D6 File Offset: 0x00000AD6
		public string Error { get; }

		// Token: 0x0600003D RID: 61 RVA: 0x000028DE File Offset: 0x00000ADE
		protected HandlerResult(bool isSuccessful, string error = null)
		{
			this.IsSuccessful = isSuccessful;
			this.Error = error;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028F4 File Offset: 0x00000AF4
		public static HandlerResult CreateSuccessful()
		{
			return new HandlerResult(true, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028FD File Offset: 0x00000AFD
		public static HandlerResult CreateFailed(string error)
		{
			return new HandlerResult(false, error);
		}
	}
}
