using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200016D RID: 365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerSanctionInternal : IGettable<PlayerSanction>, ISettable<PlayerSanction>, IDisposable
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0000F71C File Offset: 0x0000D91C
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0000F734 File Offset: 0x0000D934
		public long TimePlaced
		{
			get
			{
				return this.m_TimePlaced;
			}
			set
			{
				this.m_TimePlaced = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0000F740 File Offset: 0x0000D940
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0000F761 File Offset: 0x0000D961
		public Utf8String Action
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Action, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Action);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0000F774 File Offset: 0x0000D974
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0000F78C File Offset: 0x0000D98C
		public long TimeExpires
		{
			get
			{
				return this.m_TimeExpires;
			}
			set
			{
				this.m_TimeExpires = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0000F798 File Offset: 0x0000D998
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0000F7B9 File Offset: 0x0000D9B9
		public Utf8String ReferenceId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ReferenceId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ReferenceId);
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000F7C9 File Offset: 0x0000D9C9
		public void Set(ref PlayerSanction other)
		{
			this.m_ApiVersion = 2;
			this.TimePlaced = other.TimePlaced;
			this.Action = other.Action;
			this.TimeExpires = other.TimeExpires;
			this.ReferenceId = other.ReferenceId;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000F808 File Offset: 0x0000DA08
		public void Set(ref PlayerSanction? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.TimePlaced = other.Value.TimePlaced;
				this.Action = other.Value.Action;
				this.TimeExpires = other.Value.TimeExpires;
				this.ReferenceId = other.Value.ReferenceId;
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000F87D File Offset: 0x0000DA7D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Action);
			Helper.Dispose(ref this.m_ReferenceId);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000F898 File Offset: 0x0000DA98
		public void Get(out PlayerSanction output)
		{
			output = default(PlayerSanction);
			output.Set(ref this);
		}

		// Token: 0x040004CB RID: 1227
		private int m_ApiVersion;

		// Token: 0x040004CC RID: 1228
		private long m_TimePlaced;

		// Token: 0x040004CD RID: 1229
		private IntPtr m_Action;

		// Token: 0x040004CE RID: 1230
		private long m_TimeExpires;

		// Token: 0x040004CF RID: 1231
		private IntPtr m_ReferenceId;
	}
}
