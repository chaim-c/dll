using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A1 RID: 1441
	public struct PinGrantInfo
	{
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x00036829 File Offset: 0x00034A29
		// (set) Token: 0x060024D4 RID: 9428 RVA: 0x00036831 File Offset: 0x00034A31
		public Utf8String UserCode { get; set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x0003683A File Offset: 0x00034A3A
		// (set) Token: 0x060024D6 RID: 9430 RVA: 0x00036842 File Offset: 0x00034A42
		public Utf8String VerificationURI { get; set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x0003684B File Offset: 0x00034A4B
		// (set) Token: 0x060024D8 RID: 9432 RVA: 0x00036853 File Offset: 0x00034A53
		public int ExpiresIn { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x0003685C File Offset: 0x00034A5C
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x00036864 File Offset: 0x00034A64
		public Utf8String VerificationURIComplete { get; set; }

		// Token: 0x060024DB RID: 9435 RVA: 0x0003686D File Offset: 0x00034A6D
		internal void Set(ref PinGrantInfoInternal other)
		{
			this.UserCode = other.UserCode;
			this.VerificationURI = other.VerificationURI;
			this.ExpiresIn = other.ExpiresIn;
			this.VerificationURIComplete = other.VerificationURIComplete;
		}
	}
}
