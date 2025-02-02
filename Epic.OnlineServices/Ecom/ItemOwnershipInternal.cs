using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C1 RID: 1217
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ItemOwnershipInternal : IGettable<ItemOwnership>, ISettable<ItemOwnership>, IDisposable
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0002ED20 File Offset: 0x0002CF20
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x0002ED41 File Offset: 0x0002CF41
		public Utf8String Id
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Id, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Id);
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0002ED54 File Offset: 0x0002CF54
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x0002ED6C File Offset: 0x0002CF6C
		public OwnershipStatus OwnershipStatus
		{
			get
			{
				return this.m_OwnershipStatus;
			}
			set
			{
				this.m_OwnershipStatus = value;
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0002ED76 File Offset: 0x0002CF76
		public void Set(ref ItemOwnership other)
		{
			this.m_ApiVersion = 1;
			this.Id = other.Id;
			this.OwnershipStatus = other.OwnershipStatus;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		public void Set(ref ItemOwnership? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Id = other.Value.Id;
				this.OwnershipStatus = other.Value.OwnershipStatus;
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0002EDE7 File Offset: 0x0002CFE7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Id);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0002EDF6 File Offset: 0x0002CFF6
		public void Get(out ItemOwnership output)
		{
			output = default(ItemOwnership);
			output.Set(ref this);
		}

		// Token: 0x04000E14 RID: 3604
		private int m_ApiVersion;

		// Token: 0x04000E15 RID: 3605
		private IntPtr m_Id;

		// Token: 0x04000E16 RID: 3606
		private OwnershipStatus m_OwnershipStatus;
	}
}
