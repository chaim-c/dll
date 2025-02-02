using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000360 RID: 864
	public struct LeaveLobbyCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00022003 File Offset: 0x00020203
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x0002200B File Offset: 0x0002020B
		public Result ResultCode { get; set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00022014 File Offset: 0x00020214
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x0002201C File Offset: 0x0002021C
		public object ClientData { get; set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00022025 File Offset: 0x00020225
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x0002202D File Offset: 0x0002022D
		public Utf8String LobbyId { get; set; }

		// Token: 0x060016EA RID: 5866 RVA: 0x00022038 File Offset: 0x00020238
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00022055 File Offset: 0x00020255
		internal void Set(ref LeaveLobbyCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
