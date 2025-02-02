using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000206 RID: 518
	public struct QueryJoinRoomTokenCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00015762 File Offset: 0x00013962
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0001576A File Offset: 0x0001396A
		public Result ResultCode { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00015773 File Offset: 0x00013973
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0001577B File Offset: 0x0001397B
		public object ClientData { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00015784 File Offset: 0x00013984
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0001578C File Offset: 0x0001398C
		public Utf8String RoomName { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00015795 File Offset: 0x00013995
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0001579D File Offset: 0x0001399D
		public Utf8String ClientBaseUrl { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x000157A6 File Offset: 0x000139A6
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x000157AE File Offset: 0x000139AE
		public uint QueryId { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x000157B7 File Offset: 0x000139B7
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x000157BF File Offset: 0x000139BF
		public uint TokenCount { get; set; }

		// Token: 0x06000E91 RID: 3729 RVA: 0x000157C8 File Offset: 0x000139C8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000157E8 File Offset: 0x000139E8
		internal void Set(ref QueryJoinRoomTokenCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.RoomName = other.RoomName;
			this.ClientBaseUrl = other.ClientBaseUrl;
			this.QueryId = other.QueryId;
			this.TokenCount = other.TokenCount;
		}
	}
}
